using UnityEngine;
using System.Collections;

public class TypeWriterTextDisplayer : BaseTextDisplayer
{
	public float charsPerSecond = 30;

	#region implemented abstract members of AbstractTextDisplayer
	public override void DisplayText (string text)
	{
		Text = "";
		if (!enabled || !gameObject.activeInHierarchy || !Application.isPlaying) 
			Text = text;
		else {
			StopCoroutine ("TypeWriteAllCharacters");
			StartCoroutine ("TypeWriteAllCharacters", text);
		}
	}
	#endregion
	
	private IEnumerator TypeWriteAllCharacters (string text)
	{
		if (audio && audio.clip)
			audio.Play ();
		string typeWriterText = "";
		for (int i = 0; i < text.Length; i++) {
			yield return new WaitForSeconds(1f/charsPerSecond);
			typeWriterText += text [i];
			Text = typeWriterText;
		}
		if (audio && audio.isPlaying)
			audio.Stop ();
	}
}
