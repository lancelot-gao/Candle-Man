using UnityEngine;
using System.Collections;

/// <summary>
/// ����̨һ����ת��������ת��Yaw��������̧ͷת����Pitch��
/// </summary>
public class FPSCameraControl : CameraControllerBase
{
	public float sensitivityH = 15F;
	public float sensitivityV = 15F;
	public float minimumH = -360F;
	public float maximumH = 360F;
	public float minimumV = -60F;
	public float maximumV = 60F;
	public bool matchTargetRotationBeforeTransition = true;
	public bool matchTargetPositionBeforeTransition = true;
	public float distanctToStopMatching = 5;
	public LayerMask floorHitTestMask;
	private float rotationHorizontalSpeed = 0;
	private float rotationVerticalSpeed = 0;
	private float rotationVertical = 0;

	protected Transform Root {
		get { return transform.parent; }
	}

	public float CameraHeight {
		get {
			CharacterController charController = Root.GetComponent<CharacterController> ();
			if (charController != null)
				return charController.height;
			else
				return 1.75f;
		}
	}
#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		float distanceToTarget = Vector3.Distance (transform.position, target.transform.position);
		
		if (matchTargetPositionBeforeTransition && distanceToTarget > distanctToStopMatching) {
			Vector3 targetPosition = target.transform.position;
			Ray landingRay = new Ray (targetPosition, Vector3.down);
			RaycastHit hit;
			if (Physics.Raycast (landingRay, out hit, CameraHeight, floorHitTestMask)) {
				targetPosition.y = hit.point.y + CameraHeight;
			}
			Root.position = targetPosition;
		}
		if (matchTargetRotationBeforeTransition && distanceToTarget > distanctToStopMatching) {
			
			Vector3 targetForward = target.transform.forward;
			float targetRotV = Mathf.Atan2 (targetForward.y, Mathf.Sqrt (targetForward.x * targetForward.x + targetForward.z * targetForward.z));
			rotationVertical = Mathf.Rad2Deg * targetRotV;
			float targetRotH = target.transform.eulerAngles.y;
			Root.localEulerAngles = new Vector3 (0, targetRotH, 0);
			//transform.eulerAngles = new Vector3(-_rotationV, , 0);
			transform.localEulerAngles = new Vector3 (-rotationVertical, 0, 0);
		} else {
			rotationVertical = 0;
			transform.localEulerAngles = Vector3.zero;
		}
		rotationVerticalSpeed = rotationHorizontalSpeed = 0;
	}
	
	public override void AfterTransition ()
	{
		
	}
	#endregion
	void Update ()
	{
		if (IsInTransition)
			return;

		if (Root != null)
			Root.localEulerAngles = new Vector3 (0, Root.localEulerAngles.y + rotationHorizontalSpeed, 0);
		else
			Debug.LogError ("FPS Camera controller need a parent.");

		rotationVertical += rotationVerticalSpeed;
		rotationVertical = Mathf.Clamp (rotationVertical, minimumV, maximumV);
		Quaternion cameraTargetLocalRotation = Quaternion.Euler (new Vector3 (-rotationVertical, 0, 0));
		transform.localRotation = cameraTargetLocalRotation;

		rotationHorizontalSpeed *= 0.9f;
		rotationVerticalSpeed *= 0.9f;
	}

	public void RotateCamera (float rx, float ry)
	{
		rotationHorizontalSpeed = Mathf.Lerp (rotationHorizontalSpeed, rx * sensitivityH, 0.1f);

		rotationVerticalSpeed = Mathf.Lerp (rotationVerticalSpeed, ry * sensitivityV, 0.1f);
	}

	/// <summary>
	/// ������תx,yΪ0����rotationSpeed����
	/// </summary>
	public void Reset ()
	{
		rotationHorizontalSpeed = rotationVerticalSpeed = 0;
		rotationVertical = 0;
	}

	public override void OnDirectionInput (float x, float y, float z)
	{
		if (IsInTransition)
			return;
		RotateCamera (x, y);
	}
	
	public override void OnIndexInput (uint index)
	{
		
	}
	
	protected override void OnBecameUnFunctional ()
	{
		base.OnBecameUnFunctional ();
		Root.gameObject.SetActive (false);// parent is used for movement.
	}
	
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		base.OnBecameFunctional (forTheFirstTime);
		Root.gameObject.SetActive (true);// parent is used for movement.
	}

}
