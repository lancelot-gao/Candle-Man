using UnityEngine;
using System.Collections;

public abstract class RandomRange<T>
{
	public T min;
	public T max;

	public abstract T RandomValue {
		get;
	}
	
	public RandomRange (T min, T max)
	{
		this.min = min;
		this.max = max;
	}
}