using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YSFramework;

public class TestInjectPanel :MonoBehaviour
{

//	[ViewInject("./btn")]
	[ViewInject("Canvas/Panel/btn")]
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

//	protected override void OnAwake ()
//	{
//		InjectManager.Inject (this);
//	}
}
	
