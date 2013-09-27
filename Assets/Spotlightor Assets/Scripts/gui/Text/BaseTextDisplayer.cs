using UnityEngine;
using System.Collections;

public class BaseTextDisplayer : MonoBehaviour
{
	public delegate void TextUpdatedEventHandler (string text);

	public delegate void TextChangedEventHandler (string text);

	public event TextUpdatedEventHandler TextUpdated;
	public event TextChangedEventHandler TextChanged;

	private TextMesh textMesh;
	private bool textMeshInitialized = false;
	
	private TextMesh MyTextMesh {
		get {
			if (textMeshInitialized == false) {
				textMesh = GetComponent<TextMesh> ();
				textMeshInitialized = true;
			}
			return textMesh;
		}
	}
	
	public string Text {
		get {
			if (MyTextMesh)
				return MyTextMesh.text;
			else if (guiText)
				return guiText.text;
			this.LogWarning ("TextMesh or GUIText needed for " + this.GetType ().ToString ());
			return "";
		}
		set {
			string oldValue = this.Text;
			if (MyTextMesh)
				MyTextMesh.text = value;
			else if (guiText)
				MyTextMesh.text = value;
			
			if (TextUpdated != null)
				TextUpdated (value);
			if (TextChanged != null && oldValue != value) 
				TextChanged (value);
		}
	}
	
	public virtual void DisplayText (string text)
	{
		Text = text;
	}
}
