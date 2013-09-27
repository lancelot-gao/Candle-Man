using UnityEngine;
using System.Collections;

/// <summary>
/// Designed to be used as HP/MP in game.
/// </summary>
public class SafeInt
{
	public delegate void ArrivedAtStateEventHandler ();

	public delegate void ValueChangedEventHandler (int amount);
	
	public event ValueChangedEventHandler ValueChange;
	public event ArrivedAtStateEventHandler ReachMin;
	public event ArrivedAtStateEventHandler ReachMax;
	
	private int minValue;
	private int maxValue;
	private int currentValue;
	
	public SafeInt (int min, int max, int defaultValue)
	{
		if (min >= max) {
			Debug.LogError ("SafeInt max should be greater than min, max will be set to min+1");
			max = min + 1;
		}
		this.minValue = min;
		this.maxValue = max;
		this.CurrentValue = defaultValue;
	}
	
	public int CurrentValue {
		get {
			return currentValue;
		}
		set {
			if (value == currentValue)
				return;
			
			int oldValue = currentValue;
			currentValue = Mathf.Clamp (value, minValue, maxValue);
			if (currentValue != oldValue) {
				if (ValueChange != null)
					ValueChange (currentValue - oldValue);
				
				if (currentValue == maxValue && ReachMax != null)
					ReachMax ();
				else if (currentValue == minValue && ReachMin != null)
					ReachMin ();
				
			}
		}
	}
}
