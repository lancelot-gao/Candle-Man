using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FsmExampleIdleState))]
[RequireComponent(typeof(FsmExampleAttackState))]
public class FsmExample : MonoBehaviour
{
	public enum StateTypes
	{
		Invalid,
		Idle,
		Attack
	}
	private Fsm<FsmExample, StateTypes> fsm;

	public Fsm<FsmExample, StateTypes> StateMachine {
		get { return this.fsm; }
	}
	
	public FsmExampleState CurrentState {
		get { return fsm != null ? fsm.CurrentState as FsmExampleState : null; }
	}

	// Use this for initialization
	void Start ()
	{
		fsm = new Fsm<FsmExample, FsmExample.StateTypes> (this);
		StateMachine.AddState (GetComponent<FsmExampleIdleState> ());
		StateMachine.AddState (GetComponent<FsmExampleAttackState> ());
		StateMachine.GotoState (StateTypes.Idle);
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
			CurrentState.Jump();
	}
}
