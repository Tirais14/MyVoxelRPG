using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utils
{
	/// <returns>Returns true if the number is even</returns>
	public static bool ParityCheck(int a)
	{
		if ((a % 2) != 0)
		{ return false; }

		return true;
	}

	public static ReadOnlyCollection<GameObject> GetUnderPointerUIGameObjects()
	{
		if (!GameManagers.Controlls.IsPointerOverGameObject)
		{ return null; }

		if (GlobalProperties.PointerEventData == null)
		{
			PointerEventData tempPointerEventData = new PointerEventData(EventSystem.current);
			tempPointerEventData.position = Input.mousePosition;
			return tempPointerEventData.hovered.AsReadOnly();
		}

		return GlobalProperties.PointerEventData.hovered.AsReadOnly();
	}

	public static ReadOnlyCollection<RaycastResult> GetUnderPointerUIRaycastResults()
	{
		if (!GameManagers.Controlls.IsPointerOverGameObject)
		{ return null; }

		List<RaycastResult> results = new();
		if (GlobalProperties.PointerEventData == null)
		{
			PointerEventData tempPointerEventData = new PointerEventData(EventSystem.current);
			tempPointerEventData.position = Input.mousePosition;

			GlobalProperties.GraphicRaycaster.Raycast(tempPointerEventData, results);
		}

		GlobalProperties.GraphicRaycaster.Raycast(GlobalProperties.PointerEventData, results);
		return results.AsReadOnly();
	}

	public static Transform FindInParentsByObjName(Transform childObject, string searhicngObjectName)
	{
		Transform tempParent = childObject.parent;
		while (tempParent != null)
		{
			if (tempParent.name == searhicngObjectName)
			{ return tempParent; }
			tempParent = tempParent.parent;
		}

		return null;
	}

	public static bool TryFindInParentsByObjName(Transform childObject, string searhicngObjectName, out Transform foundObject)
	{
		Transform tempParent = childObject.parent;
		while (tempParent != null)
		{
			if (tempParent.name == searhicngObjectName)
			{
				foundObject = tempParent;
				return true; 
			}
			tempParent = tempParent.parent;
		}


		foundObject = null;
		return false;
	}

	public static Transform FindInChildrensByObjName(Transform parentObj, string searchingObjName)
	{
		if (parentObj.childCount == 0)
		{
			Logger.SendError("No Game Object Childs");
			return null;
		}
		if (parentObj == null)
		{
			Logger.NullParameter<Transform>();
			return null;
		}
		if (searchingObjName == null)
		{
			Logger.NullParameter<string>();
			return null;
		}
		if (searchingObjName == string.Empty)
		{
			Logger.SendError("Searching Object Name is Empty");
			return null;
		}

		Transform tempChild;
		for (int i = 0; i < parentObj.childCount; i++)
		{
			tempChild = parentObj.GetChild(i);
			if (tempChild.name == searchingObjName)
			{ return tempChild; }
			else if (tempChild.childCount > 0)
			{ 
				tempChild = FindInChildrensByObjName(tempChild, searchingObjName);
				if (tempChild != null && tempChild.name == searchingObjName)
				{ return tempChild; }
			}
		}

		return null;
	}

	public static bool TryFindInChildrensByObjName(in Transform parentObject, string searhicngObjectName, out Transform foundObj)
	{
		foundObj = FindInChildrensByObjName(parentObject, searhicngObjectName);
		if (foundObj != null)
		{ return true; }

		foundObj = null;
		return false;
	}

	//private static void FindInChildrensByObjNamesFunc(in List<Transform> foundObjs, in Transform parentObj, string[] objNames)
	//{
	//	Transform tempChild;
	//	for (int i = 0; i < parentObj.childCount; i++)
	//	{
	//		tempChild = parentObj.GetChild(i);

	//		if (objNames.Exists(tempChild.name))
	//		{ foundObjs.Add(tempChild); }

	//		if (tempChild.childCount > 0)
	//		{ FindInChildrensByObjNamesFunc(foundObjs, tempChild, objNames); }
	//	}
	//}
	private static void FindInChildrensByObjNamesFunc(in List<Transform> foundObjs, in Transform parentObj, in string[] objNames)
	{
		Transform tempChild;
		for (int i = 0; i < parentObj.childCount; i++)
		{
			tempChild = parentObj.GetChild(i);

			if (objNames.Exists(tempChild.name))
			{ foundObjs.Add(tempChild); }

			if (tempChild.childCount > 0)
			{ FindInChildrensByObjNamesFunc(foundObjs, tempChild, objNames); }
		}
	}
	private static void FindInChildrensByObjNamesFunc(in List<Transform> foundObjs, in Transform parentObj, in Regex[] objNames)
	{
		Transform tempChild;
		for (int i = 0; i < parentObj.childCount; i++)
		{
			tempChild = parentObj.GetChild(i);

			if (objNames.MatchExists(tempChild.name))
			{ foundObjs.Add(tempChild); }

			if (tempChild.childCount > 0)
			{ FindInChildrensByObjNamesFunc(foundObjs, tempChild, objNames); }
		}
	}

	public static Transform[] FindInChildrensByObjNames(in Transform parentObj, params string[] objNames)
	{
		if (parentObj.childCount == 0)
		{
			Logger.SendError("No Game Object Childs");
			return null;
		}
		if (parentObj == null)
		{
			Logger.NullParameter<Transform>();
			return null;
		}
		if (objNames == null)
		{
			Logger.NullParameter<string>();
			return null;
		}

		List<Transform> foundObjs = new(objNames.Length);
		FindInChildrensByObjNamesFunc(foundObjs, parentObj, objNames);

		if (foundObjs.Count > 0)
		{ return foundObjs.ToArray(); }
		else
		{ return null; }
	}
	public static Transform[] FindInChildrensByObjNames(in Transform parentObj, params Regex[] objNames)
	{
		if (parentObj.childCount == 0)
		{
			Logger.SendError("No Game Object Childs");
			return null;
		}
		if (parentObj == null)
		{
			Logger.NullParameter<Transform>();
			return null;
		}
		if (objNames == null)
		{
			Logger.NullParameter<Regex>();
			return null;
		}

		List<Transform> foundObjs = new(objNames.Length);
		FindInChildrensByObjNamesFunc(foundObjs, parentObj, objNames);

		if (foundObjs.Count > 0)
		{ return foundObjs.ToArray(); }
		else
		{ return null; }
	}

	public static void FindInChildrensByObjNames(in List<Transform> foundObjs, in Transform parentObj, params string[] objNames)
	{
		if (foundObjs == null)
		{
			Logger.NullParameter<List<Transform>>();
			return;
		}
		if (parentObj.childCount == 0)
		{
			Logger.SendError("No Game Object Childs");
			return;
		}
		if (parentObj == null)
		{
			Logger.NullParameter<Transform>();
			return;
		}
		if (objNames == null)
		{
			Logger.NullParameter<string>();
			return;
		}

		FindInChildrensByObjNamesFunc(foundObjs, parentObj, objNames);
	}
	public static void FindInChildrensByObjNames(in List<Transform> foundObjs, in Transform parentObj, params Regex[] objNames)
	{
		if (foundObjs == null)
		{
			Logger.NullParameter<List<Transform>>();
			return;
		}
		if (parentObj.childCount == 0)
		{
			Logger.SendError("No Game Object Childs");
			return;
		}
		if (parentObj == null)
		{
			Logger.NullParameter<Transform>();
			return;
		}
		if (objNames == null)
		{
			Logger.NullParameter<Regex>();
			return;
		}

		FindInChildrensByObjNamesFunc(foundObjs, parentObj, objNames);
	}

	/// <summary>Results array must be bigger or equals of colliders array. No memory allocation</summary>
	/// ///<returns>Returns the number of colliders matching the condition</returns>
	public static int FindInCollidersByComponentNonAloc<ComponentClass>(Collider[] colliders, Collider[] results)
	{
		if (colliders == null)
		{ return 0; }
		if (results.Length > colliders.Length)
		{ throw new Exception("Results array must be bigger or equals of colliders array"); }

		Array.Clear(results, 0, results.Length);
		int resultsCount = 0;
		for (int i = 0; i < results.Length; i++)
		{
			if (colliders[i] != null && colliders[i].GetComponent<ComponentClass>() != null)
			{
				results[resultsCount] = colliders[i];
				resultsCount++;
			}
		}

		return resultsCount;
	}
	public static int FindInCollidersByComponentNonAloc<ComponentClass>(Collider[] colliders, in int collidersCount, Collider[] results)
	{
		if (colliders == null)
		{ return 0; }
		if (results.Length > colliders.Length)
		{ throw new Exception("Results array must be bigger or equals of colliders array"); }

		if (results.Count() > 0)
		{ Array.Clear(results, 0, results.Length); }
		int resultsCount = 0;
		for (int i = 0; i < collidersCount; i++)
		{
			if (colliders[i] != null && colliders[i].GetComponent<ComponentClass>() != null)
			{
				results[resultsCount] = colliders[i];
				resultsCount++;
			}
		}

		return resultsCount;
	}

	public static T GetUnderPointerComponent<T>()
	{
		ReadOnlyCollection<GameObject> underPointerUIObjs = GetUnderPointerUIGameObjects();
		if (underPointerUIObjs == null)
		{ return default; }

		for (int i = 0; i < underPointerUIObjs.Count; i++)
		{
			if (underPointerUIObjs[i].TryGetComponent(out T component))
			{ return component; }
		}

		return default;
	}

	public static bool TryGetUnderPointerComponent<T>(out T component)
	{
		component = GetUnderPointerComponent<T>();
		if (component != null)
		{ return true; }

		return false;
	}
}
