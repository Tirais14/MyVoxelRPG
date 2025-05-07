using System.Text;
using UnityEngine;

public class UseItems : MonoBehaviour
{
	public GameObject SetItemToCharacter(Transform itemHolder, IEquipable equipableItem)
	{
		if (itemHolder == null)
		{
			Logger.NullParameter<Transform>(gameObject);
			return null;
		}
		if (equipableItem == null)
		{
			Logger.NullParameter<IEquipable>(gameObject);
			return null;
		}
		if (itemHolder.childCount > 0)
		{
			Logger.SendError(gameObject: gameObject, "The item already setted");
			return null;
		}

		GameObject spawnedObject = Instantiate(equipableItem.Prefab, itemHolder);
		spawnedObject.transform.localScale = Vector3.one / 100;
		return spawnedObject;
	}

	public void RemoveItemFromCharacter(GameObject objectToDelete)
	{
		if (objectToDelete == null)
		{
			Logger.NullParameter<Transform>(gameObject);
			return;
		}

		Destroy(objectToDelete);
	}
}
