using System.Reflection;
using TMPro;
using UnityEngine;

public class EquipmentSlot : ContainerSlot, IEquipmentSlot
{
	public new IEquipable Item { get { return (IEquipable)containedItem.Item; } }
	private new IEquipmentUI OwnerContainer { get { return (IEquipmentUI)ownerContainer; } }
	private TextMeshProUGUI slotName;

	private void SetItemToPlayer()
	{
		if (Item == null)
		{
			Debug.Log("Item null");
			return; 
		}

		if (name.Contains(OwnerContainer.RItemSlotName))
		{ GameManagers.Player.SetRightItem(Item); }
		else if (name.Contains(OwnerContainer.LItemSlotName))
		{ GameManagers.Player.SetLeftItem(Item); }
	}

	public override int AddItem(Item item, in int addItemsCount)
	{
		if (item is not IEquipable)
		{
			Logger.SendError(gameObject:gameObject, $"Item {item.name} is not equipable");
			return -1;
		}

		return base.AddItem(item, addItemsCount);
	}

	public override int RemoveItem(Item item, int deleteItemsCount)
	{
		if (item is not IEquipable)
		{
			Logger.SendError(gameObject: gameObject, $"Item {item.name} is not equipable");
			return -1;
		}

		return base.RemoveItem(item, deleteItemsCount);
	}

	public override void Initialize()
	{
		base.Initialize();
		slotName = this.GetComponentByObjNameInChildren<TextMeshProUGUI>("$SlotName$");
		slotName.text = gameObject.name.Replace("EquipmentSlot", "").Replace('_', ' ');
		name = slotName.text;
	}

	private void OnEnable()
	{
		onItemChangedCallback += CheckSlotState;
		onItemChangedCallback += SetItemToPlayer;
	}

	private void OnDisable()
	{
		onItemChangedCallback -= CheckSlotState;
		onItemChangedCallback -= SetItemToPlayer;
	}
}
