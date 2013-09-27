using UnityEngine;
using System.Collections;

public class ScaleByPhysicalScreenSize : MonoBehaviour
{
	[System.Serializable]
	public class ScaleSetting
	{
		public float scale = 1;
		public float physicalScreenHeight = 10;
	}
	public ScaleSetting maxScreenSetting;
	public ScaleSetting minScreenSetting;
	public float invalidDeviceScreenDpi = 93;// ipad = 132, iphone retina 326, dell U24 = 93
	
	void Start ()
	{
		UpdateScale ();
	}

	[ContextMenu ("Update Scale")]
	public void UpdateScale ()
	{
		float screenDpi = Screen.dpi;
		if (screenDpi == 0)
			screenDpi = invalidDeviceScreenDpi;
		
		float physicalScreenHeightInInches = Screen.height / screenDpi;
		float t = Mathf.InverseLerp (minScreenSetting.physicalScreenHeight, maxScreenSetting.physicalScreenHeight, physicalScreenHeightInInches);
		float scale = Mathf.Lerp (minScreenSetting.scale, maxScreenSetting.scale, t);
		transform.localScale = Vector3.one * scale;
	}
}
