using UnityEngine;
using System.Collections;

public abstract class BaseNumberDisplayer : BaseTextDisplayer
{
	private float numberValue = 0;

	public float NumberValue {
		get { return numberValue;}
		set {
			UpdateNumberValue (value);
			StopAllCoroutines ();
		}
	}
	
	private void UpdateNumberValue (float newValue)
	{
		numberValue = newValue;
		Text = FormatNumberValueToString (numberValue);
	}
	
	protected abstract string FormatNumberValueToString (float numberValue);
	
	public void TweenValueTo (float targetValue, float time)
	{
		StopAllCoroutines ();
		StartCoroutine (DoTweenValueTo (targetValue, time));
	}
	
	private IEnumerator DoTweenValueTo (float targetValue, float time)
	{
		float timeElapsed = 0;
		float startValue = numberValue;
		while (timeElapsed < time) {
			yield return 1;
			timeElapsed += Time.deltaTime;
			float newScore = Mathf.Lerp ((float)startValue, (float)targetValue, timeElapsed / time);
			UpdateNumberValue (newScore);
		}
	}
}
