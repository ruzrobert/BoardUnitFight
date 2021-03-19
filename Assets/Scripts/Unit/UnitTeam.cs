using System;
using UnityEngine;

[Serializable]
public class UnitTeam
{
    [SerializeField] private TeamSide side = TeamSide.Left;

    [Space]
    [SerializeField] private RangeI unitSpawnCountRange = new RangeI(2, 5);
    [SerializeField, Min(1)] private int spawnColumns = 3;

    [Space]
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private Color unitColor = Color.blue;

    public TeamSide Side { get => side; set => side = value; }

    public RangeI UnitSpawnCountRange => unitSpawnCountRange;
    public int SpawnColumns => spawnColumns;

    public Unit UnitPrefab => unitPrefab;
    public Color UnitColor => unitColor;
}