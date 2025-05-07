public interface IContainer
{
	void AddItem(Item item, ref int count);

	void RemoveItem(Item item, int removeCount);

	ContainerItem[] GetContainedItems();
}
