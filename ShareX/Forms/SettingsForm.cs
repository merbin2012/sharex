﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2008-2013 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using HelpersLib;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using UploadersLib;

namespace ShareX
{
    public partial class SettingsForm : Form
    {
        private bool loaded;
        private const int MaxBufferSizePower = 14;
        private ContextMenuStrip cmsSaveImageSubFolderPattern;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();

            loaded = true;
        }

        private void LoadSettings()
        {
            Text = Program.Title + " - Settings";
            Icon = Resources.ShareXIcon;

            // General
            cbShowTray.Checked = Program.Settings.ShowTray;
            cbStartWithWindows.Checked = ShortcutHelper.CheckShortcut(Environment.SpecialFolder.Startup); //RegistryHelper.CheckStartWithWindows();
            cbSendToMenu.Checked = ShortcutHelper.CheckShortcut(Environment.SpecialFolder.SendTo);
            cbShellContextMenu.Checked = RegistryHelper.CheckShellContextMenu();
            cbCheckUpdates.Checked = Program.Settings.AutoCheckUpdate;
            cbShowAfterCaptureTasksForm.Checked = Program.Settings.ShowAfterCaptureTasksForm;
            cbPlaySoundAfterCapture.Checked = Program.Settings.PlaySoundAfterCapture;
            cbPlaySoundAfterUpload.Checked = Program.Settings.PlaySoundAfterUpload;
            cbTrayBalloonTipAfterUpload.Checked = Program.Settings.TrayBalloonTipAfterUpload;
            cbHistorySave.Checked = Program.Settings.SaveHistory;

            // Paths
            cbUseCustomUploadersConfigPath.Checked = Program.Settings.UseCustomUploadersConfigPath;
            txtCustomUploadersConfigPath.Text = Program.Settings.CustomUploadersConfigPath;
            cbUseCustomHistoryPath.Checked = Program.Settings.UseCustomHistoryPath;
            txtCustomHistoryPath.Text = Program.Settings.CustomHistoryPath;
            cbUseCustomScreenshotsPath.Checked = Program.Settings.UseCustomScreenshotsPath;
            txtCustomScreenshotsPath.Text = Program.Settings.CustomScreenshotsPath;
            txtSaveImageSubFolderPattern.Text = Program.Settings.SaveImageSubFolderPattern;
            cmsSaveImageSubFolderPattern = NameParser.CreateCodesMenu(txtSaveImageSubFolderPattern, ReplacementVariables.n);

            // Proxy
            cbProxyMethod.Items.AddRange(Enum.GetNames(typeof(ProxyMethod)));
            cbProxyType.Items.AddRange(Helpers.GetEnumDescriptions<ProxyType>());
            cbProxyMethod.SelectedIndex = (int)Program.Settings.ProxySettings.ProxyMethod;
            txtProxyUsername.Text = Program.Settings.ProxySettings.Username;
            txtProxyPassword.Text = Program.Settings.ProxySettings.Password;
            txtProxyHost.Text = Program.Settings.ProxySettings.Host ?? string.Empty;
            nudProxyPort.Value = Program.Settings.ProxySettings.Port;
            cbProxyType.SelectedIndex = (int)Program.Settings.ProxySettings.ProxyType;
            UpdateProxyControls();

            // Upload / General
            nudUploadLimit.Value = Program.Settings.UploadLimit;

            for (int i = 0; i < MaxBufferSizePower; i++)
            {
                cbBufferSize.Items.Add(Math.Pow(2, i).ToString("N0") + " KiB");
            }

            cbBufferSize.SelectedIndex = Program.Settings.BufferSizePower.Between(0, MaxBufferSizePower);
            cbIfUploadFailRetryOnce.Checked = Program.Settings.IfUploadFailRetryOnce;

            // Upload / Clipboard formats
            foreach (ClipboardFormat cf in Program.Settings.ClipboardContentFormats)
            {
                AddClipboardFormat(cf);
            }

            // Upload / Watch folder
            cbWatchFolderEnabled.Checked = Program.Settings.WatchFolderEnabled;

            if (Program.Settings.WatchFolderList == null)
            {
                Program.Settings.WatchFolderList = new List<WatchFolder>();
            }
            else
            {
                foreach (WatchFolder watchFolder in Program.Settings.WatchFolderList)
                {
                    AddWatchFolder(watchFolder);
                }
            }
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            BringToFront();
            Activate();
        }

        private void SettingsForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void UpdateProxyControls()
        {
            switch (Program.Settings.ProxySettings.ProxyMethod)
            {
                case ProxyMethod.None:
                    txtProxyUsername.Enabled = txtProxyPassword.Enabled = txtProxyHost.Enabled = nudProxyPort.Enabled = cbProxyType.Enabled = false;
                    break;
                case ProxyMethod.Manual:
                    txtProxyUsername.Enabled = txtProxyPassword.Enabled = txtProxyHost.Enabled = nudProxyPort.Enabled = cbProxyType.Enabled = true;
                    break;
                case ProxyMethod.Automatic:
                    txtProxyUsername.Enabled = txtProxyPassword.Enabled = true;
                    txtProxyHost.Enabled = nudProxyPort.Enabled = cbProxyType.Enabled = false;
                    break;
            }
        }

        #region General

        private void cbShowTray_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ShowTray = cbShowTray.Checked;

            if (loaded)
            {
                Program.MainForm.niTray.Visible = Program.Settings.ShowTray;
            }
        }

        private void cbStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                //RegistryHelper.SetStartWithWindows(cbStartWithWindows.Checked);
                ShortcutHelper.SetShortcut(cbStartWithWindows.Checked, Environment.SpecialFolder.Startup, "-silent");
            }
        }

        private void cbSendToMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ShortcutHelper.SetShortcut(cbSendToMenu.Checked, Environment.SpecialFolder.SendTo);
            }
        }

        private void cbShellContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                RegistryHelper.SetShellContextMenu(cbShellContextMenu.Checked);
            }
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoCheckUpdate = cbCheckUpdates.Checked;
        }

        private void cbShowAfterCaptureTasksForm_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ShowAfterCaptureTasksForm = cbShowAfterCaptureTasksForm.Checked;
        }

        private void cbPlaySoundAfterCapture_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.PlaySoundAfterCapture = cbPlaySoundAfterCapture.Checked;
        }

        private void cbPlaySoundAfterUpload_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.PlaySoundAfterUpload = cbPlaySoundAfterUpload.Checked;
        }

        private void cbTrayBalloonTipAfterUpload_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.TrayBalloonTipAfterUpload = cbTrayBalloonTipAfterUpload.Checked;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SaveHistory = cbHistorySave.Checked;
        }

        #endregion General

        #region Paths

        private void btnOpenPersonalFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Program.PersonalPath) && Directory.Exists(Program.PersonalPath))
            {
                Process.Start(Program.PersonalPath);
            }
        }

        private void cbUseCustomUploadersConfigPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomUploadersConfigPath = cbUseCustomUploadersConfigPath.Checked;
        }

        private void txtCustomUploadersConfigPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
        }

        private void btnBrowseCustomUploadersConfigPath_Click(object sender, EventArgs e)
        {
            Helpers.BrowseFile("ShareX - Choose uploaders config file path", txtCustomUploadersConfigPath, Program.PersonalPath);
            Program.Settings.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
            Program.LoadUploadersConfig();
        }

        private void btnLoadUploadersConfig_Click(object sender, EventArgs e)
        {
            Program.LoadUploadersConfig();
        }

        private void cbUseCustomHistoryPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomHistoryPath = cbUseCustomHistoryPath.Checked;
        }

        private void txtCustomHistoryPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomHistoryPath = txtCustomHistoryPath.Text;
        }

        private void btnBrowseCustomHistoryPath_Click(object sender, EventArgs e)
        {
            Helpers.BrowseFile("ShareX - Choose history file path", txtCustomHistoryPath, Program.PersonalPath);
        }

        private void cbUseCustomScreenshotsPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomScreenshotsPath = cbUseCustomScreenshotsPath.Checked;
            lblSaveImageSubFolderPatternPreview.Text = Program.ScreenshotsPath;
        }

        private void txtCustomScreenshotsPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomScreenshotsPath = txtCustomScreenshotsPath.Text;
            lblSaveImageSubFolderPatternPreview.Text = Program.ScreenshotsPath;
        }

        private void btnBrowseCustomScreenshotsPath_Click(object sender, EventArgs e)
        {
            Helpers.BrowseFolder("Choose screenshots folder path", txtCustomScreenshotsPath, Program.PersonalPath);
        }

        private void txtSaveImageSubFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.SaveImageSubFolderPattern = txtSaveImageSubFolderPattern.Text;
            lblSaveImageSubFolderPatternPreview.Text = Program.ScreenshotsPath;
        }

        #endregion Paths

        #region Proxy

        private void cbProxyMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ProxySettings.ProxyMethod = (ProxyMethod)cbProxyMethod.SelectedIndex;

            if (Program.Settings.ProxySettings.ProxyMethod == ProxyMethod.Automatic)
            {
                Program.Settings.ProxySettings.IsValidProxy();
                txtProxyHost.Text = Program.Settings.ProxySettings.Host ?? string.Empty;
                nudProxyPort.Value = Program.Settings.ProxySettings.Port;
                cbProxyType.SelectedIndex = (int)Program.Settings.ProxySettings.ProxyType;
            }

            UpdateProxyControls();
        }

        private void txtProxyUsername_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.ProxySettings.Username = txtProxyUsername.Text;
        }

        private void txtProxyPassword_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.ProxySettings.Password = txtProxyPassword.Text;
        }

        private void txtProxyHost_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.ProxySettings.Host = txtProxyHost.Text;
        }

        private void nudProxyPort_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ProxySettings.Port = (int)nudProxyPort.Value;
        }

        private void cboProxyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ProxySettings.ProxyType = (ProxyType)cbProxyType.SelectedIndex;
        }

        #endregion Proxy

        #region Upload / Watch folder

        private void btnWatchFolderAdd_Click(object sender, EventArgs e)
        {
            using (WatchFolderForm form = new WatchFolderForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    WatchFolder watchFolder = form.WatchFolder;
                    watchFolder.FileWatcherTrigger += path => UploadManager.UploadFile(path, Program.DefaultTaskSettings);
                    Program.Settings.WatchFolderList.Add(watchFolder);
                    AddWatchFolder(watchFolder);

                    if (Program.Settings.WatchFolderEnabled)
                    {
                        watchFolder.Enable();
                    }
                }
            }
        }

        private void AddWatchFolder(WatchFolder watchFolder)
        {
            ListViewItem lvi = new ListViewItem(watchFolder.FolderPath ?? "");
            lvi.Tag = watchFolder;
            lvi.SubItems.Add(watchFolder.Filter ?? "");
            lvi.SubItems.Add(watchFolder.IncludeSubdirectories.ToString());
            lvWatchFolderList.Items.Add(lvi);
        }

        private void btnWatchFolderRemove_Click(object sender, EventArgs e)
        {
            if (lvWatchFolderList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvWatchFolderList.SelectedItems[0];
                WatchFolder watchFolder = lvi.Tag as WatchFolder;

                Program.Settings.WatchFolderList.Remove(watchFolder);
                lvWatchFolderList.Items.Remove(lvi);
                watchFolder.Dispose();
            }
        }

        private void cbWatchFolderEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                Program.Settings.WatchFolderEnabled = cbWatchFolderEnabled.Checked;

                foreach (WatchFolder watchFolder in Program.Settings.WatchFolderList)
                {
                    if (Program.Settings.WatchFolderEnabled)
                    {
                        watchFolder.Enable();
                    }
                    else
                    {
                        watchFolder.Dispose();
                    }
                }
            }
        }

        #endregion Upload / Watch folder

        #region Upload / Clipboard

        private void AddClipboardFormat(ClipboardFormat cf)
        {
            ListViewItem lvi = new ListViewItem(cf.Description ?? "");
            lvi.Tag = cf;
            lvi.SubItems.Add(cf.Format ?? "");
            lvClipboardFormats.Items.Add(lvi);
        }

        private void lvClipboardFormats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lvClipboardFormats.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvClipboardFormats.SelectedItems[0];
                ClipboardFormat cf = lvi.Tag as ClipboardFormat;
                using (ClipboardFormatForm form = new ClipboardFormatForm(cf))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        lvi.Text = form.ClipboardFormat.Description ?? "";
                        lvi.Tag = form.ClipboardFormat;
                        lvi.SubItems[1].Text = form.ClipboardFormat.Format ?? "";
                    }
                }
            }
        }

        private void btnAddClipboardFormat_Click(object sender, EventArgs e)
        {
            using (ClipboardFormatForm form = new ClipboardFormatForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ClipboardFormat cf = form.ClipboardFormat;
                    Program.Settings.ClipboardContentFormats.Add(cf);
                    AddClipboardFormat(cf);
                }
            }
        }

        private void btnClipboardFormatRemove_Click(object sender, EventArgs e)
        {
            if (lvClipboardFormats.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvClipboardFormats.SelectedItems[0];
                ClipboardFormat cf = lvi.Tag as ClipboardFormat;
                Program.Settings.ClipboardContentFormats.Remove(cf);
                lvClipboardFormats.Items.Remove(lvi);
            }
        }

        #endregion Upload / Clipboard
    }
}