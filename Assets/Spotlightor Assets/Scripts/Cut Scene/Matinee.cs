using UnityEngine;
using System.Collections;

public class Matinee : MonoBehaviour
{
	public delegate void MatineeGenericEventHandler (Matinee matinee);

	public event MatineeGenericEventHandler Completed;
	
	public bool loop = false;
	public MatineeAction[] actions;
	private int currentActionIndex = 0;
	
	public MatineeAction CurrentAction {
		get {
			if (currentActionIndex >= 0 && currentActionIndex < actions.Length)
				return actions [currentActionIndex];
			else
				return null;
		}
	}

	public void Play ()
	{
		if (CurrentAction != null) {
			CurrentAction.Play ();
			CurrentAction.Completed += OnCurrentActionCompleted;
		} else {
			OnActionsAllCompleted ();
		}
	}

	void OnCurrentActionCompleted (MatineeAction action)
	{
		CurrentAction.Completed -= OnCurrentActionCompleted;
		
		currentActionIndex ++;
		if (CurrentAction != null) {
			CurrentAction.Completed += OnCurrentActionCompleted;
			CurrentAction.Play ();
		} else {
			OnActionsAllCompleted ();
		}
	}
	
	private void OnActionsAllCompleted ()
	{
		if (Completed != null)
			Completed (this);
		currentActionIndex = 0;
		if (loop)
			Play ();
	}
	
	public void Pause ()
	{
		if (CurrentAction != null) {
			CurrentAction.Pause ();
			CurrentAction.Completed -= OnCurrentActionCompleted;
		}
	}
	
	public void Stop ()
	{
		if (CurrentAction != null) {
			CurrentAction.Stop ();
			CurrentAction.Completed -= OnCurrentActionCompleted;
		}
		currentActionIndex = 0;
	}
}
