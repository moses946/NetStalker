﻿using Microsoft.Win32;
using NetStalker.MainLogic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NetStalker
{
    public partial class Options : Form
    {
        #region Constructor

        public Options()
        {
            InitializeComponent();
            SetPasswordButton.Enabled = false;
        }

        #endregion

        #region Form Event Handlers

        private void Options_Load(object sender, EventArgs e)
        {
            if (IsPassSet())
            {
                PasswordField.Enabled = false;
                ConfirmPasswordField.Enabled = false;
                RemovePasswordButton.Enabled = true;
                SetPasswordButton.Enabled = false;
            }
            else
            {
                ConfirmPasswordField.Enabled = false;
                RemovePasswordButton.Enabled = false;
            }

            if (Properties.Settings.Default.Minimize == "Tray")
            {
                TrayCheck.Checked = true;
            }
            else if (Properties.Settings.Default.Minimize == "Taskbar")
            {
                TaskbarCheck.Checked = true;
            }

            if (Properties.Settings.Default.SpoofProtection)
            {
                SpoofProtectionCheck.Checked = true;
            }

            if (Properties.Settings.Default.SuppressNotifications == 1)
            {
                SuppressNotificationsCheck.Checked = true;
            }

            TokenField.Text = Properties.Settings.Default.APIToken;
        }

        private void Options_Click(object sender, EventArgs e)
        {
            StatusLabel.Focus();
        }

        #endregion

        #region Change Password Section

        private void PasswordField_TextChanged(object sender, EventArgs e)
        {
            if (PasswordField.Text.Length > 0)
            {
                StatusLabel.Text = "";
            }

            if (!string.IsNullOrWhiteSpace(PasswordField.Text) &&
                PasswordField.Text.Length >= 6)
            {
                if (ConfirmPasswordField.Enabled &&
                    !string.IsNullOrWhiteSpace(ConfirmPasswordField.Text))
                {
                    PassStatus.ForeColor = Color.Red;
                    PassStatus.Text = "Password mismatch!";
                    SetPasswordButton.Enabled = false;
                }
                else
                {
                    ConfirmPasswordField.Enabled = true;
                    PassStatus.Text = "";
                    SetPasswordButton.Enabled = false;
                }

            }
            else if (PasswordField.Text.Length < 6 && PasswordField.Text.Length >= 1)
            {
                PassStatus.ForeColor = Color.Red;
                PassStatus.Text = "At least 6 characters required!";
                ConfirmPasswordField.Text = "";
                ConfirmPasswordField.Enabled = false;
                SetPasswordButton.Enabled = false;
            }
            else
            {
                PassStatus.ForeColor = Color.Red;
                PassStatus.Text = "";
                ConfirmPasswordField.Text = "";
                ConfirmPasswordField.Enabled = false;
                SetPasswordButton.Enabled = false;
            }
        }

        private void ConfirmPasswordField_TextChanged(object sender, EventArgs e)
        {
            if (ConfirmPasswordField.Text.Length >= 6 &&
                PasswordField.Text == ConfirmPasswordField.Text)
            {
                PassStatus.ForeColor = Color.Green;
                PassStatus.Text = "Sounds Good!";
                SetPasswordButton.Enabled = true;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ConfirmPasswordField.Text))
                {
                    PassStatus.ForeColor = Color.Red;
                    PassStatus.Text = "Password mismatch!";
                    SetPasswordButton.Enabled = false;
                }
            }
        }

        private void SetPasswordButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PasswordField.Text) &&
                !string.IsNullOrWhiteSpace(ConfirmPasswordField.Text) &&
                PasswordField.Text == ConfirmPasswordField.Text)
            {
                string strText = PasswordField.Text;
                string key = "h777m777z777@netstalker.app";
                string encText = Tools.EncryptText(strText, key);

                var root = Registry.CurrentUser;
                RegistryKey reg = root.OpenSubKey("Software", true).CreateSubKey("hSmNz");
                reg.Close();
                RegistryKey reg1 = root.OpenSubKey("Software").OpenSubKey("hSmNz", true);
                reg1.SetValue("SNG", encText);
                reg1.SetValue("IsSNG", true);
                reg1.Close();
                RemovePasswordButton.Enabled = true;
                StatusLabel.ForeColor = Color.Green;
                StatusLabel.Text = "Password has been set!";
                SetPasswordButton.Enabled = false;
                PasswordField.Text = "";
                ConfirmPasswordField.Text = "";
                PassStatus.Text = "";
                PasswordField.Enabled = false;
                ConfirmPasswordField.Enabled = false;
            }
        }

        private void RemovePasswordButton_Click(object sender, EventArgs e)
        {
            var root = Registry.CurrentUser;
            RegistryKey reg1 = root.OpenSubKey("Software").OpenSubKey("hSmNz", true);
            reg1.DeleteValue("SNG");
            reg1.SetValue("IsSNG", false);
            reg1.Close();
            SetPasswordButton.Enabled = true;
            RemovePasswordButton.Enabled = false;
            StatusLabel.ForeColor = Color.Green;
            StatusLabel.Text = "Password has been removed!";
            PasswordField.Enabled = true;
        }

        public bool IsPassSet()
        {
            var root = Registry.CurrentUser;
            RegistryKey reg = root.OpenSubKey("Software").OpenSubKey("hSmNz");
            if (reg != null)
            {
                if ((string)reg.GetValue("IsSNG") == "True")
                {
                    reg.Close();
                    return true;
                }
            }

            reg.Close();
            return false;
        }

        #endregion

        #region Other Section

        private void SpoofProtectionCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (SpoofProtectionCheck.Checked)
            {
                Properties.Settings.Default.SpoofProtection = true;
            }
            else
            {
                Properties.Settings.Default.SpoofProtection = false;
            }
        }

        private void SuppressNotificationsCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SuppressNotifications = SuppressNotificationsCheck.Checked ? 1 : 2;
        }

        private void HelpBubble_MouseEnter(object sender, EventArgs e)
        {
            ToolTip.SetToolTip(HelpBubble, Properties.Resources.HelpBubble);
        }

        #endregion

        #region Style Section

        private void LightCheck_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DarkMode = false;
            StatusLabel.Text = "Restart application to apply changes";
        }

        private void DarkCheck_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DarkMode = true;
            StatusLabel.Text = "Restart application to apply changes";
        }

        #endregion

        #region App State Section

        private void TrayCheck_Click(object sender, EventArgs e)
        {
            TaskbarCheck.Enabled = false;
            Properties.Settings.Default.Minimize = "Tray";
            StatusLabel.Text = "";
            TaskbarCheck.Enabled = true;
        }

        private void TaskbarCheck_Click(object sender, EventArgs e)
        {
            TrayCheck.Enabled = false;
            Properties.Settings.Default.Minimize = "Taskbar";
            StatusLabel.Text = "";
            TrayCheck.Enabled = true;
        }

        #endregion

        #region ToolTip Handlers

        private void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 51, 51)), e.Bounds);//background color e.bounds

            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(204, 204, 204), 1), new Rectangle(e.Bounds.X, e.Bounds.Y,
                e.Bounds.Width - 1, e.Bounds.Height - 1));//the white bounds

            e.Graphics.DrawString(e.ToolTipText, new Font("Century Gothic", 9), new SolidBrush(Color.FromArgb(204, 204, 204)),
                new Point(10, 10)); //text with image location
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = new Size(e.ToolTipSize.Width + 30, e.ToolTipSize.Height);
        }

        #endregion

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.APIToken = TokenField.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void MacVendorsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(MacVendorsLinkLabel.Text);
        }

        private void ClearInfo_Click(object sender, EventArgs e)
        {
            File.Delete("DeviceInfo.json");
        }
    }
}
