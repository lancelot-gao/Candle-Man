using UnityEngine;
using UnityEditor;
using System.Collections;

public class ProjectAssetsFolderStructureCreator : MonoBehaviour
{
	public static string ProjectAssetsFolderPath {
		get { return string.Format ("Assets/_{0} Project Assets", PlayerSettings.productName);}
	}
	
	public static string[] AssetFolderNames = new string[]{
		"3D Arts", "Animations", "Editor", 
		"Fonts", "Materials", "Prefabs", 
		"Resources", "Scenes", "Scripts", 
		"Shaders", "Sounds", "Textures", 
		"ScriptableObjects", "Physic Materials"
	};
	
	[MenuItem("Assets/Create Folder Structure")]
	public static void CreateProjectAssetsFolderStructure ()
	{
		string rootDir = string.Format ("{0}/_{1} Project Assets", Application.dataPath, PlayerSettings.productName);
		foreach (string assetFolderName in AssetFolderNames) {
			System.IO.Directory.CreateDirectory (string.Format ("{0}/{1}", rootDir, assetFolderName));
		}
		
		AssetDatabase.Refresh (ImportAssetOptions.Default);
	}
}
