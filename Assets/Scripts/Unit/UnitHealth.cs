using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour, IUnitComponent
{
	public Unit Unit { get; private set; }

	[Space]
	[SerializeField, Min(0)] private float maxHealth = 100f;

	[Space]
	[SerializeField] private float attackDamage = 25f;

	[Header("Info")]
	[SerializeField] private float currentHealth = 0f;

	public bool IsAlive { get; private set; } = false;

	public float AttackDamage => attackDamage;
	public float CurrentHealth => currentHealth;

	public UnityEvent OnDamageReceived { get; private set; } = new UnityEvent();
	public UnityEvent OnDeath { get; private set; } = new UnityEvent();

	public void Setup(Unit unit)
	{
		Unit = unit;

		IsAlive = true;
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage)
	{
		if (IsAlive == false || damage <= 0f) return;

		currentHealth = Mathf.Max(currentHealth - damage, 0f);

		if (currentHealth <= 0f)
		{
			IsAlive = false;
			OnDeath.Invoke();
		}
		else
		{
			OnDamageReceived.Invoke();
		}
	}
}