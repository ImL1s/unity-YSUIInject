//
// /**************************************************************************
//
// MessageCenter.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-22
//
// Description:singleton 消息處理中心
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

using ClientNetFrame;
using Protocols;
using Protocols.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using YSFramework;

class MessageCenter : BaseManager<MessageCenter>
{
    #region NetCore

    public string IP = "123.193.91.27";
    public int Port = 9527;

    private INetMessageCenter netMessageCenter;
    private Stack<SocketModel> messages = null;

    public override void Init()
    {
        Debug.Log("MessageCenter init...");

        dicMessageEvents = new Dictionary<string, List<Define.MessageEvent>>();
        messages = new Stack<SocketModel>();

        Debug.Log("MessageCenter init completed");

    }

    /// <summary>
    /// 設定伺服器.
    /// </summary>
    /// <param name="IP"></param>
    /// <param name="port"></param>
    public void SetServer(string IP = "123.193.91.27", int port = 9527)
    {
        this.IP = IP;
        this.Port = port;

        NetCore.Instance.Init();
        NetCore.Instance.Connect(IP, 9527);
        Debug.Log("連線至:" + IP);
    }


    /// <summary>
    /// 發送消息到伺服器.
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

    /// <summary>
    /// 監聽伺服器的消息.
    /// </summary>
    public void ListenServer()
    {
        if (NetCore.Instance.Messages.Count > 0)
        {
            SocketModel model = NetCore.Instance.Messages[0];
            messages.Push(model);
            NetCore.Instance.Messages.Remove(model);

            netMessageCenter.MessageReceive(model);
            //Process(model);
            //Debug.Log((LoginResult)(model.Message));

        }
    }

    #region 廢棄處理登入方式，改為其他方式
    //private void Process(SocketModel model)
    //{
    //    switch (model.Type)
    //    {
    //        case (byte)Protocol.Type.Login:
    //            ProcessLogin(model);
    //            break;

    //        default:
    //            break;
    //    }
    //}

    //private void ProcessLogin(SocketModel model)
    //{
    //    SendMessage(model.Command.ToString(), this, model.Message, typeof(Protocol.Command));
    //    switch (model.Command)
    //    {
    //        case (int)Protocol.Command.LoginResponse:
    //            SendMessage("LoginResponse", this, model.Message, typeof(Protocol.Command));
    //            break;

    //        case (int)Protocol.Command.RegisterResponse:
    //            SendMessage("RegisterResponse", this, model.Message, typeof(Protocol.Command));
    //            break;

    //        default:
    //            break;
    //    }
    //}
    #endregion

    #endregion


	#region static method

    /// <summary>
    /// 註冊消息
    /// </summary>
	public static void AddListenerByName(string messageName,Define.MessageEvent messageEvent)
	{
		Instance.AddListener (messageName, messageEvent);
	}

    /// <summary>
    /// 發送消息
    /// </summary>
    public static void SendMessageByName(string name, object sender, object content = null, Type ContentType = null, Dictionary<string, object> dicPara = null)
    {
        Instance.SendMessage(name, sender, content, ContentType, dicPara);
    }

    /// <summary>
    /// 發送消息
    /// </summary>
    /// <param name="msg">Message.</param>
    public static void SendMessageByMsg(Message msg)
    {
        Instance.SendMessage(msg);
    }

    /// <summary>
    /// 移除監聽
    /// </summary>
    /// <param name="msgName">Message name.</param>
    /// <param name="msgEvent">Message event.</param>
    public static void RemoveListenerByName(string msgName,Define.MessageEvent msgEvent)
    {
        Instance.RemoveListener(msgName, msgEvent);
    }

	#endregion


    #region Message method

    private Dictionary<string, List<Define.MessageEvent>> dicMessageEvents = new Dictionary<string, List<Define.MessageEvent>>();


    /// <summary>
    /// 加入Listener.
    /// </summary>
    /// <param name="messageName"></param>
    /// <param name="messageEvent"></param>
    public void AddListener(string messageName, Define.MessageEvent messageEvent)
    {
        Debug.Log("Addlistener Name:" + messageName);

        List<Define.MessageEvent> list = null;
        if (dicMessageEvents.ContainsKey(messageName))
        {
            list = dicMessageEvents[messageName];
        }
        else
        {
            list = new List<Define.MessageEvent>();
            dicMessageEvents.Add(messageName, list);
        }

        if (!list.Contains(messageEvent))
        {
            list.Add(messageEvent);
        }
    }

    /// <summary>
    /// 移除Listener.
    /// </summary>
    /// <param name="messageName"></param>
    /// <param name="messageEvent"></param>
    public void RemoveListener(string messageName, Define.MessageEvent messageEvent)
    {
		YSFramework.Utils.Logger.Debug("RemoveListener Name:" + messageName);

        if (dicMessageEvents.ContainsKey(messageName))
        {
            List<Define.MessageEvent> list = dicMessageEvents[messageName];

            if (list.Contains(messageEvent))
            {
                list.Remove(messageEvent);
            }
            else if (list.Count <= 0)
            {
                dicMessageEvents.Remove(messageName);
            }
        }

    }

    /// <summary>
    /// 移除全部Listener.
    /// </summary>
    public void RemoveAllListener()
    {
        dicMessageEvents.Clear();
    }


    /// <summary>
    /// 發送消息.
    /// </summary>
    /// <param name="name">消息名稱(Define.MessageName)</param>
    /// <param name="sender">發送者</param>
    /// <param name="content">內容</param>
    /// <param name="ContentType">內容類型</param>
    /// <param name="dicPara">其他資料</param>
    public void SendMessage(string name, object sender, object content = null, Type ContentType = null, Dictionary<string, object> dicPara = null)
    {
        SendMessage(new Message(name, sender, content, ContentType, dicPara));
    }

    /// <summary>
    /// 發送消息.
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(Message message)
    {
        if (dicMessageEvents == null || !dicMessageEvents.ContainsKey(message.Name)) return;

        List<Define.MessageEvent> list = dicMessageEvents[message.Name];

        for (int i = 0; i < list.Count; i++)
        {
            Define.MessageEvent callback = list[i];
            if (callback != null) callback(message);
        }
    }

    internal void AddListener(object onExitPlayScene, Action<Message> onDecideCommonPlay)
    {
        throw new NotImplementedException();
    }

    #endregion

}

