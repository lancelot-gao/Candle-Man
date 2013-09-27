using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour
{
	public Transform[] pathPoints;
	public bool alwaysDrawGizmos = false;
	private float estimatedLength = -1f;
	
	public float EstimatedLength {
		get {
			if (estimatedLength == -1) {
				estimatedLength = 0;
				if (pathPoints != null && pathPoints.Length > 1) {
					for (int i = 1; i < pathPoints.Length; i++) {
						estimatedLength += Vector3.Distance (pathPoints [i].position, pathPoints [i - 1].position);
					}
				}
			}
			return estimatedLength;
		}
	}
	
	public void PlaceObjectOnPath (Transform target, float pathPercent)
	{
		target.position = GetPositionOnPath (pathPercent);
		target.rotation = GetRotationOnPath (pathPercent);
	}
	
	public Quaternion GetLookRotationOnPath (float pathPercent)
	{
		pathPercent = Mathf.Clamp01 (pathPercent);
		float fromPercent = pathPercent;
		float toPercent = pathPercent + 0.01f;
		if (toPercent > 1) {
			toPercent = 1;
			fromPercent = toPercent - 0.01f;
		}
		return Quaternion.LookRotation (GetPositionOnPath (toPercent) - GetPositionOnPath (fromPercent));
	}
	
	public Vector3 GetPositionOnPath (float pathPercent)
	{
		return iTween.PointOnPath (pathPoints, Mathf.Clamp01 (pathPercent));
	}
	
	public Quaternion GetRotationOnPath (float pathPercent)
	{
		pathPercent = Mathf.Clamp01 (pathPercent);
		
		int numSegments = pathPoints.Length - 1;
		int segmentStartPointIndex = Mathf.Min (Mathf.FloorToInt (pathPercent * (float)numSegments), numSegments - 1);
		Transform segmentStartPoint = pathPoints [segmentStartPointIndex];
		Transform segmentEndPoint = pathPoints [segmentStartPointIndex + 1];
		
		float percentPerSegment = 1f / numSegments;
		float segmentPercent = (pathPercent - percentPerSegment * segmentStartPointIndex) / percentPerSegment;
		Quaternion rotationOnPath = Quaternion.Slerp (segmentStartPoint.rotation, segmentEndPoint.rotation, segmentPercent);
		return rotationOnPath;
	}
	
	// 如果strictlyFollowPath为fasle，则不会就近寻找wp走，而是找到更符合前进方向的最近wp（可能不是最近的）
	public Vector3[] SearchRoute (Vector3 start, Vector3 dest, bool strictlyFollowPath)
	{
		int startWpIndex = FindNearestPathPointIndex (start);
		int destWpIndex = FindNearestPathPointIndex (dest);
		int numWpOffset = Mathf.Abs (destWpIndex - startWpIndex);
		if (numWpOffset == 0) {
			if (!strictlyFollowPath)
				return new Vector3[2] { start, dest };
			else
				return new Vector3[3] { start, pathPoints [startWpIndex].position, dest };
		} else if (numWpOffset == 1) {
			if (!strictlyFollowPath)
				return new Vector3[2] { start, dest };
			else
				return new Vector3[4] { start, pathPoints [startWpIndex].position, pathPoints [destWpIndex].position, dest };
		} else {
			if (!strictlyFollowPath) {
				if (destWpIndex > startWpIndex) {
					Transform nextWp = pathPoints [startWpIndex + 1];
					Transform startWp = pathPoints [startWpIndex];
					float distanceToNextWp = Vector3.Distance (nextWp.position, start);
					float distanceToStartWp = Vector3.Distance (startWp.position, start);
					float distanceFromStartToNextWp = distanceToStartWp + Vector3.Distance (startWp.position, nextWp.position);
					if (distanceFromStartToNextWp > distanceToNextWp)
						startWpIndex++;
					
					Transform secondToLastWp = pathPoints [destWpIndex - 1];
					Transform destWp = pathPoints [destWpIndex];
					float distanceToSecondToLast = Vector3.Distance (secondToLastWp.position, dest);
					float distanceFromDestToSecondToLast = Vector3.Distance (destWp.position, dest) + Vector3.Distance (destWp.position, secondToLastWp.position);
					if (distanceToSecondToLast < distanceFromDestToSecondToLast)
						destWpIndex--;
				} else {
					Transform nextWp = pathPoints [startWpIndex - 1];
					Transform startWp = pathPoints [startWpIndex];
					float distanceToNextWp = Vector3.Distance (nextWp.position, start);
					float distanceFromStartToNextWp = Vector3.Distance (startWp.position, start) + Vector3.Distance (startWp.position, nextWp.position);
					if (distanceFromStartToNextWp > distanceToNextWp)
						startWpIndex--;
					Transform secondToLastWp = pathPoints [destWpIndex + 1];
					Transform destWp = pathPoints [destWpIndex];
					float distanceToSecondToLast = Vector3.Distance (secondToLastWp.position, dest);
					float distanceFromDestToSecondToLast = Vector3.Distance (destWp.position, dest) + Vector3.Distance (destWp.position, secondToLastWp.position);
					if (distanceToSecondToLast < distanceFromDestToSecondToLast)
						destWpIndex++;
				}
			}
			Vector3[] pointsOnPath = new Vector3[2 + Mathf.Abs (destWpIndex - startWpIndex) + 1];
			pointsOnPath [0] = start;
			if (destWpIndex > startWpIndex) {
				for (int i = startWpIndex; i <= destWpIndex; i++)
					pointsOnPath [i - startWpIndex + 1] = pathPoints [i].position;
			} else {
				for (int i = startWpIndex; i >= destWpIndex; i--)
					pointsOnPath [startWpIndex - i + 1] = pathPoints [i].position;
			}
			pointsOnPath [pointsOnPath.Length - 1] = dest;
			return pointsOnPath;
		}
	}

	public int FindNearestPathPointIndex (Vector3 targetPosition)
	{
		float minDistance = float.MaxValue;
		float distance = 0;
		int nearestWaypointIndex = -1;
		for (int i = 0; i < pathPoints.Length; i++) {
			Vector3 offset = targetPosition - pathPoints [i].position;
			distance = offset.sqrMagnitude;
			if (distance < minDistance) {
				nearestWaypointIndex = i;
				minDistance = distance;
			}
		}
		return nearestWaypointIndex;
	}

	public Transform FindNearestPathPoint (Vector3 targetPosition)
	{
		return pathPoints [FindNearestPathPointIndex (targetPosition)];
	}

	void OnDrawGizmosSelected ()
	{
		DrawDebugLine ();
	}
	
	void OnDrawGizmos ()
	{
		if (alwaysDrawGizmos) {
			DrawDebugLine ();
		}
	}
	
	void DrawDebugLine ()
	{
		if (pathPoints == null || pathPoints.Length == 0)
			return;
		int numCurveParts = pathPoints.Length * 10;
		float step = (float)1 / numCurveParts;
		float percent = 0;
		Vector3 lineStart = pathPoints [0].position;
		Vector3 lineEnd = Vector3.zero;
		Gizmos.color = Color.cyan;
		for (int i = 0; i < numCurveParts; i++) {
			percent += step;
			lineEnd = iTween.PointOnPath (pathPoints, percent);
			Gizmos.DrawLine (lineStart, lineEnd);
			lineStart = lineEnd;
		}
	}
	
	public Transform[] FindNearestLineSegment (Vector3 targetPosition)
	{
		Transform[] linePoints = new Transform[2];
		if (pathPoints.Length < 2) {
			this.LogWarning ("Path points must be greater than 2");
			linePoints [0] = transform;
			linePoints [0] = transform;
		} else if (pathPoints.Length == 2) {
			linePoints [0] = pathPoints [0];
			linePoints [1] = pathPoints [1];
		} else {
			float nearestDistance = float.MaxValue - 1;
			int nearestDistanceIndex = -1;
			float nextNearestDistance = float.MaxValue;
			int nextNearestDistanceIndex = -1;
			for (int i = 0; i <pathPoints.Length; i++) {
				Transform pathPoint = pathPoints [i];
				float distance = Vector3.Distance (pathPoint.position, targetPosition);
				if (distance < nearestDistance) {
					if (nearestDistance >= 0) {
						nextNearestDistanceIndex = nearestDistanceIndex;
						nextNearestDistance = nearestDistance;
					}
					nearestDistanceIndex = i;
					nearestDistance = distance;
				} else if (distance < nextNearestDistance) {
					nextNearestDistanceIndex = i;
					nextNearestDistance = distance;
				}
			}
			linePoints [0] = pathPoints [Mathf.Min (nearestDistanceIndex, nextNearestDistanceIndex)];
			linePoints [1] = pathPoints [Mathf.Max (nearestDistanceIndex, nextNearestDistanceIndex)];
		}
		
		return linePoints;
	}
}
