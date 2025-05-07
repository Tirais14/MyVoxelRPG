using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public static class ArrayExtensions
{
	///// ///<returns>True if the given object exists</returns>
	//public static bool Exists<T>(this T[] array, T obj)
	//{
	//	bool EqualsItem(T arrayItem)
	//	{
	//		if (arrayItem.Equals(obj))
	//		{ return true; }

	//		return false;
	//	}

	//	for (int i = 0; i < array.Length; i++)
	//	{
	//		if (Array.Exists(array, EqualsItem))
	//		{ return true; }
	//	}

	//	return false;
	//}
	/// ///<returns>True if the given object exists</returns>
	public static bool Exists(this string[] array, string str)
	{
		bool EqualsString(string arrayStr)
		{
			if (arrayStr == str && arrayStr != null && str != null)
			{ return true; }

			return false;
		}

		for (int i = 0; i < array.Length; i++)
		{
			if (Array.Exists(array, EqualsString))
			{ return true; }
		}

		return false;
	}
	public static bool Exists(this int[] array, int value)
	{
		bool EqualsInt(int arrayValue)
		{
			if (arrayValue == value)
			{ return true; }

			return false;
		}

		for (int i = 0; i < array.Length; i++)
		{
			if (Array.Exists(array, EqualsInt))
			{ return true; }
		}

		return false;
	}
	public static bool MatchExists(this Regex[] array, string value)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsMatch(value))
			{ return true; }
		}

		return false;
	}

	public static GameObject[] ToGameObjectArray(this Component[] components)
	{
		if (components == null)
		{ return null; }

		GameObject[] gameObjects = new GameObject[components.Length];
		for(int i = 0;i < components.Length;i++)
		{ gameObjects[i] = components[i].gameObject; }
		return gameObjects;
	}

	public static Vector3[] ToVector3Array(this BlockMeshVertex[] cubeVertices)
	{
		Vector3[] vertecies = new Vector3[cubeVertices.Length];

		for (int i = 0; i < cubeVertices.Length; i++)
		{ vertecies[i] = cubeVertices[i].ToVector3(); }

		return vertecies;
	}
	public static Vector3[] ToVector3Array(this List<BlockMeshVertex> cubeVertices)
	{
		Vector3[] vertecies = new Vector3[cubeVertices.Count];

		for (int i = 0; i < cubeVertices.Count; i++)
		{ vertecies[i] = cubeVertices[i].ToVector3(); }

		return vertecies;
	}

	public static bool Exists(this List<BlockMeshVertex> cubeVertices, BlockMeshVertex cubeVertex)
	{
		for (int i = 0; i < cubeVertices.Count; i++)
		{
			if (cubeVertices[i].Equals(cubeVertex))
			{  return true; }
		}

		return false;
	}
	public static bool Contains(this BlockMeshSide[] faces, BlockMeshSide face)
	{
		for (int i = 0; i < faces.Length; i++)
		{
			if (faces[i] == face)
			{ return true; }
		}

		return false;
	}
	public static bool Contains(this BlockMeshSide[] faces, params BlockMeshSide[] searchingFaces)
	{
		int matchesFound = 0;

		for (int i = 0; i < faces.Length; i++)
		{
			for (int y = 0; y < searchingFaces.Length; y++)
			{
				if (faces[i] == searchingFaces[y])
				{ matchesFound++; }
			}
		}

		if (matchesFound == searchingFaces.Length)
		{ return true; }

		return false;
	}

	public static int[] ConcatWithOffset(this int[] ints, int[] intsToAdd, int offsetValue)
	{
		int[] resultInts = new int[ints.Length + intsToAdd.Length];
		ints.CopyTo(resultInts, 0);

		for (int i = 0; i < intsToAdd.Length; i++)
		{ intsToAdd[i] += offsetValue; }

		intsToAdd.CopyTo(resultInts, ints.Length);

		return resultInts;
	}

	//public static CubeMeshVertex[] Concat(this CubeMeshVertex[] vertices, CubeMeshVertex[] verticesToAdd)
	//{
	//	CubeMeshVertex[] resultVertices = new CubeMeshVertex[vertices.Length + verticesToAdd.Length];

	//	for (int i = 0; i < vertices.Length; i++)
	//	{ resultVertices[i] = vertices[i]; }
	//	for (int i = 0; i < verticesToAdd.Length; i++)
	//	{ resultVertices[i + vertices.Length] = verticesToAdd[i]; }

	//	return resultVertices;
	//}

	public static void AddRange(this BlockMeshVertex[] vertices, BlockMeshVertex[] verticesToAdd)
	{
		int oldArrayLength = vertices.Length;
		Array.Resize(ref vertices, vertices.Length + verticesToAdd.Length);
		Array.Copy(verticesToAdd, 0, vertices, oldArrayLength, verticesToAdd.Length);
	}

	public static void SetOffsetForEach(this BlockMeshVertex[] vertices, Vector3Int offset)
	{
		for (int i = 0; i < vertices.Length; i++)
		{ vertices[i].SetOffset(offset); }
	}
}
