using UnityEngine;
using System.Collections;

[System.Serializable]
public class RandomRangeFloat :RandomRange<float>
{
	public RandomRangeFloat (float min, float max):base(min,max)
	{
	}
	#region implemented abstract members of RandomRange[System.Single]
	public override float RandomValue {
		get {
			return Random.Range (min, max);
		}
	}
	#endregion
}
