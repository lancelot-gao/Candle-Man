using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class TypedFsmExample : MonoBehaviour, ITypedFsmClient<TypedFsmExample>
{
	public class IdleState : TypedFsmState<TypedFsmExample>
	{
		#region implemented abstract members of TypedFsmState[TypedFsmExample]
		public override void BeginStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> previousState)
		{
			owner.renderer.material.color = Color.grey;
			owner.StartCoroutine ("DelayAndGotoAttackState");
		}

		public override void EndStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> newState)
		{
			owner.StopCoroutine ("DelayAndGotoAttackState");
		}

		public override void ExecuteStateLogicOf (TypedFsmExample owner)
		{
			
		}
		#endregion
		
	}

	public class AttackState : TypedFsmState<TypedFsmExample>
	{
		#region implemented abstract members of TypedFsmState[TypedFsmExample]
		public override void BeginStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> previousState)
		{
			owner.renderer.material.color = Color.red;
			owner.StartCoroutine ("DelayAndGotoIdleState");
		}

		public override void EndStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> newState)
		{
			owner.StopCoroutine ("DelayAndGotoIdleState");
		}

		public override void ExecuteStateLogicOf (TypedFsmExample owner)
		{
			owner.transform.Rotate (Vector3.up, owner.speed * Time.deltaTime);
		}
		#endregion
		
	}
	
	public float speed = 3;
	public RandomRangeFloat idleTimeRange;
	public RandomRangeFloat attackTimeRange;
	private  TypedFsmState<TypedFsmExample> currentState;

	#region ITypedFsmClient[TypedFsmExample] implementation
	public TypedFsmState<TypedFsmExample> CurrentState {
		get {
			return currentState;
		}
		set {
			currentState = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start ()
	{
		TypedFsm<TypedFsmExample>.GotoState<IdleState> (this);
	}
	
	public IEnumerator DelayAndGotoAttackState ()
	{
		yield return new WaitForSeconds(idleTimeRange.RandomValue);
		TypedFsm<TypedFsmExample>.GotoState<AttackState> (this);
	}
	
	public IEnumerator DelayAndGotoIdleState ()
	{
		yield return new WaitForSeconds(attackTimeRange.RandomValue);
		TypedFsm<TypedFsmExample>.GotoState<IdleState> (this);
	}
	
	void Update ()
	{
		CurrentState.ExecuteStateLogicOf (this);
	}
}
