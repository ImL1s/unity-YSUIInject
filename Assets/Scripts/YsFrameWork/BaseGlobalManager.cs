using UnityEngine;
using System.Collections;
using YSFramework;
using YSFramework.UnityManager;

public class BaseGlobalManager : AbsUnityManager <BaseGlobalManager>
{
	protected override void OnAwake ()
	{
		InitManager ();
		InitModule ();
	}

	protected override void OnStart ()
	{
		
	}


	/// <summary>
	/// 初始化管理器
	/// </summary>
	protected virtual void InitManager()
	{
		ResourceManager.PreInit ();
        ModuleManager.PreInit();
		MessageCenter.PreInit ();
		UIManager.PreInit ();
		SceneManagerEX.PreInit ();
	}

	/// <summary>
	/// 註冊模組
	/// </summary>
	protected virtual void InitModule()
	{
//		ModuleManager.Instance.Register<> ();
        ModuleManager.Instance.Register<UserModule>();
	}
	
}
