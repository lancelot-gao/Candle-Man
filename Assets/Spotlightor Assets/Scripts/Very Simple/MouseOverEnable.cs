using UnityEngine;
using System.Collections;

/// <summary>
/// 鼠标Enter的时候令target enable，Exit的时候令target disable
/// </summary>
public class MouseOverEnable : MonoBehaviour
{
    #region Fields
    public MonoBehaviour target;
    public bool disableTargetWhenDisable = true;
    #endregion

    #region Properties

    #endregion

    #region Functions
    void OnDisable()
    {
        if (disableTargetWhenDisable && target != null) target.enabled = false;
    }

    void OnMouseEnter()
    {
        if (!enabled) return;
        target.enabled = true;
    }

    void OnMouseExit()
    {
        if (!enabled) return;
        target.enabled = false;
    }
    #endregion
}
