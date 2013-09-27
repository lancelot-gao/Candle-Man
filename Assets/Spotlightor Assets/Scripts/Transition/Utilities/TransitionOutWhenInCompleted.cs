using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransitionManager))]
public class TransitionOutWhenInCompleted : MonoBehaviour
{
	void Awake ()
	{
		GetComponent<TransitionManager> ().TransitionInCompleted += OnTransitionInCompleted;
	}

	void OnTransitionInCompleted (TransitionManager source)
	{
		GetComponent<TransitionManager> ().TransitionOut ();
	}
}
