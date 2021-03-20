using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[Space]
	[SerializeField] private GameStateManager stateManager;

	public GameState CurrentGameState => stateManager.CurrentGameState;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		StartGame();
	}

	private void StartGame()
	{
		stateManager.ChangeStateToMainMenu();
	}

	public void StartFight()
	{
		if (stateManager.CurrentGameState == GameState.MainMenu || stateManager.CurrentGameState == GameState.LevelComplete)
		{
			stateManager.ChangeStateToGameplay();
		}
	}

	public void EndGame(UnitTeam winnerTeam)
	{
		EventManager.Instance.OnGameEnded.Invoke(winnerTeam);
		stateManager.ChangeStateToLevelComplete();
	}
}