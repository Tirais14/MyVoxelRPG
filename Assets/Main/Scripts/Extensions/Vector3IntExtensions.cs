using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3IntExtensions
{
	public static Vector3IntReadOnly ToReadOnly(this Vector3Int vector3Int) => new(vector3Int);

	public static Vector3ReadOnly ToReadOnlyF(this Vector3Int vector3Int) => new(vector3Int);

	public static Vector2Int To2DReadOnly(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.z);

	public static Vector2 To2DReadOnlyF(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.z);

	public static Vector2Int To2DCoords(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.z);

	public static Vector2 To2DCoordsF(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.z);
}
