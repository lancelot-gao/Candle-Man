using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject.DontDestroyOnLoad (gameObject);
	}
}
