using UnityEngine;
using System.Collections;


public class UVScroller : MonoBehaviour
{
	public bool scrollSharedMaterial = false;
	public Vector2 speed = new Vector2 (1, 0);
	public bool ignoreTimeScale = false;
	public int materialIndex = 0;
	public string propertyName = "_MainTex";
	private Vector2 defaultTextureOffset;

	
	public Material TargetMaterial {
		get {
			if (scrollSharedMaterial) {
				return renderer.sharedMaterials [materialIndex];
			} else {
				return renderer.materials [materialIndex];
			} 
		}
	}
	
	protected virtual void Start ()
	{
		if (scrollSharedMaterial) {
			defaultTextureOffset = TargetMaterial.GetTextureOffset (propertyName);
		}
	}
	
	protected virtual void Update ()
	{
		ScrollMaterial (TargetMaterial);
	}
	
	protected virtual void ScrollMaterial (Material material)
	{
		float delta = ignoreTimeScale ? Time.deltaTime / Time.timeScale : Time.deltaTime;
		Vector2 newOffset = TargetMaterial.GetTextureOffset (propertyName) + delta * speed;
		newOffset.x -= Mathf.Floor(newOffset.x);
		newOffset.y -= Mathf.Floor(newOffset.y);
		TargetMaterial.SetTextureOffset (propertyName, newOffset);
	}
	
	protected virtual void OnApplicationQuit ()
	{
		if (scrollSharedMaterial)
			TargetMaterial.SetTextureOffset (propertyName, defaultTextureOffset);
	}
}
