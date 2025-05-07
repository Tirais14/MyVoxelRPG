using UnityEngine;
using UnityEngine.UI;

public sealed class ItemSpawner : Container
{
	public override int AddItem(Item item, in int addItemsCount) => 0;

	public override void RemoveItem(Item item, in int itemsCount) { }

	public override void AddToFilledSlots(in int containerSlotId) { }

	public override void RemoveFromFilledSlots(in int containerSlotId) { }

	private protected override void InitializeNewSlots()
	{
		slots ??= new ContainerSlot[GameManagers.ItemDatabase.ItemsCount];
		for (int i = 0; i < GameManagers.ItemDatabase.ItemsCount; i++)
		{
			GameObject newContainerSlotObj = Instantiate(containerSlotPrefab, containerSlots.transform);
			//newContainerSlotObj.transform.SetParent(containerSlots.transform);
			newContainerSlotObj.transform.localScale = Vector3.one;
			ItemSpawnerSlot newContainerSlot = newContainerSlotObj.GetComponent<ItemSpawnerSlot>();
			newContainerSlot.Initialize();
			newContainerSlot.AddItem(ItemDatabase.GetItem(GameManagers.ItemDatabase.ItemNames[i]), 1);
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
		gameObject.GetComponentsByObjNamesInChildren('$' + containerName + "Slots$",
														"$Content$",
														"$Scroll View$",
														"$Scrollbar Vertical$",
														out containerSlots,
														out contentOfScrollView,
														out scrollRectComp,
														out scrollrbarVertical);
		ChangeContentSizeOfScrollView();
		InitializeNewSlots();
		slotsCount = slots.Length;
		Hide();
	}
}
