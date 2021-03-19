using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
    [Space]
    [SerializeField] private UnitTeam leftTeam = new UnitTeam();

    [Space]
    [SerializeField] private UnitTeam rightTeam = new UnitTeam();

    public UnitManager UnitManager { get; private set; }

	private void OnValidate()
	{
        leftTeam.Side = TeamSide.Left;
        rightTeam.Side = TeamSide.Right;
	}

    public void Setup(UnitManager unitManager)
	{
        UnitManager = unitManager;
	}

	public void SpawnAllUnits()
	{
        SpawnUnitsForTeam(leftTeam);
        SpawnUnitsForTeam(rightTeam);
	}

    private void SpawnUnitsForTeam(UnitTeam team)
    {
        int unitCount = Random.Range(team.UnitSpawnCountRange.min, team.UnitSpawnCountRange.max + 1);

        for (int unitIdx = 0; unitIdx < unitCount; unitIdx++)
        {
            BoardCell randomCell = GetRandomCellToSpawnUnitAt(team);

            if (randomCell)
			{
                SpawnUnitAt(randomCell, team);
			}
        }
    }

    private BoardCell GetRandomCellToSpawnUnitAt(UnitTeam forTeam)
	{
        BoardGenerator boardGenerator = GameBoard.Instance.Generator;
        int columns = boardGenerator.BoardSize.x;

        int startIndex = forTeam.Side == TeamSide.Left ? 0 : columns - forTeam.SpawnColumns;
        int endIndex = startIndex + forTeam.SpawnColumns - 1;

        return boardGenerator.GetRandomEmptyCellInColumnRange(startIndex, endIndex);
    }

    private void SpawnUnitAt(BoardCell cell, UnitTeam team)
	{
		Unit unit = Instantiate(team.UnitPrefab, transform);
        unit.Setup(UnitManager, team);
        unit.SpawnAt(cell);

        unit.OnDone.AddListener(() => UnitManager.OnUnitDone(unit));

        UnitManager.Units.Add(unit);
	}
}