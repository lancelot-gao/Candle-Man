using UnityEngine;
using System.Collections;
using UnityEditor;

public class ResizeByTextureSize : ScriptableObject
{
	
	[MenuItem("Custom/Resize GUITexture by Texture Size")]
	public static void ResizeGUITextureByTextureSize ()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		foreach (GameObject go in selectedObjects) {
			GUITexture goGUITexture = go.GetComponent<GUITexture> ();
			if (goGUITexture) {
				Rect newInest = goGUITexture.pixelInset;
				newInest.width = goGUITexture.texture.width;
				newInest.height = goGUITexture.texture.height;
				goGUITexture.pixelInset = newInest;
			}
		}
	}

	[MenuItem("Custom/Resize Plane1x1 by Texture Size + Default Resolution")]
	public static void ResizePlane1x1ByTextureSizeAndDefaultResolution ()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		foreach (GameObject go in selectedObjects) {
			if (go.renderer != null) {
				Vector3 newScale = Vector3.one;
				Texture texture = go.renderer.sharedMaterial.mainTexture;
				newScale.z = texture.height / (float)PlayerSettings.defaultScreenHeight;
				newScale.x = newScale.z * texture.width / texture.height;
				go.transform.localScale = newScale;
			}
		}
	}

	[MenuItem("Custom/Resize Plane1x1 by Texture Size(keep width)")]
	public static void ResizePlane1x1ByTextureSizeKeepWidth ()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		foreach (GameObject go in selectedObjects) {
			if (go.renderer != null) {
				Vector3 newScale = go.transform.localScale;
				newScale.z = newScale.x * go.renderer.sharedMaterial.mainTexture.height / go.renderer.sharedMaterial.mainTexture.width;
				go.transform.localScale = newScale;
			}
		}
	}
	
	[MenuItem("Custom/Resize Plane1x1 by Texture Size(keep height)")]
	public static void ResizePlane1x1ByTextureSizeKeepHeight ()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		foreach (GameObject go in selectedObjects) {
			if (go.renderer != null) {
				Vector3 newScale = go.transform.localScale;
				newScale.x = newScale.z * go.renderer.sharedMaterial.mainTexture.width / go.renderer.sharedMaterial.mainTexture.height;
				go.transform.localScale = newScale;
			}
		}
	}
}
