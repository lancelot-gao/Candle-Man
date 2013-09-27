using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class sGUIUnityButton : sGUIBase
{
    #region Fields
	public string label;
    #endregion

    #region Properties

    #endregion

    #region Functions

	protected override void  DrawGUI (Rect drawRect, GUIStyle guiStyle)
	{
		bool click = false;

		if (guiStyle != null)
			click = GUI.Button (drawRect, label, guiStyle);
		else
			click = GUI.Button (drawRect, label);

		if (click)
			SendMessageUpwards ("OnClickButton", this, SendMessageOptions.DontRequireReceiver);
	}
    #endregion
}
