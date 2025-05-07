using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
	private protected Weapon rightHoldingItem;
	private protected Weapon leftHoldingWeapon;
	private Collider rightItemCollider;
	private Collider leftItemCollider;
	private protected float health;
	private float healthMax;
	public float HealthMax => healthMax;
	private float stamina;
	private float staminaMax;
	
	private Animator animatorComp;
	private Rigidbody rigidBodyComp;
	private Attackable attackableComp;
	private UseItems useItemsComp;

	private Material material;
	private protected Transform rightItemPlaceholder;
	private protected Transform leftItemPlaceholder;
	//private protected Transform rightItemHidedHolder;
	//private protected Transform leftItemHidedHolder;
	//private protected Transform backItemHidedHolder;
	[SerializeField] private bool CanUseItems;
	private const float takeDamageDelay = 1f;
	private float takeDamageTimer;

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

	private void DamageEffect(in Vector3 forward)
	{
		//TODO create push effect
		//rigidBodyComp.velocity += -(new Vector3(forward.x, 0.25f, forward.z) * 20f);

		StartCoroutine(EffectCoroutine());
		material.SetColor("_EmissionColor", new Color(155f, 155f, 155f, 35f));
	}

	private IEnumerator EffectCoroutine()
	{
		material.EnableKeyword("_EMISSION");
		yield return new WaitForSeconds(0.2f);
		material.DisableKeyword("_EMISSION");
		yield return new WaitForSeconds(0.7f);
	}

	public void TakeDamage(Weapon weapon, in Vector3 forward)
	{
		if (attackableComp == null)
		{ return; }

		takeDamageTimer = takeDamageDelay;
		DamageEffect(forward);
		attackableComp.TakeDamage(this, weapon, ref health);
	}

	public void SetRightItem(IEquipable equipableItem)
	{
		if (useItemsComp == null)
		{ return; }

		GameObject spawnedItemObj = useItemsComp.SetItemToCharacter(rightItemPlaceholder, equipableItem);
		rightItemCollider = spawnedItemObj.GetComponent<Collider>();
	}

	public void SetLeftItem(IEquipable equipableItem)
	{
		if (useItemsComp == null)
		{ return; }

		GameObject spawnedItemObj = useItemsComp.SetItemToCharacter(leftItemPlaceholder, equipableItem);
		leftItemCollider = spawnedItemObj.GetComponent<Collider>();
	}

	public void RemoveRightItem()
	{
		if (useItemsComp == null)
		{ return; }
		if (rightItemPlaceholder.childCount == 0)
		{
			Logger.SendError(gameObject:gameObject, "No item to remove");
			return;
		}

		useItemsComp.RemoveItemFromCharacter(rightItemPlaceholder.GetChild(0).gameObject);
		rightItemCollider = null;
	}

	public void RemoveLeftItem()
	{
		if (useItemsComp == null)
		{ return; }
		if (leftItemPlaceholder.childCount == 0)
		{
			Logger.SendError(gameObject: gameObject, "No item to remove");
			return;
		}

		useItemsComp.RemoveItemFromCharacter(leftItemPlaceholder.GetChild(0).gameObject);
		leftItemCollider = null;
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
			Logger.NullParameter<ContainerSlot>(gameObject);
			return;
		}

		if (containerSlot.Item is ItemUsable item)
		{
			ChangeHealth(item.HealthInfluenceValue);
			ChangeStamina(item.StaminaInfluenceValue);
			containerSlot.RemoveItem(item, 1);
		}
		else
		{ Logger.SendError(gameObject: gameObject, "The item cannot be used"); }
		
	}

	public void Die()
	{
	}

	private protected void Initialize()
	{
		animatorComp = GetComponent<Animator>();
		rigidBodyComp = GetComponent<Rigidbody>();
		attackableComp = GetComponent<Attackable>();
		useItemsComp = GetComponent<UseItems>();
		material = GetComponentInChildren<SkinnedMeshRenderer>().material;

		rightItemPlaceholder = Utils.FindInChildrensByObjName(transform, "$RightItemPlaceholder$");
		leftItemPlaceholder = Utils.FindInChildrensByObjName(transform, "$LeftItemPlaceholder$");
		//rightItemHidedHolder = Utils.FindInChildrensByObjName(transform, "RightItemHidedHolder");
		//leftItemHidedHolder = Utils.FindInChildrensByObjName(transform, "LeftItemHidedHolder");
		//backItemHidedHolder = Utils.FindInChildrensByObjName(transform, "BackItemHidedHolder");
	}

	private void Update()
	{
		if (takeDamageTimer > 0)
		{ takeDamageTimer -= Time.deltaTime; }
	}

	private void Awake()
	{
		Initialize();
	}

	private void OnTriggerEnter(Collider enteredCollider)
	{
		if (takeDamageTimer > 0) 
		{
			return;
		}
		if (enteredCollider != rightItemCollider && enteredCollider != leftItemCollider && enteredCollider.tag == "Weapon")
		{
			if (enteredCollider.TryGetComponent(out WorldItem worldItem))
			{
				Debug.Log("Triggered");

				TakeDamage((Weapon)worldItem.Item, worldItem.transform.forward);
			}
		}
		//if (enteredCollider.TryGetComponent(out WorldItem worldItem) && worldItem.Item != rightHoldingItem)
		//{
		//	if (worldItem.Item is Weapon weapon)
		//	{ 
		//		TakeDamage(weapon);
		//		rigidBodyComp.AddForce(new Vector3(0, 35, 35), ForceMode.Impulse);
		//	}
		//}
	}
}
