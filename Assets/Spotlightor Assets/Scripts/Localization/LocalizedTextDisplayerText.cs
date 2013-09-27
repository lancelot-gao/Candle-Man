using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseTextDisplayer))]
public class LocalizedTextDisplayerText : MonoBehaviour
{
	public LocalizedText text;
	private BaseTextDisplayer textDisplayer;

	private BaseTextDisplayer TextDisplayer {
		get {
			if (textDisplayer == null) {
				textDisplayer = GetComponent<BaseTextDisplayer> ();
				if (textDisplayer == null)
					textDisplayer = gameObject.AddComponent<BaseTextDisplayer> ();
			}
			return textDisplayer;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		DisplayLocalizedText ();
	}
	
	public void DisplayLocalizedText ()
	{
		TextDisplayer.DisplayText (text);
	}
	
	public void DisplayLocalizedTextByLanguage (LocalizationLanguageTypes language)
	{
		TextDisplayer.DisplayText (text.ToString (language));
	}
	
}
