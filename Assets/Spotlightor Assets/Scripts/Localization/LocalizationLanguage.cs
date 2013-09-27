using UnityEngine;
using System.Collections;

// Customize the supported languages here:
public enum LocalizationLanguageTypes
{
	Chinese
}

public static class LocalizationLanguage
{
	private static bool isUsingEnglish = true;
	private static LocalizationLanguageTypes currentLanguage = LocalizationLanguageTypes.Chinese;
	
	public static bool IsUsingEnglish {
		set { isUsingEnglish = value;}
		get { return isUsingEnglish;}
	}
	
	public static LocalizationLanguageTypes CurrentLanguage {
		get { return currentLanguage;}
		set {
			currentLanguage = value;
			isUsingEnglish = false;
		}
	}
}