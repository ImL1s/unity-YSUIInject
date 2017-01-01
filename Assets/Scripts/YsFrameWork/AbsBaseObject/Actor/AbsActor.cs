/*
 * Author:ImL1s
 *
 * Date:2016/02/22
 *
 * Email:ImL1s@outlook.com
 *
 * description:場景上的物體基類.
 */

using System;
using UnityEngine;
using System.Collections.Generic;


public class AbsActor :MonoBehaviour,IDynamicProperty
{
    public event Define.PropertyChangedHandler PropertyChanged;

    #region field

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<int, BaseProperty> dicProperty = null;

    /// <summary>
    /// 當前人物所屬場景.
    /// </summary>
    private AbsScene currenrScene;

    #endregion

    #region get/set method

    /// <summary>
    /// Actor類型.
    /// </summary>
    public Define.ActorType ActorType { get; set; }

    /// <summary>
    /// Actor ID.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Actor 名稱.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// BaseProperty(屬性)字典.
    /// </summary>
    protected Dictionary<int, BaseProperty> DicProperty
    {
        get
        {
            if (dicProperty == null)
            {
                dicProperty = new Dictionary<int, BaseProperty>();
            }
            return dicProperty;
        }

        set { dicProperty = value; }
    }

    /// <summary>
    /// 當前Actor所屬的場景.
    /// </summary>
    public AbsScene CurrenrScene
    {
        get
        {
            return currenrScene;
        }

        set
        {
            currenrScene = value;
        }
    }

    #endregion

    //void OnEnable()
    //{
    //    SetName();
    //}

    void Awake()
    {
        SetName();
        OnAwake();
    }
    
    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }
    
    protected virtual void OnAwake() { }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    /// <summary>
    /// 設定名稱.
    /// </summary>
    protected virtual void SetName()
    {
        this.Name = this.gameObject.name;
    }

    public virtual void AddProperty(Define.PropertyType propertyType, object content)
    {
        AddProperty((int)propertyType, content);
    }

    /// <summary>
    /// 加入屬性.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    public virtual void AddProperty(int id,object content)
    {
        BaseProperty property = new BaseProperty(id, content);
        AddProperty(property);
    }

    /// <summary>
    /// 加入屬性.
    /// </summary>
    public virtual void AddProperty(BaseProperty property)
    {
        if (DicProperty.ContainsKey(property.ID))
        {
            foreach (var item in DicProperty)
            {
                if (item.Value == property)
                {
                    DicProperty.Remove(item.Key);
                    break;
                }
            }
        }
        DicProperty.Add(property.ID, property);
    }

    /// <summary>
    /// 移除屬性.
    /// </summary>
    public void RemoveProperty(Define.PropertyType propertyType)
    {
        RemoveProperty((int)propertyType);
    }

    /// <summary>
    /// 移除屬性.
    /// </summary>
    public void RemoveProperty(int id)
    {
        if (DicProperty.ContainsKey(id)) DicProperty.Remove(id);
    }


    /// <summary>
    /// 當Actor的Property被改變時調用.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    protected virtual void OnPropertyChanged(int id,object oldValue,object newValue)
    {
        
    }

    #region Implement

    public void DoChangeProperty(int id, object oldValue, object newValue)
    {
        OnPropertyChanged(id, oldValue, newValue);
        if (PropertyChanged != null) PropertyChanged(this, id, oldValue, newValue);
    }

    public BaseProperty GetProperty(int id)
    {
        if (dicProperty == null) return null;
        if (dicProperty.ContainsKey(id)) return dicProperty[id];

        UnityEngine.Debug.LogWarning("Actor dicProperty non Property ID: " + id);
        return null;
    }

    #endregion
}

