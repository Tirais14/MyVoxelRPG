using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public readonly struct Vector3IntReadOnly
{
	public readonly int x, y, z;

	public Vector3IntReadOnly(Vector3Int vector3Int)
	{
		x = vector3Int.x;
		y = vector3Int.y;
		z = vector3Int.z;
	}
	public Vector3IntReadOnly(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3 ToVector3() => new(x, y, z);

	public Vector3Int ToVector3Int() => new(x, y, z);

	public Vector2 To2DCoordsF() => new(x, z);

	public Vector2Int To2DCoords() => new(x, z);

	public static Vector3 operator +(Vector3 left, Vector3IntReadOnly right)
	{
		return new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3Int operator +(Vector3Int left, Vector3IntReadOnly right)
	{
		return new Vector3Int(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3IntReadOnly operator +(Vector3IntReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3IntReadOnly(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3IntReadOnly operator +(Vector3IntReadOnly left, Vector3Int right)
	{
		return new Vector3IntReadOnly(left.x + right.x, left.y + right.y, left.z + right.z);
	}


	public static Vector3 operator -(Vector3 left, Vector3IntReadOnly right)
	{
		return new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3Int operator -(Vector3Int left, Vector3IntReadOnly right)
	{
		return new Vector3Int(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3IntReadOnly operator -(Vector3IntReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3IntReadOnly(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3IntReadOnly operator -(Vector3IntReadOnly left, Vector3Int right)
	{
		return new Vector3IntReadOnly(left.x - right.x, left.y - right.y, left.z - right.z);
	}


	public static Vector3 operator *(Vector3 left, Vector3IntReadOnly right)
	{
		return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3Int operator *(Vector3Int left, Vector3IntReadOnly right)
	{
		return new Vector3Int(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3IntReadOnly operator *(Vector3IntReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3IntReadOnly(left.x * right.x, left.y * right.y, left.z *right.z);
	}
	public static Vector3IntReadOnly operator *(Vector3IntReadOnly left, Vector3Int right)
	{
		return new Vector3IntReadOnly(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3IntReadOnly operator *(Vector3IntReadOnly left, int right)
	{
		return new Vector3IntReadOnly(left.x * right, left.y * right, left.z * right);
	}


	public static Vector3 operator /(Vector3 left, Vector3IntReadOnly right)
	{
		return new Vector3(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3Int operator /(Vector3Int left, Vector3IntReadOnly right)
	{
		return new Vector3Int(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3IntReadOnly operator /(Vector3IntReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3IntReadOnly(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3IntReadOnly operator /(Vector3IntReadOnly left, Vector3Int right)
	{
		return new Vector3IntReadOnly(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3IntReadOnly operator /(Vector3IntReadOnly left, int right)
	{
		return new Vector3IntReadOnly(left.x / right, left.y / right, left.z / right);
	}
}
