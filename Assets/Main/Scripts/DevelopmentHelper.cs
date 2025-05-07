using UnityEngine;
using UnityEngine.InputSystem;

public class DevelopmentHelper : MonoBehaviour
{
#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
	public void SpawnItem(InputAction.CallbackContext context)
	{
		//int itemsCount = 24;
		//itemsCount = inventory.AddItem(ItemDatabase.GetItem(1), itemsCount);

		//CharacterStatsFactory undeadCharacterStatsFactory = new CharacterStatsFactory();
		//ICharacterStats characterStats = undeadCharacterStatsFactory.GetCharacterStats();
		//Debug.Log(characterStats.Health);
		//Debug.Log(characterStats.Stamina);
	}

	public void RemoveItem(InputAction.CallbackContext context)
	{
		//int itemsCount = 25;
		//inventory.RemoveItem(ItemDatabase.GetItem(1), itemsCount);
	}
}
