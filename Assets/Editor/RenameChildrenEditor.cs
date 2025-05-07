using UnityEditor;
using UnityEngine;

public class RenameChildrenEditor : EditorWindow
{
	private static readonly Vector2Int size = new(250, 100);
	private string childrenPrefix;
	private int startIndex;

	[MenuItem("GameObject/Rename Children")]
	private static void ShowWindow()
	{
		EditorWindow window = GetWindow<RenameChildrenEditor>();
		window.minSize = size;
		window.maxSize = size;
	}

	private void OnGUI()
	{
		childrenPrefix = EditorGUILayout.TextField("Chidlren Prefix", childrenPrefix);
		startIndex = EditorGUILayout.IntField("Start Index", startIndex);

		if(GUILayout.Button("Rename Children"))
		{
			GameObject[] selectedObjects = Selection.gameObjects;
			for (int objectIndex = 0; objectIndex < selectedObjects.Length; objectIndex++)
			{
				Transform selectedObjectTr = selectedObjects[objectIndex].transform;
				for (int childIndex = 0, i = startIndex; childIndex < selectedObjectTr.childCount; childIndex++)
				{
					selectedObjectTr.GetChild(childIndex).name = $"{childrenPrefix}{i++}";
				}
			}
		}
	}
}
