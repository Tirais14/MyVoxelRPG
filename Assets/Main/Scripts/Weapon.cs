using UnityEngine;

public abstract class Weapon : Item, IEquipable
{
	[SerializeField] private float damageValue;
	public float DamageValue => damageValue;
}
