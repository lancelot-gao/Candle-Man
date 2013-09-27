using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class Pack3dArtsFolder : ScriptableWizard
{
	public const string PackedAssetsFolderName = "Packed";
	public int textureSize = 1024;
	public int padding = 0;
	public bool updateTextureAtlasOnly = false;

	[MenuItem("Optimize/Pack 3D Arts Folder")]
	public static void CreateWizard ()
	{
		ScriptableWizard.DisplayWizard ("Pack 3D Arts Folder", typeof(Pack3dArtsFolder), "Pack");
	}
	
	void OnWizardUpdate ()
	{
		helpString = "All GameObjects with mesh in selected folder will be packed.";
	}
	
	void OnWizardCreate ()
	{
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		
		string folderName = path.Substring (path.LastIndexOf ("/") + 1);
		Object[] objs = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		List<GameObject> gos = new List<GameObject> ();
		foreach (Object obj in objs) {
			if (obj is GameObject)
				gos.Add (obj as GameObject);
		}
		if (gos.Count > 0) {
			string packedAssetsPath = path + "/" + PackedAssetsFolderName;
			string textureName = folderName.ToLower ();
			textureName = textureName.Replace (" ", "_");
			textureName += "_atlas";
			
			Pack (gos.ToArray (), packedAssetsPath, textureName);
		} else 
			Debug.Log (string.Format ("No GameObjects in path:{0}", path));
	}
	
	private void Pack (GameObject[] gos, string meshFolderPath, string textureName)
	{
		List<Texture2D> texturesToPack = new List<Texture2D> ();
		List<MeshFilter> allMeshFilters = new List<MeshFilter> ();
		foreach (GameObject go in gos) {
			MeshFilter[] meshFilters = go.GetComponentsInChildren<MeshFilter> (true);
			foreach (MeshFilter m in meshFilters) {
				allMeshFilters.Add (m);
			}
			MeshRenderer[] renderers = go.GetComponentsInChildren<MeshRenderer> (true);
			foreach (MeshRenderer rd in renderers) {
				Texture2D tex = rd.sharedMaterial.mainTexture as Texture2D;
				if (tex != null && texturesToPack.IndexOf (tex) == -1)
					texturesToPack.Add (tex);
			}
		}
		
		if (allMeshFilters.Count > 0 && texturesToPack.Count > 0) {
			Texture2D atlas = new Texture2D (textureSize, textureSize);
			Rect[] atlasTextureRects = atlas.PackTextures (texturesToPack.ToArray (), padding, textureSize, false);
			
			string atlasTexturePath = meshFolderPath + "/" + textureName + ".png";
			Texture2D atlasTextureAsset = SaveAssetUtility.SaveTexture (atlas, atlasTexturePath);
			
			if (updateTextureAtlasOnly == false && atlasTextureAsset != null) {
				foreach (MeshFilter mf in allMeshFilters) {
					Texture2D tex = mf.GetComponent<MeshRenderer> ().sharedMaterial.mainTexture as Texture2D;
					if (tex != null) {
						int texIndex = texturesToPack.IndexOf (tex);
						Rect newUvSpace = atlasTextureRects [texIndex];
						
						Mesh atlasMesh = new Mesh ();
						Mesh originalMesh = mf.sharedMesh;
						
						Vector2[] uv = new Vector2[originalMesh.uv.Length];
						for (int i = 0; i < originalMesh.uv.Length; i++) {
							Vector2 vertexUv = new Vector2 (
								newUvSpace.x + newUvSpace.width * originalMesh.uv [i].x,
								newUvSpace.y + newUvSpace.height * originalMesh.uv [i].y
								);
							uv [i] = vertexUv;
						}
						int[] triangles = new int[originalMesh.triangles.Length];
						System.Array.Copy (originalMesh.triangles, triangles, triangles.Length);
						
						Vector3[] vertices = new Vector3[originalMesh.vertices.Length];
						System.Array.Copy (originalMesh.vertices, vertices, vertices.Length);
						
						Vector3[] normals = new Vector3[originalMesh.normals.Length];
						System.Array.Copy (originalMesh.normals, normals, normals.Length);
						
						Vector4[] tangents = new Vector4[originalMesh.tangents.Length];
						System.Array.Copy (originalMesh.tangents, tangents, tangents.Length);
						
						atlasMesh.vertices = vertices;
						atlasMesh.triangles = triangles;
						atlasMesh.normals = normals;
						atlasMesh.tangents = tangents;
						atlasMesh.uv = uv;
						atlasMesh.uv2 = new List<Vector2> (originalMesh.uv2).ToArray ();
						atlasMesh.RecalculateBounds ();
						
						Debug.Log (string.Format ("Mesh Pack Result = vertices:{0}/{1} uv:{2}/{3} triangles:{4}/{5} tangents:{6}/{7} normals:{8}/{9} ", 
							atlasMesh.vertices.Length, originalMesh.vertices.Length,
							atlasMesh.uv.Length, originalMesh.uv.Length,
							atlasMesh.triangles.Length, originalMesh.triangles.Length,
							atlasMesh.tangents.Length, originalMesh.tangents.Length,
							atlasMesh.normals.Length, originalMesh.normals.Length
							));
						
						string meshAssetPath = string.Format ("{0}/{1}.asset", meshFolderPath, mf.sharedMesh.name);
						
						SaveAssetUtility.SaveMesh (atlasMesh, meshAssetPath);
					}
				}
				
			} 
		} else {
			Debug.LogWarning ("Nothing to pack");
		}
	}
}
