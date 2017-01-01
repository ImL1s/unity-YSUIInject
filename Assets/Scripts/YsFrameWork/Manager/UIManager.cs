/*
 * Author:ImL1s
 *
 * Date:2016/02/11
 *
 * Email:ImL1s@outlook.com
 *
 * description:UI管理器類.
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

namespace YSFramework
{

	public class UIManager : BaseManager<UIManager>
    {
        #region field

        private Canvas canvas;

        #endregion

        #region get/set property


        /// <summary>
        /// Opened UI.
        /// 已經打開的UI.
        /// </summary>
        public Dictionary<Define.UIType, AbsUIPanel> OpenedUI { get; protected set; }

        /// <summary>
        /// 即將打開的UI.
        /// </summary>
        public Stack<UIInfo> AboutToOpenUI { get; protected set; }

        protected Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = CreateCanvas();
                    CreateEventSystem();
                }
                return canvas;
            }

            set
            {
                canvas = value;
            }
        }

        #endregion

        #region static method


        /// <summary>
        /// 打開UI
        /// </summary>
        /// <param name="UIType"></param>
        /// <param name="closeOther"></param>
        /// <param name="UIParas"></param>
        public static void OpenUpUI(Define.UIType UIType, bool closeOther = true, object UIParas = null)
        {
            Instance.OpenUI(UIType, closeOther, UIParas);
        }

		/// <summary>
		/// 關閉UI
		/// </summary>
		/// <param name="UIPanel">User interface panel.</param>
		public static void CloseUpUI(AbsUIPanel UIPanel)
		{
			Instance.CloseUI (UIPanel);
		}

        #endregion

		public override void Init()
		{
			Debug.Log("UIManager init ...");

			OpenedUI = new Dictionary<Define.UIType, AbsUIPanel>();
			AboutToOpenUI = new Stack<UIInfo>();

			Debug.Log("UIManager init completed");
		}

        #region public method




        /// <summary>
        /// 取得已經打開UI.
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        public AbsUIPanel GetOpendUI(Define.UIType uiType)
        {
            if (OpenedUI.ContainsKey(uiType)) return OpenedUI[uiType];

            Debug.Log("無法取得打開中的UI... \t UI類型:" + uiType.ToString());
            return default(AbsUIPanel);
        }

        /// <summary>
        /// 打開UI.
        /// </summary>
        /// <param name="UIType"></param>
        /// <param name="closeOther"></param>
        /// <param name="UIParas"></param>
        public void OpenUI(Define.UIType UIType, bool closeOther = true, object UIParas = null)
        {
            if (closeOther) CloseUIAll();

            if (!OpenedUI.ContainsKey(UIType))
            {
                AboutToOpenUI.Push(new UIInfo(UIType, UIParas));
            }

            if (AboutToOpenUI != null && AboutToOpenUI.Count > 0)
            {
                UnityCoroutine.Instance.StartCoroutine(AsyncLoadData());
            }
        }

        /// <summary>
        /// 打開多個UI.
        /// </summary>
        /// <param name="UITypes"></param>
        /// <param name="closeOther"></param>
        /// <param name="UIParas"></param>
        public void OpenUIMulti(Define.UIType[] UITypes, bool closeOther = true, object UIParas = null)
        {
            if (closeOther)
            {
                CloseUIAll();
            }

            for (int i = 0; i < UITypes.Length; i++)
            {
                if (!OpenedUI.ContainsKey(UITypes[i]))
                {
                    AboutToOpenUI.Push(new UIInfo(UITypes[i], UIParas));
                }
            }

            if (AboutToOpenUI != null && AboutToOpenUI.Count > 0)
            {
                UnityCoroutine.Instance.StartCoroutine(AsyncLoadData());
            }
        }

        /// <summary>
        /// 關閉所有的UI.
        /// </summary>
        public void CloseUIAll()
        {
            if (OpenedUI.Keys == null) return;

            List<Define.UIType> allUIEnum = new List<Define.UIType>(OpenedUI.Keys);
            CloseMultiUI(allUIEnum.ToArray());
        }

        /// <summary>
        /// 關閉UI.
        /// </summary>
        /// <param name="UITypes"></param>
        public void CloseMultiUI(params Define.UIType[] UITypes)
        {
            for (int i = 0; i < UITypes.Length; i++)
            {
                CloseUI(UITypes[i]);
            }
        }

        /// <summary>
        /// 關閉UI.
        /// </summary>
        /// <param name="UITypes"></param>
        public void CloseUI(Define.UIType UIType)
        {
            GameObject UIObj = GetUIGO(UIType);

            if (UIObj == null) OpenedUI.Remove(UIType);
            else
            {
                AbsUIPanel UI = UIObj.GetComponent<AbsUIPanel>();

                if (UI == null)
                {
                    GameObject.Destroy(UIObj);
                    OpenedUI.Remove(UIType);
                }
                else
                {
                    UI.UIStateChangeCallback += this.OnUIStateChanged;
                    UI.Release();
                }
            }
        }

        /// <summary>
        /// 關閉UI.
        /// </summary>
        /// <param name="UITypes"></param>
        public void CloseUI(AbsUIPanel UIPanel)
        {
            foreach (var UI in OpenedUI)
            {
                if (UIPanel == UI.Value)
                {
                    CloseUI(UI.Key);
                    break;
                }
            }
        }


        /// <summary>
        /// 取得UI的GameObject
        /// </summary>
        /// <param name="UIType"></param>
        /// <returns></returns>
        public GameObject GetUIGO(Define.UIType UIType)
        {
            AbsUIPanel UI = null;
            if (!OpenedUI.TryGetValue(UIType, out UI))
            {
                //Debug.LogError("Failed to get UIGO");
                return null;
            }
            return UI.gameObject;
        }

        /// <summary>
        /// 改變Canvas渲染模式
        /// </summary>
        /// <param name="mode"></param>
        public void ChangeCanvasRenderMode(RenderMode mode)
        {
            this.Canvas.renderMode = mode;

            if(mode == RenderMode.ScreenSpaceCamera)
            {
                this.Canvas.worldCamera = Camera.main;
            }
        }
        /// <summary>
        /// 顯示MessageBox.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="resultCallback"></param>
        //public void ShowMessageBox(string msg, Action<bool> resultCallback = null, bool canClick = true)
        //{
        //    Define.MessageBoxPara messageBoxPara = new Define.MessageBoxPara(msg, resultCallback, canClick);

        //    //UnityEngine.Object preMsgPanel = MonoBehaviour.FindObjectOfType<MessageBoxPanel>();
        //    GameObject preMsgPanel = GameObject.Find("MessageBoxPanel(Clone)");

        //    if (preMsgPanel == null)
        //    {
        //        OpenUI(Define.UIType.MessageBoxPanel, closeOther: false, UIParas: messageBoxPara);
        //    }
        //    else
        //    {
        //        //((MessageBoxPanel)preMsgPanel).Reset(messageBoxPara);
        //        preMsgPanel.GetComponent<MessageBoxPanel>().Reset(messageBoxPara);
        //    }
        //}

        #endregion

        #region protected method

        /// <summary>
        /// 協程讀取UI
        /// </summary>
        /// <returns></returns>
        protected IEnumerator AsyncLoadData()
        {
            yield return null;
            // TODO AsyncLoadData
            UIInfo info = null;
            GameObject prefab = null;
            GameObject UIObj = null;

            while (AboutToOpenUI.Count > 0)
            {
                info = AboutToOpenUI.Pop();
                string path = info.PrefabPath;
                //prefab = Resources.Load<GameObject>(path);
                prefab = ResourceManager.Instance.Load<GameObject>(path);
                UIObj = GameObject.Instantiate<GameObject>(prefab);

                #region old create canvas

                //Canvas canvas = MonoBehaviour.FindObjectOfType<Canvas>();
                //this.Canvas = canvas;

                //if (canvas == null)
                //{
                //    canvas = CreateCanvas();
                //    CreateEventSystem();
                //}

                #endregion

                UIObj.transform.parent = Canvas.transform;
                RectTransform rTransfrom = UIObj.GetComponent<RectTransform>();
                rTransfrom.localPosition = Vector2.zero;
                //rTransfrom.position = Vector2.zero;
                rTransfrom.offsetMin = Vector2.zero;
                rTransfrom.offsetMax = Vector2.zero;
                rTransfrom.localScale = Vector2.one;

                AbsUIPanel UI = UIObj.GetComponent<AbsUIPanel>();
                if (UI == null) UI = UIObj.AddComponent(info.UIScript) as AbsUIPanel;
                UI.SetUI(info.UIPara);

                if (OpenedUI.ContainsKey(info.UIType))
                {
                    CloseUI(OpenedUI[info.UIType]);
                }
                OpenedUI.Add(info.UIType, UI);

                yield return null;
            }
        }

        /// <summary>
        /// UI狀態改變事件.
        /// </summary>
        /// <param name="UI"></param>
        /// <param name="oldState"></param>
        /// <param name="currentState"></param>
        protected void OnUIStateChanged(AbsUIPanel UI, Define.UIState oldState, Define.UIState currentState)
        {
            if (currentState == Define.UIState.Closing)
            {
                UI.UIStateChangeCallback -= this.OnUIStateChanged;
                if (OpenedUI.ContainsKey(UI.Type)) OpenedUI.Remove(UI.Type);
            }
        }

        /// <summary>
        /// 創造Canvas GameObject.
        /// </summary>
        /// <returns></returns>
        protected Canvas CreateCanvas()
        {
            GameObject go = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler),
                typeof(GraphicRaycaster));
            

            Canvas canvas = go.GetComponent<Canvas>();
            CanvasScaler scaler = go.GetComponent<CanvasScaler>();
            GraphicRaycaster caster = go.GetComponent<GraphicRaycaster>();

            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            return canvas;

        }

        protected void CreateEventSystem()
        {
            GameObject go = new GameObject("EventSystem", typeof(UnityEngine.EventSystems.EventSystem),
                typeof(UnityEngine.EventSystems.StandaloneInputModule));

#if UNITY_EDITOR
            go.GetComponent<UnityEngine.EventSystems.StandaloneInputModule>().forceModuleActive = true;
#endif

        }

#endregion

#region inner class

        /// <summary>
        /// UI信息.
        /// </summary>
        public class UIInfo
        {
            /// <summary>
            /// UI類型
            /// </summary>
            public Define.UIType UIType { get; set; }

            /// <summary>
            /// UI腳本
            /// </summary>
            public Type UIScript { get; set; }

            /// <summary>
            /// UI Prefab 路徑
            /// </summary>
            public string PrefabPath { get; set; }

            public object UIPara { get; set; }

            public UIInfo(Define.UIType UIType, object para)
            {
                this.UIType = UIType;
                this.UIScript = Define.UI.GetUIType(UIType);
                this.PrefabPath = Define.UI.GetUIPath(UIType);
                this.UIPara = para;
            }

            public UIInfo(Define.UIType UIType, string path, object para)
            {
                this.UIType = UIType;
                this.UIScript = Define.UI.GetUIType(UIType);
                this.PrefabPath = path;
                this.UIPara = para;
            }


            public UIInfo(Define.UIType UIType, Type UIScript, string path, object para)
            {
                this.UIType = UIType;
                this.UIScript = UIScript;
                this.PrefabPath = path;
                this.UIPara = para;
            }

            public T GetPara<T>()
            {
                return (T)UIPara;
            }
        }

#endregion
    }
}
