using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public LightController LightController {
		get { return GetComponent<LightController> ();}
	}
}
