#define CustomEventTriggerListener

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSFramework;

namespace YSFramework.Utils
{
	public class TouchHandle
	{
		private event Define.OnTouchEventHandler eventHandle = null;
		private object[] handleParams;

		public TouchHandle(){}

		public TouchHandle(Define.OnTouchEventHandler handle,params object[] paras)
		{
			SetHandle(handle,paras);
		}

		public void SetHandle(Define.OnTouchEventHandler handle,params object[] paras)
		{
			DestoryHandle();
			this.eventHandle += handle;
			handleParams = paras;
		}

		/// <summary>
		/// 群發事件 Calls the event handle.
		/// </summary>
		/// <param name="listener">Listener.</param>
		/// <param name="arg">Argument.</param>
		public void CallEventHandle(GameObject listener,object arg)
		{
			if(eventHandle != null)
			{
				eventHandle(listener,arg,handleParams);
			}
		}

		public void DestoryHandle()
		{
			if(eventHandle != null)
			{
				eventHandle -= eventHandle;
				eventHandle = null;
			}
		}

        public static TouchHandle operator +(TouchHandle self, Define.OnTouchEventHandler handler)
        {
            self.eventHandle += handler;
            return self;
        }
    }

	public static class EventExtend
	{
		/// <summary>
		/// 加入IPointDown的EventTriggerListener,讓該物體具備接收使用者點擊的能力
		/// </summary>
		/// <param name="btn">要加入EventTriggerListener的按鈕</param>
		/// <returns></returns>
		public static void AddPointDownListener(this Button btn,
			Define.OnTouchEventHandler handler, params object[] paras)
		{
			var trigger = btn.gameObject.GetOrAddComponent<EventTriggerListener>();
			trigger.SetOnclickHandle(handler, paras);
		}

		public static void AddPointDownListener(this Text text,Define.OnTouchEventHandler handler,params object[] paras)
		{
			var trigger = text.gameObject.GetOrAddComponent<EventTriggerListener>();
			trigger.SetOnclickHandle(handler, paras);
		}
	}

	public class EventTriggerListener : MonoBehaviour,IPointerClickHandler,
IPointerDownHandler,
IPointerUpHandler,
	IPointerEnterHandler,
IPointerExitHandler,
ISelectHandler,
IUpdateSelectedHandler,
IDeselectHandler,
IDragHandler,
IEndDragHandler,
IDropHandler,
IScrollHandler,
IMoveHandler
	{

        #region TouchHandle event

        private TouchHandle onClickHandle;
        private TouchHandle onDoubleClickHandle;
        private TouchHandle onDownHandle;
        private TouchHandle onEnterHandle;
        private TouchHandle onExitHandle;
        private TouchHandle onUpHandle;
        private TouchHandle onSelectHandle;
        private TouchHandle onUpdateSelectHandle;
        private TouchHandle onDeSelectHandle;
        private TouchHandle onDragHandle;
        private TouchHandle onDragEndHandle;
        private TouchHandle onDropHandle;
        private TouchHandle onScrollHandle;
        private TouchHandle onMoveHandle;

        public TouchHandle OnClickHandle
        {
            get
            {
                if (onClickHandle == null) onClickHandle = new TouchHandle();
                return onClickHandle;
            }

            set
            {
                onClickHandle = value;
            }
        }

        public TouchHandle OnDoubleClickHandle
        {
            get
            {
                return onDoubleClickHandle;
            }

            set
            {
                onDoubleClickHandle = value;
            }
        }

        public TouchHandle OnDownHandle
        {
            get
            {
                return onDownHandle;
            }

            set
            {
                onDownHandle = value;
            }
        }

        public TouchHandle OnEnterHandle
        {
            get
            {
                return onEnterHandle;
            }

            set
            {
                onEnterHandle = value;
            }
        }

        public TouchHandle OnExitHandle
        {
            get
            {
                return onExitHandle;
            }

            set
            {
                onExitHandle = value;
            }
        }

        public TouchHandle OnUpHandle
        {
            get
            {
                return onUpHandle;
            }

            set
            {
                onUpHandle = value;
            }
        }

        public TouchHandle OnSelectHandle
        {
            get
            {
                return onSelectHandle;
            }

            set
            {
                onSelectHandle = value;
            }
        }

        public TouchHandle OnUpdateSelectHandle
        {
            get
            {
                return onUpdateSelectHandle;
            }

            set
            {
                onUpdateSelectHandle = value;
            }
        }

        public TouchHandle OnDeSelectHandle
        {
            get
            {
                return onDeSelectHandle;
            }

            set
            {
                onDeSelectHandle = value;
            }
        }

        public TouchHandle OnDragHandle
        {
            get
            {
                return onDragHandle;
            }

            set
            {
                onDragHandle = value;
            }
        }

        public TouchHandle OnDragEndHandle
        {
            get
            {
                return onDragEndHandle;
            }

            set
            {
                onDragEndHandle = value;
            }
        }

        public TouchHandle OnDropHandle
        {
            get
            {
                return onDropHandle;
            }

            set
            {
                onDropHandle = value;
            }
        }

        public TouchHandle OnScrollHandle
        {
            get
            {
                return onScrollHandle;
            }

            set
            {
                onScrollHandle = value;
            }
        }

        public TouchHandle OnMoveHandle
        {
            get
            {
                return onMoveHandle;
            }

            set
            {
                onMoveHandle = value;
            }
        }



        #endregion

        #region IPointerEnterHandler implementation

        public void OnPointerEnter (PointerEventData eventData)
		{
            if (OnEnterHandle != null)
            {
                OnEnterHandle.CallEventHandle(this.gameObject, eventData);
            }
		}

        #endregion

        #region IMoveHandler implementation

        public void OnMove (AxisEventData eventData)
		{
            if (OnEnterHandle != null)
            {
                OnEnterHandle.CallEventHandle(this.gameObject, eventData);
            }
        }

		#endregion

		#region IScrollHandler implementation

		public void OnScroll (PointerEventData eventData)
		{
			
		}

		#endregion

		#region IDropHandler implementation

		public void OnDrop (PointerEventData eventData)
		{
			
		}

		#endregion

		#region IEndDragHandler implementation

		public void OnEndDrag (PointerEventData eventData)
		{
			
		}

		#endregion

		#region IDragHandler implementation

		public void OnDrag (PointerEventData eventData)
		{
            
		}

		#endregion

		#region IDeselectHandler implementation

		public void OnDeselect (BaseEventData eventData)
		{
			
		}

		#endregion

		#region IUpdateSelectedHandler implementation

		public void OnUpdateSelected (BaseEventData eventData)
		{
			
		}

		#endregion

		#region ISelectHandler implementation

		public void OnSelect (BaseEventData eventData)
		{

		}

		#endregion

		#region IPointerExitHandler implementation

		public void OnPointerExit (PointerEventData eventData)
		{

		}

		#endregion

		#region IPointerDownHandler implementation

		public void OnPointerDown (PointerEventData eventData)
		{
			
		}



		#region IPointerUpHandler implementation

		public void OnPointerUp (PointerEventData eventData)
		{
            
		}

		#endregion

		#region IPointerClickHandler implementation

		public void OnPointerClick (PointerEventData eventData)
		{
			if(OnClickHandle != null) OnClickHandle.CallEventHandle(this.gameObject,eventData);
		}
			
		#endregion

		#endregion

        /// <summary>
        /// 加入並取得EventTriggerListener監聽器
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
		public static EventTriggerListener Get(GameObject go)
		{
            #region old

            //EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            //if(listener == null) return go.AddComponent<EventTriggerListener>();

            //return listener;

            #endregion

            return go.GetOrAddComponent<EventTriggerListener>();
		}


        /// <summary>
        /// 設定單擊事件
        /// </summary>
        /// <param name="handler"></param>
        public void SetOnclickHandle(Define.OnTouchSingleEventHandler handler)
        {
            // TODO 完成簡單的單擊事件
            //SetEventHandle(EnumTouchEventType.OnClick, handler, null);
        }

        /// <summary>
        /// 設定單擊事件
        /// </summary>
        public void SetOnclickHandle(Define.OnTouchEventHandler handler,params object[] paras)
        {
            SetEventHandle(Define.EnumTouchEventType.OnClick, handler, paras);
        }

        /// <summary>
        /// 設定指定監聽的按鍵
        /// </summary>
        /// <param name="touchEventType"></param>
        /// <param name="handle"></param>
        /// <param name="paras"></param>
        public void SetEventHandle(Define.EnumTouchEventType touchEventType, Define.OnTouchEventHandler handle, params object[] paras)
        {
            switch (touchEventType)
            {
                case Define.EnumTouchEventType.OnClick:
                    if (OnClickHandle == null) OnClickHandle = new TouchHandle(handle, paras);
                    else OnClickHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnDoubleClick:
                    if (null == OnDoubleClickHandle)
                    {
                        OnDoubleClickHandle = new TouchHandle();
                    }
                    OnDoubleClickHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnDown:
                    if (OnDownHandle == null)
                    {
                        OnDownHandle = new TouchHandle();
                    }
                    OnDownHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnUp:
                    if (OnUpHandle == null)
                    {
                        OnUpHandle = new TouchHandle();
                    }
                    OnUpHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnEnter:
                    if (OnEnterHandle == null)
                    {
                        OnEnterHandle = new TouchHandle();
                    }
                    OnEnterHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnExit:
                    if (OnExitHandle == null)
                    {
                        OnExitHandle = new TouchHandle();
                    }
                    OnExitHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnDrag:
                    if (OnDragHandle == null)
                    {
                        OnDragHandle = new TouchHandle();
                    }
                    OnDragHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnDrop:
                    if (OnDropHandle == null)
                    {
                        OnDropHandle = new TouchHandle();
                    }
                    OnDropHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnDragEnd:
                    if (OnDragEndHandle == null)
                    {
                        OnDragEndHandle = new TouchHandle();
                    }
                    OnDragEndHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnSelect:
                    if (OnSelectHandle == null)
                    {
                        OnSelectHandle = new TouchHandle();
                    }
                    OnSelectHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnUpdateSelect:
                    if (OnUpdateSelectHandle == null)
                    {
                        OnUpdateSelectHandle = new TouchHandle();
                    }
                    OnUpdateSelectHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnDeSelect:
                    if (OnDeSelectHandle == null)
                    {
                        OnDeSelectHandle = new TouchHandle();
                    }
                    OnDeSelectHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnScroll:
                    if (OnScrollHandle == null)
                    {
                        OnScrollHandle = new TouchHandle();
                    }
                    OnScrollHandle.SetHandle(handle, paras);
                    break;

                case Define.EnumTouchEventType.OnMove:
                    if (OnMoveHandle == null)
                    {
                        OnMoveHandle = new TouchHandle();
                    }
                    OnMoveHandle.SetHandle(handle, paras);
                    break;
            }
        }

		void OnDestory()
		{
//			this.RemoveAllHandle();
		}

    }
}

