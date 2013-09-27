using UnityEngine;
using System.Collections;

public abstract class TransitionManager : MonoBehaviour, ITransition
{
	public enum StateTypes
	{
		Unkown,
		In,
		TransitionIn,
		Out,
		TransitionOut
	}

    #region Constants
	
	public delegate void TransitionManagerGenericEventHandler (TransitionManager source);

	public event TransitionManagerGenericEventHandler TransitionInStarted;
	public event TransitionManagerGenericEventHandler TransitionInCompleted;
	public event TransitionManagerGenericEventHandler TransitionOutStarted;
	public event TransitionManagerGenericEventHandler TransitionOutCompleted;

	public bool outWhenAwake = false;
	public bool autoActivate = true;
	public float delayIn = 0;
	public float durationIn = 1.5f;
	public float delayOut = 0;
	public float durationOut = 1.5f;
	
    #endregion

    #region Fields
	protected StateTypes state = StateTypes.Unkown;
    #endregion

    #region Properties

	public bool IsInTransition {
		get { return state == StateTypes.TransitionIn || state == StateTypes.TransitionOut; }
	}
	
	public bool IsOutOrUnkown {
		get { return state == StateTypes.Out || state == StateTypes.Unkown;}
	}

	public StateTypes State { 
		get { return state; } 
		protected set { 
			state = value;
		}
	}
	
	public float TotalTransitionInTime { get { return delayIn + durationIn; } }

	public float TotalTransitionOutTime { get { return delayOut + durationOut; } }
	
    #endregion

    #region Functions

	protected abstract void DoTransitionIn (bool instant);
	
	protected abstract void DoTransitionOut (bool instant);

	/// <summary>
	/// CALL ME!!!!!
	/// </summary>
	protected void TransitionInComplete ()
	{
		state = StateTypes.In;
		if (TransitionInCompleted != null)
			TransitionInCompleted (this);
	}

	/// <summary>
	/// CALL ME!!!!!
	/// </summary>
	protected void TransitionOutComplete ()
	{
		state = StateTypes.Out;
        
		if (autoActivate) {
			gameObject.SetActive (false);
		}
		
		if (TransitionOutCompleted != null)
			TransitionOutCompleted (this);
	}
	
	protected virtual void Awake ()
	{
		if (outWhenAwake)
			TransitionOut (true);
	}
	
	public virtual void OnEnable ()
	{
		if (state == StateTypes.Out && autoActivate) {
			gameObject.SetActive (false);
		}
	}
    #endregion


    #region ITransition 成员

	public void TransitionIn ()
	{
		TransitionIn (false);
	}

	public void TransitionIn (bool instant)
	{
		switch (state) {
		case StateTypes.In:
			{
				TransitionInComplete ();
				break;
			}
		case StateTypes.TransitionIn:
			break;
		case StateTypes.Unkown:
		case StateTypes.Out:
		case StateTypes.TransitionOut:
			{
				state = StateTypes.TransitionIn;

				if (autoActivate) {
					gameObject.SetActive (true);
				}
				DoTransitionIn (instant);
				
				if (TransitionInStarted != null)
					TransitionInStarted (this);
				break;
			}
		}
	}

	public void TransitionOut ()
	{
		TransitionOut (false);
	}

	public void TransitionOut (bool instant)
	{
		switch (state) {
		case StateTypes.Out:
			{
				TransitionOutComplete ();
				break;
			}
		case StateTypes.TransitionOut:
			break;
		case StateTypes.Unkown:
		case StateTypes.In:
		case StateTypes.TransitionIn:
			{
				state = StateTypes.TransitionOut;
				
				DoTransitionOut (instant);
			
				if (TransitionOutStarted != null)
					TransitionOutStarted (this);
				break;
			}
		}
	}

    #endregion
}
