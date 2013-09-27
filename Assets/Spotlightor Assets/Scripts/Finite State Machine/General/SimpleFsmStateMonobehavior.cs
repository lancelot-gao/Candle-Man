using UnityEngine;
using System.Collections;

public abstract class SimpleFsmStateMonobehavior<T,U> : MonoBehaviour, IFsmState<T,U> where T : class
{
	private T owner;

	#region IFsmState[T,U] implementation
	public abstract U StateId {
		get ;
	}

	public virtual void BeginState (U previousState)
	{
		enabled = true;
	}

	public virtual void EndState (U newState)
	{
		enabled = false;
	}

	public T Owner {
		get {
			return owner;
		}
		set {
			owner = value;
		}
	}
	#endregion
	
	void Reset ()
	{
		enabled = false;
	}
}
