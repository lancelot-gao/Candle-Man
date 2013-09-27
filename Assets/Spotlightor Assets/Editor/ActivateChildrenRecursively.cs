using UnityEngine;
using UnityEditor;
using System.Collections;

public class ActivateChildrenRecursively : ScriptableObject
{
	[MenuItem("GameObject/Activate Children Recursively", false, 220)]
	public static void ActivateSelectedChildren ()
	{
		ActiveRecursively (Selection.activeGameObject, true);
	}
	
	[MenuItem("GameObject/Deactivate Children Recursively", false, 221)]
	public static void DeactivateSelectedChildren ()
	{
		ActiveRecursively (Selection.activeGameObject, false);
	}
	
	private static void ActiveRecursively (GameObject go, bool active)
	{
		go.SetActive (active);
		for (int i = 0; i < go.transform.childCount; i++) {
			GameObject childGo = go.transform.GetChild (i).gameObject;
			ActiveRecursively (childGo, active);
		}
	}
}
