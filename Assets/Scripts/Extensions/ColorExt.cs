using UnityEngine;

public static class ColorExt
{
	public static Color SetA(this Color color, float a)
	{
		color.a = a;
		return color;
	}
}