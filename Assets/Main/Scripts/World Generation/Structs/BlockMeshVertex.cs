using System;
using UnityEngine;

public struct BlockMeshVertex : IEquatable<BlockMeshVertex>
{
	private int x;
	public readonly int X => x;
	private int y;
	public readonly int Y => y;
	private int z;
	public readonly int Z => z;
	public static BlockMeshVertex LeftBackDownVertex => new (0, 0, 0);
	public static BlockMeshVertex RightBackDownVertex => new (1, 0, 0);
	public static BlockMeshVertex RightBackUpVertex => new (1, 1, 0);
	public static BlockMeshVertex LeftBackUpVertex => new (0, 1, 0);
	public static BlockMeshVertex LeftFrontUpVertex => new (0, 1, 1);
	public static BlockMeshVertex RightFrontUpVertex => new (1, 1, 1);
	public static BlockMeshVertex RightFrontDownVertex => new (1, 0, 1);
	public static BlockMeshVertex LeftFrontDownVertex => new (0, 0, 1);

	private BlockMeshVertex(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public void SetOffset(Vector3Int offset) => SetOffset(offset.x, offset.y, offset.z);
	public void SetOffset(int offsetX, int offsetY, int offsetZ)
	{
		x += offsetX;
		y += offsetY;
		z += offsetZ;
	}

	public readonly Vector3 ToVector3() => new (x, y, z);

	public readonly Vector3Int ToVector3Int() => new (x, y, z);

	public readonly bool Equals(BlockMeshVertex otherCuveVertex)
	{
		if (x != otherCuveVertex.x)
		{ return false; }
		if (y != otherCuveVertex.y)
		{ return false; }
		if (z != otherCuveVertex.z)
		{ return false; }

		return true;
	}

	public override int GetHashCode() => x.GetHashCode() + y.GetHashCode() + z.GetHashCode();

	public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

	public override string ToString()
	{
		//if (this == LeftBackDownVertex)
		//{ return nameof(LeftBackDownVertex); }
		//else if (this == RightBackDownVertex)
		//{ return nameof(RightBackDownVertex); }
		//else if (this == RightBackUpVertex)
		//{ return nameof(RightBackUpVertex); }
		//else if (this == LeftBackUpVertex)
		//{ return nameof(LeftBackUpVertex); }
		//else if (this == LeftFrontUpVertex)
		//{ return nameof(LeftFrontUpVertex); }
		//else if (this == RightFrontUpVertex)
		//{ return nameof(RightFrontUpVertex); }
		//else if (this == RightFrontDownVertex)
		//{ return nameof(RightFrontDownVertex); }
		//else if (this == LeftFrontDownVertex)
		//{ return nameof(LeftFrontDownVertex); }

		return $"x = {x}, y = {y}, z = {z}";
	}

	public static BlockMeshVertex operator + (BlockMeshVertex cubeVertex, Vector3Int vector3Int)
	{
		cubeVertex.SetOffset(cubeVertex.x + vector3Int.x, cubeVertex.y + vector3Int.y, cubeVertex.z + vector3Int.z);
		return cubeVertex;
	}

	public static BlockMeshVertex operator - (BlockMeshVertex cubeVertex, Vector3Int vector3Int)
	{
		cubeVertex.SetOffset(cubeVertex.x - vector3Int.x, cubeVertex.y - vector3Int.y, cubeVertex.z - vector3Int.z);
		return cubeVertex;
	}
	public static bool operator == (BlockMeshVertex left, BlockMeshVertex right) => left.Equals(right);
	public static bool operator != (BlockMeshVertex left, BlockMeshVertex right) => !left.Equals(right);
}
