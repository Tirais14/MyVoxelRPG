using UnityEngine;
using UnityEngine.EventSystems;

public class GetPointerEventData : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData) => GetData(eventData);

	public void OnPointerUp(PointerEventData eventData) {}

	public void OnPointerMove(PointerEventData eventData) => GetData(eventData);

	private void GetData(PointerEventData eventData)
	{
		GlobalProperties.SetPointerEventData(eventData);
		Destroy(gameObject);
	}

	private void Start()
	{
		RectTransform screen = GetComponent<RectTransform>();
		screen.sizeDelta = new Vector2(GlobalProperties.MainCamera.scaledPixelWidth, GlobalProperties.MainCamera.pixelHeight);
	}
}
