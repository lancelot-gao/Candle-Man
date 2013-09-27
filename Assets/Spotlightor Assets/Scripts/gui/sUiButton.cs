using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class sUiButton : MouseEventDispatcher
{
	public delegate void ButtonEnableStateChangedHandler (sUiButton button);

	public event ButtonEnableStateChangedHandler ButtonEnableStateChange;

	public bool ButtonEnabled {
		get { return collider != null && collider.enabled;}
		set {
			if (collider != null && ButtonEnabled != value) {
				collider.enabled = this.enabled = value;
				if (ButtonEnableStateChange != null)
					ButtonEnableStateChange (this);
			}
		}
	}
	
	private void Reset ()
	{
		if (collider == null)
			gameObject.AddComponent<BoxCollider> ().isTrigger = true;
	}
	
}
