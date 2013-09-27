using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
	public GameObject explosionPrefab;
	public float speed = 10;

	// Update is called once per frame
	void Update ()
	{
		transform.position += speed * transform.forward * Time.deltaTime;
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (!other.isTrigger) {
			Destroy (gameObject);
			GameObject.Instantiate (explosionPrefab, transform.position, transform.rotation);
		}
	}
}
