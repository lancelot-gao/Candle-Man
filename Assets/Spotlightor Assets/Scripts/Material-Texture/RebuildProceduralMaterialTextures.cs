using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class RebuildProceduralMaterialTextures : MonoBehaviour
{
	public bool asynchronous = true;
	// Use this for initialization
	void Start ()
	{
		#if !UNITY_IPHONE && !UNITY_FLASH
		Material[] sharedMaterials = renderer.sharedMaterials;
		foreach (Material m in sharedMaterials) {
			if (m is ProceduralMaterial) {
				if (asynchronous)
					(m as ProceduralMaterial).RebuildTextures ();
				else
					(m as ProceduralMaterial).RebuildTexturesImmediately ();
			}
		}
		#endif
		#if UNITY_IPHONE || !UNITY_FLASH
		Debug.LogWarning("ProceduralMaterial is not supported by iphone(maybe it's because the iphone-basic version)");
		#endif
		Destroy (this);
	}
}
