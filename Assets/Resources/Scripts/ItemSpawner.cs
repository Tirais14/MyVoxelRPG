using UnityEngine;

public sealed class ItemSpawner : Container
{
	public static ItemSpawner Instance { get; private set; }

	public override int AddItem(Item item, in int addItemsCount) => 0;

	public override void RemoveItem(Item item, in int itemsCount) { }

	public override void AddToFilledSlots(in int containerSlotId) { }

	public override void RemoveFromFilledSlots(in int containerSlotId) { }

	private protected override void InitializeNewSlots()
	{
		const sbyte idOffset = 1;
		for (int i = 0; i < ItemDatabase.Instance.ItemsCount; i++)
		{
			slots ??= new ContainerSlot[ItemDatabase.Instance.ItemsCount];
			GameObject newContainerSlotObj = Instantiate(containerSlotPrefab);
			newContainerSlotObj.transform.SetParent(containerSlots.transform);
			newContainerSlotObj.transform.localScale = Vector3.one;
			ItemSpawnerSlot newContainerSlot = newContainerSlotObj.GetComponent<ItemSpawnerSlot>();
			newContainerSlot.Initialize(i + idOffset);
			newContainerSlot.ContainedItem.SetContainer(this);
			newContainerSlot.AddItem(ItemDatabase.GetItem(ItemDatabase.Instance.ItemNames[i]), 1);
			slots[i] = newContainerSlot;
		}
	}

	private protected override void InitializeEmptySlots() { }

	private protected override void InitializeFilledSlots() { }

	public override void Show()
	{
		isOpen = true;
		gameObject.SetActive(true);
		//ContainerGUI.enabled = true;
		//for (int i = 0; i < SlotsCount; i++)
		//{ slots[i].gameObject.SetActive(true); }
	}

	public override void Hide()
	{
		isOpen = false;
		gameObject.SetActive(false);
		//ContainerGUI.enabled = false;
		//for (int i = 0; i < SlotsCount; i++)
		//{ slots[i].gameObject.SetActive(false); }
	}

	public override void Initialize()
	{
		Instance = this;
		InitializeNewSlots();
		slotsCount = slots.Length;
		Hide();
	}
}
