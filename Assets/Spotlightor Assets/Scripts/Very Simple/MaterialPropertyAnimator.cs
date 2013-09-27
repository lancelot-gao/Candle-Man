using UnityEngine;
using System.Collections;

public class MaterialPropertyAnimator : MonoBehaviour
{
	public int materialIndex = 0;
	public bool tweenSharedMaterial = false;
	public string propertyName = "_Shininess";
	public AnimationCurve valueByTime = new AnimationCurve (new Keyframe (0, 0), new Keyframe (3, 1));
	public bool ignoreTimeScale = false;
	
	public Material TargetMaterial {
		get {
			if (tweenSharedMaterial) {
				return renderer.sharedMaterials [materialIndex];
			} else {
				return renderer.materials [materialIndex];
			} 
		}
	}
	// Update is called once per frame
	void Update ()
	{
		float currentTime = ignoreTimeScale ? Time.timeSinceLevelLoad : Time.time;
		TargetMaterial.SetFloat (propertyName, valueByTime.Evaluate (currentTime));
	}
}
