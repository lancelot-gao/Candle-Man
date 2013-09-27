using UnityEngine;
using System.Collections;

public abstract class ProgressBar : MonoBehaviour
{
	public float maxTweenTime = 0.5f;
	private float currentProgress = 0;
	
	public float CurrentProgress {
		set {
			iTween.Stop (gameObject);
			UpdateProgress (value);
		}
		get { return currentProgress;}
	}
	
	/// <summary>
	/// Tweens the progress to targetProgress.
	/// </summary>
	/// <returns>
	/// The time used to tween progress.
	/// </returns>
	/// <param name='targetProgress'>
	/// Target progress.
	/// </param>
	public float TweenProgressTo (float targetProgress)
	{
		float deltaProgress = Mathf.Abs (targetProgress - currentProgress);
		float tweenTime = Mathf.Max (0.5f * maxTweenTime, maxTweenTime * deltaProgress);
		iTween.ValueTo (gameObject, iTween.Hash ("from", currentProgress, "to", targetProgress, "onupdate", "UpdateProgress",
			"time", tweenTime, "easetype", iTween.EaseType.easeInOutQuad));
		return tweenTime;
	}

	private void UpdateProgress (float progress)
	{
		this.currentProgress = progress;
		UpdateProgressDisplay (progress);
	}
	
	protected abstract void UpdateProgressDisplay (float progress);
}
