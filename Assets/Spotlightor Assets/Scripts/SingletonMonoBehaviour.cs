using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton is NOT a good design pattern. Use it wisely.
/// In short, it's just a lazy initialized(out of resource control) global variable(bad).
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T:MonoBehaviour
{
	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType (typeof(T)) as T;
				if (instance == null) {
					GameObject go = new GameObject (string.Format ("Singleton [{0}]", typeof(T).Name));
					instance = go.AddComponent<T> ();
				}
			}
			return instance;
		}
	}
	
	protected virtual void Awake ()
	{
		if (instance != null) {
			if (instance != this)
				Destroy (gameObject);
		} else
			instance = this as T;
	}
	
	protected virtual void OnDestroy ()
	{
		instance = null;
	}
	
	protected virtual void OnApplicationQuit ()
	{
		instance = null;
	}
}
