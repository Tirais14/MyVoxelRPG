using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
	public static bool IsOdd(this int value)
	{
		if ((value % 2) == 0)
		{ return true; }

		return false;
	}
}
