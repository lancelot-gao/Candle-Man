using UnityEngine;
using System.Collections;

public class FLMouseEvent : FLEventBase
{
	public const string CLICK = "click";
	public const string ROLL_OVER = "rollOver";
	public const string ROLL_OUT = "rollOut";
	public const string UP = "up";
    public const string DOWN = "down";
    public const string HOLD_DOWN = "holdDown";
	
	public FLMouseEvent(string type):base(type){}
}

