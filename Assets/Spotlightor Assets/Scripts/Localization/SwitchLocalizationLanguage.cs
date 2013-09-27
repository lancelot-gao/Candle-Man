using UnityEngine;
using System.Collections;

public class SwitchLocalizationLanguage : MonoBehaviour
{
	public LocalizationLanguageTypes targetLanguage = LocalizationLanguageTypes.Chinese;

	void Awake ()
	{
		LocalizationLanguage.CurrentLanguage = targetLanguage;
	}
}
