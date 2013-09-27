using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sGUISkinableButton))]
public class sGUIColorTintByButtonState : MonoBehaviour
{
	public Color colorNormal = Color.white;
	public Color colorOver = Color.white;
	public Color colorDown = Color.white;
	public Color colorDisable = Color.white;
	public Renderer targetRenderer;
	private sGUISkinableButton button;
	// Use this for initialization
	void Start ()
	{
		button = GetComponent<sGUISkinableButton> ();
		TintColorByButtonState ();
		
		button.StateChanged += OnButtonStateChanged;
	}

	void OnButtonStateChanged (sGUISkinableButton button)
	{
		TintColorByButtonState ();
	}
	
	private void TintColorByButtonState ()
	{
		if (targetRenderer == null)
			return;
		switch (button.ButtonState) {
		case sGUIButtonSkin.ButtonState.over:
			targetRenderer.material.color = colorOver;
			break;
		case sGUIButtonSkin.ButtonState.down:
			targetRenderer.material.color = colorDown;
			break;
		case sGUIButtonSkin.ButtonState.disable:
			targetRenderer.material.color = colorDisable;
			break;
		default:
			targetRenderer.material.color = colorNormal;
			break;
		}
	}
	
	void Reset ()
	{
		if (targetRenderer != null) {
			colorNormal = colorOver = colorDown = colorDisable = targetRenderer.sharedMaterial.color;
		}
	}
}
