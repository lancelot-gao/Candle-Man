using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera)), ExecuteInEditMode()]
public class CameraTransparencySortByZ : MonoBehaviour
{
	void Awake ()
	{
		camera.transparencySortMode = TransparencySortMode.Orthographic;
	}
}
