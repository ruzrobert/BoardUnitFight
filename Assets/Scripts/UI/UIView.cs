using System.Collections;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
	[Header("UI View")]
	[SerializeField] private CanvasGroup canvasGroup;

	public bool IsVisible { get; private set; } = false;

	protected GameUI GameUI { get; private set; }

	private Coroutine visibilityCoroutine = null;

	public virtual void Setup(GameUI gameUI)
	{
		GameUI = gameUI;

		EventManager.Instance.GameState.OnGameStateChanged.AddListener(OnGameStateChanged);
	}

	private void OnValidate()
	{
		if (canvasGroup == null)
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
	}

	protected virtual void OnGameStateChanged(GameState gameState) { }

	protected virtual void OnShowing() { }

	public void Show()
	{
		if (IsVisible == false)
		{
			IsVisible = true;
			PlayVisibilityAnimation(show: true);
		}
	}

	public void Hide()
	{
		if (IsVisible)
		{
			IsVisible = false;

			PlayVisibilityAnimation(show: false);
		}
	}

	private void PlayVisibilityAnimation(bool show)
	{
		IEnumerator Transition()
		{
			const float TargetScale = 1.1f;

			if (show && gameObject.activeSelf == false)
			{
				gameObject.SetActive(true);

				canvasGroup.alpha = 0f;
				transform.localScale = Vector3.one * TargetScale;
			}

			if (show)
			{
				OnShowing();
			}

			float startAlpha = canvasGroup.alpha;
			float endAlpha = show ? 1f : 0f;

			Vector3 startScale = transform.localScale;
			Vector3 endScale = show ? Vector3.one : Vector3.one * TargetScale;

			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.2f;

				canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
				transform.localScale = Vector3.Lerp(startScale, endScale, t);

				yield return null;
			}

			if (show == false)
			{
				gameObject.SetActive(false);
			}
		}

		CoroutineExt.Restart(ref visibilityCoroutine, GameUI, Transition());
	}
}