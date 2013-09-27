using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sUiMultiTypeButton))]
public class sUiMultiTypeButtonLoopByClick : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		GetComponent<sUiMultiTypeButton> ().Click += OnClickMultiTypeButton;
	}

	void OnClickMultiTypeButton (RealClickListener source)
	{
		sUiMultiTypeButton button = source as sUiMultiTypeButton;
		button.ButtonType++;
	}
}
