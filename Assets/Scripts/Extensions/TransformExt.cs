using UnityEngine;

public static class TransformExt
{
	public static void DestroyChildren(this Transform transform, bool immediate = false)
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			GameObject child = transform.GetChild(i).gameObject;

			if (immediate)
			{
				Object.DestroyImmediate(child);
			}
			else
			{
				Object.Destroy(child);
			}
		}
	}
}