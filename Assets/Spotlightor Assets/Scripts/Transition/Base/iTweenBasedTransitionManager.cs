using UnityEngine;
using System.Collections;
/// <summary>
/// For ease of creating iTween-based TransitionManager
/// </summary>
public abstract class iTweenBasedTransitionManager : TransitionManager
{
    #region Fields
    public iTween.EaseType easeIn = iTween.EaseType.easeOutExpo;
    public iTween.EaseType easeOut = iTween.EaseType.easeOutExpo;
	public bool ignoreTimeScale = false;
    #endregion

    #region Properties

    #endregion

    #region Functions
    #endregion
}
