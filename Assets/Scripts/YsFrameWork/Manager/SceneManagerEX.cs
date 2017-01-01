/* 
 * Author:ImL1s
 *
 * Email:ImL1s @outlook.com
 *
 * Date:2016/02/15
 *
 * Description:場景管理器.
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSFramework;

public class SceneManagerEX : BaseManager<SceneManagerEX>
{
    #region SceneInfoData class

    public class SceneInfoData
    {
        private object para;
        private string sceneName;
        private Type sceneType;

        public object Para
        {
            get
            {
                return para;
            }

            set
            {
                para = value;
            }
        }

        public string SceneName
        {
            get
            {
                return sceneName;
            }

            set
            {
                sceneName = value;
            }
        }

        public Type SceneType
        {
            get
            {
                return sceneType;
            }

            set
            {
                sceneType = value;
            }
        }


        public SceneInfoData() { }

        public SceneInfoData(string sceneName, Type sceneType, object para)
        {
            this.SceneName = sceneName;
            this.SceneType = sceneType;
            this.Para = para;
        }

    }

    #endregion

    #region field

    private Dictionary<Define.SceneType, SceneInfoData> dicSceneInfos = null;

    private AbsScene currentScene = new AbsScene();

    private Define.UIType sceneOpenUIType = Define.UIType.None;

    private object sceneOpenUIParams = null;                    // 打開場景UI的初始化資訊.

    private object sceneOpenParams = null;                      // 打開場景的初始化資訊.

    #endregion

    #region method


    #region get/set method

    /// <summary>
    /// 當前場景.
    /// </summary>
    public AbsScene CurrentScene
    {
        get { return currentScene; }
        set { currentScene = value; }
    }

    /// <summary>
    /// 最後打開的場景(非當前場景).
    /// </summary>
    public Define.SceneType LastSceneType { get; set; }

    /// <summary>
    /// 當前的場景.
    /// </summary>
    public Define.SceneType CurrentSceneType { get; set; }

    /// <summary>
    /// 打開場景的初始化資訊.
    /// </summary>
    public object SceneOpenParams
    {
        get
        {
            return sceneOpenParams;
        }

        set
        {
            sceneOpenParams = value;
        }
    }

    #endregion


    #region override/implement

	public override void Init()
    {
        Debug.Log("SceneManagerEX Init...");

        if(dicSceneInfos == null) dicSceneInfos = new Dictionary<Define.SceneType, SceneInfoData>();

        Debug.Log("SceneManagerEX init completed");
    }

    //public override void OnSingletonInit()
    //{
    //    base.OnSingletonInit();
    //    dicSceneInfos = new Dictionary<Defind.SceneType, SceneInfoData>();
    //}

    #endregion


    #region register/unregister 註冊/註銷場景

    /// <summary>
    /// 註冊所有的場景.
    /// </summary>
    public void RegisterAllScene()
    {
        //RegisterScene(Defind.SceneType.StartGame, "01_StartGame", typeof(StartGameScene), null);
        //RegisterScene(Defind.SceneType.Select, "02_Select", typeof(SelectScene), null);
    }

    /// <summary>
    /// 註冊場景.
    /// </summary>
    /// <param name="sceneEnum"></param>
    /// <param name="sceneName"></param>
    /// <param name="sceneType"></param>
    /// <param name="para"></param>
    public void RegisterScene(Define.SceneType sceneEnum, string sceneName, Type sceneType, object para = null)
    {
        if (sceneType == null || sceneType.BaseType != typeof(AbsScene))
        {
            throw new Exception("Register scene type must not null and extends BaseScene ,Scene type:" + sceneType + "," + sceneName + "," + sceneEnum);
        }
        if (!dicSceneInfos.ContainsKey(sceneEnum))
        {
            SceneInfoData sceneInfo = new SceneInfoData(sceneName, sceneType, para);
            dicSceneInfos.Add(sceneEnum, sceneInfo);
        }
    }


    /// <summary>
    /// 註冊場景.
    /// </summary>
    /// <param name="sceneEnum"></param>
    /// <param name="sceneName"></param>
    /// <param name="sceneType"></param>
    /// <param name="para"></param>
    public void RegisterScene(Define.SceneType sceneEnum ,object para = null)
    {
        RegisterScene(sceneEnum, Define.Scene.GetSceneName(sceneEnum), Define.Scene.GetSceneType(sceneEnum));
    }

    /// <summary>
    /// 註冊場景.
    /// </summary>
    /// <param name="sceneEnum"></param>
    /// <param name="sceneName"></param>
    /// <param name="sceneType"></param>
    /// <param name="para"></param>
    public void RegisterScene(AbsScene scene)
    {
        
    }

    /// <summary>
    /// 註銷場景.
    /// </summary>
    /// <param name="sceneEnum"></param>
    public void UnRegisterScene(Define.SceneType sceneEnum)
    {
        if (dicSceneInfos.ContainsKey(sceneEnum)) dicSceneInfos.Remove(sceneEnum);
    }

    /// <summary>
    /// 是否註冊過該場景.
    /// </summary>
    /// <param name="sceneType"></param>
    /// <returns></returns>
    public bool IsRegisterScene(Define.SceneType sceneType)
    {
        return dicSceneInfos.ContainsKey(sceneType);
    }

    public void ClearScene()
    {
        dicSceneInfos.Clear();
    }

    #endregion


    #region Change scene/切換場景


    /// <summary>
    /// 直接切換場景.
    /// </summary>
    /// <param name="sceneType">打開的場景類型.</param>
    /// <param name="openUIType">要打開的UI類型.</param>
    /// <param name="paras">要打開UI的參數.</param>
    /// <param name="sceneParas">打開場景後的初始化資訊</param>
    public void ChangeSceneDirect(Define.SceneType sceneType, Define.UIType openUIType = Define.UIType.None,
        object paras = null,object sceneParas = null,bool isPlayBGM = true)
    {
        if(sceneType == Define.SceneType.None)
        {
            Debug.LogError("Try open a none scene!!");
            return;
        }

        sceneOpenUIType = openUIType;
        sceneOpenUIParams = paras;
        sceneOpenParams = sceneParas;

        UIManager.Instance.CloseUIAll();

        if (CurrentScene != null)
        {
            CurrentScene.Release();
            CurrentScene = null;
        }

        LastSceneType = CurrentSceneType;
        CurrentSceneType = sceneType;
        string sceneName = GetSceneName(sceneType);

        UnityCoroutine.Instance.StartCoroutine(AsyncLoadNextScene(sceneName, isPlayBGM));

    }

    /// <summary>
    /// 協程讀取場景.
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="isPlayBGM">是否播放BGM</param>
    /// <returns></returns>
    private IEnumerator AsyncLoadNextScene(string sceneName,bool isPlayBGM = true)
    {
        Debug.Log(sceneName);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        yield return op;

        if (sceneOpenUIType != Define.UIType.None)
        {
            UIManager.Instance.OpenUI(sceneOpenUIType, UIParas: sceneOpenUIParams);
            sceneOpenUIType = Define.UIType.None;
        }

        if(isPlayBGM) AudioManager.Instance.PlayAudioByScene(CurrentSceneType);

        Type currentType = Define.Scene.GetSceneType(CurrentSceneType);
        CurrentScene = Activator.CreateInstance(currentType) as AbsScene;
        if (CurrentScene != null) CurrentScene.Init(SceneOpenParams);
    }

    /// <summary>
    /// 取得場景名稱.
    /// </summary>
    /// <param name="sceneType"></param>
    /// <returns></returns>
    private string GetSceneName(Define.SceneType sceneType)
    {
        if (dicSceneInfos.ContainsKey(sceneType))
        {
            return dicSceneInfos[sceneType].SceneName;
        }

        Debug.Log("This scene is not register! ID:" + sceneType.ToString());
        return null;
    }


    #endregion


    #endregion

}
