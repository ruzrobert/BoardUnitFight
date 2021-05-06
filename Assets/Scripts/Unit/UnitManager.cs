using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Space]
    [SerializeField] private UnitSpawner spawner;
	[SerializeField] private UnitFightController fightController;

    [Space]
    [SerializeField] private List<Unit> units = new List<Unit>();

    public List<Unit> Units => units;

	private void Awake()
	{
		spawner.Setup(this);
		fightController.Setup(this);

		fightController.enabled = false;

		EventManager.Instance.GameState.OnGameplayStarting.AddListener(OnGameplayStarting);
	}

	private void OnGameplayStarting()
	{
		IEnumerator StartSequence()
		{
			if (KillAllUnits())
			{
				yield return new WaitForSeconds(1f);
			}

			spawner.SpawnAllUnits();
			fightController.enabled = true;
		}

		StartCoroutine(StartSequence());
	}

	public void OnUnitDone(Unit unit)
	{
		Units.Remove(unit);
	}

	private bool KillAllUnits()
	{
		bool foundAny = false;

		for (int i = Units.Count - 1; i >= 0; i--)
		{
			Unit unit = Units[i];
			unit.Health.Kill();

			foundAny = true;
		}

		return foundAny;
	}
}