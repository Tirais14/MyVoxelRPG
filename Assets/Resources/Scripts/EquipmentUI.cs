using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : Container
{
	[Header("Equipment Slots")]
	[SerializeField] private EquipmentSlot headSlot;
	[SerializeField] private EquipmentSlot chestSlot;
	[SerializeField] private EquipmentSlot rightArmSlot;
	[SerializeField] private EquipmentSlot rightWeaponSlot;
	[SerializeField] private EquipmentSlot leftArmSlot;
	[SerializeField] private EquipmentSlot leftWeaponSlot;
	[SerializeField] private EquipmentSlot legsSlot;
	[SerializeField] private EquipmentSlot bootsSlot;
}
