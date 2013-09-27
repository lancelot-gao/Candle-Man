using UnityEngine;
using System.Collections;

public class MoveAlongPath : CameraControllerBase
{
	public Path path;
	public float moveSpeed = 1;
	public float targetPercentOnPath = 0;
	public bool loop = true;
	protected float _percentOnPath = 0;
	public bool _dirFollow = true;
	public float dirFollowStrengthDamping = 0.9f;
	public bool matchTargetPositionBeforeTransition = true;
	private float _dirFollowStrength = 0;
	private Transform _currentPathPoint;
	private Transform _nextPathPoint;

	public float PercentOnPath {
		get { return this._percentOnPath; }
		set { this._percentOnPath = this.targetPercentOnPath = value; }
	}

	public Transform CurrentPathPoint {
		get { return this._currentPathPoint; }
	}

	public Transform NextPathPoint {
		get { return this._nextPathPoint; }
	}
	#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		int nearestNodeIndex = matchTargetPositionBeforeTransition ? path.FindNearestPathPointIndex (target.transform.position) : path.FindNearestPathPointIndex (transform.position);
		if (nearestNodeIndex == path.pathPoints.Length - 1) {
			Debug.Log ("Cannot transition to the last path node.");
			nearestNodeIndex--;//last path node will never be auto transitioned.
		}
		_percentOnPath = targetPercentOnPath = nearestNodeIndex / (path.pathPoints.Length - 1.0f);
		transform.position = iTween.PointOnPath (path.pathPoints, _percentOnPath);
		
		int numSections = path.pathPoints.Length - 1;
		int currPt = Mathf.Min (Mathf.FloorToInt (_percentOnPath * (float)numSections), numSections - 1);
		float percentPerSegment = 1f / numSections;
		Quaternion targetRotation = Quaternion.Slerp (path.pathPoints [currPt].rotation, path.pathPoints [currPt + 1].rotation, (_percentOnPath - percentPerSegment * currPt) / percentPerSegment);
		
		transform.rotation = targetRotation;
	}
	
	public override void AfterTransition ()
	{
		
	}
	#endregion
	void LateUpdate ()
	{
		if (IsInTransition)
			return;
		
		targetPercentOnPath = Mathf.Clamp (targetPercentOnPath, 0, 1);
		_percentOnPath = Mathf.Lerp (_percentOnPath, targetPercentOnPath, 3f * Time.deltaTime);
		if (targetPercentOnPath == 1 && targetPercentOnPath - _percentOnPath < 0.001f) {
			_percentOnPath = 1;
			if (loop)
				PercentOnPath = 0;
		}
		transform.position = iTween.PointOnPath (path.pathPoints, _percentOnPath);
		
		int numSections = path.pathPoints.Length - 1;
		int currPt = Mathf.Min (Mathf.FloorToInt (_percentOnPath * (float)numSections), numSections - 1);
		_currentPathPoint = path.pathPoints [currPt];
		_nextPathPoint = path.pathPoints [currPt + 1];
		
		if (_dirFollow) {
			if (_dirFollowStrength > 0) {
				float percentPerSegment = 1f / numSections;
				Quaternion targetRotation = Quaternion.Slerp (_currentPathPoint.rotation, _nextPathPoint.rotation, (_percentOnPath - percentPerSegment * currPt) / percentPerSegment);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, _dirFollowStrength);
				target.transform.localRotation = Quaternion.Slerp (target.transform.localRotation, Quaternion.identity, _dirFollowStrength);
			}
			
			_dirFollowStrength *= dirFollowStrengthDamping;
			if (_dirFollowStrength <= 0.001f)
				_dirFollowStrength = 0;
		}
	}

	public override void OnDirectionInput (float x, float y, float z)
	{
		if (IsInTransition)
			return;
		targetPercentOnPath += (z * moveSpeed) / path.EstimatedLength;
		targetPercentOnPath = Mathf.Clamp (targetPercentOnPath, 0, 1);
		if (z != 0) {
			_dirFollowStrength += 0.03f;
			_dirFollowStrength = Mathf.Clamp (_dirFollowStrength, 0, 1);
		}
	}
	
	public override void OnIndexInput (uint index)
	{
		
	}
}
