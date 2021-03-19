using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnitGraphics : MonoBehaviour, IUnitComponent
{
	public Unit Unit { get; private set; }

	[Space]
	[SerializeField] private SpriteRenderer spriteRenderer;

	[Space]
	[SerializeField] private Transform shakeTransform;

	[Space]
	[SerializeField, Min(0)] private float damageResponseShakeAmount = 0.1f;
	[SerializeField, Min(0)] private float damageResponseShakeDuration = 0.05f;

	[Space]
	[SerializeField] private AnimationCurve attackStartCurve = Curves.CreateEaseInCurve();
	[SerializeField] private AnimationCurve attackEndCurve = Curves.CreateEaseOutCurve();

	public UnityEvent OnTriggerAttackDamage { get; private set; } = new UnityEvent();
	public UnityEvent OnHided { get; private set; } = new UnityEvent();

	private float shakeAmount = 0f;

	private Coroutine damageResponseShakeCoroutine = null;
	private Coroutine visibilityCoroutine = null;

	public void Setup(Unit unit)
	{
		Unit = unit;
	}

	private void Update()
	{
		UpdateShake();
	}

	private void UpdateShake()
	{
		if (shakeAmount > 0f)
		{
			Vector3 localPosition = shakeTransform.localPosition;
			localPosition.x = Random.insideUnitSphere.x * damageResponseShakeAmount;
			localPosition.y = Random.insideUnitSphere.y * damageResponseShakeAmount;
			shakeTransform.localPosition = localPosition;
		}
		else if (shakeAmount == 0f)
		{
			shakeTransform.localPosition = Vector3.zero;
			shakeAmount = -1f;
		}
	}

	public void SetColor(Color color)
	{
		spriteRenderer.color = color;
	}

	public void PlayAttackTo(Unit attackUnit)
	{
		IEnumerator Sequence()
		{
			Vector3 startPosition = transform.position;

			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.1f;
				Vector3 endPosition = Vector3.Lerp(startPosition, attackUnit.transform.position, 0.4f);
				transform.position = Vector3.Lerp(startPosition, endPosition, attackStartCurve.Evaluate(t));
				yield return null;
			}

			OnTriggerAttackDamage.Invoke();

			startPosition = transform.localPosition;

			t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.15f;
				transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, attackEndCurve.Evaluate(t));
				yield return null;
			}
		}

		StartCoroutine(Sequence());
	}

	public void PlayDamageResponseShake()
	{
		IEnumerator Sequence()
		{
			shakeAmount = damageResponseShakeAmount;

			yield return new WaitForSeconds(damageResponseShakeDuration);

			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.1f;
				shakeAmount = Mathf.Lerp(damageResponseShakeAmount, 0f, t);
				yield return null;
			}
		}

		CoroutineExt.Restart(ref damageResponseShakeCoroutine, this, Sequence());
	}

	public void PlayAppearAnimation()
	{
		IEnumerator Sequence()
		{
			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.2f;
				transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Curves.easeOut.Evaluate(t));
				yield return null;
			}
		}
		
		CoroutineExt.Restart(ref visibilityCoroutine, this, Sequence());
	}

	public void PlayDeathAnimation()
	{
		IEnumerator Sequence()
		{
			Vector3 startScale = transform.localScale;
			Vector3 endScale = startScale * 1.2f;

			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.2f;
				spriteRenderer.color.SetA(Mathf.Lerp(1f, 0f, t));
				transform.localScale = Vector3.Lerp(startScale, endScale, t);
				yield return null;
			}

			OnHided.Invoke();
		}

		CoroutineExt.Restart(ref visibilityCoroutine, this, Sequence());
	}
}