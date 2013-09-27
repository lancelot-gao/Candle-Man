using UnityEngine;
using System.Collections;

public class sUiMultiTypeButton : sUiButton
{
	public delegate void ButtonTypeChangedEventHandler (sUiMultiTypeButton button);

	public event ButtonTypeChangedEventHandler ButtonTypeChanged;
	
	public int buttonTypeCount = 2;
	private int buttonType = 0;

	public int ButtonType {
		get { return buttonType;}
		set {
			value = value % buttonTypeCount;
			if (buttonType != value) {
				buttonType = value;
				if (ButtonTypeChanged != null)
					ButtonTypeChanged (this);
			}
		}
	}
	
}
