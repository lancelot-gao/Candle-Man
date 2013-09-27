using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sUiButton))]
public class sUiButtonScale : MonoBehaviour
{
	public Transform target;
	public Vector3 normalScale = Vector3.one;
	public Vector3 overScale = Vector3.one;
	public Vector3 downScale = Vector3.one;
	public Vector3 disableScale = Vector3.one;
	private sUiButton button;

	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}
	// Use this for initialization
	void Start ()
	{
		Button.RollOver += OnRollOverButton;
		Button.RollOut += OnRollOutButton;
		Button.MouseDown += OnMouseDownButton;
		Button.MouseUp += OnMouseUpButton;
		Button.ButtonEnableStateChange += OnButtonEnableStateChanged;
		
		if (Button.ButtonEnabled)
			ScaleTo (normalScale);
		else
			ScaleTo (disableScale);
	}

	void OnButtonEnableStateChanged (sUiButton button)
	{
		if (button.ButtonEnabled)
			ScaleTo (normalScale);
		else
			ScaleTo (disableScale);
	}

	void OnMouseUpButton (MouseEventDispatcher source)
	{
		if (SystemInfo.deviceType == DeviceType.Handheld)
			ScaleTo (normalScale);
		else
			ScaleTo (overScale);
	}

	void OnMouseDownButton (MouseEventDispatcher source)
	{
		ScaleTo (downScale);
	}

	void OnRollOutButton (MouseEventDispatcher source)
	{
		ScaleTo (normalScale);
	}

	void OnRollOverButton (MouseEventDispatcher source)
	{
		ScaleTo (overScale);
	}
	
	private void ScaleTo (Vector3 localScale)
	{
		if (target != null) 
			target.localScale = localScale;
	}
	
	void Reset ()
	{
		if (target == null)
			target = transform;
	}
}
