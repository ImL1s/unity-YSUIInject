//
// /**************************************************************************
//
// MessageCenter.cs
//
// Author: ImL1s  <ImL1s@outlook.com>
//
// Date: 16-02-26
//
// Description:singleton 音頻管理器.
//
// Copyright (c) 2016 ImL1s
//
// **************************************************************************/

using UnityEngine;
using System.Collections;
using System;

namespace YSFramework.UnityManager
{

    public class UnityAudioManager : UnitySingleton<UnityAudioManager>, IUnityManager
    {
        private AudioSource audioSourceSE;
        private AudioSource audioSourceBG;
        private AudioSource audioSourceUI;
        private AudioSource audioSourceOther;

        private AudioClip buttonClip;


        //private AudioClip ButtonClip
        //{
        //    get
        //    {
        //        if (buttonClip == null) buttonClip = Resources.Load<AudioClip>(Define.Audio.BtnSoundPath);
        //        return buttonClip;
        //    }
        //    set { buttonClip = value; }
        //}

        #region static method

        public static void Play2D(Define.Audio audio)
        {
            Instance.audioSourceUI.clip = audio.Source;
            Instance.audioSourceUI.Play();
        }

        public static void Play3DAtPoint(GameObject gameObject, Define.Audio audio)
        {
            AudioSource.PlayClipAtPoint(audio.Source, gameObject.transform.position);
        }

        #endregion


        public void Init()
        {
            if (audioSourceSE == null) audioSourceSE = gameObject.AddComponent<AudioSource>();
            if (audioSourceBG == null) audioSourceBG = gameObject.AddComponent<AudioSource>();
            if (audioSourceUI == null) audioSourceUI = gameObject.AddComponent<AudioSource>();
            if (audioSourceOther == null) audioSourceOther = gameObject.AddComponent<AudioSource>();

            audioSourceUI.spatialBlend = 0.0f;
        }


        /// <summary>
        /// 根據場景撥放BGM.
        /// </summary>
        /// <param name="audioType"></param>
        public void PlayAudioByScene(Define.SceneType sceneType)
        {
            string audioName = Define.Audio.GetSceneAudioName(sceneType);
            AudioClip clip = Resources.Load<AudioClip>(Define.ResourcesAudioPath + audioName);

            if (clip != null)
            {
                audioSourceBG.Stop();
                audioSourceBG.loop = true;
                audioSourceBG.clip = clip;
                audioSourceBG.Play();
            }
            else
            {
                Debug.Log("Scene :" + sceneType.ToString() + "don't have BGM, stop play audio!");
                //Debug.LogError("Try play a null audio in AudioManager! SceneType: " + sceneType.ToString());
                //audioSourceBG.Stop();
            }
        }

        public void PlayBGMByName(string bgmName)
        {
            AudioClip clip = Resources.Load<AudioClip>(Define.ResourcesAudioPath + bgmName);

            if (clip != null)
            {
                audioSourceBG.Stop();
                audioSourceBG.loop = true;
                audioSourceBG.clip = clip;
                audioSourceBG.Play();
            }
        }

        /// <summary>
        /// 播放按鈕點擊音效.
        /// </summary>
        //public void PlayBtnSound()
        //{
        //    if (!audioSourceUI.clip == ButtonClip) audioSourceUI.clip = ButtonClip;
        //    if (audioSourceUI.loop) audioSourceUI.loop = false;
        //    audioSourceUI.Play();
        //}
    }
}
