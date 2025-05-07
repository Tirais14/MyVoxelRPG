using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
	/// <summary>
	/// Searches for a parents GameObject with the specified name and returns the specified component
	/// </summary>
	/// <param name="parentObjectName">Name of the parent GameObejct from which you want to get the component</param>
	public static T GetComponentByObjNameInParent<T>(this Component component, string parentObjectName)
	{ return component.gameObject.GetComponentByObjNameInParent<T>(parentObjectName); }

	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified component
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the component</param>
	public static T GetComponentByObjNameInChildren<T>(this Component component, string childObjectName)
	{ return component.gameObject.GetComponentByObjNameInChildren<T>(childObjectName); }

	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2>(this Component component,
																	string objName1,
																	string objName2,
																	out T1 cmp1,
																	out T2 cmp2)
		where T1 : Component
		where T2 : Component
	{ component.gameObject.GetComponentsByObjNamesInChildren(objName1, objName2, out cmp1, out cmp2); }
	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2, T3>(this Component component,
																		string objName1,
																		string objName2,
																		string objName3,
																		out T1 cmp1,
																		out T2 cmp2,
																		out T3 cmp3)
		where T1 : Component
		where T2 : Component
		where T3 : Component
	{ component.gameObject.GetComponentsByObjNamesInChildren(objName1, objName2, objName3, out cmp1, out cmp2, out cmp3); }
	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2, T3, T4>(this Component component,
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
		component.gameObject.GetComponentsByObjNamesInChildren(objName1,
																objName2,
																objName3,
																objName4,
																out cmp1,
																out cmp2,
																out cmp3,
																out cmp4);
	}
	/// <summary>
	/// Searches for a childrens GameObejct with the specified name and returns the specified components
	/// </summary>
	/// <param name="childObjectName">Name of the child GameObejct from which you want to get the components</param>
	public static void GetComponentsByObjNamesInChildren<T1, T2, T3, T4, T5>(this Component component,
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
		component.gameObject.GetComponentsByObjNamesInChildren(objName1,
																objName2,
																objName3,
																objName4,
																objName5,
																out cmp1,
																out cmp2,
																out cmp3,
																out cmp4,
																out cmp5);
	}
}
