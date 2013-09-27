using UnityEngine;
using System.Collections;

/// <summary>
/// 用于sGUI按钮系列，定义了标准的按钮SKIN，并且提供了为按钮（不论是基于GUITexture还是3D）根据状态跟换外观的方法
/// </summary>
[System.Serializable]
public class sGUIButtonSkin
{
    #region Fields
	public enum ButtonState
	{
		normal,
		over,
		down,
		disable
	};
	public Texture normalTexture;
	public Color normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Texture overTexture;
	public Color overColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Texture downTexture;
	public Color downColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	public Texture disableTexture;
	public Color disableColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
    #endregion

    #region Properties
	public Texture overOrNormalTexture { get { return overTexture == null ? normalTexture : overTexture; } }

	public Texture downOrNormalTexture { get { return downTexture == null ? normalTexture : downTexture; } }

	public Texture disableOrNormalTexture { get { return disableTexture == null ? normalTexture : disableTexture; } }

	public static sGUIButtonSkin DefaultSkinForGUITexture {
		get {
			sGUIButtonSkin skin = new sGUIButtonSkin ();
			Color semiTransparentGrey = Color.grey;
			semiTransparentGrey.a = 0.5f;
			skin.normalColor = skin.overColor = skin.downColor = skin.disableColor = semiTransparentGrey;
			return skin;
		}
	}

	public static sGUIButtonSkin DefaultSkinForMesh {
		get {
			sGUIButtonSkin skin = new sGUIButtonSkin ();
			skin.normalColor = skin.overColor = skin.downColor = Color.white;
			skin.disableColor = Color.gray;
			return skin;
		}
	}
    #endregion

    #region Functions
	public void ChangeButtonAppearanceByState (Material buttonMaterial, ButtonState state)
	{
		Texture buttonTexture;
		Color buttonColor;
		switch (state) {
		case ButtonState.over:
			buttonColor = overColor;
			buttonTexture = overOrNormalTexture;
			break;
		case ButtonState.down:
			buttonColor = downColor;
			buttonTexture = downOrNormalTexture;
			break;
		case ButtonState.disable:
			buttonColor = disableColor;
			buttonTexture = disableOrNormalTexture;
			break;
		default:
			buttonColor = normalColor;
			buttonTexture = normalTexture;
			break;
		}
		ChangeButtonAppearance (buttonMaterial, buttonColor, buttonTexture);
	}

	public void ChangeButtonAppearanceByState (GUITexture buttonGUI, ButtonState state)
	{
		Texture buttonTexture;
		Color buttonColor;
		switch (state) {
		case ButtonState.over:
			buttonColor = overColor;
			buttonTexture = overOrNormalTexture;
			break;
		case ButtonState.down:
			buttonColor = downColor;
			buttonTexture = downOrNormalTexture;
			break;
		case ButtonState.disable:
			buttonColor = disableColor;
			buttonTexture = disableOrNormalTexture;
			break;
		default:
			buttonColor = normalColor;
			buttonTexture = normalTexture;
			break;
		}
		ChangeButtonAppearance (buttonGUI, buttonColor, buttonTexture);
	}
	
	protected void ChangeButtonAppearance (GUITexture buttonGUI, Color buttonColor, Texture buttonTexture)
	{
		buttonGUI.color = buttonColor;
		if (buttonTexture != null)
			buttonGUI.texture = buttonTexture;
	}
	
	protected void ChangeButtonAppearance (Material buttonMaterial, Color buttonColor, Texture buttonTexture)
	{
		buttonMaterial.color = buttonColor;
		if (buttonTexture != null)
			buttonMaterial.mainTexture = buttonTexture;
	}
    #endregion
}
