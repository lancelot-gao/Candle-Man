using UnityEngine;
using System.Collections;

public class HandleProgressBar : ProgressBar
{
	public Transform handle;
	public Vector3 posTo;
	private Vector3 posFrom;

	void Awake ()
	{
		posFrom = handle.localPosition;
	}
	
	#region implemented abstract members of ProgressBar
	protected override void UpdateProgressDisplay (float progress)
	{
		handle.localPosition = Vector3.Lerp (posFrom, posTo, progress);
	}
	#endregion
}
