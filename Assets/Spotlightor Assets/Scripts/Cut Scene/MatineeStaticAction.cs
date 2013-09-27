using UnityEngine;
using System.Collections;

public class MatineeStaticAction : MatineeAction
{
	public float duration;
	private float timeElapsed = 0;
	
	#region implemented abstract members of MatineeAction
	public override float Progress {
		get {
			return timeElapsed / duration;
		}
	}

	protected override void DoPlay ()
	{
		target.transform.position = transform.position;
		target.transform.rotation = transform.rotation;
		
		StopCoroutine ("DelayAndComplete");
		StartCoroutine ("DelayAndComplete");
	}
	
	private IEnumerator DelayAndComplete ()
	{
		while (timeElapsed < duration) {
			yield return 1;
			timeElapsed += Time.deltaTime;
		}
		OnCompleted ();
		timeElapsed = 0;
	}

	protected override void DoPause ()
	{
		StopCoroutine ("DelayAndComplete");
	}

	protected override void DoStop ()
	{
		StopCoroutine ("DelayAndComplete");
		timeElapsed = 0;
	}
	#endregion
}
