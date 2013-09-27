using UnityEngine;
using System.Collections;

public class ConstantRotationWithRandomSpeed : ConstantRotation
{
	public float rotationSpeedVaration = 30;
	// Use this for initialization
	protected override void Start ()
	{
		rotationSpeed += Random.Range (-rotationSpeedVaration, rotationSpeedVaration);
		base.Start();
	}
}
