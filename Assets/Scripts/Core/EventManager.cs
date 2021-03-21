using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script should have smallest order value in Script Execution Order
/// </summary>
public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	public UnityEvent OnStartFightRequest { get; private set; } = new UnityEvent();
	public UnityEvent OnGameplayStarting { get; private set; } = new UnityEvent();
	public UnityEventGameState OnGameStateChanged { get; private set; } = new UnityEventGameState();
	public UnityEventUnitTeam OnGameEnded { get; private set; } = new UnityEventUnitTeam();

	private void Awake()
	{
		Instance = this;
	}

	[Serializable]
	public class UnityEventUnitTeam : UnityEvent<UnitTeam> { }

	[Serializable]
	public class UnityEventGameState : UnityEvent<GameState> { }
}