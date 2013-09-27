using UnityEngine;
using System.Collections;

public abstract class MatineeAction : MonoBehaviour
{
	public delegate void MatineeActionGenericEventHandler (MatineeAction action);

	public event MatineeActionGenericEventHandler Started;
	public event MatineeActionGenericEventHandler Paused;
	public event MatineeActionGenericEventHandler Stopped;
	public event MatineeActionGenericEventHandler Completed;
	
	public Transform target;
	
	public abstract float Progress {
		get;
	}
	
	public void Play ()
	{
		DoPlay ();
		if (Started != null)
			Started (this);
	}
	
	protected abstract void DoPlay ();

	public void Pause ()
	{
		DoPause ();
		if (Paused != null)
			Paused (this);
	}
	
	protected abstract void DoPause ();

	public void Stop ()
	{
		DoStop ();
		if (Stopped != null)
			Stopped (this);
	}
	
	protected abstract void DoStop ();
	
	protected void OnCompleted ()
	{
		if (Completed != null)
			Completed (this);
	}
}
