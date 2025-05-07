using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabDatabase : MonoBehaviour
{
	[SerializeField] private GameObject[] tempPrefabs;
	private string[] existsPrefabsNames;
	private Dictionary<string, GameObject> prefabs;

	public static GameObject GetPrefab(string prefabName)
	{
		if (!TryFindNameInExists(prefabName))
		{
			Logger.SendError("Incorrect prefab name");
			return null;
		}

		
		return GameManagers.PrefabDatabase.prefabs[prefabName];
	}

	private static bool TryFindNameInExists(string prefabName)
	{
		for (int i = 0; i < GameManagers.PrefabDatabase.existsPrefabsNames.Length; i++)
		{
			if (GameManagers.PrefabDatabase.existsPrefabsNames[i] == prefabName)
			{ return true; }
		}

		return false;
	}

	public void Initialize()
	{
		prefabs = new Dictionary<string, GameObject>(tempPrefabs.Length);
		existsPrefabsNames = new string[tempPrefabs.Length];

		for (int i = 0; i < tempPrefabs.Length; i++)
		{
			existsPrefabsNames[i] = tempPrefabs[i].name;
			prefabs.Add(tempPrefabs[i].name, tempPrefabs[i]);
		}
	}
}
