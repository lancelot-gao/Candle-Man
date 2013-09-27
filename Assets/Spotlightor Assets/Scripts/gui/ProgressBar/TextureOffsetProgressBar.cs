using UnityEngine;
using System.Collections;

public class TextureOffsetProgressBar : ProgressBar
{
	public float emptyOffsetX = -0.003f;
	public float fullOffsetX = 1;

	protected override void UpdateProgressDisplay (float progress)
	{
		renderer.material.mainTextureOffset = (new Vector2 (Mathf.Lerp (emptyOffsetX, fullOffsetX, progress), 0));
		if (progress == 0 || progress == 1)
			renderer.enabled = false;
		else
			renderer.enabled = true;
	}
}
