using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class EquipmentUI : Container, IEquipmentUI
{
	private IEquipmentSlot headSlot;
	private IEquipmentSlot chestSlot;
	private IEquipmentSlot rightArmSlot;
	private IEquipmentSlot rightItemSlot;
	private IEquipmentSlot leftArmSlot;
	private IEquipmentSlot leftItemSlot;
	private IEquipmentSlot legsSlot;
	private IEquipmentSlot bootsSlot;

	[HideInInspector] private const string headSlotName = "Head";
	public string HeadSlotName => headSlotName;
	[HideInInspector] private const string chestSlotName = "Chest";
	public string ChestSlotName => chestSlotName;
	[HideInInspector] private const string rArmSlotName = "Right Arm";
	public string RArmSlotName => rArmSlotName;
	[HideInInspector] private const string rItemSlotName = "Right Item";
	public string RItemSlotName => rItemSlotName;
	[HideInInspector] private const string lArmSlotName = "Left Arm";
	public string LArmSlotName => lArmSlotName;
	[HideInInspector] private const string lItemSlotName = "Left Item";
	public string LItemSlotName => lItemSlotName;
	[HideInInspector] private const string legsSlotName = "Legs";
	public string LegsSlotName => legsSlotName;
	[HideInInspector] private const string bootsSlotName = "Boots";
	public string BootsSlotName => bootsSlotName;

	private IEquipmentSlot CreateEquipmentSlot(string slotFuncName)
	{
		GameObject newEquipmentSlotObj = Instantiate(containerSlotPrefab, containerSlots.transform);
		newEquipmentSlotObj.name = slotFuncName.Replace(' ', '_') + "EquipmentSlot";
		EquipmentSlot newEquipmentSlot = newEquipmentSlotObj.GetComponent<EquipmentSlot>();
		newEquipmentSlot.Initialize();
		return newEquipmentSlot;
	}

	private void CreateEmptySlot()
	{
		GameObject newEmptySlotObj = new GameObject("empty", typeof(RectTransform));
		newEmptySlotObj.transform.SetParent(containerSlots.transform);
	}

	private protected override void InitializeNewSlots()
	{
		GameObject[] objsToDelete = Utils.FindInChildrensByObjNames(containerSlots.transform, new Regex(@"\$Empty\$(.*)")).ToGameObjectArray();
		int slotsCount = objsToDelete.Count();

		for (int i = 0; i < objsToDelete.Length; i++)
		{ Destroy(objsToDelete[i]); }

		for (int i = 0; i < slotsCount; i++)
		{
			switch (i)
			{
				case 1:
					rightArmSlot = CreateEquipmentSlot(rArmSlotName);
					break;
				case 2:
					rightItemSlot = CreateEquipmentSlot(rItemSlotName);
					break;
				case 4:
					headSlot = CreateEquipmentSlot(headSlotName);
					break;
				case 5:
					chestSlot = CreateEquipmentSlot(chestSlotName);
					break;
				case 6:
					legsSlot = CreateEquipmentSlot(legsSlotName);
					break;
				case 7:
					bootsSlot = CreateEquipmentSlot(bootsSlotName);
					break;
				case 9:
					leftArmSlot = CreateEquipmentSlot(lArmSlotName);
					break;
				case 10:
					leftItemSlot = CreateEquipmentSlot(lItemSlotName);
					break;
				default:
					CreateEmptySlot();
					break;
			}
		}
	}
}
