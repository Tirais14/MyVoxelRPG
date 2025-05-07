using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
	[SerializeField] private Button useButton;
	[SerializeField] private TextMeshProUGUI useButtonTextMesh;
	[SerializeField] private Button dropButton;
	[SerializeField] private TextMeshProUGUI dropButtonTextMesh;
	private ContainerSlot onClickedContainerSlot;
	private bool isOpen;

#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
	public void ClickCheck(InputAction.CallbackContext context)
	{
		if (!isOpen)
		{ return; }

		for (int i = 0; i < GlobalProperties.LeftClickUnderPointerObjects.Count; i++)
		{
			if (GlobalProperties.LeftClickUnderPointerObjects[i].gameObject.GetComponentInParent<ContextMenu>() != null)
			{ return; }
		}

		Hide();
	}

	public void UseItem()
	{
		if (onClickedContainerSlot == null)
		{
			Logger.NullParameter<ContainerSlot>(gameObject:gameObject);
			return;
		}

		GameManagers.Player.UseItem(onClickedContainerSlot);
		Hide();
	}

	public void DropItem()
	{
		if (onClickedContainerSlot == null)
		{
			Logger.NullParameter<ContainerSlot>(gameObject: gameObject);
			return;
		}

		DragAndDrop.DropItemToWorld(onClickedContainerSlot, onClickedContainerSlot.Count);
		onClickedContainerSlot = null;
		Hide();
	}

	public void Show(InputAction.CallbackContext context)
	{
		ReadOnlyCollection<RaycastResult> underPointerGameObejcts = Utils.GetUnderPointerUIRaycastResults();

		if ((underPointerGameObejcts.Count == 0 && gameObject.activeSelf))
		{ Hide(); }

		for (int i = 0; i < underPointerGameObejcts.Count; i++)
		{
			if (underPointerGameObejcts[i].gameObject.TryGetComponent(out ContainerSlot onClickedContainerSlot)
				&& onClickedContainerSlot.ContextMenuEnabled)
			{
				if (onClickedContainerSlot == this.onClickedContainerSlot || onClickedContainerSlot.ConditionProperty == IContainerSlot.Condition.Empty)
				{
					Hide();
					return;
				}

				this.onClickedContainerSlot = onClickedContainerSlot;
				gameObject.SetActive(true);
				Rect onClickedContainerSlotRect = onClickedContainerSlot.GetComponent<RectTransform>().rect;
				transform.position = onClickedContainerSlot.transform.position;
				transform.localPosition += new Vector3(onClickedContainerSlotRect.width, 0f);
				isOpen = true;

				if (!useButton.interactable && onClickedContainerSlot.Item is ItemUsable)
				{ useButton.interactable = true; }
				else if (useButton.interactable && onClickedContainerSlot.Item is not ItemUsable)
				{ useButton.interactable = false; }

				return;
			}
		}
	}

	public void Hide()
	{
		if (onClickedContainerSlot != null)
		{ onClickedContainerSlot = null; }

		isOpen = false;
		transform.position = new Vector3(0f, GlobalProperties.MainCamera.pixelHeight + 200, 0f);
		gameObject.SetActive(false);
	}

	public void Hide(InputAction.CallbackContext context) => Hide();
#pragma warning restore IDE0060 // Удалите неиспользуемый параметр

	public void Initialize()
	{
		Hide();
	}
}
