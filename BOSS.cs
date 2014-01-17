/*   The Bolt-On Screenshot System is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Bolt-On Screenshot System is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with The Bolt-On Screenshot System.  If not, see <http://www.gnu.org/licenses/>.*/

//---Warning, here be spaghetti-code. Read at your own risk, I am not responsible for any fits of rage, strokes or haemorrhages that occur from reading this code.---//


using System;
using Toolbar;
using UnityEngine;
using KSP.IO;

[KSPAddon(KSPAddon.Startup.EveryScene, false)]
public class BOSS : MonoBehaviour
{
    public Vessel activeVessel;
    protected Rect windowPos, helpWindowPos;
    private string kspDir = KSPUtil.ApplicationRootPath + @"GameData/BOSS/PluginData/BOSS/";
    public int screenshotCount, superSampleValueInt = 1, i;
    public string superSampleValueString = "1", screenshotKey = "z", showGUIKey = "p";
    public bool showHelp = false, burstMode = false, showGUI = false;
    private IButton toolbarButton;
    public BOSSSettings BOSSsettings = new BOSSSettings();


    public void Awake()
    {
        if (!File.Exists<BOSS>(kspDir + "config.xml"))
        {
            try
            {
                createSettings();
            }
            catch
            {
                throw new AccessViolationException("Can't create settings file, please confirm directory is writeable.");
            }
        }
        loadSettings();
        initToolbar();
        RenderingManager.AddToPostDrawQueue(60, new Callback(drawGUI));
    }

    private void initToolbar()
    {
        toolbarButton = ToolbarManager.Instance.add("BOSS", "button1");
        toolbarButton.TexturePath = showHelp ? "BOSS/bon" : "BOSS/boff";
        toolbarButton.ToolTip = "Toggle Bolt-On Screenshot System";
        toolbarButton.OnClick += (e) =>
        {
            if (showHelp) showHelp = false;
            else if (!showHelp) showHelp = true;
            toolbarButton.TexturePath = showHelp ? "BOSS/bon" : "BOSS/boff";
            saveSettings();
        };
    }

    private void drawGUI()
    {
        GUI.skin = HighLogic.Skin;
        if (showHelp)
            helpWindowPos = GUILayout.Window(568, helpWindowPos, UIContent, "B.O.S.S. Control", GUILayout.Width(150),
                GUILayout.Height(150));
    }

    public void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            print("Screenshot button pressed!");
            takeScreenshot();
        }
        if (Input.GetKeyDown(showGUIKey))
        {
            if (showHelp) showHelp = false;
            else if (!showHelp) showHelp = true;
            saveSettings();
            toolbarButton.TexturePath = showHelp ? "BOSS/bon" : "BOSS/boff";
        }
    }

    private void UIContent(int windowID)
    {
        GUIStyle mainGUI = new GUIStyle(GUI.skin.button);
        mainGUI.normal.textColor = mainGUI.focused.textColor = Color.white;
        mainGUI.margin = new RectOffset(12, 12, 8, 0);
        mainGUI.hover.textColor = mainGUI.active.textColor = Color.yellow;
        mainGUI.onNormal.textColor =
            mainGUI.onFocused.textColor = mainGUI.onHover.textColor = mainGUI.onActive.textColor = Color.green;
        mainGUI.padding = new RectOffset(8, 8, 8, 8);

        GUILayout.BeginVertical();
        GUILayout.Label("Current supersample value: " + superSampleValueInt.ToString(), GUILayout.ExpandHeight(true),
            GUILayout.ExpandWidth(true));
        GUILayout.Label("Current take ss key: ", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        screenshotKey = GUILayout.TextField(screenshotKey);
        GUILayout.Label("Supersample value: ");

        if (!int.TryParse(superSampleValueString, out superSampleValueInt))
        {
            superSampleValueString = " ";
        }
        superSampleValueString = GUILayout.TextField(superSampleValueString);

        if (GUILayout.Button("Take Screenshot", mainGUI, GUILayout.Width(125)))
            //GUILayout.Button is "true" when clicked
        {
            if (burstMode)
            {
                //burstModeMethod();
            }
            else
            {
                print("Screenshot button pressed!");
                takeScreenshot();
            }
        }
        GUILayout.Label(screenshotCount + " screenshots taken.");
        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void takeScreenshot()
    {
        string screenshotFilename = "SS_" + DateTime.Today.ToString("MM-dd-yyyy") + "_" + DateTime.Now.ToString("HH-mm-ss");
        print("Screenshot Count:" + screenshotCount);
        print(kspDir + screenshotFilename + ".png");
        print("Your supersample value was " + superSampleValueInt + "!");
        Application.CaptureScreenshot(kspDir + screenshotFilename + ".png", superSampleValueInt);
        screenshotCount++;
        saveSettings();
    }

    private void createSettings()
    {
        BOSSsettings.SetValue("BOSS::screenshotCount", "0");
        BOSSsettings.SetValue("BOSS::windowPos.x", "250");
        BOSSsettings.SetValue("BOSS::windowPos.y", "250");
        BOSSsettings.SetValue("BOSS::helpWindowPos.x", "400");
        BOSSsettings.SetValue("BOSS::helpWindowPos.y", "400");
        BOSSsettings.SetValue("BOSS::showHelp", "True");
        BOSSsettings.SetValue("BOSS::showGUI", "True");
        BOSSsettings.SetValue("BOSS::screenshotKey", "z");
        BOSSsettings.SetValue("BOSS::showGUIKey", "p");
        BOSSsettings.SetValue("BOSS::supersampValue","1");
        BOSSsettings.Save();
        print("Created BOSS settings.");
    }

    private void saveSettings()
    {
        BOSSsettings.SetValue("BOSS::screenshotCount", screenshotCount.ToString());
        BOSSsettings.SetValue("BOSS::windowPos.x", windowPos.x.ToString());
        BOSSsettings.SetValue("BOSS::windowPos.y", windowPos.y.ToString());
        BOSSsettings.SetValue("BOSS::helpWindowPos.x", helpWindowPos.x.ToString());
        BOSSsettings.SetValue("BOSS::helpWindowPos.y", helpWindowPos.y.ToString());
        BOSSsettings.SetValue("BOSS::showHelp", showHelp.ToString());
        BOSSsettings.SetValue("BOSS::showGUI", showGUI.ToString());
        BOSSsettings.SetValue("BOSS::screenshotKey", screenshotKey);
        BOSSsettings.SetValue("BOSS::showGUIKey", showGUIKey);
        BOSSsettings.Save();
        print("Saved BOSS settings.");
    }

    private void loadSettings()
    {
        BOSSsettings.Load();
        windowPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::windowPos.x"));
        windowPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::windowPos.y"));
        helpWindowPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.x"));
        helpWindowPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.y"));
        screenshotCount = Convert.ToInt32(BOSSsettings.GetValue("BOSS::screenshotCount"));
        showHelp = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showHelp"));
        showGUI = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showGUI"));
        screenshotKey = (BOSSsettings.GetValue("BOSS::screenshotKey"));
        showGUIKey = (BOSSsettings.GetValue("BOSS::showGUIKey"));
        print("Loaded BOSS settings.");
    }
}