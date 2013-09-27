using UnityEngine;
using UnityEditor;
using System.Collections;

public class SkyboxBaker : ScriptableObject
{
	
	public const int TextureResolution = 1024;
	public const string TexturesBasePath = "Assets/Textures/Skyboxes";
	public const string SkyboxShaderName = "RenderFX/Skybox";
	public static string[] SkyboxTextureSuffixes = new string[]{"front", "right", "back", "left", "up", "down"};
	public static string[] SkyboxTextureShaderPropertys = new string[]{"_FrontTex", "_RightTex", "_BackTex", "_LeftTex", "_UpTex", "_DownTex"};
	public static Vector3[] RenderEulerRotations = new Vector3[]{new Vector3 (0, 0, 0), new Vector3 (0, -90, 0), new Vector3 (0, 180, 0), 
		new Vector3 (0, 90, 0), new Vector3 (-90, 0, 0), new Vector3 (90, 0, 0)};
    
	[MenuItem("Assets/Bake Skybox", false, 4)]
	public static void BakeSkybox ()
	{
		if (Selection.transforms.Length == 0) {
			Debug.LogWarning ("Select at least one scene object as skybox center!");
			return;
		}
		
		if (!System.IO.Directory.Exists (TexturesBasePath))
			System.IO.Directory.CreateDirectory (TexturesBasePath);
		
		foreach (Transform selectedTransform in Selection.transforms)
			RenderSkyboxToTextures (selectedTransform);
	}
	
	private static void RenderSkyboxToTextures (Transform skyboxRoot)
	{
		Camera renderCamera = skyboxRoot.camera;
		if (renderCamera == null) {
			GameObject go = new GameObject ("SkyboxCamera", typeof(Camera));
			go.transform.position = skyboxRoot.position;
			go.transform.eulerAngles = skyboxRoot.eulerAngles;
			renderCamera = go.camera;
			renderCamera.backgroundColor = Color.black;
			renderCamera.clearFlags = CameraClearFlags.Skybox;
		}
		renderCamera.fieldOfView = 90;    
		renderCamera.aspect = 1.0f;
		
		Material skyboxMaterial = new Material (Shader.Find(SkyboxShaderName));
		Vector3 originalEulerRotation = renderCamera.transform.eulerAngles;
		string skyboxBaseName = skyboxRoot.name.ToLower().Replace(" ", "_");
		for(int i = 0; i < RenderEulerRotations.Length; i++){
			renderCamera.transform.eulerAngles = originalEulerRotation + RenderEulerRotations[i];
			
			string textureAssetPath = System.IO.Path.Combine(TexturesBasePath,skyboxBaseName + "_" + SkyboxTextureSuffixes[i] + ".png");
			Texture2D faceTexture = RenderSkyboxFaceAndSaveToAsset(renderCamera, textureAssetPath);
			faceTexture.wrapMode = TextureWrapMode.Clamp;
			
			skyboxMaterial.SetTexture(SkyboxTextureShaderPropertys[i], faceTexture);
		}
		
        // save material
        string skyboxMaterialPath = System.IO.Path.Combine(TexturesBasePath, skyboxBaseName + "_skybox.mat");
        AssetDatabase.CreateAsset(skyboxMaterial, skyboxMaterialPath);
		
		if(renderCamera.transform != skyboxRoot){
			DestroyImmediate(renderCamera.gameObject);
		}else{
			renderCamera.transform.eulerAngles = originalEulerRotation;
		}
	}
	
	private static Texture2D RenderSkyboxFaceAndSaveToAsset (Camera skyboxCamera, string textureAssetPath)
	{
		RenderTexture rt = new RenderTexture (TextureResolution, TextureResolution, 24);
		skyboxCamera.targetTexture = rt;
		skyboxCamera.Render ();
		RenderTexture.active = rt;
        
		Texture2D faceTexture = new Texture2D (TextureResolution, TextureResolution, TextureFormat.RGB24, false);
		faceTexture.ReadPixels (new Rect (0, 0, TextureResolution, TextureResolution), 0, 0);
        
		RenderTexture.active = null;
		GameObject.DestroyImmediate (rt);
        
		System.IO.File.WriteAllBytes (textureAssetPath, faceTexture.EncodeToPNG ());
		AssetDatabase.ImportAsset (textureAssetPath, ImportAssetOptions.ForceUpdate);
		
		AssetDatabase.Refresh();
		
		return AssetDatabase.LoadAssetAtPath(textureAssetPath, typeof(Texture2D)) as Texture2D;
	}
}
