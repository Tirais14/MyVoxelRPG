using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
	private protected Weapon rightHoldingWeapon;
	private protected Weapon leftHoldingWeapon;
	private protected float health;
	private float healthMax;
	public float HealthMax => healthMax;
	private float stamina;
	private float staminaMax;
	
	private Animator animatorComp;
	private Rigidbody rigidBodyComp;
	private Attackable attackableComp;
	private ChangableHoldingItem changableHoldingItemComp;

	private protected Transform rightItemHolder;
	private protected Transform leftItemHolder;
	private protected Transform rightItemHidedHolder;
	private protected Transform leftItemHidedHolder;
	private protected Transform backItemHidedHolder;
	[SerializeField] private bool isCanUseItems;

	public void GetWeapon()
	{
		//attackableComp.GetWeapon(weaponHolder);
	}

	public void Attack()
	{
		if (attackableComp == null)
		{ return; }

		attackableComp.Attack(animatorComp);
	}

	public void TakeDamage(Weapon weapon)
	{
		if (attackableComp == null)
		{ return; }

		attackableComp.TakeDamage(this, weapon, ref health);
	}

	public void SetItemToRightArm(Item item)
	{
		if (changableHoldingItemComp == null)
		{ return; }

		rightHoldingWeapon = item as Weapon;
		changableHoldingItemComp.SetItemToCharacter(rightItemHolder, item);
	}

	public void SetItemToLeftArm(Item item)
	{
		if (changableHoldingItemComp == null)
		{ return; }

		changableHoldingItemComp.SetItemToCharacter(leftItemHolder, item);
	}

	public void RemoveItemFromRightArm()
	{
		if (changableHoldingItemComp == null)
		{ return; }
		if (rightItemHolder.childCount == 0)
		{
			DebugLogging.SendError(gameObject:gameObject, "No item to remove");
			return;
		}

		changableHoldingItemComp.RemoveItemFromCharacter(rightItemHolder.GetChild(0).gameObject);
	}

	public void RemoveItemFromLeftArm()
	{
		if (changableHoldingItemComp == null)
		{ return; }
		if (leftItemHolder.childCount == 0)
		{
			DebugLogging.SendError(gameObject: gameObject, "No item to remove");
			return;
		}

		changableHoldingItemComp.RemoveItemFromCharacter(leftItemHolder.GetChild(0).gameObject);
	}

	public void ChangeHealth(float changeValue)
	{
		if (changeValue == 0)
		{ return; }

		health += changeValue;
	}

	public void ChangeStamina(float changeValue)
	{
		if (changeValue == 0)
		{ return; }

		stamina += changeValue;
	}

	public void UseItem(ContainerSlot containerSlot)
	{
		if (containerSlot == null)
		{
			DebugLogging.NullParameter<ContainerSlot>(gameObject);
			return;
		}

		if (containerSlot.ContainedItem.Item is ItemUsable item)
		{
			ChangeHealth(item.HealthInfluenceValue);
			ChangeStamina(item.StaminaInfluenceValue);
			containerSlot.RemoveItem(item, 1);
		}
		else
		{ DebugLogging.SendError(gameObject: gameObject, "The item cannot be used"); }
		
	}

	public void Die()
	{
	}

	private protected void Initialize()
	{
		animatorComp = GetComponent<Animator>();
		rigidBodyComp = GetComponent<Rigidbody>();
		attackableComp = GetComponent<Attackable>();
		changableHoldingItemComp = GetComponentInChildren<ChangableHoldingItem>();

		rightItemHolder = Utils.FindInChildrens(transform, "RightItemHolder");
		leftItemHolder = Utils.FindInChildrens(transform, "LeftItemHolder");
		rightItemHidedHolder = Utils.FindInChildrens(transform, "RightItemHidedHolder");
		leftItemHidedHolder = Utils.FindInChildrens(transform, "LeftItemHidedHolder");
		backItemHidedHolder = Utils.FindInChildrens(transform, "BackItemHidedHolder");
	}

	private void Awake()
	{
		Initialize();
	}

	private void OnTriggerEnter(Collider enteredCollider)
	{
		if (enteredCollider.TryGetComponent(out WorldItem worldItem) && worldItem.Item != rightHoldingWeapon)
		{
			if (worldItem.Item is Weapon weapon)
			{ 
				TakeDamage(weapon);
				rigidBodyComp.AddForce(new Vector3(0, 35, 35), ForceMode.Impulse);
			}
		}
	}
}
