using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Items/Melee Weapon")]
public class WeaponMelee : Weapon
{	public enum HoldType : byte
	{
		OneHand,
		TwoHand
	}
	[Header("Weapon Settings")]
	[SerializeField] private HoldType holdType;
	public HoldType HoldTypeProp => holdType;
	public enum DamageType : byte
	{
		Cutting,
		Stabbing,
		Crushing
	}
	[SerializeField] private DamageType damageType;
	public DamageType DamageTypeProp => damageType;
}

