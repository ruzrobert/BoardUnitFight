using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour, IUnitComponent
{
	[Space]
	[SerializeField] private bool canMoveStraight = true;
	[SerializeField] private bool canMoveDiagonally = true;

	[Space]
	[SerializeField] private bool tryMoveAroundOccupiedCell = true;

	public Unit Unit { get; private set; }
	public BoardCell Cell { get; private set; }

	private Coroutine moveCoroutine = null;

	public void Setup(Unit unit)
	{
		Unit = unit;
	}

	public void TeleportToCell(BoardCell cell)
	{
		if (Cell) Cell.FreeFrom(Unit);

		Cell = cell;
		cell.OccupyBy(Unit);

		transform.position = cell.transform.position;
		CoroutineExt.Stop(ref moveCoroutine, this);
	}

	public void MoveToCell(BoardCell cell)
	{
		if (Cell) Cell.FreeFrom(Unit);

		Cell = cell;
		cell.OccupyBy(Unit);

		CoroutineExt.Restart(ref moveCoroutine, this, MoveTransition());
	}

	public bool TryMoveToCell(BoardCell cell)
	{
		if (cell == null || cell.IsOccupied || (Cell != null && cell.IsNeighbourOf(Cell) == false))
		{
			return false;
		}

		MoveToCell(cell);
		return true;
	}

	private bool TryMoveToCellVariants(BoardCell cell1, BoardCell cell2, BoardCell cell3)
	{
		if (tryMoveAroundOccupiedCell)
		{
			return TryMoveToCell(cell1) || TryMoveToCell(cell2) || TryMoveToCell(cell3);
		}
		else
		{
			return TryMoveToCell(cell1);
		}
	}

	private bool TryMoveToCellVariants(BoardCell cell1, BoardCell cell2, BoardCell cell3, BoardCell cell4, BoardCell cell5)
	{
		if (tryMoveAroundOccupiedCell)
		{
			return TryMoveToCell(cell1) || TryMoveToCell(cell2) || TryMoveToCell(cell3) || TryMoveToCell(cell4) || TryMoveToCell(cell5);
		}
		else
		{
			return TryMoveToCell(cell1);
		}
	}

	public bool MoveInDirection(float directionAngle)
	{
		MoveDirection direction = MoveDirectionHelper.GetMoveDirectionFromDirectionAngle(directionAngle, canMoveStraight, canMoveDiagonally);
		return MoveInDirection(direction);
	}

	public bool MoveInDirection(MoveDirection direction)
	{
		if (Cell == null) return false;

		bool straight = canMoveStraight;
		bool diag = canMoveDiagonally;

		switch (direction)
		{
			case MoveDirection.Left:
				return TryMoveToCellVariants(Cell.LeftCell, diag ? Cell.DiagonalUpperLeftCell : null,
										diag ? Cell.DiagonalLowerLeftCell : null, Cell.UpperCell, Cell.LowerCell);
			case MoveDirection.Up:
				return TryMoveToCellVariants(Cell.UpperCell, diag ? Cell.DiagonalUpperLeftCell : null,
										diag ? Cell.DiagonalUpperRightCell : null, Cell.LeftCell, Cell.RightCell);
			case MoveDirection.Right:
				return TryMoveToCellVariants(Cell.RightCell, diag ? Cell.DiagonalUpperRightCell : null,
										diag ? Cell.DiagonalLowerRightCell : null, Cell.UpperCell, Cell.LowerCell);
			case MoveDirection.Down:
				return TryMoveToCellVariants(Cell.LowerCell, diag ? Cell.DiagonalLowerRightCell : null,
										diag ? Cell.DiagonalLowerLeftCell : null, Cell.LeftCell, Cell.RightCell);
			case MoveDirection.DiagonalUpLeft:
				return TryMoveToCellVariants(Cell.DiagonalUpperLeftCell, straight ? Cell.LeftCell : null, straight ? Cell.UpperCell : null);
			case MoveDirection.DiagonalUpRight:
				return TryMoveToCellVariants(Cell.DiagonalUpperRightCell, straight ? Cell.UpperCell : null, straight ? Cell.RightCell : null);
			case MoveDirection.DiagonalDownLeft:
				return TryMoveToCellVariants(Cell.DiagonalLowerLeftCell, straight ? Cell.LeftCell : null, straight ? Cell.LowerCell : null);
			case MoveDirection.DiagonalDownRight:
				return TryMoveToCellVariants(Cell.DiagonalLowerRightCell, straight ? Cell.LowerCell : null, straight ? Cell.RightCell : null);
		}

		return false;
	}

	private IEnumerator MoveTransition()
	{
		Vector3 startPosition = transform.position;

		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / 0.15f;
			float curvedT = Curves.smooth.Evaluate(t);

			transform.position = Vector3.Lerp(startPosition, Cell.transform.position, curvedT);

			yield return null;
		}
	}
}