using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class ScreenShot : ScriptableObject
{
	private const string ScreenShotsFolderPath = "Assets/ScreenShots/";

	[MenuItem("Custom/ScreenShot", false, 100)]
	public static void SaveScreenShotSize1 ()
	{
		SaveScreenShot (1);
	}
	
	[MenuItem("Custom/ScreenShot x2", false, 101)]
	public static void SaveScreenShotSize2 ()
	{
		SaveScreenShot (2);
	}
	
	[MenuItem("Custom/ScreenShot x3", false, 102)]
	public static void SaveScreenShotSize3 ()
	{
		SaveScreenShot (3);
	}
	
	private static void SaveScreenShot (int superSize)
	{
		string filePath = GenerateScreenShotFilePath ();
		SaveAssetUtility.CreateFolderForAssetIfNeeded (filePath);
		Application.CaptureScreenshot (filePath, superSize);
		AssetDatabase.Refresh ();
	}

	private static string GenerateScreenShotFilePath ()
	{
		DateTime now = DateTime.Now;
		string fileName = string.Format ("ScreenShot_{0}-{1:00}-{2:00}_{3:00}-{4:00}-{5:00}.png",
			now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
		
		return ScreenShotsFolderPath + fileName;
	}
}
