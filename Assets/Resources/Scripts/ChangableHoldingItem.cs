using UnityEngine;

public class ChangableHoldingItem : MonoBehaviour
{
	public void SetItemToCharacter(Transform itemHolder, Item item)
	{
		if (itemHolder == null)
		{
			DebugLogging.NullParameter<Transform>(gameObject);
			return;
		}
		if (item == null)
		{
			DebugLogging.NullParameter<Item>(gameObject);
			return;
		}
		if (itemHolder.childCount > 0)
		{
			DebugLogging.SendError(gameObject: gameObject, "The item already setted");
			return;
		}

		GameObject spawnedObject = Instantiate(item.Prefab, itemHolder);
		spawnedObject.transform.localScale = Vector3.one / 100;
	}

	public void RemoveItemFromCharacter(GameObject objectToDelete)
	{
		if (objectToDelete == null)
		{
			DebugLogging.NullParameter<Transform>(gameObject);
			return;
		}

		Destroy(objectToDelete);
	}
}
