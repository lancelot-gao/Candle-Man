using UnityEngine;
using System.Collections;

public class FsmExampleIdleState : FsmExampleState
{
	public string idleMessage = "Idle";
	#region implemented abstract members of FsmExampleState

	public override FsmExample.StateTypes StateId {
		get {
			return FsmExample.StateTypes.Idle;
		}
	}

	public override void Jump ()
	{
		transform.SetUniformLocalScale (transform.localScale.x * 1.1f);
	}
	#endregion
	
	void OnMouseDown ()
	{
		Owner.StateMachine.GotoState (FsmExample.StateTypes.Attack);
	}
}
