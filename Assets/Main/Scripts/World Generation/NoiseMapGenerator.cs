using UnityEngine;

public static class NoiseMapGenerator
{
	public static int[,] GetHeightMapArrayInt(int size, float scale, ref Vector2 offset, int heightValueMultiplier = 1, int randomSeed = 0)
	{
		return GetHeightMapArrayInt(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	}
	//public static int[,] GetHeightMapArrayInt(int size, float scale, ref Vector2ReadOnly offset, int heightValueMultiplier = 1, int randomSeed = 0)
	//{
	//	return GetHeightMapArrayInt(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	//}
	public static int[,] GetHeightMapArrayInt(int size, float scale, float offsetX, float offsetY, int heightValueMultiplier = 1, int randomSeed = 0)
	{
		float[,] heightMapArray = GetHeightMapArray(size, scale, offsetX, offsetY, heightValueMultiplier, randomSeed);
		int[,] heightMapArrayInt = new int[size, size];

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{ heightMapArrayInt[x, y] = Mathf.RoundToInt(heightMapArray[x, y]); }
		}

		return heightMapArrayInt;
	}

	public static float[,] GetHeightMapArray(int size, float scale, ref Vector2 offset, int heightValueMultiplier = 1, int randomSeed = 0)
	{
		return GetHeightMapArray(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	}
	//public static float[,] GetHeightMapArray(int size, float scale, ref Vector2ReadOnly offset, int heightValueMultiplier = 1, int randomSeed = 0)
	//{
	//	return GetHeightMapArray(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	//}
	public static float[,] GetHeightMapArray(int size, float scale, float offsetX, float offsetY, int heightValueMultiplier = 1, int randomSeed = 0)
	{
		//Random random = new();
		//if (randomSeed == 0)
		//{ randomSeed = random.Next(0, int.MaxValue); }
		if (scale <= 0)
		{ Logger.WrongParameter<float>(); }
		if (heightValueMultiplier == 0)
		{ Logger.WrongParameter<int>(); }

		float[,] heightMapArray = new float[size, size];
		float minHeghtValue = float.MaxValue;
		float maxHeightValue = float.MinValue;
		float mapXCoord;
		float mapYCoord;
		float perlinValue;
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				mapXCoord = (float)x / size * scale + offsetX / size * scale;
				mapYCoord = (float)y / size * scale + offsetY / size * scale;

				perlinValue = Mathf.PerlinNoise(mapXCoord, mapYCoord);
				heightMapArray[x, y] = perlinValue;

				if (perlinValue > maxHeightValue)
				{ maxHeightValue = perlinValue; }
				else if (perlinValue < minHeghtValue)
				{ minHeghtValue = perlinValue; }
			}
		}

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{ heightMapArray[x, y] = Mathf.InverseLerp(minHeghtValue, maxHeightValue, heightMapArray[x, y]) * heightValueMultiplier; }
		}

		return heightMapArray;
	}

	public static Texture2D GetHeightMapTexture(int size, float scale, ref Vector2 offset, int randomSeed = 0)
	{
		return GetHeightMapTexture(size, scale, offset.x, offset.y, randomSeed);
	}
	//public static Texture2D GetHeightMapTexture(int size, float scale, ref Vector2ReadOnly offset, int randomSeed = 0)
	//{
	//	return GetHeightMapTexture(size, scale, offset.x, offset.y, randomSeed);
	//}
	public static Texture2D GetHeightMapTexture(int size, float scale, float offsetX, float offsetY, int randomSeed = 0)
	{
		float[,] heightMapArray = GetHeightMapArray(size, scale, offsetX, offsetY, 1, randomSeed);
		Color32[] colorsArray = new Color32[size * size];
		Texture2D heightMap = new(size, size);
		Color32 black = new(0, 0, 0, 255);
		Color32 white = new(255, 255, 255, 255);

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)			
			{
				float heightValue = heightMapArray[x, y];
				colorsArray[y * size + x] = Color32.Lerp(black, white, heightValue);
			}
		}

		heightMap.SetPixels32(colorsArray);
		heightMap.filterMode = FilterMode.Point;
		heightMap.wrapMode = TextureWrapMode.Clamp;
		heightMap.Apply();

		return heightMap;
	}

	public static Texture2D GetHeightMapTextureBy(int[,] heightMapArray, int heightValueDivider = 1)
	{
		int width = heightMapArray.GetLength(0);
		int height = heightMapArray.GetLength(1);
		Color32[] colorsArray = new Color32[width * height];
		Texture2D heightMap = new(width, height);
		Color32 black = new(0, 0, 0, 255);
		Color32 white = new(255, 255, 255, 255);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{ colorsArray[y * width + x] = Color32.Lerp(black, white, (float)heightMapArray[x, y] / heightValueDivider); }
		}

		heightMap.SetPixels32(colorsArray);
		heightMap.filterMode = FilterMode.Point;
		heightMap.wrapMode = TextureWrapMode.Clamp;
		heightMap.Apply();

		return heightMap;
	}
	public static Texture2D GetHeightMapTextureBy(float[,] heightMapArray, int heightValueDivider = 1)
	{
		int width = heightMapArray.GetLength(0);
		int height = heightMapArray.GetLength(1);
		Color32[] colorsArray = new Color32[width * height];
		Texture2D heightMap = new(width, height);
		Color32 black = new(0, 0, 0, 255);
		Color32 white = new(255, 255, 255, 255);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{ colorsArray[y * width + x] = Color32.Lerp(black, white, heightMapArray[x, y] / heightValueDivider); }
		}

		heightMap.SetPixels32(colorsArray);
		heightMap.filterMode = FilterMode.Point;
		heightMap.wrapMode = TextureWrapMode.Clamp;
		heightMap.Apply();

		return heightMap;
	}

	public static float GetHeightMapPoint(int x, int y, int size, float scale, ref Vector2 offset,
															int heightValueMultiplier = 1, int randomSeed = 0)
	{
		return GetHeightMapPoint(x, y, size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	}
	//public static float GetHeightMapPoint(int x, int y, int size, float scale, ref Vector2ReadOnly offset,
	//														int heightValueMultiplier = 1, int randomSeed = 0)
	//{
	//	return GetHeightMapPoint(x, y, size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	//}
	public static float GetHeightMapPoint(int x, int y, int size, float scale, float offsetX, float offsetY,
															int heightValueMultiplier = 1, int randomSeed = 0)
	{
		float mapXCoord;
		float mapYCoord;
		float noiseValue;

		mapXCoord = (float)x / size * scale + offsetX / size * scale;
		mapYCoord = (float)y / size * scale + offsetY / size * scale;

		noiseValue = Mathf.PerlinNoise(mapXCoord, mapYCoord);


		noiseValue = Mathf.InverseLerp(0, 1f, noiseValue) * heightValueMultiplier;

		return noiseValue;
	}

	public static float[,] GetHeightMapArrayByPoints(int size, float scale, ref Vector2 offset,
															int heightValueMultiplier = 1, int randomSeed = 0)
	{
		return GetHeightMapArrayByPoints(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	}
	//public static float[,] GetHeightMapArrayByPoints(int size, float scale, ref Vector2ReadOnly offset,
	//														int heightValueMultiplier = 1, int randomSeed = 0)
	//{
	//	return GetHeightMapArrayByPoints(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	//}
	public static float[,] GetHeightMapArrayByPoints(int size, float scale, float offsetX, float offsetY,
															int heightValueMultiplier = 1, int randomSeed = 0)
	{
		float[,] heightMapArray = new float[size, size];

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{ heightMapArray[x, y] = GetHeightMapPoint(x, y, size, scale, offsetX, offsetY, heightValueMultiplier, randomSeed); }
		}

		return heightMapArray;
	}

	public static int[,] GetHeightMapArrayIntByPoints(int size, float scale, ref Vector2 offset,
															int heightValueMultiplier = 1, int randomSeed = 0)
	{
		return GetHeightMapArrayIntByPoints(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	}
	//public static int[,] GetHeightMapArrayIntByPoints(int size, float scale, ref Vector2ReadOnly offset,
	//														int heightValueMultiplier = 1, int randomSeed = 0)
	//{
	//	return GetHeightMapArrayIntByPoints(size, scale, offset.x, offset.y, heightValueMultiplier, randomSeed);
	//}
	public static int[,] GetHeightMapArrayIntByPoints(int size, float scale, float offsetX, float offsetY,
															int heightValueMultiplier = 1, int randomSeed = 0)
	{
		int[,] heightMapArrayInt = new int[size, size];
		float heightValue;

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				heightValue = GetHeightMapPoint(x, y, size, scale, offsetX, offsetY, heightValueMultiplier, randomSeed);
				heightMapArrayInt[x, y] = Mathf.RoundToInt(heightValue);
			}
		}

		return heightMapArrayInt;
	}

	public static Texture2D GetHeightMapTextureByPoints(int size, float scale, ref Vector2 offset, int randomSeed = 0)
	{
		return GetHeightMapTextureByPoints(size, scale, offset.x, offset.y, randomSeed);
	}
	//public static Texture2D GetHeightMapTextureByPoints(int size, float scale, ref Vector2ReadOnly offset, int randomSeed = 0)
	//{
	//	return GetHeightMapTextureByPoints(size, scale, offset.x, offset.y, randomSeed);
	//}
	public static Texture2D GetHeightMapTextureByPoints(int size, float scale, float offsetX, float offsetY, int randomSeed = 0)
	{
		Color32[] colorsArray = new Color32[size * size];
		Texture2D heightMap = new(size, size);
		Color32 black = new(0, 0, 0, 255);
		Color32 white = new(255, 255, 255, 255);

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				float heightValue = GetHeightMapPoint(x, y, size, scale, offsetX, offsetY, 1, randomSeed);
				colorsArray[y * size + x] = Color32.Lerp(black, white, heightValue);
			}
		}

		heightMap.SetPixels32(colorsArray);
		heightMap.filterMode = FilterMode.Point;
		heightMap.wrapMode = TextureWrapMode.Clamp;
		heightMap.Apply();

		return heightMap;
	}
}
