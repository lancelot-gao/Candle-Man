using UnityEngine;
using System.Collections;
using System;

public class FLEvent : FLEventBase
{
	public const string START = "start";
	public const string PAUSE = "pause";
	public const string STOP = "stop";
	public const string COMPLETE = "complete";
	public const string CHANGE = "change";
	public const string OPEN = "open";
	public const string CLOSE = "close";
	public const string ACTIVATE = "activate";
	public const string DEACTIVATE = "deactivate";
	
	public FLEvent (string type):base(type)
	{
	}

	public FLEvent (Enum type):base(type)
	{
	}
}
