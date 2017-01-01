//
// /**************************************************************************
//
// IUnityManager.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 17-01-01
//
// Description:UI注入管理器.
//
// Copyright (c) 2017 ImL1s
//
// **************************************************************************/

#region define

/*
 * 
 */
#define YSLogger			// 是否導入YSLogger

#define YSUtils				// 是否導入YSUtils

#endregion

#region using

using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using YSFramework;
using YSFramework.Utils;

#endregion

public class InjectManager : BaseManager<InjectManager> 
{
	#region 內部資料類; inner data class

	private class InjectInfo<T>
		where T:MemberInfo
	{
		private Type currentType = null;

		private MemberInfo[] memberInfos = null;

		public MemberInfo[] MemberInfos 
		{
			get 
			{
				return memberInfos;
			}
		}

		public T this[int index]
		{
			get
			{
				return (T)MemberInfos [index];
			}
		}


		public InjectInfo(){}

		public InjectInfo(MemberInfo[] memberInfos)
		{
			this.memberInfos = memberInfos;
			currentType = typeof(T);
		}

		/// <summary>
		/// 取得指定字段或是屬性的Type,因為該MemberInfo有可能是PropertyInfo、FieldInfo 或是其他繼承MemberInfo的子類,
		/// </summary>
		/// <returns>The field or property type.</returns>
		/// <param name="index">Index.</param>
		public Type GetFieldOrPropertyType(int index)
		{
			if(currentType.Equals(typeof(FieldInfo)))
			{
				return (this [index] as FieldInfo).FieldType;
			}
			else
			{
				return (this [index] as PropertyInfo).PropertyType;
			}
		}

		/// <summary>
		/// 設定指定的MemberInfo的值,因為該MemberInfo有可能是PropertyInfo、FieldInfo 或是其他繼承MemberInfo的子類,所以推薦藉由此方法設定Value.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="obj">Object.</param>
		/// <param name="value">Value.</param>
		public void SetValue(int index,object obj,object value)
		{
			MemberInfo info = this [index];

			if(currentType.Equals(typeof(FieldInfo)))
			{
				(info as FieldInfo).SetValue (obj, value);
			}
			else
			{
				(info as PropertyInfo).SetValue (obj, value, null);
			}
		}

		/// <summary>
		/// 取得指定MemberInfo中的特性數組,因為該MemberInfo有可能是PropertyInfo、FieldInfo 或是其他繼承MemberInfo的子類,所以推薦使用此方法取的特性;
		/// </summary>
		/// <returns>The custom attributes.</returns>
		/// <param name="index">Index.</param>
		/// <param name="attrubuteType">Attrubute type.</param>
		public object[] GetCustomAttributes(int index, Type attrubuteType)
		{
			MemberInfo info = this [index];

			return info.GetCustomAttributes (attrubuteType, true);
		}
	}

	#endregion


	#region 靜態方法; static method

	public static void LogError(string error)
	{
		#if YSLogger
		YSFramework.Utils.Logger.Error (error);
		#elif
		Debug.LogError(error);
		#endif
	}

	/// <summary>
	/// 注入傳入Panel所有擁有ViewInject特性的UI
	/// </summary>
	/// <param name="panel">Panel.</param>
	public static void Inject(MonoBehaviour panel)
	{
		Instance.DoInject (panel);
	}

	#endregion


	#region 實例方法; instance method

	/// <summary>
	/// 注入傳入Panel所有擁有ViewInject特性的UI
	/// </summary>
	/// <param name="panel">Panel.</param>
	public void DoInject(MonoBehaviour panel)
	{
		Type panelType = panel.GetType ();

		if(panel != null && panelType != typeof(MonoBehaviour))
		{
			MemberInfo[] infos = panelType.GetFields();	// 取得所有字段集合

			InjectFromMemberInfo (panelType, panel, new InjectInfo<FieldInfo>(infos));
			#region 棄用方法
//			InjectFromField (panelType, panel, fieldInfos);
			#endregion

			infos = panelType.GetProperties(); 			// 取得所有屬性資訊集合
			InjectFromMemberInfo (panelType, panel, new InjectInfo<PropertyInfo>(infos));
			#region 棄用方法
//			InjectFromProperty (panelType, panel, propertyInfos);
			#endregion
		}

	}

	/// <summary>
	/// 從MemberInfo中注入UI; Injects from member info.
	/// </summary>
	/// <param name="panelType">Panel type.</param>
	/// <param name="panel">Panel.</param>
	/// <param name="injectInfo">Inject info.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private void InjectFromMemberInfo<T>(Type panelType, MonoBehaviour panel, InjectInfo<T> injectInfo)
		where T:MemberInfo
	{
		for (int i = 0; i < injectInfo.MemberInfos.Length; i++) 
		{
			// 取得當前Field所有的Attribute
			object[] attributes = injectInfo.GetCustomAttributes (i,typeof(ViewInject));

			if (attributes != null && attributes.Length > 0) 
			{
				// 取得當前Field特性指定的路徑
				string uiPath = ((ViewInject)(attributes [0])).UIPath;

				Type componentType = injectInfo.GetFieldOrPropertyType(i);
				object targetUI = null;

				// 使用相對路徑
				if (uiPath.StartsWith (".") || uiPath.StartsWith ("~")) 
				{
					string path = uiPath.Remove (0, 2);
					targetUI = panel.transform.FindAndGet (componentType, path);
				}
				// 使用絕對路徑
				else 
				{
					#if YSUtils
					targetUI = GameObjectUtil.FindAndGet(componentType,uiPath);
					#elif
					targetUI = GameObject.Find (uiPath).GetComponent (componentType);
					#endif
				}

				if (targetUI != null) 
				{
					injectInfo.SetValue (i, panel, targetUI);
				} else 
				{
					LogError ("Fail to inject UI, current UI field is :" + injectInfo[i].Name);
				}
			}
		}
	}

	#region 非泛型方法,已棄用; not genericity method,deprecated

	private void InjectFromField(Type panelType, MonoBehaviour panel, FieldInfo[] fieldInfos)
	{
		foreach (var field in fieldInfos) 
		{
			// 取得當前Field所有的Attribute
			object[] attributes = field.GetCustomAttributes (typeof(ViewInject), true);

			if (attributes != null && attributes.Length > 0) 
			{
				string uiPath = ((ViewInject)(attributes [0])).UIPath;	// 取得當前Field特性指定的路徑
				Type componentType = field.FieldType;
				object targetUI = null;

				// 使用相對路徑
				if (uiPath.StartsWith (".") || uiPath.StartsWith ("~")) 
				{
					string path = uiPath.Remove (0, 2);
					targetUI = panel.transform.FindAndGet (componentType, path);
				}
				// 使用絕對路徑
				else 
				{
					targetUI = GameObject.Find (uiPath).GetComponent (componentType);
				}

				if (targetUI != null) 
				{
					field.SetValue (panel, targetUI);	
				} else 
				{
					LogError ("Fail to inject UI, current UI field is :" + field.Name);
				}
			}
		}
	}


	private void InjectFromProperty(Type panelType, MonoBehaviour panel, PropertyInfo[] propertyInfos)
	{
		
	}

	#endregion

	#endregion
}

#region 注入特性; Inject Attribute

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property)]
/// <summary>
/// 注入指定UI ; View inject.
/// </summary>
public class ViewInject : Attribute
{
	public ViewInject(string uiPath)
	{
		this.UIPath = uiPath;
	}

	/// <summary>
	/// UI path.
	/// </summary>
	/// <value>The user interface path.</value>
	public string UIPath{ get; private set;}
}

#endregion
