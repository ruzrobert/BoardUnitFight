using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIView
{
	[Header("Main Menu")]
	[SerializeField] private Text winText;

	[Space]
	[SerializeField] private Button startFightButton;
	[SerializeField] private Button restartButton;

	public override void Setup(GameUI gameUI)
	{
		base.Setup(gameUI);

		EventManager.Instance.OnGameEnded.AddListener(OnGameEnded);
	}

	private void OnGameEnded(UnitTeam winnerTeam)
	{
		if (winnerTeam == null)
		{
			winText.text = "DRAW!";
		}
		else
		{
			winText.text = $"{winnerTeam.TeamName.ToUpper()} TEAM WON!";
		}
	}

	protected override void OnGameStateChanged(GameState gameState)
	{
		if (gameState == GameState.MainMenu || gameState == GameState.LevelComplete)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	protected override void OnShowing()
	{
		bool isLevelComplete = GameManager.Instance.CurrentGameState == GameState.LevelComplete;

		startFightButton.gameObject.SetActive(isLevelComplete == false);
		restartButton.gameObject.SetActive(isLevelComplete);

		if (isLevelComplete == false)
		{
			winText.enabled = false;
		}
		else
		{
			winText.enabled = true;
		}
	}

	public void StartFightButton()
	{
		GameManager.Instance.StartFight();
	}
}