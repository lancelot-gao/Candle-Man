using UnityEngine;
using System.Collections;

public class LightLoverMonster : MonoBehaviour
{
	public float speed = 10;
	public float senseRadius = 20;
	public RandomRangeFloat stopTimeRange = new RandomRangeFloat (3, 5);
	public float timeToStop = 0;
	private float verticalSpeed = 0;

	private CharacterController Controller {
		get { return GetComponent<CharacterController> ();}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Player player = GameContext.Player;
		float distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance < senseRadius) {
			if (player.LightController.LightOn) {
				Vector3 lookPos = player.transform.position;
				lookPos.y = transform.position.y;
				transform.LookAt (lookPos);
				
				timeToStop = stopTimeRange.RandomValue;
			}
		}
		if (timeToStop > 0) {
			timeToStop -= Time.deltaTime;
			Controller.Move (transform.forward * speed * Time.deltaTime);
			
		}
		if (Controller.isGrounded) {
			verticalSpeed = 0;
		} else {
			verticalSpeed += Time.deltaTime * Physics.gravity.y * Time.deltaTime;
			Controller.Move (transform.up * verticalSpeed);
		}
		
		if (transform.position.y < -50)
			Destroy (gameObject);
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.collider.tag == "Deadly") {
			Destroy (gameObject);
		}
	}
}
