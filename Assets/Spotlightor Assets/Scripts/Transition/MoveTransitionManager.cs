using UnityEngine;
using System.Collections;

public class MoveTransitionManager : iTweenBasedTransitionManager
{
    #region Fields
    public Vector3 posIn = Vector3.one;
    public Vector3 posOut = Vector3.zero;

    public bool isLocal = true;
    #endregion

    #region Properties
	public virtual Vector3 PositionOut{
		get {
			return posOut;
		}
	}
    #endregion

    #region Functions
    protected override void DoTransitionIn(bool instant)
    {
		iTween.Stop(gameObject, "MoveTo");
        if (instant)
        {
            if (isLocal) transform.localPosition = posIn;
            else transform.position = posIn;
            TransitionInComplete();
        }
        else iTween.MoveTo(gameObject, iTween.Hash("position", posIn, "ignoretimescale", ignoreTimeScale, "delay", delayIn, "time", durationIn, "easetype", easeIn, "islocal", isLocal, "oncomplete", "TransitionInComplete"));
    }

    protected override void DoTransitionOut(bool instant)
    {
		iTween.Stop(gameObject, "MoveTo");
        if (instant)
        {
            if (isLocal) transform.localPosition = PositionOut;
            else transform.position = PositionOut;
            TransitionOutComplete();
        }
        else iTween.MoveTo(gameObject, iTween.Hash("position", PositionOut, "ignoretimescale", ignoreTimeScale, "delay", delayOut, "time", durationOut, "easetype", easeOut, "islocal", isLocal, "oncomplete", "TransitionOutComplete"));
    }

    /// <summary>
    /// For good default value
    /// </summary>
    void Reset()
    {
        posIn = posOut = transform.localPosition;
    }
	
	void OnDisable()
	{
		iTween.Stop(gameObject, "MoveTo");
	}
    #endregion
}
