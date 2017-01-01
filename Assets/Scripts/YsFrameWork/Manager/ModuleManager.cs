/* 
 * Author:ImL1s
 *
 * Email:ImL1s @outlook.com
 *
 * Date:2016/02/15
 *
 * Description:ModuleManager,模塊(資料+Controller)管理器.
 *
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using YSFramework;

class ModuleManager : BaseManager<ModuleManager>
{
    /// <summary>
    /// 受管理的Module.
    /// </summary>
    private Dictionary<string, AbsModule> dicModules = null;

	#region static method

	/// <summary>
	/// 使用物件(對象)註冊
	/// </summary>
	/// <param name="module">Module.</param>
	public void RegisterByInstance(AbsModule module)
	{
		Instance.Register(module);
	}

	/// <summary>
	/// 使用泛型註冊
	/// </summary>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void RegisterByGenericity<T>() where T :AbsModule,new()
	{
		Instance.Register<T>();
	}

	#endregion


	public override void Init()
    {
        Debug.Log("ModuleManager Init...");
        
        dicModules = new Dictionary<string, AbsModule>();

        Debug.Log("ModuleManager init completed");
    }


    /// <summary>
    /// 取得Module.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public AbsModule Get(Type UIType)
    {
        return Get(UIType.ToString());
    }

    /// <summary>
    /// 取得Module.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public AbsModule Get(string key)
    {
        if (dicModules.ContainsKey(key)) return dicModules[key];

        return null;
    }

    /// <summary>
    /// 使用泛型取得Module.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Get<T>() where T :AbsModule
    {
        AbsModule module = Get(typeof(T).ToString());
        if (module != null) return (T)module;
        return null;
    }

    /// <summary>
    /// 註冊Module.
    /// </summary>
    /// <param name="Module"></param>
	public void Register(AbsModule module)
    {
		Register(module.GetType().ToString(), module);
    }

    /// <summary>
    /// 註冊Module.
    /// </summary>
    /// <param name="Module"></param>
    public void Register(string key,AbsModule module)
    {
		InnerRegister (key, module);
    }

    /// <summary>
    /// 註冊Module.
    /// </summary>
    /// <param name="Module"></param>
    public void Register<T>() where T :AbsModule,new()
    {
        Register(typeof(T).ToString(), new T());
    }


    /// <summary>
    /// 註銷Module.
    /// </summary>
    /// <param name="module"></param>
    public void UnRegister(AbsModule module)
    {
        if (dicModules.ContainsKey(module.GetType().ToString()))
        {
            AbsModule absModule = dicModules[module.GetType().ToString()];
            absModule.Release();
        }
    }


    /// <summary>
    /// 註銷Module.
    /// </summary>
    /// <param name="module"></param>
    public void UnRegister(string key)
    {
        if(dicModules.ContainsKey(key))
        {
            AbsModule module = dicModules[key];
            module.Release();
        }
    }

    /// <summary>
    /// 取消註冊全部.
    /// </summary>
    public void UnRegisterAll()
    {
        List<string> keys = new List<string>(dicModules.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            UnRegister(keys[i]);
        }

        dicModules.Clear();
    }


	private void InnerRegister(string key,AbsModule module)
	{
		if (!dicModules.ContainsKey(key))
		{
			dicModules.Add(key, module);
			module.Load();
		}
	}
}

