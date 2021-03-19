public static class MathExt
{
	public static bool IsBetween(float value, float min, float max)
	{
		return min <= max ? value >= min && value <= max : value >= max && value <= min;
	}
}