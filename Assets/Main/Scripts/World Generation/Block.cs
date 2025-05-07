using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Block
{
	private static readonly int maxFacesCount = 6;
	private int id;
	public int Id => id;
	private readonly Vector3Int localPosition;
	public Vector3Int LocalPosition => localPosition;
	private readonly BlockType type;
	public BlockType Type => type;
	private BlockMeshSide[] blockMeshSides;
	public BlockMeshSide[] BblockMeshSides => blockMeshSides;

	public Block(ref Vector3Int localPosition, BlockType type)
	{
		this.localPosition = localPosition;
		this.type = type;
	}
	public Block(int x, int y, int z, BlockType type)
	{
		localPosition = new Vector3Int(x, y, z);
		this.type = type;
	}

	public bool IsSolid() => type != BlockType.None && type != BlockType.Air;

	public override string ToString()
	{
		StringBuilder blockString = new StringBuilder($"Block position: x = {localPosition.x}, y = {localPosition.y}, z= {localPosition.z};\nType = {type};");

		if (blockMeshSides != null)
		{
			blockString.Append("Faces: ");
			for (int i = 0; i < blockMeshSides.Length; i++)
			{
				blockString.Append(blockMeshSides[i].ToString());
				if (i < blockMeshSides.Length - 1)
				{ blockString.Append(", "); }
			}
		}

		return blockString.ToString();
	}

	//public void SetFaceDirections(BlockMeshSide[] faceDirections)
	//{
	//	if (faceDirections == null)
	//	{ Logger.NullParameter(faceDirections); }
	//	if (faceDirections.Length == 0)
	//	{ Logger.EmptyArray(faceDirections); }


	//}
	public void SetBlockMeshSides(BlockMeshSideList blockMeshSideList)
	{
		//if (blockMeshSideList.Count == 0)
		//{ Logger.EmptyList(blockMeshSideList); }

		//if (blockMeshSides == null)
		//{
		//	blockMeshSides = new BlockMeshSide[blockMeshSideList.Count];

		//}

		//foreach (BlockMeshSide blockMeshSide in blockMeshSideList)
		//{
		//	blockMeshSide.
		//}
	}
	public void SetBlockMeshSides(List<BlockMeshSide> blockMeshSideList)
	{
		if (blockMeshSides == null)
		{
			blockMeshSides = new BlockMeshSide[maxFacesCount];
			blockMeshSideList.CopyTo(blockMeshSides);
		}
		else
		{
			Array.Clear(blockMeshSides, 0, blockMeshSides.Length);
			blockMeshSideList.CopyTo(blockMeshSides);
		}
	}
}