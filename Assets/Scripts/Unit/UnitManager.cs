using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Space]
    [SerializeField] private UnitSpawner spawner;
	[SerializeField] private UnitLogicManager logicManager;

    [Space]
    [SerializeField] private List<Unit> units = new List<Unit>();

    public List<Unit> Units => units;

	private void Awake()
	{
		spawner.Setup(this);
		logicManager.Setup(this);
	}

	private void Start()
	{
        spawner.SpawnAllUnits();
	}

	public void OnUnitDone(Unit unit)
	{
		Units.Remove(unit);
	}
}