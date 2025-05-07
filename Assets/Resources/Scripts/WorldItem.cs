using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
	private Item item;
	public Item Item => item;
	//public Type Type {  get; private set; }

	//private void Awake()
	//{
	//	item = ItemDatabase.GetItem(gameObject.name);
	//}

	//public T GetItem<T>(T type)
	//	where T : Type
	//{ return item as T; }

	private void Start()
	{
		item = ItemDatabase.GetItem(gameObject.name.TrimEnd("(Clone)"));
		//Type = item.GetType();
	}
}
