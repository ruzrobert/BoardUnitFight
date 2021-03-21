using System.Linq;
using UnityEngine;

public class UnitFightController : MonoBehaviour
{
	public UnitManager UnitManager { get; private set; }

	[Space]
	[SerializeField, Min(0)] private float updatePeriodicity = 1f;
	
	public TeamSide WinnerTeam { get; private set; }

	private float lastUpdateTime = 0f;

	public void Setup(UnitManager unitManager)
	{
		UnitManager = unitManager;
	}

	private void OnEnable()
	{
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
			UnitManager.Units[i].BehaviourController.UpdateAttackTarget();
		}

		// Update Movement for every Unit
		bool[] movementUpdated = new bool[UnitManager.Units.Count];

		for (int i = 0; i < UnitManager.Units.Count; i++)
		{
			movementUpdated[i] = UnitManager.Units[i].BehaviourController.UpdateMovement();
		}

		// Update Attack for every Unit
		for (int i = 0; i < UnitManager.Units.Count; i++)
		{
			if (movementUpdated[i] == false)
			{
				UnitManager.Units[i].BehaviourController.UpdateAttack();
			}
		}

		CheckForGameEnd();
	}

	private void CheckForGameEnd()
	{
		int countOfAliveTeams = UnitManager.Units.Select(x => x.Team).Distinct().Count();

		if (countOfAliveTeams <= 1)
		{
			UnitTeam winnerTeam = null;

			if (countOfAliveTeams == 1)
			{
				winnerTeam = UnitManager.Units.First().Team;
			}

			OnGameEnded(winnerTeam);
		}
	}

	private void OnGameEnded(UnitTeam winnerTeam)
	{
		this.enabled = false;
		
		EventManager.Instance.OnGameEnded.Invoke(winnerTeam);
	}
}