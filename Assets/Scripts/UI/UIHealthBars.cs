using System.Collections.Generic;
using UnityEngine;

public class UIHealthBars : MonoBehaviour
{
	[Space]
	[SerializeField] private UIHealthBar healthBarPrefab;

	[Space]
	[SerializeField] private Vector2 healthBarOffset = Vector2.zero;

	public Vector2 HealthBarOffset => healthBarOffset;

	private List<UIHealthBar> healthBars = new List<UIHealthBar>();

    public UIHealthBar Create(UnitHealth healthSystem)
	{
		UIHealthBar healthBar = Instantiate(healthBarPrefab, transform);
		healthBar.Setup(this, healthSystem);

		RegisterHealthBar(healthBar);

		return healthBar;
	}

	private void RegisterHealthBar(UIHealthBar healthBar) => healthBars.Add(healthBar);
	public void UnRegisterHealthBar(UIHealthBar healthBar) => healthBars.Remove(healthBar);
	
	public void HideAllHealthBars()
	{
		for (int i = healthBars.Count - 1; i >= 0; i--)
		{
			UIHealthBar hb = healthBars[i];
			hb.Hide();
		}
	}

	private void OnDisable()
	{
		HideAllHealthBars();
	}
}