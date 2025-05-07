using Unity.VisualScripting;
using UnityEngine.UI;

public readonly struct ContainerItemTemp
{
	public readonly Item item;
	public readonly int count;

	public ContainerItemTemp(Item item, int count)
	{
		this.item = item;
		this.count = count;
	}

	//public static bool operator ==(ContainerItemTemp left, ContainerItemTemp right)
	//{
	//	if (left.item == right.item && left.count == right.count)
	//	{ return true; }
	//	else
	//	{ return false; }
	//}
	//public static bool operator !=(ContainerItemTemp left, ContainerItemTemp right)
	//{
	//	if (left.item != right.item || left.count != right.count)
	//	{ return true; }
	//	else
	//	{ return false; }
	//}
}
