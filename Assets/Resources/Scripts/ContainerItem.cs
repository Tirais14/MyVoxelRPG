using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContainerItem : MonoBehaviour, IContainerItem, ISwitchableGUI
{
	[SerializeField] private protected Image imageComp;
	[SerializeField] private GraphicItemCounter itemCounter;
	[SerializeField] private ContainerSlot ownerSlot;
	//[SerializeField] private DragAndDrop dragAndDropComp;
	//public DragAndDrop DragAndDropComp => dragAndDropComp;
	private protected Container ownerContainer;
	public ContainerSlot OwnerSlot => ownerSlot;
	private protected Item item;
	public Item Item => item;
	private protected int count;
	public int Count
	{
		get
		{ return count; }
		private set
		{
			if (value < 0)
			{ count = 0; }
			else if (value > item.MaxCount)
			{ count = item.MaxCount; }
			else
			{ count = value; }
		}
	}
	public GraphicItemCounter ItemCounter => itemCounter;
	private protected Color defaultColor;
	private protected Color transparentColor;


	public Action OnItemCountChangedCallback { get; private protected set; }


	public ContainerItemTemp GetContainerItemTemp() => new(item, count);

	/// <param name="replacedItem">Returns the replaced item if there is one</param>
	/// <returns>Returns true if it replaces an object</returns>
	public virtual bool SetItem(ContainerItemTemp containerItemTemp)
	{
		if (containerItemTemp.item == null)
		{
			DebugLogging.NullParameter<ContainerItemTemp>(gameObject: gameObject);
			return false;
		}
		if (containerItemTemp.count <= 0)
		{
			DebugLogging.WrongParameter(gameObject: gameObject, containerItemTemp.count);
			return false;
		}

		item = containerItemTemp.item;
		imageComp.sprite = item.Icon;
		imageComp.color = defaultColor;

		if (item.Stackable)
		{ Count = containerItemTemp.count; }
		else
		{ Count = 1; }

		OnItemCountChangedCallback?.Invoke();
		ownerSlot.OnItemChangedCallback?.Invoke();
		ownerContainer.AddToFilledSlots(ownerSlot.Id);
		ShowOnlyIfOpened();
		return true;
	}

	public virtual bool SetItem(Item item, in int itemsCount)
	{
		if (item == null)
		{
			DebugLogging.NullParameter<Item>(gameObject: gameObject);
			return false;
		}
		if (itemsCount <= 0)
		{
			DebugLogging.WrongParameter(gameObject:gameObject, itemsCount);
			return false;
		}

		this.item = item;
		imageComp.sprite = item.Icon;
		Count = itemsCount;
		imageComp.color = defaultColor;

		Debug.Log(OnItemCountChangedCallback);
		Debug.Log(ownerSlot.OnItemChangedCallback);
		OnItemCountChangedCallback?.Invoke();
		ownerSlot.OnItemChangedCallback?.Invoke();
		ownerContainer.AddToFilledSlots(ownerSlot.Id);
		ShowOnlyIfOpened();
		return true;
	}

	public virtual bool SetItemMax(Item item)
	{
		if (item == null)
		{
			DebugLogging.NullParameter<Item>(gameObject: gameObject);
			return false;
		}

		this.item = item;
		imageComp.sprite = item.Icon;
		imageComp.color = defaultColor;
		Count = item.MaxCount;

		Debug.Log(gameObject.name);
		Debug.Log(OnItemCountChangedCallback);
		Debug.Log(ownerSlot.OnItemChangedCallback);
		OnItemCountChangedCallback?.Invoke();
		ownerSlot.OnItemChangedCallback?.Invoke();
		ownerContainer.AddToFilledSlots(ownerSlot.Id);
		ShowOnlyIfOpened();
		return true;
	}

	public virtual void ResetItem()
	{
		Count = 0;
		item = null;
		imageComp.sprite = null;
		imageComp.color = transparentColor;
		OnItemCountChangedCallback?.Invoke();
		ownerSlot.OnItemChangedCallback?.Invoke();
		ownerContainer.RemoveFromFilledSlots(ownerSlot.Id);
		Hide();
	}

	public void EnableRaycastTarget() => imageComp.raycastTarget = true;

	public void DisableRaycastTarget() => imageComp.raycastTarget = false;

	public void EnableImage() => imageComp.enabled = true;

	public void DisableImage() => imageComp.enabled = false;


	public void ShowOnlyIfOpened()
	{
        if (ownerContainer.IsOpen)
		{ Show(); }
	}

	public virtual void Show()
	{
		if (ownerSlot.ConditionProperty != ContainerSlot.Condition.Empty && item.Stackable)
		{ itemCounter.gameObject.SetActive(true); }
	}

	public virtual void Hide()
	{
		itemCounter.gameObject.SetActive(false);
	}

	public void SetContainer(Container container) => this.ownerContainer = container;

	public virtual void Initialize()
	{
		imageComp = GetComponent<Image>();
		itemCounter = GetComponentInChildren<GraphicItemCounter>();
		ownerContainer = GetComponentInParent<Container>();
		ownerSlot = GetComponentInParent<ContainerSlot>();
		if (itemCounter != null)
		{ OnItemCountChangedCallback = itemCounter.Initialize(); }
		defaultColor = new Color(255f, 255f, 255f, 255f);
		transparentColor = new Color(255f, 255f, 255f, 0);
		if (TryGetComponent(out DragAndDrop dragAndDrop))
		{ dragAndDrop.Initialize(); }
	}
}