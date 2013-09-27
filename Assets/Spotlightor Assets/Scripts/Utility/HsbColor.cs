using UnityEngine;
using System.Collections;

[System.Serializable]
public struct HsbColor
{
	public float h;
	public float s;
	public float b;
	public float a;
	
	public HsbColor (float h, float s, float b)
	{
		this.h = h;
		this.s = s;
		this.b = b;
		this.a = 1;
	}
	
	public HsbColor (float h, float s, float b, float a)
	{
		this.h = h;
		this.s = s;
		this.b = b;
		this.a = a;
	}
	
	public HsbColor (Color color)
	{
		float colorR = color.r;
		float colorG = color.g;
		float colorB = color.b;

		float maxRGB = Mathf.Max (colorR, Mathf.Max (colorG, colorB));
		float minRGB = Mathf.Min (colorR, Mathf.Min (colorG, colorB));
		float rgbMaxMinDif = maxRGB - minRGB;
		
		float hueInDegrees = 0;
		
		if (maxRGB > minRGB) {
			if (colorG == maxRGB) 
				hueInDegrees = (colorB - colorR) / rgbMaxMinDif * 60f + 120f;
			else if (colorB == maxRGB) 
				hueInDegrees = (colorR - colorG) / rgbMaxMinDif * 60f + 240f;
			else if (colorB > colorG) 
				hueInDegrees = (colorG - colorB) / rgbMaxMinDif * 60f + 360f;
			else 
				hueInDegrees = (colorG - colorB) / rgbMaxMinDif * 60f;
				
			if (hueInDegrees < 0) 
				hueInDegrees += 360f;
		} else {
			hueInDegrees = 0;
		}

		this.h = hueInDegrees / 360f;
		this.s = maxRGB == 0 ? 0 : rgbMaxMinDif / maxRGB;
		this.b = maxRGB;
		this.a = color.a;
	}
	
	public static implicit operator Color (HsbColor hsbColor)
	{
		float hueInDegrees = hsbColor.h * 360f;

		int i = (int)((int)(hueInDegrees / 60.0f) % 6);
		float f = hueInDegrees / 60.0f - (int)(hueInDegrees / 60.0f);
		float p = hsbColor.b * (1 - hsbColor.s);
		float q = hsbColor.b * (1 - hsbColor.s * f);
		float t = hsbColor.b * (1 - (1 - f) * hsbColor.s);
		switch (i) {   
		case 0:
			return new Color (hsbColor.b, t, p, 1);
		case 1:
			return new Color (q, hsbColor.b, p, 1);
		case 2:
			return new Color (p, hsbColor.b, t, 1);
		case 3:
			return new Color (p, q, hsbColor.b, 1);
		case 4:
			return new Color (t, p, hsbColor.b, 1);
		case 5:
			return new Color (hsbColor.b, p, q, 1);
		}
		return Color.black;
	}
	
	public static implicit operator HsbColor (Color rgbColor)
	{
		return new HsbColor (rgbColor);
	}
	
	public override string ToString ()
	{
		return string.Format ("[HsbColor{H:{0} S:{1} B:{2} A:{3}]", h, s, b, a);
	}
	
	public static Color LerpColorByHsb (Color colorFrom, Color colorTo, float t)
	{
		return Lerp (colorFrom, colorTo, t);
	}
	
	public static HsbColor Lerp (HsbColor colorFrom, HsbColor colorTo, float t)
	{
		float h, s;

		// Check special case black (color.colorTo==0): interpolate neither hue nor saturation.
		if (colorFrom.b == 0) {
			h = colorTo.h;
			s = colorTo.s;
		} else if (colorTo.b == 0) {
			h = colorFrom.h;
			s = colorFrom.s;
		} else {
			// Check special case grey (color.s==0): don't interpolate hue.
			if (colorFrom.s == 0) {
				h = colorTo.h;
			} else if (colorTo.s == 0) {
				h = colorFrom.h;
			} else {
				float angle = Mathf.LerpAngle (colorFrom.h * 360f, colorTo.h * 360f, t);
				angle %= 360;
				if (angle < 0)
					angle += 360;
				h = angle / 360f;
			}
			s = Mathf.Lerp (colorFrom.s, colorTo.s, t);
		}
		return new HsbColor (h, s, Mathf.Lerp (colorFrom.b, colorTo.b, t), Mathf.Lerp (colorFrom.a, colorTo.a, t));
	}
}
