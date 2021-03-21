using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [Space]
    [SerializeField] private UnitGraphics graphics;
    [SerializeField] private UnitMover mover;
    [SerializeField] private UnitHealth health;
    [SerializeField] private UnitAttacker attacker;
    [SerializeField] private UnitBehaviourController behaviourController;

    public UnitGraphics Graphics => graphics;
    public UnitMover Mover => mover;
    public UnitHealth Health => health;
    public UnitAttacker Attacker => attacker;
    public UnitBehaviourController BehaviourController => behaviourController;

    public UnitManager UnitManager { get; private set; }
    public UnitTeam Team { get; private set; }

    public UnityEvent OnDone { get; private set; } = new UnityEvent();

	private void Awake()
	{
        graphics.Setup(this);
        mover.Setup(this);
        health.Setup(this);
        attacker.Setup(this);
        behaviourController.Setup(this);

        graphics.OnTriggerAttackDamage.AddListener(OnGraphicsTriggeredAttackDamage);
        graphics.OnHided.AddListener(OnHided);
        
        health.OnDamageReceived.AddListener(OnReceivedDamage);
        health.OnDeath.AddListener(OnDeath);
	}

    public void Setup(UnitManager unitManager, UnitTeam team)
	{
        UnitManager = unitManager;
        Team = team;

        if (graphics) graphics.SetColor(team.UnitColor);
	}

    private void OnGraphicsTriggeredAttackDamage()
	{
        attacker.DoDamageToCurrentTarget();
	}

    private void OnReceivedDamage()
	{
        graphics.PlayDamageResponseShake();
	}

    private void OnDeath()
	{
        graphics.PlayDeathAnimation();
	}

    private void OnHided()
	{
        Mover.Cell.FreeFrom(this);

        OnDone.Invoke();
        gameObject.SetActive(false);
	}

	public void SpawnAt(BoardCell cell)
    {
        if (cell.IsOccupied)
        {
            Debug.LogError($"Cell {cell} is already occupied by {cell.OccupiedByUnit}!");
            return;
        }

        graphics.PlayAppearAnimation();
        mover.TeleportToCell(cell);
    }

    public void AttackUnit(Unit attackUnit)
	{
        graphics.PlayAttackTo(attackUnit);
	}
}