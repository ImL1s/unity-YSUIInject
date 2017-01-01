/*
 * Copyright(c) 2016  All Rights Reserved.  
 *
 * auth： ImL1s 
 *
 * mail：  aa22396584@gmail.com  
 *
 * date： 2016/9/15 下午 09:48:09  
 *
 * desc： 卡片資料  
 *
 * Ver : V1.0.0  
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schemas;

public class CardConfigProvider :BaseProvider<CardConfigProvider>
{
    private CardConfig cardConfig;

    public CardConfig CardConfig
    {
        get
        {
            if (cardConfig == null) cardConfig = LoadConfig<CardConfig>("/xml/CardConfig.xml");
            return cardConfig;
        }

        set
        {
            cardConfig = value;
        }
    }
}

