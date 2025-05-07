public sealed class ItemSpawnerSlot : ContainerSlot
{
	private protected override void CheckSlotState() { }

	public override int AddItem(Item item, in int addItemsCount)
	{
		containedItem.SetItem(item, item.MaxCount);
		return 0;
	}

	public override int RemoveItem(Item item, int deleteItemsCount) => 0;

	public override void Initialize()
	{
		base.Initialize();
		condition = IContainerSlot.Condition.FilledMax;
	}
}
