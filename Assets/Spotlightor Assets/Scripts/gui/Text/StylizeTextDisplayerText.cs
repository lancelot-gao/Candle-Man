using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseTextDisplayer))]
public class StylizeTextDisplayerText : MonoBehaviour
{
	public enum CaseConversionTypes
	{
		None = 0,
		ToUpper = 1,
		ToLower = 2
	}
	public Color32 tintColor = Color.white;
	public CaseConversionTypes caseConverstion = CaseConversionTypes.None;
	private TextMesh myTextMesh;

	public TextMesh MyTextMesh {
		get {
			if (myTextMesh == null)
				myTextMesh = GetComponent<TextMesh> ();
			return myTextMesh;
		}
	}
	
	void Awake ()
	{
		Stylize ();
		GetComponent<BaseTextDisplayer> ().TextUpdated += HandleTextUpdated;
	}

	void HandleTextUpdated (string text)
	{
		StylizeText (text);
	}
	
	public void Stylize ()
	{
		if (MyTextMesh)
			StylizeText (MyTextMesh.text);
		else if (guiText)
			StylizeText (guiText.text);
	}
	
	private void StylizeText (string text)
	{
		string styledText = text;
		switch (caseConverstion) {
		case CaseConversionTypes.ToLower:
			styledText = styledText.ToLower ();
			break;
		case CaseConversionTypes.ToUpper:
			styledText = styledText.ToUpper ();
			break;
		}
		styledText = string.Format ("<color=#{0}>{1}</color>", RGBToHex (tintColor), styledText);
		
		if (MyTextMesh)
			MyTextMesh.text = styledText;
		else if (guiText)
			guiText.text = styledText;
	}
	
	private static string RGBToHex (Color32 color)
	{
		return
			DecimalToHexChar (color.r / 16).ToString () +
			DecimalToHexChar (color.r % 16).ToString () +
			DecimalToHexChar (color.g / 16).ToString () +
			DecimalToHexChar (color.g % 16).ToString () +
			DecimalToHexChar (color.b / 16).ToString () +
			DecimalToHexChar (color.b % 16).ToString () +
			DecimalToHexChar (color.a / 16).ToString () +
			DecimalToHexChar (color.a % 16).ToString ();
	}
	
	public static char DecimalToHexChar (int num)
	{
		if (num > 15)
			return 'f';
		if (num < 10)
			return (char)('0' + num);
		return (char)('a' + num - 10);
	}

}
