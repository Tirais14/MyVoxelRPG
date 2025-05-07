using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Provides access to methods common to the user interfaces
/// </summary>
public class MainUI : MonoBehaviour
{
	[SerializeField] private GameObject containerSlotPrefab;
	[SerializeField] private GameObject draggingItemPrefab;
	private IContainer selectedContainer;


#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
	public void DropItemToWorldActionCheck(InputAction.CallbackContext context)
#pragma warning restore IDE0060 // Удалите неиспользуемый параметр
	{
		if (DragAndDrop.IsDragging && !GameManagers.Controlls.IsPointerOverGameObject)
		{ GameManagers.DraggingItem.DropToWorld(); }
	}

	public void OnScroll(InputAction.CallbackContext context)
	{
		float scrollValue = context.action.ReadValue<Vector2>().y;
		if (Utils.TryGetUnderPointerComponent(out DragAndDrop dragAndDropComp))
		{ dragAndDropComp.MouseScrollingAction(scrollValue); }
	}

	public void DisableScrollingView(InputAction.CallbackContext context)
	{
		if (context.action.IsPressed())
		{
			if (GameManagers.Controlls.IsPointerOverGameObject && Utils.TryGetUnderPointerComponent(out IContainerSlot containerSlot))
			{
				if (containerSlot.OwnerContainer.ScrollViewEnabled)
				{
					containerSlot.OwnerContainer.DisableScrollView();
					selectedContainer = containerSlot.OwnerContainer;
				}
			}
		}
		else if (!context.action.IsPressed() && selectedContainer != null)
		{
			selectedContainer.EnableScrollView();
			selectedContainer = null;
		}
	}

	private void UpdateDraggingItemPosition() => GameManagers.DraggingItemUnsafe.transform.position = Input.mousePosition;

	public void Initialize() { }

	private void Update()
	{
		DragAndDrop.DraggingCheck();

		if (DragAndDrop.IsDragging)
		{ UpdateDraggingItemPosition(); }
	}
}
