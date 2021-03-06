using UnityEngine;

[ExecuteAlways]
public class DarkerSpriteColor : MonoBehaviour
{
    [Space]
	[SerializeField] private SpriteRenderer thisSpriteRenderer;

    [Space]
    [SerializeField, Range(0f, 1f)] private float darkness = 0.2f;

	[Space]
	[SerializeField] private SpriteRenderer sourceSpriteRenderer;

	private Color sourceColor = Color.black;

	private void Update()
	{
		if (sourceSpriteRenderer)
		{
			if (sourceColor != sourceSpriteRenderer.color)
			{
				sourceColor = sourceSpriteRenderer.color;

				UpdateColor();
			}
		}
	}

	private void Reset()
	{
		OnValidate();
	}

	private void OnValidate()
	{
		if (thisSpriteRenderer == null)
		{
			thisSpriteRenderer = GetComponent<SpriteRenderer>();
		}

		if (sourceSpriteRenderer == null && transform.parent != null)
		{
			sourceSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
		}

		UpdateColor();
	}

	private void UpdateColor()
	{
		if (thisSpriteRenderer && sourceSpriteRenderer)
		{
			Color sourceColor = sourceSpriteRenderer.color;

			Color.RGBToHSV(sourceColor, out float H, out float S, out float V);
			V = Mathf.Clamp01(V - darkness);

			Color newColor = Color.HSVToRGB(H, S, V).SetA(sourceColor.a);
			thisSpriteRenderer.color = newColor;
		}
	}
}