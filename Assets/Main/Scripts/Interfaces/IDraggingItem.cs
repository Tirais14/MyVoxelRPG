using UnityEngine;

public interface IDraggingItem
{
	Item Item { get; }
	int Count { get; }
	bool IsActive { get; }

	void DropToWorld();

	int AddItem(Item item, in int addItemsCount);

	void RemoveItem();
	void RemoveItem(Item item, int deleteItemsCount);
}
