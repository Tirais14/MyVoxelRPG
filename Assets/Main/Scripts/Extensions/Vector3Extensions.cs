using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Vector3Extensions
{
	public static Vector3ReadOnly ToReadOnly(this Vector3 vector3) => new(vector3);

	public static Vector2 To2DCoords(this Vector3 vector3) => new(vector3.x, vector3.z);
}
