using UnityEngine;
using System.Collections;

public class TimeDisplayer : BaseNumberDisplayer
{
	public string miniutesPostfix = "\'";
	public string secondsPostfix = "\"";
	
	#region implemented abstract members of BaseNumberDisplayer
	protected override string FormatNumberValueToString (float numberValue)
	{
		int minutes = Mathf.FloorToInt (numberValue / 60);
		int seconds = Mathf.FloorToInt (numberValue - minutes * 60);
		return string.Format ("{0:00}{1}{2:00}{3}", minutes, miniutesPostfix, seconds, secondsPostfix);
	}
	#endregion
}
