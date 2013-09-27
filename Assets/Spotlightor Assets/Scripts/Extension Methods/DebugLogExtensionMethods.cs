using UnityEngine;
using System.Collections;

public static class DebugLogExtensionMethods
{
	public static void Log (this Object obj, string format, params object[] args)
	{
		Debug.Log (FormatLogMessage (obj, format, args));
	}
	
	public static void LogWarning (this Object obj, string format, params object[] args)
	{
		Debug.LogWarning (FormatLogMessage (obj, format, args));
	}
	
	public static void LogError (this Object obj, string format, params object[] args)
	{
		Debug.LogError (FormatLogMessage (obj, format, args));
	}
	
	private static string FormatLogMessage (Object obj, string format, object[] args)
	{
		return string.Format ("{0}[{1}]: ", obj.name, obj.GetType().Name) + string.Format (format, args);
	}
}
