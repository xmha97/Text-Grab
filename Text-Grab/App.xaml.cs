﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Text_Grab.Properties;
using Text_Grab.Utilities;
using Text_Grab.Views;
using Windows.System.UserProfile;

namespace Text_Grab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static List<string> InstalledLanguages => GlobalizationPreferences.Languages.ToList();

        void appStartup(object sender, StartupEventArgs e)
        {
            // Register COM server and activator type
            DesktopNotificationManagerCompat.RegisterActivator<TextGrabNotificationActivator>();

            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "Settings")
                {
                    SettingsWindow sw = new SettingsWindow();
                    sw.Show();
                }
                if (e.Args[i] == "GrabFrame")
                {
                    GrabFrame gf = new GrabFrame();
                    gf.Show();
                }
                if (e.Args[i] == "Fullscreen")
                {
                    WindowUtilities.LaunchFullScreenGrab();
                }
                if (e.Args[i] == "EditText")
                {
                    EditTextWindow manipulateTextWindow = new EditTextWindow();
                    manipulateTextWindow.Show();
                }
                if (File.Exists(e.Args[i]))
                {
                    EditTextWindow manipulateTextWindow = new EditTextWindow();
                    manipulateTextWindow.OpenThisPath(e.Args[i]);
                    manipulateTextWindow.Show();
                }
            }

            if(e.Args.Length == 0 && Settings.Default.FirstRun == false)
            {
                switch (Settings.Default.DefaultLaunch)
                {
                    case "Fullscreen":
                        WindowUtilities.LaunchFullScreenGrab();
                        break;
                    case "GrabFrame":
                        GrabFrame gf = new GrabFrame();
                        gf.Show();
                        break;
                    case "EditText":
                        EditTextWindow manipulateTextWindow = new EditTextWindow();
                        manipulateTextWindow.Show();
                        break;
                    default:
                        EditTextWindow editTextWindow = new EditTextWindow();
                        editTextWindow.Show();
                        break;
                }
            }

            // if (true)
            if (Settings.Default.FirstRun)
            {
                FirstRunWindow frw = new FirstRunWindow();
                frw.Show();

                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }
        }

        
    }
}
