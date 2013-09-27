using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class TintMeshVertexColor : FunctionalMonoBehaviour
{
	public Color color = Color.white;

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		Tint ();
	}
	
	protected override void OnBecameUnFunctional ()
	{
		
	}
	
	public void Tint (Color color)
	{
		this.color = color;
		Tint ();
	}
	
	public void Tint ()
	{
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		MeshUtility.Tint (mesh, color);
	}
}
