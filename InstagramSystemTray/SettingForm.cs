using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InstagramSystemTray.Helper;
using InstagramSystemTray.Models;
using InstagramSystemTray.Server;

namespace InstagramSystemTray
{
    public partial class SettingForm : Form
    {
        private Setting currentSetting;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            var set = AppSettings.GetSetting();
            lblError.Text = "";
            currentSetting = set;
            lblError.Text = AppSettings.ValidateSetting(set);
            tbPort.Text = set.WebSocketPort == 0 ? 8181.ToString() : set.WebSocketPort.ToString();
            if (set.Accounts != null)
                for (int i = 0; i < set.Accounts.Count; i++)
                {
                    if (i == 0)
                        tbAccount1.Text = set.Accounts[i];
                    if (i == 1)
                        tbAccount2.Text = set.Accounts[i];
                    if (i == 2)
                        tbAccount3.Text = set.Accounts[i];
                    if (i == 3)
                        tbAccount4.Text = set.Accounts[i];
                    if (i == 4)
                        tbAccount5.Text = set.Accounts[i];
                }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var oldSett = AppSettings.GetSetting();


            var newSetting = new Setting
            {
                WebSocketPort = Convert.ToInt32(tbPort.Text),
                Accounts = new List<string>()
            };

            if (!string.IsNullOrEmpty(tbAccount1.Text))
                newSetting.Accounts.Add(tbAccount1.Text);
            if (!string.IsNullOrEmpty(tbAccount2.Text))
                newSetting.Accounts.Add(tbAccount2.Text);
            if (!string.IsNullOrEmpty(tbAccount3.Text))
                newSetting.Accounts.Add(tbAccount3.Text);
            if (!string.IsNullOrEmpty(tbAccount4.Text))
                newSetting.Accounts.Add(tbAccount4.Text);
            if (!string.IsNullOrEmpty(tbAccount5.Text))
                newSetting.Accounts.Add(tbAccount5.Text);

            lblError.Text = AppSettings.ValidateSetting(newSetting);

            InstaApi.SetApiInstances(newSetting.Accounts);

            if (string.IsNullOrEmpty(lblError.Text))
            {
                AppSettings.SetSetting(newSetting);
                if (oldSett.WebSocketPort != newSetting.WebSocketPort)
                    ServerWebSocket.ChangePort(newSetting.WebSocketPort);

                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            AppSettings.SetSetting(currentSetting);
            Close();
        }

        private void LblError_Click(object sender, EventArgs e)
        {

        }
    }
}