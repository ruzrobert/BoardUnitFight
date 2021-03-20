using UnityEngine;

public class GameplayView : UIView
{
	[Space]
	[SerializeField] private UIHealthBars healthBars;

	public UIHealthBars HealthBars => healthBars;

	protected override void OnGameStateChanged(GameState gameState)
	{
		if (gameState == GameState.Gameplay)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}
}