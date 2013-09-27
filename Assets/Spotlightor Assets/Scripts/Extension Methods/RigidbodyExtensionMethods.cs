using UnityEngine;
using System.Collections;

public static class RigidbodyExtensionMethods
{

	public static void SetVelocityX (this Rigidbody rigidbody, float x)
	{
		rigidbody.velocity = new Vector3 (x, rigidbody.velocity.y, rigidbody.velocity.z);
	}

	public static void SetVelocityY (this Rigidbody rigidbody, float y)
	{
		rigidbody.velocity = new Vector3 (rigidbody.velocity.x, y, rigidbody.velocity.z);
	}

	public static void SetVelocityZ (this Rigidbody rigidbody, float z)
	{
		rigidbody.velocity = new Vector3 (rigidbody.velocity.z, rigidbody.velocity.y, z);
	}
}
