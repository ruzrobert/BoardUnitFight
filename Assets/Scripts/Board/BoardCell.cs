using UnityEngine;

public class BoardCell : MonoBehaviour
{
	[Space]
	[SerializeField] private Vector2Int cellPosition = Vector2Int.zero;

	[Space]
	[SerializeField] private BoardCell[] sideCells = new BoardCell[4]; // left, upper, right, lower

	public BoardCell LeftCell { get => sideCells[0]; private set => sideCells[0] = value; }
	public BoardCell UpperCell { get => sideCells[1]; private set => sideCells[1] = value; }
	public BoardCell RightCell { get => sideCells[2]; private set => sideCells[2] = value; }
	public BoardCell LowerCell { get => sideCells[3]; private set => sideCells[3] = value; }

	public BoardCell DiagonalUpperLeftCell => LeftCell ? LeftCell.UpperCell : null;
	public BoardCell DiagonalUpperRightCell => RightCell ? RightCell.UpperCell : null;
	public BoardCell DiagonalLowerLeftCell => LeftCell ? LeftCell.LowerCell : null;
	public BoardCell DiagonalLowerRightCell => RightCell ? RightCell.LowerCell : null;

	public Vector2Int CellPosition { get => cellPosition; set => cellPosition = value; }

    public Unit OccupiedByUnit { get; private set; }

    public bool IsOccupied => OccupiedByUnit;

	public void SetNeighbourCells(BoardCell leftCell, BoardCell upperCell, BoardCell rightCell, BoardCell lowerCell)
	{
		LeftCell = leftCell;
		UpperCell = upperCell;
		RightCell = rightCell;
		LowerCell = lowerCell;
	}

    public void OccupyBy(Unit unit)
	{
		OccupiedByUnit = unit;
	}

	public void FreeFrom(Unit unit)
	{
		if (OccupiedByUnit == unit)
		{
			OccupiedByUnit = null;
		}
	}

	public bool IsNeighbourOf(BoardCell cell, bool includeStraight = true, bool includeDiagonal = true)
	{
		if (includeStraight)
		{
			if (LeftCell == cell || UpperCell == cell || RightCell == cell || LowerCell == cell)
			{
				return true;
			}
		}

		if (includeDiagonal)
		{
			if (DiagonalUpperLeftCell == cell || DiagonalUpperRightCell == cell
					|| DiagonalLowerRightCell == cell || DiagonalLowerLeftCell == cell)
			{
				return true;
			}
		}

		return false;
	}
}