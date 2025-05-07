using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class BlockMeshFaceTriangleList : IList<BlockMeshFaceTriangle>
{
	private readonly List<BlockMeshFaceTriangle> triangles;
	private int startPoint;
	public bool IsFixedSize => false;
	public bool IsReadOnly => false;
	public int Count => triangles.Count;
	public BlockMeshFaceTriangle this[int index]
	{
		get => triangles[index];
		set => throw new NotSupportedException();
	}

	///<summary>Creates a list with capacity for 12 triangles</summary>
	public BlockMeshFaceTriangleList(int startPoint = 0)
	{
		this.startPoint = startPoint;
		triangles = new List<BlockMeshFaceTriangle>(12);
	}

	public BlockMeshFaceTriangleList(int capacity, int startPoint = 0)
	{
		this.startPoint = startPoint;
		triangles = new List<BlockMeshFaceTriangle>(capacity);
	}

	//public BlockMeshFaceTriangleList(BlockMeshFaceTriangle triangle) => triangles = new List<BlockMeshFaceTriangle>(2) { triangle };
	//public BlockMeshFaceTriangleList(params BlockMeshFaceTriangle[] triangles)
	//{
	//	this.triangles = new List<BlockMeshFaceTriangle>(triangles.Length);
	//	this.triangles.AddRange(triangles);
	//}
	//public BlockMeshFaceTriangleList(int capacity, params BlockMeshFaceTriangle[] triangles)
	//{
	//	this.triangles = new List<BlockMeshFaceTriangle>(capacity);
	//	this.triangles.AddRange(triangles);
	//}

	public void Add(BlockMeshFaceTriangle[] blockFaceTriangles)
	{
		if (triangles.Count == 0 && startPoint == 0 || triangles.Count == 0 && startPoint > 0)
		{ triangles.AddRange(blockFaceTriangles); }
		else if (startPoint > 0)
		{
			int trianglesCount = startPoint;

			for (int i = 0; i < blockFaceTriangles.Length; i++)
			{
				triangles.Add(triangles[i] + (trianglesCount / 2 * 4));
				startPoint++;
			}
		}
		else if (triangles.Count >= 2)
		{
			int trianglesCount = triangles.Count;

			for (int i = 0; i < blockFaceTriangles.Length; i++)
			{ triangles.Add(triangles[i] + (trianglesCount / 2 * 4)); }
		}
	}

	public int[] ToArray()
	{
		int[] intTriangles = new int[triangles.Count * 3];

		int q = 0;
		for (int i = 0; i < triangles.Count; i++)
		{
			intTriangles[q] = triangles[i].Vertex1;
			intTriangles[q + 1] = triangles[i].Vertex2;
			intTriangles[q + 2] = triangles[i].Vertex3;
			q += 3;
		}

		return intTriangles;
	}

	public void Clear() => triangles.Clear();

	public void CopyTo(BlockMeshFaceTriangle[] array, int arrayIndex) => triangles.CopyTo(array, arrayIndex);

	public IEnumerator<BlockMeshFaceTriangle> GetEnumerator() => triangles.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => triangles.GetEnumerator();

	#region Not Implemented
	public bool IsSynchronized => throw new NotImplementedException();
	public object SyncRoot => throw new NotImplementedException();

	/// <summary>!Not Supported!</summary>
	public int IndexOf(BlockMeshFaceTriangle item) => throw new NotSupportedException();

	/// <summary>!Not Supported!</summary>
	public void Insert(int index, BlockMeshFaceTriangle item) => throw new NotSupportedException();

	/// <summary>!Not Supported!</summary>
	public bool Remove(BlockMeshFaceTriangle item) => throw new NotSupportedException();

	/// <summary>!Not Supported!</summary>
	public void RemoveAt(int index) => throw new NotSupportedException();

	/// <summary>!Not Supported!</summary>
	public void Add(BlockMeshFaceTriangle item) => throw new NotSupportedException();

	/// <summary>!Not Supported!</summary>
	public bool Contains(BlockMeshFaceTriangle item) => throw new NotSupportedException();

	#endregion
}
