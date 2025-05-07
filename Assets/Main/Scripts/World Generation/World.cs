using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class World : MonoBehaviour
{
	public static World Instance { get; private set; }
	public const int blockSize = 1;
	[SerializeField] private GameObject chunkPrefab;
	public GameObject ChunkPrefab => chunkPrefab;
	[SerializeField] private int chunkSize = 16;
	[SerializeField] private int chunkHeight = 100;
	[SerializeField] private int SqrMapSizeInChunks = 1;
	[SerializeField] private GameObject testPrefab;
	[SerializeField] private Dictionary<Vector3Int, Chunk> chunks;
	[Range(0f, 100f)]
	[SerializeField] private float scale = 1f;
	private GameObject cube;
	private GameObject test3;
	[SerializeField] private Material testMaterial;

	private int[,] GetHeightMap(int offsetX, int offsetY)
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(chunkSize, chunkSize, MapGenerator.Instance.Seed, MapGenerator.Instance.NoiseScale, MapGenerator.Instance.Octaves,
			MapGenerator.Instance.Persistance, MapGenerator.Instance.Lancunarity, new Vector2(offsetX, offsetY));

		int mapWidth = noiseMap.GetLength(0);
		int mapHeight = noiseMap.GetLength(1);
		int[,] heightMap = new int[mapWidth, mapHeight];

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{ heightMap[x, y] = Mathf.RoundToInt(noiseMap[x, y] * chunkHeight); }
		}
		
		return heightMap;
	}

	//public void GenerateChunk(Vector3Int position)
	//{
	//	//Chunk newChunk = new(chunkSize, chunkHeight, Vector3Int.zero);
	//	Chunk newChunk = Instantiate(chunkPrefab, position, Quaternion.identity).GetComponent<Chunk>();
	//	newChunk.Initialize(chunkSize, chunkHeight, position);
	//	//int[,] heightMap = GetHeightMap(position.x, position.z);
	//	float[,] heightMap = new float[chunkSize, chunkSize];

	//	for (int y = 0; y < chunkSize; y++)
	//	{
	//		for (int x = 0; x < chunkSize; x++)
	//		{
	//			//heightMap[x, y] = Mathf.Round(Mathf.PerlinNoise(x + position.x * 0.01f, y + position.y * 0.01f) * chunkHeight);
	//			//heightMap[x, y] = Noise.GenerateNoise(x, y, MapGenerator.Instance.Seed, MapGenerator.Instance.NoiseScale, MapGenerator.Instance.Octaves, MapGenerator.Instance.Persistance, MapGenerator.Instance.Lancunarity, new Vector2(position.x + x, position.z + y));
	//		}
	//	}

	//	//newChunk.Generate(heightMap);
	//	CreateTexture(position + new Vector3Int(0, 60, 0), heightMap);
	//}
	public void GenerateChunk(ref Vector3Int position)
	{
		Chunk newChunk = Instantiate(chunkPrefab, position, Quaternion.identity).GetComponent<Chunk>();
		chunks.Add(position, newChunk);
		newChunk.Initialize(chunkSize, chunkHeight, ref position);
		Vector2 mapCoords = position.To2DReadOnlyF();
		int[,] heightMap = NoiseMapGenerator.GetHeightMapArrayIntByPoints(chunkSize, scale, ref mapCoords, chunkHeight);

		newChunk.Generate(heightMap);
		CreateTexturePlane(position + new Vector3Int(0, 60, 0), NoiseMapGenerator.GetHeightMapTextureBy(heightMap, chunkHeight));
	}

	public void Generate()
	{
		chunks ??= new Dictionary<Vector3Int, Chunk>(100);
		if (chunks != null && chunks.Count > 0)
		{
			foreach (var chunk in chunks)
			{ Destroy(chunk.Value.gameObject); }
			chunks.Clear();
		}

		Vector3Int position = Vector3Int.zero;
		for (int z = 0; z < SqrMapSizeInChunks; z++)
		{
			for (int x = 0; x < SqrMapSizeInChunks; x++)
			{
				position.Set(x * chunkSize, 0, z * chunkSize);
				GenerateChunk(ref position);
			}
		}
	}

	//public bool ChunkExists(Vector3Int position, out Chunk chunk) => chunks.TryGetValue(position, out chunk);

	public bool TryGetChunk(Vector3Int position, out Chunk chunk) => chunks.TryGetValue(position, out chunk);
	//public bool TryGetNearestChunk(Vector3Int position, out Chunk chunk)
	//{
	//	Vector3IntReadOnly backChunkPos = new();
	//}

	public void CreateTexturePlane(Vector3 planeTexturePosition, Texture2D heightMapTexture)
	{
		GameObject gameObject = new("Test_Test", typeof(MeshFilter), typeof(MeshRenderer));
		gameObject.transform.position = planeTexturePosition;
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
		meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		meshRenderer.receiveShadows = false;
		Mesh mesh = new();

		Vector3[] planeVertices = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(chunkSize, 0f, chunkSize),
			new Vector3(chunkSize, 0f, 0f),
			new Vector3(0f, 0f, chunkSize),
		};
		int[] planeTriangles = new int[]
		{
			0,1,2,
			0,3,1
		};
		Vector2[] planeUVs = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 1f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
		};

		mesh.SetVertices(planeVertices);
		mesh.SetTriangles(planeTriangles, 0);
		mesh.SetUVs(0, planeUVs);
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.Optimize();
		meshFilter.sharedMesh = mesh;

		//Material newMaterial = new(testMaterial);
		//newMaterial.mainTexture = heightMapTexture;
		meshRenderer.material.mainTexture = heightMapTexture;
	}
	public void CreateTexturePlane(Vector3 planeTexturePosition, float[,] heightMap)
	{
		GameObject gameObject = new GameObject("Test_Test", typeof(MeshFilter), typeof(MeshRenderer));
		gameObject.transform.position = planeTexturePosition;
		//gameObject.transform.localScale = new Vector3(chunkSize, 0f, chunkSize);
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
		Mesh mesh = new();

		Vector3[] planeVertices = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(chunkSize, 0f, chunkSize),
			new Vector3(chunkSize, 0f, 0f),
			new Vector3(0f, 0f, chunkSize),
		};
		int[] planeTriangles = new int[]
		{
			0,1,2,
			0,3,1
		};
		Vector2[] planeUVs = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 1f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
		};

		mesh.SetVertices(planeVertices);
		mesh.SetTriangles(planeTriangles, 0);
		mesh.SetUVs(0, planeUVs);
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.Optimize();
		meshFilter.sharedMesh = mesh;

		Texture2D texture = new(chunkSize, chunkSize);
		Color[] colors = new Color[chunkSize * chunkSize];

		for (int y = 0; y < chunkSize; y++)
		{
			for (int x = 0; x < chunkSize; x++)
			{
				float value = heightMap[x, y] / chunkHeight;
				colors[y * chunkSize + x] = Color.Lerp(Color.black, Color.white, value);
			}
		}
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels(colors);
		texture.Apply();

		Material newMaterial = new(testMaterial);
		newMaterial.mainTexture = texture;
		meshRenderer.sharedMaterial = newMaterial;
	}

	//public void GenerateCube()
	//{
	//	Mesh mesh = new Mesh();
	//	List<CubeMeshVertex> vertices = new (8);
	//	List<int> triangles = new (36);
	//	BlockMeshFace[] faces = new BlockMeshFace[]
	//	{
	//		BlockMeshFace.Back,
	//		BlockMeshFace.Up,
	//		BlockMeshFace.Front,
	//		BlockMeshFace.Down,
	//	};

	//	BlockFacesMask facesMask = new BlockFacesMask(faces);
	//	BlockFacesMask addedFacesMask = new BlockFacesMask();


	//	//if (facesMask.Contains(BlockMeshFace.Back))
	//	//{
	//	//	vertices.Add(CubeMeshVertex.LeftBackDownVertex);
	//	//	vertices.Add(CubeMeshVertex.RightBackUpVertex);
	//	//	vertices.Add(CubeMeshVertex.RightBackDownVertex);
	//	//	vertices.Add(CubeMeshVertex.LeftBackUpVertex);

	//	//	if (facesMask.Contains(BlockMeshFace.Up))
	//	//	{
	//	//		vertices.Add(CubeMeshVertex.RightFrontUpVertex);
	//	//		vertices.Add(CubeMeshVertex.LeftFrontUpVertex);
	//	//	}
	//	//	if (facesMask.Contains(BlockMeshFace.Front))
	//	//	{
	//	//		vertices.Add(CubeMeshVertex.RightFrontDownVertex);
	//	//		vertices.Add(CubeMeshVertex.LeftFrontDownVertex);
	//	//	}
	//	//}

	//	vertices.Add(CubeMeshVertex.LeftBackDownVertex);
	//	vertices.Add(CubeMeshVertex.RightBackUpVertex);
	//	vertices.Add(CubeMeshVertex.RightBackDownVertex);
	//	vertices.Add(CubeMeshVertex.LeftBackUpVertex);

	//	vertices.Add(CubeMeshVertex.RightFrontUpVertex);
	//	vertices.Add(CubeMeshVertex.LeftFrontUpVertex);

	//	vertices.Add(CubeMeshVertex.RightFrontDownVertex);
	//	vertices.Add(CubeMeshVertex.LeftFrontDownVertex);

	//	for (int i = 0; i < vertices.Count; i++)
	//	{
	//		switch (i)
	//		{
	//			case 0:
	//				triangles.Add(i);
	//				triangles.Add(i + 1);
	//				triangles.Add(i + 2);

	//				triangles.Add(i);
	//				triangles.Add(i + 3);
	//				triangles.Add(i + 1);
	//				i = 1;
	//				break;
	//			case 3:
	//				triangles.Add(i);
	//				triangles.Add(i + 1);
	//				triangles.Add(i - 2);

	//				triangles.Add(i);
	//				triangles.Add(i + 2);
	//				triangles.Add(i + 1);
	//				break;
	//			case 5:
	//				triangles.Add(i);
	//				triangles.Add(i + 1);
	//				triangles.Add(i - 1);

	//				triangles.Add(i);
	//				triangles.Add(i + 2);
	//				triangles.Add(i + 1);
	//				break;
	//			case 7:
	//				triangles.Add(i);
	//				triangles.Add(i - 5);
	//				triangles.Add(i - 1);

	//				triangles.Add(i);
	//				triangles.Add(i - i);
	//				triangles.Add(i - 5);
	//				break;
	//		}
	//	}

	//	triangles.Add(2);
	//	triangles.Add(4);
	//	triangles.Add(6);

	//	triangles.Add(2);
	//	triangles.Add(1);
	//	triangles.Add(4);

	//	triangles.Add(7);
	//	triangles.Add(3);
	//	triangles.Add(0);

	//	triangles.Add(7);
	//	triangles.Add(5);
	//	triangles.Add(3);

	//	for (int i = 0; i < triangles.Count; i+=3)
	//	{
	//		Debug.Log($"Triangle {triangles[i]}, {triangles[i + 1]}, {triangles[i + 2]}");
	//	}

	//	for (int i = 0; i < vertices.Count; i++)
	//	{
	//		Debug.Log(i);
	//		Debug.Log(vertices[i].ToString());
	//	}

	//	mesh.vertices = vertices.ToVector3Array();
	//	mesh.triangles = triangles.ToArray();

	//	GameObject newGO = new GameObject("Cube", typeof(MeshFilter), typeof(MeshRenderer));
	//	cube = newGO;
	//	newGO.GetComponent<MeshFilter>().mesh = null;
	//	newGO.GetComponent<MeshFilter>().mesh = mesh;
	//}
	//public void GenerateCube()
	//{
	//	Mesh mesh = new Mesh();
	//	BlockMeshGenerator.GetFullCube(out CubeMeshVertex[] vertices, out int[] triangles, out Vector2[] uvs);
	//	BlockMeshGenerator.GetFullCube(out CubeMeshVertex[] vertices1, out int[] triangles1, out Vector2[] uvs1);

	//	vertices1.SetOffsetForEach(new Vector3Int(1, 0, 0));
	//	vertices = vertices.Concat(vertices1).ToArray();
	//	triangles = triangles.ConcatWithOffset(triangles1, 8).ToArray();
	//	uvs = uvs.Concat(uvs1).ToArray();

	//	GameObject newGO = Instantiate(testPrefab);
	//	mesh.Optimize();
	//	mesh.RecalculateNormals();
	//	newGO.GetComponent<MeshFilter>().mesh = mesh;
	//	cube = newGO;

	//	mesh.vertices = vertices.ToVector3Array();
	//	mesh.triangles = triangles;
	//	mesh.uv = uvs;
	//	Logger.Cycle(triangles);
	//}
	//public void GenerateCube()
	//{
	//	Mesh mesh = new ();
	//	BlockMeshGenerator.GetFullCube(out CubeMeshVertex[] vertices, out int[] triangles, out Vector2[] uvs);
	//	BlockMeshGenerator.GetFullCube(out CubeMeshVertex[] vertices1, out int[] triangles1, out Vector2[] uvs1);

	//	BlockFacesMask facesMask = new (BlockMeshFaceDirection.Back, BlockMeshFaceDirection.Up);
	//	for (int i = 0; i < 6; i++)
	//	{

	//	}

	//	vertices1.SetOffsetForEach(new Vector3Int(1, 0, 0));
	//	vertices = vertices.Concat(vertices1).ToArray();
	//	triangles = triangles.ConcatWithOffset(triangles1, 8).ToArray();
	//	uvs = uvs.Concat(uvs1).ToArray();

	//	GameObject newGO = Instantiate(testPrefab);
	//	mesh.Optimize();
	//	mesh.RecalculateNormals();
	//	newGO.GetComponent<MeshFilter>().mesh = mesh;
	//	cube = newGO;

	//	mesh.vertices = vertices.ToVector3Array();
	//	mesh.triangles = triangles;
	//	mesh.uv = uvs;
	//	Logger.Cycle(triangles);
	//}
	public void GenerateCube()
	{
		Mesh mesh = new();

		BlockMeshSide[] faceDirs = new BlockMeshSide[]
		{
			BlockMeshSide.Back,
			BlockMeshSide.Up,
			BlockMeshSide.Front,
			BlockMeshSide.Down,
			BlockMeshSide.Right,
			BlockMeshSide.Left
		};
		List<BlockMeshSide> faces = new();
		BlockMeshVertexList vertices = new (24);
		BlockMeshFaceTriangleList triangles = new (12);
		List<Vector2> uvs = new (24);

		BlockMeshCalculator.GetBlockMeshFaces(faces, Vector3Int.zero, vertices, triangles, uvs);

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.uv = uvs.ToArray();
		Logger.Cycle(triangles, triangles.Count);
		mesh.Optimize();
		mesh.RecalculateNormals();

		GameObject newGO = Instantiate(testPrefab);
		newGO.GetComponent<MeshFilter>().mesh = mesh;
		cube = newGO;
	}

	public void DeleteRandomBlock()
	{
		System.Random random = new(44);
		//Vector3Int blockPosition = new(random.Next(0, 16), random.Next(0, 16), random.Next(0, 16));
		Vector3Int blockPosition = new(0, 7, 0);

		Chunk chunk = chunks[new Vector3Int(0, 0, 0)];
		MeshFilter meshFilter = chunk.GetComponent<MeshFilter>();
		List<Vector3> vertices = new(meshFilter.mesh.vertices.Length);
		List<int> triangles = new(meshFilter.mesh.triangles.Length);
		meshFilter.mesh.GetVertices(vertices);
		meshFilter.mesh.GetTriangles(triangles, 0);
		vertices.RemoveRange(vertices.Count - 4, 4);
		//if (vertices.Remove(blockPosition))
		//{ Debug.Log("removed"); }

		//for (int i = 0; i < triangles.Count; i++)
		//{
		//	triangles[i] -= 4;
		//}

		triangles.RemoveRange(triangles.Count - 6, 6);

		//triangles.Remove();
		//Logger.Cycle(vertices, vertices.Count);
		Logger.Cycle(triangles, triangles.Count);
		meshFilter.mesh.SetTriangles(triangles, 0);
		meshFilter.mesh.SetVertices(vertices);
	}

	public void Test3()
	{
		GameObject gameObject = new GameObject("test3", typeof(MeshFilter), typeof(MeshRenderer));
		test3 = gameObject;
		gameObject.transform.position = new Vector3(16, 0, 0);
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[]
		{
			new Vector3(0, 0, 0),
			new Vector3(1, 1, 0),
			new Vector3(1, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 1),
			new Vector3(1, 0, 1),
			new Vector3(1, 1, 1),
			new Vector3(0, 1, 1),
		};
		int[] triangles = new int[]
		{
			0,1,2,
			0,3,1
		};
		mesh.SetVertices(vertices);
		mesh.SetTriangles(triangles, 0);
		mesh.RecalculateNormals();
		mesh.Optimize();
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
	}

	public void TestSpeed()
	{
		Stopwatch sw = new Stopwatch();

		sw.Start();
		for (int i = 0; i < 500; i++)
		{
		}
		sw.Stop();
		Debug.Log(sw.ElapsedTicks);
		sw.Reset();

		sw.Start();
		for (int i = 0; i < 500; i++)
		{
		}

		sw.Stop();
		Debug.Log(sw.ElapsedTicks);
	}

	private void Test(ref Vector3 left, ref Vector3 right)
	{
	}

	private void Test1(Vector3 left, Vector3 right)
	{
	}

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
	}

	private void OnDrawGizmos()
	{
		if (cube == null)
			return;

		Gizmos.color = Color.red;
		for (int i = 0; i < cube.GetComponent<MeshFilter>().mesh.vertices.Length; i++)
		{
			Gizmos.DrawSphere(cube.GetComponent<MeshFilter>().mesh.vertices[i], 0.15f);
		}
	}
}
