using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour
{

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
			Messenger.Broadcast (MessageTypes.GameEnded, false);
		else if (other.tag == "Creature")
			Destroy (other.gameObject);
	}
}
