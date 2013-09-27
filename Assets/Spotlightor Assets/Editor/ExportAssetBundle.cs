using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExportAssetBundle : ScriptableWizard
{
	public static string DefaultAssetBundlePath {
		get { return Application.dataPath + "/../Build/assets/unity3d/"; }
	}
	
	[MenuItem("Assets/Build AssetBundle", false, 100)]
	public static void ExportSelectedAssetsToBundle ()
	{
		string path = EditorUtility.SaveFilePanel ("Save Resource", "", "New Resource", "unity3d");
		if (path.Length != 0) {
			Object[] selection = Selection.GetFiltered (typeof(object), SelectionMode.DeepAssets);

			BuildPipeline.BuildAssetBundle (Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
			Selection.objects = selection;
		}
	}
	
	public string bundleName = "bundle";
	
	[MenuItem("Assets/Build AssetBundle to default path", false, 101)]
	public static void ExportSelectedAssetsToBundleWithName ()
	{
		ScriptableWizard.DisplayWizard<ExportAssetBundle> ("Export AssetBundle", "Export");
	}
	
	void OnWizardCreate ()
	{
		string pathName = DefaultAssetBundlePath + bundleName + ".unity3d";
		Object[] selection = Selection.GetFiltered (typeof(object), SelectionMode.DeepAssets);
		BuildPipeline.BuildAssetBundle (Selection.activeObject, selection, pathName, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		Selection.objects = selection;
	}
	
	[MenuItem("Assets/Build AssetBundle for EACH SINGLE", false, 102)]
	public static void ExportEachSelectedAssetToBundle ()
	{
		string path = DefaultAssetBundlePath;
		Object[] selection = Selection.GetFiltered (typeof(object), SelectionMode.TopLevel);
			
		foreach (Object obj in selection) {
			Object[] assets = new Object[1];
			assets [0] = obj;
			BuildPipeline.BuildAssetBundle (obj, assets, path + obj.name + ".unity3d", BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
		}
		Selection.objects = selection;
	}
	
	public static void ExportAssetBundleToDefaultPath (Object mainAsset, Object[] assets, string fileName)
	{
		string path = DefaultAssetBundlePath + fileName + ".unity3d";
		BuildPipeline.BuildAssetBundle (mainAsset, assets, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
	}
}
