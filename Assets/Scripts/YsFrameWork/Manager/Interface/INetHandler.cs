//
// /**************************************************************************
//
// MessageCenter.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-28
//
// Description:網路消息處理
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

public interface INetHandler
{
    void MessageReceive(Protocols.SocketModel model);
}

