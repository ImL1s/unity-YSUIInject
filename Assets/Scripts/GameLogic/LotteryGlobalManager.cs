using UnityEngine;
using System.Collections;
using YSFramework;

public class LotteryGlobalManager :BaseGlobalManager
{
    protected override void OnStart()
    {
        UIManager.OpenUpUI (Define.UIType.LoginPanel);
    }
}
