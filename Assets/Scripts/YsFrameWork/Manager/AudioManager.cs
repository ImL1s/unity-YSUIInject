using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using YSFramework.UnityManager;

namespace YSFramework
{
    public class AudioManager : Singleton<AudioManager>, IManager
    {
        private Dictionary<string,Define.Audio> audioDic;
        private UnityAudioManager unityAudioManager;

        public static void Play(string audioName)
        {
            var audioDic = Instance.audioDic;

            if (audioDic.ContainsKey(audioName))
            {
                Define.Audio audio = audioDic[audioName];

                if (audio != null)
                {
                    UnityAudioManager.Play2D(audio);
                }
                else
                {
                    Debug.LogError("123");
                }
            }
            else
            {
                Debug.LogError("Audio不存在... 名稱:" + audioName);
            }
        }

        public void Init()
        {
            Debug.Log("AudioManager Init...");

            audioDic = new Dictionary<string, Define.Audio>();
            unityAudioManager = UnityAudioManager.Instance;
            unityAudioManager.Init();

            Debug.Log("AudioManager init completed");
        }

        /// <summary>
        /// <para>Register audio by default path in Define.cs</para>
        /// <para> 註冊音效、BGM，默認名字為該檔案的名稱，本管理器會自動尋找Resource/{默認路徑}/該檔案名稱 </para>
        /// </summary>
        /// <param name="audioName">audioName,don't contain path</param>
        public void RegisterByName(string audioName)
        {
            if (!audioDic.ContainsKey(audioName))
            {
                AudioClip source = Resources.Load<AudioClip>(Define.Audio.AudioPath + audioName);

                if (source != null)
                {
                    Define.Audio audio = new Define.Audio(audioName, source);
                    audioDic.Add(audioName, audio);
                }
                else
                {
                    Debug.LogError("Register audio fail :path dosn't contain clip, audio name: " + audioName);
                }
            }
            else
            {
                Debug.LogError("Register audio fail :already exist audioName, audio name: " + audioName);
            }
        }


        // TODO
        public void RegisterByPath(string path)
        {
            Debug.LogError("還未實現!!");
        }

        public void PlayAudioByScene(Define.SceneType sceneType)
        {
            unityAudioManager.PlayAudioByScene(sceneType);
        }

        public void PlayByName(string audioName)
        {
            Play(audioName);
        }

        public void PlayBGMByName(string bgmName)
        {
            unityAudioManager.PlayBGMByName(bgmName);
        }
    }
}
