using System.Collections.Generic;
using UnityEngine;

public class UnitAttacker : MonoBehaviour, IUnitComponent
{
	[Space]
	[SerializeField] private float attackDamage = 25f;

	[Space]
	[SerializeField] private bool canAttackStraight = true;
	[SerializeField] private bool canAttackDiagonally = true;

	public Unit Unit { get; private set; }

	public Unit AttackTarget { get; private set; }

	public void Setup(Unit unit)
	{
		Unit = unit;
	}

	public void UpdateAttackTarget()
	{
		Unit targetUnit = FindTargetEnemyUnit();
		SetAttackTarget(targetUnit);
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
			AttackTarget.Health.TakeDamage(attackDamage);
		}
	}

	private void SetAttackTarget(Unit targetUnit)
	{
		if (AttackTarget && AttackTarget == targetUnit)
		{
			return;
		}

		AttackTarget = targetUnit;
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
