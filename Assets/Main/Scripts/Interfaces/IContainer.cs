using System;
using UnityEngine.Animations.Rigging;

public interface IContainer
{
	int SlotsCount { get; }
	int InitializedSlotsCount { get; }
	bool SpecialContainer { get; }
	bool IsOpen { get; }
	bool ScrollViewEnabled { get; }
	string ContainerName { get; }
	Action OnInitializedSlotCallback { get; }

	int AddItem(Item item, in int addItemsCount);

	void RemoveItem(Item item, in int itemsCount);

	void AddToFilledSlots(in int containerSlotId);

	void RemoveFromFilledSlots(in int containerSlotId);

	void EnableScrollView();

	void DisableScrollView();

	void Show();

	void Hide();
}
