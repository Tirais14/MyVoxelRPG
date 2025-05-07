using UnityEngine;

public class Attackable : MonoBehaviour
{
	public void GetWeapon(Transform weaponHolder, Weapon weapon)
	{
		//weapon.transform.SetParent(weaponHolder.transform);
	}

	public void Attack(Animator animatorComp)
	{
		animatorComp.SetTrigger("Attack");
	}

	public void TakeDamage(Character character, Weapon weapon, ref float health)
	{
		health -= weapon.DamageValue;
	}
}
