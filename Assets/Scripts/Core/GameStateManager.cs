using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	public GameState CurrentGameState { get; private set; } = GameState.None;

	public void ChangeStateToMainMenu() => SetGameState(GameState.MainMenu);
	public void ChangeStateToGameplay() => SetGameState(GameState.Gameplay);
	public void ChangeStateToLevelComplete() => SetGameState(GameState.LevelComplete);

	private void SetGameState(GameState gameState)
	{
		if (CurrentGameState == gameState)
		{
			return;
		}

		CurrentGameState = gameState;
		EventManager.Instance.OnGameStateChanged.Invoke(gameState);

		switch (gameState)
		{
			case GameState.MainMenu:
				break;
			case GameState.Gameplay:
				EventManager.Instance.OnGameplayStarting.Invoke();
				break;
			case GameState.LevelComplete:
				break;
		}
	}
}