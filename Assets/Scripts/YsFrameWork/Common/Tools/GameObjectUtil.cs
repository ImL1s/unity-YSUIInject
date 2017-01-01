//
// /**************************************************************************
//
// GameObjectUtil.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 17-01-01
//
// Description:GameObject工具類.
//
// Copyright (c) 2017 ImL1s
//
// **************************************************************************/

#define YSLogger

using UnityEngine;
using System.Collections;
using System;

public class GameObjectUtil 
{
	public static void LogError(string error)
	{
		#if YSLogger
		YSFramework.Utils.Logger.Error (error);

		#else
		Debug.LogError(error);

		#endif
	}

	public static Component FindAndGet(Type type,string path,bool isAdd = false)
	{
		if(!type.IsSubclassOf(typeof(Component)))
		{
			LogError ("Fail to FindAndGet<T> component. Causes by type is not subclass of Commponent.\tCurrent type: " + type.ToString());
			return null;
		}

		GameObject go = GameObject.Find(path);

		if (go != null)
		{
			Component target = go.GetComponent(type);

			if (target != null)
			{
				return target;
			}
			else if (isAdd)
			{
				target = go.AddComponent(type);
				return target;
			}
		}

		LogError ("Fail to FindAndGet<T> component. Causes by can't find specific path gameObject. Component: " + type.ToString ());

		return null;
	}
}
