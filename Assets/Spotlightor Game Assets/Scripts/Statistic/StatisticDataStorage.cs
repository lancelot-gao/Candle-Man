using UnityEngine;
using System.Collections;
using System;

public static class StatisticDataStorage
{
	public static void DecreaseValue (Enum identifier, int amount)
	{
		IncreaseValue (identifier, -amount);
	}
	
	public static void IncreaseValue (Enum identifier, int amount)
	{
		if (amount == 0)
			return;
		
		int currentValue = GetValue (identifier);
		SetValue (identifier, currentValue + amount);
	}
	
	public static void UpdateMax (Enum identifier, int newValue)
	{
		int currentValue = GetValue (identifier);
		if (currentValue >= newValue)
			return;
		SetValue (identifier, newValue);
	}

	public static int GetValue (Enum identifier)
	{
		return BasicDataTypeStorage.GetInt (identifier);
	}
	
	public static void SetValue (Enum identifier, int newValue)
	{
		BasicDataTypeStorage.SetInt (identifier, newValue);
	}
	
	public static void DeleteValue (Enum identifier)
	{
		BasicDataTypeStorage.DeleteInt (identifier);
	}
}
