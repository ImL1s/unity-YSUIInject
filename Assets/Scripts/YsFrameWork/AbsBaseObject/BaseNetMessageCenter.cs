//
// /**************************************************************************
//
// MessageCenter.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-03-03
//
// Description:singleton 網路消息處理中心(抽象類)
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

using Protocols;
using ClientNetFrame;

public class BaseNetMessageCenter : Singleton<BaseNetMessageCenter>,INetMessageCenter
{
    /// <summary>
    /// 接收到伺服器端資料.
    /// </summary>
    /// <param name="model"></param>
    public virtual void MessageReceive(SocketModel model) { }

    /// <summary>
    /// 發送資料到伺服器端.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="area"></param>
    /// <param name="command"></param>
    /// <param name="message"></param>
    public void SendToServer(byte type, int area, int command, object message)
    {
        SendToServer(new SocketModel()
        {
            Type = type,
            Area = area,
            Command = command,
            Message = message
        });
    }

    /// <summary>
    /// 發送消息到伺服器.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="area"></param>
    /// <param name="command"></param>
    /// <param name="message"></param>
    public void SendToServer(SocketModel socketModel)
    {
        NetCore.Instance.Send(socketModel);
    }
}

