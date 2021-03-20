using System.Linq;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	public static GameUI Instance { get; private set; }

	[Space]
	[SerializeField] private Canvas canvas;

	public Canvas Canvas => canvas;

	public MainMenuView MainMenu { get; private set; }
	public GameplayView Gameplay { get; private set; }

	private UIView[] views = new UIView[0];

	private void Awake()
	{
		Instance = this;

		views = GetComponentsInChildren<UIView>(includeInactive: true);

		for (int i = 0; i < views.Length; i++)
		{
			UIView view = views[i];

			view.Setup(this);
			view.gameObject.SetActive(false);
		}

		MainMenu = FindView<MainMenuView>();
		Gameplay = FindView<GameplayView>();
	}

	private T FindView<T>() where T : UIView
	{
		return (T)views.FirstOrDefault(x => x is T);
	}
}