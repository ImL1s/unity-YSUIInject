# Unity-YSUIInject

## 介紹
使用這套UI注入工具可以讓你在Monobehavior中關聯UI時不需要寫大量的transform.find("xxx")或是GameObject.Find("xxx")

只需要在需要的Component上加入特性,就能達到該目的.

## 使用方式

<b>使用特性(Attribute)的方式注入</b>

	using System;
	using System.Collections;
	using UnityEngine;
	using UnityEngine.UI;
	using YSFramework;

	public class TestInjectPanel :MonoBehaviour
	{

		[ViewInject("./btn")]
		public Button test0Button;
	
		[ViewInject("Canvas/Panel/btn")]
		public Button test1Button
		{
			get;
			set;
		}
	
		public Button test2Button
		{
			get;
			set;
		}
	
		[ViewInject("Canvas/Panel/btn")]
		public Button test3Button;
	
		[ViewInject("./Img")]
		public Image test0Image;
	
	
		void Awake()
		{
			InjectManager.Inject (this);
	
			print ("Button:" + test0Button);
			print ("Button:" + test1Button);
			print ("Button:" + test2Button);
			print ("Button:" + test3Button);
			print ("Button:" + test0Image);
		}
	}

輸出結果為:

	Button:btn (UnityEngine.UI.Button)
	UnityEngine.MonoBehaviour:print(Object)
	
	Button:btn (UnityEngine.UI.Button)
	UnityEngine.MonoBehaviour:print(Object)
	
	Button:
	UnityEngine.MonoBehaviour:print(Object)
	
	Button:btn (UnityEngine.UI.Button)
	UnityEngine.MonoBehaviour:print(Object)
	
	Button:Img (UnityEngine.UI.Image)
	UnityEngine.MonoBehaviour:print(Object)
	






	




