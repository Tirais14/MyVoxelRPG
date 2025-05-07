using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
	public Action<float> OnValueChanged { get; private set; }
	[SerializeField] private Image barBorder;
	[SerializeField] private Image barIndicator;
	[SerializeField] private TextMeshProUGUI textValue;

	private void ChangeIndicatorsValue(float value)
	{
		textValue.text = Mathf.RoundToInt(value).ToString();

	}
}
