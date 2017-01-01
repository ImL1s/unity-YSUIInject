using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConfigManager : Singleton<ConfigManager>
{
	private List<System.Type> defaultConfig = new List<System.Type>() {};
	
	public static void Init(Action<int, int> progress = null, Action finished = null)
	{

	}


}

