using System;
using System.Reflection;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GraphicItemCounter : MonoBehaviour, ISwitchableGUI
{
	[SerializeField] private TextMeshProUGUI counterGUI;
	[SerializeField] private ContainerItem containerItem;
	private Action OnItemCountChangedCallback;

	private void UpdateCounterGUI()
	{
		//Debug.Log("OnItemCountChanged");
		counterGUI.text = containerItem.Count.ToString();
		if (containerItem.Count <= 0)
		{ counterGUI.enabled = false; }
		else if (containerItem.Count > 0)
		{ counterGUI.enabled = true; }
	}

	public void Show() => counterGUI.enabled = true;

	public void Hide() => counterGUI.enabled = false;

	public Action Initialize()
	{
		return OnItemCountChangedCallback;
	}

	private void OnEnable() => OnItemCountChangedCallback += UpdateCounterGUI;

	private void OnDisable() => OnItemCountChangedCallback -= UpdateCounterGUI;
}
