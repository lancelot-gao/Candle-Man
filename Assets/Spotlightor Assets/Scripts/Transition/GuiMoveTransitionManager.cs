using UnityEngine;
using System.Collections;

public class GuiMoveTransitionManager : MoveTransitionManager
{
	public Vector2 posOutPixelOffset;
	
	public override Vector3 PositionOut {
		get {
			Vector3 posOutWithPixelOffset = base.PositionOut;
			posOutWithPixelOffset.x = posOut.x + (float)posOutPixelOffset.x / Screen.width;
			posOutWithPixelOffset.y = posOut.y + (float)posOutPixelOffset.y / Screen.height;
			return posOutWithPixelOffset;
		}
	}
}
