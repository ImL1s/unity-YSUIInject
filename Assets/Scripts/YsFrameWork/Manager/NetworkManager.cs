using UnityEngine;
using System.Collections.Generic;
using Protocols;
using ClientNetFrame;
using System;

namespace YSFramework
{

	public class NetworkManager :BaseManager<NetworkManager>
    {
        private Dictionary<Protocol.Type, List<BaseNetMessageCenter>> handlerDic = new Dictionary<Protocol.Type, List<BaseNetMessageCenter>>();

		public override void Init()
        {
			YSFramework.Utils.Logger.Info("NetworkManager Init Succ!");
        }

        public void Register(Protocol.Type type, BaseNetMessageCenter handler)
        {
            if (!handlerDic.ContainsKey(type))
            {
                var list = new List<BaseNetMessageCenter>();
                list.Add(handler);
                handlerDic.Add(type, list);
            }
            else
            {
                handlerDic[type].Add(handler);
            }
        }

        public void Listen()
        {
            while (NetCore.Instance.Messages.Count > 0)
            {
                SocketModel model = NetCore.Instance.Messages[0];
                MessageReceive(model);
                NetCore.Instance.Messages.RemoveAt(0);
            }
        }

        private void MessageReceive(SocketModel model)
        {
            Protocol.Type type = (Protocol.Type)model.Type;

            if (handlerDic.ContainsKey(type))
            {
                foreach (var handler in handlerDic[type])
                {
                    handler.MessageReceive(model);
                }
            }
        }
    }
}
