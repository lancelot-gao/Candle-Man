using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TypedFsmState<T>
{
	public abstract void BeginStateOf (T owner, TypedFsmState<T> previousState);

	public abstract void EndStateOf (T owner, TypedFsmState<T> newState);

	public abstract void ExecuteStateLogicOf (T owner);
}

public interface ITypedFsmClient<T>
{
	TypedFsmState<T> CurrentState {
		get;
		set;
	}
}

public static class TypedFsm <T> where T:ITypedFsmClient<T>
{
	private static Dictionary<System.Type,  TypedFsmState<T>> stateInstances = new Dictionary<System.Type, TypedFsmState<T>> ();
	
	public static void GotoState<U> (T owner) where U: TypedFsmState<T>, new()
	{
		System.Type stateType = typeof(U);
		if (!stateInstances.ContainsKey (stateType)) {
			stateInstances [stateType] = new U ();
		}
		TypedFsmState<T> previousState = owner.CurrentState;
			
		TypedFsmState<T> newState = stateInstances [stateType];
		if (owner.CurrentState != null)
			owner.CurrentState.EndStateOf (owner, newState);
			
		owner.CurrentState = newState;
		if (newState != null)
			newState.BeginStateOf (owner, previousState);
	}
}
