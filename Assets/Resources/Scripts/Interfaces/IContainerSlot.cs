using static ContainerSlot;

public interface IContainerSlot
{
	int AddItem(Item item, in int addItemsCount);

	int RemoveItem(Item item, int deleteItemsCount);

	void Show();

	void Hide();

	int Id { get; }

	bool ContextMenuEnabled { get; }

	Condition ConditionProperty { get; }
}
