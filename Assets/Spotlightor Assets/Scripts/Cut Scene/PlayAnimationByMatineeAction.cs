using UnityEngine;
using System.Collections;

public class PlayAnimationByMatineeAction : MonoBehaviour
{
	[System.Serializable]
	public class AnimationSetting
	{
		public MatineeAction action;
		public string actionStartAnimation;
		public string actionCompletedAnimation;
		private Animation target;

		public void RegisterActionEvents (Animation animationTarget)
		{
			target = animationTarget;
			action.Started += OnActionStarted;
			action.Completed += OnActionCompleted;
		}
		
		public void UnregisterActionEvents ()
		{
			action.Started -= OnActionStarted;
			action.Completed -= OnActionCompleted;
		}

		void OnActionStarted (MatineeAction action)
		{
			if (actionStartAnimation != "")
				target.CrossFade (actionStartAnimation);
		}
		
		void OnActionCompleted (MatineeAction action)
		{
			if (actionCompletedAnimation != "")
				target.CrossFade (actionCompletedAnimation);
		}
	}
	
	public Animation animationTarget;
	public AnimationSetting[] animationSettings;
	
	void OnEnable ()
	{
		foreach (AnimationSetting animationSetting in animationSettings) {
			animationSetting.RegisterActionEvents (animationTarget);
		}
	}
	
	void OnDisable ()
	{
		foreach (AnimationSetting animationSetting in animationSettings) {
			animationSetting.UnregisterActionEvents ();
		}
	}
}
