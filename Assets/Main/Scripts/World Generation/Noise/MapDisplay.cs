using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
	[SerializeField] private Renderer textureRenderer;

	public void DrawTexture(Texture2D texture)
	{
		textureRenderer.sharedMaterial.mainTexture = texture;
		textureRenderer.transform.localScale = new Vector3(texture.width, 1f, texture.height);
	}
}
