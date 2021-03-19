using UnityEngine;

public class UnitLogicManager : MonoBehaviour
{
	public UnitManager UnitManager { get; private set; }

	[Space]
	[SerializeField, Min(0)] private float updatePeriodicity = 1f;

	private float lastUpdateTime = 0f;

	public void Setup(UnitManager unitManager)
	{
		UnitManager = unitManager;

		lastUpdateTime = Time.time;
	}

	private void Update()
	{
		if (UnitManager == false) return;

		if (Time.time < lastUpdateTime + updatePeriodicity) return;

		lastUpdateTime = Time.time;

		// Update Attack Target for every Unit
		for (int i = 0; i < UnitManager.Units.Count; i++)
		{
			UnitManager.Units[i].Logic.UpdateAttackTarget();
		}

		// Update Movement for every Unit
		bool[] movementUpdated = new bool[UnitManager.Units.Count];

		for (int i = 0; i < UnitManager.Units.Count; i++)
		{
			movementUpdated[i] = UnitManager.Units[i].Logic.UpdateMovement();
		}

		// Update Attack for every Unit
		for (int i = 0; i < UnitManager.Units.Count; i++)
		{
			if (movementUpdated[i] == false)
			{
				UnitManager.Units[i].Logic.UpdateAttack();
			}
		}
	}
}