using UnityEngine;
using System.Collections;

public class StaticCameraController : CameraControllerBase {
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, transform.forward * 1 + transform.position);
	}

	#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		
	}

	public override void AfterTransition ()
	{
		
	}
	#endregion
	
	public override void OnDirectionInput (float x, float y, float z)
	{
		
	}
	
	public override void OnIndexInput (uint index)
	{
		
	}
}
