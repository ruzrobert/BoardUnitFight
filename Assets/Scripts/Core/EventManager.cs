using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script should have smallest order value in Script Execution Order
/// </summary>
public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	public class LoadingEvents
	{
		public UnityEvent LoadLevel { get; } = new UnityEvent();
	}

	public class GameStateEvents
	{
		public UnityEvent OnStartFightRequest { get; } = new UnityEvent();
		public UnityEvent OnGameplayStarting { get; } = new UnityEvent();

		public UnityEventGameState OnGameStateChanged { get; } = new UnityEventGameState();
		public UnityEventUnitTeam OnGameEnded { get; } = new UnityEventUnitTeam();
	}

	public LoadingEvents Loading { get; } = new LoadingEvents();
	public GameStateEvents GameState { get; } = new GameStateEvents();

	private void Awake()
	{
		Instance = this;
	}

	[Serializable]
	public class UnityEventUnitTeam : UnityEvent<UnitTeam> { }

	[Serializable]
	public class UnityEventGameState : UnityEvent<GameState> { }
}