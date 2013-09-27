using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class AnimationProgressBar : ProgressBar
{
	private AnimationNormalizedController animationController;

	private AnimationNormalizedController AnimationController {
		get {
			if (animationController == null) 
				animationController = gameObject.AddComponent<AnimationNormalizedController> ();
			return animationController;
		}
	}

	protected override void UpdateProgressDisplay (float progress)
	{
		AnimationController.SampleAtTime (progress);
	}
}
