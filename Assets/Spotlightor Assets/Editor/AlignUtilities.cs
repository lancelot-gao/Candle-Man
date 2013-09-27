using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class AlignUtilities
{

	[MenuItem("GameObject/Align World X", false, 120)]
	public static void AlignWorldX ()
	{
		AlignWorldPosition (new Vector3 (1, 0, 0));
	}

	[MenuItem("GameObject/Align World Y", false, 121)]
	public static void AlignWorldY ()
	{
		AlignWorldPosition (new Vector3 (0, 1, 0));
	}

	[MenuItem("GameObject/Align World Z", false, 122)]
	public static void AlignWorldZ ()
	{
		AlignWorldPosition (new Vector3 (0, 0, 1));
	}
	
	private static void AlignWorldPosition (Vector3 axis)
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Vector3 averagePosition = Vector3.zero;
		foreach (GameObject go in selectedGos) {
			averagePosition += go.transform.position;
		}
		averagePosition /= selectedGos.Length;
		
		Undo.RegisterUndo (selectedGos, "Align World Position");
		foreach (GameObject go in selectedGos) {
			Vector3 newPos = go.transform.position;
			if (axis.x != 0)
				newPos.x = averagePosition.x * axis.x;
			if (axis.y != 0)
				newPos.y = averagePosition.y * axis.y;
			if (axis.z != 0)
				newPos.z = averagePosition.z * axis.z;
			
			go.transform.position = newPos;
		}
	}

	[MenuItem("GameObject/Distribute World X", false, 123)]
	public static void DistributeInWorldX ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.position.x.CompareTo (y.transform.position.x));
		float start = selectedGos [0].transform.position.x;
		float end = selectedGos [selectedGos.Length - 1].transform.position.x;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetPositionX ((float)i * step + start);
		}
	}

	[MenuItem("GameObject/Distribute World Y", false, 124)]
	public static void DistributeInWorldY ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.position.y.CompareTo (y.transform.position.y));
		float start = selectedGos [0].transform.position.y;
		float end = selectedGos [selectedGos.Length - 1].transform.position.y;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetPositionY ((float)i * step + start);
		}
	}

	[MenuItem("GameObject/Distribute World Z", false, 125)]
	public static void DistributeInWorldZ ()
	{
		GameObject[] selectedGos = Selection.gameObjects;
		if (selectedGos.Length < 2) {
			Debug.LogWarning ("Select at least 2 GameObjects in the scene!");
			return;
		}
		
		Array.Sort (selectedGos, (x, y) => x.transform.position.z.CompareTo (y.transform.position.z));
		float start = selectedGos [0].transform.position.z;
		float end = selectedGos [selectedGos.Length - 1].transform.position.z;
		float step = (end - start) / (selectedGos.Length - 1);
		for (int i = 1; i < selectedGos.Length-1; i++) {
			selectedGos [i].transform.SetPositionZ ((float)i * step + start);
		}
	}
	
	
}
