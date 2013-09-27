using UnityEngine;
using System.Collections;

public class PlayerSoundEffects : MonoBehaviour
{
	public AudioClip soundLightOn;
	public AudioClip soundLightOff;
	// Use this for initialization
	void Start ()
	{
		GetComponent<LightController> ().LightStateChanged += HandleLightStateChanged;
	}

	void HandleLightStateChanged (bool lightOn)
	{
		if (lightOn)
			audio.PlayOneShot (soundLightOn);
		else
			audio.PlayOneShot (soundLightOff,0.2f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
