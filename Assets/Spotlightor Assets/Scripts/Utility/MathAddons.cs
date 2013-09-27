using UnityEngine;
using System.Collections;

/// <summary>
/// Addons of math functions
/// </summary>
public static class MathAddons
{
	public const float TWO_PI = Mathf.PI * 2;
	public const float HALF_PI = Mathf.PI * 0.5f;
	
	/// <summary>
	/// Is value in range min to max
	/// </summary>
	/// <param name="value"></param>
	/// <param name="min"></param>
	/// <param name="max"></param>
	/// <returns></returns>
	public static bool IsInRange (float value, float min, float max)
	{
		return (value >= min) && (value <= max);
	}
	/// <summary>
	/// Is value in range min to max
	/// </summary>
	/// <param name="value"></param>
	/// <param name="min"></param>
	/// <param name="max"></param>
	/// <returns></returns>
	public static bool IsInRange (int value, int min, int max)
	{
		return (value >= min) && (value <= max);
	}
	
	/// <summary>
	/// Get the critical damping acceleration.
	/// Read more:http://en.wikipedia.org/wiki/Damping
	/// </summary>
	/// <returns>
	/// The critical damping acceleration.
	/// </returns>
	/// <param name='k'>
	/// Spring constant K.
	/// </param>
	/// <param name='dx'>
	/// Distance to ballenct position.
	/// </param>
	/// <param name='velocity'>
	/// Velocity.
	/// </param>
	/// <param name='m'>
	/// Mass
	/// </param>
	public static float GetCriticalDampingAcceleration (float k, float dx, float velocity, float m)
	{
		float fSpring = -k * dx;
		float c = 2 * Mathf.Sqrt (k);
		float fDamping = - c * velocity;
		float fTotal = fSpring + fDamping;
		return fTotal / m;
	}
	
	// Simplified version without mass(m = 1)
	public static float GetCriticalDampingAcceleration (float k, float dx, float velocity)
	{
		return - 2 * Mathf.Sqrt (k) * velocity - k * dx;
	}
	
	public static void FormatAngleInDegree (ref float angleInDegree)
	{
		angleInDegree %= 360;
		if (angleInDegree > 180)
			angleInDegree -= 360;
		else if (angleInDegree < -180)
			angleInDegree += 360;
	}
	
	public static void FormatAngleInRadian (ref float angleInRadian)
	{
		angleInRadian %= TWO_PI;
		if (angleInRadian > Mathf.PI)
			angleInRadian -= TWO_PI;
		else if (angleInRadian < -Mathf.PI)
			angleInRadian += TWO_PI;
	}
}
