using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

public class HeroConfigProvider : BaseProvider<HeroConfigProvider>
{
	private Schemas.HelpConfig _helpConfig;
	private Schemas.HeroConfig _heroConfig;

	public override void Init ()
	{
		_helpConfig = HelpConfig;
		_heroConfig = HeroConfig;
	}
	
	public Schemas.HelpConfig HelpConfig
	{
		get {
			if (_helpConfig == null)
				_helpConfig = LoadConfig<Schemas.HelpConfig>("/xml/HelpConfig.xml");
			return _helpConfig;
		}
	}

	public Schemas.HeroConfig HeroConfig
	{
		get {
			if (_heroConfig == null)
				_heroConfig = LoadConfig<Schemas.HeroConfig>("/xml/HeroConfig.xml");
			return _heroConfig;
		}
	}

	/// <summary>
	/// Gets all.
	/// </summary>
	/// <returns>The all.</returns>
	public Schemas.HelpConfigContent[] GetAll()
	{
		return HelpConfig.Content;
	}

	/// <summary>
	/// Finds the name of the help config by.通过name获取数据
	/// </summary>
	/// <returns>The help config by name.</returns>
	/// <param name="name">Name.</param>
	public Schemas.HelpConfigContent GetHelpConfigByName(string name)
	{
		
		Schemas.HelpConfigContent content = System.Array.Find<Schemas.HelpConfigContent>(HelpConfig.Content, delegate(Schemas.HelpConfigContent obj){
			return obj.Name == name;
		});
		if (content == null)
			Debug.Log("HeroConfigProvider FindHelpConfigByName not Find! key: " + name);
		return content;
	}

	/// <summary>
	/// Gets the help config by position.通过position获取数据
	/// </summary>
	/// <returns>The help config by position.</returns>
	/// <param name="position">Position.</param>
	public Schemas.HelpConfigContent GetdHelpConfigByPosition(string position)
	{       
		Schemas.HelpConfigContent content = System.Array.Find<Schemas.HelpConfigContent>(HelpConfig.Content,delegate(Schemas.HelpConfigContent obj){
			return obj.Position == position;
		});
		if (content == null)
			Debug.Log("HeroConfigProvider FindHelpConfigByName not Find! key: " + position);
		return content;
	}
}

