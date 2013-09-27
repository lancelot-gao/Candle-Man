using UnityEngine;
using System.Collections;

public abstract class FsmExampleState : MonoBehaviour, IFsmState<FsmExample, FsmExample.StateTypes>
{
	public Color tintColor = Color.green;
	private FsmExample owner;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	public abstract void Jump ();
	
	#region IFsmState[FsmExample,FsmExample.StateTypes] implementation
	public FsmExample Owner {
		get {
			return owner;
		}
		set {
			owner = value;
		}
	}

	public abstract FsmExample.StateTypes StateId {
		get ;
	}

	public virtual void BeginState (FsmExample.StateTypes previousState)
	{
		Owner.renderer.material.color = tintColor;
		enabled = true;
	}

	public virtual void EndState (FsmExample.StateTypes newState)
	{
		enabled = false;
	}
	#endregion
	
	void OnGUI ()
	{
		Vector2 screenPos = Camera.main.WorldToScreenPoint (Owner.transform.position);
		GUI.Box (new Rect (screenPos.x - 100, screenPos.y - 50, 200, 100), string.Format ("State: {0}\nPress space bar.", StateId.ToString ()));
	}
}
