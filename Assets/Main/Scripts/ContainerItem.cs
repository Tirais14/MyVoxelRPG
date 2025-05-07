using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerItem : MonoBehaviour
{
	private protected Item item;
	public Item Item => item;
	private protected int count;
	public int Count => count;
	private protected Image imageComp;
	private IContainerSlot ownerSlot;
	public IContainerSlot OwnerSlot => ownerSlot;
	private TextMeshProUGUI itemCounter;
	private protected Color defaultColor;
	private protected Color transparentColor;
	[SerializeField] private GameObject itemCounterPrefab;
	[SerializeField] private bool itemCounterEnabled = true;
	[SerializeField] private bool dragAndDropEnabled = true;


	/// <param name="replacedItem">Returns the replaced item if there is one</param>
	/// <returns>Returns true if it replaces an object</returns>
	public virtual bool SetItem(Item item, in int itemsCount)
	{
		if (item == null)
		{
			Logger.NullParameter<Item>(gameObject: gameObject);
			return false;
		}
		if (itemsCount <= 0)
		{
			Logger.WrongParameter<GameObject>();
			return false;
		}

		this.item = item;
		imageComp.sprite = item.Icon;
		imageComp.color = defaultColor;
		count = itemsCount;

		if (itemCounterEnabled)
		{ UpdateItemCounter(); }
		if (item.Stackable)
		{ itemCounter.enabled = true; }
		ownerSlot.OnItemChangedCallback?.Invoke();
		if (!ownerSlot.OwnerContainer.SpecialContainer)
		{ ownerSlot.OwnerContainer.AddToFilledSlots(ownerSlot.Id); }
		return true;
	}
	public virtual bool SetItem(ContainerItemTemp containerItemTemp)
	{
		if (containerItemTemp == ContainerItemTemp.Empty)
		{
			Logger.SendError(gameObject: gameObject, "Container item temp is empty");
			return false;
		}

		return SetItem(containerItemTemp.item, containerItemTemp.count);
	}

	public virtual bool SetItemMax(Item item)
	{
		if (item == null)
		{
			Logger.NullParameter<Item>(gameObject: gameObject);
			return false;
		}

		return SetItem(item, item.MaxCount);
	}

	public virtual void ResetItem()
	{
		item = null;
		imageComp.sprite = null;
		imageComp.color = transparentColor;
		count = 0;

		if (itemCounterEnabled)
		{ 
			UpdateItemCounter();
			if (itemCounter.enabled)
			{ itemCounter.enabled = false; }
		}

		ownerSlot.OnItemChangedCallback?.Invoke();
		if (!ownerSlot.OwnerContainer.SpecialContainer)
		{ ownerSlot.OwnerContainer.RemoveFromFilledSlots(ownerSlot.Id); }
	}

	private void UpdateItemCounter() => itemCounter.text = Count.ToString();

	public virtual void Initialize()
	{
		imageComp = GetComponent<Image>();
		ownerSlot = GetComponentInParent<ContainerSlot>();
		itemCounter = GetComponentInChildren<TextMeshProUGUI>();
		defaultColor = new Color(255f, 255f, 255f, 255f);
		transparentColor = new Color(255f, 255f, 255f, 0);
		if (!itemCounterEnabled)
		{
			if (itemCounter != null)
			{
				Destroy(itemCounter.gameObject);
				itemCounter = null;
			}
		}

		if (dragAndDropEnabled)
		{
			DragAndDrop dragAndDrop = gameObject.AddComponent<DragAndDrop>();
			dragAndDrop.Initialize();
		}
	}
}