using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Jobs;

public static class GameObjectExtensions
{
	/// <summary>
	/// Searches for a parents GameObject with the specified name and returns the specified component
	/// </summary>
	/// <param name="parentObjectName">Name of the parent GameObejct from which you want to get the component</param>
	public static T GetComponentByObjNameInParent<T>(this GameObject obj, string parentObjectName)
	{
		if (Utils.TryFindInParentsByObjName(obj.transform, parentObjectName, out Transform foundObject))
		{ return foundObject.GetComponent<T>(); }

		return default;
	}

	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified component
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the component</param>
	public static T GetComponentByObjNameInChildren<T>(this GameObject obj, string childObjectName)
	{
		if (Utils.TryFindInChildrensByObjName(obj.transform, childObjectName, out Transform foundObject))
		{ return foundObject.GetComponent<T>(); }

		return default;
	}

	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2>(this GameObject obj,
		string objName1, 
		string objName2,
		out T1 cmp1,
		out T2 cmp2)
		where T1 : Component
		where T2 : Component
	{
		Transform[] foundObjs = Utils.FindInChildrensByObjNames(obj.transform, objName1, objName2);
		cmp1 = null;
		cmp2 = null;
		if (foundObjs == null)
		{ return; }

		for (int i = 0; i < foundObjs.Length; i++)
		{
			if (cmp1 == null && objName1 != string.Empty && foundObjs[i].name == objName1)
			{
				cmp1 = foundObjs[i].GetComponent<T1>();
				if (cmp1 != null)
				{ objName1 = string.Empty; }
			}
			else if (cmp2 == null && objName2 != string.Empty && foundObjs[i].name == objName2)
			{
				cmp2 = foundObjs[i].GetComponent<T2>();
				if (cmp2 != null)
				{ objName2 = string.Empty; }
			}
		}
	}
	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2, T3>(this GameObject obj,
		string objName1,
		string objName2,
		string objName3,
		out T1 cmp1,
		out T2 cmp2,
		out T3 cmp3)
		where T1 : Component
		where T2 : Component
		where T3 : Component
	{
		Transform[] foundObjs = Utils.FindInChildrensByObjNames(obj.transform, objName1, objName2, objName3);
		cmp1 = null;
		cmp2 = null;
		cmp3 = null;
		if (foundObjs == null)
		{ return; }

		for (int i = 0; i < foundObjs.Length; i++)
		{
			if (cmp1 == null && objName1 != string.Empty && foundObjs[i].name == objName1)
			{
				cmp1 = foundObjs[i].GetComponent<T1>();
				if (cmp1 != null)
				{ objName1 = string.Empty; }
			}
			else if (cmp2 == null && objName2 != string.Empty && foundObjs[i].name == objName2)
			{
				cmp2 = foundObjs[i].GetComponent<T2>();
				if (cmp2 != null)
				{ objName2 = string.Empty; }
			}
			else if (cmp3 == null && objName3 != string.Empty && foundObjs[i].name == objName3)
			{
				cmp3 = foundObjs[i].GetComponent<T3>();
				if (cmp3 != null)
				{ objName3 = string.Empty; }
			}
		}
	}
	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2, T3, T4>(this GameObject obj,
		string objName1,
		string objName2,
		string objName3,
		string objName4,
		out T1 cmp1,
		out T2 cmp2,
		out T3 cmp3,
		out T4 cmp4)
		where T1 : Component
		where T2 : Component
		where T3 : Component
		where T4 : Component
	{
		Transform[] foundObjs = Utils.FindInChildrensByObjNames(obj.transform, objName1, objName2, objName3, objName4);
		cmp1 = null;
		cmp2 = null;
		cmp3 = null;
		cmp4 = null;
		if (foundObjs == null)
		{ return; }

		for (int i = 0; i < foundObjs.Length; i++)
		{
			if (cmp1 == null && objName1 != string.Empty && foundObjs[i].name == objName1)
			{
				cmp1 = foundObjs[i].GetComponent<T1>();
				if (cmp1 != null)
				{ objName1 = string.Empty; }
			}
			else if (cmp2 == null && objName2 != string.Empty && foundObjs[i].name == objName2)
			{
				cmp2 = foundObjs[i].GetComponent<T2>();
				if (cmp2 != null)
				{ objName2 = string.Empty; }
			}
			else if (cmp3 == null && objName3 != string.Empty && foundObjs[i].name == objName3)
			{
				cmp3 = foundObjs[i].GetComponent<T3>();
				if (cmp3 != null)
				{ objName3 = string.Empty; }
			}
			else if (cmp4 == null && objName4 != string.Empty && foundObjs[i].name == objName4)
			{
				cmp4 = foundObjs[i].GetComponent<T4>();
				if (cmp4 != null)
				{ objName4 = string.Empty; }
			}
		}
	}
	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2, T3, T4, T5>(this GameObject obj,
		string objName1,
		string objName2,
		string objName3,
		string objName4,
		string objName5,
		out T1 cmp1,
		out T2 cmp2,
		out T3 cmp3,
		out T4 cmp4,
		out T5 cmp5)
		where T1 : Component
		where T2 : Component
		where T3 : Component
		where T4 : Component
		where T5 : Component
	{
		Transform[] foundObjs = Utils.FindInChildrensByObjNames(obj.transform, objName1, objName2, objName3, objName4, objName5);
		cmp1 = null;
		cmp2 = null;
		cmp3 = null;
		cmp4 = null;
		cmp5 = null;
		if (foundObjs == null)
		{ return; }

		for (int i = 0; i < foundObjs.Length; i++)
		{
			if (cmp1 == null && objName1 != string.Empty && foundObjs[i].name == objName1)
			{
				cmp1 = foundObjs[i].GetComponent<T1>();
				if (cmp1 != null)
				{ objName1 = string.Empty; }
			}
			else if (cmp2 == null && objName2 != string.Empty && foundObjs[i].name == objName2)
			{
				cmp2 = foundObjs[i].GetComponent<T2>();
				if (cmp2 != null)
				{ objName2 = string.Empty; }
			}
			else if (cmp3 == null && objName3 != string.Empty && foundObjs[i].name == objName3)
			{
				cmp3 = foundObjs[i].GetComponent<T3>();
				if (cmp3 != null)
				{ objName3 = string.Empty; }
			}
			else if (cmp4 == null && objName4 != string.Empty && foundObjs[i].name == objName4)
			{
				cmp4 = foundObjs[i].GetComponent<T4>();
				if (cmp4 != null)
				{ objName4 = string.Empty; }
			}
			else if (cmp5 == null && objName5 != string.Empty && foundObjs[i].name == objName5)
			{ 
				cmp5 = foundObjs[i].GetComponent<T5>();
				if (cmp5 != null)
				{ objName5 = string.Empty; }
			}
		}
	}
}
