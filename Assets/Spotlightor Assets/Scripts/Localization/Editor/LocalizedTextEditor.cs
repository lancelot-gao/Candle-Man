using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalizedTextEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		
		DrawDefaultInspector ();
		
		SerializedProperty property = serializedObject.GetIterator ();
		property.Next (true);
		while (property.NextVisible(false)) {
			if (property.type == typeof(LocalizedText).ToString ()) {
				DrawLocalizedTextInspectorGUI (property);
			}
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
	
	private void DrawLocalizedTextInspectorGUI (SerializedProperty localizedTextProperty)
	{
		SerializedProperty textAssetProperty = localizedTextProperty.FindPropertyRelative ("textsAsset");
		SerializedProperty keyProperty = localizedTextProperty.FindPropertyRelative ("key");
		
		LocalizationTextsAsset textAsset = textAssetProperty.objectReferenceValue as LocalizationTextsAsset;
		
		if (textAsset != null) {
			string key = keyProperty.stringValue;
			LocalizationText localizationText = textAsset.GetLocalizationTextByKey (key);
		
			if (localizationText != null) {
				bool dirty = false;
				string propertyName = localizedTextProperty.name;
				string textFieldText;
				textFieldText = EditorGUILayout.TextField (string.Format ("{0}(En)", propertyName), localizationText.englishText);
				if (textFieldText != localizationText.englishText) {
					localizationText.englishText = textFieldText;
					dirty = true;
				}
				LocalizationText.TextByLanguage[] textByLanguages = localizationText.textsByLanguages;
				foreach (LocalizationText.TextByLanguage tbl in textByLanguages) {
					textFieldText = EditorGUILayout.TextField (string.Format ("{0}({1})", propertyName, tbl.language.ToString ()), tbl.text);
					if (textFieldText != tbl.text) {
						tbl.text = textFieldText;
						dirty = true;
					}
				}
		
				if (dirty)
					EditorUtility.SetDirty (textAsset);
			} else {
				if (GUILayout.Button ("Create Key: " + key)) {
					LocalizationText newKeyText = new LocalizationText ();
					newKeyText.key = key;
					newKeyText.englishText = key;
					
					List<LocalizationText.TextByLanguage> textsByLanguages = new List<LocalizationText.TextByLanguage> ();
					foreach (LocalizationLanguageTypes languageType in System.Enum.GetValues(typeof(LocalizationLanguageTypes))) {
						LocalizationText.TextByLanguage languageText = new LocalizationText.TextByLanguage ();
						languageText.language = languageType;
						languageText.text = key;
						textsByLanguages.Add (languageText);
					}
					newKeyText.textsByLanguages = textsByLanguages.ToArray ();
					
					List<LocalizationText> newTexts = new List<LocalizationText> (textAsset.texts);
					newTexts.Add (newKeyText);
					textAsset.texts = newTexts.ToArray ();
					
					EditorUtility.SetDirty (textAsset);
				}
			}
		}
	}
}
