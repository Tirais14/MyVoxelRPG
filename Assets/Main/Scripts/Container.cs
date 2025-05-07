using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Container : MonoBehaviour, IContainer, ISwitchableGUI
{
	[SerializeField] private protected GameObject containerSlotPrefab;
	private protected GridLayoutGroup containerSlots;
	private protected ScrollRect scrollRectComp;
	private protected RectTransform contentOfScrollView;
	private protected Scrollbar scrollrbarVertical;

	[Header("Container Settings")]
	[SerializeField] [Range(0, 100)] private protected int slotsCount = -1;
	public int SlotsCount => slotsCount;
	[SerializeField] private protected string containerName = "******";
	public string ContainerName => containerName;
	[Tooltip("If checked allows to use scroll view in container, when slots count more than container size")]
	[SerializeField] private bool scrollViewEnabled = true;
	public bool ScrollViewEnabled => scrollViewEnabled;
	[Tooltip("If checked creates new slots, instead of finding exists")]
	[SerializeField] private bool initializeNewSlots = true;
	[Tooltip("If checked don't create lists what creates for simple container\nNeeds for containers like EquipmentUI")]
	[SerializeField] private bool specialContainer;
	public bool SpecialContainer => specialContainer;
	private protected bool isOpen = false;
	public bool IsOpen => isOpen;
	private int initializedSlotsCount;
	public int InitializedSlotsCount => initializedSlotsCount;
	private protected IContainerSlot[] slots;
	private IContainerSlot[] emptySlots;
	private IContainerSlot[] filledSlots;
	public Action OnInventoryStateChangedCallback { get; set; }
	public Action OnInitializedSlotCallback { get; private set; }
	

	private void InitializedSlotsIncreament() => initializedSlotsCount++;

	//private void InitializedSlotsDecreament() => initializedSlotsCount--;

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
			Logger.WrongParameter<int>();
			return -1;
		}

		int remainingAddItemsCount = addItemsCount;
		for (int i = 0; i < slotsCount; i++)
		{
			if (filledSlots[i] != null && filledSlots[i].Item == item)
			{
				remainingAddItemsCount = filledSlots[i].AddItem(item, remainingAddItemsCount);
				Logger.SendMessage(gameObject:gameObject, $"Item {item.name} added, count = {addItemsCount}");
			}

			if (remainingAddItemsCount <= 0)
			{ break; }
		}

		for (int i = 0; i < slotsCount; i++)
		{
			if (emptySlots[i] != null)
			{
				remainingAddItemsCount = emptySlots[i].AddItem(item, remainingAddItemsCount);
				Logger.SendMessage(gameObject: gameObject, $"Item {item.name} added, count = {addItemsCount}");
			}

			if (remainingAddItemsCount <= 0)
			{ break; }
		}

		return remainingAddItemsCount;
	}

	public virtual void RemoveItem(Item item, in int itemsCount)
	{
		if (TryGetSlotsWithItem(item, itemsCount, out List<IContainerSlot> foundSlots))
		{
			int tempItemsCount = itemsCount;
			for (int i = 0; i < foundSlots.Count; i++)
			{
				if (tempItemsCount == 0)
				{ return; }

				int debugValue = tempItemsCount;
				tempItemsCount = foundSlots[i].RemoveItem(item, tempItemsCount);
				Logger.SendMessage(gameObject: gameObject, $"Removed {debugValue - tempItemsCount} items from {foundSlots[i]} id = {foundSlots[i].Id}. Rest count to remove = {tempItemsCount}");
			}
		}
		else
		{ Logger.SendWarning(gameObject:gameObject, "Not enough items to remove");}
	}

	private bool TryGetSlotsWithItem(Item item, in int itemsCount, out List<IContainerSlot> foundSlots)
	{
		foundSlots = new List<IContainerSlot>(slotsCount);
		int foundItemsCount = 0;
		if (itemsCount == 0)
		{
			Logger.WrongParameter<int>();
			return false;
		}

		for (int i = 0; i < slotsCount; i++)
		{
			if (filledSlots[i] != null && filledSlots[i].Item == item)
			{

				if (filledSlots[i].Item.Stackable)
				{
					foundSlots.Add(filledSlots[i]);
					foundItemsCount += filledSlots[i].Count;
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

		Logger.SendMessage(gameObject: gameObject, $"Found items to remove {foundItemsCount}. Requierd = {itemsCount}");
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
			Logger.WrongParameter<int>();
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
			Logger.WrongParameter<int>();
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

	public void EnableScrollView()
	{
		scrollRectComp.enabled = true;
		scrollrbarVertical.enabled = true;
	}

	public void DisableScrollView()
	{
		scrollRectComp.enabled = false;
		scrollrbarVertical.enabled = false;
	}

	//public virtual void Show()
	//{
	//	isOpen = true;
	//	ContainerGUI.gameObject.SetActive(true);
	//	if (scrollView != null)
	//	{ scrollView.gameObject.SetActive(true); }
	//	for (int i = 0; i < slotsCount; i++)
	//	{
	//		slots[i].Show();
	//	}
	//}
	public virtual void Show()
	{
		gameObject.SetActive(true);
		isOpen = true;
	}

	//public virtual void Hide()
	//{
	//	isOpen = false;
	//	ContainerGUI.gameObject.SetActive(false);
	//	if (scrollView != null)
	//	{ scrollView.gameObject.SetActive(false); }
	//	for (int i = 0; i < slotsCount; i++)
	//	{
	//		slots[i].Hide();
	//	}
	//}
	public virtual void Hide()
	{
		gameObject.SetActive(false);
		isOpen = false;
	}

	public virtual void SwitchState(InputAction.CallbackContext context)
	{
		if (isOpen)
		{ Hide(); }
		else
		{ Show(); }
	}

	private protected void ChangeContentSizeOfScrollView()
	{
		if (containerSlots.startAxis == GridLayoutGroup.Axis.Horizontal)
		{
			float verticalSlotsCount = Mathf.Ceil(slotsCount / containerSlots.constraintCount);
			Rect containerSlotPrefabSize = containerSlotPrefab.GetComponent<RectTransform>().rect;
			contentOfScrollView.sizeDelta = new Vector2(contentOfScrollView.rect.width, containerSlotPrefabSize.height * verticalSlotsCount + 4);
		}
	}

	private protected virtual void InitializeNewSlots()
	{
		slots = new ContainerSlot[slotsCount];

		var defaultScale = new Vector3(1f, 1f, 1f);
		for (int i = 0; i < slotsCount; i++)
		{
			GameObject newContainerSlotObj = Instantiate(containerSlotPrefab);
			
			newContainerSlotObj.transform.SetParent(containerSlots.transform);
			newContainerSlotObj.transform.localScale = defaultScale;
			ContainerSlot newContainerSlot = newContainerSlotObj.GetComponent<ContainerSlot>();
			slots[i] = newContainerSlot;
			newContainerSlot.Initialize();
			newContainerSlotObj.name = containerName + $"Slot_{newContainerSlot.Id}";
		}
	}

	private protected virtual void InitializeExistsSlots()
	{
		ContainerSlot[] existContainerSlots = GetComponentsInChildren<ContainerSlot>();
		slotsCount = existContainerSlots.Length;
		slots = new ContainerSlot[slotsCount];

		for (int i = 0; i < slotsCount; i++)
		{
			slots[i] = existContainerSlots[i];
			existContainerSlots[i].Initialize();
		}
	}

	private protected virtual void InitializeEmptySlots()
	{
		emptySlots = new IContainerSlot[slotsCount];
		for (int i = 0; i < slotsCount; i++)
		{
			if (slots[i].ConditionProperty == IContainerSlot.Condition.Empty)
			{ emptySlots[i] = slots[i]; }
		}
	}

	private protected virtual void InitializeFilledSlots()
	{
		filledSlots = new IContainerSlot[slotsCount];
		for (int i = 0; i < slotsCount; i++)
		{
			if (slots[i].ConditionProperty != IContainerSlot.Condition.Empty)
			{ filledSlots[i] = slots[i]; }
		}
	}

	public virtual void Initialize()
	{
		string containerSlotsName = '$' + containerName + "Slots$";
		if (scrollViewEnabled)
		{
			gameObject.GetComponentsByObjNamesInChildren(containerSlotsName,
														"$Content$",
														"$Scroll View$",
														"$Scrollbar Vertical$",
														out containerSlots,
														out contentOfScrollView,
														out scrollRectComp,
														out scrollrbarVertical);
			ChangeContentSizeOfScrollView();
		}
		else
		{ containerSlots = gameObject.GetComponentByObjNameInChildren<GridLayoutGroup>(containerSlotsName); }

		if (!initializeNewSlots)
		{ InitializeExistsSlots(); }
		else
		{ InitializeNewSlots(); }

		if (!specialContainer)
		{
			InitializeEmptySlots();
			InitializeFilledSlots();
		}
		
		Hide();
	}

	private void OnEnable() => OnInitializedSlotCallback += InitializedSlotsIncreament;

	private void OnDisable() => OnInitializedSlotCallback -= InitializedSlotsIncreament;
}
