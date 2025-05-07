using UnityEngine;

public class Initializer : MonoBehaviour
{
	[SerializeField] private GameManagers gameManagers;
	[SerializeField] private GlobalProperties globalProperties;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private Controlls controlls;
	[SerializeField] private ItemDatabase itemDatabase;
	[SerializeField] private Inventory inventory;
	[SerializeField] private EquipmentUI equipment;
	[SerializeField] private ItemSpawner itemSpawner;
	[SerializeField] private Player player;

	private void Awake()
	{
		globalProperties.Initialize();
		playerController.Initialize();
		controlls.Initialize();
		itemDatabase.Initialize();
		gameManagers.Initialize();
		GameManagers.PrefabDatabase.Initialize();
		GameManagers.DraggingItemUnsafe.Initialize();
		GameManagers.Inventory.Initialize();
		GameManagers.MainUI.Initialize();
		equipment.Initialize();
		itemSpawner.Initialize();
		player.Initialize();
		GameManagers.ContextMenu.Initialize();
	}
}
