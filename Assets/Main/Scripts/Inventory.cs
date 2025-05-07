using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Inventory : Container
{
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
}
