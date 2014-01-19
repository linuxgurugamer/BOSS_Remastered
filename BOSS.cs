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
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using SyNUtils;
using Toolbar;
using UnityEngine;
using Color = UnityEngine.Color;
using File = KSP.IO.File;

[KSPAddon(KSPAddon.Startup.EveryScene, false)]
public class BOSS : MonoBehaviour
{

    public Vector2 scrollPosition;
    public Vessel activeVessel;
    protected Rect helpWindowPos, BurstPos, showUIPos;
    private string BossFldr = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+@"\";
    private string kspPluginDataFldr = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)  + @"\PluginData\BOSS\";
    public int screenshotCount, superSampleValueInt = 1, burstTime = 1;
    private double burstInterval = 1;
    public string superSampleValueString = "1",
        burstTimeString = "1",
        burstIntervalString = "1",
        screenshotKey = "z",
        showGUIKey = "p",
        helpContent = "",
        worked = "";

    public bool showUI = true,
        burstMode = false,
        showBurst = false,
        showHelp = true,
        unitySkin = true,
        unitySS = true;
        
    private IButton toolbarButton;
    public BOSSSettings BOSSsettings = new BOSSSettings(); 

    public void Awake()
    {
        if (!File.Exists<BOSS>(kspPluginDataFldr + "config.xml"))
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
        if (!File.Exists<BOSS>(BossFldr + "readme.txt"))
        try
        {
            using (StreamReader sr = new StreamReader(BossFldr + "readme.txt"))
            {
                 helpContent = sr.ReadToEnd();
                
            }
        }
        catch (Exception e)
        {
            print("Could not read readme.txt");
        }

        loadSettings();
        initToolbar();
        RenderingManager.AddToPostDrawQueue(60, new Callback(drawGUI));
    }

    private void initToolbar()
    {
        toolbarButton = ToolbarManager.Instance.add("BOSS", "toolbarButton");
        toolbarButton.TexturePath = showUI ? "BOSS/bon" : "BOSS/boff";
        toolbarButton.ToolTip = "Toggle Bolt-On Screenshot System";
        toolbarButton.OnClick += (e) =>
        {
            if (showUI) showUI = false;
            else if (!showUI) showUI = true;
            toolbarButton.TexturePath = showUI ? "BOSS/bon" : "BOSS/boff";
            saveSettings();
        };
    }

    private void drawGUI()
    {
        if (unitySkin) GUI.skin = null;
        else GUI.skin = HighLogic.Skin;

        if (showUI)
            showUIPos = GUILayout.Window(568, showUIPos, UIContent, "B.O.S.S. Control", GUILayout.Width(150),
                GUILayout.Height(150));

        if (showHelp)
            helpWindowPos = GUILayout.Window(570, helpWindowPos, UIContentHelp, "Help!!!", GUILayout.Width(300),
                GUILayout.Height(300));

        if (burstMode)
            BurstPos = GUILayout.Window(569, BurstPos, UIContentBurst, "Burst Control", GUILayout.Width(150),
                GUILayout.Height(150));
    }

    public void Update()
    {
        if (burstMode)
        {
            superSampleValueInt = 1;
            superSampleValueString = "1";
        }

        try
        {
            if (Input.GetKeyDown(screenshotKey))
            {
                if (burstMode)
                {
                    saveSettings();
                    loadSettings();
                    print("burst mode start");
                    fireBurstShot();
                }
                else
                {
                    saveSettings();
                    loadSettings();
                    print("Screenshot button pressed!");
                    takeScreenshot();
                }
            }
            if (Input.GetKeyDown(showGUIKey))
            {
                if (showUI) showUI = false;
                else if (!showUI) showUI = true;
                toolbarButton.TexturePath = showUI ? "BOSS/bon" : "BOSS/boff";
            }
        }
        catch (UnityException e)
        {
            if (screenshotKey != "invalid" || screenshotKey != "")
            {
                screenshotKey = "invalid";
            }
        }
    }

    private void UIContentBurst(int windowID)
    {
        GUILayout.BeginVertical();

        GUILayout.Label("Set interval in secs: " + burstInterval.ToString(), GUILayout.ExpandHeight(true),
            GUILayout.ExpandWidth(true));
        if (!double.TryParse(burstIntervalString, out burstInterval))
        {
            burstIntervalString = " ";
        }
        burstIntervalString = GUILayout.TextField(burstIntervalString);


        GUILayout.Label("Set time in secs: " + burstTime.ToString(), GUILayout.ExpandHeight(true),
            GUILayout.ExpandWidth(true));
        if (!int.TryParse(burstTimeString, out burstTime))
        {
            burstTimeString = " ";
        }
        burstTimeString = GUILayout.TextField(burstTimeString);


        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    private void UIContentHelp(int windowID)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(295), GUILayout.Height(295));
        GUILayout.Label(helpContent);
        GUILayout.EndScrollView();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
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
        GUILayout.Label("Did it work?" + worked, GUILayout.ExpandHeight(true),
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
                saveSettings();
                loadSettings();
                superSampleValueInt = 1;
                superSampleValueString = "1";

                print("burst mode shot");
                fireBurstShot();
            }
            else
            {
                saveSettings();
                loadSettings();
                print("Screenshot button pressed!");
                takeScreenshot();
            }
        }
        burstMode = GUILayout.Toggle(burstMode, "Toggle Burst", GUILayout.ExpandWidth(true));
        showHelp = GUILayout.Toggle(showHelp, "Toggle Help", GUILayout.ExpandWidth(true));
        if (unitySkin) unitySkin = GUILayout.Toggle(unitySkin, "Toggle ksp skin", GUILayout.ExpandWidth(true));
        else unitySkin = GUILayout.Toggle(unitySkin, "Toggle unity skin", GUILayout.ExpandWidth(true));
        if (unitySS) unitySS = GUILayout.Toggle(unitySS, "Toggle synik4l processing", GUILayout.ExpandWidth(true));
        else unitySS = GUILayout.Toggle(unitySS, "Toggle unity processing", GUILayout.ExpandWidth(true));


        GUILayout.Label(screenshotCount + " screenshots taken.");
        GUILayout.EndVertical();
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    public void takeScreenshot()
    {
        string screenshotFilename = "SS_" + DateTime.Today.ToString("MM-dd-yyyy") + "_" +
                                    DateTime.Now.ToString("HH-mm-ss");
        print("Screenshot Count:" + screenshotCount);
        print(kspPluginDataFldr + screenshotFilename + ".png");
        print("Your supersample value was " + superSampleValueInt + "!");
        if (unitySS)
            Application.CaptureScreenshot(kspPluginDataFldr + screenshotFilename + ".png", superSampleValueInt);
        else
        {
           
            print(kspPluginDataFldr + screenshotFilename);
            BackgroundWorker bw = new BackgroundWorker();

            bw.WorkerReportsProgress = true;

            bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    Application.CaptureScreenshot(kspPluginDataFldr + screenshotFilename + ".png", 1);
                    Thread.Sleep(5000);
                });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    showHelp = !showHelp;
                    SyNUtils.AntiAliaser process = new AntiAliaser();
                    worked = process.processImage(kspPluginDataFldr + screenshotFilename, superSampleValueInt);
                    System.IO.File.Delete(kspPluginDataFldr + screenshotFilename + "2.png");
                    print("synik4l Screenshot processing successful!");
                });

            bw.RunWorkerAsync();

           
           
            
        }
        screenshotCount++;
        saveSettings();
    }

    public void fireBurstShot()
    {
        int bursts = burstTime;
        int interval = Convert.ToInt32(burstInterval*1000);
        BackgroundWorker bw = new BackgroundWorker();

        bw.WorkerReportsProgress = true;

        bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;


                for (; bursts > 0; bursts--)
                {
                    takeScreenshot();
                    Thread.Sleep(interval);
                }
            });

        // what to do when worker completes its task (notify the user)
        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args) { burstMode = !burstMode; });

        bw.RunWorkerAsync();
    }

    private void createSettings()
    {
        BOSSsettings.SetValue("BOSS::BurstPos.x", "250");
        BOSSsettings.SetValue("BOSS::BurstPos.y", "250");
        BOSSsettings.SetValue("BOSS::helpWindowPos.x", "600");
        BOSSsettings.SetValue("BOSS::helpWindowPos.y", "500");
        BOSSsettings.SetValue("BOSS::showUIPos.x", "400");
        BOSSsettings.SetValue("BOSS::showUIPos.y", "400");
        BOSSsettings.SetValue("BOSS::screenshotCount", "0");
        BOSSsettings.SetValue("BOSS::showUI", "True");
        BOSSsettings.SetValue("BOSS::screenshotKey", "z");
        BOSSsettings.SetValue("BOSS::showGUIKey", "p");
        BOSSsettings.SetValue("BOSS::supersampValue", "1");
        BOSSsettings.SetValue("BOSS::burstTime", "1");
        BOSSsettings.SetValue("BOSS::burstInterval", "1");
        BOSSsettings.SetValue("BOSS::showBurst", "False");
        BOSSsettings.Save();
        print("Created BOSS settings.");
    }

    private void saveSettings()
    {
        BOSSsettings.SetValue("BOSS::BurstPos.x", BurstPos.x.ToString());
        BOSSsettings.SetValue("BOSS::BurstPos.y", BurstPos.y.ToString());
        BOSSsettings.SetValue("BOSS::helpWindowPos.x", helpWindowPos.x.ToString());
        BOSSsettings.SetValue("BOSS::helpWindowPos.y", helpWindowPos.y.ToString());
        BOSSsettings.SetValue("BOSS::showUIPos.x", showUIPos.x.ToString());
        BOSSsettings.SetValue("BOSS::showUIPos.y", showUIPos.y.ToString());
        BOSSsettings.SetValue("BOSS::screenshotCount", screenshotCount.ToString());
        BOSSsettings.SetValue("BOSS::showUI", showUI.ToString());
        BOSSsettings.SetValue("BOSS::screenshotKey", screenshotKey);
        BOSSsettings.SetValue("BOSS::showGUIKey", showGUIKey);
        BOSSsettings.SetValue("BOSS::supersampValue", superSampleValueString);
        BOSSsettings.SetValue("BOSS::burstTime", burstTime.ToString());
        BOSSsettings.SetValue("BOSS::burstInterval", burstInterval.ToString());
        BOSSsettings.SetValue("BOSS::showBurst", burstMode.ToString());
        BOSSsettings.Save();
        print("Saved BOSS settings.");
    }

    private void loadSettings()
    {
        BOSSsettings.Load();
        BurstPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::BurstPos.x"));
        BurstPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::BurstPos.y"));
        helpWindowPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.x"));
        helpWindowPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.y"));
        showUIPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::showUIPos.x"));
        showUIPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::showUIPos.y"));
        screenshotCount = Convert.ToInt32(BOSSsettings.GetValue("BOSS::screenshotCount"));
        showUI = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showUI"));
        screenshotKey = (BOSSsettings.GetValue("BOSS::screenshotKey"));
        showGUIKey = (BOSSsettings.GetValue("BOSS::showGUIKey"));
        superSampleValueString = (BOSSsettings.GetValue("BOSS::supersampValue"));
        burstTime = Convert.ToInt32(BOSSsettings.GetValue("BOSS::burstTime"));
        burstInterval = Convert.ToDouble(BOSSsettings.GetValue("BOSS::burstInterval"));
        burstMode = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showBurst"));
        print("Loaded BOSS settings.");
    }
}