public abstract class AbsModule
{

    #region private field

    private Define.RegisterMode registerMode = Define.RegisterMode.NotRegister; // 當前Module註冊狀態.
    private Define.ModuleState state = Define.ModuleState.Initial;              // 當前Module狀態.
    private Define.MoudleStateChangeHandler StateChanged;                       // 狀態改變通知事件.

    #endregion

    #region get/set method

    /// <summary>
    /// 模塊當前狀態.
    /// </summary>
    public Define.ModuleState State
    {
        get
        {
            return state;
        }
        set
        {
            if (value == state) return;
            Define.ModuleState oldState = state;
            state = value;
            if (StateChanged != null) StateChanged(this, oldState, state);
        }
    }

    /// <summary>
    /// 是否自動註冊.
    /// </summary>
    public bool AutoRegister
    {
        get { return registerMode == Define.RegisterMode.AutoRegister; }
        set
        {
            if (registerMode == Define.RegisterMode.NotRegister || registerMode == Define.RegisterMode.AutoRegister)
                registerMode = value ? Define.RegisterMode.AutoRegister : Define.RegisterMode.NotRegister;
        }
    }

    /// <summary>
    /// 是否已經註冊過了.
    /// </summary>
    public bool AlereadyRegister { get { return registerMode == Define.RegisterMode.AlereadyRegister; } }

    #endregion

    #region public method

    /// <summary>
    /// 開始讀取.
    /// </summary>
    /// <param name="autoRegister">是否自動註冊.</param>
    public void Load()
    {
        if (State != Define.ModuleState.Initial) return;

        if (AutoRegister)
        {
            // 註冊
            ModuleManager.Instance.Register(this);
            registerMode = Define.RegisterMode.AlereadyRegister;
        }
        Onload();

        State = Define.ModuleState.Readly;
    }


    /// <summary>
    /// 釋放.
    /// </summary>
    public void Release()
    {
        if (State == Define.ModuleState.Closing) return;
        if (State != Define.ModuleState.Closing) State = Define.ModuleState.Closing;

        if (AlereadyRegister)
        {
            ModuleManager.Instance.UnRegister(this);
            registerMode = Define.RegisterMode.AutoRegister;
        }
    }

    #endregion

    #region virtual method

    public virtual void Onload() { }

    /// <summary>
    ///  當Moudle狀態改變時被調用
    /// </summary>
    /// <param name="state"></param>
    /// <param name="oldState"></param>
    public virtual void OnStateChanged(Define.ModuleState state, Define.ModuleState oldState) { }

    #endregion

}
