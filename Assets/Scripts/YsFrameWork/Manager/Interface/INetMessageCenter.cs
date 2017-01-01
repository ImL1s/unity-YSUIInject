// /**************************************************************************
//
// MessageCenter.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-28
//
// Description:網路消息處理中心.
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

public interface INetMessageCenter
{
    /// <summary>
    /// 消息接收.
    /// </summary>
    /// <param name="model"></param>
    void MessageReceive(Protocols.SocketModel model);
}

