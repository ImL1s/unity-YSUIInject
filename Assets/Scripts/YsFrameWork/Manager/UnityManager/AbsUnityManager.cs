//
// /**************************************************************************
//
// IUnityManager.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-24
//
// Description:掛在Unity GameObject上的管理器抽象類.
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 掛在Unity GameObject上的管理器抽象類.
/// </summary>
public abstract class AbsUnityManager<T> : UnitySingleton<T>, IUnityManager 
    where T : MonoBehaviour
{
    public virtual void Init() { }
}

