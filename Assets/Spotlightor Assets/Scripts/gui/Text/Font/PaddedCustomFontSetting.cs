using UnityEngine;
using System.Collections;

public class PaddedCustomFontSetting : ScriptableObject
{
	public Font sourceFont;
	public Font customFont;
	public int maxTextureSize = 1024;
	public int padding = 5;
}
