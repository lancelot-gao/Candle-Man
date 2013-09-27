using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour
{
	public float rotationSpeed = 30;
	public bool isRandomRotateDirection = false;
	public Vector3 axis = Vector3.up;
	public bool randomInitialRotation = false;
	public bool ignoreTimeScale = false;
	private float lastUpdateRealTime = 0;
	
	protected virtual void Start ()
	{
		if (isRandomRotateDirection) {
			if (Random.value >= 0.5f) {
				rotationSpeed = -rotationSpeed;
			}
		}
		if (randomInitialRotation)
			transform.Rotate (axis, Random.Range (0, 360));
	}
	
	void Update ()
	{
		float deltaTime = Time.deltaTime;
		if (ignoreTimeScale) {
			deltaTime = Time.realtimeSinceStartup - lastUpdateRealTime;
			lastUpdateRealTime = Time.realtimeSinceStartup;
		}
		transform.Rotate (axis, rotationSpeed * deltaTime);
		
	}
}

