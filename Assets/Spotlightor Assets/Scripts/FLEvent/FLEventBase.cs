using UnityEngine;
using System.Collections;
using System;

public class FLEventBase
{
	public delegate void FLEventHandler (FLEventBase e);

	private string _type;

	public string Type { get { return _type; } }

	public IFLEventDispatcher target;

	public FLEventBase (string type)
	{
		_type = type;
	}

	public FLEventBase (Enum type)
	{
		_type = EnumHelper.EnumToString (type);
	}
	
	public virtual FLEventBase Clone ()
	{
		return new FLEventBase (_type);
	}
}
