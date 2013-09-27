using UnityEngine;
using System.Collections;

public class SmoothFollowBehind : FunctionalMonoBehaviour
{
	public Transform target;
	public Vector3 lookPositionOffset = Vector3.zero;
	public float distance = 3;
	public float height = 2;
	public float heightDamping = 2;
	public float rotationDamping = 3;
	public bool followMoveDir = false;
	public float minSpeedToFollowMoveDir = 0.1f;
	private Vector3 oldTargetPosition;
	
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		oldTargetPosition = target.position;
	}
	
	protected override void OnBecameUnFunctional ()
	{
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{
		if (target != null && target.rigidbody != null) {
			transform.position = GetSmoothFollowPosition (Time.fixedDeltaTime);
			transform.rotation = GetSmoothFollowRotation ();
		}
	}
	
	protected virtual void LateUpdate ()
	{
		if (target != null && target.rigidbody == null) {
			transform.position = GetSmoothFollowPosition (Time.deltaTime);
			transform.rotation = GetSmoothFollowRotation ();
		}
	}
	
	private Vector3 GetSmoothFollowPosition (float deltaTime)
	{
		Vector3 dir = target.forward;
		if (followMoveDir) {
			Vector3 deltaMovement = target.position - oldTargetPosition;
			Vector3 velocity = deltaMovement / deltaTime;
			if (velocity.magnitude >= minSpeedToFollowMoveDir)
				dir = deltaMovement.normalized;
		}
		
		float targetAngle = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg;
		float targetHeight = target.position.y + height;
		float currentAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
			
		currentAngle = Mathf.LerpAngle (currentAngle, targetAngle, deltaTime * rotationDamping);
		currentHeight = Mathf.Lerp (currentHeight, targetHeight, deltaTime * heightDamping);
			
		Quaternion currentRotation = Quaternion.Euler (0, currentAngle, 0);
		Vector3 targetPos = target.position - currentRotation * Vector3.forward * distance;
		targetPos.y = currentHeight;
		
		oldTargetPosition = targetPos;
		
		return targetPos;
	}
	
	private Quaternion GetSmoothFollowRotation ()
	{
		return Quaternion.LookRotation (target.position + lookPositionOffset - transform.position);
	}
}
