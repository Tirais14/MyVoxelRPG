using UnityEngine;

public readonly struct BlockMeshFace
{
	public readonly Vector3 leftDownVertex;
	public readonly Vector3 rightUpVertex;
	public readonly Vector3 rightDownVertex;
	public readonly Vector3 leftUpVertex;
	public static readonly BlockMeshFaceTriangle[] triangles =
	{
		new BlockMeshFaceTriangle(0, 1, 2),
		new BlockMeshFaceTriangle(0, 3, 1)
	};
	public static readonly Vector2[] uvs =
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 1f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f)
	};
	public static BlockMeshFace Back
	{
		get
		{
			return new BlockMeshFace(new Vector3(0f, 0f, 0f),
									new Vector3(1f, 1f, 0f),
									new Vector3(1f, 0f, 0f),
									new Vector3(0f, 1f, 0f));
		}
	}
	public static BlockMeshFace Up
	{
		get
		{
			return new BlockMeshFace(new Vector3(0f, 1f, 0f),
									new Vector3(1f, 1f, 1f),
									new Vector3(1f, 1f, 0f),
									new Vector3(0f, 1f, 1f));
		}
	}
	public static BlockMeshFace Front
	{
		get
		{
			//return new BlockMeshFace(new Vector3(0f, 1f, 1f),
			//	new Vector3(1f, 0f, 1f),
			//	new Vector3(1f, 1f, 1f),
			//	new Vector3(0f, 0f, 1f));

			return new BlockMeshFace(new Vector3(1f, 0f, 1f),
									new Vector3(0f, 1f, 1f),
									new Vector3(0f, 0f, 1f),
									new Vector3(1f, 1f, 1f));
		}
	}
	public static BlockMeshFace Down
	{
		get
		{
			return new BlockMeshFace(new Vector3(0f, 0f, 1f),
									new Vector3(1f, 0f, 0f),
									new Vector3(1f, 0f, 1f),
									new Vector3(0f, 0f, 0f));
		}
	}
	public static BlockMeshFace Right
	{
		get
		{
			return new BlockMeshFace(new Vector3(1f, 0f, 0f),
									new Vector3(1f, 1f, 1f),
									new Vector3(1f, 0f, 1f),
									new Vector3(1f, 1f, 0f));
		}
	}
	public static BlockMeshFace Left
	{
		get
		{
			return new BlockMeshFace(new Vector3(0f, 0f, 1f),
									new Vector3(0f, 1f, 0f),
									new Vector3(0f, 0f, 0f),
									new Vector3(0f, 1f, 1f));
		}
	}

	private BlockMeshFace(Vector3 leftDownVertex,
		Vector3 rightUpVertex,
		Vector3 rightDownVertex,
		Vector3 leftUpVertex)
	{
		this.leftDownVertex = leftDownVertex;
		this.rightUpVertex = rightUpVertex;
		this.rightDownVertex = rightDownVertex;
		this.leftUpVertex = leftUpVertex;
	}
}
