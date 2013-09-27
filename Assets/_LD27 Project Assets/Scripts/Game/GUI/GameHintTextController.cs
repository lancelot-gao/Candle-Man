using UnityEngine;
using System.Collections;

public class GameHintTextController : SingletonMonoBehaviour<GameHintTextController>
{
	public BaseTextDisplayer hintDisplayer;
	public AudioClip soundHintShow;

	void Start ()
	{
		hintDisplayer.Text = "";
	}

	public void DisplayHintTextForTime (string text, float time)
	{
		DisplayHintText (text);
		Invoke ("ClearHintText", time);
	}
	
	public void DisplayHintText (string text)
	{
		CancelInvoke ("ClearHintText");
		hintDisplayer.DisplayText (text);
		sUiSoundPlayer.PlaySound (soundHintShow);
	}
	
	public void ClearHintText ()
	{
		hintDisplayer.DisplayText ("");
	}
}
