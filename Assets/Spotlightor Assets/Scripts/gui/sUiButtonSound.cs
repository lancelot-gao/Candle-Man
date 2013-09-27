using UnityEngine;
using System.Collections;

[RequireComponent(typeof(sUiButton))]
public class sUiButtonSound : MonoBehaviour
{
	[System.Serializable]
	public class SoundSetting
	{
		public AudioClip audioClip;
		public float volume = 1;
		public float pitch = 1;
	}
	public SoundSetting rollOverSound;
	public SoundSetting rollOutSound;
	public SoundSetting mouseDownSound;
	public SoundSetting mouseUpSound;
	public SoundSetting clickSound;
	private sUiButton button;

	private sUiButton Button {
		get {
			if (button == null)
				button = GetComponent<sUiButton> ();
			return button;
		}
	}
	// Use this for initialization
	void Start ()
	{
		if (rollOutSound.audioClip)
			Button.RollOver += OnRollOverButton;
		if (rollOutSound.audioClip)
			Button.RollOut += OnRollOutButton;
		if (mouseDownSound.audioClip)
			Button.MouseDown += OnMouseDownButton;
		if (mouseUpSound.audioClip)
			Button.MouseUp += OnMouseUpButton;
		if (clickSound.audioClip)
			Button.Click += OnClickButton;
	}

	void OnClickButton (RealClickListener source)
	{
		PlaySound (clickSound);
	}
	
	void OnMouseUpButton (MouseEventDispatcher source)
	{
		PlaySound (mouseUpSound);
	}

	void OnMouseDownButton (MouseEventDispatcher source)
	{
		PlaySound (mouseDownSound);
	}

	void OnRollOutButton (MouseEventDispatcher source)
	{
		PlaySound (rollOutSound);
	}

	void OnRollOverButton (MouseEventDispatcher source)
	{
		PlaySound (rollOverSound);
	}
	
	private void PlaySound (SoundSetting sound)
	{
		if (sound.audioClip != null) {
			sUiSoundPlayer.PlaySound (sound.audioClip, sound.volume, sound.pitch);
		}
	}
}
