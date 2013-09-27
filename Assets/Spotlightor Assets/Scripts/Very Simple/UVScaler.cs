using UnityEngine;
using System.Collections;

public class UVScaler : MonoBehaviour
{

	public bool scaleSharedMaterial = false;
	public Vector2 scaleRange = new Vector2 (0.5f, 1.5f);
	public float scaleTime = 1f;
	public bool ignoreTimeScale = false;
	public int materialIndex = 0;
	public string propertyName = "_MainTex";
	private Vector2 originalScale;

	public Material TargetMaterial {
		get {
			if (scaleSharedMaterial) {
				return renderer.sharedMaterials [materialIndex];
			} else {
				return renderer.materials [materialIndex];
			} 
		}
	}
	
	protected virtual void Start ()
	{
		originalScale = TargetMaterial.GetTextureScale (propertyName);
	}
	
	void Update ()
	{
		ScaleMaterial ();
	}
	
	protected virtual void ScaleMaterial ()
	{
		float numPeriodsSinceStart = ignoreTimeScale ? Time.timeSinceLevelLoad / scaleTime : Time.time / scaleTime;
		float sinValue = Mathf.Sin (numPeriodsSinceStart * Mathf.PI);
		float t = Mathf.InverseLerp (-1, 1, sinValue);
		TargetMaterial.SetTextureScale (propertyName, originalScale * Mathf.Lerp (scaleRange.x, scaleRange.y, t));
	}
	
	protected virtual void OnApplicationQuit ()
	{
		if (scaleSharedMaterial)
			TargetMaterial.SetTextureScale (propertyName, originalScale);
	}
	
}
