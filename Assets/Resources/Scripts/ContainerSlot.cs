using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ContainerSlot : MonoBehaviour, IContainerSlot, ISwitchableGUI
{
	private int id;
	public int Id => id;
	[SerializeField] private Image imageComp;
	[SerializeField] private Button buttonComp;
	[SerializeField] private protected ContainerItem containedItem;
	[SerializeField] private bool isContextMenuEnabled;
	public bool ContextMenuEnabled => isContextMenuEnabled;
	public ContainerItem ContainedItem => containedItem;
	public enum Condition : byte
	{
		Empty,
		Filled,
		FilledMax
	}
	private protected Condition condition;
	public virtual Condition ConditionProperty => condition;

	public Action OnItemChangedCallback { get; private protected set; }

	private protected virtual void CheckSlotState()
	{
		if (containedItem.Item != null)
		{
			if (containedItem.Count < containedItem.Item.MaxCount)
			{ condition = Condition.Filled; }
			else if (containedItem.Count == containedItem.Item.MaxCount)
			{ condition = Condition.FilledMax; }
		}
		else
		{ condition = Condition.Empty; }
    }

	private void SetSlotId(int id) => this.id = id;

	public virtual int AddItem(Item item, in int addItemsCount)
	{
		if (item == null)
		{
			DebugLogging.NullParameter<Item>(gameObject);
			return addItemsCount;
		}
		if (addItemsCount == 0)
		{ return 0; }
		if (addItemsCount < 0)
		{
			DebugLogging.WrongParameter(gameObject: gameObject, addItemsCount);
			return -1;
		}

		int resultItemsCount = containedItem.Count + addItemsCount;
		if (resultItemsCount == 0)
		{ return 0; }

		if ((condition == Condition.Filled && containedItem.Item == item) || condition == Condition.Empty)
        {
			if (resultItemsCount >= item.MaxCount)
			{
				containedItem.SetItemMax(item);
				return resultItemsCount - item.MaxCount;
			}
			else if (resultItemsCount < item.MaxCount)
			{
				containedItem.SetItem(item, resultItemsCount);
				return 0;
			}
		}

		if (item.Stackable)
		{ DebugLogging.SendWarning(gameObject: gameObject, $"Number of items that did not fit in {this} = {resultItemsCount} of item - {item.name}"); }
		
		return resultItemsCount;
	}

	public virtual int RemoveItem(Item item, int deleteItemsCount)
	{
		if (item == null)
		{
			DebugLogging.NullParameter<Item>(gameObject);
			return deleteItemsCount;
		}
		if (deleteItemsCount == 0)
		{
			DebugLogging.WrongParameter(gameObject, deleteItemsCount);
			return -1;
		}
		if (condition == Condition.Empty)
		{
			DebugLogging.SendError(gameObject:gameObject, $"Trying to remove items from empty slot. Slot id = {id}");
			return deleteItemsCount;
		}

		if (deleteItemsCount > containedItem.Count)
		{
			deleteItemsCount = containedItem.Count - deleteItemsCount;
			if (Math.Sign(deleteItemsCount) == -1)
			{ deleteItemsCount *= -1; }
			containedItem.ResetItem();
		}
		else
		{
			int tempItemsCount = containedItem.Count - deleteItemsCount;
			deleteItemsCount = 0;
			if (tempItemsCount == 0)
			{ containedItem.ResetItem(); }
			else
			{ containedItem.SetItem(item, tempItemsCount); }
		}

		return deleteItemsCount;
	}

	public virtual void Show()
	{
		imageComp.enabled = true;
		buttonComp.enabled = true;
	}

	public virtual void Hide()
	{
		imageComp.enabled = false;
		buttonComp.enabled = false;
	}

	public virtual void Initialize(int id)
	{
		containedItem.Initialize();
		SetSlotId(id);
		CheckSlotState();
	}

	private void OnEnable()
	{
		OnItemChangedCallback += CheckSlotState; 
	}

	private void OnDisable()
	{
		OnItemChangedCallback -= CheckSlotState;
	}
}
