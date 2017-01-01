# Unity-YSUIInject
-

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



