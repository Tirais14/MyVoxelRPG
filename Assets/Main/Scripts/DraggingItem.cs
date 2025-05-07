using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DraggingItem : ContainerItem, IDraggingItem, ISwitchableGUI
{
	private TextMeshProUGUI counter;
	private bool isActive;
	public bool IsActive => isActive;

	public void DropToWorld()
	{
		DragAndDrop.DropItemToWorld(this);
		ResetItem();
	}

	public int AddItem(Item item, in int addItemsCount)
	{
		if (item == null)
		{
			Logger.NullParameter<Item>(gameObject:gameObject);
			return addItemsCount;
		}
		if (this.item != null && item != this.item)
		{
			Logger.SendError(gameObject: gameObject, "The added item is not the same as the existing one");
			return addItemsCount;
		}
		if (addItemsCount <= 0)
		{
			Logger.WrongParameter<GameObject>();
			return -1;
		}
		if (this.item != null && count == this.item.MaxCount)
		{
			Logger.SendWarning(gameObject: gameObject, "No more item can be added");
			return addItemsCount;
		}

		int resultItemsCount = count + addItemsCount;
		if (this.item == null && addItemsCount < item.MaxCount)
		{
			SetItem(item, resultItemsCount);
			return 0;
		}
		else if (this.item == null && addItemsCount >= item.MaxCount)
		{
			SetItem(item, item.MaxCount);
			return resultItemsCount - item.MaxCount;
		}
		else if (this.item != null && resultItemsCount <= this.item.MaxCount)
		{
			count = resultItemsCount;
			UpdateCounter();
			return 0;
		}
		else if (this.item != null && resultItemsCount > this.item.MaxCount)
		{
			count = this.item.MaxCount;
			UpdateCounter();
			return resultItemsCount -= this.item.MaxCount;
		}

		Logger.SendError(gameObject:gameObject, "Failed to add an item");
		return addItemsCount;
	}

	/// <summary>Clear dragging item</summary>
	public void RemoveItem()
	{
		if (item == null)
		{
			Logger.SendError(gameObject: gameObject, "Not found item to remove");
			return;
		}
		if (count == 0)
		{
			Logger.SendError(gameObject: gameObject, "Dragging items count = 0. There's nothing to remove");
			return;
		}

		ResetItem();
		UpdateCounter();
	}

	public void RemoveItem(Item item, int deleteItemsCount)
	{
		if (item == null)
		{
			Logger.SendError(gameObject: gameObject, "Not found item to remove");
			return;
		}
		if (item != this.item)
		{
			Logger.SendError(gameObject: gameObject, "The added item is not the same as the existing one");
			return;
		}
		if (deleteItemsCount < 0 || deleteItemsCount > count || deleteItemsCount > this.item.MaxCount)
		{
			Logger.WrongParameter<GameObject>();
			return;
		}

		int resultItemsCount = count - deleteItemsCount;
		if (resultItemsCount == 0)
		{ ResetItem(); }
		else if (resultItemsCount > 0)
		{ count = resultItemsCount; }

		UpdateCounter();
	}

	public new void SetItem(Item item, in int itemsCount = 1)
	{
		this.item = item;
		imageComp.sprite = item.Icon;
		count = itemsCount;
		transform.SetAsLastSibling();
		UpdateCounter();
		Show();
	}

	public override void ResetItem()
	{
		item = null;
		imageComp.sprite = null;
		count = 0;
		gameObject.transform.position = Vector2.zero;
		UpdateCounter();
		Hide();
	}

	private void UpdateCounter()
	{
		if (count > 1 && !counter.enabled)
		{ counter.enabled = true; }
		else if (count == 1 && counter.enabled)
		{ counter.enabled = false; }

		counter.text = count.ToString();
	}

	public void Show()
	{
		gameObject.SetActive(true);
		isActive = true;
		if (item.Stackable)
		{ counter.enabled = true; }
	}

	public void Hide()
	{
		if (counter.enabled)
		{ counter.enabled = false; }
		gameObject.SetActive(false);
		isActive = false;
	}

	#region Not Implemented
	//public new void EnableRaycastTarget() => throw new System.NotImplementedException();

	//public new void DisableRaycastTarget() => throw new System.NotImplementedException();

	//public new void EnableImage() => throw new System.NotImplementedException();

	//public new void DisableImage() => throw new System.NotImplementedException();
	#endregion

	public override void Initialize()
	{
		imageComp = GetComponent<Image>();
		counter = GetComponentInChildren<TextMeshProUGUI>();
		Hide();
	}
}
