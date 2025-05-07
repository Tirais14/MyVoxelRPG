using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static ContainerSlot;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler
{
	public static bool IsDragging { get; private set; }
	public static BufferContainerItem DraggingItem { get; private set; }
	public static Vector3 DraggingItemPointerPositionOffset { get; private set; }
	private ContainerSlot ownerSlot;
	private ContainerItem draggableItem;
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
	public static void DraggingCheck() => IsDragging = GameManagers.Inventory.IsOpen && DraggingItem.IsActive;

	/// <returns>True if Container Slot is empty</returns>
	private bool OwnerSlotEmptyCheck() => ownerSlot.ConditionProperty == Condition.Empty;

	/// <returns>True if Container Slot filled max</returns>
	private bool OwnerSlotFilledMaxCheck() => ownerSlot.ConditionProperty == Condition.FilledMax;

	/// <returns>True if Dragging Item Max Count reached</returns>
	private bool DraggingItemMaxCountCheck() => DraggingItem.Count >= DraggingItem.Item.MaxCount;

	/// <returns>True if dragging item and container item equals</returns>
	private bool ItemsEqualsCheck()
	{
		return
			DraggingItem.Item != null &&
			draggableItem.Item != null &&
			DraggingItem.Item == draggableItem.Item;
	}


	/// <returns>True if dragging item and container item stackable</returns>
	private bool ItemsStackableCheck()
	{
		return
			IsDragging && ItemsEqualsCheck() && DraggingItem.Item.Stackable &&
			draggableItem.Item.Stackable && !OwnerSlotEmptyCheck() && !OwnerSlotFilledMaxCheck() && !DraggingItemMaxCountCheck();
	}

	/// <returns>True if the item can be taken</returns>
	private bool ItemCanBeTakenCheck() => !IsDragging && !OwnerSlotEmptyCheck() || ItemsStackableCheck();

	/// <returns>True if dragging item may be dropped in clicked slot</returns>
	private bool DraggingItemCanBeDropped() => IsDragging && !OwnerSlotFilledMaxCheck();

	private int GetHalfOfStack()
	{
		const sbyte divider = 2;
		int itemsCountToDrag = Math.DivRem(draggableItem.Count, divider, out int remainder);
		return itemsCountToDrag + remainder;
	}

	private void TakeContainerItem(in int countToDrag)
	{
		if (!ItemCanBeTakenCheck())
		{
			DebugLogging.SendWarning(gameObject:gameObject, "Items can't be taken");
			return;
		}
		DebugLogging.SendMessage(gameObject: gameObject, $"Item - {draggableItem.Item}, items count = {draggableItem.Count}");

		if (!DraggingItem.IsActive)
		{
			DraggingItem.transform.SetParent(GameManagers.Inventory.transform.parent, true);
			DraggingItem.transform.SetAsLastSibling();
		}
		
		int remainingItemsCount = DraggingItem.AddItem(draggableItem.Item, countToDrag);
		int removeItemsCount = countToDrag - remainingItemsCount;
		draggableItem.OwnerSlot.RemoveItem(draggableItem.Item, removeItemsCount);
	}

	private void DropContainerItemToContainerSlot(in int countToDrop)
	{
		if (!ItemsStackableCheck() && !OwnerSlotEmptyCheck())
		{
			ChangeDraggingItem();
			return;
		}
		DebugLogging.SendMessage(gameObject: gameObject, $"Item - {DraggingItem.Item}, items count = {DraggingItem.Count}");

		int remainingItemsCount = draggableItem.OwnerSlot.AddItem(DraggingItem.Item, countToDrop);
		int removeItemsCount = countToDrop - remainingItemsCount; 
		DraggingItem.RemoveItem(DraggingItem.Item, removeItemsCount);

		if (DraggingItem.Count == 0)
		{ EndDragging(); }
	}

	public static void EndDragging()
	{
		DebugLogging.SendMessage("End Dragging");

		DraggingItem.ResetItem();
		DraggingItem.transform.SetParent(GameManagers.Inventory.transform, true);
		DraggingItem.transform.SetAsLastSibling();
	}

	public static void ReturnToInventoryRemainderOfTakenContainerItem()
	{
		DebugLogging.SendMessage($"Item - {DraggingItem.Item}, items count = {DraggingItem.Count}");

		int remainingItemsCount = GameManagers.Inventory.AddItem(DraggingItem.Item, DraggingItem.Count);

		if (remainingItemsCount == 0)
		{ EndDragging(); }
		else
		{ DropItemToWorld(DraggingItem); }
	}

	private void ChangeDraggingItem()
	{
		DebugLogging.SendMessage(gameObject: gameObject, $"Changing dragging item from {DraggingItem.Item.Name} to {draggableItem.Item.Name}");

		ContainerItemTemp containerItemTemp = draggableItem.GetContainerItemTemp();
		ownerSlot.RemoveItem(draggableItem.Item, draggableItem.Count);
		ownerSlot.AddItem(DraggingItem.Item, DraggingItem.Count);
		DraggingItem.RemoveItem(DraggingItem.Item, DraggingItem.Count);
		DraggingItem.AddItem(containerItemTemp.item, containerItemTemp.count);
	}

	public static void DropItemToWorld(ContainerSlot containerSlot, in int itemsCountToDrop)
	{
		if (containerSlot == null)
		{
			DebugLogging.NullParameter<Item>();
			return;
		}
		if (containerSlot.ConditionProperty == Condition.Empty)
		{
			DebugLogging.SendError($"Attempt to drop item from empty slot {containerSlot.gameObject.name}");
			return;
		}
		DebugLogging.SendMessage($"Dropepd item - {containerSlot.ContainedItem.Item.name}, items count = {itemsCountToDrop}");

		Instantiate(containerSlot.ContainedItem.Item.Prefab, GameManagers.Player.transform.position + GameManagers.Player.transform.forward * 10f, containerSlot.ContainedItem.Item.Prefab.transform.rotation);
		containerSlot.RemoveItem(containerSlot.ContainedItem.Item, itemsCountToDrop);
	}

	public static void DropItemToWorld(BufferContainerItem bufferContainerItem)
	{
		if (bufferContainerItem == null)
		{
			DebugLogging.NullParameter<Item>();
			return;
		}
		DebugLogging.SendMessage($"Dropepd dragging item - {bufferContainerItem.Item.name}, items count = {bufferContainerItem.Count}");

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
			else if (scrollValue == 1)
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
				TakeContainerItem(draggableItem.Count);
				break;
			case Operation.TakeHalf:
				TakeContainerItem(GetHalfOfStack());
				break;
			case Operation.DropOne:
				DropContainerItemToContainerSlot(1);
				break;
			case Operation.DropAll:
				DropContainerItemToContainerSlot(DraggingItem.Count);
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
		if (DraggingItem == null)
		{ DraggingItem = GameManagers.Inventory.BufferContainerItem; }
		if (DraggingItemPointerPositionOffset == Vector3.zero)
		{ DraggingItemPointerPositionOffset = new Vector3(15f, 0f); }
		ownerSlot = GetComponentInParent<ContainerSlot>();
		draggableItem = ownerSlot.ContainedItem;
	}
}
