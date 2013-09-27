using UnityEngine;
using System.Collections;

public abstract class SimpleFsmState<T,U> : IFsmState<T,U> where T : class
{
	private T owner;

	#region IFsmState[T,U] implementation
	public abstract U StateId {
		get ;
	}

	public abstract void BeginState (U previousState);

	public abstract void EndState (U newState);

	public T Owner {
		get {
			return owner;
		}
		set {
			owner = value;
		}
	}
	#endregion
}
