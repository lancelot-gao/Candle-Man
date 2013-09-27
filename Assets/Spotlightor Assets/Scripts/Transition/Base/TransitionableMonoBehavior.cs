using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransitionManager))]
public class TransitionableMonoBehavior : MonoBehaviour, ITransition
{
	public event TransitionManager.TransitionManagerGenericEventHandler TransitionInStarted {
		add{ Transition.TransitionInStarted += value;}
		remove{ Transition.TransitionInStarted -= value;}
	}

	public event TransitionManager.TransitionManagerGenericEventHandler TransitionInCompleted {
		add{ Transition.TransitionInCompleted += value;}
		remove{ Transition.TransitionInCompleted -= value;}
	}

	public event TransitionManager.TransitionManagerGenericEventHandler TransitionOutStarted {
		add{ Transition.TransitionOutStarted += value;}
		remove{ Transition.TransitionOutStarted -= value;}
	}

	public event TransitionManager.TransitionManagerGenericEventHandler TransitionOutCompleted {
		add{ Transition.TransitionOutCompleted += value;}
		remove{ Transition.TransitionOutCompleted -= value;}
	}

	private TransitionManager transitionManager;

	public TransitionManager Transition {
		get {
			if (transitionManager == null) {
				transitionManager = GetComponent<TransitionManager> ();
				if (transitionManager == null)
					Debug.LogError ("No TransitionManager in " + name);
			}
			return transitionManager; 
		}
	}

    #region ITransition 

	public void TransitionIn ()
	{
		Transition.TransitionIn ();
	}

	public void TransitionIn (bool instant)
	{
		Transition.TransitionIn (instant);
	}

	public void TransitionOut ()
	{
		Transition.TransitionOut ();
	}

	public void TransitionOut (bool instant)
	{
		Transition.TransitionOut (instant);
	}

    #endregion
}
