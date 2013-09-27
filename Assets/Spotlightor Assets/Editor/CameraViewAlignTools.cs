using UnityEngine;
using UnityEditor;
using System.Collections;

public class CameraViewAlignTools : ScriptableObject
{

	[MenuItem("GameObject/Align MainCamera with Selected %#m")]
	public static void AlignMainCameraToSelected ()
	{
		if (Selection.activeGameObject != null) {
			Undo.RegisterUndo (Camera.main.gameObject, "Align Main Camera to Selected");
			Camera.main.transform.position = Selection.activeGameObject.transform.position;
			Camera.main.transform.rotation = Selection.activeGameObject.transform.rotation;
		}
	}
}
