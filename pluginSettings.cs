using KSP.IO;

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
            configFile = ConfigNode.Load(KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData/BOSS.cfg");
            return configFile != null;
            //pluginsettings.load();
        }

        public void Save()
        {
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
            name = name.Substring(6, name.Length - 6);
            return configFileNode.GetValue(name);
            //return pluginsettings.GetValue<string>(name);
        }
    }
}