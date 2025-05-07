using UnityEngine;

public abstract class Weapon : Item
{
	[SerializeField] private float damageValue;
	public float DamageValue => damageValue;
}
