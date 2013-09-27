using UnityEngine;
using System.Collections;

public class FsmExampleAttackState : FsmExampleState
{
	public float rotateSpeed = 30;
	public RandomRangeFloat durationRange = new RandomRangeFloat (3, 5);
	#region implemented abstract members of FsmExampleState
	
	public override void BeginState (FsmExample.StateTypes previousState)
	{
		base.BeginState (previousState);
		StartCoroutine ("DelayAndGotoIdle");
	}
	
	public override void EndState (FsmExample.StateTypes newState)
	{
		base.EndState (newState);
		StopCoroutine ("DelayAndGotoIdle");
	}

	public override FsmExample.StateTypes StateId {
		get {
			return FsmExample.StateTypes.Attack;
		}
	}

	public override void Jump ()
	{
		Owner.transform.position += Owner.transform.forward * 0.3f;
	}
	#endregion
	
	void Update ()
	{
		transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime);
	}
	
	private IEnumerator DelayAndGotoIdle ()
	{
		yield return new WaitForSeconds(durationRange.RandomValue);
		Owner.StateMachine.GotoState (FsmExample.StateTypes.Idle);
	}
}
