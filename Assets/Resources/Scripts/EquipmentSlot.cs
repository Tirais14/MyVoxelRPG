using TMPro;
using UnityEngine;

public class EquipmentSlot : ContainerSlot
{
	[SerializeField] private TextMeshProUGUI slotName;

	private void ItemChanged()
	{
		if (containedItem.Item != null && slotName.text.Contains("Right Item"))
		{
			GameManagers.Player.SetItemToRightArm((Weapon)containedItem.Item);
		}
		else if (containedItem.Item == null && slotName.text.Contains("Right Item"))
		{
			GameManagers.Player.RemoveItemFromRightArm();
		}
	}

	public override void Show()
	{
		base.Show();
		slotName.enabled = true;
	}

	public override void Hide()
	{
		base.Hide();
		slotName.enabled = false;
	}

	public override void Initialize(int id)
	{
		base.Initialize(id);
		slotName.text = gameObject.name.Replace("EquipmentSlot", "").Replace('_', ' ');
	}

	private void OnEnable()
	{
		OnItemChangedCallback += CheckSlotState;
		OnItemChangedCallback += ItemChanged;
	}

	private void OnDisable()
	{
		OnItemChangedCallback -= CheckSlotState;
		OnItemChangedCallback -= ItemChanged;
	}
}
