using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// T: owner class
// U: state id class(enum, string, int...)
public class Fsm<T,U> where T : class
{
	public delegate void StateChangedEventHandler (U newStateId,U previousStateId);

	public delegate void StateGenericEventHandler (U stateId);

	public event StateChangedEventHandler StateChange;
	public event StateGenericEventHandler StateBegin;
	public event StateGenericEventHandler StateEnd;

	private IFsmState<T,U> currentState;
	private List<IFsmState<T,U>> stackedStates;
	private Dictionary<U, IFsmState<T,U>> states;
	private T owner;
	
	public IFsmState<T,U> CurrentState {
		get { return currentState;}
	}
	
	public Fsm (T owner)
	{
		this.owner = owner;
		states = new Dictionary<U, IFsmState<T, U>> ();
	}

	public void AddState (IFsmState<T,U> state)
	{
		state.Owner = this.owner;
		states [state.StateId] = state;
	}
	
	public void RemoveState (IFsmState<T,U> state)
	{
		if (states.ContainsKey (state.StateId)) {
			states.Remove (state.StateId);
			if (stackedStates != null) {
				int i = 0;
				while (i < stackedStates.Count) {
					if (stackedStates [i] == state)
						stackedStates.RemoveAt (i);
					else
						i++;
				}
			}
			state.Owner = null;
		}
	}
	
	public void ForceGotoState (U newStateId)
	{
		DoGotoState (newStateId, true);
	}
	
	public void GotoState (U newStateId)
	{
		DoGotoState (newStateId, false);
	}
	
	private void DoGotoState (U newStateId, bool forceStateTransition)
	{
		if (states.ContainsKey (newStateId)) {
			DoStateTransition (states [newStateId], forceStateTransition);
		} else
			Debug.LogError (string.Format ("Fail to go to state[{0}] because no state with this ID added.", newStateId.ToString ()));
	}
	
	private void DoStateTransition (IFsmState<T, U> newState, bool forceStateTransition)
	{
		IFsmState<T, U> previousState = currentState;
		
		if (currentState != newState || forceStateTransition) {
			U newStateId = newState != null ? newState.StateId : default(U);
			U previousStateId = previousState != null ? previousState.StateId : default(U);
			
			if (currentState != null)
				currentState.EndState (newStateId);
			
			currentState = newState;
			if (currentState != null)
				currentState.BeginState (previousStateId);
			
			if (StateBegin != null)
				StateBegin (newStateId);
			if (StateEnd != null)
				StateEnd (previousStateId);
			if (StateChange != null)
				StateChange (newStateId, previousStateId);
		}
	}
	
	public void PushState (U NewStateId)
	{
		if (stackedStates == null)
			stackedStates = new List<IFsmState<T, U>> ();
		
		GotoState (NewStateId);
		if (currentState != null)
			stackedStates.Add (currentState);
	}
	
	public IFsmState<T, U> ForcePopState ()
	{
		return DoPopState (true);
	}
	
	public IFsmState<T, U> PopState ()
	{
		return DoPopState (false);
	}
	
	public IFsmState<T, U> DoPopState (bool forceStateTransition)
	{
		if (stackedStates != null && stackedStates.Count > 0) {
			IFsmState<T,U> popedState = stackedStates [stackedStates.Count - 1];
			stackedStates.RemoveAt (stackedStates.Count - 1);
			
			DoStateTransition (popedState, forceStateTransition);
			
			return popedState;
		} else {
			Debug.LogWarning ("No state to pop!");
			return null;
		}
	}
	
	public bool IsInState (U stateId)
	{
		return currentState != null && states.ContainsKey (stateId) && currentState == states [stateId];
	}
}
