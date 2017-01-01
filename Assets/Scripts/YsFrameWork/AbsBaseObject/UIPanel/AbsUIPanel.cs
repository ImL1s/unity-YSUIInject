/*
 * Author:ImL1s
 *
 * Date:2016/02/11
 *
 * Email:ImL1s@outlook.com
 *
 * description:UI面板抽象類.
 */

using UnityEngine;
using System.Collections;
using System;

public abstract class AbsUIPanel : MonoBehaviour
{
    #region event 事件

    /// <summary>
    /// UI狀態改變事件.
    /// </summary>
    public event Define.UIStateChangeHandler UIStateChangeCallback;

    #endregion

    #region field

    protected Define.UIState state = Define.UIState.None;

    #endregion

    #region get/set property

    public Define.UIState State
    {
        get { return state; }

        set
        {
            Define.UIState oldState = state;
            state = value;
            if (oldState != state) if(UIStateChangeCallback != null)
                    UIStateChangeCallback(this, oldState, state);
        }
    }

    public virtual Define.UIType Type
    {
        get
        {
            Debug.LogError("Please override UIType property! ");
            return default(Define.UIType);
        }
    }

    /// <summary>
    /// 是否可以進行Update()更新
    /// </summary>
    public bool CanUpdate { get; set; }

    #endregion

    #region unity method

    private void Awake()
    {
        this.State = Define.UIState.Initial;
        OnAwake();
    }

    private void Start()
    {
        this.State = Define.UIState.Loading;
        CanUpdate = true;
        OnStart();
    }

    private void Update()
    {
        if (this.State == Define.UIState.Readly && CanUpdate) OnUpdate();
    }

    void OnEnable()
    {
        OnEnabled();
    }

    void OnDisable()
    {
        OnDisabled();
    }

    #endregion

    #region public method

    /// <summary>
    /// 關閉(釋放)UI.
    /// </summary>
    public void Release()
    {
        this.State = Define.UIState.Closing;
        OnRelease();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 傳入UI參數
    /// </summary>
    /// <param name="para"></param>
    public void SetUI(object para = null)
    {
        OnSetUI(para);
        StartCoroutine(LoadDataCoroutine());
    }

    #endregion

    #region private method

    /// <summary>
    /// 等待一幀，讀取資料
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadDataCoroutine()
    {
        yield return null;
        if(this.State == Define.UIState.Loading)
        {
            this.OnLoadData();
            this.State = Define.UIState.Readly;
        }
    }

    #endregion

    #region virtual method

    /// <summary>
    /// UI最早初始化.
    /// </summary>
    protected virtual void OnAwake() { }

    /// <summary>
    /// UI初始化.
    /// </summary>
    protected virtual void OnStart() { }

    /// <summary>
    /// UI Update
    /// </summary>
    protected virtual void OnUpdate() { }

    /// <summary>
    /// UI釋放
    /// </summary>
    protected virtual void OnRelease() { }

    /// <summary>
    /// OnEnable UI
    /// </summary>
    protected virtual void OnEnabled() { }

    /// <summary>
    /// OnDisable UI
    /// </summary>
    protected virtual void OnDisabled() { }

    /// <summary>
    /// 播放音效
    /// </summary>
    protected virtual void OnPlayOpenUISound()
    {
        AudioClip audioclip = Resources.Load<AudioClip>(Define.ResourceDefaultAudioPath);
        if (audioclip == null) return;
        AudioSource.PlayClipAtPoint(audioclip, Vector3.zero);
    }

    /// <summary>
    /// 設定UI.
    /// </summary>
    /// <param name="para"></param>
    protected virtual void OnSetUI(object para) { }
    
    /// <summary>
    /// 讀取UI數值.
    /// </summary>
    protected virtual void OnLoadData() { }


    #endregion

}
