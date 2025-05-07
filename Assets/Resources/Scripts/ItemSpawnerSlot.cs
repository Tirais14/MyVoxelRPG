public sealed class ItemSpawnerSlot : ContainerSlot
{
	private void SetSlotStateToFilledMax() => condition = Condition.FilledMax;

	private protected override void CheckSlotState() => SetSlotStateToFilledMax();

	public override int AddItem(Item item, in int addItemsCount)
	{
		containedItem.SetItem(item, item.MaxCount);
		return 0;
	}

	public override int RemoveItem(Item item, int deleteItemsCount) => 0;

	public override void Show() { }

	public override void Hide() { }

	private void OnEnable()
	{
		OnItemChangedCallback += CheckSlotState;
		OnItemChangedCallback += containedItem.ResetItem;
	}

	private void OnDisable()
	{
		OnItemChangedCallback -= CheckSlotState;
		OnItemChangedCallback -= containedItem.ResetItem;
	}
}
