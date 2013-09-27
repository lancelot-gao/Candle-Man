using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class LocalizationText
{
	[System.Serializable]
	public class TextByLanguage
	{
		public LocalizationLanguageTypes language;
		public string text;
	}
	public string key;
	public string englishText = "";
	public TextByLanguage[] textsByLanguages;
	
	public string GetLocalizedTextByLanguage (LocalizationLanguageTypes language)
	{
		TextByLanguage textByLanguage = Array.Find (textsByLanguages, element => element.language == language);
		if (textByLanguage != null)
			return textByLanguage.text;
		else
			return englishText;
	}
	
	public string GetAllTexts ()
	{
		string allTexts = englishText;
		foreach (TextByLanguage textByLanguage in textsByLanguages)
			allTexts += textByLanguage.text;
		return allTexts;
	}
	
	public override string ToString ()
	{
		if (!LocalizationLanguage.IsUsingEnglish) 
			return GetLocalizedTextByLanguage (LocalizationLanguage.CurrentLanguage);
		else
			return englishText;
	}
	
	public static implicit operator string (LocalizationText t)
	{
		return t.ToString ();
	}
}

public class LocalizationTextsAsset : ScriptableObject
{
	public Font[] fonts;
	public string additionalChars;
	public LocalizationText[] texts;
	
	public int TextsCount {
		get { return texts != null ? texts.Length : 0;}
	}

	public string GetLocalizedTextStringByIndex (int index)
	{
		LocalizationText text = GetLocalizationTextByIndex (index);
		return text == null ? "NULL" : text;
	}
	
	public LocalizationText GetLocalizationTextByIndex (int index)
	{
		if (texts != null && texts.Length > 0 && index >= 0 && index < texts.Length) {
			return texts [index];
		}
		return null;
	}

	public string GetLocalizedTextStringByKey (string key)
	{
		LocalizationText text = GetLocalizationTextByKey (key);
		return text == null ? "NULL" : text;
	}
	
	public LocalizationText GetLocalizationTextByKey (string key)
	{
		return System.Array.Find<LocalizationText> (texts, element => element.key == key);
	}
	
	public string GetAllTexts ()
	{
		string sum = additionalChars;
		foreach (LocalizationText lt in texts) {
			string ltText = lt.GetAllTexts ();
			for (int i = 0; i < ltText.Length; i++) {
				if (sum.IndexOf (ltText [i]) == -1)
					sum += ltText [i];
			}
		}
		return sum;
	}
}
