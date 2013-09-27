using UnityEngine;
using System.Collections;

public class DigitalDisplayer : BaseNumberDisplayer
{
	public int digitsCount = 3;
	private string stringFormat = "";
	
	#region implemented abstract members of BaseNumberDisplayer
	protected override string FormatNumberValueToString (float numberValue)
	{
		if (stringFormat == "") {
			if (digitsCount > 0) {
				stringFormat = "{0:";
				for (int i = 0; i < digitsCount; i++)
					stringFormat += "0";
				stringFormat += "}";
			} else
				stringFormat = "{0}";
		}
		return string.Format (stringFormat, (int)numberValue);
	}
	#endregion
}
