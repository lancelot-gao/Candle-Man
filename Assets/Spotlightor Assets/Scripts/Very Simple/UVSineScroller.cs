using UnityEngine;
using System.Collections;

public class UVSineScroller : MonoBehaviour
{
	public float speed = 0.5f;
	public Vector2 amplitude = new Vector2 (1, 1);
	public string propertyName = "_MainTex";
	
	private Vector2 _defaultOffset;
	void Start()
	{
		_defaultOffset = renderer.material.GetTextureOffset(propertyName);
	}
	// Update is called once per frame
	void Update ()
	{
		renderer.material.SetTextureOffset(propertyName, _defaultOffset + amplitude * Mathf.Sin(Time.time * speed));
	}
}
