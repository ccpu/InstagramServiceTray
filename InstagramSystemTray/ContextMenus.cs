using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using InstagramSystemTray.Properties;

namespace InstagramSystemTray
{
    /// <summary>
    /// 
    /// </summary>
    class ContextMenus
    {
        /// <summary>
        /// Is the About box displayed?
        /// </summary>
        bool _isSettingLoaded = false;

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip Create()
        {
            // Add the default menu options.
            var menu = new ContextMenuStrip();

            var item = new ToolStripMenuItem { Text = "Settings" };
            item.Click += new EventHandler(Setting_Click);
            item.Image = Resources.setting;
            menu.Items.Add(item);

            item = new ToolStripMenuItem { Text = "Logs" };
            item.Click += new EventHandler(Logs_Click);
            item.Image = Resources.log;
            menu.Items.Add(item);

            //// Separator.
            var sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            //// Exit.
            item = new ToolStripMenuItem { Text = "Exit" };
            item.Click += new System.EventHandler(Exit_Click);
            item.Image = Resources.exit;
            menu.Items.Add(item);

            return menu;
        }

        private void Logs_Click(object sender, EventArgs e)
        {
            if (File.Exists("log.txt"))
                Process.Start("log.txt");
            else
                MessageBox.Show("No log file found.");
        }

        /// <summary>
        /// Handles the Click event of the About control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Setting_Click(object sender, EventArgs e)
        {
            if (!_isSettingLoaded)
            {
                _isSettingLoaded = true;
                new SettingForm().Show();
                _isSettingLoaded = false;
            }
        }

        /// <summary>
        /// Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
        {
            // Quit without further ado.
            Application.Exit();
        }
    }
}