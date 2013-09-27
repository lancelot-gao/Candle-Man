using UnityEngine;
using System.Collections;

public class FLTransitionEvent : FLEventBase
{
    public const string IN_START = "inStart";
    public const string IN_COMPLETE = "inComplete";
    public const string OUT_START = "outStart";
    public const string OUT_COMPLETE = "outComplete";

    public FLTransitionEvent(string type):base(type){}
}
