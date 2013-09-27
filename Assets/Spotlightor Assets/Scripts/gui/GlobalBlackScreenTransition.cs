using UnityEngine;
using System.Collections;

/// <summary>
/// Global Singleton BlackScreen.
/// Dont' need to add it to scene, call it from script directly is simpler.
/// </summary>
public class GlobalBlackScreenTransition : ColorScreenTransition
{
	private static GlobalBlackScreenTransition instance;

	public static GlobalBlackScreenTransition Instance {
		get {
			if (instance == null) {
				GameObject go = new GameObject ("Global Black Screen Transition");
				instance = go.AddComponent<GlobalBlackScreenTransition> ();
				instance.autoActivate = true;
				instance.durationIn = instance.durationOut = 1f;
				instance.color = Color.black;
				DontDestroyOnLoad (go);
			}
			return instance;
		}
	}
	
	protected override void Awake ()
	{
		if (GlobalBlackScreenTransition.instance != null && GlobalBlackScreenTransition.instance != this) {
			GameObject.Destroy (this);
		} else
			base.Awake ();
	}
}
