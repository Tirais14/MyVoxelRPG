using UnityEngine;

public sealed class ItemSpawnerItem : ContainerItem
{
	public override bool SetItem(Item item, in int itemsCount) => SetItemMax(item);

	public override bool SetItem(ContainerItemTemp containerItemTemp) => false;

	public override bool SetItemMax(Item item)
	{
		this.item = item;
		count = item.MaxCount;
		imageComp.sprite = item.Icon;
		imageComp.color = defaultColor;

		return true;
	}

	public override void ResetItem()
	{
		count = item.MaxCount;
	}

	//public override void Initialize()
	//{
	//	ownerContainer = GetComponentInParent<Container>();
	//	defaultColor = new Color(255f, 255f, 255f, 255f);
	//	transparentColor = new Color(255f, 255f, 255f, 0);
	//	if (TryGetComponent(out DragAndDrop dragAndDrop))
	//	{ dragAndDrop.Initialize(); }
	//}
}
