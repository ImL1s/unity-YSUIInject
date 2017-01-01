//
// /**************************************************************************
//
// IDynamicProperty.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-22
//
// Description:
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

using System;
using System.Collections.Generic;



public interface IDynamicProperty
{
    /// <summary>
    /// 屬性被改變.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    void DoChangeProperty(int id, object oldValue, object newValue);

    /// <summary>
    /// 取得屬性.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BaseProperty GetProperty(int id);
}

