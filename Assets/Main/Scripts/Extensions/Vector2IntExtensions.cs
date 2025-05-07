using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2IntExtensions
{
	public static Vector2IntReadOnly ToReadOnly(this Vector2Int vector2) => new(vector2);
}
