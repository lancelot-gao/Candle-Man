using UnityEngine;
using System.Collections;

public abstract class ProgressiveUnlockableObject : UnlocakableObject
{
	public int pointsNeeded = 100;

	public float Progress {
		get {
			if (IsUnlocked)
				return 1;
			return (float)PointsAchieved / pointsNeeded;
		}
	}
	
	public int PointsAchieved {
		get {
			if (IsUnlocked)
				return pointsNeeded;
			else
				return BasicDataTypeStorage.GetInt (PointsDataIdentifier);
		}
	}
	
	public void IncreasePoints (int amount)
	{
		int newPoints = PointsAchieved + amount;
		UpdatePoints (newPoints);
	}
	
	public virtual void UpdatePoints (int newPoints)
	{
		newPoints = Mathf.Clamp (newPoints, 0, pointsNeeded);
		BasicDataTypeStorage.SetInt (PointsDataIdentifier, newPoints);
		if (IsUnlocked && newPoints < pointsNeeded) {
			Lock ();
		} else if (!IsUnlocked && newPoints >= pointsNeeded) {
			Unlock ();
		}
	}
	
	protected string PointsDataIdentifier {
		get {
			return Identifier + "_points";
		}
	}
	
	public override void Lock ()
	{
		BasicDataTypeStorage.SetInt (PointsDataIdentifier, 0);
		base.Lock ();
	}
	
	public override void Unlock ()
	{
		base.Unlock ();
		BasicDataTypeStorage.DeleteInt (PointsDataIdentifier);
	}
}
