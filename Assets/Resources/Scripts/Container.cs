using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static DebugLogging;

public class Container : MonoBehaviour, ISwitchableGUI
{
	[Header("Components")]
	private protected ScrollRect scrollRectComp;
	[SerializeField] private protected Image ContainerGUI;
	[SerializeField] private protected GameObject containerSlotPrefab;
	[SerializeField] private protected RectTransform containerSlots;
	[SerializeField] private RectTransform contentOfScrollView;
	[SerializeField] private GridLayoutGroup containerSlotsGrid;
	[SerializeField] private Image scrollView;

	[Header("Container Settings")]
	[SerializeField] [Range(0, 100)] private protected int slotsCount = -1;
	[SerializeField] private protected string containerName = "Container";
	private protected bool isOpen = false;
	private protected ContainerSlot[] slots;
	private ContainerSlot[] emptySlots;
	private ContainerSlot[] filledSlots;
	public int SlotsCount => slotsCount;
	public bool IsOpen => isOpen;
	public Action OnInventoryStateChangedCallback { get; set; }

	private protected void EnableMouseScrollView()
	{
		scrollRectComp.enabled = true;
	}

	private protected void DisableMouseScrollView()
	{
		scrollRectComp.enabled = false;
	}

	public virtual int AddItem(Item item, in int addItemsCount)
	{
		if (item == null)
		{
			NullParameter<Item>(gameObject);
			return addItemsCount;
		}
		if (addItemsCount == 0)
		{ return 0; }
		if (addItemsCount < 0)
		{
			WrongParameter(gameObject, addItemsCount);
			return -1;
		}

		int remainingAddItemsCount = addItemsCount;
		for (int i = 0; i < slotsCount; i++)
		{
			if (filledSlots[i] != null && filledSlots[i].ContainedItem.Item == item)
			{
				remainingAddItemsCount = filledSlots[i].AddItem(item, remainingAddItemsCount);
				DebugLogging.SendMessage(gameObject:gameObject, $"Item {item.name} added, count = {addItemsCount}");
			}

			if (remainingAddItemsCount <= 0)
			{ break; }
		}

		for (int i = 0; i < slotsCount; i++)
		{
			if (emptySlots[i] != null)
			{
				remainingAddItemsCount = emptySlots[i].AddItem(item, remainingAddItemsCount);
				DebugLogging.SendMessage(gameObject: gameObject, $"Item {item.name} added, count = {addItemsCount}");
			}

			if (remainingAddItemsCount <= 0)
			{ break; }
		}

		return remainingAddItemsCount;
	}

	public virtual void RemoveItem(Item item, in int itemsCount)
	{
		if (TryGetSlotsWithItem(item, itemsCount, out List<ContainerSlot> foundSlots))
		{
			int tempItemsCount = itemsCount;
			for (int i = 0; i < foundSlots.Count; i++)
			{
				if (tempItemsCount == 0)
				{ return; }

				int debugValue = tempItemsCount;
				tempItemsCount = foundSlots[i].RemoveItem(item, tempItemsCount);
				DebugLogging.SendMessage(gameObject: gameObject, $"Removed {debugValue - tempItemsCount} items from {foundSlots[i]} id = {foundSlots[i].Id}. Rest count to remove = {tempItemsCount}");
			}
		}
		else
		{ SendWarning(gameObject:gameObject, "Not enough items to remove");}
	}

	private bool TryGetSlotsWithItem(Item item, in int itemsCount, out List<ContainerSlot> foundSlots)
	{
		foundSlots = new List<ContainerSlot>(slotsCount);
		int foundItemsCount = 0;
		if (itemsCount == 0)
		{
			DebugLogging.WrongParameter(gameObject:gameObject, itemsCount);
			return false;
		}

		for (int i = 0; i < slotsCount; i++)
		{
			if (filledSlots[i] != null && filledSlots[i].ContainedItem.Item == item)
			{

				if (filledSlots[i].ContainedItem.Item.Stackable)
				{
					foundSlots.Add(filledSlots[i]);
					foundItemsCount += filledSlots[i].ContainedItem.Count;
				}
				else
				{
					foundSlots.Add(filledSlots[i]);
					foundItemsCount++;
				}

				if (foundItemsCount >= itemsCount)
				{ return true; }
			}
		}

		DebugLogging.SendMessage(gameObject: gameObject, $"Found items to remove {foundItemsCount}. Requierd = {itemsCount}");
		return false;
	}

	//private bool TryGetContainsItems(Item item, out int containsCount, out List<ContainerSlot> foundItemSlots)
	//{
	//	containsCount = 0;
	//	foundItemSlots = null;
	//	for (int i = 0; i < filledSlots.Length; i++)
	//	{
	//		foundItemSlots ??= new List<ContainerSlot>(slotsCount);
	//		if (filledSlots[i].ContainedItem.Item == item)
	//		{
	//			containsCount += filledSlots[i].ContainedItem.Count;
	//			foundItemSlots.Add(filledSlots[i]);
	//		}
	//	}

	//	Debug.Log(containsCount);
	//	if (containsCount > 0)
	//	{ return true; }

	//	return false;
	//}

	public virtual void AddToFilledSlots(in int containerSlotId)
	{
		if (containerSlotId > slotsCount || containerSlotId <= 0)
		{
			DebugLogging.WrongParameter(gameObject, containerSlotId);
			return;
		}

		for (int i = 0; i < slotsCount; i++)
		{
			if (slots[i].Id == containerSlotId)
			{
				filledSlots[i] = slots[i];
				emptySlots[i] = null;
			}
		}
	}

	public virtual void RemoveFromFilledSlots(in int containerSlotId)
	{
		if (containerSlotId > slotsCount || containerSlotId <= 0)
		{
			WrongParameter(gameObject, containerSlotId);
			return;
		}

		for (int i = 0; i < slotsCount; i++)
		{
			if (slots[i].Id == containerSlotId)
			{
				emptySlots[i] = slots[i];
				filledSlots[i] = null;
			}
		}
	}

	public virtual void Show()
	{
		isOpen = true;
		ContainerGUI.gameObject.SetActive(true);
		if (scrollView != null)
		{ scrollView.gameObject.SetActive(true); }
		for (int i = 0; i < slotsCount; i++)
		{
			slots[i].Show();
			slots[i].ContainedItem.EnableImage();
			slots[i].ContainedItem.Show();
		}
	}

	public virtual void Hide()
	{
		isOpen = false;
		ContainerGUI.gameObject.SetActive(false);
		if (scrollView != null)
		{ scrollView.gameObject.SetActive(false); }
		for (int i = 0; i < slotsCount; i++)
		{
			slots[i].Hide();
			slots[i].ContainedItem.Hide();
			slots[i].ContainedItem.DisableImage();
		}
	}

	public virtual void SwitchState(InputAction.CallbackContext context)
	{
		if (isOpen)
		{ Hide(); }
		else
		{ Show(); }
	}

	private void ChangeContentSizeOfScrollView()
	{
		if (containerSlotsGrid.startAxis == GridLayoutGroup.Axis.Horizontal)
		{
			float verticalSlotsCount = Mathf.Ceil(slotsCount / containerSlotsGrid.constraintCount);
			Rect containerSlotPrefabSize = containerSlotPrefab.GetComponent<RectTransform>().rect;
			contentOfScrollView.sizeDelta = new Vector2(contentOfScrollView.rect.width, containerSlotPrefabSize.height * verticalSlotsCount + 4);
		}
	}

	private protected virtual void InitializeNewSlots()
	{
		const int idOffset = 1;
		slots = new ContainerSlot[slotsCount];

		var defaultScale = new Vector3(1f, 1f, 1f);
		for (int i = 0; i < slotsCount; i++)
		{
			GameObject newContainerSlotObj = Instantiate(containerSlotPrefab);
			newContainerSlotObj.name = containerName + $"Slot_{i + idOffset}";
			newContainerSlotObj.transform.SetParent(containerSlots.transform);
			newContainerSlotObj.transform.localScale = defaultScale;
			ContainerSlot newContainerSlot = newContainerSlotObj.GetComponent<ContainerSlot>();
			slots[i] = newContainerSlot;
			newContainerSlot.Initialize(i + idOffset);
			newContainerSlot.ContainedItem.SetContainer(this);
		}
	}

	private protected virtual void InitializeExistsSlots()
	{
		ContainerSlot[] existContainerSlots = GetComponentsInChildren<ContainerSlot>();
		slotsCount = existContainerSlots.Length;
		slots = new ContainerSlot[slotsCount];
		const int idOffset = 1;

		for (int i = 0; i < slotsCount; i++)
		{
			slots[i] = existContainerSlots[i];
			existContainerSlots[i].Initialize(i + idOffset);
			existContainerSlots[i].ContainedItem.SetContainer(this);
		}
	}

	private protected virtual void InitializeEmptySlots()
	{
		emptySlots = new ContainerSlot[slotsCount];
		for (int i = 0; i < slotsCount; i++)
		{
			if (slots[i].ConditionProperty == ContainerSlot.Condition.Empty)
			{ emptySlots[i] = slots[i]; }
		}
	}

	private protected virtual void InitializeFilledSlots()
	{
		filledSlots = new ContainerSlot[slotsCount];
		for (int i = 0; i < slotsCount; i++)
		{
			if (slots[i].ConditionProperty != ContainerSlot.Condition.Empty)
			{ filledSlots[i] = slots[i]; }
		}
	}

	public virtual void Initialize()
	{
		if (GetComponentInChildren<ContainerSlot>() != null)
		{ InitializeExistsSlots(); }
		else if (GetComponentInChildren<ContainerSlot>() == null)
		{ InitializeNewSlots(); }
		if (contentOfScrollView != null && containerSlotsGrid != null)
		{ ChangeContentSizeOfScrollView(); }

		if (Utils.TryFindInChildrens(transform, "Scroll View", out Transform foundObject))
		{ scrollRectComp = foundObject.GetComponent<ScrollRect>(); }
		InitializeEmptySlots();
		InitializeFilledSlots();
		Hide();
	}

	private void Update()
	{
		if (scrollRectComp != null)
		{
			if (GameManagers.Controlls.DisableScrollingInView.IsPressed())
			{ DisableMouseScrollView(); }
			else
			{ EnableMouseScrollView(); }
		}
	}
}
