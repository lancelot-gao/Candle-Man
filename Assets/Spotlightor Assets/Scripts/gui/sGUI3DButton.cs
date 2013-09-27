using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class sGUI3DButton : sGUISkinableButton
{
	public Renderer buttonRenderer;

	void Awake ()
	{
		if (buttonRenderer == null)
			buttonRenderer = renderer;
		if (buttonSkin.normalTexture == null && buttonRenderer.material.mainTexture != null)
			buttonSkin.normalTexture = buttonRenderer.material.mainTexture;
	}

	protected override void ChangeButtonAppearanceUseSkinByState (sGUIButtonSkin skin, sGUIButtonSkin.ButtonState state)
	{
		if (buttonRenderer)
			skin.ChangeButtonAppearanceByState (buttonRenderer.material, state);
	}
	
	private void Reset ()
	{
		if (collider == null)
			gameObject.AddComponent<BoxCollider> ().isTrigger = true;
		if (renderer != null && buttonRenderer.sharedMaterial != null) {
			buttonSkin.normalColor = buttonRenderer.sharedMaterial.color;
			buttonSkin.overColor = buttonRenderer.sharedMaterial.color;
			buttonSkin.downColor = buttonRenderer.sharedMaterial.color;
		} else {
			buttonSkin.normalColor = Color.white;
			buttonSkin.overColor = Color.white;
			buttonSkin.downColor = Color.white;
		}
	}
}

