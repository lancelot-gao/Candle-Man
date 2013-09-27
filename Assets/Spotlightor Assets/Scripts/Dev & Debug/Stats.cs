using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
	private const float UpdateInterval = 1f;
	public KeyCode visibilitySwitchKey = KeyCode.F;
	private float fps = 0;
	private int framesCounter = 0;
	private float lastUpdateTime;
	private bool isVisible = true;
	// Use this for initialization
	void Start ()
	{
		lastUpdateTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (visibilitySwitchKey))
			isVisible = !isVisible;
		
		framesCounter ++;
		float timeElpased = Time.time - lastUpdateTime;
		if (timeElpased >= UpdateInterval) {
			fps = (float)framesCounter / timeElpased;
			lastUpdateTime = Time.time;
			framesCounter = 0;
		}
	}
	
	void OnGUI ()
	{
		if (isVisible)
			GUI.Box (new Rect (Screen.width - 100, 0, 100, 30), string.Format ("FPS:{0:00.0}", fps));
	}
}
