using UnityEngine;
using System.Collections;

public class SetupAnimationLayer : MonoBehaviour
{
	[System.Serializable]
	public class AnimationLayerSetting
	{
		public string animationName;
		public int layer = 1;
	}
	
	public Animation target;
	public AnimationLayerSetting[] layerSettings;
	
	void Awake ()
	{
		if (target == null)
			target = animation;
		if (target != null) {
			foreach (AnimationLayerSetting setting in layerSettings) {
				target [setting.animationName].layer = setting.layer;
			}
		} else
			this.LogWarning ("Target is null and has no Animation component!");
		
		Destroy (this);
	}
	
	void Reset ()
	{
		if (animation != null)
			target = animation;
		else
			target = GetComponentInChildren<Animation> ();
	}
}
