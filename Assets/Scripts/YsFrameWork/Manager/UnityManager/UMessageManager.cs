//
// /**************************************************************************
//
// IUnityManager.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-24
//
// Description:掛在Unity GameObject上的管理器，負責在Update處理網路相關功能.
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

using UnityEngine;

/// <summary>
/// 掛在Unity GameObject上的管理器，負責在Update處理網路相關功能.
/// </summary>
public class UMessageManager : AbsUnityManager<UMessageManager>
{
    public override void Init()
    {
        Debug.Log("UMessageManager init completed!");
    }


    void Update()
    {
        MessageCenter.Instance.ListenServer();
    }
}
