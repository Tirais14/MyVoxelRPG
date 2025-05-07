using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public static Inventory Instance { get; private set; }
	public static bool IsOpen { get; private set; }
	public static bool IsDragging { get; private set; }
}
