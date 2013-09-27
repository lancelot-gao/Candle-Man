using UnityEngine;
using System.Collections;

public class HideGlobalBlackScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GlobalBlackScreenTransition.Instance.TransitionOut ();
	}
}
