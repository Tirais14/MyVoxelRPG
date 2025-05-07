using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class Controlls : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private GraphicRaycaster graphicRaycaster;
	//[SerializeField] private PlayerController playerController;
	[SerializeField] private DevelopmentHelper developmentHelper;
	private InputSystem inputSystem;
	[SerializeField] private InputSystemUIInputModule inputSystemUIInputModule;
	public Action OnInventoryStateChangedCallback { get; private set; }
	public GraphicRaycaster GraphicRaycaster => graphicRaycaster;
	public bool IsPointerOverGameObject { get; private set; }
	public InputAction LeftClick { get; private set; }
	public InputAction MiddleClick { get; private set; }
	public InputAction RightClick { get; private set; }
	public InputAction ScrollWheel { get; private set; }
	public InputAction Cancel { get; private set; }
	public InputAction OpenInventory { get; private set; }
	public InputAction RunSwitch { get; private set; }
	public InputAction SpawnItem { get; private set; }
	public InputAction RemoveItem { get; private set; }
	public InputAction GetHalfStackOfItems { get; private set; }
	public InputAction GetItemsWithScroll { get; private set; }
	public InputAction MoveForwardAndBackward { get; private set; }
	public InputAction MoveRightAndLeft { get; private set; }
	public InputAction Attack { get; private set; }
	public InputAction ItemSpawner { get; private set; }
	public InputAction Interact { get; private set; }
	public InputAction OpenContextMenu { get; private set; }
	public InputAction Escape { get; private set; }
	public InputAction Jump { get; private set; }
	public InputAction DisableScrollingInView { get; private set; }

	private void SwitchWorldInteractControlls()
	{
		if (GameManagers.Inventory.IsOpen)
		{
			RunSwitch.Disable();
			Attack.Disable();
		}
		else
		{
			RunSwitch.Enable();
			Attack.Enable();
		}
		
	}
	public void Initialize()
	{
		inputSystem = new InputSystem();

		LeftClick = inputSystemUIInputModule.leftClick.action;
		MiddleClick = inputSystemUIInputModule.middleClick.action;
		RightClick = inputSystemUIInputModule.rightClick.action;
		ScrollWheel = inputSystemUIInputModule.scrollWheel.action;
		Cancel = inputSystemUIInputModule.cancel.action;
	}

	private void Update()
	{
		IsPointerOverGameObject = inputSystemUIInputModule.IsPointerOverGameObject(0);
	}

	private void OnEnable()
	{
		OnInventoryStateChangedCallback += SwitchWorldInteractControlls;

		OpenInventory = inputSystem.Player.OpenInventory;
		RunSwitch = inputSystem.Player.Run;
		SpawnItem = inputSystem.Player.SpawnItem;
		RemoveItem = inputSystem.Player.RemoveItem;
		GetHalfStackOfItems = inputSystem.Player.GetHalfStackOfItems;
		GetItemsWithScroll = inputSystem.Player.GetItemsWithScroll;
		MoveForwardAndBackward = inputSystem.Player.MoveForwardAndBackward;
		MoveRightAndLeft = inputSystem.Player.MoveRightAndLeft;
		Attack = inputSystem.Player.Attack;
		ItemSpawner = inputSystem.Player.ItemSpawner;
		Interact = inputSystem.Player.Interact;
		OpenContextMenu = inputSystem.Player.OpenContextMenu;
		Escape = inputSystem.Player.Escape;
		Jump = inputSystem.Player.Jump;
		DisableScrollingInView = inputSystem.Player.DisableScrollingInView;

		LeftClick.performed += GlobalProperties.MouseLeftButtonAction;
		LeftClick.performed += GameManagers.ContextMenu.ClickCheck;
		LeftClick.performed += GameManagers.MainUI.DropItemToWorldActionCheck;
		OpenInventory.performed += GameManagers.Inventory.SwitchState;
		OpenInventory.performed += GameManagers.EquipmentUI.SwitchState;
		RunSwitch.performed += GameManagers.PlayerController.SwitchWalkingToRun;
		SpawnItem.performed += developmentHelper.SpawnItem;
		RemoveItem.performed += developmentHelper.RemoveItem;
		GetItemsWithScroll.performed += GameManagers.MainUI.OnScroll;
		MoveForwardAndBackward.performed += GameManagers.PlayerController.MoveForwardAndBackward;
		MoveRightAndLeft.performed += GameManagers.PlayerController.MoveRightAndLeft;
		Attack.performed += GameManagers.PlayerController.Attack;
		ItemSpawner.performed += GameManagers.Inventory.SwitchState;
		ItemSpawner.performed += GameManagers.ItemSpawner.SwitchState;
		//Interact.performed += 
		OpenContextMenu.performed += GameManagers.ContextMenu.Show;
		Escape.performed += GameManagers.ContextMenu.Hide;
		Jump.performed += GameManagers.PlayerController.Jump;
		DisableScrollingInView.performed += GameManagers.MainUI.DisableScrollingView;

		OpenInventory.Enable();
		RunSwitch.Enable();
		SpawnItem.Enable();
		RemoveItem.Enable();
		GetHalfStackOfItems.Enable();
		GetItemsWithScroll.Enable();
		MoveForwardAndBackward.Enable();
		MoveRightAndLeft.Enable();
		Attack.Enable();
		ItemSpawner.Enable();
		Interact.Enable();
		Escape.Enable();
		Jump.Enable();
		DisableScrollingInView.Enable();
	}

	private void OnDisable()
	{
		OnInventoryStateChangedCallback -= SwitchWorldInteractControlls;

		OpenInventory.Disable();
		RunSwitch.Disable();
		SpawnItem.Disable();
		RemoveItem.Disable();
		GetHalfStackOfItems.Disable();
		MoveForwardAndBackward.Disable();
		MoveRightAndLeft.Disable();
		Attack.Disable();
		ItemSpawner.Disable();
		Interact.Disable();
		OpenContextMenu.Disable();
		Escape.Disable();
		Jump.Disable();
		DisableScrollingInView.Disable();
	}
}
