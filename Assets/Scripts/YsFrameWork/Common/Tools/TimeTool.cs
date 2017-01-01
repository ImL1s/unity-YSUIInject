/*
 * Author:ImL1s
 *
 * Date:2016/06/19
 *
 * Email:ImL1s@outlook.com
 *
 * description:時間工具.
 */


using UnityEngine;
using System.Collections;
using System;

namespace YSFramework.Tools
{

    public class TimeTool
    {
        /// <summary>
        /// 將秒數轉成 分鐘:秒數
        /// </summary>
        /// <param name="currentSec"></param>
        /// <param name="minute"></param>
        /// <param name="sec"></param>
        public static void GetMinuteAndSec(float currentSec, out int minute, out int sec)
        {
            minute = (int)Math.Floor(currentSec / 60);
            sec = (int)(currentSec % 60);
        }

    }
}
