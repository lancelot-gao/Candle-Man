using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
	
	public delegate void LightStateChangedEventHandler (bool lightOn);

	public event LightStateChangedEventHandler LightStateChanged;

	public Light targetLight;
	public Color ambientLightWhenTorchOn = Color.white;
	public float lightOffRange = 5;
	public float maxLightTime = 10;
	public float lightOnOffTime = 0.3f;
	public ParticleSystem flameParticle;
	[HideInInspector()]
	public bool interactive = true;
	private float defaultLightIntensity = 1;
	private float defaultLightRange = 10;
	private float lightTimeUsed = 0;
	private bool lightOn = true;
	
	public float LightTimeLeftPercent {
		get { return LightTimeLeft / maxLightTime;}
	}
	
	public float LightTimeLeft {
		get { return Mathf.Max (0, maxLightTime - lightTimeUsed);}
	}
	
	public bool LightOn {
		get { return this.lightOn;}
		set {
			this.lightOn = value;
			
			if (lightOn)
				flameParticle.Play ();
			else
				flameParticle.Stop ();
			
			if (LightStateChanged != null)
				LightStateChanged (lightOn);
		}
	}
	
	private float LightStrength {
		get { return Mathf.InverseLerp (0, defaultLightIntensity, targetLight.intensity);}
		set {
			value = Mathf.Clamp01 (value);
			
			targetLight.enabled = value > 0;
			targetLight.intensity = Mathf.Lerp (0, defaultLightIntensity, value);
			targetLight.range = Mathf.Lerp (lightOffRange, defaultLightRange, value);
			
			RenderSettings.ambientLight = Color.Lerp (Color.black, ambientLightWhenTorchOn, value);
		}
	}

	// Use this for initialization
	void Start ()
	{
		defaultLightIntensity = targetLight.intensity;
		defaultLightRange = targetLight.range;
		LightOn = false;
		LightStrength = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (LightOn) {
			if (LightStrength < 1)
				LightStrength += Time.deltaTime * (1 / lightOnOffTime);
		} else {
			if (LightStrength > 0)
				LightStrength -= Time.deltaTime * (1 / lightOnOffTime);
		}
		
		if (interactive) {
			if (LightStrength > 0.5f) {
				lightTimeUsed += Time.deltaTime;
				if (LightTimeLeft <= 0)
					LightOn = false;
			}
			if (LightTimeLeft > 0) {
				if (Input.GetMouseButtonDown (0))
					LightOn = true;
				else if (Input.GetMouseButtonUp (0))
					LightOn = false;
			
				if (Input.GetKeyDown (KeyCode.L))
					LightOn = true;
				else if (Input.GetKeyUp (KeyCode.L))
					LightOn = false;
			}
		}
	}
	
	public void RefillLightLeftTime ()
	{
		iTween.ValueTo (gameObject, iTween.Hash ("from", lightTimeUsed, "to", 0, "onupdate", "UpdateLightTimeUsed"));
	}
	
	private void UpdateLightTimeUsed (float v)
	{
		lightTimeUsed = v;
	}
	
	void OnGUI ()
	{
		string text = string.Format ("LIFE: {0:0.0}   ", LightTimeLeft);
		int barCount = Mathf.CeilToInt (LightTimeLeft * 5);
		for (int i = 0; i < barCount; i++)
			text += "=";
		GUI.Box (new Rect (5, 5, 500, 28), text);
	}
}
