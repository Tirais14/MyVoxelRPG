using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Inventory : Container
{
	[SerializeField] private GameObject bufferContainerItemPrefab;

	private BufferContainerItem bufferContainerItem;
	public BufferContainerItem BufferContainerItem => bufferContainerItem;


	public override void SwitchState(InputAction.CallbackContext context)
	{
		if (isOpen)
		{
			GameManagers.Controlls.OpenContextMenu.Disable();
			Hide();
		}
		else
		{ 
			GameManagers.Controlls.OpenContextMenu.Enable();
			Show();
		}
	}

	private void CreateBufferContainerSlot()
	{
		var bufferContainerItemObj = Instantiate(bufferContainerItemPrefab, transform);
		bufferContainerItemObj.name = "Buffer" + containerName + "Item";
		bufferContainerItemObj.transform.localScale = new Vector3(1f, 1f, 1f);
		bufferContainerItemObj.transform.position = Vector3.zero;
		Vector2 slotSize = containerSlotPrefab.GetComponent<RectTransform>().rect.size;
		RectTransform bufferContainerItemRctTr = bufferContainerItemObj.GetComponent<RectTransform>();
		bufferContainerItemRctTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize.x);
		bufferContainerItemRctTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize.y);
		bufferContainerItem = bufferContainerItemObj.GetComponent<BufferContainerItem>();
		bufferContainerItem.Initialize();
	}

	#region Controlled by Controlls
#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
	public void DropItemToWorldActionCheck(InputAction.CallbackContext context)
#pragma warning restore IDE0060 // Удалите неиспользуемый параметр
	{
		if (DragAndDrop.IsDragging && !GameManagers.Controlls.IsPointerOverGameObject)
		{ bufferContainerItem.DropToWorld(); }
	}

	public void OnScroll(InputAction.CallbackContext context)
	{
		float scrollValue = context.action.ReadValue<Vector2>().y;
		ReadOnlyCollection<RaycastResult> underPointerObjects = Utils.GetUnderPointerRaycastsResults();
		for (int i = 0; i < underPointerObjects.Count; i++)
		{
			if (underPointerObjects[i].gameObject.TryGetComponent(out DragAndDrop dragAndDropComp))
			{ dragAndDropComp.MouseScrollingAction(scrollValue); }
		}
	}
	#endregion

	public override void Show()
	{
		base.Show();
		GameManagers.Controlls.OnInventoryStateChangedCallback?.Invoke();
	}

	public override void Hide()
	{
		base.Hide();
		GameManagers.Controlls.OnInventoryStateChangedCallback?.Invoke();
	}

	public override void Initialize()
	{
		CreateBufferContainerSlot();
		base.Initialize();
	}

	private void Update()
	{
		DragAndDrop.DraggingCheck();

		if (DragAndDrop.IsDragging)
		{ bufferContainerItem.transform.position = Input.mousePosition; }
		else if (!DragAndDrop.IsDragging && bufferContainerItem.Item != null)
		{ DragAndDrop.ReturnToInventoryRemainderOfTakenContainerItem(); }

		if (scrollRectComp != null)
		{
			if (GameManagers.Controlls.DisableScrollingInView.IsPressed())
			{ DisableMouseScrollView(); }
			else
			{ EnableMouseScrollView(); }
		}
	}
}
