using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BufferContainerItem : ContainerItem
{
	//private Item item;
	//public Item Item => item;
	//private Image imageComp;
	private TextMeshProUGUI counter;
	public bool IsActive { get; private set; }
	//private int count;
	//public int Count => count;

	public void DropToWorld()
	{
		DragAndDrop.DropItemToWorld(this);
		ResetItem();
	}

	public int AddItem(Item item, in int addItemsCount)
	{
		if (item == null)
		{
			DebugLogging.NullParameter<Item>(gameObject:gameObject);
			return addItemsCount;
		}
		if (this.item != null && item != this.item)
		{
			DebugLogging.SendError(gameObject: gameObject, "The added item is not the same as the existing one");
			return addItemsCount;
		}
		if (addItemsCount <= 0)
		{
			DebugLogging.WrongParameter(gameObject:gameObject, addItemsCount);
			return -1;
		}
		if (this.item != null && count == this.item.MaxCount)
		{
			DebugLogging.SendWarning(gameObject: gameObject, "No more item can be added");
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

		DebugLogging.SendError(gameObject:gameObject, "Failed to add an item");
		return addItemsCount;
	}

	public void RemoveItem(Item item, int deleteItemsCount)
	{
		if (item == null)
		{
			DebugLogging.SendError(gameObject: gameObject, "Not found item to remove");
			return;
		}
		if (item != this.item)
		{
			DebugLogging.SendError(gameObject: gameObject, "The added item is not the same as the existing one");
			return;
		}
		if (deleteItemsCount < 0 || deleteItemsCount > count || deleteItemsCount > this.item.MaxCount)
		{
			DebugLogging.WrongParameter(gameObject: gameObject, deleteItemsCount);
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

	public override void Show()
	{
		imageComp.enabled = true;
		gameObject.SetActive(true);
		counter.gameObject.SetActive(true);
		IsActive = true;
		if (item.Stackable)
		{ counter.enabled = true; }
	}

	public override void Hide()
	{
		imageComp.enabled = false;
		counter.enabled = false;
		gameObject.SetActive(false);
		counter.gameObject.SetActive(false);
		IsActive = false;
	}

	#region Not Implemented
	public new void SetContainer(Container container) => throw new System.NotImplementedException();
	public new void EnableRaycastTarget() => throw new System.NotImplementedException();

	public new void DisableRaycastTarget() => throw new System.NotImplementedException();

	public new void EnableImage() => throw new System.NotImplementedException();

	public new void DisableImage() => throw new System.NotImplementedException();

	public new void ShowOnlyIfOpened() => throw new System.NotImplementedException();
	#endregion

	public override void Initialize()
	{
		imageComp = GetComponent<Image>();
		counter = GetComponentInChildren<TextMeshProUGUI>();
	}
}
