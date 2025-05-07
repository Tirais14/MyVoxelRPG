using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct Vector2ReadOnly
{
	public readonly float x, y;

	public Vector2ReadOnly(Vector2 vector2)
	{
		x = vector2.x;
		y = vector2.y;
	}
	public Vector2ReadOnly(Vector2Int vector2)
	{
		x = vector2.x;
		y = vector2.y;
	}
	public Vector2ReadOnly(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public static Vector2 operator +(Vector2 left, Vector2ReadOnly right)
	{
		return new Vector2(left.x + right.x, left.y + right.y);
	}
	public static Vector2ReadOnly operator +(Vector2ReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2ReadOnly(left.x + right.x, left.y + right.y);
	}
	public static Vector2ReadOnly operator +(Vector2ReadOnly left, Vector2ReadOnly right)
	{
		return new Vector2ReadOnly(left.x + right.x, left.y + right.y);
	}
	public static Vector2ReadOnly operator +(Vector2ReadOnly left, Vector2Int right)
	{
		return new Vector2ReadOnly(left.x + right.x, left.y + right.y);
	}
	public static Vector2ReadOnly operator +(Vector2ReadOnly left, Vector2 right)
	{
		return new Vector2ReadOnly(left.x + right.x, left.y + right.y);
	}


	public static Vector2 operator -(Vector2 left, Vector2ReadOnly right)
	{
		return new Vector2(left.x - right.x, left.y - right.y);
	}
	public static Vector2ReadOnly operator -(Vector2ReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2ReadOnly(left.x - right.x, left.y - right.y);
	}
	public static Vector2ReadOnly operator -(Vector2ReadOnly left, Vector2ReadOnly right)
	{
		return new Vector2ReadOnly(left.x - right.x, left.y - right.y);
	}
	public static Vector2ReadOnly operator -(Vector2ReadOnly left, Vector2Int right)
	{
		return new Vector2ReadOnly(left.x - right.x, left.y - right.y);
	}
	public static Vector2ReadOnly operator -(Vector2ReadOnly left, Vector2 right)
	{
		return new Vector2ReadOnly(left.x - right.x, left.y - right.y);
	}


	public static Vector2 operator *(Vector2 left, Vector2ReadOnly right)
	{
		return new Vector2(left.x * right.x, left.y * right.y);
	}
	public static Vector2ReadOnly operator *(Vector2ReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2ReadOnly(left.x * right.x, left.y * right.y);
	}
	public static Vector2ReadOnly operator *(Vector2ReadOnly left, Vector2ReadOnly right)
	{
		return new Vector2ReadOnly(left.x * right.x, left.y * right.y);
	}
	public static Vector2ReadOnly operator *(Vector2ReadOnly left, Vector2Int right)
	{
		return new Vector2ReadOnly(left.x * right.x, left.y * right.y);
	}
	public static Vector2ReadOnly operator *(Vector2ReadOnly left, Vector2 right)
	{
		return new Vector2ReadOnly(left.x * right.x, left.y * right.y);
	}
	public static Vector2ReadOnly operator *(Vector2ReadOnly left, int right)
	{
		return new Vector2ReadOnly(left.x * right, left.y * right);
	}
	public static Vector2ReadOnly operator *(Vector2ReadOnly left, float right)
	{
		return new Vector2ReadOnly(left.x * right, left.y * right);
	}


	public static Vector2 operator /(Vector2 left, Vector2ReadOnly right)
	{
		return new Vector2(left.x / right.x, left.y / right.y);
	}
	public static Vector2ReadOnly operator /(Vector2ReadOnly left, Vector2IntReadOnly right)
	{
		return new Vector2ReadOnly(left.x / right.x, left.y / right.y);
	}
	public static Vector2ReadOnly operator /(Vector2ReadOnly left, Vector2ReadOnly right)
	{
		return new Vector2ReadOnly(left.x / right.x, left.y / right.y);
	}
	public static Vector2ReadOnly operator /(Vector2ReadOnly left, Vector2Int right)
	{
		return new Vector2ReadOnly(left.x / right.x, left.y / right.y);
	}
	public static Vector2ReadOnly operator /(Vector2ReadOnly left, Vector2 right)
	{
		return new Vector2ReadOnly(left.x / right.x, left.y / right.y);
	}
	public static Vector2ReadOnly operator /(Vector2ReadOnly left, int right)
	{
		return new Vector2ReadOnly(left.x / right, left.y / right);
	}
	public static Vector2ReadOnly operator /(Vector2ReadOnly left, float right)
	{
		return new Vector2ReadOnly(left.x / right, left.y / right);
	}
}
