using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomInput : SingletonMonoBehaviour<CustomInput>
{
	[System.Serializable]
	public class InputSetting
	{
		public string axisName = "";
		public float sensitivity = 3;
		public float gravity = 1;
		public float dead = 0.001f;
		public bool snap = false;
		public string altInputAxisName = "";
		private float smoothedValue = 0;
		private float rawValue = 0;
		private bool rawValueUpdated = false;
		
		public float SmoothedValue {
			get { return smoothedValue;}
		}
		
		public float RawValue {
			set { 
				rawValue = value;
				rawValueUpdated = true;
			}
		}
		
		public InputSetting (float sensitivity, float gravity, float dead, bool snap)
		{
			this.sensitivity = sensitivity;
			this.gravity = gravity;
			this.dead = dead;
			this.snap = snap;
		}
		
		public void UpdateSmoothedValue ()
		{
			if (rawValueUpdated == false && altInputAxisName != "")
				rawValue = Input.GetAxisRaw (altInputAxisName);
			
			if (rawValue == 0) {
				if (smoothedValue != 0) {
					smoothedValue = Mathf.Lerp (smoothedValue, 0, Time.deltaTime * gravity);
					if (Mathf.Abs (smoothedValue) <= dead)
						smoothedValue = 0;
				}
			} else {
				if (snap) {
					if (rawValue >= 0 && smoothedValue < 0 || rawValue <= 0 && smoothedValue > 0)
						smoothedValue = 0;
				}
				smoothedValue = Mathf.Lerp (smoothedValue, rawValue, Time.deltaTime * sensitivity);
			}
			rawValueUpdated = false;
		}
	}
	
	public InputSetting[] inputSettings;
	private Dictionary<string, InputSetting> inputSettingDictionary;
	
	private Dictionary<string, InputSetting> InputSettingDictionary {
		get {
			if (inputSettingDictionary == null) {
				inputSettingDictionary = new Dictionary<string, InputSetting> ();
				foreach (InputSetting inputSetting in inputSettings) {
					inputSettingDictionary.Add (inputSetting.axisName, inputSetting);
				}
			}
			return inputSettingDictionary;
		}
	}
	
	public float GetAxis (string axisName)
	{
		InputSetting inputSetting = GetInputSettingByAxisName (axisName);
		if (inputSetting != null)
			return inputSetting.SmoothedValue;
		else
			return 0;
	}
	
	public void SetAxisRawValue (string axisName, float rawValue)
	{
		InputSetting inputSetting = GetInputSettingByAxisName (axisName);
		if (inputSetting != null)
			inputSetting.RawValue = rawValue;
	}
	
	private InputSetting GetInputSettingByAxisName (string axisName)
	{
		InputSetting inputSetting;
		if (InputSettingDictionary.TryGetValue (axisName, out inputSetting)) {
			return inputSetting;
		} else {
			Debug.LogWarning ("Cannot find Custom Input Axis with name: " + axisName);
			return null;
		}
	}
	
	void Update ()
	{
		foreach (InputSetting inputSetting in inputSettings)
			inputSetting.UpdateSmoothedValue ();
	}
}
