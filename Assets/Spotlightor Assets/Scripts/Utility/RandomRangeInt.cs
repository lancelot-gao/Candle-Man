using UnityEngine;
using System.Collections;

[System.Serializable]
public class RandomRangeInt:RandomRange<int>
{
	public RandomRangeInt (int min, int max):base(min,max)
	{
	}
	#region implemented abstract members of RandomRange[System.Int32]
	public override int RandomValue {
		get {
			return Random.Range (min, max + 1);
		}
	}
	#endregion
	
}
