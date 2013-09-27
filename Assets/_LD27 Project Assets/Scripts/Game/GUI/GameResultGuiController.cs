using UnityEngine;
using System.Collections;

public class GameResultGuiController : MonoBehaviour
{
	public TransitionManager titleWinTransition;
	public TransitionManager titleLoseTransition;
	// Use this for initialization
	void Start ()
	{
		titleWinTransition.TransitionOut (true);
		titleLoseTransition.TransitionOut (true);
		Messenger.AddListener (MessageTypes.GameEnded, OnGameEnded);
	}
	
	private void OnGameEnded (object data)
	{
		bool win = (bool)data;
		if (win)
			titleWinTransition.TransitionIn ();
		else
			titleLoseTransition.TransitionIn ();
	}
}
