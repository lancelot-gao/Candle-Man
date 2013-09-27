using UnityEngine;
using System.Collections;

public static class AppMarketLinks
{
	public static string RateIt (string id)
	{
		string linkFormat = "itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id={0}";
		return string.Format (linkFormat, id);
	}
}
