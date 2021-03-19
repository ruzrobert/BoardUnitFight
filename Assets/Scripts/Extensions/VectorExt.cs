using UnityEngine;

public static class VectorExt
{
	public static Vector3 SetX(this Vector3 vector, float x)
	{
		vector.x = x;
		return vector;
	}

	public static Vector3 SetY(this Vector3 vector, float y)
	{
		vector.y = y;
		return vector;
	}

	public static Vector3 SetZ(this Vector3 vector, float z)
	{
		vector.z = z;
		return vector;
	}
}