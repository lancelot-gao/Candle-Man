using UnityEngine;
using System.Collections;

public class AnimationSfxPlayer : MonoBehaviour {
	public AudioSource sfx;
	
	public void PlaySfx()
	{
		sfx.PlayOneShot(sfx.clip);
	}
}
