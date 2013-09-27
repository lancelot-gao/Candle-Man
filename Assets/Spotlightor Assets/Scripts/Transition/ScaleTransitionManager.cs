using UnityEngine;
using System.Collections;

/// <summary>
/// 缩放Transition
/// </summary>
public class ScaleTransitionManager : iTweenBasedTransitionManager
{
	#region Fields
	public Vector3 scaleIn = Vector3.one;
	public Vector3 scaleOut = Vector3.zero;
	#endregion

	#region Properties

	#endregion

	#region Functions
	protected override void DoTransitionIn (bool instant)
	{
		iTween.Stop (gameObject, "ScaleTo");
		if (instant) {
			transform.localScale = scaleIn;
			TransitionInComplete ();
		} else
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", scaleIn, "ignoretimescale", ignoreTimeScale, "time", durationIn, "easetype", easeIn, "delay", delayIn, "oncomplete", "TransitionInComplete"));
	}

	protected override void DoTransitionOut (bool instant)
	{
		iTween.Stop (gameObject, "ScaleTo");
		if (instant) {
			transform.localScale = scaleOut;
			TransitionOutComplete ();
		} else
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", scaleOut, "ignoretimescale", ignoreTimeScale, "time", durationOut, "easetype", easeOut, "delay", delayOut, "oncomplete", "TransitionOutComplete"));
	}
	
	void Reset()
	{
		scaleIn = transform.localScale;
	}
	#endregion
}
