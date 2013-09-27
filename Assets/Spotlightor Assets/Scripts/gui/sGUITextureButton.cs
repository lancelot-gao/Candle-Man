using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class sGUITextureButton : sGUISkinableButton
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Functions
	void Awake ()
	{
		if (guiTexture.texture != null && buttonSkin.normalTexture == null)
			buttonSkin.normalTexture = guiTexture.texture;
	}

	protected override void ChangeButtonAppearanceUseSkinByState (sGUIButtonSkin skin, sGUIButtonSkin.ButtonState state)
	{
		skin.ChangeButtonAppearanceByState (guiTexture, state);
	}
    #endregion
}
