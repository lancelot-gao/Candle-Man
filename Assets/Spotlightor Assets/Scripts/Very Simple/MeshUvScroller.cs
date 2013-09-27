using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshUvScroller : MonoBehaviour
{
	public Vector2 speed;
	public bool ignoreTimeScale = false;
	public bool scrollSharedMesh = false;
	private Vector2[] defaultUvs;
	private Vector2[] uvs;
	private Vector2 uvTotalOffset;
	private Mesh targetMesh;
	
	// Use this for initialization
	void Start ()
	{
		if (scrollSharedMesh) {
			targetMesh = GetComponent<MeshFilter> ().sharedMesh;
			defaultUvs = targetMesh.uv;
		} else
			targetMesh = GetComponent<MeshFilter> ().mesh;
		uvs = targetMesh.uv;
		uvTotalOffset = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ScrollMeshUv (targetMesh);
	}
	
	protected virtual void ScrollMeshUv (Mesh mesh)
	{
		float deltaTime = ignoreTimeScale ? Time.deltaTime / Time.timeScale : Time.deltaTime;
		Vector2 deltaUv = deltaTime * speed;
		uvTotalOffset += deltaUv;
		if (Mathf.Abs (uvTotalOffset.x) > 1) {
			float loopX = Mathf.Floor (uvTotalOffset.x);
			deltaUv.x -= loopX;
			uvTotalOffset.x -= loopX;
		}
		if (Mathf.Abs (uvTotalOffset.y) > 1) {
			float loopY = Mathf.Floor (uvTotalOffset.y);
			deltaUv.y -= loopY;
			uvTotalOffset.y -= loopY;
		}
		
		for (int i = 0; i < uvs.Length; i++) 
			uvs [i] += deltaUv;
		
		targetMesh.uv = uvs;
	}
	
	protected virtual void OnApplicationQuit ()
	{
		if (scrollSharedMesh)
			targetMesh.uv = defaultUvs;
	}
}
