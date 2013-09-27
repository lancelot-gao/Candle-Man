using UnityEngine;
using System.Collections;

public class CameraOrbit : CameraControllerBase
{
    #region Fields
	public Transform orbitTarget;
	public float distance = 6;
	public float zoomRange = 3; // Min distance = distance - zoomRange
	public float xSpeed = 2;
	public float ySpeed = 2;
	public float zoomSpeed = 3;
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	public float xMinLimit = -360;
	public float xMaxLimit = 360;
	public bool matchTargetBeforeTransition = true;
	private float targetAngleX = 0;
	private float targetAngleY = 0;
	private float angleX = 0;
	private float angleY = 0;
	private Vector3 focusPos;
	private float currentDistance;
	private float targetZoomPercent;
	private float currentZoom;
    #endregion

    #region Properties
	public float AngleX { get { return angleX; } }

	public float AngleY { get { return angleY; } }
    #endregion

    #region Functions
	
	private void Awake ()
	{
		if (orbitTarget)
			focusPos = orbitTarget.transform.position;
		ResetToSimilarRotation (transform);
	}
	
	#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		if (matchTargetBeforeTransition)
			ResetToSimilarRotation (target.transform);
	}
	
	public override void AfterTransition ()
	{
		
	}
	#endregion	
	void LateUpdate ()
	{
		if (IsInTransition)
			return;

		this.angleX += Mathf.DeltaAngle (this.angleX, targetAngleX) * 0.1f;
		this.angleY += Mathf.DeltaAngle (this.angleY, targetAngleY) * 0.1f;

		focusPos = Vector3.Lerp (focusPos, orbitTarget.transform.position, 0.1f);// TODO: Make it FPS independable
		currentDistance = Mathf.Lerp (currentDistance, distance, 0.1f);
		currentZoom = Mathf.Lerp (currentZoom, targetZoomPercent * zoomRange, 0.1f);

		UpdateTransformation ();
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
		this.angleX = targetAngleX = angleX;
		this.angleY = targetAngleY = Mathf.Clamp (angleY % 360, yMinLimit, yMaxLimit);
		zoom = Mathf.Clamp (zoom, 0, zoomRange);
		currentZoom = zoom;
		targetZoomPercent = zoomRange == 0 ? 0 : currentZoom / zoomRange;
		
		focusPos = orbitTarget.position;
		currentDistance = distance;
		
		UpdateTransformation ();
	}
	
	public void ResetToSimilarRotation (Transform rotationTarget)
	{
		Vector3 targetForward = rotationTarget.forward;
		targetForward.Normalize ();
		
		float angleY = -Mathf.Atan2 (targetForward.y, Mathf.Sqrt (targetForward.x * targetForward.x + targetForward.z * targetForward.z)) * Mathf.Rad2Deg;
		this.angleY = targetAngleY = Mathf.Clamp (angleY, yMinLimit, yMaxLimit);
		this.angleX = targetAngleX = Mathf.Atan2 (targetForward.x, targetForward.z) * Mathf.Rad2Deg;
		
		float d = Vector3.Distance (rotationTarget.transform.position, orbitTarget.transform.position);
		currentZoom = distance - d;
		currentZoom = Mathf.Clamp (currentZoom, 0, zoomRange);
		targetZoomPercent = zoomRange == 0 ? 0 : currentZoom / zoomRange;
		currentDistance = distance;
		
		UpdateTransformation ();
	}

	/// <summary>
	/// Orbit around target in euler degrees.
	/// </summary>
	/// <param name="angleX">Horizontal</param>
	/// <param name="angleY">Vertical</param>
	public void Orbit (float angleX, float angleY)
	{
		targetAngleX += angleX;
		targetAngleX %= 360;
        
		targetAngleY += angleY;
		targetAngleY %= 360;
		targetAngleY = Mathf.Clamp (targetAngleY, yMinLimit, yMaxLimit);
		targetAngleX = Mathf.Clamp (targetAngleX, xMinLimit, xMaxLimit);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="dZoom">zoomPercent的改变值，zoomPercent的含义：0(no zoom) - 1(zoom end)</param>
	public void Zoom (float dZoom)
	{
		targetZoomPercent += dZoom;
		targetZoomPercent = Mathf.Clamp (targetZoomPercent, 0, 1);
	}
    #endregion

    #region IGenericInputReciever 成员

	public override void OnDirectionInput (float x, float y, float z)
	{
		if (IsInTransition)
			return;

		if (target == null)
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
	
	void OnDrawGizmos ()
	{
		if (orbitTarget != null) {
			Gizmos.color = Color.gray;
			Gizmos.DrawWireSphere (orbitTarget.transform.position, distance);
			Gizmos.DrawWireSphere (orbitTarget.transform.position, distance - zoomRange);
		}
	}
}
