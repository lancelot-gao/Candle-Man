using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
	public GameObject objectPrefab;
	public float interval = 3;
	public Vector3 spawnPosition;
	// Use this for initialization
	IEnumerator Start ()
	{
		while (true) {
			yield return new WaitForSeconds(interval);
			
			GameObject obj = GameObject.Instantiate (objectPrefab) as GameObject;
			obj.transform.parent = transform;
			obj.transform.localPosition = spawnPosition;
			obj.transform.localRotation = Quaternion.identity;
			
			obj.SetActive (true);
		}
	}
}
