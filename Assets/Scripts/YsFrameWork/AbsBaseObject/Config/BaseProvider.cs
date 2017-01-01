using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Diagnostics;
using System;

#region Singleton<> second version

//public abstract class BaseProvider
//{
//	public int ID { get; protected set; }
//
//	protected static T LoadConfig<T> (string path) where T : class, new()
//	{
//#if DEBUG
//		Stopwatch sw = new Stopwatch();
//		sw.Start();
//#endif
//		XmlDocument doc = Utils.GetLocalXmlDoc(path);
//		MemoryStream stream = new MemoryStream(Utils.StringToUTF8ByteArray(Utils.ConvertXmlToString(doc)));
//		XmlSerializer xs = new XmlSerializer(typeof(T));
//		T config = xs.Deserialize(stream) as T;
//#if DEBUG
//		sw.Stop();
//		UnityEngine.Debug.Log(String.Concat(typeof(T).ToString(), " time: ", sw.ElapsedMilliseconds));
//#endif
//		return config;
//	}
//
//
//}

/// <summary>
/// Generic C# singleton.
/// </summary>
public abstract class ProviderSingleton<T>  where T : class, new()//: BaseProvider where T : class, new()
{
	
	/// <summary>
	/// The m_ instance.
	/// </summary>
	protected static T _Instance = null;
	
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{ 
			if (null == _Instance)
			{
				_Instance = new T();
			}
			return _Instance; 
		}
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="XHEngine.Singleton`1"/> class.
	/// </summary>
	protected ProviderSingleton()
	{
//		if(null != _Instance)
//			throw new SingletonException("This " + (typeof(T)).ToString() + " Singleton Instance is not null !!!");
		Init ();
	}
	
	
	/// <summary></summary>
	/// Init this Singleton.
	/// </summary>
	public virtual void Init() {}
}
#endregion


/// <summary>
/// Generic C# singleton.
/// </summary>
public abstract class BaseProvider<T> : ProviderSingleton<T> where T : class, new()
{
	public int ID { get; protected set; }

    /// <summary>
    /// 读取config中的xml
    /// </summary>
    /// <typeparam name="C"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
	protected static C LoadConfig<C> (string path) where C : class, new()
	{
		#if DEBUG
		Stopwatch sw = new Stopwatch();
		sw.Start();
		#endif

		XmlDocument doc = YSFramework.Tools.Utils.GetLocalXmlDoc(path);
		MemoryStream stream = new MemoryStream(YSFramework.Tools.Utils.StringToUTF8ByteArray(YSFramework.Tools.Utils.ConvertXmlToString(doc)));
		XmlSerializer xs = new XmlSerializer(typeof(C));
		C config = xs.Deserialize(stream) as C;

		#if DEBUG
		sw.Stop();
		UnityEngine.Debug.Log(String.Concat(typeof(C).ToString(), " time: ", sw.ElapsedMilliseconds));
		#endif
		return config;
	}
}
