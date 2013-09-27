using UnityEngine;
using System.Collections;

[System.Serializable]
public class LocalizedText
{
	public LocalizationTextsAsset textsAsset;
	public string key;
	
	public bool IsValid {
		get { return textsAsset != null && !string.IsNullOrEmpty (key);}
	}
	
	public string ToString (LocalizationLanguageTypes language)
	{
		return textsAsset.GetLocalizationTextByKey (key).GetLocalizedTextByLanguage (language);
	}

	public override string ToString ()
	{
		if (IsValid)
			return textsAsset.GetLocalizedTextStringByKey (key);
		else
			return "INVALID";
	}
	
	public static implicit operator string (LocalizedText t)
	{
		return t.ToString ();
	}
}
