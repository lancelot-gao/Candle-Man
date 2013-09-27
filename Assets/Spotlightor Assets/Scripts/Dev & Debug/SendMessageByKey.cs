using UnityEngine;
using System.Collections;

public class SendMessageByKey : MonoBehaviour
{
	[System.Serializable]
	public class KeyMessageBinding
	{
		public KeyCode key;
		public string message;
	}
	
	public GameObject target;
	public KeyMessageBinding[] keyMessageBindings;
	
	void Start ()
	{
		if (target == null)
			target = gameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		foreach (KeyMessageBinding keyMsg in keyMessageBindings) {
			if (Input.GetKeyDown (keyMsg.key))
				target.SendMessage (keyMsg.message, SendMessageOptions.DontRequireReceiver);
		}
	}
}
