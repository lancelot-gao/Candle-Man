using UnityEngine;
using System.Collections;

/// <summary>
/// 自毁 
/// </summary>
public class AutoDestruct : MonoBehaviour
{
	public float delay = 3;
	// Use this for initialization
	void Start ()
	{
		GameObject.Destroy(gameObject, delay);
	}
}

