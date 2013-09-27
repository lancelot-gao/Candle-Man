using UnityEngine;
using System.Collections;

public class HomeSceneController : MonoBehaviour
{
	public sUiButton btStartEn;
	public sUiButton btStartChs;
	// Use this for initialization
	void Start ()
	{
		btStartEn.Click += HandleBtStartEnClick;
		btStartChs.Click += HandleBtStartChsClick;
	}

	void HandleBtStartChsClick (RealClickListener source)
	{
		LocalizationLanguage.CurrentLanguage = LocalizationLanguageTypes.Chinese;
		Application.LoadLevel (1);
	}

	void HandleBtStartEnClick (RealClickListener source)
	{
		LocalizationLanguage.IsUsingEnglish = true;
		Application.LoadLevel (1);
	}
}
