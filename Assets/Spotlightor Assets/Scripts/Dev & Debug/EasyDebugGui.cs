using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode()]
public abstract class EasyDebugGui : MonoBehaviour
{
	public GUISkin skin;
	public Vector2 topLeft;
	public float verticleOffset = 10;
	public float horizontalOffset = 10;
	public float labelWidth = 50;
	public float buttonWidth = 100;
	public float lineHeight = 30;
	private float top = 0;
	private float left = 0;
	
	private void OnGUI ()
	{
		if (skin != null)
			GUI.skin = skin;
		left = topLeft.x;
		top = topLeft.y;
		
		DrawDebugGUI ();
	}
	
	protected abstract void DrawDebugGUI ();
	
	protected void DrawPrefsDataSetting (string title, Enum key, params string[] buttonLabels)
	{
		DrawPrefsDataSetting (title, BasicDataTypeStorage.ConvertEnumToStringIdentifier (key), buttonLabels);
	}
	
	protected void DrawPrefsDataSetting (string title, string key, params string[] buttonLabels)
	{
		int keyValue = BasicDataTypeStorage.GetInt (key);
		string keyValueName = "INVALID";
		
		if (keyValue >= 0 && keyValue < buttonLabels.Length)
			keyValueName = buttonLabels [keyValue];
		
		Label (string.Format ("{0}: {1}", title, keyValueName));
		for (int i = 0; i < buttonLabels.Length; i++) {
			if (Button (buttonLabels [i]))
				BasicDataTypeStorage.SetInt (key, i);
		}
		NewLine ();
	}
	
	protected void Label (string text)
	{
		GUI.Label (new Rect (left, top, labelWidth, lineHeight), text);
		left += labelWidth + horizontalOffset;
	}
	
	protected bool Button (string label)
	{
		bool buttonClicked = GUI.Button (new Rect (left, top, buttonWidth, lineHeight), label);
		left += buttonWidth + horizontalOffset;
		return buttonClicked;
	}
	
	protected bool Button (string label, float width)
	{
		bool buttonClicked = GUI.Button (new Rect (left, top, width, lineHeight), label);
		left += width + horizontalOffset;
		return buttonClicked;
	}
	
	protected void EmptyButtonSpace ()
	{
		left += buttonWidth + horizontalOffset;
	}
	
	protected void NewLine ()
	{
		left = topLeft.x;
		top += verticleOffset + lineHeight;
	}
	
}
