using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// Expansion for Create Empty command
/// </summary>
class CreateGameObjectUtilities : ScriptableObject
{
	[MenuItem("GameObject/Create Empty At Selected %#&n")]
	/// <summary>
	/// Create empty at the location of 1st selected object.
	/// </summary>
    static void CreateAtSamePlace ()
	{
		GameObject[] objs = Selection.gameObjects;
		GameObject go = new GameObject ();
		if (objs.Length > 0) {
			GameObject target = objs [0];
            
			go.transform.position = target.transform.position;
			go.transform.rotation = target.transform.rotation;
			go.transform.parent = target.transform.parent;
			go.layer = target.layer;
		}
		
		Selection.activeObject = go;
	}
}