using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class AnimationFxbyTime : MonoBehaviour
{
	public class FxSetting
	{
		public float time;
		//TODO:Ignore time
	}

	[System.Serializable]
	public class VfxSetting : FxSetting
	{
		public Transform vfxPrefab;
		public Transform waypoint;
		public bool addAsChild;
	}

	[System.Serializable]
	public class SfxSetting : FxSetting
	{
		public AudioSource audioSource;
		public AudioClip clip;
		public float volume = 1;
	}
	public SfxSetting[] sfxs;
	public VfxSetting[] vfxs;
	private float _animationTimeLastFrame;
	// Use this for initialization
	void Start ()
	{
		_animationTimeLastFrame = animation [animation.clip.name].time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float currentAnimationTime = animation [animation.clip.name].time;
		currentAnimationTime %= animation [animation.clip.name].length;
		foreach (SfxSetting sfx in sfxs) {
			if (sfx.time > _animationTimeLastFrame && sfx.time <= currentAnimationTime) {
				sfx.audioSource.PlayOneShot (sfx.clip, sfx.volume);
			}
		}
		foreach (VfxSetting vfx in vfxs) {
			if (vfx.time > _animationTimeLastFrame && vfx.time <= currentAnimationTime) {
				Transform vfxInstance = Instantiate (vfx.vfxPrefab, vfx.waypoint.position, vfx.waypoint.rotation) as Transform;
				if (vfx.addAsChild)
					vfxInstance.parent = vfx.waypoint;
			}
		}
		_animationTimeLastFrame = currentAnimationTime;
	}
}
