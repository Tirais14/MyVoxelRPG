using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
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

	public static ReadOnlyCollection<RaycastResult> GetUnderPointerRaycastsResults()
	{
		List<RaycastResult> results = new();
		GlobalProperties.GraphicRaycaster.Raycast(GlobalProperties.PointerEventData, results);
		return results.AsReadOnly();
	}

	public static Transform FindInChildrens(Transform parentObject, string searhicngObjectName)
	{
		Transform tempChild;
		for (int i = 0; i < parentObject.childCount; i++)
		{
			tempChild = parentObject.GetChild(i);
			if (tempChild.name == searhicngObjectName)
			{ return tempChild; }
		}

		for (int i = 0; i < parentObject.childCount; i++)
		{
			tempChild = parentObject.GetChild(i);
			if (tempChild.childCount > 0 && TryFindInChildrens(tempChild, searhicngObjectName, out Transform foundObject))
			{ return foundObject; }
		}

		return null;
	}

	public static bool TryFindInChildrens(Transform parentObject, string searhicngObjectName, out Transform foundObject)
	{
		Transform tempChild;
		for (int i = 0; i < parentObject.childCount; i++)
		{
			tempChild = parentObject.GetChild(i);
			if (tempChild.name == searhicngObjectName)
			{
				foundObject = tempChild;
				return true;
			}
		}

		for (int i = 0; i < parentObject.childCount; i++)
		{
			tempChild = parentObject.GetChild(i);
			if (tempChild.childCount > 0 && TryFindInChildrens(tempChild, searhicngObjectName, out foundObject))
			{ return true; }
		}

		foundObject = null;
		return false;
	}

	/// <summary>
	/// Searches for a game object with the specified name and returns the specified component
	/// </summary>
	public static T GetComponentInChildren<T>(this Component component, string searchingObjectName)
		where T : Component
	{
		if (TryFindInChildrens(component.transform, searchingObjectName, out Transform foundObject))
		{ return foundObject.GetComponent<T>(); }

		return null;
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
}
