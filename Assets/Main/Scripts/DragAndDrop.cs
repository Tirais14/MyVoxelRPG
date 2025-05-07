using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler
{
	public static bool IsDragging { get; private set; }
	private static IDraggingItem draggingItem;
	private IContainerSlot ownerSlot;
	public static Vector3 DraggingItemPointerPositionOffset { get; private set; }
	private enum Operation : byte
	{
		TakeOne,
		TakeAll,
		TakeHalf,
		DropOne,
		DropAll,
		DropSwap,
		DropAndDragNew,
		Nothing
	}

	/// <summary>Checks condition of dragging</summary>
	public static void DraggingCheck() => IsDragging = GameManagers.Inventory.IsOpen && draggingItem.IsActive;

	/// <returns>True if Container Slot is empty</returns>
	private bool OwnerSlotEmptyCheck() => ownerSlot.ConditionProperty == IContainerSlot.Condition.Empty;

	/// <returns>True if Container Slot filled max</returns>
	private bool OwnerSlotFilledMaxCheck() => ownerSlot.ConditionProperty == IContainerSlot.Condition.FilledMax;

	/// <returns>True if Dragging Item Max Count reached</returns>
	private bool DraggingItemMaxCountCheck() => draggingItem.Count >= draggingItem.Item.MaxCount;

	/// <returns>True if dragging item and container item equals</returns>
	private bool ItemsEqualsCheck()
	{
		return
			draggingItem.Item != null &&
			ownerSlot.Item != null &&
			draggingItem.Item == ownerSlot.Item;
	}

	/// <returns>True if dragging item and container item stackable</returns>
	private bool ItemsStackableCheck()
	{
		return
			IsDragging && ItemsEqualsCheck() && draggingItem.Item.Stackable &&
			ownerSlot.Item.Stackable && !OwnerSlotEmptyCheck() && !OwnerSlotFilledMaxCheck() && !DraggingItemMaxCountCheck();
	}

	/// <returns>True if the item can be taken</returns>
	private bool ItemCanBeTakenCheck() => !IsDragging && !OwnerSlotEmptyCheck() || ItemsStackableCheck();

	/// <returns>True if dragging item may be dropped in clicked slot</returns>
	private bool DraggingItemCanBeDropped() => IsDragging && !OwnerSlotFilledMaxCheck();

	private int GetHalfOfStack()
	{
		const sbyte divider = 2;
		int itemsCountToDrag = Math.DivRem(ownerSlot.Count, divider, out int remainder);
		return itemsCountToDrag + remainder;
	}

	private void TakeContainerItem(in int countToDrag)
	{
		if (!ItemCanBeTakenCheck())
		{
			Logger.SendWarning(gameObject:gameObject, "Items can't be taken");
			return;
		}
		Logger.SendMessage(gameObject: gameObject, $"Item - {ownerSlot.Item}, items count = {ownerSlot.Count}");

		
		int remainingItemsCount = draggingItem.AddItem(ownerSlot.Item, countToDrag);
		int removeItemsCount = countToDrag - remainingItemsCount;
		ownerSlot.RemoveItem(ownerSlot.Item, removeItemsCount);
	}

	private void DropContainerItemToContainerSlot(in int countToDrop)
	{
		if (!ItemsStackableCheck() && !OwnerSlotEmptyCheck())
		{
			ChangeDraggingItem();
			return;
		}
		Logger.SendMessage(gameObject: gameObject, $"Item - {draggingItem.Item}, items count = {draggingItem.Count}");

		int remainingItemsCount = ownerSlot.AddItem(draggingItem.Item, countToDrop);
		int removeItemsCount = countToDrop - remainingItemsCount; 
		draggingItem.RemoveItem(draggingItem.Item, removeItemsCount);

		if (draggingItem.Count == 0)
		{ EndDragging(); }
	}

	public static void EndDragging()
	{
		//DebugLogging.SendMessage("End Dragging");

		//DraggingItem.RemoveItem();
	}

	public static void ReturnToInventoryRemainderOfTakenContainerItem()
	{
		Logger.SendMessage($"Item - {draggingItem.Item}, items count = {draggingItem.Count}");

		int remainingItemsCount = GameManagers.Inventory.AddItem(draggingItem.Item, draggingItem.Count);

		if (remainingItemsCount == 0)
		{ EndDragging(); }
		else
		{ DropItemToWorld(draggingItem); }
	}

	private void ChangeDraggingItem()
	{
		Logger.SendMessage(gameObject: gameObject, $"Changing dragging item from {draggingItem.Item.Name} to {ownerSlot.Item.Name}");

		ContainerItemTemp containerItemTemp = ownerSlot.GetContainerItemTemp();
		ownerSlot.RemoveItem(ownerSlot.Item, ownerSlot.Count);
		ownerSlot.AddItem(draggingItem.Item, draggingItem.Count);
		draggingItem.RemoveItem(draggingItem.Item, draggingItem.Count);
		draggingItem.AddItem(containerItemTemp.item, containerItemTemp.count);
	}

	public static void DropItemToWorld(ContainerSlot containerSlot, in int itemsCountToDrop)
	{
		if (containerSlot == null)
		{
			Logger.NullParameter<Item>();
			return;
		}
		if (containerSlot.ConditionProperty == IContainerSlot.Condition.Empty)
		{
			Logger.SendError($"Attempt to drop item from empty slot {containerSlot.gameObject.name}");
			return;
		}
		Logger.SendMessage($"Dropepd item - {containerSlot.Item.name}, items count = {itemsCountToDrop}");

		Instantiate(containerSlot.Item.Prefab, GameManagers.Player.transform.position + GameManagers.Player.transform.forward * 10f, containerSlot.Item.Prefab.transform.rotation);
		containerSlot.RemoveItem(containerSlot.Item, itemsCountToDrop);
	}

	public static void DropItemToWorld(IDraggingItem bufferContainerItem)
	{
		if (bufferContainerItem == null)
		{
			Logger.NullParameter<Item>();
			return;
		}
		Logger.SendMessage($"Dropepd dragging item - {bufferContainerItem.Item.name}, items count = {bufferContainerItem.Count}");

		Instantiate(bufferContainerItem.Item.Prefab, GameManagers.Player.transform.position + GameManagers.Player.transform.forward * 10f, bufferContainerItem.Item.Prefab.transform.rotation);
		bufferContainerItem.RemoveItem(bufferContainerItem.Item, bufferContainerItem.Count);
	}

	public void MouseScrollingAction(in float scrollValue) => DoDrag(scrollValue);

	//private Operation GetOperationType(in float scrollValue = 0)
	//{
	//	bool leftButtonClicked = GameManagers.Controlls.LeftClick.IsPressed();
	//	bool halfStackOfItemsButtonClicked = GameManagers.Controlls.GetHalfStackOfItems.IsPressed();

	//	if (scrollValue != 0)
	//	{
	//		if (scrollValue == -1)
	//		{ return Operation.TakeOne; }
	//		else if (scrollValue == 1)
	//		{ return Operation.DropOne; }
	//	}
	//    else if (!IsDragging)
	//	{
	//		if (halfStackOfItemsButtonClicked)
	//		{ return Operation.TakeHalf; }
	//		else if (leftButtonClicked)
	//		{ return Operation.TakeAll; }
	//	}
	//	else
	//	{
	//		if (leftButtonClicked)
	//		{ return Operation.DropAll; }
	//		else if (leftButtonClicked && !ItemsStackableCheck())
	//		{ return Operation.DropAndDragNew; }
	//	}

	//	return Operation.Nothing;
	//}
	private Operation GetOperationType(in float scrollValue = 0)
	{
		bool leftButtonClicked = GameManagers.Controlls.LeftClick.IsPressed();
		bool halfStackOfItemsButtonClicked = GameManagers.Controlls.GetHalfStackOfItems.IsPressed();

		if (scrollValue != 0)
		{
			if (scrollValue == -1)
			{ return Operation.TakeOne; }
			else if (IsDragging && scrollValue == 1)
			{ return Operation.DropOne; }
		}
		else if (!IsDragging)
		{
			if (halfStackOfItemsButtonClicked)
			{ return Operation.TakeHalf; }
			else if (leftButtonClicked)
			{ return Operation.TakeAll; }
		}
		else
		{
			if (leftButtonClicked)
			{ return Operation.DropAll; }
		}

		return Operation.Nothing;
	}

	private void DoDrag(in float scrollValue = 0)
	{
		switch (GetOperationType(scrollValue))
		{
			case Operation.TakeOne:
				TakeContainerItem(1);
				break;
			case Operation.TakeAll:
				TakeContainerItem(ownerSlot.Count);
				break;
			case Operation.TakeHalf:
				TakeContainerItem(GetHalfOfStack());
				break;
			case Operation.DropOne:
				DropContainerItemToContainerSlot(1);
				break;
			case Operation.DropAll:
				DropContainerItemToContainerSlot(draggingItem.Count);
				break;
			case Operation.DropAndDragNew:
				ChangeDraggingItem();
				break;
			case Operation.Nothing:
				break;
		}
	}

	public void OnPointerDown(PointerEventData eventData) => DoDrag();

	public void Initialize()
	{
		draggingItem ??= GameManagers.DraggingItemUnsafe;
		if (DraggingItemPointerPositionOffset == Vector3.zero)
		{ DraggingItemPointerPositionOffset = new Vector3(15f, 0f); }
		ownerSlot = GetComponentInParent<IContainerSlot>();
	}
}
