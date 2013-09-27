using UnityEngine;
using System.Collections;

public class CameraOrbitWithCharController : CameraControllerBase
{
	#region Fields
	public Transform orbitTarget;
	public float distance = 6;
	public float zoomRange = 3;
	public float xSpeed = 2;
	public float ySpeed = 2;
	public float zoomSpeed = 3;
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	public float initialAngleX = 0;
	public float initialAngleY = 0;
	private float rotateSpeedX = 0;
	private float rotateSpeedY = 0;
	private float angleX = 0;
	private float angleY = 0;
	private Vector3 focusPos;
	private float currentDistance;
	private float targetZoomPercent;
	private float currentZoom;
	private CharacterController controller;
	#endregion

	#region Properties
	public float AngleX {
		get { return angleX; }
	}

	public float AngleY {
		get { return angleY; }
	}
	#endregion

	#region Functions
	protected override void Start ()
	{
		if (orbitTarget)
			focusPos = orbitTarget.transform.position;
		currentDistance = distance;
		targetZoomPercent = 0;
		currentZoom = 0;
		
		angleX = initialAngleX;
		angleY = initialAngleY;
		
		controller = GetComponent<CharacterController> ();
		
		base.Start ();
	}
	#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		transform.LookAt (orbitTarget);
		Vector3 axis = transform.rotation * Vector3.forward;
		
		float currentAngleX = Mathf.Atan2 (axis.x, axis.z) * Mathf.Rad2Deg;
		float currentAngleY = Mathf.Atan2 (-axis.y, Mathf.Sqrt (axis.x * axis.x + axis.z * axis.z)) * Mathf.Rad2Deg;
		
		float currentDistance = Vector3.Distance (transform.position, orbitTarget.position);
		currentDistance = Mathf.Clamp (currentDistance, distance - zoomRange, distance);
		float currentZoom = (distance - currentDistance) / zoomRange;
		
		Reset (currentAngleX, currentAngleY, currentZoom);
	}
	
	public override void AfterTransition ()
	{
		
	}
	#endregion
	void LateUpdate ()
	{
		if (IsInTransition)
			return;
		
		Vector3 axis = transform.rotation * Vector3.forward;
		
		angleX = Mathf.Atan2 (axis.x, axis.z) * Mathf.Rad2Deg;
		angleY = Mathf.Atan2 (-axis.y, Mathf.Sqrt (axis.x * axis.x + axis.z * axis.z)) * Mathf.Rad2Deg;
		
		angleX += rotateSpeedX * 0.1f;
		angleY += rotateSpeedY * 0.1f;
		angleY = Mathf.Clamp (angleY, yMinLimit, yMaxLimit);
		
		Quaternion rot = Quaternion.Euler (angleY, angleX, 0f);
		
		focusPos = Vector3.Lerp (focusPos, orbitTarget.position, 0.1f);
		currentDistance = Mathf.Lerp (currentDistance, distance, 0.1f);
		currentZoom = Mathf.Lerp (currentZoom, targetZoomPercent * zoomRange, 0.1f);
		Vector3 position = focusPos + rot * new Vector3 (0f, 0f, -currentDistance + currentZoom);
		
		controller.Move (position - transform.position);
		
		transform.LookAt (focusPos);
		rotateSpeedX *= 0.9f;
		rotateSpeedY *= 0.9f;
	}

	void UpdateTransformation ()
	{
		Quaternion rotation = Quaternion.Euler (angleY, angleX, 0f);
		Vector3 position = focusPos + rotation * new Vector3 (0f, 0f, -currentDistance + currentZoom);
		
		transform.rotation = rotation;
		transform.position = position;
	}

	public void Reset (float angleX, float angleY, float zoom)
	{
		this.angleX = angleX;
		this.angleY = Mathf.Clamp (angleY % 360, yMinLimit, yMaxLimit);
		rotateSpeedX = 0;
		rotateSpeedY = 0;
		zoom = Mathf.Clamp (zoom, 0, zoomRange);
		currentZoom = zoom;
		targetZoomPercent = currentZoom / zoomRange;
		
		focusPos = orbitTarget.position;
		currentDistance = distance;
		
		UpdateTransformation ();
	}

	public void Orbit (float deltaAngleX, float deltaAngleY)
	{
		rotateSpeedX += deltaAngleX;
		rotateSpeedY += deltaAngleY;
	}

	public void Zoom (float deltaZoom)
	{
		targetZoomPercent += deltaZoom;
		targetZoomPercent = Mathf.Clamp (targetZoomPercent, 0, 1);
	}

	#endregion

	#region IGenericInputReciever ��Ա

	public override void OnDirectionInput (float x, float y, float z)
	{
		if (orbitTarget == null)
			return;
		float angleX = x * xSpeed;
		float angleY = -y * ySpeed;
		Orbit (angleX, angleY);
		
		if (z != 0)
			Zoom (z * zoomSpeed);
	}

	#endregion

	public override void OnIndexInput (uint index)
	{
		
	}
}
