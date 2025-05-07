using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContainerSlotSettings", menuName = "Container Slot Settings")]
public class ContainerSlotInfo : ScriptableObject
{
	[SerializeField] private protected GameObject containerSlotPrefab;
	public GameObject ContainerSlotPrefab => containerSlotPrefab;
}
