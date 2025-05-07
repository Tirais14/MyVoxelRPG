using System;

public interface IContainerSlot
{
	int Id { get; }
	Item Item { get; }
	int Count { get; }
	IContainer OwnerContainer { get; }
	Action OnItemChangedCallback { get; }
	enum Condition : byte
	{
		Empty,
		Filled,
		FilledMax
	}
	Condition ConditionProperty { get; }

	ContainerItemTemp GetContainerItemTemp();

	int AddItem(Item item, in int addItemsCount);

	int RemoveItem(Item item, int deleteItemsCount);
}
