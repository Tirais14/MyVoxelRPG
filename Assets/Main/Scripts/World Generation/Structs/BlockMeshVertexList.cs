using System.Collections.Generic;
using UnityEngine;

public class BlockMeshVertexList
{
	private List<Vector3> vertices;
	public IReadOnlyList<Vector3> Vertices => vertices;
	public int Count => vertices.Count;
	public Vector3 this[int index] => vertices[index];

	///<summary>Creates a list with capacity for 24 vertices</summary>
	public BlockMeshVertexList() => vertices = new List<Vector3>();
	public BlockMeshVertexList(int capacity) => vertices = new List<Vector3>(capacity);

	public void Add(BlockMeshFace face, ref Vector3 localBlockPosition)
	{
		vertices.Add(face.leftDownVertex + localBlockPosition);
		vertices.Add(face.rightUpVertex + localBlockPosition);
		vertices.Add(face.rightDownVertex + localBlockPosition);
		vertices.Add(face.leftUpVertex + localBlockPosition);
	}
	//public void Add(in BlockMeshFace face, in Vector3ReadOnly blockPosOffset)
	//{
	//	vertices.Add(face.leftDownVertex + blockPosOffset);
	//	vertices.Add(face.rightUpVertex + blockPosOffset);
	//	vertices.Add(face.rightDownVertex + blockPosOffset);
	//	vertices.Add(face.leftUpVertex + blockPosOffset);
	//}
	public void Add(BlockMeshFace face, ref Vector3Int localBlockPosition)
	{
		vertices.Add(face.leftDownVertex + localBlockPosition);
		vertices.Add(face.rightUpVertex + localBlockPosition);
		vertices.Add(face.rightDownVertex + localBlockPosition);
		vertices.Add(face.leftUpVertex + localBlockPosition);
	}
	//public void Add(in BlockMeshFace face, in Vector3IntReadOnly blockPosOffset)
	//{
	//	vertices.Add(face.leftDownVertex + blockPosOffset);
	//	vertices.Add(face.rightUpVertex + blockPosOffset);
	//	vertices.Add(face.rightDownVertex + blockPosOffset);
	//	vertices.Add(face.leftUpVertex + blockPosOffset);
	//}

	public void Clear() => vertices.Clear();

	public Vector3[] ToArray() => vertices.ToArray();

	public List<Vector3> ToList()
	{
		List<Vector3> verticesCopy = new(vertices.Count);
		verticesCopy.AddRange(vertices);

		return verticesCopy;
	}
}
