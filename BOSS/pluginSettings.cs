using System.IO;
using UnityEngine;

namespace BOSS
{
    public class BOSSSettings
    {
        ConfigNode configFile;
        ConfigNode configFileNode;

        //PluginConfiguration pluginsettings = PluginConfiguration.CreateForType<BOSS>(null);

        public void Create()
        {
            if (configFileNode == null)
            {
                configFile = new ConfigNode();
            }
            if (!configFile.HasNode("BOSS"))
            {
                configFileNode = new ConfigNode("BOSS");
                configFile.SetNode("BOSS", configFileNode, true);
            }
            else
            {
                if (configFileNode == null)
                {
                    configFileNode = configFile.GetNode("BOSS");
                    //if (configFileNode == null)
                    //print("configFileNode is null");

                }
            }
        }

        public bool Load()
        {
            if (!Directory.Exists(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData"))
                Directory.CreateDirectory(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData");
            configFile = ConfigNode.Load(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData/BOSS.cfg");
            if (configFile != null)
                configFileNode = configFile.GetNode("BOSS");
            return configFile != null;
            //pluginsettings.load();
        }

        public void Save()
        {
            if (!Directory.Exists(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData"))
                Directory.CreateDirectory(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData");

            configFile.Save(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData/BOSS.cfg");
            //pluginsettings.save();
        }

        public void SetValue(string name, string value)
        {
            name = name.Substring(6, name.Length - 6);
            //pluginsettings.SetValue(name, value);
            configFileNode.SetValue(name, value, true);
        }

        public string GetValue(string name)
        {
            string s = "";
            if (name == null)
                return "";
            name = name.Substring(6, name.Length - 6);
            if (configFileNode.HasValue(name))
                s = configFileNode.GetValue(name);
           
            return s;
            //return pluginsettings.GetValue<string>(name);
        }
    }
}