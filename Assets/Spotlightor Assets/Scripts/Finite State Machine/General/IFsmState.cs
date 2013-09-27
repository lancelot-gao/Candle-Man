using UnityEngine;
using System.Collections;
using System;

// T: owner class
// U: state id class(enum, string, int...)
public interface IFsmState<T,U> where T : class
{
	U StateId {
		get;
	}
	
	T Owner {
		get;
		set;
	}
	
	void BeginState (U previousState);

	void EndState (U newState);
}
