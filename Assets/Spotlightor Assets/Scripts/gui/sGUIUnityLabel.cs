using UnityEngine;
using System.Collections;

/// <summary>
/// 使用类似于GUIText的方法来码放Unity GUI.Label！多么创新性的功能！主要是为了用Label的Word Wrap特性来弥补GUIText的不足
/// </summary>
[ExecuteInEditMode()]
public class sGUIUnityLabel : sGUIBase
{
    #region Fields
    public string text;
    #endregion

    #region Properties

    #endregion

    #region Functions
    protected override void DrawGUI(Rect drawRect, GUIStyle guiStyle)
    {
        if (guiStyle != null)
        {
            GUI.Label(drawRect, text, guiStyle);
        }
        else GUI.Label(drawRect, text);
    }
    #endregion
}
