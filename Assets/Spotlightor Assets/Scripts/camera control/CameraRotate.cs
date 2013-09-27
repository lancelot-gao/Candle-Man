using UnityEngine;
using System.Collections;

public class CameraRotate : CameraControllerBase
{
	public bool matchTargetRotationBeforeTransition = true;
	public float sensitivityH = 100f;
	public float sensitivityV = 100f;
	public float minimumH = -360f;
	public float maximumH = 360f;
	public float minimumV = -60f;
	public float maximumV = 60f;
	private float _rotationHSpeed;
	private float _rotationVSpeed;
	private float _rotationH = 0;

	void Update ()
	{
		if (IsInTransition)
			return;
		
		float newRotationH = _rotationH + _rotationHSpeed * Time.deltaTime;
		newRotationH %= 360;
		if (MathAddons.IsInRange (newRotationH, minimumH, maximumH)) {
			transform.Rotate (0, _rotationHSpeed * Time.deltaTime, 0, Space.World);
			_rotationH = newRotationH;
		}
		
		float angleV = 0;
		Vector3 camForward = transform.forward;
		angleV = Mathf.Atan2 (camForward.y, Mathf.Sqrt (camForward.x * camForward.x + camForward.z * camForward.z)) * Mathf.Rad2Deg;
		if ((angleV > maximumV && _rotationVSpeed > 0) || (angleV < minimumV && _rotationVSpeed < 0)) {
		} else
			transform.Rotate (-_rotationVSpeed * Time.deltaTime, 0, 0, Space.Self);
		
		_rotationHSpeed *= 0.9f;
		_rotationVSpeed *= 0.9f;
		
	}

	public void RotateCamera (float rx, float ry)
	{
		_rotationHSpeed = Mathf.Lerp (_rotationHSpeed, rx * sensitivityH, 0.1f);
		
		_rotationVSpeed = Mathf.Lerp (_rotationVSpeed, ry * sensitivityV, 0.1f);
	}

	public void ClearRotationSpeed ()
	{
		_rotationHSpeed = _rotationVSpeed = 0;
	}
	#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		ClearRotationSpeed ();
		if (matchTargetRotationBeforeTransition)
			transform.rotation = target.transform.rotation;
	}
	
	public override void AfterTransition ()
	{
		
	}
	#endregion
	public override void OnDirectionInput (float x, float y, float z)
	{
		RotateCamera (x, y);
	}

	public override void OnIndexInput (uint index)
	{
		
	}
}
