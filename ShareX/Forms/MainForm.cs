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
using HistoryLib;
using ScreenCapture;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UpdateCheckerLib;
using UploadersLib;

namespace ShareX
{
    public partial class MainForm : HotkeyForm
    {
        public bool IsReady { get; private set; }

        private bool forceClose;
        private UploadInfoManager uim;

        public MainForm()
        {
            InitControls();
            UpdateControls();
        }

        private void AfterLoadJobs()
        {
            LoadSettings();
            InitHotkeys();

#if !DEBUG
            if (Program.Settings.AutoCheckUpdate)
            {
                Thread updateThread = new Thread(CheckUpdate);
                updateThread.IsBackground = true;
                updateThread.Start();
            }
#endif

            IsReady = true;

            DebugHelper.WriteLine("Startup time: {0} ms", Program.StartTimer.ElapsedMilliseconds);

            UseCommandLineArgs(Environment.GetCommandLineArgs());
        }

        private void AfterShownJobs()
        {
            this.ShowActivate();
        }

        private void InitControls()
        {
            InitializeComponent();

            Text = Program.Title;
            Icon = Resources.ShareXIcon;

            AddMultiEnumItems<AfterCaptureTasks>(x => Program.DefaultTaskSettings.AfterCaptureJob = Program.DefaultTaskSettings.AfterCaptureJob.Swap(x),
                tsddbAfterCaptureTasks, tsmiTrayAfterCaptureTasks);
            AddMultiEnumItems<AfterUploadTasks>(x => Program.DefaultTaskSettings.AfterUploadJob = Program.DefaultTaskSettings.AfterUploadJob.Swap(x),
                tsddbAfterUploadTasks, tsmiTrayAfterUploadTasks);
            AddEnumItems<ImageDestination>(x => Program.DefaultTaskSettings.ImageDestination = x, tsmiImageUploaders, tsmiTrayImageUploaders);
            AddEnumItems<TextDestination>(x => Program.DefaultTaskSettings.TextDestination = x, tsmiTextUploaders, tsmiTrayTextUploaders);
            AddEnumItems<FileDestination>(x => Program.DefaultTaskSettings.FileDestination = x, tsmiFileUploaders, tsmiTrayFileUploaders);
            AddEnumItems<UrlShortenerType>(x => Program.DefaultTaskSettings.URLShortenerDestination = x, tsmiURLShorteners, tsmiTrayURLShorteners);
            AddEnumItems<SocialNetworkingService>(x => Program.DefaultTaskSettings.SocialNetworkingServiceDestination = x, tsmiSocialServices, tsmiTraySocialServices);

            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.Images.Add(Properties.Resources.navigation_090_button);
            il.Images.Add(Properties.Resources.cross_button);
            il.Images.Add(Properties.Resources.tick_button);
            il.Images.Add(Properties.Resources.navigation_000_button);
            lvUploads.SmallImageList = il;

            TaskManager.ListViewControl = lvUploads;
            uim = new UploadInfoManager(lvUploads);
        }

        private void UpdateDestinationStates()
        {
            if (Program.UploadersConfig != null)
            {
                EnableDisableToolStripMenuItems<ImageDestination>(tsmiImageUploaders, tsmiTrayImageUploaders);
                EnableDisableToolStripMenuItems<TextDestination>(tsmiTextUploaders, tsmiTrayTextUploaders);
                EnableDisableToolStripMenuItems<FileDestination>(tsmiFileUploaders, tsmiTrayFileUploaders);
                EnableDisableToolStripMenuItems<UrlShortenerType>(tsmiURLShorteners, tsmiTrayURLShorteners);
                EnableDisableToolStripMenuItems<SocialNetworkingService>(tsmiSocialServices, tsmiTraySocialServices);
            }
        }

        private void AddEnumItems<T>(Action<T> selectedEnum, params ToolStripDropDownItem[] parents)
        {
            string[] enums = Helpers.GetEnumDescriptions<T>();

            foreach (ToolStripDropDownItem parent in parents)
            {
                for (int i = 0; i < enums.Length; i++)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(enums[i]);

                    int index = i;

                    tsmi.Click += (sender, e) =>
                    {
                        foreach (ToolStripDropDownItem parent2 in parents)
                        {
                            for (int i2 = 0; i2 < enums.Length; i2++)
                            {
                                ToolStripMenuItem tsmi2 = (ToolStripMenuItem)parent2.DropDownItems[i2];
                                tsmi2.Checked = index == i2;
                            }
                        }

                        selectedEnum((T)Enum.ToObject(typeof(T), index));

                        UpdateUploaderMenuNames();
                    };

                    parent.DropDownItems.Add(tsmi);
                }
            }
        }

        private void SetEnumChecked(Enum value, params ToolStripDropDownItem[] parents)
        {
            int index = value.GetIndex();

            foreach (ToolStripDropDownItem parent in parents)
            {
                ((ToolStripMenuItem)parent.DropDownItems[index]).Checked = true;
            }
        }

        private void AddMultiEnumItems<T>(Action<T> selectedEnum, params ToolStripDropDownItem[] parents)
        {
            string[] enums = Enum.GetValues(typeof(T)).Cast<Enum>().Skip(1).Select(x => x.GetDescription()).ToArray();

            foreach (ToolStripDropDownItem parent in parents)
            {
                for (int i = 0; i < enums.Length; i++)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(enums[i]);

                    int index = i;

                    tsmi.Click += (sender, e) =>
                    {
                        foreach (ToolStripDropDownItem parent2 in parents)
                        {
                            ToolStripMenuItem tsmi2 = (ToolStripMenuItem)parent2.DropDownItems[index];
                            tsmi2.Checked = !tsmi2.Checked;
                        }

                        selectedEnum((T)Enum.ToObject(typeof(T), 1 << index));

                        UpdateUploaderMenuNames();
                    };

                    parent.DropDownItems.Add(tsmi);
                }
            }
        }

        private void SetMultiEnumChecked(Enum value, params ToolStripDropDownItem[] parents)
        {
            for (int i = 0; i < parents[0].DropDownItems.Count; i++)
            {
                foreach (ToolStripDropDownItem parent in parents)
                {
                    ToolStripMenuItem tsmi = (ToolStripMenuItem)parent.DropDownItems[i];
                    tsmi.Checked = value.HasFlag(1 << i);
                }
            }
        }

        private void EnableDisableToolStripMenuItems<T>(params ToolStripDropDownItem[] parents)
        {
            foreach (ToolStripDropDownItem parent in parents)
            {
                for (int i = 0; i < parent.DropDownItems.Count; i++)
                {
                    parent.DropDownItems[i].Enabled = Program.UploadersConfig.IsActive<T>(i);
                }
            }
        }

        private void UpdateControls()
        {
            cmsUploadInfo.SuspendLayout();

            tsmiStopUpload.Visible = tsmiOpen.Visible = tsmiCopy.Visible = tsmiShowErrors.Visible = tsmiShowResponse.Visible =
                tsmiUploadSelectedFile.Visible = tsmiClearList.Visible = tssUploadInfo1.Visible = false;
            pbPreview.Reset();
            uim.RefreshSelectedItems();

            if (lvUploads.SelectedItems.Count > 0)
            {
                if (GetCurrentTasks().Any(x => x.IsWorking))
                {
                    tsmiStopUpload.Visible = true;
                }
                else
                {
                    if (uim.IsSelectedItemsValid())
                    {
                        if (uim.SelectedItems[0].Info.Result.IsError)
                        {
                            tsmiShowErrors.Visible = true;
                        }
                        else
                        {
                            // Open
                            tsmiOpen.Visible = true;

                            tsmiOpenURL.Enabled = uim.SelectedItems[0].IsURLExist;
                            tsmiOpenShortenedURL.Enabled = uim.SelectedItems[0].IsShortenedURLExist;
                            tsmiOpenThumbnailURL.Enabled = uim.SelectedItems[0].IsThumbnailURLExist;
                            tsmiOpenDeletionURL.Enabled = uim.SelectedItems[0].IsDeletionURLExist;

                            tsmiOpenFile.Enabled = uim.SelectedItems[0].IsFileExist;
                            tsmiOpenFolder.Enabled = uim.SelectedItems[0].IsFileExist;

                            // Copy
                            tsmiCopy.Visible = true;

                            tsmiCopyURL.Enabled = uim.SelectedItems.Any(x => x.IsURLExist);
                            tsmiCopyShortenedURL.Enabled = uim.SelectedItems.Any(x => x.IsShortenedURLExist);
                            tsmiCopyThumbnailURL.Enabled = uim.SelectedItems.Any(x => x.IsThumbnailURLExist);
                            tsmiCopyDeletionURL.Enabled = uim.SelectedItems.Any(x => x.IsDeletionURLExist);

                            tsmiCopyFile.Enabled = uim.SelectedItems[0].IsFileExist;
                            tsmiCopyImage.Enabled = uim.SelectedItems[0].IsImageFile;
                            tsmiCopyText.Enabled = uim.SelectedItems[0].IsTextFile;

                            tsmiCopyHTMLLink.Enabled = uim.SelectedItems.Any(x => x.IsURLExist);
                            tsmiCopyHTMLImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL);
                            tsmiCopyHTMLLinkedImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL && x.IsThumbnailURLExist);

                            tsmiCopyForumLink.Enabled = uim.SelectedItems.Any(x => x.IsURLExist);
                            tsmiCopyForumImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL && x.IsURLExist);
                            tsmiCopyForumLinkedImage.Enabled = uim.SelectedItems.Any(x => x.IsImageURL && x.IsThumbnailURLExist);

                            tsmiCopyFilePath.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);
                            tsmiCopyFileName.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);
                            tsmiCopyFileNameWithExtension.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);
                            tsmiCopyFolder.Enabled = uim.SelectedItems.Any(x => x.IsFilePathValid);

                            tsmiCopyCustomFormat.DropDownItems.Clear();
                            foreach (ClipboardFormat cf in Program.Settings.ClipboardContentFormats)
                            {
                                ToolStripMenuItem tsmiClipboardFormat = new ToolStripMenuItem(cf.Description);
                                tsmiClipboardFormat.Tag = cf;
                                tsmiClipboardFormat.Click += tsmiClipboardFormat_Click;
                                tsmiCopyCustomFormat.DropDownItems.Add(tsmiClipboardFormat);
                            }

                            if (!scMain.Panel2Collapsed)
                            {
                                if (uim.SelectedItems[0].IsImageFile)
                                {
                                    pbPreview.LoadImageFromFile(uim.SelectedItems[0].Info.FilePath);
                                }
                                else if (uim.SelectedItems[0].IsImageURL)
                                {
                                    pbPreview.LoadImageFromURL(uim.SelectedItems[0].Info.Result.URL);
                                }
                            }
                        }

                        if (uim.SelectedItems[0].Info.Result.IsError && !string.IsNullOrEmpty(uim.SelectedItems[0].Info.Result.Response))
                        {
                            tsmiShowResponse.Visible = true;
                        }

                        tsmiUploadSelectedFile.Visible = uim.SelectedItems[0].IsFileExist;
                    }
                }
            }

            tsmiClearList.Visible = tssUploadInfo1.Visible = lvUploads.Items.Count > 0;

            cmsUploadInfo.ResumeLayout();
        }

        private void tsmiClipboardFormat_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiClipboardFormat = sender as ToolStripMenuItem;
            ClipboardFormat cf = tsmiClipboardFormat.Tag as ClipboardFormat;
            uim.CopyCustomFormat(cf.Format);
        }

        private void LoadSettings()
        {
            niTray.Icon = Resources.sharex_16px_6;
            niTray.Visible = Program.Settings.ShowTray;

            UpdateMainFormSettings();

            UpdateMenu();
            UpdateUploaderMenuNames();

            UploadManager.UpdateProxySettings();

            if (Program.Settings.PreviewSplitterDistance > 0)
            {
                scMain.SplitterDistance = Program.Settings.PreviewSplitterDistance;
            }

            UpdatePreviewSplitter();

            if (Program.Settings.ClipboardContentFormats.Count == 0)
            {
                Program.Settings.ClipboardContentFormats.Add(new ClipboardFormat()
                {
                    Description = "HTML linked image",
                    Format = "<a href=\"$url\"><img src=\"$thumbnailurl\" alt=\"\" title\"\" /></a>"
                });
            }
        }

        private class WatchFolderWithTaskSettings
        {
            public WatchFolder WatchFolder { get; set; }
            public TaskSettings TaskSettings { get; set; }
        }

        private void HandleWatchFolder()
        {
            IEnumerable<WatchFolderWithTaskSettings> watchFolders = Program.HotkeySettings.Hotkeys.Where(x => x.TaskSettings.WatchFolderEnabled).
                SelectMany(hotkeySetting => hotkeySetting.TaskSettings.WatchFolderList, (hotkeySetting, watchFolder) => new WatchFolderWithTaskSettings
                {
                    WatchFolder = watchFolder,
                    TaskSettings = hotkeySetting.TaskSettings
                });

            if (Program.DefaultTaskSettings.WatchFolderEnabled)
            {
                watchFolders = Program.DefaultTaskSettings.WatchFolderList.Select(watchFolder => new WatchFolderWithTaskSettings
                {
                    WatchFolder = watchFolder,
                    TaskSettings = Program.DefaultTaskSettings
                }).Concat(watchFolders);
            }

            foreach (WatchFolderWithTaskSettings x in watchFolders)
            {
                x.WatchFolder.FileWatcherTrigger += path => UploadManager.UploadFile(path, x.TaskSettings);
                x.WatchFolder.Enable();
            }
        }

        public void UpdateMainFormSettings()
        {
            SetMultiEnumChecked(Program.DefaultTaskSettings.AfterCaptureJob, tsddbAfterCaptureTasks, tsmiTrayAfterCaptureTasks);
            SetMultiEnumChecked(Program.DefaultTaskSettings.AfterUploadJob, tsddbAfterUploadTasks, tsmiTrayAfterUploadTasks);
            SetEnumChecked(Program.DefaultTaskSettings.ImageDestination, tsmiImageUploaders, tsmiTrayImageUploaders);
            SetEnumChecked(Program.DefaultTaskSettings.TextDestination, tsmiTextUploaders, tsmiTrayTextUploaders);
            SetEnumChecked(Program.DefaultTaskSettings.FileDestination, tsmiFileUploaders, tsmiTrayFileUploaders);
            SetEnumChecked(Program.DefaultTaskSettings.URLShortenerDestination, tsmiURLShorteners, tsmiTrayURLShorteners);
            SetEnumChecked(Program.DefaultTaskSettings.SocialNetworkingServiceDestination, tsmiSocialServices, tsmiTraySocialServices);
        }

        private void UpdateUploaderMenuNames()
        {
            string imageUploader = Program.DefaultTaskSettings.ImageDestination == ImageDestination.FileUploader ?
                Program.DefaultTaskSettings.FileDestination.GetDescription() : Program.DefaultTaskSettings.ImageDestination.GetDescription();
            tsmiImageUploaders.Text = tsmiTrayImageUploaders.Text = "Image uploader: " + imageUploader;

            string textUploader = Program.DefaultTaskSettings.TextDestination == TextDestination.FileUploader ?
                Program.DefaultTaskSettings.FileDestination.GetDescription() : Program.DefaultTaskSettings.TextDestination.GetDescription();
            tsmiTextUploaders.Text = tsmiTrayTextUploaders.Text = "Text uploader: " + textUploader;

            tsmiFileUploaders.Text = tsmiTrayFileUploaders.Text = "File uploader: " + Program.DefaultTaskSettings.FileDestination.GetDescription();

            tsmiURLShorteners.Text = tsmiTrayURLShorteners.Text = "URL shortener: " + Program.DefaultTaskSettings.URLShortenerDestination.GetDescription();

            tsmiSocialServices.Text = tsmiTraySocialServices.Text = "Social networking service: " + Program.DefaultTaskSettings.SocialNetworkingServiceDestination.GetDescription();
        }

        private void CheckUpdate()
        {
            UpdateChecker updateChecker = new UpdateChecker(Links.URL_UPDATE, Application.ProductName, Program.AssemblyVersion,
                ReleaseChannelType.Stable, Uploader.ProxyInfo.GetWebProxy());
            updateChecker.CheckUpdate();

            if (updateChecker.UpdateInfo != null)
            {
                switch (updateChecker.UpdateInfo.Status)
                {
                    case UpdateStatus.UpdateRequired:
                        string updateText = string.Format("Would you like to download the update?\r\n\r\n{0} is current version.\r\n{1} is latest version.",
                            updateChecker.UpdateInfo.CurrentVersion, updateChecker.UpdateInfo.LatestVersion);

                        if (MessageBox.Show(updateText, "ShareX update is available", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            UpdaterForm downloader = new UpdaterForm(updateChecker.UpdateInfo.URL, updateChecker.Proxy, updateChecker.UpdateInfo.Summary);
                            downloader.ShowDialog();
                            if (downloader.Status == DownloaderFormStatus.InstallStarted)
                            {
                                Application.Exit();
                            }
                        }
                        break;
                    case UpdateStatus.UpdateCheckFailed:
                        DebugHelper.WriteLine("Update check failed.");
                        break;
                }
            }
        }

        private void ForceClose()
        {
            forceClose = true;
            Close();
        }

        public void UseCommandLineArgs(string[] args, TaskSettings taskSettings = null)
        {
            if (taskSettings == null) taskSettings = Program.DefaultTaskSettings;

            if (args != null && args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].Equals("-clipboardupload", StringComparison.InvariantCultureIgnoreCase))
                    {
                        UploadManager.ClipboardUpload(taskSettings);
                    }
                    else if (args[i][0] != '-')
                    {
                        UploadManager.UploadFile(args[i], taskSettings);
                    }
                }
            }
        }

        private UploadTask[] GetCurrentTasks()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                return lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadTask).Where(x => x != null).ToArray();
            }

            return null;
        }

        private TaskInfo GetCurrentUploadInfo()
        {
            TaskInfo info = null;
            UploadTask[] tasks = GetCurrentTasks();

            if (tasks != null && tasks.Length > 0)
            {
                info = tasks[0].Info;
            }

            return info;
        }

        private void RemoveSelectedItems()
        {
            lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadTask).Where(x => x != null && !x.IsWorking).ForEach(x => TaskManager.Remove(x));
        }

        private void RemoveAllItems()
        {
            lvUploads.Items.Cast<ListViewItem>().Select(x => x.Tag as UploadTask).Where(x => x != null && !x.IsWorking).ForEach(x => TaskManager.Remove(x));
        }

        private void UpdateMenu()
        {
            if (Program.Settings.ShowMenu)
            {
                tsmiHideMenu.Text = "Hide menu";
            }
            else
            {
                tsmiHideMenu.Text = "Show menu";
            }

            tsMain.Visible = Program.Settings.ShowMenu;
            lblSplitter.Visible = Program.Settings.ShowMenu;
        }

        private void UpdatePreviewSplitter()
        {
            if (Program.Settings.IsPreviewCollapsed)
            {
                tsmiShowPreview.Text = "Show image preview";
            }
            else
            {
                tsmiShowPreview.Text = "Hide image preview";
            }

            scMain.Panel2Collapsed = Program.Settings.IsPreviewCollapsed;
            lblSplitter2.Visible = !Program.Settings.IsPreviewCollapsed;
        }

        private void DoScreenRecorder(TaskSettings taskSettings, bool isHotkey = false)
        {
            ScreenRecordForm form = ScreenRecordForm.Instance;

            if (form.IsRecording)
            {
                form.StopRecording();
            }
            else
            {
                form.StartRecording(taskSettings);
            }
        }

        private void OpenAutoCapture()
        {
            AutoCapture autoCaptureForm = new AutoCapture();
            autoCaptureForm.Show();
        }

        #region Form events

        protected override void SetVisibleCore(bool value)
        {
            if (value && !IsHandleCreated)
            {
                if (Program.IsSilentRun && Program.Settings.ShowTray)
                {
                    CreateHandle();
                    value = false;
                }

                AfterLoadJobs();
            }

            base.SetVisibleCore(value);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            AfterShownJobs();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && Program.Settings.ShowTray && !forceClose)
            {
                e.Cancel = true;
                Hide();
                Program.Settings.SaveAsync();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskManager.StopAllTasks();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetDataPresent(DataFormats.Bitmap, false) ||
                e.Data.GetDataPresent(DataFormats.Text, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            UploadManager.DragDropUpload(e.Data, Program.DefaultTaskSettings);
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUploadWithContentViewer(Program.DefaultTaskSettings);
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile(Program.DefaultTaskSettings);
        }

        private void tsddbDestinations_DropDownOpened(object sender, EventArgs e)
        {
            UpdateDestinationStates();
        }

        private void tsmiShowDebugLog_Click(object sender, EventArgs e)
        {
            DebugForm dlg = new DebugForm(Application.ProductName, Program.MyLogger);
            dlg.Show();
        }

        private void tsmiTestImageUpload_Click(object sender, EventArgs e)
        {
            UploadManager.RunImageTask(Resources.ShareXLogo);
        }

        private void tsmiTestTextUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadText(Program.ApplicationName + " text upload test", Program.DefaultTaskSettings);
        }

        private void tsmiTestFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadImage(Resources.ShareXLogo, ImageDestination.FileUploader);
        }

        private void tsmiTestURLShortener_Click(object sender, EventArgs e)
        {
            UploadManager.ShortenURL(Links.URL_WEBSITE, Program.DefaultTaskSettings);
        }

        private void tsmiTestUploaders_Click(object sender, EventArgs e)
        {
            using (UploadTestForm form = new UploadTestForm())
            {
                form.ShowDialog();
            }
        }

        private void tsmiTestShapeCapture_Click(object sender, EventArgs e)
        {
            new RegionCapturePreview(Program.DefaultTaskSettings.CaptureSettings.SurfaceOptions).Show();
        }

        private void tsmiScreenRecorderGIF_Click(object sender, EventArgs e)
        {
            DoScreenRecorder(Program.DefaultTaskSettings);
        }

        private void tsmiAutoCapture_Click(object sender, EventArgs e)
        {
            OpenAutoCapture();
        }

        private void tsbApplicationSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.ShowDialog();
            }

            UploadManager.UpdateProxySettings();
            Program.Settings.SaveAsync();
        }

        private void tsbTaskSettings_Click(object sender, EventArgs e)
        {
            using (TaskSettingsForm taskSettingsForm = new TaskSettingsForm(Program.DefaultTaskSettings, true))
            {
                taskSettingsForm.ShowDialog();
            }
        }

        private void tsbHotkeySettings_Click(object sender, EventArgs e)
        {
            if (Program.HotkeySettings == null)
            {
                Program.HotkeySettingsResetEvent.WaitOne();
            }

            using (HotkeySettingsForm hotkeySettingsForm = new HotkeySettingsForm())
            {
                hotkeySettingsForm.ShowDialog();
                Program.HotkeySettings.SaveAsync();
            }
        }

        private void tsbDestinationSettings_Click(object sender, EventArgs e)
        {
            if (Program.UploadersConfig == null)
            {
                Program.UploaderSettingsResetEvent.WaitOne();
            }

            using (UploadersConfigForm uploadersConfigForm = new UploadersConfigForm(Program.UploadersConfig, new UploadersAPIKeys()))
            {
                uploadersConfigForm.ShowDialog();
                uploadersConfigForm.Config.SaveAsync(Program.UploadersConfigFilePath);
            }
        }

        private void tsmiCursorHelper_Click(object sender, EventArgs e)
        {
            using (DialogColor colorForm = new DialogColor())
            {
                colorForm.ScreenPicker = true;
                colorForm.ShowDialog();
            }
        }

        private void tsmiHashCheck_Click(object sender, EventArgs e)
        {
            HashCheckForm hashCheckForm = new HashCheckForm();
            hashCheckForm.Show();
        }

        private void tsbScreenshotsFolder_Click(object sender, EventArgs e)
        {
            Helpers.OpenFolder(Program.ScreenshotsPath);
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            HistoryManager.ConvertHistoryToNewFormat(Program.HistoryFilePath, Program.OldHistoryFilePath);

            using (HistoryForm historyForm = new HistoryForm(Program.HistoryFilePath))
            {
                Program.Settings.HistoryWindowState.AutoHandleFormState(historyForm);
                historyForm.Text = "ShareX - History: " + Program.HistoryFilePath;
                historyForm.ShowDialog();
            }
        }

        private void tsbImageHistory_Click(object sender, EventArgs e)
        {
            HistoryManager.ConvertHistoryToNewFormat(Program.HistoryFilePath, Program.OldHistoryFilePath);

            using (ImageHistoryForm imageHistoryForm = new ImageHistoryForm(Program.HistoryFilePath,
                Program.Settings.ImageHistoryViewMode, Program.Settings.ImageHistoryThumbnailSize, Program.Settings.ImageHistoryMaxItemCount))
            {
                Program.Settings.ImageHistoryWindowState.AutoHandleFormState(imageHistoryForm);
                imageHistoryForm.Text = "ShareX - Image history: " + Program.HistoryFilePath;
                imageHistoryForm.ShowDialog();

                Program.Settings.ImageHistoryViewMode = imageHistoryForm.ViewMode;
                Program.Settings.ImageHistoryThumbnailSize = imageHistoryForm.ThumbnailSize;
                Program.Settings.ImageHistoryMaxItemCount = imageHistoryForm.MaxItemCount;
            }
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            Helpers.LoadBrowserAsync(Links.URL_DONATE);
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void lvUploads_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UpdateControls();
                cmsUploadInfo.Show(lvUploads, e.X + 1, e.Y + 1);
            }
        }

        private void lvUploads_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                uim.TryOpen();
            }
        }

        private void scMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Program.Settings.PreviewSplitterDistance = scMain.SplitterDistance;
        }

        private void lvUploads_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                default: return;
                case Keys.Enter:
                    uim.TryOpen();
                    break;
                case Keys.Control | Keys.Enter:
                    uim.OpenFile();
                    break;
                case Keys.Control | Keys.X:
                    uim.CopyURL();
                    RemoveSelectedItems();
                    break;
                case Keys.Control | Keys.C:
                    uim.CopyURL();
                    break;
                case Keys.Control | Keys.V:
                    UploadManager.ClipboardUploadWithContentViewer(Program.DefaultTaskSettings);
                    break;
                case Keys.Delete:
                    RemoveSelectedItems();
                    break;
            }

            e.Handled = true;
        }

        #region Tray events

        private void niTray_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    CaptureScreenshot(CaptureType.Rectangle, Program.DefaultTaskSettings, false);
                    break;
            }
        }

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ShowActivate();
            }
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            string url = niTray.Tag as string;

            if (!string.IsNullOrEmpty(url))
            {
                Helpers.LoadBrowserAsync(url);
            }
        }

        private void tsmiTrayExit_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        #endregion Tray events

        #region UploadInfoMenu events

        private void tsmiStopUpload_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                foreach (UploadTask task in GetCurrentTasks())
                {
                    task.Stop();
                }
            }
        }

        private void tsmiOpenURL_Click(object sender, EventArgs e)
        {
            uim.OpenURL();
        }

        private void tsmiOpenShortenedURL_Click(object sender, EventArgs e)
        {
            uim.OpenShortenedURL();
        }

        private void tsmiOpenThumbnailURL_Click(object sender, EventArgs e)
        {
            uim.OpenThumbnailURL();
        }

        private void tsmiOpenDeletionURL_Click(object sender, EventArgs e)
        {
            uim.OpenDeletionURL();
        }

        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            uim.OpenFile();
        }

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            uim.OpenFolder();
        }

        private void tsmiCopyURL_Click(object sender, EventArgs e)
        {
            uim.CopyURL();
        }

        private void tsmiCopyShortenedURL_Click(object sender, EventArgs e)
        {
            uim.CopyShortenedURL();
        }

        private void tsmiCopyThumbnailURL_Click(object sender, EventArgs e)
        {
            uim.CopyThumbnailURL();
        }

        private void tsmiCopyDeletionURL_Click(object sender, EventArgs e)
        {
            uim.CopyDeletionURL();
        }

        private void tsmiCopyFile_Click(object sender, EventArgs e)
        {
            uim.CopyFile();
        }

        private void tsmiCopyImage_Click(object sender, EventArgs e)
        {
            uim.CopyImage();
        }

        private void tsmiCopyText_Click(object sender, EventArgs e)
        {
            uim.CopyText();
        }

        private void tsmiCopyHTMLLink_Click(object sender, EventArgs e)
        {
            uim.CopyHTMLLink();
        }

        private void tsmiCopyHTMLImage_Click(object sender, EventArgs e)
        {
            uim.CopyHTMLImage();
        }

        private void tsmiCopyHTMLLinkedImage_Click(object sender, EventArgs e)
        {
            uim.CopyHTMLLinkedImage();
        }

        private void tsmiCopyForumLink_Click(object sender, EventArgs e)
        {
            uim.CopyForumLink();
        }

        private void tsmiCopyForumImage_Click(object sender, EventArgs e)
        {
            uim.CopyForumImage();
        }

        private void tsmiCopyForumLinkedImage_Click(object sender, EventArgs e)
        {
            uim.CopyForumLinkedImage();
        }

        private void tsmiCopyFilePath_Click(object sender, EventArgs e)
        {
            uim.CopyFilePath();
        }

        private void tsmiCopyFileName_Click(object sender, EventArgs e)
        {
            uim.CopyFileName();
        }

        private void tsmiCopyFileNameWithExtension_Click(object sender, EventArgs e)
        {
            uim.CopyFileNameWithExtension();
        }

        private void tsmiCopyFolder_Click(object sender, EventArgs e)
        {
            uim.CopyFolder();
        }

        private void tsmiShowErrors_Click(object sender, EventArgs e)
        {
            uim.ShowErrors();
        }

        private void tsmiShowResponse_Click(object sender, EventArgs e)
        {
            uim.ShowResponse();
        }

        private void tsmiUploadSelectedFile_Click(object sender, EventArgs e)
        {
            uim.Upload();
        }

        private void tsmiClearList_Click(object sender, EventArgs e)
        {
            RemoveAllItems();
        }

        private void tsmiClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUploadWithContentViewer(Program.DefaultTaskSettings);
        }

        private void tsmiUploadFile_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile(Program.DefaultTaskSettings);
        }

        private void tsmiHideMenu_Click(object sender, EventArgs e)
        {
            Program.Settings.ShowMenu = !Program.Settings.ShowMenu;
            UpdateMenu();
        }

        private void tsmiShowPreview_Click(object sender, EventArgs e)
        {
            Program.Settings.IsPreviewCollapsed = !Program.Settings.IsPreviewCollapsed;
            UpdatePreviewSplitter();
            UpdateControls();
        }

        #endregion UploadInfoMenu events

        #endregion Form events
    }
}