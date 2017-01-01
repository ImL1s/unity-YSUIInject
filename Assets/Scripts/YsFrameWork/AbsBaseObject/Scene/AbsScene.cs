//
// /**************************************************************************
//
// AbsScene.cs
//
// Author: ImL1s  <ImL1s@hotmail.com>
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


public class AbsScene : AbsModule
{
    private List<AbsActor> actorList = null;

    /// <summary>
    /// 初始化場景.
    /// </summary>
    /// <param name="para"></param>
    public void Init(object para)
    {
        base.Load();
        OnInit(para);
    }

    protected virtual void OnInit(object para) { }

    protected List<AbsActor> ActorList
    {
        get
        {
            if (actorList == null) actorList = new List<AbsActor>();
            return actorList;
        }

        set
        {
            actorList = value;
        }
    }

    public AbsScene()
    {
        ActorList = new List<AbsActor>();
    }

    /// <summary>
    /// 加入Actor.
    /// </summary>
    /// <param name="actor"></param>
    public void AddActor(AbsActor actor)
    {
        if (!ActorList.Contains(actor))
        {
            ActorList.Add(actor);
            actor.PropertyChanged += OnActorPropertyChanged;
        }
    }

    /// <summary>
    /// 移除Actor.
    /// </summary>
    /// <param name="actor"></param>
    public void RemoveActor(AbsActor actor)
    {
        if (!ActorList.Contains(actor))
        {
            ActorList.Remove(actor);
            actor.PropertyChanged -= OnActorPropertyChanged;
            actor = null;
        }
    }

    public virtual AbsActor GetActorByID(int id)
    {
        if(ActorList.Count > 0)
        {
            for (int i = 0; i < ActorList.Count; i++)
            {
                if (actorList[i].ID == id) return actorList[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 使用Actor名稱取得Actor(一般就是GameObject名稱).
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual AbsActor GetActorByName(string name)
    {
        name += "(Clone)";

        for (int i = 0; i < actorList.Count; i++)
        {
            if (actorList[i].Name == name) return actorList[i];
        }

        return default(AbsActor);
    }

    /// <summary>
    /// 當Actor的屬性發生變化時調用.
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="id"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    protected void OnActorPropertyChanged(AbsActor actor, int id, object oldValue, object newValue)
    {

    }
}