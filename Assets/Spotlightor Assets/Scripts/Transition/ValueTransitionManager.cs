using UnityEngine;
using System.Collections;

public class ValueTransitionManager : iTweenBasedTransitionManager
{
	public float valueIn = 1;
	public float valueOut = 0;

	private float _value;
	public float Value {
		get { return _value; }
	}

	void UpdateValue (float newValue)
	{
		_value = newValue;
	}

	protected override void DoTransitionIn (bool instant)
	{
		iTween.Stop (gameObject, "ValueTo");
		if (instant) {
			_value = valueIn;
			TransitionInComplete ();
		} else
			iTween.ValueTo (gameObject, iTween.Hash ("from", _value, "to", valueIn, "ignoretimescale", ignoreTimeScale, "delay", delayIn, "time", durationIn,
			"easetype", easeIn, "onupdate", "UpdateValue", "oncomplete", "TransitionInComplete"));
	}

	protected override void DoTransitionOut (bool instant)
	{
		iTween.Stop (gameObject, "ValueTo");
		if (instant) {
			_value = valueOut;
			TransitionOutComplete ();
		} else
			iTween.ValueTo (gameObject, iTween.Hash ("from", _value, "to", valueOut, "ignoretimescale", ignoreTimeScale, "delay", delayOut, "time", durationOut,
			"easetype", easeOut, "onupdate", "UpdateValue", "oncomplete", "TransitionOutComplete"));
	}
}
