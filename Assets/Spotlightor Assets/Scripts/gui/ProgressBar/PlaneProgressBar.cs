using UnityEngine;
using System.Collections;

public class PlaneProgressBar : ProgressBar
{
	public Transform progressPlane;
	public Vector3 startScale = new Vector3 (0, 1, 1);
	public Vector3 endScale = Vector3.one;

	protected override void UpdateProgressDisplay (float progress)
	{
		progressPlane.localScale = Vector3.Lerp (startScale, endScale, progress);
	}
	
}
