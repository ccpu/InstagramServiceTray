using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace InstagramSystemTray
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location))
                    .Count() > 1)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

//
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //bypass "Culture ID 4096 (0x1000) is a neutral culture" error when loading setting form from context menu
            //  CountryList.SetCountryList();

            // Show the system tray icon.					
            using (var pi = new Main())
            {
                pi.Init();

                // Make sure the application runs!
                Application.Run();
            }
        }
    }
}