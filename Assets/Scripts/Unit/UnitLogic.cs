using System.Collections.Generic;
using UnityEngine;

public class UnitLogic : MonoBehaviour, IUnitComponent
{
	[Space]
	[SerializeField] private bool canAttackStraight = true;
	[SerializeField] private bool canAttackDiagonally = true;

	public Unit Unit { get; private set; }

	public Unit AttackTarget { get; private set; }

	public List<Unit> AttackedByUnits { get; private set; } = new List<Unit>();

	public void Setup(Unit unit)
	{
		Unit = unit;
	}

	public void UpdateAttackTarget()
	{
		Unit targetUnit = FindTargetEnemyUnit();
		SetAttackTarget(targetUnit);
	}

	public bool UpdateMovement()
	{
		if (AttackTarget)
		{
			if (AttackTarget.Mover.Cell.IsNeighbourOf(Unit.Mover.Cell, includeDiagonal: false) == false)
			{
				Vector3 to = AttackTarget.transform.position;
				Vector3 from = Unit.transform.position;

				float angleRad = Mathf.Atan2(to.y - from.y, to.x - from.x);
				float angleDeg = 180f / Mathf.PI * angleRad * -1f + 180f;

				float walkDirection = Quaternion.Euler(0f, 0f, angleDeg).eulerAngles.z;

				Unit.Mover.MoveInDirection(walkDirection);
			}
		}

		return false;
	}

	public void UpdateAttack()
	{
		if (AttackTarget)
		{
			if (AttackTarget.Mover.Cell.IsNeighbourOf(Unit.Mover.Cell, 
						includeStraight: canAttackStraight, includeDiagonal: canAttackDiagonally))
			{
				Unit.AttackUnit(AttackTarget);
			}
		}
	}

	public void DoDamageToCurrentTarget()
	{
		if (AttackTarget)
		{
			AttackTarget.Health.TakeDamage(Unit.Health.AttackDamage);
		}
	}

	private void SetAttackTarget(Unit targetUnit)
	{
		if (AttackTarget && AttackTarget == targetUnit)
		{
			return;
		}

		if (AttackTarget && AttackTarget != targetUnit)
		{
			AttackTarget.Logic.AttackedByUnits.Remove(this.Unit);
		}

		AttackTarget = targetUnit;

		if (AttackTarget)
		{
			AttackTarget.Logic.AttackedByUnits.Add(this.Unit);
		}
	}

	private Unit FindTargetEnemyUnit()
	{
		Unit closestEnemyUnit = null;
		float closestEnemyUnitDistance = float.MaxValue;

		List<Unit> units = Unit.UnitManager.Units;

		for (int i = 0; i < units.Count; i++)
		{
			Unit otherUnit = units[i];

			if (Unit != otherUnit && Unit.Team != otherUnit.Team && otherUnit.Health.IsAlive)
			{
				float distance = (Unit.transform.position - otherUnit.transform.position).magnitude;

				if (closestEnemyUnit)
				{
					if (distance < closestEnemyUnitDistance)
					{
						closestEnemyUnit = otherUnit;
						closestEnemyUnitDistance = distance;
					}
				}
				else
				{
					closestEnemyUnit = otherUnit;
					closestEnemyUnitDistance = distance;
				}
			}
		}

		return closestEnemyUnit;
	}
}