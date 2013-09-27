using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LocalizationEditorUtilities : ScriptableObject
{
	private const string LocalizationAssetsDefaultPath = "Assets";

	[MenuItem("Localization/Create Localization Asset")]
	public static void CreateLocalizationAsset ()
	{
		Object data = ScriptableObject.CreateInstance<LocalizationTextsAsset> ();
		string path = LocalizationAssetsDefaultPath + "/" + "localization.asset";
		AssetDatabase.CreateAsset (data, path);
		Selection.activeObject = AssetDatabase.LoadAssetAtPath (path, typeof(LocalizationTextsAsset));
	}

	[MenuItem("Localization/Update fonts for ALL")]
	public static void UpdateFontCharactersForAll ()
	{
		List<TrueTypeFontImporter> modifiedFontImporters = new List<TrueTypeFontImporter> ();
		List<LocalizationTextsAsset> assets = LoadAllLocalizationTextsAssets ();
		string defaultString = "";
		foreach (LocalizationTextsAsset asset in assets) {
			string assetAllText = asset.GetAllTexts ();
			foreach (Font assetFont in asset.fonts) {
				TrueTypeFontImporter fontImporter = TrueTypeFontImporter.GetAtPath (AssetDatabase.GetAssetPath (assetFont)) as TrueTypeFontImporter;
				if (modifiedFontImporters.Contains (fontImporter) == false) {
					fontImporter.customCharacters = defaultString;
					modifiedFontImporters.Add (fontImporter);
				}
				for (int i =0; i<assetAllText.Length; i++) {
					if (fontImporter.customCharacters.IndexOf (assetAllText [i]) == -1)
						fontImporter.customCharacters += assetAllText [i];
				}
			}
		}
		foreach (TrueTypeFontImporter fontImporter in modifiedFontImporters) {
			Debug.Log ("Reimport Font " + fontImporter.assetPath + " with characters count: " + fontImporter.customCharacters.Length.ToString ());
			AssetDatabase.ImportAsset (fontImporter.assetPath);
		}
		modifiedFontImporters = null;
	}
	
	private static List<LocalizationTextsAsset> LoadAllLocalizationTextsAssets ()
	{
		List<LocalizationTextsAsset> result = new List<LocalizationTextsAsset> ();
		string[] allAssetPaths = AssetDatabase.GetAllAssetPaths ();
		foreach (string assetPath in allAssetPaths) {
			if (assetPath.Substring (assetPath.Length - 5) == "asset") {
				LocalizationTextsAsset asset = AssetDatabase.LoadAssetAtPath (assetPath, typeof(LocalizationTextsAsset)) as LocalizationTextsAsset;
				if (asset != null) 
					result.Add (asset);
			}
		}
		Debug.Log (string.Format ("Load {0} LocalizationTextsAsset.", result.Count));
		return result;
	}
}
