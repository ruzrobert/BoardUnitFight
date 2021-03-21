using UnityEngine;

public static class MoveDirectionHelper
{
	public static MoveDirection GetMoveDirectionFromDirectionAngle(float directionAngle, bool canMoveStraight, bool canMoveDiagonally)
	{
		if (canMoveStraight && canMoveDiagonally)
		{
			return DirectionAngleToEightMoveDirections(directionAngle);
		}
		else if (canMoveStraight && canMoveDiagonally == false)
		{
			return DirectionAngleToStraightMoveDirection(directionAngle);
		}
		else if (canMoveStraight == false && canMoveDiagonally)
		{
			return DirectionAngleToDiagonalMoveDirection(directionAngle);
		}

		return MoveDirection.None;
	}

	private static MoveDirection DirectionAngleToStraightMoveDirection(float directionAngle)
	{
		return DirectionAngleToFourMoveDirections(directionAngle, isStraight: true);
	}

	private static MoveDirection DirectionAngleToDiagonalMoveDirection(float directionAngle)
	{
		return DirectionAngleToFourMoveDirections(directionAngle, isStraight: false);
	}

	private static MoveDirection DirectionAngleToFourMoveDirections(float directionAngle, bool isStraight)
	{
		float stepAngle = 360f / 4f;
		float stepHalf = stepAngle * 0.5f;
		directionAngle = isStraight ? Quaternion.Euler(0f, 0f, directionAngle + stepHalf).eulerAngles.z : directionAngle;

		if (MathExt.IsBetween(directionAngle, 0f, stepAngle))
			return isStraight ? MoveDirection.Left : MoveDirection.DiagonalUpLeft;
		else if (MathExt.IsBetween(directionAngle, stepAngle, stepAngle * 2f))
			return isStraight ? MoveDirection.Up : MoveDirection.DiagonalUpRight;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 2f, stepAngle * 3f))
			return isStraight ? MoveDirection.Right : MoveDirection.DiagonalDownRight;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 3f, stepAngle * 4f))
			return isStraight ? MoveDirection.Down : MoveDirection.DiagonalDownLeft;
		else
			return MoveDirection.None;
	}

	private static MoveDirection DirectionAngleToEightMoveDirections(float directionAngle)
	{
		float stepAngle = 360f / 8f;
		float stepHalf = stepAngle * 0.5f;
		directionAngle = Quaternion.Euler(0f, 0f, directionAngle + stepHalf).eulerAngles.z;

		if (MathExt.IsBetween(directionAngle, 0f, stepAngle))
			return MoveDirection.Left;
		else if (MathExt.IsBetween(directionAngle, stepAngle, stepAngle * 2f))
			return MoveDirection.DiagonalUpLeft;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 2f, stepAngle * 3f))
			return MoveDirection.Up;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 3f, stepAngle * 4f))
			return MoveDirection.DiagonalUpRight;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 4f, stepAngle * 5f))
			return MoveDirection.Right;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 5f, stepAngle * 6f))
			return MoveDirection.DiagonalDownRight;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 6f, stepAngle * 7f))
			return MoveDirection.Down;
		else if (MathExt.IsBetween(directionAngle, stepAngle * 7f, stepAngle * 8f))
			return MoveDirection.DiagonalDownLeft;

		return MoveDirection.None;
	}
}