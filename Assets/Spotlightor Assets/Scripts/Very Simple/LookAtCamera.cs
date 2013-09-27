using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{
	public Vector3 additionalRotation = Vector3.zero;
	
	void Update ()
	{
		if (renderer == null) {
			transform.LookAt (Camera.main.transform);
			transform.Rotate (additionalRotation, Space.Self);
		}
	}
	
	void OnWillRenderObject ()
	{
		transform.LookAt (Camera.current.transform);
		transform.Rotate (additionalRotation, Space.Self);
	}
	
	void OnBecameVisible ()
	{
		enabled = true;
	}
	
	void OnBecameInvisible ()
	{
		enabled = false;
	}
}
