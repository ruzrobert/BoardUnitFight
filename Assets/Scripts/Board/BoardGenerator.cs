using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
	[Space]
	[SerializeField] private BoardCell cellPrefab;

	[Space]
	[SerializeField] private Vector2Int boardSize = new Vector2Int(8, 6);
	[SerializeField] private float cellSize = 1f;

	[Space]
	[SerializeField] private BoardCell[,] boardCells = new BoardCell[0, 0];

	[Space]
	[SerializeField] private bool generateBoard = false; // inspector stuff

	public Vector2Int BoardSize => boardSize;

	public BoardCell[,] BoardCells => boardCells;

	private void Awake()
	{
		GenerateBoard();
	}

	private void Start()
	{
		GameCamera.Instance.AdjustCameraSizeToBoardSize();
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		if (generateBoard)
		{
			generateBoard = false;

			if (Application.isPlaying == false)
			{
				GenerateBoard();
			}
		}

		boardSize.x = Mathf.Max(boardSize.x, 1);
		boardSize.y = Mathf.Max(boardSize.y, 1);
	}
#endif

	public void GenerateBoard()
	{
		if (Application.isPlaying)
		{
			transform.DestroyChildren();
		}

		boardCells = new BoardCell[boardSize.x, boardSize.y];

		for (int rowIdx = 0; rowIdx < boardSize.y; rowIdx++)
		{
			for (int columnIdx = 0; columnIdx < boardSize.x; columnIdx++)
			{
				BoardCell cell = Instantiate(cellPrefab, transform);

				cell.transform.localPosition = new Vector3(columnIdx * cellSize, rowIdx * -cellSize);

				cell.name = $"Cell R: {rowIdx}, C: {columnIdx}";

				cell.CellPosition = new Vector2Int(columnIdx, rowIdx);

				boardCells[columnIdx, rowIdx] = cell;

#if UNITY_EDITOR
				UnityEditor.Undo.RegisterCreatedObjectUndo(cell.gameObject, nameof(GenerateBoard));
#endif
			}
		}

		SetupCellNeighbours();

		float boardWidth = boardSize.x * cellSize;
		float boardHeight = boardSize.y * cellSize;

		transform.localPosition = new Vector3(-boardWidth / 2f + cellSize / 2f, boardHeight / 2f - cellSize / 2f);
	}

	private void SetupCellNeighbours()
	{
		int columnCount = boardSize.x;
		int rowCount = boardSize.y;

		for (int columnIdx = 0; columnIdx < columnCount; columnIdx++)
		{
			for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
			{
				BoardCell cell = boardCells[columnIdx, rowIdx];

				BoardCell leftCell = columnIdx > 0 ? boardCells[columnIdx - 1, rowIdx] : null;
				BoardCell upperCell = rowIdx > 0 ? boardCells[columnIdx, rowIdx - 1] : null;
				BoardCell rightCell = columnIdx < columnCount - 1 ? boardCells[columnIdx + 1, rowIdx] : null;
				BoardCell lowerCell = rowIdx < rowCount - 1 ? boardCells[columnIdx, rowIdx + 1] : null;

				cell.SetNeighbourCells(leftCell, upperCell, rightCell, lowerCell);
			}
		}
	}

	public BoardCell GetRandomEmptyCellInColumnRange(int startIndex, int endIndex)
	{
		int columns = boardSize.x;

		startIndex = Mathf.Max(startIndex, 0);
		endIndex = Mathf.Min(Mathf.Max(endIndex, startIndex), columns - 1);

		return GetEmptyCellsInColumns(startIndex, endIndex).GetRandomOrDefault();
	}

	private IEnumerable<BoardCell> GetEmptyCellsInColumns(int startIndex, int endIndex)
	{
		int rowCount = boardSize.y;

		for (int columnIdx = startIndex; columnIdx <= endIndex; columnIdx++)
		{
			for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
			{
				BoardCell cell = BoardCells[columnIdx, rowIdx];
				
				if (cell.IsOccupied == false)
				{
					yield return cell;
				}
			}
		}
	}
}