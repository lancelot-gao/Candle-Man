using UnityEngine;
using System.Collections;

public class ColorTweenEffect : MonoBehaviour
{
	public string propertyName = "_Color";
	public Color[] colors;
	public float speed = 1;
	public bool tweenSharedMaterial = false;
	public bool usHsbColorLerp = false;
	public bool ignoreTimeScale = false;
	private int startColorIndex = 0;
	private float progress = 0;
	private float lastUpdateRealTime = 0;
	
	public Color StartColor {
		get { return colors [StartColorIndex];}
	}
	
	public Color TargetColor {
		get {
			int targetColorIndex = StartColorIndex + 1;
			targetColorIndex %= colors.Length;
			return colors [targetColorIndex];		
		}
	}
	
	protected int StartColorIndex {
		get { return startColorIndex; }
		set {
			startColorIndex = value % colors.Length;
			if (startColorIndex < 0)
				startColorIndex += colors.Length;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		if (colors.Length == 0) {
			Debug.LogWarning ("ColorTweenEffects with 0 colors to tween! Auto remove the component.");
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		float deltaTime = Time.deltaTime;
		if (ignoreTimeScale) {
			deltaTime = Time.realtimeSinceStartup - lastUpdateRealTime;
			lastUpdateRealTime = Time.realtimeSinceStartup;
		}
		progress += speed * deltaTime;
		Color color = usHsbColorLerp ? HsbColor.LerpColorByHsb (StartColor, TargetColor, progress) : Color.Lerp (StartColor, TargetColor, progress);
		
		TextMesh textMesh = GetComponent<TextMesh> ();
		if (textMesh != null) {
			textMesh.color = color;
		} else {
			if (tweenSharedMaterial)
				renderer.sharedMaterial.SetColor (propertyName, color);
			else
				renderer.material.SetColor (propertyName, color);
		}
		
		if (progress > 1) {
			progress = 0;
			StartColorIndex++;
		}
	}
}
