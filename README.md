# Unity-YSUIInject

## 介紹
使用這套UI注入工具可以讓你在Monobehavior中關聯UI時不需要寫大量的transform.find("xxx")或是GameObject.Find("xxx")

## 使用方式

<b>使用特性(Attribute)的方式注入</b>

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



