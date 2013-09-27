using UnityEngine;
using System.Collections;

public class Swinger : MonoBehaviour
{
	public Transform target;
	public float maxAngle = 60;
	public float delay = 0;
	public float time = 3;
	
	// Use this for initialization
	IEnumerator Start ()
	{
		yield return new WaitForSeconds(delay);
		
		target.localEulerAngles = new Vector3 (0, 0, maxAngle);
		
		while (true) {
			TweenTo (-maxAngle);
			yield return new WaitForSeconds(time);
			TweenTo (maxAngle);
			yield return new WaitForSeconds(time);
		}
	}
	
	private void TweenTo (float angle)
	{
		iTween.RotateTo (target.gameObject, iTween.Hash ("time", time, "easetype", iTween.EaseType.easeInOutSine, "rotation", new Vector3 (0, 0, angle), "islocal", true));
	}
}
