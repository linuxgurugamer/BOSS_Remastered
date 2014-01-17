using System;
using System.Collections.Generic;
using System.Text;
using KSP.IO;


public class BOSSSettings
{
    PluginConfiguration pluginsettings = PluginConfiguration.CreateForType<BOSS>(null);

    public void Create()
    { 
        
    }

    public void Load()
    {

        pluginsettings.load();
    }

    public void Save()
    {
        pluginsettings.save();
    }

    public void SetValue(string name, string value)
    {
        pluginsettings.SetValue(name, value);
    }

    public string GetValue(string name)
    {

        return pluginsettings.GetValue<string>(name);
    }
}


