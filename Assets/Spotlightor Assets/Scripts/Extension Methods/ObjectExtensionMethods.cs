using UnityEngine;
using System.Collections;
using System.Linq;

public static class ObjectExtensionMethods
{
	public static T FindObjectOfType<T> (this Object obj) where T:Object
	{
		return Object.FindObjectOfType (typeof(T)) as T;
	}
	
	public static T[] FindObjectsOfType<T> (this Object obj) where T:Object
	{
		return Object.FindObjectsOfType (typeof(T)).Cast<T> ().ToArray ();
	}
}
