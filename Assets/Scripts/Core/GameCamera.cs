using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public static GameCamera Instance { get; private set; }

    [Space]
    [SerializeField] private new Camera camera;

    public Camera Camera => camera;

	private Vector2 screenSize = Vector2.zero;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		Vector2 newScreenSize = new Vector2(Screen.width, Screen.height);

		if (screenSize != newScreenSize)
		{
			screenSize = newScreenSize;

			OnScreenSizeChanged();
		}
	}

	private void OnScreenSizeChanged()
	{
		AdjustCameraSizeToBoardSize();
	}

	public void AdjustCameraSizeToBoardSize()
	{
		BoardCell[,] boardCells = GameBoard.Instance.Generator.BoardCells;

		if (boardCells == null || boardCells.Length == 0)
		{
			return;
		}

		float minX = boardCells[0, 0].transform.position.x;
		float maxX = boardCells[boardCells.GetLength(0) - 1, 0].transform.position.x;

		float boardWidth = Mathf.Abs(maxX - minX) + 1.5f;

		camera.orthographicSize = boardWidth * Screen.height / Screen.width * 0.5f;
	}
}