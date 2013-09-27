using UnityEngine;
using System.Collections;

public class ColorScreenTransition : iTweenBasedTransitionManager
{
	public Color color = Color.black;
	private Texture2D whiteTexture;

	public Texture WhiteTexture {
		get {
			if (whiteTexture == null) {
				whiteTexture = new Texture2D (1, 1);
				Color[] whitePixels = new Color[1]{Color.white};
				whiteTexture.SetPixels (whitePixels);
				whiteTexture.Apply ();
			}
			return whiteTexture;
		}
	}
	
	public float TintPercent {
		get {
			return color.a;
		}
		protected set {
			color.a = value;
		}
	}

	void OnGUI ()
	{
		if (TintPercent != 0) {
			GUI.color = color;
			GUI.depth = -1;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), WhiteTexture);
		}
	}

	protected override void DoTransitionIn (bool instant)
	{
		iTween.Stop (gameObject, "ValueTo");
		if (instant) {
			TintPercent = 1;
			TransitionInComplete ();
		} else {
			iTween.ValueTo (gameObject, iTween.Hash ("from", TintPercent, "to", 1, 
				"ignoretimescale", ignoreTimeScale, "ease", easeIn, 
				"time", durationIn, "delay", delayIn, "onupdate", "UpdateTintPercent",
			"oncomplete", "TransitionInComplete"));
		}
	}

	protected override void DoTransitionOut (bool instant)
	{
		iTween.Stop (gameObject, "ValueTo");
		if (instant) {
			TintPercent = 0;
			TransitionOutComplete ();
		} else {
			iTween.ValueTo (gameObject, iTween.Hash ("from", TintPercent, "to", 0, 
				"ignoretimescale", ignoreTimeScale, "ease", easeOut, "time", 
				durationOut, "delay", delayOut, "onupdate", "UpdateTintPercent",
			"oncomplete", "TransitionOutComplete"));
		}
	}

	private void UpdateTintPercent (float newAlpha)
	{
		TintPercent = newAlpha;
	}
}
