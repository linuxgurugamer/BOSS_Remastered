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
/*
 * Plugin Owner - Ted
 * Contributors - Ted/SyNik4l
 * Last Update - 1/21/2014
 * Contact: ted@squad.com.mx/synik4l@gmail.com
 * Forum Thread: http://forum.kerbalspaceprogram.com/threads/34631-0-23-Bolt-On-Screenshot-System-(BOSS)-v2-1-2
*/

using System;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
//using Toolbar;
using UnityEngine;
//using File = KSP.IO.File;
using KSP.UI.Screens;


namespace BOSS
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, true)]
    public class BOSS : MonoBehaviour
    {
        //Using this method to get the boss folder gives you a cleaner path to it. As the KSPUtils class gets paths that like this C:\KSP_Root\ksp_data\..\
        private readonly string kspPluginDataFldr =
            KSPUtil.ApplicationRootPath + "GameData/BOSS/PluginData/";
        private readonly string screenshotDir = KSPUtil.ApplicationRootPath + "Screenshots/";

        //            System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/PluginData/";

        public BOSSSettings BOSSsettings = new BOSSSettings();
        protected Rect BurstPos, helpWindowPos, showUIPos;
        public Vessel activeVessel;
        public Vector2 scrollPosition;
        public int screenshotCount, burstTime = 1, superSampleValueInt = 1;
        private double burstInterval = 1;
        private bool useToolbarIfAvailable = false;
        private IButton toolbarButton;
        private ApplicationLauncherButton AppLauncherButton;

        Texture2D bon_38, boff_38;

        public bool showBurst = false,
            showHelp = false,
            showUI = false,
            unitySkin = true,
            buttonsEnabled = true,
            overrideLimiter = false,
            showFullUI = false,
            invalidkey = false,
            uiSaveDelay = false;

        public string burstTimeString = "1",
            helpContent = "",
            screenshotKey = "z",
            burstIntervalString = "1",
            showGUIKey = "p",
            superSampleValueString = "1";

        void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void Awake()
        {
            if (!File.Exists(kspPluginDataFldr + "config.xml"))
            {
                try
                {
                    //createSettings();
                }
                catch
                {
                    throw new AccessViolationException("Can't create settings file, please confirm directory is writeable.");
                }
            }
            helpContent = KSPConstants.getHelpText();
            loadSettings();
            // initToolbar();
            // RenderingManager.AddToPostDrawQueue(60, drawGUI);
        }

        void LateUpdate()
        {
            if (toolbarButton == null && AppLauncherButton == null)
                initToolbar();
        }

        private void initToolbar()
        {
            //bon = GameDatabase.Instance.GetTexture("BOSS/Resources/bon", false);
            //boff = GameDatabase.Instance.GetTexture("BOSS/Resources/boff", false);
            if (bon_38 == null)
                bon_38 = GameDatabase.Instance.GetTexture("BOSS/Resources/bon_38", false);
            if (boff_38 == null)
                boff_38 = GameDatabase.Instance.GetTexture("BOSS/Resources/boff_38", false);

            if (ToolbarManager.ToolbarAvailable && useToolbarIfAvailable)
            {
                if (toolbarButton == null)
                {
                    toolbarButton = ToolbarManager.Instance.add("BOSS", "toolbarButton");
                    toolbarButton.Text = "BOSS";
                    toolbarButton.Visible = true;

                    if (showFullUI)
                        toolbarButton.TexturePath = "BOSS/Resources/bon";
                    else
                        toolbarButton.TexturePath = "BOSS/Resources/boff";

                    toolbarButton.ToolTip = "Toggle Bolt-On Screenshot System";
                    toolbarButton.OnClick += e =>
                    {
                        if (showFullUI)
                        {
                            showFullUI = false;
                            showUI = false;
                        }
                        else if (!showFullUI)
                        {
                            showUI = true;
                            showFullUI = true;
                        }
                        if (showFullUI)
                            toolbarButton.TexturePath = "BOSS/Resources/bon";
                        else
                            toolbarButton.TexturePath = "BOSS/Resources/boff";
                        saveSettings();
                    };
                }
                if (this.AppLauncherButton != null)
                {
                    ApplicationLauncher.Instance.RemoveModApplication(this.AppLauncherButton);
                    this.AppLauncherButton = null;
                }

            }
            else
            {
                if (this.AppLauncherButton == null)
                {
                    if (ApplicationLauncher.Instance != null)
                    {
                        Texture2D t;
                        if (showFullUI)
                            t = bon_38;
                        else
                            t = boff_38;
                        this.AppLauncherButton = ApplicationLauncher.Instance.AddModApplication(
                            this.ToggleMainWindow, this.ToggleMainWindow,
                            null, null,
                            null, null,
                            ApplicationLauncher.AppScenes.ALWAYS,
                            t
                        );

                      
                    }
                }
                if (this.toolbarButton != null)
                {
                    this.toolbarButton.Destroy();
                    this.toolbarButton = null;
                }

            }
        }
        void ToggleMainWindow()
        {
            showFullUI = !showFullUI;
            showUI = !showUI;
            if (showUI)
                this.AppLauncherButton.SetTexture(bon_38);
            else
                this.AppLauncherButton.SetTexture(boff_38);
        }

        //private void drawGUI()
        void OnGUI()
        {
            if (unitySkin) GUI.skin = null;
            else GUI.skin = HighLogic.Skin;
            if (showFullUI)
            {
                if (showUI)
                    showUIPos = GUILayout.Window(568, showUIPos, UIContent, "B.O.S.S. Control", GUILayout.Width(150),
                        GUILayout.Height(150));

                if (showHelp)
                    helpWindowPos = GUILayout.Window(570, helpWindowPos, UIContentHelp, "Help!!!", GUILayout.Width(400),
                        GUILayout.Height(400));

                if (showBurst)
                    BurstPos = GUILayout.Window(569, BurstPos, UIContentBurst, "Burst Control", GUILayout.Width(150),
                        GUILayout.Height(150));
            }
        }

        public void Update()
        {
            if (showBurst) //Set sampling to one always whenver burst mode is on to prevent crashing.
            {
                superSampleValueInt = 1;
                superSampleValueString = "1";
            }
            try
            {
                if (buttonsEnabled && Input.GetKeyDown(screenshotKey))
                {
                    if (showBurst)
                    {
                        print("burst mode start");
                        fireBurstShot();
                    }
                    else
                    {
                        print("Screenshot button pressed!");
                        takeScreenshot();
                    }
                }
                if (Input.GetKeyDown(showGUIKey))
                {
                    showFullUI = !showFullUI;
                    showUI = !showUI;
                    toolbarButton.TexturePath = showFullUI ? "BOSS/bon" : "BOSS/boff";
                    saveSettings();
                }
            }
            catch (UnityException e)
            //Catches the unity exception for a keycode that isnt a valid key. Updating the UI to let the user know.
            {
                if (!invalidkey)
                    invalidkey = true;
            }
        }

        private void UIContentBurst(int windowID)
        {
            if (Event.current.type == EventType.KeyDown && GUI.GetNameOfFocusedControl() == "burstinterval" &&
                Event.current.keyCode != KeyCode.Backspace ||
                Event.current.type == EventType.keyDown && GUI.GetNameOfFocusedControl() == "bursttime" &&
                Event.current.keyCode != KeyCode.Backspace)
                saveSettings();


            GUILayout.BeginVertical();

            GUILayout.Label("Set interval in secs: " + burstInterval, GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true));
            if (!double.TryParse(burstIntervalString, out burstInterval)) burstIntervalString = " ";
            GUI.SetNextControlName("burstinterval");
            burstIntervalString = GUILayout.TextField(burstIntervalString);

            GUILayout.Label("Set time in secs: " + burstTime, GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true));
            if (!int.TryParse(burstTimeString, out burstTime)) burstTimeString = " ";
            GUI.SetNextControlName("bursttime");
            burstTimeString = GUILayout.TextField(burstTimeString);

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private void UIContentHelp(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(395), GUILayout.Height(395));
            GUILayout.Label(helpContent);
            GUILayout.EndScrollView();

            GUILayout.BeginVertical();
            GUILayout.Space(8);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            if (unitySkin) unitySkin = GUILayout.Toggle(unitySkin, "Toggle ksp skin", GUILayout.ExpandWidth(true));
            else unitySkin = GUILayout.Toggle(unitySkin, "Toggle unity skin", GUILayout.ExpandWidth(true));
            if (Event.current.type == EventType.Repaint &&
                GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && !uiSaveDelay)
            {
                uiSaveDelay = true;
                UISave();
            }
            GUILayout.Space(75);
            overrideLimiter = GUILayout.Toggle(overrideLimiter, "Override limiter", GUILayout.ExpandWidth(true));
            if (Event.current.type == EventType.Repaint &&
                GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && !uiSaveDelay)
            {
                uiSaveDelay = true;
                UISave();
            }

            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }

        private void UIContent(int windowID)
        {
            var mainGUI = new GUIStyle(GUI.skin.button);
            mainGUI.normal.textColor = mainGUI.focused.textColor = Color.white;
            mainGUI.margin = new RectOffset(12, 12, 8, 0);
            mainGUI.hover.textColor = mainGUI.active.textColor = Color.yellow;
            mainGUI.onNormal.textColor =
                mainGUI.onFocused.textColor = mainGUI.onHover.textColor = mainGUI.onActive.textColor = Color.green;
            mainGUI.padding = new RectOffset(8, 8, 8, 8);

            if (Event.current.type == EventType.KeyDown && GUI.GetNameOfFocusedControl() == "superSampleVal" &&
                Event.current.keyCode != KeyCode.Backspace ||
                Event.current.type == EventType.keyDown && GUI.GetNameOfFocusedControl() == "currentkey" &&
                Event.current.keyCode != KeyCode.Backspace)
            {
                invalidkey = false;
                uiSaveDelay = true;
                UISave();
            }


            GUILayout.BeginVertical();

            GUILayout.Label("Current screenshot key: ", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            GUI.SetNextControlName("currentkey");
            screenshotKey = GUILayout.TextField(screenshotKey);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            if (invalidkey)
            {
                GUILayout.Label("INVALID KEY!", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Supersample value: ");

            if (!int.TryParse(superSampleValueString, out superSampleValueInt)) superSampleValueString = " ";


            GUI.SetNextControlName("superSampleVal");
            superSampleValueString = GUILayout.TextField(superSampleValueString);
            if (GUILayout.Button("Take Screenshot", mainGUI, GUILayout.Width(125)))
            {
                if (buttonsEnabled)
                {
                    if (showBurst)
                    {
                        superSampleValueInt = 1;
                        superSampleValueString = "1";

                        print("burst mode shot");
                        fireBurstShot();
                    }
                    else
                    {
                        print("Screenshot button pressed!");
                        takeScreenshot();
                    }
                }
            }

            bool newshowBurst = GUILayout.Toggle(showBurst, "Toggle Burst", GUILayout.ExpandWidth(true));
            if (newshowBurst != showBurst)
                //&& Event.current.type == EventType.Repaint &&
                //GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && !uiSaveDelay)
            {
                showBurst = newshowBurst;
                uiSaveDelay = true;
                UISave();
            }

            bool newshowHelp = GUILayout.Toggle(showHelp, "Toggle Help", GUILayout.ExpandWidth(true));
            if (newshowHelp != showHelp)
                //&& Event.current.type == EventType.Repaint &&
                //GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && !uiSaveDelay)
            {
                showHelp = newshowHelp;
                uiSaveDelay = true;
                UISave();
            }


            bool newuseToolbarIfAvailable = GUILayout.Toggle(useToolbarIfAvailable, "Use Toolbar", GUILayout.ExpandWidth(true));
            if (newuseToolbarIfAvailable != useToolbarIfAvailable)
                //&& Event.current.type == EventType.Repaint &&
                //GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && !uiSaveDelay)
            {
                useToolbarIfAvailable = newuseToolbarIfAvailable;
                uiSaveDelay = true;
                UISave();
                initToolbar();
            }



            GUILayout.Label(screenshotCount + " screenshots taken.");

            if (!buttonsEnabled)
                GUILayout.Label("Currently supersampling!\n      Buttons locked!", GUILayout.Width(145),
                    GUILayout.ExpandHeight(true));
            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        public void takeScreenshot()
        {
            saveSettings();
            string screenshotFilename = "SS_" + DateTime.Today.ToString("MM-dd-yyyy") + "_" +
                                        DateTime.Now.ToString("HH-mm-ss");
            print("Screenshot Count:" + screenshotCount);
            print(screenshotDir + screenshotFilename + ".png");
            print("Your supersample value was " + superSampleValueInt + "!");
            Application.CaptureScreenshot(screenshotDir + screenshotFilename + ".png", superSampleValueInt);
            if (superSampleValueInt > 1 && overrideLimiter != true)
            {
                buttonsEnabled = false;
                checkforPicture(screenshotDir + screenshotFilename + ".png");
            }
            screenshotCount++;
            saveSettings();
        }


        public void UISave()
        {
            saveSettings();
            uiSaveDelay = false;
            return;

            var bw = new BackgroundWorker();

            bw.WorkerReportsProgress = true;

            bw.DoWork += delegate (object o, DoWorkEventArgs args)
            {
                var b = o as BackgroundWorker;
                Thread.Sleep(4000);
                saveSettings();
                bw.ReportProgress(100);
            };

            bw.RunWorkerCompleted += delegate { uiSaveDelay = false; };

            bw.RunWorkerAsync();
        }


        public void checkforPicture(string shotname)
        {
            /*This method checks for the image to appear inside the ss folder.
            Thus not allowing the user to take multiple shots in a row crashing their game.*/

            var bw = new BackgroundWorker();

            bw.DoWork += delegate (object o, DoWorkEventArgs args)
            {
                bool exists = false;
                var b = o as BackgroundWorker;
                while (exists == false)
                {
                    Thread.Sleep(500);
                    if (File.Exists(screenshotDir + shotname))
                    {
                        exists = true;
                        buttonsEnabled = true;
                    }
                }
            };
            bw.RunWorkerAsync();
        }

        public void fireBurstShot()
        {
            //Sets up background worker for burst fire mode. Takes the screenshots in seperate thread from the UI thread.
            int bursts = burstTime;
            int interval = Convert.ToInt32(burstInterval * 1000);
            var bw = new BackgroundWorker();

            bw.WorkerReportsProgress = true;

            bw.DoWork += delegate (object o, DoWorkEventArgs args)
            {
                var b = o as BackgroundWorker;

                for (; bursts > 0; bursts--)
                {
                    takeScreenshot();
                    Thread.Sleep(interval);
                }
            };

            bw.RunWorkerCompleted += delegate { showBurst = !showBurst; };

            bw.RunWorkerAsync();
        }

        private void createSettings()
        {
            BOSSsettings.Create();
            BOSSsettings.SetValue("BOSS::BurstPos.x", "250");
            BOSSsettings.SetValue("BOSS::BurstPos.y", "250");
            BOSSsettings.SetValue("BOSS::helpWindowPos.x", "600");
            BOSSsettings.SetValue("BOSS::helpWindowPos.y", "500");
            BOSSsettings.SetValue("BOSS::showUIPos.x", "400");
            BOSSsettings.SetValue("BOSS::showUIPos.y", "400");
            BOSSsettings.SetValue("BOSS::screenshotCount", "0");

            BOSSsettings.SetValue("BOSS::showFullUI", "False");
            BOSSsettings.SetValue("BOSS::showUI", "False");
            BOSSsettings.SetValue("BOSS::showBurst", "False");
            BOSSsettings.SetValue("BOSS::showHelp", "False");

            BOSSsettings.SetValue("BOSS::unitySkin", "true");
            BOSSsettings.SetValue("BOSS::overrideLimiter", "false");

            BOSSsettings.SetValue("BOSS::screenshotKey", "z");
            BOSSsettings.SetValue("BOSS::showGUIKey", "p");
            BOSSsettings.SetValue("BOSS::supersampValue", "1");
            BOSSsettings.SetValue("BOSS::burstTime", "1");
            BOSSsettings.SetValue("BOSS::burstInterval", "1");

            BOSSsettings.SetValue("BOSS::useToolbarIfAvailable", "False");

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

            BOSSsettings.SetValue("BOSS::showFullUI", showFullUI.ToString());
            BOSSsettings.SetValue("BOSS::showUI", showUI.ToString());
            BOSSsettings.SetValue("BOSS::showBurst", showBurst.ToString());
            BOSSsettings.SetValue("BOSS::showHelp", showHelp.ToString());

            BOSSsettings.SetValue("BOSS::unitySkin", unitySkin.ToString());
            BOSSsettings.SetValue("BOSS::overrideLimiter", overrideLimiter.ToString());


            BOSSsettings.SetValue("BOSS::screenshotKey", screenshotKey);
            BOSSsettings.SetValue("BOSS::showGUIKey", showGUIKey);
            BOSSsettings.SetValue("BOSS::supersampValue", superSampleValueString);
            BOSSsettings.SetValue("BOSS::burstTime", burstTime.ToString());
            BOSSsettings.SetValue("BOSS::burstInterval", burstInterval.ToString());

            BOSSsettings.SetValue("BOSS::useToolbarIfAvailable", useToolbarIfAvailable.ToString());
            

            BOSSsettings.Save();
            print("Saved BOSS settings.");
        }

        private void loadSettings()
        {
            if (BOSSsettings.Load() == false)
                createSettings();
            BurstPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::BurstPos.x"));
            BurstPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSSp::BurstPos.y"));
            helpWindowPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.x"));
            helpWindowPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::helpWindowPos.y"));
            showUIPos.x = Convert.ToSingle(BOSSsettings.GetValue("BOSS::showUIPos.x"));
            showUIPos.y = Convert.ToSingle(BOSSsettings.GetValue("BOSS::showUIPos.y"));
            screenshotCount = Convert.ToInt32(BOSSsettings.GetValue("BOSS::screenshotCount"));

            showFullUI = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showFullUI"));
            showUI = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showUI"));
            showBurst = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showBurst"));
            showHelp = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::showHelp"));

            overrideLimiter = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::overrideLimiter"));
            unitySkin = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::unitySkin"));


            screenshotKey = (BOSSsettings.GetValue("BOSS::screenshotKey"));
            showGUIKey = (BOSSsettings.GetValue("BOSS::showGUIKey"));
            superSampleValueString = (BOSSsettings.GetValue("BOSS::supersampValue"));
            burstTime = Convert.ToInt32(BOSSsettings.GetValue("BOSS::burstTime"));
            burstInterval = Convert.ToDouble(BOSSsettings.GetValue("BOSS::burstInterval"));

            useToolbarIfAvailable = Convert.ToBoolean(BOSSsettings.GetValue("BOSS::useToolbarIfAvailable"));
            
            print("Loaded BOSS settings.");
        }
    }
}