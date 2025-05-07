using UnityEngine;
using UnityEngine.EventSystems;

public class GetGlobalProperties : MonoBehaviour, IPointerMoveHandler
{
	public void OnPointerMove(PointerEventData eventData)
	{
		GlobalProperties.PointerEventData = eventData;
		Destroy(this);
	}
}
