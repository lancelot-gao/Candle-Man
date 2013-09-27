using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(LocalizedTextDisplayerText))]
public class LocalizedTextDisplayerTextEditor : LocalizedTextEditor
{
	private LocalizedTextDisplayerText Target {
		get { return target as LocalizedTextDisplayerText;}
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		
		if (GUILayout.Button ("Update Text - En")) {
			LocalizationLanguage.IsUsingEnglish = true;
			Target.DisplayLocalizedText ();
		}
		foreach (LocalizationLanguageTypes language in System.Enum.GetValues(typeof(LocalizationLanguageTypes))) {
			if (GUILayout.Button (string.Format ("Update Text - {0}", language.ToString ())))
				Target.DisplayLocalizedTextByLanguage (language);
		}
	}
}
