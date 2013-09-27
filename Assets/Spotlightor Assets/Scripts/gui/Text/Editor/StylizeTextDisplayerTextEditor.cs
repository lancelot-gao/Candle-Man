using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(StylizeTextDisplayerText))]
public class StylizeTextDisplayerTextEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Stylize Text")) {
			(target as StylizeTextDisplayerText).Stylize ();
		}
	}
	
}
