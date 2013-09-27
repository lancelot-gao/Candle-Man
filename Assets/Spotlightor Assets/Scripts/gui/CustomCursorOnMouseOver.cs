using UnityEngine;
using System.Collections;

public class CustomCursorOnMouseOver : MonoBehaviour
{
	public bool hideMouseCursor;
	public Vector2 offset;
	public Texture cursorTexture;
	void OnMouseEnter ()
	{
		if (hideMouseCursor)
			Screen.showCursor = false;
		enabled = true;
	}

	void OnMouseExit ()
	{
		if (hideMouseCursor)
			Screen.showCursor = true;
		enabled = false;
	}

	void OnDisable ()
	{
		if (hideMouseCursor)
			Screen.showCursor = true;
	}

	void Start ()
	{
		enabled = false;
	}

	void OnGUI ()
	{
		if (cursorTexture) {
			Vector3 mousePos = Input.mousePosition;
			Rect drawRect = new Rect (mousePos.x + offset.x, Screen.height - mousePos.y + offset.y, cursorTexture.width, cursorTexture.height);
			GUI.DrawTexture (drawRect, cursorTexture);
		}
	}
}
