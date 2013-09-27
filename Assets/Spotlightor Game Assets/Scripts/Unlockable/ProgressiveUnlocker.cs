using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ProgressiveUnlocker : ScriptableObject
{
	[System.NonSerialized]
	private List<ProgressiveUnlockableObject> targets;
	
	public void UpdatePoints (int newPoints)
	{
		if (targets != null) {
			foreach (ProgressiveUnlockableObject target in targets)
				target.UpdatePoints (newPoints);
		}
	}
	
	public void IncreasePoints (int amount)
	{
		if (targets != null) {
			foreach (ProgressiveUnlockableObject target in targets)
				target.IncreasePoints (amount);
		}
	}
	
	public void StartDetection (ProgressiveUnlockableObject target)
	{
		if (targets == null) {
			targets = new List<ProgressiveUnlockableObject> ();
			Initialize ();
		}
		targets.Add (target);
	}
	
	protected abstract void Initialize ();

	public void StopDetection ()
	{
		if (targets != null) {
			targets.Clear ();
			targets = null;
			Clean ();
		}
	}
	
	protected abstract void Clean ();
}
