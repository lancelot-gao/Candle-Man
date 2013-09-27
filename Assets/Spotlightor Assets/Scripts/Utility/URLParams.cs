using UnityEngine;
using System.Collections;

public static class URLParams {
	
	/// <summary>
	/// Get a param value from url with paramName
	/// </summary>
	/// <param name="paramName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="url">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.String"/>
	/// </returns>
	public static string GetURLParam(string paramName, string url)
	{
		if(url == null || url == "")return "";
		if(paramName == null || paramName == "")return "";
		
		int paramStartIndex = url.LastIndexOf(paramName+"=");
		if(paramStartIndex != -1)
		{
			int paramEndIndex = url.IndexOf ("&", paramStartIndex);
			if (paramEndIndex != -1)
				return url.Substring (paramStartIndex + paramName.Length+1, paramEndIndex - 1 - paramStartIndex - paramName.Length);
			else
				return url.Substring (paramStartIndex + paramName.Length+1);
		}
		else return "";
	}
}
