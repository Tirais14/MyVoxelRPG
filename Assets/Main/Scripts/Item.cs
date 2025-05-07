using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
	[SerializeField] private GameObject prefab;
	public GameObject Prefab => prefab;

	[Header("CommonSettings")]
	[SerializeField] private int id = -1;
	public int Id => id;
	[SerializeField] private new string name = "Unnamed";
	public string Name => name;
	[TextArea(4, 12)]
	[SerializeField] private string description;
	public string Description => description;
	[SerializeField] private Sprite icon;
	public Sprite Icon => icon;
	[SerializeField] private float cost;
	public float Cost => cost;
	//[SerializeField] private bool equipable;
	//public bool Equipable => equipable;
	//[SerializeField] private bool usable;
	//public bool Usable => usable;
	public enum Type : byte
	{
		Common,
		Usable,
		Equipable
	}
	[SerializeField] private Type type = Type.Common;
	public Type TypeProp => type;
	

	[Header("Stack Settings")]
	[SerializeField] private bool stackable = false;
	public bool Stackable => stackable;
	[SerializeField] private ushort maxCount = 1;
	public ushort MaxCount => maxCount;
}
