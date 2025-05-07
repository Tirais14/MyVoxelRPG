using UnityEngine;

public static class Noise
{
	//public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float octaves, float persistance, float lacunarity)
	//{
	//	float[,] noiseMap = new float[mapWidth, mapHeight];

	//	if (scale <= 0)
	//	{ scale = 0.0001f; }

	//	float maxNoiseHeight = float.MaxValue;
	//	float minNoiseHeight = float.MinValue;

	//	for (int y = 0; y < mapHeight; y++) 
	//	{ 
	//		for (int x = 0; x < mapWidth; x++) 
	//		{
	//			float amplitude = 1f;
	//			float frequency = 1f;
	//			float noiseHeight = 0f;
	//			for (int i = 0; i < octaves; i++)
	//			{
	//				float sampleX = x / scale * frequency;
	//				float sampleY = y / scale * frequency;

	//				float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
	//				noiseHeight += perlinValue * amplitude;

	//				amplitude *= persistance;
	//				frequency *= lacunarity;
	//			}

	//			if (noiseHeight > maxNoiseHeight)
	//			{ maxNoiseHeight = noiseHeight; }
	//			else if (noiseHeight < minNoiseHeight)
	//			{ minNoiseHeight = noiseHeight; }

	//			noiseMap[x, y] = noiseHeight;
	//		}
	//	}

	//	for (int y = 0; y < mapHeight; y++)
	//	{
	//		for (int x = 0; x < mapWidth; x++)
	//		{ noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); }
	//		//{ noiseMap[x, y] = Mathf.Clamp(noiseMap[x, y], minNoiseHeight, maxNoiseHeight); }

	//	}

	//	for (int y = 0; y < mapHeight; y++)
	//	{
	//		for (int x = 0; x < mapWidth; x++)
	//		{ Debug.Log(noiseMap[x, y]); }
	//	}

	//	return noiseMap;
	//}
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
	{
		float[,] noiseMap = new float[mapWidth, mapHeight];

		System.Random prng = new(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000, 100000) + offset.x;
			float offsetY = prng.Next(-100000, 100000) + offset.y;
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		if (scale <= 0)
		{ scale = 0.0001f; }

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight)
				{ maxNoiseHeight = noiseHeight; }
				else if (noiseHeight < minNoiseHeight)
				{ minNoiseHeight = noiseHeight; }
				noiseMap[x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{ noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); }
		}

		return noiseMap;
	}

	public static float GenerateNoise(int posX, int posZ, int seed, float scale, int octave, float persistance, float lacunarity, Vector2 offset)
	{
		float noise;

		System.Random prng = new(seed);
		Vector2[] octaveOffsets = new Vector2[octave];
		for (int i = 0; i < octave; i++)
		{
			float offsetX = prng.Next(-100000, 100000) + offset.x;
			float offsetY = prng.Next(-100000, 100000) + offset.y;
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		if (scale <= 0)
		{ scale = 0.0001f; }

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float amplitude = 1;
		float frequency = 1;
		float noiseHeight = 0;

		for (int i = 0; i < octave; i++)
		{
			float sampleX = posX / scale * frequency + octaveOffsets[i].x;
			float sampleY = posZ / scale * frequency + octaveOffsets[i].y;

			float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
			noiseHeight += perlinValue * amplitude;

			amplitude *= persistance;
			frequency *= lacunarity;
		}

		if (noiseHeight > maxNoiseHeight)
		{ maxNoiseHeight = noiseHeight; }
		else if (noiseHeight < minNoiseHeight)
		{ minNoiseHeight = noiseHeight; }

		noise = noiseHeight;

		noise = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noise);

		return noise;
	}
}
