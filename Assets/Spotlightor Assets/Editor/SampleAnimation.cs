using UnityEngine;
using UnityEditor;
using System.Collections;

public class SampleAnimation : ScriptableObject {

	[MenuItem("Animation/Sample 0")]
	static void SampleTheAnimation0 ()
	{
		AnimationClip clip = Selection.activeGameObject.GetComponent<Animation>().clip;
		Selection.activeGameObject.SampleAnimation(clip, 0);
	}
	
	[MenuItem("Animation/Sample 1")]
	static void SampleTheAnimation1 ()
	{
		AnimationClip clip = Selection.activeGameObject.GetComponent<Animation>().clip;
		Selection.activeGameObject.SampleAnimation(clip, clip.length);
	}
	
	[MenuItem("Animation/Sample 0.5")]
	static void SampleTheAnimationHalf ()
	{
		AnimationClip clip = Selection.activeGameObject.GetComponent<Animation>().clip;
		Selection.activeGameObject.SampleAnimation(clip, clip.length * 0.5f);
	}
	
	[MenuItem("Animation/Sample 0.25")]
	static void SampleTheAnimationQuarter ()
	{
		AnimationClip clip = Selection.activeGameObject.GetComponent<Animation>().clip;
		Selection.activeGameObject.SampleAnimation(clip, clip.length * 0.25f);
	}
}
