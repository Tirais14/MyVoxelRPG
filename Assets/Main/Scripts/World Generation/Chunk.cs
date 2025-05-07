using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
	private MeshFilter meshFilterComp;
	private MeshRenderer meshRendererComp;
	private int size;
	private int height;
	private Vector3Int position;
	public Vector3Int Position => position;
	private Dictionary<Vector3Int, Block> blocks;

	private bool InXSizeOfChunk(int blockPosX)
	{
		int minX = position.x;
		int maxX = position.x + size;

		if (blockPosX >= minX && blockPosX < maxX)
		{ return true; }

		return false;
	}

	private bool InZSizeOfChunk(int blockPosZ)
	{
		int minZ = position.z;
		int maxZ = position.z + size;

		if (blockPosZ >= minZ && blockPosZ < maxZ)
		{ return true; }

		return false;
	}

	private bool InHeightOfChunk(int blockPosY)
	{
		int minY = position.y;
		int maxY = position.y + height;
		if (blockPosY >= minY && blockPosY < maxY)
		{  return true; }

		return false;
	}

	private bool InChunkBounds(ref Vector3Int blockPosition)
	{
		return InChunkBounds(blockPosition.x, blockPosition.y, blockPosition.z);
	}
	private bool InChunkBounds(int blockPositionX, int blockPositionY, int blockPositionZ)
	{
		return InXSizeOfChunk(blockPositionX) && InHeightOfChunk(blockPositionY) && InZSizeOfChunk(blockPositionZ);
	}

	private Vector3Int GetChunkPositionByBlockPosition(ref Vector3Int blockPosition)
	{
		Vector3Int searchChunkPos = blockPosition;
		for (int i = 0; i < 3; i++)
		{
			if ((searchChunkPos[i] % size) != 0)
			{ searchChunkPos[i] = searchChunkPos[i] - (searchChunkPos[i] % size); }
		}

		return searchChunkPos;
	}

	//private bool BlockExistsOnPoint(int blockPositionX, int blockPositionY, int blockPositionZ)
	//{
	//	Vector3Int blockPosition = new(blockPositionX, blockPositionY, blockPositionZ);
	//	return BlockExistsOnPoint(ref blockPosition);
	//}
	private bool BlockExistsOnPosition(ref Vector3Int blockPosition, bool inChunkBounds)
	{
		Vector3Int localBlockPosition = blockPosition - position;
		//inChunkBoundsNullable ??= InChunkBounds(ref blockPosition);
		//bool inChunkBounds = (bool)inChunkBoundsNullable;

		if (inChunkBounds && TryGetBlock(ref localBlockPosition, out Block block) && block.IsSolid())
		{ return true; }
		else if (!inChunkBounds)
		{
			Vector3Int searchChunkPos = GetChunkPositionByBlockPosition(ref blockPosition);
			if (searchChunkPos == position)
			{ return true; }

			if (World.Instance.TryGetChunk(searchChunkPos, out Chunk foundChunk))
			{
				localBlockPosition = blockPosition - foundChunk.position;
				if (foundChunk.TryGetBlock(ref localBlockPosition, out block) && block.IsSolid())
				{ return true; }
			}
			else
			{ return true; }
		}

		return false;
	}


	private void RecalculateBlockMeshSide(Chunk chunk, Vector3Int blockPosition)
	{
		if (chunk == null)
		{ return; }

		MeshFilter meshFilter = chunk.GetComponent<MeshFilter>();
		List<Vector3> vertices = new(meshFilter.mesh.vertexCount);
		List<int> triangles = new();

		meshFilter.mesh.GetVertices(vertices);
		meshFilter.mesh.GetTriangles(triangles, 0);

		Vector3Int localBlockPosition = blockPosition - chunk.position;
		BlockMeshFace backFace = BlockMeshFace.Back;
		vertices.Add(backFace.leftDownVertex + localBlockPosition);
		vertices.Add(backFace.rightUpVertex + localBlockPosition);
		vertices.Add(backFace.rightDownVertex + localBlockPosition);
		vertices.Add(backFace.leftUpVertex + localBlockPosition);
		triangles.Add(0 + triangles.Count);
		triangles.Add(1 + triangles.Count);
		triangles.Add(2 + triangles.Count);
		triangles.Add(0 + triangles.Count);
		triangles.Add(3 + triangles.Count);
		triangles.Add(1 + triangles.Count);
		//uvs.AddRange(BlockMeshFace.uvs);

		meshFilter.mesh.SetVertices(vertices);
		meshFilter.mesh.SetTriangles(triangles, 0);

		meshFilter.mesh.RecalculateNormals();
		meshFilter.mesh.Optimize();
	}

	private void AddBackFace(BlockMeshVertexList backChunkVertices, BlockMeshFaceTriangleList backChunkTriangles, List<Vector2> backChunkUVs,
								ref Vector3Int localBlockPosition)
	{
		backChunkVertices.Add(BlockMeshFace.Back, ref localBlockPosition);
		backChunkTriangles.Add(BlockMeshFace.triangles);
	}

	private void AddUpFace(BlockMeshVertexList backChunkVertices, BlockMeshFaceTriangleList backChunkTriangles, List<Vector2> backChunkUVs,
								ref Vector3Int localBlockPosition)
	{
		backChunkVertices.Add(BlockMeshFace.Up, ref localBlockPosition);
		backChunkTriangles.Add(BlockMeshFace.triangles);
	}

	private void AddFrontFace(BlockMeshVertexList backChunkVertices, BlockMeshFaceTriangleList backChunkTriangles, List<Vector2> backChunkUVs,
								ref Vector3Int localBlockPosition)
	{
		backChunkVertices.Add(BlockMeshFace.Front, ref localBlockPosition);
		backChunkTriangles.Add(BlockMeshFace.triangles);
		backChunkUVs.AddRange(BlockMeshFace.uvs);
	}

	private void AddDownFace(BlockMeshVertexList backChunkVertices, BlockMeshFaceTriangleList backChunkTriangles, List<Vector2> backChunkUVs,
								ref Vector3Int localBlockPosition)
	{
		backChunkVertices.Add(BlockMeshFace.Down, ref localBlockPosition);
		backChunkTriangles.Add(BlockMeshFace.triangles);
		backChunkUVs.AddRange(BlockMeshFace.uvs);
	}

	private void AddRightFace(BlockMeshVertexList backChunkVertices, BlockMeshFaceTriangleList backChunkTriangles, List<Vector2> backChunkUVs,
								ref Vector3Int localBlockPosition)
	{
		backChunkVertices.Add(BlockMeshFace.Right, ref localBlockPosition);
		backChunkTriangles.Add(BlockMeshFace.triangles);
		backChunkUVs.AddRange(BlockMeshFace.uvs);
	}

	private void AddLeftFace(BlockMeshVertexList backChunkVertices, BlockMeshFaceTriangleList backChunkTriangles, List<Vector2> backChunkUVs,
								ref Vector3Int localBlockPosition)
	{
		backChunkVertices.Add(BlockMeshFace.Left, ref localBlockPosition);
		backChunkTriangles.Add(BlockMeshFace.triangles);
		backChunkUVs.AddRange(BlockMeshFace.uvs);
	}

	//private void CalculateBlockMeshSides(List<BlockMeshSide> blockMeshSideList, ref Vector3Int blockPosition)
	//{
	//	CalculateBlockMeshSides(blockMeshSideList, blockPosition.x, blockPosition.y, blockPosition.z);
	//}
	private void CalculateBlockMeshSides(List<BlockMeshSide> blockMeshSideList, ref Vector3Int blockPosition, Chunk[] nearestChunks,
										BlockMeshVertexList[] nearestChunksVertices, BlockMeshFaceTriangleList[] nearestChunksTriangles,
										List<Vector2>[] chunkMeshesUVs, Mesh[] chunkMeshes)
	{
		Vector3Int backBlockPos = new(blockPosition.x, blockPosition.y, blockPosition.z - 1);
		Vector3Int upBlockPos = new(blockPosition.x, blockPosition.y + 1, blockPosition.z);
		Vector3Int frontBlockPos = new(blockPosition.x, blockPosition.y, blockPosition.z + 1);
		Vector3Int downBlockPos = new(blockPosition.x, blockPosition.y - 1, blockPosition.z);
		Vector3Int rightBlockPos = new(blockPosition.x + 1, blockPosition.y, blockPosition.z);
		Vector3Int leftBlockPos = new(blockPosition.x - 1, blockPosition.y, blockPosition.z);

		Vector3Int backChunkPos = new(position.x, position.y, position.z - size);
		Vector3Int upChunkPos = new(position.x, position.y + size, position.z);
		Vector3Int frontChunkPos = new(position.x, position.y, position.z + size);
		Vector3Int downChunkPos = new(position.x, position.y - size, position.z);
		Vector3Int rightChunkPos = new(position.x + size, position.y, position.z);
		Vector3Int leftChunkPos = new(position.x - size, position.y, position.z);

		World.Instance.TryGetChunk(backChunkPos, out Chunk backChunk);
		World.Instance.TryGetChunk(upChunkPos, out Chunk upChunk);
		World.Instance.TryGetChunk(frontChunkPos, out Chunk frontChunk);
		World.Instance.TryGetChunk(downChunkPos, out Chunk downChunk);
		World.Instance.TryGetChunk(rightChunkPos, out Chunk rightChunk);
		World.Instance.TryGetChunk(leftChunkPos, out Chunk leftChunk);

		nearestChunks[0] ??= backChunk;
		nearestChunks[1] ??= upChunk;
		nearestChunks[2] ??= frontChunk;
		nearestChunks[3] ??= downChunk;
		nearestChunks[4] ??= rightChunk;
		nearestChunks[5] ??= leftChunk;

		nearestChunksVertices[0] ??= new();
		nearestChunksVertices[1] ??= new();
		nearestChunksVertices[2] ??= new();
		nearestChunksVertices[3] ??= new();
		nearestChunksVertices[4] ??= new();
		nearestChunksVertices[5] ??= new();

		chunkMeshesUVs[0] ??= new();
		chunkMeshesUVs[1] ??= new();
		chunkMeshesUVs[2] ??= new();
		chunkMeshesUVs[3] ??= new();
		chunkMeshesUVs[4] ??= new();
		chunkMeshesUVs[5] ??= new();

		if (backChunk != null && chunkMeshes[0] == null && nearestChunksTriangles[0] == null)
		{
			chunkMeshes[0] = backChunk.GetComponent<MeshFilter>().mesh;
			nearestChunksTriangles[0] = new(chunkMeshes[0].vertexCount / 4 * 6 / 3);
		}
		if (upChunk != null && chunkMeshes[1] == null && nearestChunksTriangles[1] == null)
		{
			chunkMeshes[1] = upChunk.GetComponent<MeshFilter>().mesh;
			nearestChunksTriangles[1] = new(chunkMeshes[1].vertexCount / 4 * 6 / 3); 
		}
		if (frontChunk != null && chunkMeshes[2] == null && nearestChunksTriangles[2] == null)
		{
			chunkMeshes[2] = frontChunk.GetComponent<MeshFilter>().mesh;
			nearestChunksTriangles[2] = new(chunkMeshes[2].vertexCount / 4 * 6 / 3);
		}
		if (downChunk != null && chunkMeshes[3] == null && nearestChunksTriangles[3] == null)
		{
			chunkMeshes[3] = downChunk.GetComponent<MeshFilter>().mesh;
			nearestChunksTriangles[3] = new(chunkMeshes[3].vertexCount / 4 * 6 / 3);
		}
		if (rightChunk != null && chunkMeshes[4] == null && nearestChunksTriangles[4] == null)
		{
			chunkMeshes[4] = rightChunk.GetComponent<MeshFilter>().mesh;
			nearestChunksTriangles[4] = new(chunkMeshes[4].vertexCount / 4 * 6 / 3); 
		}
		if (leftChunk != null && chunkMeshes[5] == null && nearestChunksTriangles[5] == null)
		{
			chunkMeshes[5] = leftChunk.GetComponent<MeshFilter>().mesh;
			nearestChunksTriangles[5] = new(chunkMeshes[5].vertexCount / 4 * 6 / 3); 
		}

		bool backBlockInChunkBounds = InChunkBounds(ref backBlockPos);
		bool upBlockInChunkBounds = InChunkBounds(ref upBlockPos);
		bool frontBlockInChunkBounds = InChunkBounds(ref frontBlockPos);
		bool downBlockInChunkBounds = InChunkBounds(ref downBlockPos);
		bool rightBlockInChunkBounds = InChunkBounds(ref rightBlockPos);
		bool leftBlockInChunkBounds = InChunkBounds(ref leftBlockPos);

		Block tempBlock;
		Vector3Int tempLocalBlockPosition = new();
		if (!backBlockInChunkBounds && backChunk != null 
			&& backChunk.TryGetBlock(backBlockPos - backChunk.position, out tempBlock) && tempBlock.IsSolid())
		{
			tempLocalBlockPosition.Set(backBlockPos.x - backChunk.position.x, 
										backBlockPos.y - backChunk.position.y,
										backBlockPos.z - backChunk.position.z);
			AddFrontFace(nearestChunksVertices[0], nearestChunksTriangles[0], chunkMeshesUVs[0], ref tempLocalBlockPosition);
		}
		if (!upBlockInChunkBounds && upChunk != null 
			&& upChunk.TryGetBlock(upBlockPos - upChunk.position, out tempBlock) && tempBlock.IsSolid())
		{
			tempLocalBlockPosition.Set(upBlockPos.x - upChunk.position.x,
										upBlockPos.y - upChunk.position.y,
										upBlockPos.z - upChunk.position.z);
			AddDownFace(nearestChunksVertices[1], nearestChunksTriangles[1], chunkMeshesUVs[1], ref tempLocalBlockPosition);
		}
		if (!frontBlockInChunkBounds && frontChunk != null
			&& frontChunk.TryGetBlock(frontBlockPos - frontChunk.position, out tempBlock) && tempBlock.IsSolid())
		{
			tempLocalBlockPosition.Set(frontBlockPos.x - frontChunk.position.x,
										frontBlockPos.y - frontChunk.position.y,
										frontBlockPos.z - frontChunk.position.z);
			AddBackFace(nearestChunksVertices[2], nearestChunksTriangles[2], chunkMeshesUVs[2], ref tempLocalBlockPosition);
		}
		if (!downBlockInChunkBounds && downChunk != null
			&& downChunk.TryGetBlock(downBlockPos - downChunk.position, out tempBlock) && tempBlock.IsSolid())
		{
			tempLocalBlockPosition.Set(downBlockPos.x - downChunk.position.x,
										downBlockPos.y - downChunk.position.y,
										downBlockPos.z - downChunk.position.z);
			AddUpFace(nearestChunksVertices[3], nearestChunksTriangles[3], chunkMeshesUVs[3], ref tempLocalBlockPosition);
		}
		if (!rightBlockInChunkBounds && rightChunk != null
			&& rightChunk.TryGetBlock(rightBlockPos - rightChunk.position, out tempBlock) && tempBlock.IsSolid())
		{
			tempLocalBlockPosition.Set(rightBlockPos.x - rightChunk.position.x,
										rightBlockPos.y - rightChunk.position.y,
										rightBlockPos.z - rightChunk.position.z);
			AddLeftFace(nearestChunksVertices[4], nearestChunksTriangles[4], chunkMeshesUVs[4], ref tempLocalBlockPosition);
		}
		if (!leftBlockInChunkBounds && leftChunk != null
			&& leftChunk.TryGetBlock(leftBlockPos - leftChunk.position, out tempBlock) && tempBlock.IsSolid())
		{
			tempLocalBlockPosition.Set(leftBlockPos.x - leftChunk.position.x,
										leftBlockPos.y - leftChunk.position.y,
										leftBlockPos.z - leftChunk.position.z);
			AddRightFace(nearestChunksVertices[5], nearestChunksTriangles[5], chunkMeshesUVs[5], ref tempLocalBlockPosition);
		}

		Vector3Int temp = blockPosition - position;
		if (TryGetBlock(temp, out Block temp1Block) && !temp1Block.IsSolid())
		{ return; }

		if (!BlockExistsOnPosition(ref backBlockPos, backBlockInChunkBounds))
		{ blockMeshSideList.Add(BlockMeshSide.Back); }

		if (!BlockExistsOnPosition(ref upBlockPos, upBlockInChunkBounds))
		{ blockMeshSideList.Add(BlockMeshSide.Up); }

		if (!BlockExistsOnPosition(ref frontBlockPos, frontBlockInChunkBounds))
		{ blockMeshSideList.Add(BlockMeshSide.Front); }

		if (!BlockExistsOnPosition(ref downBlockPos, downBlockInChunkBounds))
		{ blockMeshSideList.Add(BlockMeshSide.Down); }

		if (!BlockExistsOnPosition(ref rightBlockPos, rightBlockInChunkBounds))
		{ blockMeshSideList.Add(BlockMeshSide.Right); }

		if (!BlockExistsOnPosition(ref leftBlockPos, leftBlockInChunkBounds))
		{ blockMeshSideList.Add(BlockMeshSide.Left); }
	}
	//private void CalculateBlockMeshSides(List<BlockMeshSide> blockMeshSideList, int blockPosX, int blockPosY, int blockPosZ)
	//{
	//	Vector3Int backBlockPos = new(blockPosX, blockPosY, blockPosZ - 1);
	//	Vector3Int upBlockPos = new(blockPosX, blockPosY + 1, blockPosZ);
	//	Vector3Int frontBlockPos = new(blockPosX, blockPosY, blockPosZ + 1);
	//	Vector3Int downBlockPos = new(blockPosX, blockPosY - 1, blockPosZ);
	//	Vector3Int rightBlockPos = new(blockPosX + 1, blockPosY, blockPosZ);
	//	Vector3Int leftBlockPos = new(blockPosX - 1, blockPosY, blockPosZ);

	//	Vector3Int backChunkPos = new(position.x, position.y, position.z - size);
	//	Vector3Int upChunkPos = new(position.x, position.y + size, position.z);
	//	Vector3Int frontChunkPos = new(position.x, position.y, position.z + size);
	//	Vector3Int downChunkPos = new(position.x, position.y - size, position.z);
	//	Vector3Int rightChunkPos = new(position.x + size, position.y, position.z);
	//	Vector3Int leftChunkPos = new(position.x - size, position.y, position.z);

	//	Chunk backChunk;
	//	Chunk upChunk;
	//	Chunk frontChunk;
	//	Chunk downChunk;
	//	Chunk rightChunk;
	//	Chunk leftChunk;

	//	World.Instance.TryGetChunk(backChunkPos, out backChunk);
	//	World.Instance.TryGetChunk(upChunkPos, out upChunk);
	//	World.Instance.TryGetChunk(frontChunkPos, out frontChunk);
	//	World.Instance.TryGetChunk(downChunkPos, out downChunk);
	//	World.Instance.TryGetChunk(rightChunkPos, out rightChunk);
	//	World.Instance.TryGetChunk(leftChunkPos, out leftChunk);

	//	bool backBlockInChunkBounds = InChunkBounds(ref backBlockPos);
	//	bool upBlockInChunkBounds = InChunkBounds(ref upBlockPos);
	//	bool frontBlockInChunkBounds = InChunkBounds(ref frontBlockPos);
	//	bool downBlockInChunkBounds = InChunkBounds(ref downBlockPos);
	//	bool rightBlockInChunkBounds = InChunkBounds(ref rightBlockPos);
	//	bool leftBlockInChunkBounds = InChunkBounds(ref leftBlockPos);

	//	if (!BlockExistsOnPosition(ref backBlockPos, backBlockInChunkBounds))
	//	{ 
	//		blockMeshSideList.Add(BlockMeshSide.Back); 
	//		if (!backBlockInChunkBounds)
	//		{  }
	//	}

	//	if (!BlockExistsOnPosition(ref upBlockPos, upBlockInChunkBounds))
	//	{ blockMeshSideList.Add(BlockMeshSide.Up); }

	//	if (!BlockExistsOnPosition(ref frontBlockPos, frontBlockInChunkBounds))
	//	{
	//		blockMeshSideList.Add(BlockMeshSide.Front);
	//		if (!frontBlockInChunkBounds)
	//		{ }
	//	}

	//	if (!BlockExistsOnPosition(ref downBlockPos, downBlockInChunkBounds))
	//	{ blockMeshSideList.Add(BlockMeshSide.Down); }

	//	if (!BlockExistsOnPosition(ref rightBlockPos, rightBlockInChunkBounds))
	//	{ blockMeshSideList.Add(BlockMeshSide.Right); }

	//	if (!BlockExistsOnPosition(ref leftBlockPos, leftBlockInChunkBounds))
	//	{ blockMeshSideList.Add(BlockMeshSide.Left); }
	//}

	public bool TryGetBlock(ref Vector3Int localBlockPosition, out Block block) => blocks.TryGetValue(localBlockPosition, out block);
	public bool TryGetBlock(Vector3Int localBlockPosition, out Block block) => blocks.TryGetValue(localBlockPosition, out block);
	public bool TryGetBlock(int localBlockPosX, int localBlockPosY, int localBlockPosZ, out Block block)
	{
		Vector3Int localBlockPosition = new(localBlockPosX, localBlockPosY, localBlockPosZ);
		return blocks.TryGetValue(localBlockPosition, out block);
	}

	private void CalculateBlockTypes(int[,] heightMap)
	{
		int heightMapSizeX = heightMap.GetLength(0);
		int heightMapSizeY = heightMap.GetLength(1);

		Vector3Int newBlockPos = Vector3Int.zero;
		for (int mapY = 0; mapY < heightMapSizeY; mapY++)
		{
			for (int mapX = 0; mapX < heightMapSizeX; mapX++)
			{
				for (int worldY = 0; worldY < height; worldY++)
				{

					if (worldY < heightMap[mapX, mapY])
					{
						newBlockPos.Set(mapX, worldY, mapY);
						blocks.Add(newBlockPos, new Block(ref newBlockPos, BlockType.Dirt));
					}
					else
					{
						newBlockPos.Set(mapX, worldY, mapY);
						blocks.Add(newBlockPos, new Block(ref newBlockPos, BlockType.Air));
					}
				}
			}
		}
	}

	public void Generate(int[,] heightMap)
	{
		//if (heightMap.GetLength(0) < size)
		//{ Logger.SendError(gameObject: gameObject, "Wrong height map: x"); }
		//if (heightMap.GetLength(1) < size)
		//{ Logger.SendError(gameObject: gameObject, "Wrong height map: y"); }

		const int cubeFacesCount = 6;
		const int blockVertexCount = 24;

		Mesh chunkMesh = new();
		List<BlockMeshSide> blockMeshSides = new(cubeFacesCount);
		BlockMeshVertexList vertices = new();
		BlockMeshFaceTriangleList triangles = new();
		List<Vector2> uvs =	new(blockVertexCount);

		//Vector3Int[] nearestBlockPositions = new Vector3Int[cubeFacesCount];
		//Vector3Int[] nearestChunkPositions = new Vector3Int[cubeFacesCount];
		Chunk[] nearestChunks = new Chunk[cubeFacesCount];
		BlockMeshVertexList[] nearestChunksVertices = new BlockMeshVertexList[cubeFacesCount];
		BlockMeshFaceTriangleList[] nearestChunksTriangles = new BlockMeshFaceTriangleList[cubeFacesCount];
		Mesh[] chunkMeshes = new Mesh[cubeFacesCount];
		List<Vector2>[] chunkMeshesUVs = new List<Vector2>[cubeFacesCount];
		//bool[] blocksInChunksBounds = new bool[cubeFacesCount];

		CalculateBlockTypes(heightMap);
		Block block;
		Vector3Int blockPosition;
		foreach (var blocksCollectionItem in blocks)
		{
			block = blocksCollectionItem.Value;
			//if (block.Type != BlockType.Air && block.Type != BlockType.None)
			if (true)
			{
				blockPosition = position + block.LocalPosition;
				CalculateBlockMeshSides(blockMeshSides, ref blockPosition, nearestChunks, nearestChunksVertices, nearestChunksTriangles, chunkMeshesUVs, chunkMeshes);
				BlockMeshCalculator.Calculate(blockMeshSides, block.LocalPosition, vertices, triangles, uvs);

				if (blockMeshSides.Count > 0)
				{ block.SetBlockMeshSides(blockMeshSides); }
			}
			blockMeshSides.Clear();
		}

		Mesh tempMesh;
		List<Vector3> tempVertices = new();
		List<int> tempTriangles = new();
		List<Vector2> tempUVs = new();
		for (int i = 0; i < nearestChunks.Length; i++)
		{
			if (nearestChunks[i] != null && nearestChunksVertices[i] != null && nearestChunksTriangles[i] != null && chunkMeshesUVs[i] != null)
			{
				tempMesh = nearestChunks[i].GetComponent<MeshFilter>().mesh;
				tempMesh.GetVertices(tempVertices);
				tempMesh.GetTriangles(tempTriangles, 0);
				tempMesh.GetUVs(0, tempUVs);

				tempVertices.AddRange(nearestChunksVertices[i].ToArray());
				tempTriangles.AddRange(nearestChunksTriangles[i].ToArray());
				tempUVs.AddRange(chunkMeshesUVs[i]);
				//Logger.Cycle(tempTriangles, 0);
				//Logger.Cycle(nearestChunksTriangles[i].ToArray(), 0);

				tempMesh.SetVertices(tempVertices);
				tempMesh.SetTriangles(tempTriangles, 0);
				tempMesh.SetUVs(0, tempUVs);

				tempMesh.RecalculateNormals();
				tempMesh.Optimize();
			}
		}

		chunkMesh.SetVertices(vertices.ToArray());
		chunkMesh.SetTriangles(triangles.ToArray(), 0);
		chunkMesh.SetUVs(0, uvs);
		chunkMesh.RecalculateNormals();
		chunkMesh.Optimize();
		meshFilterComp.mesh = chunkMesh;
	}

	public void Initialize(int size, int height, ref Vector3Int position)
	{
		this.size = size;
		this.height = height;
		this.position = position;
		blocks = new Dictionary<Vector3Int, Block>(size * height * size / 4);

		meshFilterComp = GetComponent<MeshFilter>();
		meshRendererComp = GetComponent<MeshRenderer>();
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (meshFilterComp == null)
			return;

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(new Vector3(size / 2, height / 2, size / 2) + position, new Vector3(size, height, size));

		Block tempBlock;
		Vector3 gizmosCubePos = Vector3.zero;
		foreach (var collectionItem in blocks)
		{
			tempBlock = collectionItem.Value;
			gizmosCubePos.Set(collectionItem.Value.LocalPosition.x + position.x + 0.5f,
								collectionItem.Value.LocalPosition.y + position.y + 0.5f,
								collectionItem.Value.LocalPosition.z + position.z + 0.5f);
			Gizmos.DrawWireCube(gizmosCubePos, new Vector3(1f, 1f, 1f));
		}
	}
#endif
}