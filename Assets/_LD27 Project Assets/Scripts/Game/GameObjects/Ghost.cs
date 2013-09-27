using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
	public float speed = 3;
	public float senseRadius = 10;
	public AnimationCurve escapeSpeedByDistance = new AnimationCurve (new Keyframe (1, 5), new Keyframe (6, 2));
	public float escapeSpeedSlowDownSmoothing = 1;
	private float escapeSpeed = 0;
	
	// Update is called once per frame
	void Update ()
	{
		Player player = GameContext.Player;
		float distance = Vector3.Distance (player.transform.position, transform.position);
		
		
		if (distance < senseRadius && player.LightController.LightOn) {
			escapeSpeed = escapeSpeedByDistance.Evaluate (distance);
		} else {
			escapeSpeed = Mathf.Lerp (escapeSpeed, 0, escapeSpeedSlowDownSmoothing * Time.deltaTime);
			if (escapeSpeed <= 0.001f) 
				escapeSpeed = 0;			
		}
		
		if (distance < senseRadius) {
			transform.LookAt (player.transform.position);
			if (escapeSpeed > 0) {
				transform.position += transform.forward * -escapeSpeed * Time.deltaTime;
			} else {
				transform.position += transform.forward * speed * Time.deltaTime;
			}
		}
	}
}
