using UnityEditor;
using UnityEngine;
using System.Collections;

public class CATUtilities : ScriptableObject {
	
	[MenuItem("Assets/CAT Clear Bone Mesh")]
	public static void ClearCATBoneMeshes()
	{
		GameObject go = Selection.activeGameObject;
		MeshRenderer[] rds = go.GetComponentsInChildren<MeshRenderer>();
		foreach(MeshRenderer rd in rds){
			if(rd.sharedMaterial.name.Contains("No Name")){// Ungly, but it works.
				DestroyImmediate(rd.GetComponent<MeshFilter>());
				DestroyImmediate(rd);
			}
		}
	}
}
