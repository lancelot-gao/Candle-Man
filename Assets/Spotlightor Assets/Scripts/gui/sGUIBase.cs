using UnityEngine;
using System.Collections;

public abstract class sGUIBase : MonoBehaviour
{
    #region Fields
    public GUISkin guiSkin;
    public string styleName;
    public Rect pixelInset = new Rect(0, 0, 100, 30);

    #endregion

    #region Properties
	public GUIStyle guiStyle {
		get { return guiSkin != null ? guiSkin.GetStyle(styleName) : null; }
	}
    #endregion

    #region Functions

    void OnGUI()
    {
        GUIStyle guiStyle = null;
        if (guiSkin != null && styleName.Length > 0) guiStyle = guiSkin.GetStyle(styleName);

        if (guiSkin != null) GUI.skin = guiSkin;

        Vector3 localPos = transform.position;
        GUI.depth = Mathf.FloorToInt(localPos.z); // depth by z
        Rect drawRect = new Rect(localPos.x * Screen.width + pixelInset.x, (1 - localPos.y) * Screen.height - pixelInset.y, pixelInset.width, pixelInset.height); // draw rect by x,y and inset

        DrawGUI(drawRect, guiStyle);
    }
	
    protected abstract void DrawGUI(Rect drawRect, GUIStyle guiStyle);
    #endregion
}
