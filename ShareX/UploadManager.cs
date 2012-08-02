﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using HelpersLib;
using HistoryLib;
using ShareX.Properties;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ShareX
{
    public static class UploadManager
    {
        public static MyListView ListViewControl { get; set; }
        public static List<Task> Tasks { get; private set; }

        private static object uploadManagerLock = new object();
        private static Icon[] trayIcons;

        static UploadManager()
        {
            Tasks = new List<Task>();
            trayIcons = new Icon[] { Resources.sharex_16px_0, Resources.sharex_16px_1, Resources.sharex_16px_2, Resources.sharex_16px_3,
                Resources.sharex_16px_4, Resources.sharex_16px_5, Resources.sharex_16px_6 };
        }

        public static void UploadFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    Task task = Task.CreateFileUploaderTask(filePath);
                    StartTask(task);
                }
                else if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);
                    UploadFile(files);
                }
            }
        }

        public static void UploadFile(string[] files)
        {
            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    UploadFile(file);
                }
            }
        }

        public static void UploadFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UploadFile(ofd.FileNames);
                }
            }
        }

        public static void ClipboardUpload()
        {
            if (Clipboard.ContainsImage())
            {
                Image img = Clipboard.GetImage();
                AfterCaptureTasks tasks = Program.Settings.AfterCaptureTasks.Remove(AfterCaptureTasks.CopyImageToClipboard);
                UploadManager.UploadImage(img, tasks);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string[] files = Clipboard.GetFileDropList().Cast<string>().ToArray();
                UploadFile(files);
            }
            else if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();

                if (Program.Settings.ClipboardUploadAutoDetectURL && Helpers.IsValidURL(text))
                {
                    ShortenURL(text.Trim());
                }
                else
                {
                    UploadText(text);
                }
            }
        }

        public static void ClipboardUploadWithContentViewer()
        {
            if (Program.Settings.ShowClipboardContentViewer)
            {
                using (ClipboardContentViewer ccv = new ClipboardContentViewer())
                {
                    if (ccv.ShowDialog() == DialogResult.OK && !ccv.IsClipboardEmpty)
                    {
                        UploadManager.ClipboardUpload();
                    }

                    Program.Settings.ShowClipboardContentViewer = !ccv.DontShowThisWindow;
                }
            }
            else
            {
                UploadManager.ClipboardUpload();
            }
        }

        public static void DragDropUpload(IDataObject data)
        {
            if (data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = data.GetData(DataFormats.FileDrop, false) as string[];
                UploadFile(files);
            }
            else if (data.GetDataPresent(DataFormats.Bitmap, false))
            {
                Image img = data.GetData(DataFormats.Bitmap, false) as Image;
                UploadImage(img);
            }
            else if (data.GetDataPresent(DataFormats.Text, false))
            {
                string text = data.GetData(DataFormats.Text, false) as string;
                UploadText(text);
            }
        }

        public static void UploadImage(Image img, AfterCaptureTasks imageJob = AfterCaptureTasks.UploadImageToHost)
        {
            if (img != null && imageJob != AfterCaptureTasks.None)
            {
                Task task = Task.CreateImageUploaderTask(img, imageJob);
                StartTask(task);
            }
        }

        public static void UploadImage(Image img, ImageDestination imageDestination)
        {
            if (img != null)
            {
                Task task = Task.CreateImageUploaderTask(img);
                task.Info.ImageDestination = imageDestination;
                StartTask(task);
            }
        }

        public static void UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Task task = Task.CreateTextUploaderTask(text);
                StartTask(task);
            }
        }

        public static void UploadImageStream(Stream stream, string filename)
        {
            if (stream != null && stream.Length > 0 && !string.IsNullOrEmpty(filename))
            {
                Task task = Task.CreateDataUploaderTask(EDataType.Image, stream, filename);
                StartTask(task);
            }
        }

        public static void ShortenURL(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Task task = Task.CreateURLShortenerTask(url);
                StartTask(task);
            }
        }

        private static void StartTask(Task task)
        {
            Tasks.Add(task);
            task.Info.ID = Tasks.Count - 1;
            task.UploadPreparing += new Task.TaskEventHandler(task_UploadPreparing);
            task.UploadStarted += new Task.TaskEventHandler(task_UploadStarted);
            task.UploadProgressChanged += new Task.TaskEventHandler(task_UploadProgressChanged);
            task.UploadCompleted += new Task.TaskEventHandler(task_UploadCompleted);
            CreateListViewItem(task.Info);
            StartTasks();
        }

        private static void StartTasks()
        {
            int workingTasksCount = Tasks.Count(x => x.IsWorking);
            Task[] inQueueTasks = Tasks.Where(x => x.Status == TaskStatus.InQueue).ToArray();

            if (inQueueTasks.Length > 0)
            {
                int len;

                if (Program.Settings.UploadLimit == 0)
                {
                    len = inQueueTasks.Length;
                }
                else
                {
                    len = (Program.Settings.UploadLimit - workingTasksCount).Between(0, inQueueTasks.Length);
                }

                for (int i = 0; i < len; i++)
                {
                    inQueueTasks[i].Start();
                }
            }
        }

        public static void StopUpload(int index)
        {
            if (Tasks.Count < index)
            {
                Tasks[index].Stop();
            }
        }

        public static void UpdateProxySettings()
        {
            ProxySettings proxy = new ProxySettings();
            if (!string.IsNullOrEmpty(Program.Settings.ProxySettings.Host))
            {
                proxy.ProxyConfig = EProxyConfigType.ManualProxy;
            }
            proxy.ProxyActive = Program.Settings.ProxySettings;
            Uploader.ProxySettings = proxy;
        }

        private static void ChangeListViewItemStatus(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[1].Text = info.Status;
            }
        }

        private static void CreateListViewItem(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                Program.MyLogger.WriteLine("Task in queue. ID: {0}, Job: {1}, Type: {2}, Host: {3}", info.ID, info.Job, info.UploadDestination, info.UploaderHost);

                ListViewItem lvi = new ListViewItem();
                lvi.Tag = info;
                lvi.Text = info.FileName;
                lvi.SubItems.Add("In queue");
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(info.DataType.ToString());
                lvi.SubItems.Add(info.IsUploadJob ? info.UploaderHost : string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.BackColor = info.ID % 2 == 0 ? Color.White : Color.WhiteSmoke;
                lvi.ImageIndex = 3;
                ListViewControl.Items.Add(lvi);
                lvi.EnsureVisible();
                ListViewControl.FillLastColumn();
            }
        }

        private static void task_UploadPreparing(UploadInfo info)
        {
            Program.MyLogger.WriteLine("Task preparing. ID: {0}", info.ID);
            ChangeListViewItemStatus(info);
            UpdateProgressUI();
        }

        private static void task_UploadStarted(UploadInfo info)
        {
            string status = string.Format("Upload started. ID: {0}, Filename: {1}", info.ID, info.FileName);
            if (!string.IsNullOrEmpty(info.FilePath)) status += ", Filepath: " + info.FilePath;
            Program.MyLogger.WriteLine(status);

            ListViewItem lvi = ListViewControl.Items[info.ID];
            lvi.Text = info.FileName;
            lvi.SubItems[1].Text = info.Status;
            lvi.ImageIndex = 0;
        }

        private static void task_UploadProgressChanged(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[1].Text = string.Format("{0:0.0}%", info.Progress.Percentage);
                lvi.SubItems[2].Text = string.Format("{0} / {1}", Helpers.ProperFileSize(info.Progress.Position), Helpers.ProperFileSize(info.Progress.Length));

                if (info.Progress.Speed > 0)
                {
                    lvi.SubItems[3].Text = Helpers.ProperFileSize((long)info.Progress.Speed, "/s");
                }

                lvi.SubItems[4].Text = Helpers.ProperTimeSpan(info.Progress.Elapsed);
                lvi.SubItems[5].Text = Helpers.ProperTimeSpan(info.Progress.Remaining);
                UpdateProgressUI();
            }
        }

        private static void task_UploadCompleted(UploadInfo info)
        {
            try
            {
                if (ListViewControl != null && info != null && info.Result != null)
                {
                    ListViewItem lvi = ListViewControl.Items[info.ID];

                    if (info.Result.IsError)
                    {
                        string errors = string.Join("\r\n\r\n", info.Result.Errors.ToArray());

                        Program.MyLogger.WriteLine("Task failed. ID: {0}, Filename: {1}, Errors:\r\n{2}", info.ID, info.FileName, errors);

                        lvi.SubItems[1].Text = "Error";
                        lvi.SubItems[8].Text = string.Empty;
                        lvi.ImageIndex = 1;

                        if (Program.Settings.PlaySoundAfterUpload)
                        {
                            SystemSounds.Asterisk.Play();
                        }
                    }
                    else
                    {
                        Program.MyLogger.WriteLine("Task completed. ID: {0}, Filename: {1}, URL: {2}, Duration: {3}ms", info.ID, info.FileName,
                            info.Result.URL, (int)info.UploadDuration.TotalMilliseconds);

                        lvi.Text = info.FileName;
                        lvi.SubItems[1].Text = info.Status;
                        lvi.ImageIndex = 2;

                        if (Program.Settings.SaveHistory && (!string.IsNullOrEmpty(info.Result.URL) || !string.IsNullOrEmpty(info.FilePath)))
                        {
                            HistoryManager.ConvertHistoryToNewFormat(Program.HistoryFilePath, Program.OldHistoryFilePath);
                            HistoryManager.AddHistoryItemAsync(Program.HistoryFilePath, info.GetHistoryItem());
                        }

                        if (!string.IsNullOrEmpty(info.Result.URL))
                        {
                            string url = info.Result.ToString();

                            lvi.SubItems[8].Text = url;

                            if (info.AfterUploadJob.HasFlag(AfterUploadTasks.CopyURLToClipboard))
                            {
                                Helpers.CopyTextSafely(url);
                            }

                            if (Program.Settings.TrayBalloonTipAfterUpload && Program.MainForm.niTray.Visible)
                            {
                                Program.MainForm.niTray.Tag = url;
                                Program.MainForm.niTray.ShowBalloonTip(5000, "ShareX - Upload completed", url, ToolTipIcon.Info);
                            }
                        }

                        if (Program.Settings.PlaySoundAfterUpload)
                        {
                            SystemSounds.Exclamation.Play();
                        }
                    }

                    lvi.EnsureVisible();
                }
            }
            finally
            {
                StartTasks();
                UpdateProgressUI();
            }
        }

        public static void UpdateProgressUI()
        {
            bool isWorkingTasks = false;
            double averageProgress = 0;

            IEnumerable<Task> workingTasks = Tasks.Where(x => x != null && x.IsWorking && x.Info != null);

            if (workingTasks.Count() > 0)
            {
                isWorkingTasks = true;

                workingTasks = workingTasks.Where(x => x.Info.Progress != null);

                if (workingTasks.Count() > 0)
                {
                    averageProgress = workingTasks.Average(x => x.Info.Progress.Percentage);
                }
            }

            if (Program.MainForm.niTray.Visible)
            {
                Icon icon = null;

                if (isWorkingTasks)
                {
                    int index = (int)(averageProgress / 100 * (trayIcons.Length - 1));
                    icon = trayIcons.ReturnIfValidIndex(index) ?? trayIcons.Last();
                }
                else
                {
                    icon = trayIcons.Last();
                }

                if (Program.MainForm.niTray.Icon != icon)
                {
                    Program.MainForm.niTray.Icon = icon;
                }
            }

            string title;

            if (isWorkingTasks)
            {
                title = string.Format("{0} - {1:0.0}%", Program.Title, averageProgress);
            }
            else
            {
                title = Program.Title;
            }

            if (Program.MainForm.Text != title)
            {
                Program.MainForm.Text = title;
            }
        }
    }
}