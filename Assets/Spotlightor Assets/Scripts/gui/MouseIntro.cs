using UnityEngine;
using System.Collections;

public class MouseIntro : SmartUnityGuiLabel
{
	private const float SystemCursorWidth = 6;
	private const float SystemCursorHeight = 9;
	private float defaultOffsetX;
	private float defaultOffsetY;
	private bool mouseOverThisFrame = false;// MouseExit is not reliable, let's do it myself.
	private bool mouseOverLastFrame = false;
	
	#region Functions
	
	protected virtual void Awake ()
	{
		defaultOffsetX = offsetX;
		defaultOffsetY = offsetY;
		TransitionOut(true);
	}
	
	void OnMouseEnter ()
	{
		TransitionIn ();
	}
	
	void OnMouseReallyExit ()
	{
		TransitionOut ();
	}

	void OnMouseOver ()
	{
		mouseOverThisFrame = true;
	}

	void Update ()
	{
		if (!mouseOverThisFrame && mouseOverLastFrame) {
			OnMouseReallyExit ();
		}
		mouseOverLastFrame = mouseOverThisFrame;
		mouseOverThisFrame = false;
		
		Vector3 mousePos = Input.mousePosition;
		UpdateDrawPositionOnScreen (new Vector2 (mousePos.x, mousePos.y));
		
		if (DrawAtRight)
			offsetX = defaultOffsetX + SystemCursorWidth;
		else
			offsetX = defaultOffsetX;
		if (DrawAtBottom)
			offsetY = defaultOffsetY + SystemCursorHeight;
		else
			offsetY = defaultOffsetY;
	}
	
	#endregion
}
