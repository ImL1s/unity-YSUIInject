/*
 * Copyright(c) 2016  All Rights Reserved.  
 *
 * auth： ImL1s 
 *
 * mail：  aa22396584@gmail.com  
 *
 * date： 2016/7/15 下午 05:49:59  
 *
 * desc： Path路徑提共  
 *
 * Ver : V1.0.0  
 *
 */

using System;
using Schemas;
using UnityEngine;

class PathConfigProvider :BaseProvider<PathConfigProvider>
{
    private Schemas.PathConfig pathConfig;

    public PathConfig PathConfig
    {
        get
        {
            if (pathConfig == null) pathConfig = LoadConfig<PathConfig>("/xml/PathConfig.xml");
            return pathConfig;
        }

        private set
        {
            pathConfig = value;
        }
    }

    public PathConfigContent GetContent(string name)
    {
        PathConfigContent pathContent = Array.Find(PathConfig.Items, p => p.Name == name);

        if(pathContent == null)
            Debug.Log("PathConfigProvider FindHelpConfigByName not Find! key: " + name);

        return pathContent;
    }
}