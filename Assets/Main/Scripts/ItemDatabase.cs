using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour, IInitializable
{
	[SerializeField] private Item[] itemsArray;
	public int ItemsCount { get; private set; }
	private Dictionary<string, Item> items;
	private List<string> itemNames;
	public IReadOnlyList<string> ItemNames => itemNames;
	private List<int> itemIds;
	public IReadOnlyList<int> ItemIds => itemIds;

	public static Item GetItem(string name)
	{
		bool nameExists = false;
		for (int i = 0; i < GameManagers.ItemDatabase.itemNames.Count; i++)
		{
			if (GameManagers.ItemDatabase.itemNames[i] == name)
			{ 
				nameExists = true;
				break;
			}
		}
		if (!nameExists)
		{
			Logger.SendError("Wrong item name");
			return null;
		}

		return GameManagers.ItemDatabase.items[name];
	}
	public static Item GetItem(int itemId)
	{
		bool idExists = false;
		for (int i = 0; i < GameManagers.ItemDatabase.itemIds.Count; i++)
		{
			if (GameManagers.ItemDatabase.itemIds[i] == itemId)
			{
				idExists = true;
				break;
			}
		}
		if (!idExists)
		{
			Logger.SendError("Wrong item id");
			return null; 
		}

		Item item = null;
		foreach (var dictionaryItem in GameManagers.ItemDatabase.items) 
		{
			item = dictionaryItem.Value;
			if (item.Id == itemId)
			{ break; }
		}

		return item;
	}

	public void Initialize()
	{
		int itemsCount = itemsArray.Length;
		items = new Dictionary<string, Item>(itemsCount);
		itemNames = new List<string>(itemsCount);
		itemIds = new List<int>(itemsCount);

		for (int i = 0; i < itemsCount; i++)
		{ 
			items.Add(itemsArray[i].Name, itemsArray[i]);
			itemNames.Add(itemsArray[i].Name);
			itemIds.Add(itemsArray[i].Id);
		}
		ItemsCount = items.Count;
	}
}
