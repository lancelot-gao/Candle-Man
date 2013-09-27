using UnityEngine;
using System.Collections;

public class sUiButtonKeyBinding : MonoBehaviour
{
	public KeyCode keyCode = KeyCode.Alpha1;

	// Update is called once per frame
	void Update ()
	{
		if (keyCode != KeyCode.None) {
			if (Input.GetKeyDown (keyCode))
				gameObject.SendMessage ("OnMouseDown", SendMessageOptions.DontRequireReceiver);
			else if (Input.GetKeyUp (keyCode)) {
				gameObject.SendMessage ("OnMouseUp", SendMessageOptions.DontRequireReceiver);
				gameObject.SendMessage ("OnMouseRealClick", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
