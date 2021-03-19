using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CollectionExt
{
	public static T GetRandomOrDefault<T>(this List<T> list)
	{
		return list.Count > 0 ? list[Random.Range(0, list.Count)] : default;
	}

	public static T GetRandomOrDefault<T>(this T[] array)
	{
		return array.Length > 0 ? array[Random.Range(0, array.Length)] : default;
	}

	public static T GetRandomOrDefault<T>(this IEnumerable<T> collection)
	{
		int count = collection.Count();
		return count > 0 ? collection.ElementAt(Random.Range(0, count)) : default;
	}

	public static bool IsNullOrEmpty<T>(this List<T> list)
	{
		return list == null || list.Count == 0;
	}

	public static bool IsNullOrEmpty<T>(this T[] array)
	{
		return array == null || array.Length == 0;
	}
}