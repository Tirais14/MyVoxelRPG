using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BlockMeshFaceTriangle
{
	private int vertex1, vertex2, vertex3;
	public readonly int Vertex1 => vertex1;
	public readonly int Vertex2 => vertex2;
	public readonly int Vertex3 => vertex3;

	public BlockMeshFaceTriangle(int vertex1, int vertex2, int vertex3)
	{
		this.vertex1 = vertex1;
		this.vertex2 = vertex2;
		this.vertex3 = vertex3;
	}

	public void SetOffset(int offsetValue)
	{
		vertex1 += offsetValue;
		vertex2 += offsetValue;
		vertex3 += offsetValue;
	}

	public static BlockMeshFaceTriangle operator + (BlockMeshFaceTriangle left, int value)
	{
		left.SetOffset(value);
		return left;
	}
}
