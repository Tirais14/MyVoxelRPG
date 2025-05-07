using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct Vector3ReadOnly
{
	public readonly float x, y, z;

	public Vector3ReadOnly(Vector3 vector3)
	{
		x = vector3.x;
		y = vector3.y;
		z = vector3.z;
	}
	public Vector3ReadOnly(Vector3Int vector3)
	{
		x = vector3.x;
		y = vector3.y;
		z = vector3.z;
	}
	public Vector3ReadOnly(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3 ToVector3() => new(x, y, z);

	public Vector2 To2DCoords() => new(x, z);

	public static Vector3 operator +(Vector3 left, Vector3ReadOnly right)
	{
		return new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3ReadOnly operator +(Vector3ReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3ReadOnly(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3ReadOnly operator +(Vector3ReadOnly left, Vector3ReadOnly right)
	{
		return new Vector3ReadOnly(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3ReadOnly operator +(Vector3ReadOnly left, Vector3Int right)
	{
		return new Vector3ReadOnly(left.x + right.x, left.y + right.y, left.z + right.z);
	}
	public static Vector3ReadOnly operator +(Vector3ReadOnly left, Vector3 right)
	{
		return new Vector3ReadOnly(left.x + right.x, left.y + right.y, left.z + right.z);
	}


	public static Vector3 operator -(Vector3 left, Vector3ReadOnly right)
	{
		return new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3ReadOnly operator -(Vector3ReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3ReadOnly(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3ReadOnly operator -(Vector3ReadOnly left, Vector3ReadOnly right)
	{
		return new Vector3ReadOnly(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3ReadOnly operator -(Vector3ReadOnly left, Vector3Int right)
	{
		return new Vector3ReadOnly(left.x - right.x, left.y - right.y, left.z - right.z);
	}
	public static Vector3ReadOnly operator -(Vector3ReadOnly left, Vector3 right)
	{
		return new Vector3ReadOnly(left.x - right.x, left.y - right.y, left.z - right.z);
	}


	public static Vector3 operator *(Vector3 left, Vector3ReadOnly right)
	{
		return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3ReadOnly operator *(Vector3ReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3ReadOnly(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3ReadOnly operator *(Vector3ReadOnly left, Vector3ReadOnly right)
	{
		return new Vector3ReadOnly(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3ReadOnly operator *(Vector3ReadOnly left, Vector3Int right)
	{
		return new Vector3ReadOnly(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3ReadOnly operator *(Vector3ReadOnly left, Vector3 right)
	{
		return new Vector3ReadOnly(left.x * right.x, left.y * right.y, left.z * right.z);
	}
	public static Vector3ReadOnly operator *(Vector3ReadOnly left, int right)
	{
		return new Vector3ReadOnly(left.x * right, left.y * right, left.z * right);
	}
	public static Vector3ReadOnly operator *(Vector3ReadOnly left, float right)
	{
		return new Vector3ReadOnly(left.x * right, left.y * right, left.z * right);
	}


	public static Vector3 operator /(Vector3 left, Vector3ReadOnly right)
	{
		return new Vector3(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3ReadOnly operator /(Vector3ReadOnly left, Vector3IntReadOnly right)
	{
		return new Vector3ReadOnly(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3ReadOnly operator /(Vector3ReadOnly left, Vector3ReadOnly right)
	{
		return new Vector3ReadOnly(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3ReadOnly operator /(Vector3ReadOnly left, Vector3Int right)
	{
		return new Vector3ReadOnly(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3ReadOnly operator /(Vector3ReadOnly left, Vector3 right)
	{
		return new Vector3ReadOnly(left.x / right.x, left.y / right.y, left.z / right.z);
	}
	public static Vector3ReadOnly operator /(Vector3ReadOnly left, int right)
	{
		return new Vector3ReadOnly(left.x / right, left.y / right, left.z / right);
	}
	public static Vector3ReadOnly operator /(Vector3ReadOnly left, float right)
	{
		return new Vector3ReadOnly(left.x / right, left.y / right, left.z / right);
	}
}
