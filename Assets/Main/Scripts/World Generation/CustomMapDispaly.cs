using UnityEngine;

public class CustomMapDispaly : MonoBehaviour
{
	[Range(0.01f, 100f)]
	[SerializeField] private float scale = 1f;
	[SerializeField] private Vector2 offset;
	[Range(0, 1024)]
	[SerializeField] private int mapSize = 32;
	[SerializeField] private int chunkSize = 32;
	[SerializeField] private bool pixelise = false;
	[SerializeField] private Material testMat;
	private MeshRenderer meshRenderer;

	private void UpdateTexture()
	{
		transform.localScale = new Vector3(mapSize, 0f, mapSize);
		meshRenderer.material.mainTexture = NoiseMapGenerator.GetHeightMapTextureByPoints(mapSize, scale, offset.x, offset.y);
	}

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	private void FixedUpdate()
	{
		UpdateTexture();
	}
}
