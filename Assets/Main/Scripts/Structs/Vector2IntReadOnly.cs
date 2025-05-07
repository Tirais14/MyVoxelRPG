using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct Vector2IntReadOnly
{
	public readonly int x, y;

	public Vector2IntReadOnly(Vector2Int vector2Int)
	{
		x = vector2Int.x;
		y = vector2Int.y;
	}
	public Vector2IntReadOnly(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public static Vector2 operator +(Vector2 left, Vector2IntReadOnly right)
	{
		return new Vector2(left.x + right.x, left.y + right.y);
	}
	public static Vector2Int operator +(Vector2Int left, Vector2IntReadOnly right)
	{
		return new Vector2Int(left.x + right.x, left.y + right.y);
	}
	public static Vector2IntReadOnly operator +(Vector2IntReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2IntReadOnly(left.x + right.x, left.y + right.y);
	}
	public static Vector2IntReadOnly operator +(Vector2IntReadOnly left, Vector2Int right)
	{
		return new Vector2IntReadOnly(left.x + right.x, left.y + right.y);
	}


	public static Vector2 operator -(Vector2 left, Vector2IntReadOnly right)
	{
		return new Vector2(left.x - right.x, left.y - right.y);
	}
	public static Vector2Int operator -(Vector2Int left, Vector2IntReadOnly right)
	{
		return new Vector2Int(left.x - right.x, left.y - right.y);
	}
	public static Vector2IntReadOnly operator -(Vector2IntReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2IntReadOnly(left.x - right.x, left.y - right.y);
	}
	public static Vector2IntReadOnly operator -(Vector2IntReadOnly left, Vector2Int right)
	{
		return new Vector2IntReadOnly(left.x - right.x, left.y - right.y);
	}


	public static Vector2 operator *(Vector2 left, Vector2IntReadOnly right)
	{
		return new Vector2(left.x * right.x, left.y * right.y);
	}
	public static Vector2Int operator *(Vector2Int left, Vector2IntReadOnly right)
	{
		return new Vector2Int(left.x * right.x, left.y * right.y);
	}
	public static Vector2IntReadOnly operator *(Vector2IntReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2IntReadOnly(left.x * right.x, left.y * right.y);
	}
	public static Vector2IntReadOnly operator *(Vector2IntReadOnly left, Vector2Int right)
	{
		return new Vector2IntReadOnly(left.x * right.x, left.y * right.y);
	}
	public static Vector2IntReadOnly operator *(Vector2IntReadOnly left, int right)
	{
		return new Vector2IntReadOnly(left.x * right, left.y * right);
	}


	public static Vector2 operator /(Vector2 left, Vector2IntReadOnly right)
	{
		return new Vector2(left.x / right.x, left.y / right.y);
	}
	public static Vector2Int operator /(Vector2Int left, Vector2IntReadOnly right)
	{
		return new Vector2Int(left.x / right.x, left.y / right.y);
	}
	public static Vector2IntReadOnly operator /(Vector2IntReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2IntReadOnly(left.x / right.x, left.y / right.y);
	}
	public static Vector2IntReadOnly operator /(Vector2IntReadOnly left, Vector2Int right)
	{
		return new Vector2IntReadOnly(left.x / right.x, left.y / right.y);
	}
	public static Vector2IntReadOnly operator /(Vector2IntReadOnly left, int right)
	{
		return new Vector2IntReadOnly(left.x / right, left.y / right);
	}
}
