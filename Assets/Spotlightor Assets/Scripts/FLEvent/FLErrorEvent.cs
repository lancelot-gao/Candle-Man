using UnityEngine;
using System.Collections;

public class FLErrorEvent : FLEventBase
{
	public const string IO_ERROR = "ioError";
	private string _error;
	public string error {
		get { return _error; }
	}
	public FLErrorEvent (string type, string error) : base(type)
	{
		_error = error;
	}
	public override FLEventBase Clone ()
	{
		return new FLErrorEvent(Type, _error);
	}
}

