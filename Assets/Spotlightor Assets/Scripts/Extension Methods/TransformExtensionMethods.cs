using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformExtensionMethods
{

	public static void SetPositionX (this Transform transform, float x)
	{
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);
	}

	public static void SetPositionY (this Transform transform, float y)
	{
		transform.position = new Vector3 (transform.position.x, y, transform.position.z);
	}

	public static void SetPositionZ (this Transform transform, float z)
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, z);
	}
	
	public static void SetLocalPositionX (this Transform transform, float x)
	{
		transform.localPosition = new Vector3 (x, transform.localPosition.y, transform.localPosition.z);
	}

	public static void SetLocalPositionY (this Transform transform, float y)
	{
		transform.localPosition = new Vector3 (transform.localPosition.x, y, transform.localPosition.z);
	}

	public static void SetLocalPositionZ (this Transform transform, float z)
	{
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, z);
	}
	
	public static void SetLocalEulerAngleX (this Transform transform, float x)
	{
		transform.localEulerAngles = new Vector3 (x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleY (this Transform transform, float y)
	{
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleZ (this Transform transform, float z)
	{
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, z);
	}
	
	public static void SetUniformLocalScale (this Transform transform, float uniformScale)
	{
		transform.localScale = Vector3.one * uniformScale;
	}
	
	public static List<Transform> FindChildren (this Transform transform, string childName)
	{
		List<Transform> result = new List<Transform> ();
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);
			if (child.name == childName)
				result.Add (child);
			if (child.childCount > 0)
				result.AddRange (child.FindChildren (childName));
		}
		return result;
	}
	
	public static T FindInParent<T> (this Transform transform) where T : Component
	{
		while (transform != null) {
			T result = transform.GetComponent<T> ();
			if (result != null)
				return result;
			transform = transform.parent;
		}
		return null;
	}
}
