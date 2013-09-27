using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sGUISkinableButton))]
public class sGUIMultiStateButton : MonoBehaviour
{
	public sGUIButtonSkin[] additionalSkins;
	private int state = 0;
	private sGUISkinableButton button;
	private sGUIButtonSkin defaultSkin;

	public int State {
		get { return state; }
		set {
			state = value;
			ChangeApperanceByCurrentState ();
		}
	}
	
	protected virtual void ChangeApperanceByCurrentState ()
	{
		if (State <= additionalSkins.Length) {
			if (defaultSkin == null)
				defaultSkin = Button.buttonSkin;
			if (State == 0)
				Button.ChangeButtonSkin (defaultSkin);
			else
				Button.ChangeButtonSkin (additionalSkins [State - 1]);
		}
	}

	public sGUISkinableButton Button {
		get {
			if (button == null)
				button = gameObject.GetComponent<sGUISkinableButton> ();
			return button;
		}
	}
	
	private void Reset ()
	{
		if (Application.isPlaying)
			return;
		additionalSkins = new sGUIButtonSkin[1];
		additionalSkins [0] = guiTexture != null ? sGUIButtonSkin.DefaultSkinForGUITexture : sGUIButtonSkin.DefaultSkinForMesh;
	}
}
