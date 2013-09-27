using UnityEngine;
using System.Collections;

public class PlayerDeathDetector : MonoBehaviour
{

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.collider.tag == "Deadly") {
			Messenger.Broadcast (MessageTypes.GameEnded, false);
		}
	}
}
