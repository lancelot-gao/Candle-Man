using UnityEngine;
using System.Collections;

public static class MeshUtility {

	public static void Tint(Mesh mesh, Color color)
	{
		Color32 color32 = color;
		Color32[] colors32 = mesh.colors32;
		if (colors32.Length != mesh.vertexCount)
			colors32 = new Color32[mesh.vertexCount];
		for (int i = 0; i  < colors32.Length; i++)
			colors32 [i] = color32;
		
		mesh.colors32 = colors32;
	}
}
