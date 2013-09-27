using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sUiMultiTypeButton))]
public class sUiMultiTypeButtonMesh : MonoBehaviour
{
	public MeshFilter target;
	public Mesh[] meshOfButtonTypes;
	// Use this for initialization
	void Start ()
	{
		ChangeTargetMeshByButtonType (GetComponent<sUiMultiTypeButton> ());
		GetComponent<sUiMultiTypeButton> ().ButtonTypeChanged += HandleButtonTypeChanged;
	}

	void HandleButtonTypeChanged (sUiMultiTypeButton button)
	{
		ChangeTargetMeshByButtonType (button);
	}
	
	private void ChangeTargetMeshByButtonType (sUiMultiTypeButton button)
	{
		if (target != null && meshOfButtonTypes.Length > 0) {
			int meshIndex = Mathf.Clamp (button.ButtonType, 0, meshOfButtonTypes.Length);
			target.mesh = meshOfButtonTypes [meshIndex];
		}
	}
}
