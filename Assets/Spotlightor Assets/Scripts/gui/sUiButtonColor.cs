using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sUiButton))]
public class sUiButtonColor : MonoBehaviour
{
	public GameObject target;
	public Color normalColor = Color.white;
	public Color overColor = Color.white;
	public Color downColor = Color.white;
	public Color disableColor = Color.white;
	public bool tintVertexColor = true;
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
			ChangeColor (normalColor);
		else
			ChangeColor (disableColor);
	}

	void OnButtonEnableStateChanged (sUiButton button)
	{
		if (Button.ButtonEnabled)
			ChangeColor (normalColor);
		else
			ChangeColor (disableColor);
	}

	void OnMouseUpButton (MouseEventDispatcher source)
	{
		if (SystemInfo.deviceType == DeviceType.Handheld)
			ChangeColor (normalColor);
		else
			ChangeColor (overColor);
	}

	void OnMouseDownButton (MouseEventDispatcher source)
	{
		ChangeColor (downColor);
	}

	void OnRollOutButton (MouseEventDispatcher source)
	{
		ChangeColor (normalColor);
	}

	void OnRollOverButton (MouseEventDispatcher source)
	{
		ChangeColor (overColor);
	}
	
	public void UpdateColor ()
	{
		if (Button.ButtonEnabled)
			ChangeColor (normalColor);
		else
			ChangeColor (disableColor);
	}
	
	private void ChangeColor (Color targetColor)
	{
		if (target != null) {
			if (tintVertexColor) {
				MeshFilter meshFilter = target.GetComponent<MeshFilter> ();
				if (meshFilter != null && meshFilter.mesh != null) 
					MeshUtility.Tint (meshFilter.mesh, targetColor);
			} else {
				if (target.renderer != null && target.renderer.material != null)
					target.renderer.material.color = targetColor;
			}
		}
	}
}
