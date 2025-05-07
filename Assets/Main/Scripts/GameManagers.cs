using UnityEngine;

public class GameManagers : MonoBehaviour
{
	public static Controlls Controlls {  get; private set; }
	public static Inventory Inventory { get;private set; }
	public static EquipmentUI EquipmentUI { get; private set; }
	public static ItemSpawner ItemSpawner { get; private set; }
	public static ContextMenu ContextMenu { get; private set; }
	public static Player Player { get; private set; }
	public static PlayerController PlayerController { get; private set; }
	public static MainUI MainUI { get; private set; }
	public static PrefabDatabase PrefabDatabase { get; private set; }
	public static DraggingItem DraggingItemUnsafe { get; private set; }
	public static IDraggingItem DraggingItem { get; private set; }
	public static ItemDatabase ItemDatabase { get; private set; }


	public void Initialize()
	{
		Controlls = FindObjectOfType<Controlls>();
		Inventory = FindObjectOfType<Inventory>();
		EquipmentUI = FindObjectOfType<EquipmentUI>();
		ItemSpawner = FindObjectOfType<ItemSpawner>();
		ContextMenu = FindObjectOfType<ContextMenu>();
		Player = FindObjectOfType<Player>();
		PlayerController = FindObjectOfType<PlayerController>();
		MainUI = FindObjectOfType<MainUI>();
		PrefabDatabase = FindObjectOfType<PrefabDatabase>();
		DraggingItemUnsafe = FindObjectOfType<DraggingItem>();
		DraggingItem = DraggingItemUnsafe;
		ItemDatabase = FindObjectOfType<ItemDatabase>();
	}
}
