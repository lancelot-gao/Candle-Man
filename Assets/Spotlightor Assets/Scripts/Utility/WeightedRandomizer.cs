using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeightedRandomizer<T> where T: class
{
	private List<T> elements;
	private List<float> elementWeights;
	
	public WeightedRandomizer ()
	{
		elements = new List<T> ();
		elementWeights = new List<float> ();
	}
	
	public void Add (T element, float weight)
	{
		elements.Add (element);
		elementWeights.Add (weight);
	}
	
	public void Remove (T element)
	{
		int index = elements.IndexOf (element);
		if (index != -1) {
			elements.RemoveAt (index);
			elementWeights.RemoveAt (index);
		} else
			Debug.LogWarning ("Cannot find element to remove: " + element.ToString ());
	}
	
	public T GetRandomElement ()
	{
		T result = null;
		if (elements.Count > 0) {
			float totalWeight = 0;
			foreach (float weight in elementWeights)
				totalWeight += weight;
			
			float randomValue = Random.Range (0, totalWeight);
			float currentWeight = 0;
			for (int i = 0; i < elements.Count; i++) {
				currentWeight += elementWeights [i];
				if (currentWeight >= randomValue) {
					result = elements [i];
					break;
				}
			}
		}
		return result;
	}
}
