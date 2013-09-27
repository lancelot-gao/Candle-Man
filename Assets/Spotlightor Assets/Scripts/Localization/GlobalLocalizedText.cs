using UnityEngine;
using System.Collections;
using System.Linq;

public static class GlobalLocalizedText
{
	public const string GlobalLocalizationTextsAssetResourcePath = "Global Localization";
	private static LocalizationTextsAsset[] globalLocalizationTextsAssets;
	
	static GlobalLocalizedText ()
	{
		globalLocalizationTextsAssets = Resources.LoadAll (GlobalLocalizationTextsAssetResourcePath, typeof(LocalizationTextsAsset)).Cast<LocalizationTextsAsset> ().ToArray ();
		if (globalLocalizationTextsAssets.Length == 0)
			Debug.LogWarning (string.Format ("Cannot load any LocalizationTextsAsset from Resource path:{0}. Put at least 1 LocalizationTextsAsset there to enable the GlobalLocalizedText function.", GlobalLocalizationTextsAssetResourcePath));
	}
	
	public static string GetByKey (string key)
	{
		string text = "NULL";
		foreach (LocalizationTextsAsset textsAsset in globalLocalizationTextsAssets) {
			LocalizationText localizationText = textsAsset.GetLocalizationTextByKey (key);
			if (localizationText != null) {
				text = localizationText;
				break;
			}
		}
		return text;
	}
	
	public static string GetByKeyInLanguage (string key, LocalizationLanguageTypes language)
	{
		string text = "NULL";
		foreach (LocalizationTextsAsset textsAsset in globalLocalizationTextsAssets) {
			LocalizationText localizationText = textsAsset.GetLocalizationTextByKey (key);
			if (localizationText != null) {
				text = localizationText.GetLocalizedTextByLanguage (language);
				break;
			}
		}
		return text;
	}
}
