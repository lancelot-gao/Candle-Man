using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseTextDisplayer))]
public class TextChangedSoundEffect : FunctionalMonoBehaviour
{
	public AudioClip textChangedSound;
	public float minSoundPlayInterval = 0.3f;
	private float lastSoundPlayTime = 0;

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		GetComponent<BaseTextDisplayer> ().TextChanged += HandleTextChanged;
	}

	protected override void OnBecameUnFunctional ()
	{
		GetComponent<BaseTextDisplayer> ().TextChanged -= HandleTextChanged;
	}
	
	void HandleTextChanged (string text)
	{
		if (textChangedSound != null) {
			if (Time.time - lastSoundPlayTime > minSoundPlayInterval) {
				sUiSoundPlayer.PlaySound (textChangedSound);
				lastSoundPlayTime = Time.time;
			}
		}
	}
	
}
