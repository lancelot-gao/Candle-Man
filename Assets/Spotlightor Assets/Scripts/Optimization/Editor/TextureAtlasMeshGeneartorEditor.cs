using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TextureAtlasMeshGeneartor))]
public class TextureAtlasMeshGeneartorEditor : Editor
{
	private const string MeshFolderName = "Atlas Meshes";
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Generate Meshes")) {
			TextureAtlasMeshGeneartor meshGenerator = target as TextureAtlasMeshGeneartor;
			
			GenerateMeshes (meshGenerator);
		}
		if (GUILayout.Button ("Generate Meshes & Pack Atlas")) {
			TextureAtlasMeshGeneartor meshGenerator = target as TextureAtlasMeshGeneartor;
			
			TextureAtlasEditor.Pack (meshGenerator.atlas);
			
			GenerateMeshes (meshGenerator);
		}
	}
	
	public static void GenerateMeshes (TextureAtlasMeshGeneartor meshGenerator)
	{
		string path = AssetDatabase.GetAssetPath (meshGenerator);
		string currentFolderPath = path.Substring (0, path.LastIndexOf ("/"));
		string meshFolderPath = currentFolderPath + "/" + MeshFolderName;
			
		Mesh[] generatedMeshes = meshGenerator.GenerateAllMeshes ();
		foreach (Mesh mesh in generatedMeshes)
			SaveAssetUtility.SaveMesh (mesh, string.Format ("{0}/{1}.asset", meshFolderPath, mesh.name));
	}
}
