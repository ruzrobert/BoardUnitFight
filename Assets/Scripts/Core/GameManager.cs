using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Space]
	[SerializeField] private GameStateManager stateManager;

	public GameState CurrentGameState => stateManager.CurrentGameState;

	private void Awake()
	{
		EventManager.Instance.GameState.OnStartFightRequest.AddListener(OnStartFightRequest);
		EventManager.Instance.GameState.OnGameEnded.AddListener(OnGameEnded);
	}

	private void Start()
	{
		Application.targetFrameRate = Screen.currentResolution.refreshRate;
		
		StartGame();
	}

	private void StartGame()
	{
		LoadLevel();

		stateManager.ChangeStateToMainMenu();
	}

	private void LoadLevel()
	{
		EventManager.Instance.Loading.LoadLevel.Invoke();
	}

	private void OnStartFightRequest()
	{
		if (stateManager.CurrentGameState == GameState.MainMenu || stateManager.CurrentGameState == GameState.LevelComplete)
		{
			stateManager.ChangeStateToGameplay();
		}
	}

	private void OnGameEnded(UnitTeam winnerTeam)
	{
		stateManager.ChangeStateToLevelComplete();
	}
}