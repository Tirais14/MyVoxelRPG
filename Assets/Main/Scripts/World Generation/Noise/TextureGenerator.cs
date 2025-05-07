using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class TextureGenerator
{
	public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
	{
		Texture2D texture = new (width, height);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels(colorMap);
		texture.Apply();
		return texture;
	}

	public static Texture2D TextureFromHeightMap(float[,] heightMap)
	{
		int mapWidth = heightMap.GetLength(0);
		int mapHeight = heightMap.GetLength(1);

		Texture2D heightMapTexture = new(mapWidth, mapHeight);

		Color[] heightMapColours = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{ heightMapColours[y * mapWidth + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]); }
		}

		return TextureFromColorMap(heightMapColours, mapWidth, mapHeight);
	}
}
