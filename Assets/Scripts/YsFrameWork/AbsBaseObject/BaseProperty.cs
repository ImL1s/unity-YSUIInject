//
// /**************************************************************************
//
// BaseProperty.cs
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


public class BaseProperty
{
    public int ID { get; set; }

    private object content;                                         // 偽裝屬性內容(加入亂數).
    private object rawContent;                                      // 真實屬性內容.
    private bool canRandom = false;                                 // 是否是數字.
    private int curRandomInt;                                       // 當前的亂數(Int).
    private float curRandomFloat;                                   // 當前的亂數(float).
    private Type propertyType;                                      // 當前內容的類型.

    public IDynamicProperty Owner = null;

    /// <summary>
    /// 屬性值(內容).
    /// </summary>
    public object Content
    {
        get { return GetContent(); }
        set
        {
            if (value != GetContent())
            {
                object oldContent = GetContent();
                SetContent(value);
                if (Owner != null) Owner.DoChangeProperty(ID, oldContent, value);
            }
        }
    }

    public BaseProperty() { }

    public BaseProperty(int ID,object content)
    {
        propertyType = content.GetType();
        if(propertyType == typeof(System.Int32) || propertyType == typeof(System.Single))
        {
            canRandom = true;
        }

        this.ID = ID;
        SetContent(content);
    }

    /// <summary>
    /// 設定屬性值.
    /// </summary>
    /// <param name="value"></param>
    public void SetContent(object value)
    {
        rawContent = value;
        if (canRandom)
        {
            if (propertyType == typeof(System.Int32))
            {
                curRandomInt = UnityEngine.Random.Range(1, 1000);
                this.content = (int)value + curRandomInt;
            }
            else if (propertyType == typeof(System.Single))
            {
                curRandomFloat = UnityEngine.Random.Range(1.0f, 1000.0f);
                this.content = (float)value + curRandomFloat;
            }
        }
    }

    /// <summary>
    /// 取得屬性值(經過防作弊計算).
    /// </summary>
    /// <returns></returns>
    public object GetContent()
    {
        if (canRandom)
        {
            if (propertyType == typeof(System.Int32))
            {
                int temp = (int)this.content - curRandomInt;
                if (temp != (int)rawContent)
                {
                    Message message = new Message("PropertyItemDataException", this, ID);
                    message.Send();
                }

                return temp;
            }

            else if (propertyType == typeof(System.Single))
            {
                float temp = (float)this.content - curRandomFloat;
                if (temp != (float)rawContent)
                {
                    Message message = new Message("PropertyItemDataException", this, ID);
                    message.Send();
                }

                return temp;
            }
        }

        return this.content;
    }
}
