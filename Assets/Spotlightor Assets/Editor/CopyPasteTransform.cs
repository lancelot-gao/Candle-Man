using UnityEditor;
using UnityEngine;
using System.Collections;

class CopyPasteTransform : ScriptableObject
{
	private static Vector3 copiedPosition = Vector3.zero;
	private static Quaternion copiedRotation = Quaternion.identity;

	[MenuItem("GameObject/Copy Transform %#c", false, 200)]
	static void CopyTransform ()
	{
		if (Selection.activeGameObject != null) {
			copiedPosition = Selection.activeGameObject.transform.position;
			copiedRotation = Selection.activeGameObject.transform.rotation;
			Debug.Log (string.Format ("Position and Rotation of {0} copied.", Selection.activeObject.name));
		}
	}
	
	[MenuItem("GameObject/Paste Transform %#v", false, 201)]
	static void PasteTransform ()
	{
		GameObject[] gos = Selection.gameObjects;
		
		Undo.RegisterUndo(gos, "Paste transform");
		foreach (GameObject go in gos) {
			go.transform.position = copiedPosition;
			go.transform.rotation = copiedRotation;
			Debug.Log (string.Format ("Position and Rotation of {0} set by copied transform.", go.name));
		}
	}
}