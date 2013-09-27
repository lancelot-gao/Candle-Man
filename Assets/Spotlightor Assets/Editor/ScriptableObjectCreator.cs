using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Editor script to create ScriptableObject asset.
/// </summary>
public class ScriptableObjectCreator : ScriptableObject {
	
	[MenuItem("Assets/Create ScriptableObject Asset")]
	public static void CreateScriptableObjectAsset(){
		foreach(Object obj in Selection.objects){
			if(obj is MonoScript){
				MonoScript script = obj as MonoScript;
				Object data = ScriptableObject.CreateInstance(script.GetClass());
				AssetDatabase.CreateAsset(data, "Assets/"+script.name + ".asset");
			}
		}
	}
}
