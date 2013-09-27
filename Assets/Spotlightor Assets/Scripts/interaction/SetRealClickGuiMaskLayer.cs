using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUILayer))]
public class SetRealClickGuiMaskLayer : MonoBehaviour
{
	void Awake ()
	{
		GUILayer guiLayer = GetComponent<GUILayer> ();
		if (guiLayer != null)
			RealClickListener.HitTestMaskGUILayer = guiLayer;
		Destroy (this);
	}
}
