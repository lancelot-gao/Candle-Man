using UnityEngine;
using System.Collections;
using System;

public static class BasicDataTypeStorage
{
	public static void SetInt (Enum identifier, int newValue)
	{
		SetInt (ConvertEnumToStringIdentifier (identifier), newValue);
	}
	
	public static void SetInt (string identifier, int newValue)
	{
		PlayerPrefs.SetInt (identifier, newValue);
	}
	
	public static int GetInt (Enum identifier)
	{
		return GetInt (ConvertEnumToStringIdentifier (identifier));
	}
	
	public static int GetInt (string identifier)
	{
		return PlayerPrefs.GetInt (identifier, 0);
	}
	
	public static void DeleteInt (Enum identifier)
	{
		DeleteInt (ConvertEnumToStringIdentifier (identifier));
	}
	
	public static void DeleteInt (string identifer)
	{
		PlayerPrefs.DeleteKey (identifer);
	}
	
	public static void SetFloat (Enum identifier, float newValue)
	{
		SetFloat (ConvertEnumToStringIdentifier (identifier), newValue);
	}
	
	public static void SetFloat (string identifier, float newValue)
	{
		PlayerPrefs.SetFloat (identifier, newValue);
	}
	
	public static float GetFloat (Enum identifier)
	{
		return GetFloat (ConvertEnumToStringIdentifier (identifier));
	}
	
	public static float GetFloat (string identifier)
	{
		return PlayerPrefs.GetFloat (identifier, 0);
	}
	
	public static void DeleteFloat (Enum identifier)
	{
		DeleteInt (ConvertEnumToStringIdentifier (identifier));
	}
	
	public static void DeleteFloat (string identifier)
	{
		PlayerPrefs.DeleteKey (identifier);
	}

	public static void SetString (Enum identifier, string newValue)
	{
		SetString (ConvertEnumToStringIdentifier (identifier), newValue);
	}
	
	public static void SetString (string identifier, string newValue)
	{
		PlayerPrefs.SetString (identifier, newValue);
	}
	
	public static string GetString (Enum identifier)
	{
		return GetString (ConvertEnumToStringIdentifier (identifier));
	}
	
	public static string GetString (string identifier)
	{
		return PlayerPrefs.GetString (identifier, "");
	}
	
	public static void DeleteString (Enum identifier)
	{
		DeleteString (ConvertEnumToStringIdentifier (identifier));
	}
	
	public static void DeleteString (string identifier)
	{
		PlayerPrefs.DeleteKey (identifier);
	}

	public static string ConvertEnumToStringIdentifier (Enum identifer)
	{
		return identifer.GetType ().ToString () + "_" + EnumHelper.EnumToString (identifer);
	}
	
	public static void ForceWritingAllDatasToDisk ()
	{
		PlayerPrefs.Save ();
	}
	
	public static void DeleteAll ()
	{
		PlayerPrefs.DeleteAll ();
	}
}
