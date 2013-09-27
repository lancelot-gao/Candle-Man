using UnityEngine;
using System.Collections;

public class MouseEventDispatcher : RealClickListener
{
	public delegate void GenericMouseEventHandler (MouseEventDispatcher source);

	public event GenericMouseEventHandler RollOver;
	public event GenericMouseEventHandler RollOut;
	public event GenericMouseEventHandler MouseDown;
	public event GenericMouseEventHandler MouseUp;
	public event GenericMouseEventHandler HoldDown;

	private bool mouseDownOnMe = false;
	private bool mouseOnMe = false;
	
	protected virtual void OnMouseEnter ()
	{
		if (!enabled)
			return;
		
		if (this.mouseDownOnMe == false) {
			if (RollOver != null)
				RollOver (this);
		} else if (mouseOnMe == false) {
			if (MouseDown != null)
				MouseDown (this);
		}
		
		mouseOnMe = true;
	}

	protected virtual void OnMouseExit ()
	{
		if (!enabled)
			return;
		
		mouseOnMe = false;
		
		if (RollOut != null)
			RollOut (this);
	}

	protected virtual void OnMouseDown ()
	{
		if (!enabled)
			return;
		this.mouseDownOnMe = true;
		this.mouseOnMe = true;
		if (MouseDown != null)
			MouseDown (this);
	}
	
	protected virtual void OnMouseUp ()
	{
		if (!enabled)
			return;
		
		this.mouseDownOnMe = false;
		this.mouseOnMe = false;
		if (MouseUp != null)
			MouseUp (this);
	}

	protected virtual void OnMouseOver ()
	{
		if (!enabled)
			return;
		
		if (Input.GetMouseButton (0) && this.mouseDownOnMe == true) {
			if (HoldDown != null)
				HoldDown (this);
		}
	}
	
}
