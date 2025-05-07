using System;
using UnityEngine;
using UnityEngine.UI;

public class ContainerSlot : MonoBehaviour, IContainerSlot
{
	private int id;
	public int Id => id;
	public Item Item { get { return containedItem.Item; } }
	public int Count { get { return containedItem.Count; } }
	private protected ContainerItem containedItem;
	private protected IContainer ownerContainer;
	public IContainer OwnerContainer => ownerContainer;
	private protected IContainerSlot.Condition condition;
	public virtual IContainerSlot.Condition ConditionProperty => condition;
	private protected Action onItemChangedCallback;
	public Action OnItemChangedCallback => onItemChangedCallback;
	[SerializeField] private bool isContextMenuEnabled;
	public bool ContextMenuEnabled => isContextMenuEnabled;


	public ContainerItemTemp GetContainerItemTemp() => new ContainerItemTemp(Item, Count);

	private protected virtual void CheckSlotState()
	{
		if (containedItem.Item != null)
		{
			if (containedItem.Count < containedItem.Item.MaxCount)
			{ condition = IContainerSlot.Condition.Filled; }
			else if (containedItem.Count == containedItem.Item.MaxCount)
			{ condition = IContainerSlot.Condition.FilledMax; }
		}
		else
		{ condition = IContainerSlot.Condition.Empty; }
    }

	private void SetSlotId() => id = ownerContainer.InitializedSlotsCount + 1;

	public virtual int AddItem(Item item, in int addItemsCount)
	{
		if (item == null)
		{
			Logger.NullParameter<Item>(gameObject);
			return addItemsCount;
		}
		if (addItemsCount == 0)
		{ return 0; }
		if (addItemsCount < 0)
		{
			Logger.WrongParameter<GameObject>();
			return -1;
		}

		int resultItemsCount = containedItem.Count + addItemsCount;
		if (resultItemsCount == 0)
		{ return 0; }

		if ((condition == IContainerSlot.Condition.Filled && containedItem.Item == item) || condition == IContainerSlot.Condition.Empty)
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
		{ Logger.SendWarning(gameObject: gameObject, $"Number of items that did not fit in {this} = {resultItemsCount} of item - {item.name}"); }
		
		return resultItemsCount;
	}

	public virtual int RemoveItem(Item item, int deleteItemsCount)
	{
		if (item == null)
		{
			Logger.NullParameter<Item>(gameObject);
			return deleteItemsCount;
		}
		if (deleteItemsCount == 0)
		{
			Logger.WrongParameter<GameObject>();
			return -1;
		}
		if (condition == IContainerSlot.Condition.Empty)
		{
			Logger.SendError(gameObject:gameObject, $"Trying to remove items from empty slot. Slot id = {id}");
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

	public virtual void Initialize()
	{
		ownerContainer = GetComponentInParent<Container>();
		SetSlotId();
		ownerContainer.OnInitializedSlotCallback?.Invoke();

		containedItem = GetComponentInChildren<ContainerItem>();
		containedItem.Initialize();
		CheckSlotState();
	}

	private void OnEnable() => onItemChangedCallback += CheckSlotState;

	private void OnDisable() => onItemChangedCallback -= CheckSlotState;
}
