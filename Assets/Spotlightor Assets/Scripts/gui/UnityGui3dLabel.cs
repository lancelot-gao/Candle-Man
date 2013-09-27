using UnityEngine;
using System.Collections;

public class UnityGui3dLabel : SmartUnityGuiLabel
{
    void Update()
	{
		UpdateDrawPositionInWorld(transform.position);
	}
}
