using UnityEngine;
using System.Collections;

/// <summary>
/// Auto transition camera when enabled.
/// 
/// HOW TO CUSTOMIZE
///     If you want to do sth before auto camera transition: override BeforeTransition
///     If you want to do sth after auto camera transition: override AfterTransition
/// </summary>
public abstract class CameraControllerBase : GenericInputReciever
{
	#region Fields
	public const string ITWEEN_NAME_FORMAT = "{0} CamTransition Move";
	public Camera target;
	public bool autoTransition = true;
	public iTween.EaseType transitionEase = iTween.EaseType.easeInOutQuad;
	public float transitionSpeed = 3;
	public float maxTransitionDuration = 3;
	public float minTransitionDuration = 0.5f;
	private bool inTransition = false;
	#endregion

	#region Properties
	public bool IsInTransition {
		get { return inTransition; }
	}
	#endregion

	#region Functions
	
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if (forTheFirstTime) {
			if (target == null) {
				Debug.Log ("No camera target found, the main camera will be used.");
				target = Camera.main;
			}
		}
		BeforeTransition ();
		if (autoTransition) {
			target.transform.parent = transform;
			float transitionDuration = Vector3.Distance (transform.position, target.transform.position) / transitionSpeed;
			transitionDuration = Mathf.Clamp (transitionDuration, minTransitionDuration, maxTransitionDuration);
			
			string tweenName = string.Format (ITWEEN_NAME_FORMAT, gameObject.name);
			iTween.StopByName (target.gameObject, tweenName);
			iTween.MoveTo (target.gameObject, iTween.Hash ("name", tweenName, "time", transitionDuration, "easetype", transitionEase, "position", Vector3.zero, "islocal", true,
			"oncomplete", "OnCameraTransitionComplete", "oncompletetarget", gameObject));
			iTween.RotateTo (target.gameObject, iTween.Hash ("name", tweenName, "time", transitionDuration, "easetype", transitionEase, "rotation", Vector3.zero, "islocal", true,
			"overwrite", false));
			inTransition = true;
		} else
			OnCameraTransitionComplete ();
	}

	private void OnCameraTransitionComplete ()
	{
		inTransition = false;
		AfterTransition ();
		DispatchEvent (new FLTransitionEvent (FLTransitionEvent.IN_COMPLETE));// added 02.28
	}

	/// <summary>
	/// Called before transition
	/// </summary>
	public abstract void BeforeTransition ();

	/// <summary>
	/// Caled after transition
	/// </summary>
	public abstract void AfterTransition ();
	
	protected override void OnBecameUnFunctional ()
	{
		inTransition = false;
		iTween.StopByName (target.gameObject, string.Format (ITWEEN_NAME_FORMAT, gameObject.name));
		
		if (target.transform.parent == transform) {
			Debug.Log ("Camera seemed to be deactivated together with me, I'll activate it.");
			target.transform.parent = null;
			if (target.gameObject.activeSelf == false)
				target.gameObject.SetActive (true);
		}
	}
	#endregion

	void Reset ()
	{
		target = Camera.main;
	}
}
