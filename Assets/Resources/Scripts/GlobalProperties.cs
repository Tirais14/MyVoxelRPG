using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GlobalProperties : MonoBehaviour
{
	public static PointerEventData PointerEventData { get; private set; }
	public static GraphicRaycaster GraphicRaycaster { get; private set; }
	public static Vector3 ScreenCenterPoint {  get; private set; }
	public static Camera MainCamera { get; private set; }
	public static Ray FromScreenCenterRay { get; private set; }
	public static ReadOnlyCollection<RaycastResult> LeftClickUnderPointerObjects { get; private set; }

#pragma warning disable IDE0060 // Удалите неиспользуемый параметр
	public static void MouseLeftButtonAction(InputAction.CallbackContext context)
#pragma warning restore IDE0060 // Удалите неиспользуемый параметр
	{
		LeftClickUnderPointerObjects = Utils.GetUnderPointerRaycastsResults();
	}

	public static void SetPointerEventData(PointerEventData pointerEventData)
	{
		if (pointerEventData == null)
		{
			Debug.Log("pointerEventData is null");
			return;
		}
		PointerEventData = pointerEventData;
	}

	public void Initialize()
	{
		GraphicRaycaster = FindObjectOfType<GraphicRaycaster>();
		MainCamera = Camera.main;
		ScreenCenterPoint = new Vector3(MainCamera.pixelWidth / 2, MainCamera.pixelHeight / 2, 0f);
		FromScreenCenterRay = MainCamera.ScreenPointToRay(ScreenCenterPoint);
	}
}
