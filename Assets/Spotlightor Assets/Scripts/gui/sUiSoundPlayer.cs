using UnityEngine;
using System.Collections;

public static class sUiSoundPlayer
{
	private static AudioSource audio;

	private static AudioSource Audio {
		get {
			if (audio == null) {
				GameObject audioGo = new GameObject ("sUI Sound Player [Dont Destroy]");
				audio = audioGo.AddComponent<AudioSource> ();
				GameObject.DontDestroyOnLoad (audioGo);
			}
			return audio;
		}
	}

	public static void PlaySound (AudioClip audioClip)
	{
		PlaySound (audioClip, 1, 1);
	}
	
	public static void PlaySound (AudioClip audioClip, float volume)
	{
		PlaySound (audioClip, volume, 1);
	}
	
	public static  void PlaySound (AudioClip audioClip, float volume, float pitch)
	{
		Audio.pitch = pitch;
		Audio.PlayOneShot (audioClip, volume);
	}
}
