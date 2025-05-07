using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	//[SerializeField] private int mapWidth;
	//[SerializeField] private int mapHeight;
	//[SerializeField] private float noiseScale;
	//[SerializeField] private float octaves;
	//[SerializeField] private float persistance;
	//[SerializeField] private float lacunarity;
	//[SerializeField] private bool autoUpdate;
	//public bool AutoUpdate => autoUpdate;

	//public void GenerateMap()
	//{
	//	float[,] nosieMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity);

	//	MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
	//	mapDisplay.DrawNoiseMap(nosieMap);
	//}
	public static MapGenerator Instance;
	[SerializeField] private DrawMode drawMode;
	[SerializeField] private int mapWidth;
	[SerializeField] private int mapHeight;
	[SerializeField] private float noiseScale;
	public float NoiseScale => noiseScale;

	[SerializeField] private int octaves;
	public int Octaves => octaves;
	[Range(0, 1)]
	[SerializeField] private float persistance;
	public float Persistance => persistance;
	[SerializeField] private float lacunarity;
	public float Lancunarity => lacunarity;

	[SerializeField] private int seed;
	public int Seed => seed;
	[SerializeField] private Vector2 offset;
	public Vector2 Offset => offset;

	[SerializeField] private bool autoUpdate;
	public bool AutoUpdate => autoUpdate;
	[SerializeField] private TerrainType[] regions;
	public TerrainType[] Regions => regions;

	public void GenerateMap()
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
		Color[] colorMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						colorMap[y * mapWidth + x] = regions[i].color;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay>();
		if (drawMode == DrawMode.NoiseMap)
		{ display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap)); }
		else if (drawMode == DrawMode.ColorMap)
		{ display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight)); }
	}

	private void Awake()
	{
		Instance = this;
	}

	void OnValidate()
	{
		if (mapWidth < 1)
		{
			mapWidth = 1;
		}
		if (mapHeight < 1)
		{
			mapHeight = 1;
		}
		if (lacunarity < 1)
		{
			lacunarity = 1;
		}
		if (octaves < 0)
		{
			octaves = 0;
		}
	}
}
