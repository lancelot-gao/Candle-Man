using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MoveTransitionManager))]
[CanEditMultipleObjects()]
public class MoveTransitionManagerEditor : Editor
{
	public MoveTransitionManager Target {
		get { return target as MoveTransitionManager;}
	}
	
	public Vector3 SerializedPosIn {
		get { return serializedObject.FindProperty ("posIn").vector3Value;}
		set { serializedObject.FindProperty ("posIn").vector3Value = value; }
	}
	
	public Vector3 SerializedPosOut {
		get { return serializedObject.FindProperty ("posOut").vector3Value;}
		set { serializedObject.FindProperty ("posOut").vector3Value = value; }
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		
		GUILayoutOption buttonLayoutOption = GUILayout.Width (60);
		
		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Set As Position:", GUILayout.Width (110));
		if (GUILayout.Button ("In", buttonLayoutOption)) 
			SerializedPosIn = Target.transform.localPosition;
		if (GUILayout.Button ("Out", buttonLayoutOption)) 
			SerializedPosOut = Target.transform.localPosition;
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Move to Position:", GUILayout.Width (110));
		if (GUILayout.Button ("In", buttonLayoutOption)) 
			Target.transform.localPosition = SerializedPosIn;
		if (GUILayout.Button ("Out", buttonLayoutOption)) 
			Target.transform.localPosition = SerializedPosOut;
		GUILayout.EndHorizontal ();
		
		
		
		serializedObject.ApplyModifiedProperties ();
	}
}
