using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public abstract class BasePlayerTrigger : MonoBehaviour
{
	private bool playerInTrigger;

	public bool PlayerInTrigger {
		get { return playerInTrigger;}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			playerInTrigger = true;
			OnPlayerEnter (other.GetComponent<Player> ());
		}
	}
	
	protected abstract void OnPlayerEnter (Player player);
		
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			playerInTrigger = false;
			OnPlayerExit (other.GetComponent<Player> ());
		}
	}
	
	protected abstract void OnPlayerExit (Player player);
	
	void Reset ()
	{
		if (collider == null)
			gameObject.AddComponent<BoxCollider> ();
		collider.isTrigger = true;
	}
}
