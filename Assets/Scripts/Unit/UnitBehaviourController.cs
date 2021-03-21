using UnityEngine;

public class UnitBehaviourController : MonoBehaviour, IUnitComponent
{
	public Unit Unit { get; private set; }

	public void Setup(Unit unit)
	{
		Unit = unit;
	}

	public void UpdateAttackTarget()
	{
		Unit.Attacker.UpdateAttackTarget();
	}

	public bool UpdateMovement()
	{
		return Unit.Mover.UpdateMovement();
	}

	public void UpdateAttack()
	{
		Unit.Attacker.UpdateAttack();
	}
}