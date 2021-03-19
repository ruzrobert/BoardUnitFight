using System;
using System.Collections;
using UnityEngine;

public static class CoroutineExt
{
	/// <summary>
	/// Stops coroutine if not null. Sets null on reference after stopping.
	/// </summary>
	public static void Stop(ref Coroutine coroutine, MonoBehaviour behaviour)
	{
		if (coroutine != null)
		{
			behaviour.StopCoroutine(coroutine);
			coroutine = null;
		}
	}

	/// <summary>
	/// Starts the coroutine, alias for coroutine = StartCoroutine(routine).
	/// </summary>
	public static void Start(ref Coroutine coroutine, MonoBehaviour behaviour, IEnumerator routine)
	{
		coroutine = behaviour.StartCoroutine(routine);
	}

	/// <summary>
	/// Stops the existing coroutine (if is running), and starts the new one
	/// </summary>
	public static void Restart(ref Coroutine coroutine, MonoBehaviour behaviour, IEnumerator routine)
	{
		Stop(ref coroutine, behaviour);
		Start(ref coroutine, behaviour, routine);
	}

	/// <summary>
	/// Starts coroutine with delayed action, alias for StartCoroutine(DelayedAction(action))
	/// </summary>
	public static Coroutine StartDelayed(MonoBehaviour behaviour, Action action, float delay, bool realtime = false)
	{
		return behaviour.StartCoroutine(DelayedAction(action, delay, realtime));
	}

	/// <summary>
	/// IEnumerator for delayed actions. Can use realtime time.
	/// </summary>
	public static IEnumerator DelayedAction(Action action, float delay, bool realtime = false)
	{
		if (delay > 0f)
		{
			if (realtime)
			{
				yield return new WaitForSecondsRealtime(delay);
			}
			else
			{
				yield return new WaitForSeconds(delay);
			}
		}

		action();
	}
}