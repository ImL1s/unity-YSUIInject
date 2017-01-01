using System;
using YSFramework;


public class UserModule : AbsModule
{
    public UserData Data{ get; set;}

    public override void Onload()
    {
        MessageCenter.AddListenerByName(Define.MessageName.Logout, this.OnLogout);
        MessageCenter.AddListenerByName(Define.MessageName.OpenUserUI, this.OnReceiveOpenUserUI);
        MessageCenter.AddListenerByName(Define.MessageName.Net_GetUserDataCallback, this.OnGetUserData);
    }

    /// <summary>
    /// 登出操作
    /// </summary>
    /// <param name="message">Message.</param>
    void OnLogout(Message message)
    {
        var para = new Define.LoadingPanelParameter()
        {
                EventName = Define.MessageName.Net_LogoutCallback,
                OpenUIType = Define.UIType.LoginPanel,
                OnFinishAction = msg =>
                    {
                        return true;
                    }
        };

        UIManager.OpenUpUI(Define.UIType.LoadingPanel, false, para);

        UnityCoroutine.Instance.AsyncAction(TestMsg, 2f);
    }

    private void OnReceiveOpenUserUI(Message message)
    {
        UIManager.OpenUpUI(Define.UIType.LoadingPanel, closeOther: false);
        MessageCenter.SendMessageByName(Define.MessageName.Net_GetUserData,this);

        UnityCoroutine.Instance.AsyncAction(TestSendGetUserDataMsg, 2f);
    }

    /// <summary>
    /// 更新並取得網路中User的資料
    /// </summary>
    /// <param name="message">Message.</param>
    private void OnGetUserData(Message message)
    {
        // TODO 處理資料
//        var data = message.GetContent<UserData>();
//        this.Data = data;

        MessageCenter.SendMessageByName(Define.MessageName.Data_LoadingCompleted,this);

        UIManager.OpenUpUI(Define.UIType.UserPanel);
    }

    #region Test

    private void TestMsg()
    {
        MessageCenter.SendMessageByName(Define.MessageName.Net_LogoutCallback, this);
    }

    private void TestSendGetUserDataMsg()
    {
        MessageCenter.SendMessageByName(Define.MessageName.Net_GetUserDataCallback,this);
    }

    #endregion

}

public class UserData
{
    public string Account{get;set;}

    public string Password{get;set;}
}


