/*
 * Author:ImL1s
 * Email:ImL1s@outlook.com
 * Description:協程管理類
 */

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Unity協程管理類
/// </summary>
public class UnityCoroutine : UnitySingleton<UnityCoroutine>
{

    ///// <summary>
    ///// 啟動協程
    ///// </summary>
    ///// <param name="routine"></param>
    ///// <returns></returns>
    //public Coroutine StartCoroutine(IEnumerator routine)
    //{
    //   return StartCoroutine(routine);       
    //}

    /// <summary>
    /// 延遲啟動協程
    /// </summary>
    /// <param name="routine"></param>
    /// <param name="delay"></param>
    /// <param name="startedCallback"></param>
    /// <returns></returns>
    public void StartCoroutineDelay(IEnumerator routine,float delay, Define.CoroutineStartedHandler startedCallback = null)
    {
        StartCoroutine(DelayCoroutine(routine, delay, startedCallback));
    }

    public void AsyncAction(Action method,float delaySec)
    {
        StartCoroutine(DelayCallMethod(method, delaySec));
    }

    /// <summary>
    /// 延遲啟動協程
    /// </summary>
    /// <param name="routine"></param>
    /// <param name="delay"></param>
    /// <param name="startedCallback"></param>
    /// <returns></returns>
    private IEnumerator DelayCoroutine(IEnumerator routine, float delay, Define.CoroutineStartedHandler startedCallback)
    {
        float timer = 0;

        while (timer > delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (startedCallback != null) startedCallback(StartCoroutine(routine));
        else StartCoroutine(routine);
    }

    private IEnumerator DelayCallMethod(Action method,float delaySec)
    {
        yield return new WaitForSeconds(delaySec);

        if (method != null)
            method.Invoke();
    }
}
