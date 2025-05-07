using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Logger : MonoBehaviour
{
	public static Logger Instance { get; private set; }
	[SerializeField] private bool isDebugEnabled;
	[SerializeField] private bool isMessagesEnabled;
	[SerializeField] private bool isWarningsEnabled;
	[SerializeField] private bool isErrorsEnabled;
	private const int stringCapacity = 300;
	[SerializeField] private string textPointer = ">>";

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


	//public static void Cycle(IList list, int length, string gameObjectName = default, [CallerMemberName] string methodName = default)
	//{
	//	if (list == null)
	//	{ return; }

	//	if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
	//	{
	//		if (gameObjectName != default)
	//		{
	//			for (int i = 0; i < length; i++)
	//			{ Debug.Log($"{gameObjectName}{Instance.textPointer}{methodName}:{nameof(list)} value = {list[i]}"); }
	//		}
	//		else
	//		{
	//			for (int i = 0; i < length; i++)
	//			{ Debug.Log($"{methodName}:{nameof(list)} value = {list[i]}"); }
	//		}
	//	}
	//}
	public static void Cycle(IEnumerable list, int length, string gameObjectName = default, [CallerMemberName] string methodName = default)
	{
		if (list == null)
		{ return; }

		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{
			if (gameObjectName != default)
			{
				foreach (var item in list)
				{ Debug.Log($"{gameObjectName}{Instance.textPointer}{methodName}:{nameof(list)} value = {item}"); }
			}
			else
			{
				foreach (var item in list)
				{ Debug.Log($"{methodName}:{nameof(list)} value = {item}"); }
			}
		}
	}
	public static void Cycle<T>(T[,] twoDimsArray, string gameObjectName = default, [CallerMemberName] string methodName = default)
	{
		if (twoDimsArray == null)
		{ return; }

		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{
			if (gameObjectName != default)
			{
				for (int y = 0; y < twoDimsArray.GetLength(1); y++)
				{
					for (int x = 0; x < twoDimsArray.GetLength(0); x++)
					{ Debug.Log($"{gameObjectName}{Instance.textPointer}{methodName}:{nameof(twoDimsArray)} value = {twoDimsArray[x, y]}"); }
				}
			}
			else
			{
				for (int y = 0; y < twoDimsArray.GetLength(1); y++)
				{
					for (int x = 0; x < twoDimsArray.GetLength(0); x++)
					{ Debug.Log($"{methodName}:{nameof(twoDimsArray)} value = {twoDimsArray[x, y]}"); }
				}
			}
		}
	}
	public static void Cycle<TKey, TValue>(Dictionary<TKey, TValue> dictrionary, 
											string gameObjectName = null, 
											[CallerMemberName] string methodName = default)
	{
		if (dictrionary == null)
		{ return; }
		StringBuilder resultString = new(stringCapacity);

		if (Instance.isDebugEnabled && Instance.isMessagesEnabled)
		{
			foreach (var collectionItem in dictrionary)
			{
				if (gameObjectName != default)
				{ resultString.Append($"{gameObjectName}>>"); }

				resultString.Append($"{methodName}:{nameof(dictrionary)} >> key = {collectionItem.Key}, value = {collectionItem.Value}");
				Debug.Log(resultString.ToString());
				resultString.Clear();
			}
		}
	}


	public static async void CycleAsync(IList list,
										int length,
										string gameObjectName = default,
										[CallerMemberName] string methodName = default)
	{
		await Task.Run(() => Cycle(list, length, gameObjectName, methodName));
	}
	public static async void CycleAsync<T>(T[,] twoDimsArray,
										string gameObjectName = default,
										[CallerMemberName] string methodName = default)
	{
		await Task.Run(() => Cycle(twoDimsArray, gameObjectName, methodName));
	}
	public static async void CycleAsync<TKey, TValue>(Dictionary<TKey, TValue> dictrionary,
													string gameObjectName = default,
													[CallerMemberName] string methodName = default)
	{
		await Task.Run(() => Cycle(dictrionary, gameObjectName, methodName));
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
		//if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		//{ Debug.LogError($"{gameObject.name}>>{methodName}:{message}"); }
		throw new Exception($"{gameObject.name}>>{methodName}:{message}");
	}
	public static void SendError<T>(T message, [CallerMemberName]string methodName = "")
	{
		//if (Instance.isDebugEnabled && Instance.isErrorsEnabled)
		//{ Debug.LogError($"{methodName}:{message}"); }
		throw new Exception($"{methodName}:{message}");
	}

	//public static void EmptyArray<T>(T[] array, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	//{
	//	if (gameObject == null)
	//	{ throw new Exception($"{methodName}:Array type of {array.GetType().Name} is empty"); }
	//	else
	//	{ throw new Exception($"{gameObject.name}{Instance.textPointer}{methodName}:Array type of {array.GetType().Name} is empty"); }
	//}
	public static void EmptyList<T>(IList<T> list, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new Exception($"{methodName}:Array type of {list.GetType().Name} is empty"); }
		else
		{ throw new Exception($"{gameObject.name}{Instance.textPointer}{methodName}:Array type of {list.GetType().Name} is empty"); }
	}

	public static void WrongParameter(string message, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new Exception($"{methodName}:{message}"); }
		else
		{ throw new Exception($"{gameObject.name}{Instance.textPointer}{methodName}:{message}"); }
	}
	public static void WrongParameter(Type type, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new Exception($"{methodName}:Wrong parameter {type.Name}"); }
		else
		{ throw new Exception($"{gameObject.name}{Instance.textPointer}{methodName}:Wrong parameter {type.Name}"); }
	}
	public static void WrongParameter<T>(T parameter, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new Exception($"{methodName}:Wrong parameter {parameter.GetType().Name}"); }
		else
		{ throw new Exception($"{gameObject.name}{Instance.textPointer}{methodName}:Wrong parameter {parameter.GetType().Name}"); }
	}
	public static void WrongParameter<T>(GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new Exception($"{methodName}:Wrong parameter {typeof(T).Name}"); }
		else
		{ throw new Exception($"{gameObject.name}{Instance.textPointer}{methodName}:Wrong parameter {typeof(T).Name}"); }
	}
	public static void NullParameter(Type type, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new NullReferenceException($"{methodName}:Parameter {type.Name} is null"); }
		else
		{ throw new NullReferenceException($"{gameObject.name}{Instance.textPointer}{methodName}:Parameter {type.Name} is null"); }
	}
	public static void NullParameter<T>(T parameter, GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new NullReferenceException($"{methodName}:Parameter {parameter.GetType().Name} is null"); }
		else
		{ throw new NullReferenceException($"{gameObject.name}{Instance.textPointer}{methodName}:Parameter {parameter.GetType().Name} is null"); }
	}
	public static void NullParameter<T>(GameObject gameObject = null, [CallerMemberName] string methodName = null)
	{
		if (gameObject == null)
		{ throw new NullReferenceException($"{methodName}:Parameter {typeof(T).Name} is null"); }
		else
		{ throw new NullReferenceException($"{gameObject.name}{Instance.textPointer}{methodName}:Parameter {typeof(T).Name} is null"); }
	}

	public void Awake()
	{
		Instance = this;

	}
}
