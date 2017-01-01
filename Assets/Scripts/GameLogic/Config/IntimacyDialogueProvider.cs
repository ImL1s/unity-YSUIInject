/*
 * Copyright(c) 2016  All Rights Reserved.  
 *
 * auth： ImL1s 
 *
 * mail：  aa22396584@gmail.com  
 *
 * date： 2016/9/14 上午 06:15:52  
 *
 * desc： 親密度對話資料提供
 *
 * Ver : V1.0.0  
 *
 */


using Schemas;
using System;
/// <summary>
/// 親密度對話資料提供
/// </summary>
public class IntimacyDialogueProvider :BaseProvider<IntimacyDialogueProvider>
{
    private IntimacyDialogue intimacyDialogue;

    public IntimacyDialogue IntimacyDialogue
    {
        get
        {
            if (intimacyDialogue == null) intimacyDialogue = LoadConfig<Schemas.IntimacyDialogue>("/xml/IntimacyDialogue.xml");
            return intimacyDialogue;
        }

        set
        {
            intimacyDialogue = value;
        }
    }

    /// <summary>
    /// 取得親密度組
    /// </summary>
    /// <param name="minLevel"></param>
    /// <returns></returns>
    public IntimacyDialogueGroup GetIntimacyDialogueGroup(int minLevel)
    {
        IntimacyDialogueGroup group = Array.Find(this.IntimacyDialogue.Items, obj => obj.MinLevel == minLevel.ToString());
        return group;
    }

    /// <summary>
    /// 取得親密度組
    /// </summary>
    /// <param name="minLevel"></param>
    /// <returns></returns>
    public IntimacyDialogueGroup GetIntimacyDialogueGroup(int minLevel,int maxLevel)
    {
        return Array.Find(this.IntimacyDialogue.Items, obj => (obj.MinLevel == minLevel.ToString() && obj.MaxLevel == maxLevel.ToString()));
    }
}

