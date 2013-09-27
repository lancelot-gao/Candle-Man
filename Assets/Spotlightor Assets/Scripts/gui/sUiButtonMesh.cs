using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseEventDispatcher))]
public class sUiButtonMesh : MonoBehaviour
{
	public MeshFilter target;
	public Mesh overMesh;
	public Mesh downMesh;
	public Mesh disableMesh;
	private Mesh normalMesh;
	private sUiButton button;
	
	public Mesh NormalMesh {
		get {
			if (normalMesh == null)
				normalMesh = target.sharedMesh;
			return normalMesh;
		}
	}
	
	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}
	
	void Awake ()
	{
		normalMesh = target.sharedMesh;
	}

	void Start ()
	{
		if (target != null) {
			Button.RollOver += OnRollOverButton;
			Button.RollOut += OnRollOutButton;
			Button.MouseDown += OnMouseDownButton;
			Button.MouseUp += OnMouseUpButton;
			Button.ButtonEnableStateChange += OnButtonEnableStateChanged;
			ChangeMesh (Button.ButtonEnabled ? NormalMesh : disableMesh);
		} else
			this.LogWarning ("Button mesh target(MeshFilter) is null");
	}

	void OnButtonEnableStateChanged (sUiButton button)
	{
		if (button.ButtonEnabled)
			ChangeMesh (NormalMesh);
		else
			ChangeMesh (disableMesh);
	}

	void OnMouseUpButton (MouseEventDispatcher source)
	{
		if (SystemInfo.deviceType == DeviceType.Handheld)
			ChangeMesh (NormalMesh);
		else
			ChangeMesh (overMesh);
	}

	void OnMouseDownButton (MouseEventDispatcher source)
	{
		ChangeMesh (downMesh);
	}
	
	void OnRollOverButton (MouseEventDispatcher source)
	{
		ChangeMesh (overMesh);
	}
	
	void OnRollOutButton (MouseEventDispatcher source)
	{
		ChangeMesh (NormalMesh);
	}

	private void ChangeMesh (Mesh newMesh)
	{
		target.mesh = newMesh != null ? newMesh : NormalMesh;
	}
}
