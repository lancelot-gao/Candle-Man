using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Path))]
public class MatineeFlybyAction : MatineeAction
{
	public float speed = 1;
	private float percentOnPath = 0;

	protected Path FlyPath {
		get { return GetComponent<Path> ();}
	}
	
	public float PercentOnPath {
		set {
			percentOnPath = Mathf.Clamp01 (value);
			FlyPath.PlaceObjectOnPath (target, percentOnPath);
		}
	}
	#region implemented abstract members of MatineeAction
	public override float Progress {
		get {
			return percentOnPath;
		}
	}

	protected override void DoPlay ()
	{
		StopCoroutine ("FlyOnPath");
		StartCoroutine ("FlyOnPath");
	}
	
	private IEnumerator FlyOnPath ()
	{
		PercentOnPath = Progress;
		
		float pathLength = FlyPath.EstimatedLength;
		float pathPercentSpeed = speed / pathLength;
		while (Progress < 1) {
			yield return 1;
			PercentOnPath = Progress + pathPercentSpeed * Time.deltaTime;
		}
		
		OnCompleted ();
		
		percentOnPath = 0;
	}

	protected override void DoPause ()
	{
		StopCoroutine ("FlyOnPath");
	}

	protected override void DoStop ()
	{
		StopCoroutine ("FlyOnPath");
		percentOnPath = 0;
	}
	#endregion
}
