using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class BlockMeshCalculator
{
	public static void GetBlockMeshFaces(List<BlockMeshSide> blockMeshSideList,
									Vector3Int localBlockPosition,
									BlockMeshVertexList vertices,
									BlockMeshFaceTriangleList triangles,
									List<Vector2> uvs)
	{
		if (blockMeshSideList == null)
		{ Logger.NullParameter(blockMeshSideList); }
		if (vertices == null)
		{ Logger.NullParameter(vertices); }
		else if (triangles == null)
		{ Logger.NullParameter(triangles); }
		else if (uvs == null)
		{ Logger.NullParameter(uvs); }

		for (int i = 0; i < blockMeshSideList.Count; i++)
		{
			switch (blockMeshSideList[i])
			{
				case BlockMeshSide.Back:
					BlockMeshFace backFace = BlockMeshFace.Back;
					vertices.Add(backFace, ref localBlockPosition);
					triangles.Add(BlockMeshFace.triangles);
					uvs.AddRange(BlockMeshFace.uvs);
					break;
				case BlockMeshSide.Up:
					BlockMeshFace upFace = BlockMeshFace.Up;
					vertices.Add(upFace, ref localBlockPosition);
					triangles.Add(BlockMeshFace.triangles);
					uvs.AddRange(BlockMeshFace.uvs);
					break;
				case BlockMeshSide.Front:
					BlockMeshFace frontFace = BlockMeshFace.Front;
					vertices.Add(frontFace, ref localBlockPosition);
					triangles.Add(BlockMeshFace.triangles);
					uvs.AddRange(BlockMeshFace.uvs);
					break;
				case BlockMeshSide.Down:
					BlockMeshFace downFace = BlockMeshFace.Down;
					vertices.Add(downFace, ref localBlockPosition);
					triangles.Add(BlockMeshFace.triangles);
					uvs.AddRange(BlockMeshFace.uvs);
					break;
				case BlockMeshSide.Right:
					BlockMeshFace rightFace = BlockMeshFace.Right;
					vertices.Add(rightFace, ref localBlockPosition);
					triangles.Add(BlockMeshFace.triangles);
					uvs.AddRange(BlockMeshFace.uvs);
					break;
				case BlockMeshSide.Left:
					BlockMeshFace leftFace = BlockMeshFace.Left;
					vertices.Add(leftFace, ref localBlockPosition);
					triangles.Add(BlockMeshFace.triangles);
					uvs.AddRange(BlockMeshFace.uvs);
					break;
			}
		}
	}

	public static void CalculateColliders()
	{

	}

	public static void Calculate(List<BlockMeshSide> blockMeshSides, 
									Vector3Int localBlockPosition,
									BlockMeshVertexList vertices,
									BlockMeshFaceTriangleList triangles,
									List<Vector2> uvs)
	{
		GetBlockMeshFaces(blockMeshSides, localBlockPosition, vertices, triangles, uvs);
	}

	#region Not Impelemented
	//public static void GetFullCube(out CubeMeshVertex[] vertices, out int[] triangles, out Vector2[] uvs)
	//{
	//	vertices = new CubeMeshVertex[8]
	//	{
	//		CubeMeshVertex.LeftBackDownVertex,
	//		CubeMeshVertex.RightBackUpVertex,
	//		CubeMeshVertex.RightBackDownVertex,
	//		CubeMeshVertex.LeftBackUpVertex,
	//		CubeMeshVertex.RightFrontUpVertex,
	//		CubeMeshVertex.LeftFrontUpVertex,
	//		CubeMeshVertex.RightFrontDownVertex,
	//		CubeMeshVertex.LeftFrontDownVertex
	//	};
	//	triangles = new int[36]
	//	{
	//		0,1,2,
	//		0,3,1,
	//		3,4,1,
	//		3,5,4,
	//		5,6,4,
	//		5,7,6,
	//		7,2,6,
	//		7,0,2,
	//		2,4,6,
	//		2,1,4,
	//		7,3,0,
	//		7,5,3
	//	};
	//	uvs = new Vector2[]
	//	{
	//		new Vector2(0, 0.66f),
	//		new Vector2(0.25f, 0.33f),
	//		new Vector2(0, 0.33f),
	//		new Vector2(0.25f, 0.66f),

	//		new Vector2(0.5f, 0.66f),
	//		new Vector2(0.75f, 0.33f),
	//		new Vector2(0.5f, 0.3f),
	//		new Vector2(0.75f, 0.66f),
	//		new Vector2(0.75f, 0.66f)
	//	};
	//}

	//public static void GetBackUpFrontDownRightCube(out CubeMeshVertex[] vertices, out int[] triangles)
	//{
	//	vertices = new CubeMeshVertex[8]
	//	{
	//		CubeMeshVertex.LeftBackDownVertex,
	//		CubeMeshVertex.RightBackUpVertex,
	//		CubeMeshVertex.RightBackDownVertex,
	//		CubeMeshVertex.LeftBackUpVertex,
	//		CubeMeshVertex.RightFrontUpVertex,
	//		CubeMeshVertex.LeftFrontUpVertex,
	//		CubeMeshVertex.RightFrontDownVertex,
	//		CubeMeshVertex.LeftFrontDownVertex
	//	};
	//	triangles = new int[30]
	//	{
	//		0,1,2,
	//		0,3,1,
	//		3,4,1,
	//		3,5,4,
	//		5,6,4,
	//		5,7,6,
	//		7,2,6,
	//		7,0,2,
	//		2,4,6,
	//		2,1,4,
	//	};
	//}

	//public static void GetBackUpFrontDownLeftCube(out CubeMeshVertex[] vertices, out int[] triangles)
	//{
	//	vertices = new CubeMeshVertex[8]
	//	{
	//		CubeMeshVertex.LeftBackDownVertex,
	//		CubeMeshVertex.RightBackUpVertex,
	//		CubeMeshVertex.RightBackDownVertex,
	//		CubeMeshVertex.LeftBackUpVertex,
	//		CubeMeshVertex.RightFrontUpVertex,
	//		CubeMeshVertex.LeftFrontUpVertex,
	//		CubeMeshVertex.RightFrontDownVertex,
	//		CubeMeshVertex.LeftFrontDownVertex
	//	};
	//	triangles = new int[30]
	//	{
	//		0,1,2,
	//		0,3,1,
	//		3,4,1,
	//		3,5,4,
	//		5,6,4,
	//		5,7,6,
	//		7,2,6,
	//		7,0,2,
	//		2,4,6,
	//		2,1,4,
	//	};
	//}
	#endregion
}
