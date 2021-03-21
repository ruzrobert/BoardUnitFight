using UnityEngine;
using UnityEngine.UI;

public class UIFpsText : MonoBehaviour
{
    [Space]
    [SerializeField] private Text text;

	//private float fpsDeltaTime = 0f;

	private void Update()
	{
		//fpsDeltaTime += (Time.unscaledDeltaTime - fpsDeltaTime) * 0.1f;
		//text.text = (1f / fpsDeltaTime).ToString("0");

		int fps = (int)(1f / Time.unscaledDeltaTime);
		text.text = fps.ToString();
	}

	private void OnValidate()
	{
		if (text == false)
		{
			text = GetComponent<Text>();
		}
	}
}