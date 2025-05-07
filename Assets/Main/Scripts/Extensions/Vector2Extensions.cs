using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Vector2Extensions
{
	public static Vector2ReadOnly ToReadOnly(this Vector2 vector2) => new(vector2);

	public static Vector2 ToVector2(this Vector2Int vector2Int) => new(vector2Int.x, vector2Int.y);
}
