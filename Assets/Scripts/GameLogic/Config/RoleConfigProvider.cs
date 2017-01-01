using UnityEngine;
using System.Collections;

public class RoleConfigProvider : BaseProvider<RoleConfigProvider>
{
    private RoleConfig _roleConfig;

    public override void Init()
    {
        _roleConfig = RoleConfig;
    }

    public RoleConfig RoleConfig
    {
        get
        {
            if (_roleConfig == null)
                _roleConfig = LoadConfig<RoleConfig>("/xml/RoleConfig.xml");
            return _roleConfig;
        }
    }


    /// <summary>
    /// Gets all.
    /// </summary>
    /// <returns>The all.</returns>
    public RoleConfigRace[] GetAll()
    {
        return RoleConfig.Items;
    }

    /// <summary>
    /// Finds the name of the help config by.通过种族名称获取种族包含的职业数据
    /// </summary>
    /// <returns>The help config by name.</returns>
    /// <param name="name">Name.</param>
    public RoleConfigRace GetRoleConfigRaceByName(string name)
    {

        RoleConfigRace content = System.Array.Find<RoleConfigRace>(RoleConfig.Items, delegate(RoleConfigRace obj)
        {
            return obj.RaceEName == name;
        });
        if (content == null)
            Debug.Log("HeroConfigProvider FindHelpConfigByName not Find! key: " + name);
        return content;
    }

    /// <summary>
    /// Finds the name of the help config by.通过种族名称和职业名称获取种族职业数据
    /// </summary>
    /// <returns>The help config by name.</returns>
    /// <param name="name">Name.</param>
    public RoleConfigRaceProfession GetRoleConfigRaceProfessionByName(string raceName,string professionName)
    {
        RoleConfigRace content = System.Array.Find<RoleConfigRace>(RoleConfig.Items, delegate(RoleConfigRace obj)
        {
            return obj.RaceEName == raceName;
        });
        if (content == null)
            Debug.Log("HeroConfigProvider FindHelpConfigByName not Find! key: " + raceName);
        foreach (RoleConfigRaceProfession roleConfigRaceProfession in content.Profession)
        {
            if (roleConfigRaceProfession.ProfessionEName==professionName)
            {
                return roleConfigRaceProfession;
            }
        }
        return null;
    }
}


