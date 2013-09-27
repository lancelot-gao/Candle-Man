using UnityEngine;
using System.Collections;
// Inspired by:http://answers.unity3d.com/questions/47885/easiest-way-to-rotate-a-cubemap.html
[RequireComponent(typeof(Skybox))]
public class ConstantRotateSkybox : MonoBehaviour {
	public Vector3 rotationSpeed = new Vector3(0,1,0);
	private Vector3 _currentRotation = Vector3.zero;

	// Update is called once per frame
	void Update () {
		_currentRotation += rotationSpeed * Time.deltaTime;
		Matrix4x4 m = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler(_currentRotation), new Vector3(1,1,1) );
        GetComponent<Skybox>().material.SetMatrix ("_Rotation", m);
	}
}
