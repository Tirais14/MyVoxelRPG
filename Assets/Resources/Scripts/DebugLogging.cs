using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugLogging : MonoBehaviour
{
	public static DebugLogging Instance { get; private set; }
	[SerializeField] private bool isDebugEnabled;
	[SerializeField] private bool isMessagesEnabled;
	[SerializeField] private bool isWarningsEnabled;
	[SerializeField] private bool isErrorsEnabled;

	public static void SendMessage<T>(GameObject gameObject, T message, [CallerMemberName] string methodName = "")
	{
		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{ Debug.Log($"{gameObject.name}>>{methodName}:{message}"); }
	}
	public static void SendMessage<T>(T message, [CallerMemberName] string methodName = "")
	{
		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{ Debug.Log($"{methodName}:{message}"); }
	}
	public static void Cycle<T>(GameObject gameObject, T[] array, [CallerMemberName] string methodName = "")
	{
		if (array == null)
		{ return; }

		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{
			for (int i = 0; i < array.Length; i++)
			{ Debug.Log($"{gameObject}:{methodName}:{nameof(array)} value = {array[i]}"); }
		}
	}
	public static void Cycle<T>(GameObject gameObject, List<T> list, [CallerMemberName] string methodName = "")
	{
		if (list == null)
		{ return; }

		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{
			for (int i = 0; i < list.Count; i++)
			{ Debug.Log($"{gameObject}:{methodName}:{nameof(list)} value = {list[i]}"); }
		}
	}


	public static void SendWarning<T>(GameObject gameObject, T message, [CallerMemberName] string methodName = "")
	{
		if (Instance.isDebugEnabled && Instance.isWarningsEnabled)
		{ Debug.LogWarning($"{gameObject.name}>>{methodName}:{message}"); }
	}
	public static void SendWarning<T>(T message, [CallerMemberName] string methodName = "")
	{
		if (Instance.isDebugEnabled && Instance.isWarningsEnabled)
		{ Debug.LogWarning($"{methodName}:{message}"); }
	}
	public static void WrongParameterWarning<T>(GameObject gameObject, T parameter, [CallerMemberName] string methodName = null)
	{
		if (Instance.isDebugEnabled && Instance.isWarningsEnabled)
		{ Debug.LogWarning($"{gameObject.name}:{methodName}:Wrong parameter {typeof(T).Name} > {parameter}"); }
	}


	public static void SendError<T>(GameObject gameObject, T message, [CallerMemberName]string methodName = "")
	{
		if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		{ Debug.LogError($"{gameObject.name}>>{methodName}:{message}"); }
	}
	public static void SendError<T>(T message, [CallerMemberName]string methodName = "")
	{
		if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		{ Debug.LogError($"{methodName}:{message}"); }
	}
	public static void WrongParameter<T>(GameObject gameObject, T parameter, [CallerMemberName] string methodName = null)
	{
		if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		{ Debug.LogError($"{gameObject.name}:{methodName}:Wrong parameter {typeof(T).Name} > {parameter}"); }
	}
	public static void NullParameter<T>(GameObject gameObject, [CallerMemberName] string methodName = null)
	{
		if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		{ Debug.LogError($"{gameObject.name}:{methodName}:Parameter {typeof(T).Name} is null"); }
	}
	public static void NullParameter<T>([CallerMemberName] string methodName = null)
	{
		if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		{ Debug.LogError($"{methodName}:Parameter {typeof(T).Name} is null"); }
	}

	public void Awake()
	{
		Instance = this;
	}
}
