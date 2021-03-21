using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [Space]
    [SerializeField] private Color maxColor = Color.green;
    [SerializeField] private Color minColor = Color.red;

    [Space]
    [SerializeField] private RectTransform rectTransform;

    [Space]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image fillImage;

    public UIHealthBars HealthBars { get; private set; }

    private UnitHealth healthSystem = null;
    private bool isVisible = false;

    private Coroutine visibilityCoroutine = null;
    private Coroutine fillCoroutine = null;

    private bool shouldDestroyAfterHide = false;

    private Vector2 targetPosition = Vector2.zero;

    public void Setup(UIHealthBars healthBars, UnitHealth healthSystem)
	{
        HealthBars = healthBars;
        this.healthSystem = healthSystem;

        healthSystem.OnDamageReceived.AddListener(OnDamageReceived);
        healthSystem.OnDeath.AddListener(OnDeath);

        isVisible = false;
        gameObject.SetActive(false);

        fillImage.fillAmount = 1f;
        fillImage.color = GetFillColor();
    }

	private void Update()
	{
        if (isVisible)
        {
            UpdatePosition();
        }
	}

    private void UpdatePosition()
	{
        Vector2 screenOffset = HealthBars.HealthBarOffset;
        Vector3 worldPosition = healthSystem.Unit.transform.position;

        Vector2 targetPositionScreenPoint = GameCamera.Instance.Camera.WorldToScreenPoint(worldPosition);
        float scaleFactor = GameUI.Instance.Canvas.scaleFactor;
        targetPositionScreenPoint = new Vector2(targetPositionScreenPoint.x / scaleFactor, targetPositionScreenPoint.y / scaleFactor);

        Vector2 prevTargetPosition = targetPosition;
        targetPosition = targetPositionScreenPoint + screenOffset;

        if (prevTargetPosition != Vector2.zero)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * 40f);
        }
        else
        {
            rectTransform.anchoredPosition = targetPosition;
        }
    }

	private Color GetFillColor()
	{
        return Color.Lerp(minColor, maxColor, fillImage.fillAmount);
	}

    private void OnDamageReceived()
	{
        Show();
        PlayUpdateValue();
    }

    private void OnDeath()
	{
        shouldDestroyAfterHide = true;

        if (isVisible)
        {
            Hide();
        }
        else
		{
            OnHidden();
        }
	}

    private void Show()
	{
        if (isVisible == false)
		{
            isVisible = true;
            PlayVisibility(show: true);

            targetPosition = Vector2.zero;
            UpdatePosition();
        }
	}

    public void Hide()
	{
        if (isVisible)
		{
            isVisible = false;

            if (gameObject.activeInHierarchy)
            {
                PlayVisibility(show: false);
            }
            else
			{
                OnHidden();
			}
		}
	}

    private void PlayVisibility(bool show)
	{
        IEnumerator Sequence()
		{
            if (show && gameObject.activeSelf == false)
			{
                gameObject.SetActive(true);
			}

            float t = 0f;
            while (t < 1f)
			{
                t += Time.deltaTime / 0.15f;

                yield return null;
			}

            if (show == false)
			{
                OnHidden();
            }
		}

        CoroutineExt.Restart(ref visibilityCoroutine, HealthBars, Sequence());
	}

    private void OnHidden()
	{
        gameObject.SetActive(false);

        CoroutineExt.Stop(ref fillCoroutine, HealthBars);

        if (shouldDestroyAfterHide)
        {
            Destroy(gameObject);
        }

        HealthBars.UnRegisterHealthBar(this);
    }

    private void PlayUpdateValue()
	{
        IEnumerator Sequence()
		{
            float startFillAmount = fillImage.fillAmount;
            float endFillAmount = healthSystem.NormalizedCurrentHealth;

            float t = 0f;
            while (t < 1f)
			{
                t += Time.deltaTime / 0.15f;

                fillImage.fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, Curves.smooth.Evaluate(t));
                fillImage.color = GetFillColor();

                yield return null;
			}
		}

        CoroutineExt.Restart(ref fillCoroutine, HealthBars, Sequence());
	}
}