using UnityEngine;
using System.Collections;

public class FLProgressEvent : FLEventBase
{
	public const string PROGRESS = "progress";
	public const string BUFFERING_START = "buffering start";
	public const string BUFFERING_COMPLETE = "buffering complete";
	
	private float _progress;
	
	public float progress { get {return _progress;} }
	
	public FLProgressEvent(string type, float progress) : base(type)
	{
		_progress = progress;
	}
	
	public override FLEventBase Clone ()
	{
		return new FLProgressEvent(Type, _progress);
	}
}

