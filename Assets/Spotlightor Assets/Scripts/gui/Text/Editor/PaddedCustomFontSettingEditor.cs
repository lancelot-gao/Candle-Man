using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PaddedCustomFontSetting))]
public class PaddedCustomFontSettingEditor : Editor
{
	public const string PaddedFontPostfix = "_padded";
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Upadte Custom Font")) {
			Update (target as PaddedCustomFontSetting);
		}
	}
	
	private static void Update (PaddedCustomFontSetting customFontSetting)
	{
		Font sourceFont = customFontSetting.sourceFont;
		Font customFont = customFontSetting.customFont;
		if (sourceFont != null && customFont != null) {
			int maxTextureSize = customFontSetting.maxTextureSize;
			int padding = customFontSetting.padding;
		
			string sourceFontAssetPath = AssetDatabase.GetAssetPath (sourceFont);
			TrueTypeFontImporter sourceFontImporter = TrueTypeFontImporter.GetAtPath (sourceFontAssetPath) as TrueTypeFontImporter;
			string sourceFontFolderPath = sourceFontAssetPath.Substring (0, sourceFontAssetPath.LastIndexOf ("/") + 1);
			string editableFontAssetPath = sourceFontFolderPath + sourceFont.name + "_editable.fontsettings";
		
			Font editableFontCopy = sourceFontImporter.GenerateEditableFont (editableFontAssetPath);
		
			string editableFontTextureAssetPath = AssetDatabase.GetAssetPath (editableFontCopy.material.mainTexture);
			TextureImporter editableFontTextureImporter = TextureImporter.GetAtPath (editableFontTextureAssetPath) as TextureImporter;
			editableFontTextureImporter.isReadable = true;
			AssetDatabase.ImportAsset (editableFontTextureAssetPath);
		
			Texture2D fontTexture = editableFontCopy.material.mainTexture as Texture2D;
			
			Texture2D[] charTextures = new Texture2D[editableFontCopy.characterInfo.Length];
			for (int k = 0; k < editableFontCopy.characterInfo.Length; k++) {
				CharacterInfo charInfo = editableFontCopy.characterInfo [k];
				
				int charPixelStartX = Mathf.RoundToInt (charInfo.uv.x * (fontTexture.width));
				int charPixelWidth = Mathf.FloorToInt (charInfo.uv.width * (fontTexture.width));
				int charPixelStartY = Mathf.RoundToInt ((charInfo.uv.y) * (fontTexture.height));
				int charPixelHeight = Mathf.FloorToInt (charInfo.uv.height * (fontTexture.height));
				
				int newTextureWidth = Mathf.Abs (charPixelWidth) + padding * 2;
				int newTextureHeight = Mathf.Abs (charPixelHeight) + padding * 2;
				if (charInfo.flipped) {
					int temp = newTextureWidth;
					newTextureWidth = newTextureHeight;
					newTextureHeight = temp;
				}
				
				if (charPixelHeight < 0)
					charPixelStartY --;
				if (charPixelWidth < 0)
					charPixelStartX--;
				
				Texture2D charTexture = new Texture2D (newTextureWidth, newTextureHeight, TextureFormat.ARGB32, false);
				Color32[] defaultPixels = new Color32[charTexture.width * charTexture.height];
				for (int i = 0; i < defaultPixels.Length; i++)
					defaultPixels [i] = new Color32 (0, 0, 0, 0);
				charTexture.SetPixels32 (defaultPixels);
				
				for (int i= 0; i < Mathf.Abs(charPixelWidth); i++) {
					for (int j = 0; j < Mathf.Abs(charPixelHeight); j++) {
						int x = charPixelStartX + (charPixelWidth > 0 ? i : -i);
						int y = charPixelStartY + (charPixelHeight > 0 ? j : -j);
						
						Color pixel = fontTexture.GetPixel (x, y);
						int newCharX = padding + i;
						int newCharY = padding + j;
						if (!charInfo.flipped)
							charTexture.SetPixel (newCharX, newCharY, pixel);
						else
							charTexture.SetPixel (newCharY, newCharX, pixel);
					}
				}
				charTextures [k] = charTexture;
			}
			
			Texture2D newFontTexture = new Texture2D (maxTextureSize, maxTextureSize, TextureFormat.ARGB32, false);
			Rect[] newCharUvs = newFontTexture.PackTextures (charTextures, 0, maxTextureSize, false);
			
			CharacterInfo[] newCharInfos = editableFontCopy.characterInfo;
			for (int i = 0; i < editableFontCopy.characterInfo.Length; i++) {
				newCharInfos [i].uv = newCharUvs [i];
				newCharInfos [i].flipped = false;
			}
			customFont.characterInfo = newCharInfos;
			EditorUtility.SetDirty (customFont);
			
			string path = AssetDatabase.GetAssetPath (sourceFont);
			string currentFolderPath = path.Substring (0, path.LastIndexOf ("/") + 1);
			string textureAssetPath = currentFolderPath + sourceFont.name + PaddedFontPostfix + ".png";
			
			SaveAssetUtility.SaveTexture (newFontTexture, textureAssetPath);
			
			AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (editableFontCopy.material.mainTexture));
			AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (editableFontCopy.material));
			AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (editableFontCopy));
			foreach (Texture2D charTexture in charTextures) {
				DestroyImmediate (charTexture);
			}
		} else
			Debug.LogError ("Some font is null");
	}
}
