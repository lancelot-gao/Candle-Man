using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransitionManager))]
public class TransitionSoundEffects : MonoBehaviour
{
	public AudioClip transitionInSound;
	public AudioClip transitionInCompletedSound;
	public AudioClip transitionOutSound;
	public AudioClip transitionOutCompletedSound;
	
	void Awake ()
	{
		TransitionManager transitionManager = GetComponent<TransitionManager> ();
		transitionManager.TransitionInStarted += OnTransitionInStarted;
		transitionManager.TransitionOutStarted += OnTransitionOutStarted;
		transitionManager.TransitionOutCompleted += HandleTransitionTransitionOutCompleted;
		transitionManager.TransitionInCompleted += HandleTransitionTransitionInCompleted;
	}

	void HandleTransitionTransitionInCompleted (TransitionManager source)
	{
		if (transitionInCompletedSound)
			sUiSoundPlayer.PlaySound (transitionInCompletedSound);
	}

	void HandleTransitionTransitionOutCompleted (TransitionManager source)
	{
		if (transitionOutCompletedSound)
			sUiSoundPlayer.PlaySound (transitionOutCompletedSound);
	}
	
	void OnTransitionInStarted (TransitionManager source)
	{
		if (transitionInSound)
			sUiSoundPlayer.PlaySound (transitionInSound);
	}
	
	void OnTransitionOutStarted (TransitionManager source)
	{
		if (transitionOutSound)
			sUiSoundPlayer.PlaySound (transitionOutSound);
	}
	
	
}
