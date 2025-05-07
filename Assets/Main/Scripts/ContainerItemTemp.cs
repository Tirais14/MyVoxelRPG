using Unity.VisualScripting;
using UnityEngine.UI;

public readonly struct ContainerItemTemp
{
	public static ContainerItemTemp Empty { get { return new ContainerItemTemp(); } }
	public readonly Item item;
	public readonly int count;

	public ContainerItemTemp(Item item, int count)
	{
		this.item = item;
		this.count = count;
	}

	public override int GetHashCode() => item.GetHashCode() + item.name.GetHashCode();

	public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

	public static bool operator ==(ContainerItemTemp left, ContainerItemTemp right) => left.Equals(right);
	public static bool operator !=(ContainerItemTemp left, ContainerItemTemp right) => !left.Equals(right);
}
