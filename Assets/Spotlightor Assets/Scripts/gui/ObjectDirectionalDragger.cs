using UnityEngine;
using System.Collections;

public class ObjectDirectionalDragger : MonoBehaviour
{
	private const string ProgressTweenName = "tween progress";
	public Camera viewCamera;
	public Rect dragViewportArea = new Rect (0.2f, 0.2f, 0.6f, 0.6f);
	public Transform dragTarget;
	public Vector3 targetStartPos;
	public Vector3 targetEndPos;
	public float dragStrength = 12f;
	public float stopDeceleration = 3;
	public float borderResistance = 0.5f;
	public float borderRestoreStrength = 8;
	public float snapInterval = 0;
	public float snapSpeedThreshold = 0.5f;
	public float snapStrength = 4f;
	public iTween.EaseType tweenEase = iTween.EaseType.easeInOutQuart;
	public float tweenSpeed = 0.5f;
	private bool isDragging = false;
	private float dragStartDistance = 0;
	private float dragStartFingerNearestDistance = 0;
	private float distanceSpeed = 0;
	private float distance = 0;
	private bool isSnapping = false;
	
	private Camera ViewCamera {
		get { return viewCamera != null ? viewCamera : Camera.main;}
	}

	protected bool IsDragging {
		get {
			return isDragging;
		}
		set {
			if (isDragging != value) {
				isDragging = value;
				isSnapping = false;
				if (isDragging)
					StopTweenProgress ();
			}
		}
	}
	
	public float Progress {
		get { return Distance / MaxDistance;}
		set {
			Distance = value * MaxDistance;
			distanceSpeed = 0;
		}
	}
	
	private float Distance {
		get { return distance;}
		set {
			distance = value;
			dragTarget.position = TargetStartWorldPosition + (TargetEndWorldPosition - TargetStartWorldPosition).normalized * value;
		}
	}
	
	private float MaxDistance {
		get { return Vector3.Distance (TargetStartWorldPosition, TargetEndWorldPosition);}
	}
	
	private Vector3 TargetStartWorldPosition {
		get { return dragTarget.parent == null ? targetStartPos : dragTarget.parent.TransformPoint (targetStartPos);}
	}
	
	private Vector3 TargetEndWorldPosition {
		get { return dragTarget.parent == null ? targetEndPos : dragTarget.parent.TransformPoint (targetEndPos);}
	}
	
	private bool IsFingerDown {
		get {
			if (SystemInfo.deviceType == DeviceType.Handheld)
				return Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Began;
			else
				return Input.GetMouseButtonDown (0);
		}
	}
	
	private bool IsFingerUp {
		get {
			if (SystemInfo.deviceType == DeviceType.Handheld)
				return Input.touchCount == 0;
			else
				return Input.GetMouseButtonUp (0);
		}
	}
	
	private Vector2 CurrentFingerPosition {
		get {
			if (SystemInfo.deviceType == DeviceType.Handheld && Input.touchCount > 0)
				return Input.touches [0].position ;
			else
				return new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		}
	}
	
	public bool IsTweening {
		get { return iTween.Count (dragTarget.gameObject, "value") > 0; }
	}
	// Use this for initialization
	void Start ()
	{
		Distance = 0;
		distanceSpeed = 0;
	}
	
	public void TweenProgressTo (float progress)
	{
		distanceSpeed = 0;
		IsDragging = false;
		StopTweenProgress ();
		
		progress = Mathf.Clamp01 (progress);
		float tweenDistance = Mathf.Abs (progress - Progress) * MaxDistance;
		float time = tweenDistance / tweenSpeed;
		iTween.ValueTo (dragTarget.gameObject, iTween.Hash ("from", Progress, "to", progress, "easetype", tweenEase, "time", time, 
			"onupdate", "OnTweenProgressUpdate", "onupdatetarget", gameObject, "name", ProgressTweenName));
	}
	
	private void OnTweenProgressUpdate (float progress)
	{
		this.Progress = progress;
	}
	
	private void StopTweenProgress ()
	{
		iTween.StopByName (dragTarget.gameObject, ProgressTweenName);
	}
	
	void Update ()
	{
		Vector2 fingerPos = CurrentFingerPosition;
		if (IsFingerDown) {
			Vector3 fingerViewportPos = ViewCamera.ScreenToViewportPoint (fingerPos);
			if (dragViewportArea.Contains (new Vector2 (fingerViewportPos.x, fingerViewportPos.y))) {
				IsDragging = true;
				dragStartDistance = Distance;
				dragStartFingerNearestDistance = ScreenPositionToNearestDistance (fingerPos);
				distanceSpeed = 0;
			}
		} else if (IsFingerUp)
			IsDragging = false;
		
		if (IsDragging && !IsTweening) {
			
			float deltaDistance = ScreenPositionToNearestDistance (fingerPos) - dragStartFingerNearestDistance;
			float targetDistance = deltaDistance + dragStartDistance;
			
			if (targetDistance > MaxDistance) {
				float dDistance = targetDistance - MaxDistance;
				dDistance *= 1f / (1f + dDistance * borderResistance);
				targetDistance = MaxDistance + dDistance;
			} else if (targetDistance < 0) {
				float dDistance = - targetDistance;
				dDistance *= 1f / (1f + dDistance * borderResistance);
				targetDistance = 0 - dDistance;
			}

			Distance = Mathf.SmoothDamp (Distance, targetDistance, ref distanceSpeed, 1f / dragStrength);
		} else {
			if (Distance >= 0 && Distance <= MaxDistance) {
				
				if (snapInterval <= 0 || !isSnapping) {
					bool oldSpeedGreaterThanZero = distanceSpeed > 0;
					distanceSpeed -= stopDeceleration * distanceSpeed * Time.deltaTime;
					bool newSpeedGreaterThanZero = distanceSpeed > 0;
					if (oldSpeedGreaterThanZero != newSpeedGreaterThanZero)
						distanceSpeed = 0;
					if (distanceSpeed != 0) 
						Distance += distanceSpeed * Time.deltaTime;
					
					if (Mathf.Abs (distanceSpeed) < snapSpeedThreshold)
						isSnapping = true;
				} else {
					int nearestSnapIndex = Mathf.RoundToInt (Distance / snapInterval);
					float nearestSnapTargetDistance = nearestSnapIndex * snapInterval;
					Distance = Mathf.SmoothDamp (Distance, nearestSnapTargetDistance, ref distanceSpeed, 1f / snapStrength);
				}
			} else {
				float targetDistance = Distance < 0 ? 0 : MaxDistance;
				Distance = Mathf.SmoothDamp (Distance, targetDistance, ref distanceSpeed, 1f / borderRestoreStrength);
			}
		}
	}
	
	private float ScreenPositionToNearestDistance (Vector2 screenPos)
	{
		Vector3 targetStartScreenPos = ViewCamera.WorldToScreenPoint (TargetStartWorldPosition);
		Vector3 targetEndScreenPos = ViewCamera.WorldToScreenPoint (TargetEndWorldPosition);
		targetStartScreenPos.z = targetEndScreenPos.z = 0;
		
		float dragPathScreenDistance = Vector3.Distance (targetStartScreenPos, targetEndScreenPos);
		
		if (dragPathScreenDistance == 0)
			return 0;
		
		if (Mathf.Abs (targetStartScreenPos.x - targetEndScreenPos.x) < 0.01f) {
			float nearestDistance = MaxDistance * (screenPos.y - targetStartScreenPos.y) / dragPathScreenDistance;
			return targetStartScreenPos.y < targetEndScreenPos.y ? nearestDistance : -nearestDistance;
		} else if (Mathf.Abs (targetStartScreenPos.y - targetEndScreenPos.y) < 0.01f) {
			float nearestDistance = MaxDistance * (screenPos.x - targetStartScreenPos.x) / dragPathScreenDistance;
			return targetEndScreenPos.x > targetStartScreenPos.x ? nearestDistance : -nearestDistance;
		} else {
			float dragPathScreenAngle = Mathf.Atan2 (targetEndScreenPos.y - targetStartScreenPos.y, targetEndScreenPos.x - targetStartScreenPos.x);
			float distanceOfHorizontalLineIntersection = (screenPos.y - targetStartScreenPos.y) / Mathf.Sin (dragPathScreenAngle);
			float distanceOfVerticalLineIntersection = (screenPos.x - targetStartScreenPos.x) / Mathf.Cos (dragPathScreenAngle);
			float d = distanceOfVerticalLineIntersection - distanceOfHorizontalLineIntersection;
			float nearestScreenDistance = distanceOfHorizontalLineIntersection + d * Mathf.Cos (dragPathScreenAngle) * Mathf.Cos (dragPathScreenAngle);
			float nearestPathProgress = nearestScreenDistance / dragPathScreenDistance;
			return nearestPathProgress * MaxDistance;
		}
	}
	
	void OnDrawGizmosSelected ()
	{
		float cameraDistance = ViewCamera.transform.InverseTransformPoint (transform.position).z;
		Vector3 topLeftViewportPos = new Vector3 (dragViewportArea.x, dragViewportArea.y, cameraDistance);
		Vector3 bottomRightViewportPos = new Vector3 (dragViewportArea.xMax, dragViewportArea.yMax, cameraDistance);
		Vector3 topRightViewportPos = new Vector3 (dragViewportArea.xMax, dragViewportArea.y, cameraDistance);
		Vector3 bottomLeftViewportPos = new Vector3 (dragViewportArea.x, dragViewportArea.yMax, cameraDistance);
		
		Vector3[] cornerPoints = new Vector3[4];
		cornerPoints [0] = ViewCamera.ViewportToWorldPoint (topLeftViewportPos);
		cornerPoints [1] = ViewCamera.ViewportToWorldPoint (topRightViewportPos);
		cornerPoints [2] = ViewCamera.ViewportToWorldPoint (bottomRightViewportPos);
		cornerPoints [3] = ViewCamera.ViewportToWorldPoint (bottomLeftViewportPos);
		for (int i = 0; i < cornerPoints.Length; i++) {
			Gizmos.DrawLine (cornerPoints [i], cornerPoints [i + 1 >= cornerPoints.Length ? 0 : i + 1]);
		}
		
	}
}
