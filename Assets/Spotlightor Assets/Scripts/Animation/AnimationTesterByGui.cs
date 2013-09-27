using UnityEngine;
using System.Collections;

/// <summary>
/// Test animations using a simple GUI interface.
/// </summary>
[RequireComponent(typeof(Animation))]
public class AnimationTesterByGui : MonoBehaviour
{
	public AnimationClip idleAnimation;
	public AnimationClip[] animations;
	public int offsetX = 10;
	public int offsetY = 10;
	public int btHeight = 30;
	public int btWidth = 150;
	// Use this for initialization
	void Start ()
	{
		foreach (AnimationClip clip in animations)
			animation [clip.name].layer = 1;
	}

	// Update is called once per frame
	void OnGUI ()
	{
		bool hasAnimation = false;
		for (int i = 0; i < animations.Length; i++) {
			if (GUI.Button (new Rect (offsetX, offsetY + i * btHeight, btWidth, btHeight), animations [i].name)) {
				animation.CrossFade (animations [i].name, 0.5f);
				hasAnimation = true;
			}
		}
		if (!hasAnimation)
			animation.CrossFadeQueued (idleAnimation.name, 0.5f);
	}
}

