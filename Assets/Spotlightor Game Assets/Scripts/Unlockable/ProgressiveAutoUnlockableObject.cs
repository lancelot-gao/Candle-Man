using UnityEngine;
using System.Collections;

public abstract class ProgressiveAutoUnlockableObject : ProgressiveUnlockableObject
{
	public ProgressiveUnlocker unlocker;
	public bool clearPointsAtStart = false;

	public void StartAchievementDetection ()
	{
		if (clearPointsAtStart && !IsUnlocked)
			UpdatePoints (0);
		unlocker.StartDetection (this);
	}
	
	public void StopAchievementDetection ()
	{
		unlocker.StopDetection ();
	}
}
