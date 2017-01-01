/*
 * Author:ImL1s
 *
 * Date:2016/02/11
 *
 * Email:ImL1s@outlook.com
 *
 * description:消息.
 */

using System.Collections;
using System;
using System.Collections.Generic;

public class Message : IEnumerable<KeyValuePair<string,object>>
{
	private Dictionary<string,object> dicDatas = null;

    /// <summary>
    /// Message名稱.
    /// </summary>
	public string Name { get; set; }

    /// <summary>
    /// Message發送者.
    /// </summary>
	public object Sender { get; set; }

    /// <summary>
    /// Message內容.
    /// </summary>
	public object Content { get; set; }

    /// <summary>
    /// Message內容類型.
    /// </summary>
    public Type ContentType { get; set; }


	#region message[key] = value of data = message[key]

	public object this[string key]
	{
		get
		{
			if(dicDatas == null || !dicDatas.ContainsKey(key))
				return null;
			return dicDatas[key];
		}
		set
		{
			if(dicDatas == null)
				dicDatas = new Dictionary<string, object>();
			
			else if(dicDatas.ContainsKey(key))
				dicDatas[key] = value;
			
			else
				dicDatas.Add(key,value);
		}
	}

	#endregion

	#region IEnumerable implementation 實現IEnumberable介面

	public IEnumerator<KeyValuePair<string, object>> GetEnumerator ()
	{
		if(dicDatas == null) yield break;

		foreach (var kvp in dicDatas) 
		{
			yield return kvp;	
		}
	}


	IEnumerator IEnumerable.GetEnumerator ()
	{
		return dicDatas.GetEnumerator();
	}

    #endregion

    public Message() { }

    public Message(string name, object sender, object content = null, Type contentType = null, Dictionary<string, object> dicParas = null)
    {
        dicDatas = new Dictionary<string, object>();
        Name = name;
        Sender = sender;
        Content = content;
        ContentType = contentType;

        //		if(dicParas.GetType() == typeof(Dictionary<string,object>))
        if (dicParas != null && dicParas.Count > 0)
        {
            foreach (var dicPara in dicParas)
            {
                this[dicPara.Key] = dicPara.Value;
            }

        }
    }

	/// <summary>
	/// 向資料中加入key Value.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	public void Add(string key,object value)
	{
		this[key] = value;
	}

    /// <summary>
    /// 發送Message.
    /// </summary>
    public void Send()
    {
        MessageCenter.Instance.SendMessage(this);
    }

    public T GetContent<T>()
    {
        if (Content != null) return (T)this.Content;
        else return default(T);
    }

}

