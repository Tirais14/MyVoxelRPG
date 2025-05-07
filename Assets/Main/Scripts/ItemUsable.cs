using UnityEngine;

[CreateAssetMenu(fileName = "Item Usable", menuName = "Item Usable")]
public class ItemUsable : Item
{
	[Header("Effect Settings")]
	[SerializeField] private float healthInfluenceValue;
	public float HealthInfluenceValue => healthInfluenceValue;
	[SerializeField] private float staminaInfluenceValue;
	public float StaminaInfluenceValue => staminaInfluenceValue;
}
