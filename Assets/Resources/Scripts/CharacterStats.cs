using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
	[SerializeField] private float baseHealthMax;
	public float BaseHealthMax => baseHealthMax;
	[SerializeField] private float baseStaminaMax;
	public float BaseStaminaMax => baseStaminaMax;
}