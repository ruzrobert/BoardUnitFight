using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour, IUnitComponent
{
	public Unit Unit { get; private set; }

	[Space]
	[SerializeField, Min(0)] private float maxHealth = 100f;

	[Header("Info")]
	[SerializeField] private float currentHealth = 0f;

	public bool IsAlive { get; private set; } = false;
	
	public float CurrentHealth => currentHealth;

	public float NormalizedCurrentHealth => Mathf.InverseLerp(0f, maxHealth, currentHealth);

	public UnityEvent OnDamageReceived { get; private set; } = new UnityEvent();
	public UnityEvent OnDeath { get; private set; } = new UnityEvent();

	private UIHealthBar healthBar;

	public void Setup(Unit unit)
	{
		Unit = unit;

		IsAlive = true;
		currentHealth = maxHealth;

		healthBar = GameUI.Instance.Gameplay.HealthBars.Create(this);
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

	public void Kill()
	{
		TakeDamage(currentHealth);
	}
}