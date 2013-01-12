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

using HelpersLib;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.GUI;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;

namespace ShareX
{
    public class UploadTask : IDisposable
    {
        public delegate void TaskEventHandler(UploadTask task);

        public event TaskEventHandler UploadStarted;
        public event TaskEventHandler UploadPreparing;
        public event TaskEventHandler UploadProgressChanged;
        public event TaskEventHandler UploadCompleted;

        public UploadInfo Info { get; private set; }

        public TaskStatus Status { get; private set; }

        public bool IsWorking
        {
            get
            {
                return Status == TaskStatus.Preparing || Status == TaskStatus.Working;
            }
        }

        public bool IsStopped { get; private set; }

        private Stream data;
        private Image tempImage;
        private string tempText;
        private ThreadWorker threadWorker;
        private Uploader uploader;

        #region Constructors

        private UploadTask(TaskJob job, EDataType dataType)
        {
            Status = TaskStatus.InQueue;
            Info = new UploadInfo();
            Info.Job = job;
            Info.DataType = dataType;
        }

        public static UploadTask CreateDataUploaderTask(EDataType dataType, Stream stream, string fileName)
        {
            UploadTask task = new UploadTask(TaskJob.DataUpload, dataType);
            task.Info.FileName = fileName;
            task.data = stream;
            return task;
        }

        public static UploadTask CreateDataUploaderTask(EDataType dataType, string filePath)
        {
            UploadTask task = new UploadTask(TaskJob.DataUpload, dataType);
            task.Info.FilePath = filePath;
            task.data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return task;
        }

        public static UploadTask CreateFileUploaderTask(string filePath)
        {
            EDataType dataType = Helpers.FindDataType(filePath);
            UploadTask task = new UploadTask(TaskJob.FileUpload, dataType);
            task.Info.FilePath = filePath;
            task.data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return task;
        }

        public static UploadTask CreateImageUploaderTask(Image image, AfterCaptureTasks imageJob = AfterCaptureTasks.UploadImageToHost)
        {
            UploadTask task = new UploadTask(TaskJob.ImageJob, EDataType.Image);
            task.Info.AfterCaptureJob = imageJob;
            task.Info.FileName = GetImageFilename(image);
            task.tempImage = image;
            return task;
        }

        public static UploadTask CreateTextUploaderTask(string text)
        {
            UploadTask task = new UploadTask(TaskJob.TextUpload, EDataType.Text);
            task.Info.FileName = TaskHelper.GetFilename("txt");
            task.tempText = text;
            return task;
        }

        public static UploadTask CreateURLShortenerTask(string url)
        {
            UploadTask task = new UploadTask(TaskJob.ShortenURL, EDataType.URL);
            task.Info.FileName = "URL shorten";
            task.Info.Result.URL = url;
            return task;
        }

        #endregion Constructors

        private static string GetImageFilename(Image image)
        {
            string filename;

            NameParser nameParser = new NameParser(NameParserType.FileName);
            nameParser.MaxNameLength = 100;
            nameParser.Picture = image;
            nameParser.AutoIncrementNumber = Program.Settings.AutoIncrementNumber;

            ImageTag imageTag = image.Tag as ImageTag;

            if (imageTag != null)
            {
                nameParser.WindowText = imageTag.ActiveWindowTitle;
            }

            if (string.IsNullOrEmpty(nameParser.WindowText))
            {
                filename = nameParser.Convert(Program.Settings.NameFormatPattern) + ".bmp";
            }
            else
            {
                filename = nameParser.Convert(Program.Settings.NameFormatPatternActiveWindow) + ".bmp";
            }

            Program.Settings.AutoIncrementNumber = nameParser.AutoIncrementNumber;

            return filename;
        }

        public void Start()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();

                threadWorker = new ThreadWorker();
                threadWorker.DoWork += ThreadDoWork;
                threadWorker.Completed += ThreadCompleted;
                threadWorker.Start(ApartmentState.STA);
            }
        }

        public void StartSync()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();
                ThreadDoWork();
                ThreadCompleted();
            }
        }

        public void Stop()
        {
            IsStopped = true;

            if (Status == TaskStatus.InQueue)
            {
                OnUploadCompleted();
            }
            else if (Status == TaskStatus.Working && uploader != null)
            {
                uploader.StopUpload();
            }
        }

        private void ThreadDoWork()
        {
            Info.StartTime = DateTime.UtcNow;

            DoThreadJob();

            if (Info.IsUploadJob)
            {
                if (Program.UploadersConfig == null)
                {
                    Program.UploaderSettingsResetEvent.WaitOne();
                }

                Status = TaskStatus.Working;
                Info.Status = "Uploading";

                if (threadWorker != null)
                {
                    threadWorker.InvokeAsync(OnUploadStarted);
                }
                else
                {
                    OnUploadStarted();
                }

                try
                {
                    switch (Info.UploadDestination)
                    {
                        case EDataType.Image:
                            Info.Result = UploadImage(data, Info.FileName);
                            break;
                        case EDataType.File:
                            Info.Result = UploadFile(data, Info.FileName);
                            break;
                        case EDataType.Text:
                            Info.Result = UploadText(data);
                            break;
                    }
                }
                catch (Exception e)
                {
                    if (!IsStopped)
                    {
                        DebugHelper.WriteException(e);

                        if (Info.Result == null) Info.Result = new UploadResult();
                        Info.Result.Errors.Add(e.ToString());
                    }
                }
                finally
                {
                    if (Info.Result == null) Info.Result = new UploadResult();
                    if (uploader != null) Info.Result.Errors.AddRange(uploader.Errors);
                }
            }
            else
            {
                Info.Result.IsURLExpected = false;
            }

            if (!IsStopped && Info.Result != null && Info.Result.IsURLExpected && !Info.Result.IsError)
            {
                if (string.IsNullOrEmpty(Info.Result.URL))
                {
                    Info.Result.Errors.Add("URL is empty.");
                }
                else
                {
                    DoURLJobs();
                }
            }

            Info.UploadTime = DateTime.UtcNow;
        }

        private void DoThreadJob()
        {
            if (Info.Job == TaskJob.ImageJob && tempImage != null)
            {
                if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.AddWatermark) && Program.Settings.WatermarkConfig != null)
                {
                    WatermarkManager watermarkManager = new WatermarkManager(Program.Settings.WatermarkConfig);
                    watermarkManager.ApplyWatermark(tempImage);
                }

                if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.AddBorder))
                {
                    tempImage = CaptureHelpers.DrawBorder(tempImage, BorderType.Outside, Program.Settings.BorderColor, Program.Settings.BorderSize);
                }

                if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.CopyImageToClipboard))
                {
                    Helpers.CopyImageSafely(tempImage);
                    DebugHelper.WriteLine("CopyImageToClipboard");
                }

                if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.SendImageToPrinter))
                {
                    new PrintForm(tempImage, new PrintSettings()).ShowDialog();
                }

                if (Info.AfterCaptureJob.HasFlagAny(AfterCaptureTasks.SaveImageToFile, AfterCaptureTasks.SaveImageToFileWithDialog, AfterCaptureTasks.UploadImageToHost))
                {
                    using (tempImage)
                    {
                        ImageData imageData = TaskHelper.PrepareImage(tempImage);
                        data = imageData.ImageStream;
                        Info.FileName = Path.ChangeExtension(Info.FileName, imageData.ImageFormat.GetDescription());

                        if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.SaveImageToFile))
                        {
                            Info.FilePath = Path.Combine(Program.ScreenshotsPath, Info.FileName);
                            imageData.Write(Info.FilePath);
                            DebugHelper.WriteLine("SaveImageToFile: " + Info.FilePath);
                        }

                        if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.SaveImageToFileWithDialog))
                        {
                            using (SaveFileDialog sfd = new SaveFileDialog())
                            {
                                sfd.InitialDirectory = Program.ScreenshotsPath;
                                sfd.FileName = Info.FileName;
                                sfd.DefaultExt = Path.GetExtension(Info.FileName).Substring(1);
                                sfd.Filter = string.Format("*{0}|*{0}|All files (*.*)|*.*", Path.GetExtension(Info.FileName));
                                sfd.Title = "Choose a folder to save " + Path.GetFileName(Info.FileName);

                                if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfd.FileName))
                                {
                                    Info.FilePath = sfd.FileName;
                                    imageData.Write(Info.FilePath);
                                    DebugHelper.WriteLine("SaveImageToFileWithDialog: " + Info.FilePath);
                                }
                            }
                        }

                        if (Info.AfterCaptureJob.HasFlag(AfterCaptureTasks.PerformActions) && Program.Settings.ExternalPrograms != null &&
                            !string.IsNullOrEmpty(Info.FilePath) && File.Exists(Info.FilePath))
                        {
                            var actions = Program.Settings.ExternalPrograms.Where(x => x.IsActive);

                            if (actions.Count() > 0)
                            {
                                if (data != null)
                                {
                                    data.Dispose();
                                }

                                foreach (ExternalProgram fileAction in actions)
                                {
                                    fileAction.Run(Info.FilePath);
                                }

                                data = new FileStream(Info.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                            }
                        }
                    }
                }
            }
            else if (Info.Job == TaskJob.TextUpload && !string.IsNullOrEmpty(tempText))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(tempText);
                data = new MemoryStream(byteArray);
            }

            if (Info.IsUploadJob && data != null && data.CanSeek)
            {
                data.Position = 0;
            }
        }

        private void DoURLJobs()
        {
            if (Info.AfterUploadJob.HasFlag(AfterUploadTasks.UseURLShortener) || Info.Job == TaskJob.ShortenURL)
            {
                Info.Result.ShortenedURL = ShortenURL(Info.Result.URL);
            }

            if (Info.AfterUploadJob.HasFlag(AfterUploadTasks.ShareURLToSocialNetworkingService))
            {
                OAuthInfo twitterOAuth = Program.UploadersConfig.TwitterOAuthInfoList.ReturnIfValidIndex(Program.UploadersConfig.TwitterSelectedAccount);

                if (twitterOAuth != null)
                {
                    using (TwitterMsg twitter = new TwitterMsg(twitterOAuth))
                    {
                        twitter.Message = Info.Result.ToString();
                        twitter.Config = Program.UploadersConfig.TwitterClientConfig;
                        twitter.ShowDialog();
                    }
                }
            }
        }

        public UploadResult UploadImage(Stream stream, string fileName)
        {
            ImageUploader imageUploader = null;

            switch (Info.ImageDestination)
            {
                case ImageDestination.ImageShack:
                    imageUploader = new ImageShackUploader(ApiKeys.ImageShackKey, Program.UploadersConfig.ImageShackAccountType,
                        Program.UploadersConfig.ImageShackRegistrationCode)
                    {
                        IsPublic = Program.UploadersConfig.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageDestination.TinyPic:
                    imageUploader = new TinyPicUploader(ApiKeys.TinyPicID, ApiKeys.TinyPicKey, Program.UploadersConfig.TinyPicAccountType,
                        Program.UploadersConfig.TinyPicRegistrationCode);
                    break;
                case ImageDestination.Imgur:
                    imageUploader = new Imgur(Program.UploadersConfig.ImgurAccountType, ApiKeys.ImgurAnonymousKey, Program.UploadersConfig.ImgurOAuthInfo)
                    {
                        ThumbnailType = Program.UploadersConfig.ImgurThumbnailType
                    };
                    break;
                case ImageDestination.Flickr:
                    imageUploader = new FlickrUploader(ApiKeys.FlickrKey, ApiKeys.FlickrSecret, Program.UploadersConfig.FlickrAuthInfo, Program.UploadersConfig.FlickrSettings);
                    break;
                case ImageDestination.Photobucket:
                    imageUploader = new Photobucket(Program.UploadersConfig.PhotobucketOAuthInfo, Program.UploadersConfig.PhotobucketAccountInfo);
                    break;
                case ImageDestination.Picasa:
                    imageUploader = new Picasa(Program.UploadersConfig.PicasaOAuthInfo);
                    break;
                case ImageDestination.UploadScreenshot:
                    imageUploader = new UploadScreenshot(ApiKeys.UploadScreenshotKey);
                    break;
                case ImageDestination.Twitpic:
                    int indexTwitpic = Program.UploadersConfig.TwitterSelectedAccount;

                    if (Program.UploadersConfig.TwitterOAuthInfoList != null && Program.UploadersConfig.TwitterOAuthInfoList.IsValidIndex(indexTwitpic))
                    {
                        imageUploader = new TwitPicUploader(ApiKeys.TwitPicKey, Program.UploadersConfig.TwitterOAuthInfoList[indexTwitpic])
                        {
                            TwitPicThumbnailMode = Program.UploadersConfig.TwitPicThumbnailMode,
                            ShowFull = Program.UploadersConfig.TwitPicShowFull
                        };
                    }
                    break;
                case ImageDestination.Twitsnaps:
                    int indexTwitsnaps = Program.UploadersConfig.TwitterSelectedAccount;

                    if (Program.UploadersConfig.TwitterOAuthInfoList.IsValidIndex(indexTwitsnaps))
                    {
                        imageUploader = new TwitSnapsUploader(ApiKeys.TwitsnapsKey, Program.UploadersConfig.TwitterOAuthInfoList[indexTwitsnaps]);
                    }
                    break;
                case ImageDestination.yFrog:
                    YfrogOptions yFrogOptions = new YfrogOptions(ApiKeys.ImageShackKey);
                    yFrogOptions.Username = Program.UploadersConfig.YFrogUsername;
                    yFrogOptions.Password = Program.UploadersConfig.YFrogPassword;
                    yFrogOptions.Source = Application.ProductName;
                    imageUploader = new YfrogUploader(yFrogOptions);
                    break;
                case ImageDestination.Immio:
                    imageUploader = new ImmioUploader();
                    break;
            }

            if (imageUploader != null)
            {
                PrepareUploader(imageUploader);
                return imageUploader.Upload(stream, fileName);
            }

            return null;
        }

        public UploadResult UploadText(Stream stream)
        {
            TextUploader textUploader = null;

            switch (Info.TextDestination)
            {
                case TextDestination.Pastebin:
                    textUploader = new Pastebin(ApiKeys.PastebinKey, Program.UploadersConfig.PastebinSettings);
                    break;
                case TextDestination.PastebinCA:
                    textUploader = new Pastebin_ca(ApiKeys.PastebinCaKey);
                    break;
                case TextDestination.Paste2:
                    textUploader = new Paste2();
                    break;
                case TextDestination.Slexy:
                    textUploader = new Slexy();
                    break;
                case TextDestination.Pastee:
                    textUploader = new Pastee();
                    break;
                case TextDestination.Paste_ee:
                    textUploader = new Paste_ee(Program.UploadersConfig.Paste_eeUserAPIKey);
                    break;
            }

            if (textUploader != null)
            {
                PrepareUploader(textUploader);
                return textUploader.UploadText(stream);
            }

            return null;
        }

        public UploadResult UploadFile(Stream stream, string fileName)
        {
            FileUploader fileUploader = null;

            switch (Info.FileDestination)
            {
                case FileDestination.Dropbox:
                    NameParser parser = new NameParser(NameParserType.URL);
                    string uploadPath = parser.Convert(Dropbox.TidyUploadPath(Program.UploadersConfig.DropboxUploadPath));
                    fileUploader = new Dropbox(Program.UploadersConfig.DropboxOAuthInfo, uploadPath, Program.UploadersConfig.DropboxAccountInfo)
                    {
                        AutoCreateShareableLink = Program.UploadersConfig.DropboxAutoCreateShareableLink
                    };
                    break;
                case FileDestination.RapidShare:
                    fileUploader = new RapidShare(Program.UploadersConfig.RapidShareUsername, Program.UploadersConfig.RapidSharePassword,
                        Program.UploadersConfig.RapidShareFolderID);
                    break;
                case FileDestination.SendSpace:
                    fileUploader = new SendSpace(ApiKeys.SendSpaceKey);
                    switch (Program.UploadersConfig.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ApiKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ApiKeys.SendSpaceKey, Program.UploadersConfig.SendSpaceUsername, Program.UploadersConfig.SendSpacePassword);
                            break;
                    }
                    break;
                case FileDestination.Minus:
                    fileUploader = new Minus(Program.UploadersConfig.MinusConfig, new OAuthInfo(ApiKeys.MinusConsumerKey, ApiKeys.MinusConsumerSecret));
                    break;
                case FileDestination.Box:
                    fileUploader = new Box(ApiKeys.BoxKey)
                    {
                        AuthToken = Program.UploadersConfig.BoxAuthToken,
                        FolderID = Program.UploadersConfig.BoxFolderID,
                        Share = Program.UploadersConfig.BoxShare
                    };
                    break;
                case FileDestination.Ge_tt:
                    if (Program.UploadersConfig.IsActive(FileDestination.Ge_tt))
                    {
                        fileUploader = new Ge_tt(ApiKeys.Ge_ttKey)
                        {
                            AccessToken = Program.UploadersConfig.Ge_ttLogin.AccessToken
                        };
                    }
                    break;
                case FileDestination.CustomUploader:
                    if (Program.UploadersConfig.CustomUploadersList.IsValidIndex(Program.UploadersConfig.CustomUploaderSelected))
                    {
                        fileUploader = new CustomUploader(Program.UploadersConfig.CustomUploadersList[Program.UploadersConfig.CustomUploaderSelected]);
                    }
                    break;
                case FileDestination.FTP:
                    int index = Program.UploadersConfig.GetFTPIndex(Info.DataType);

                    FTPAccount account = Program.UploadersConfig.FTPAccountList.ReturnIfValidIndex(index);

                    if (account != null)
                    {
                        if (account.Protocol == FTPProtocol.SFTP)
                        {
                            fileUploader = new SFTP(account);
                        }
                        else
                        {
                            fileUploader = new FTPUploader(account);
                        }
                    }
                    break;
                case FileDestination.SharedFolder:
                    int idLocalhost = Program.UploadersConfig.GetLocalhostIndex(Info.DataType);
                    if (Program.UploadersConfig.LocalhostAccountList.IsValidIndex(idLocalhost))
                    {
                        fileUploader = new SharedFolderUploader(Program.UploadersConfig.LocalhostAccountList[idLocalhost]);
                    }
                    break;
                case FileDestination.Email:
                    using (EmailForm emailForm = new EmailForm(Program.UploadersConfig.EmailRememberLastTo ? Program.UploadersConfig.EmailLastTo : string.Empty,
                        Program.UploadersConfig.EmailDefaultSubject, Program.UploadersConfig.EmailDefaultBody))
                    {
                        if (emailForm.ShowDialog() == DialogResult.OK)
                        {
                            if (Program.UploadersConfig.EmailRememberLastTo)
                            {
                                Program.UploadersConfig.EmailLastTo = emailForm.ToEmail;
                            }

                            fileUploader = new Email
                            {
                                SmtpServer = Program.UploadersConfig.EmailSmtpServer,
                                SmtpPort = Program.UploadersConfig.EmailSmtpPort,
                                FromEmail = Program.UploadersConfig.EmailFrom,
                                Password = Program.UploadersConfig.EmailPassword,
                                ToEmail = emailForm.ToEmail,
                                Subject = emailForm.Subject,
                                Body = emailForm.Body
                            };
                        }
                        else
                        {
                            IsStopped = true;
                        }
                    }
                    break;
            }

            if (fileUploader != null)
            {
                PrepareUploader(fileUploader);
                return fileUploader.Upload(stream, fileName);
            }

            return null;
        }

        public string ShortenURL(string url)
        {
            URLShortener urlShortener = null;

            switch (Info.URLShortenerDestination)
            {
                case UrlShortenerType.BITLY:
                    urlShortener = new BitlyURLShortener(ApiKeys.BitlyLogin, ApiKeys.BitlyKey);
                    break;
                case UrlShortenerType.Google:
                    urlShortener = new GoogleURLShortener(Program.UploadersConfig.GoogleURLShortenerAccountType, ApiKeys.GoogleApiKey,
                        Program.UploadersConfig.GoogleURLShortenerOAuthInfo);
                    break;
                case UrlShortenerType.ISGD:
                    urlShortener = new IsgdURLShortener();
                    break;
                case UrlShortenerType.Jmp:
                    urlShortener = new JmpURLShortener(ApiKeys.BitlyLogin, ApiKeys.BitlyKey);
                    break;
                /*case UrlShortenerType.THREELY:
                    urlShortener = new ThreelyURLShortener(Program.ThreelyKey);
                    break;*/
                case UrlShortenerType.TINYURL:
                    urlShortener = new TinyURLShortener();
                    break;
                case UrlShortenerType.TURL:
                    urlShortener = new TurlURLShortener();
                    break;
            }

            if (urlShortener != null)
            {
                return urlShortener.ShortenURL(url);
            }

            return null;
        }

        private void ThreadCompleted()
        {
            OnUploadCompleted();
        }

        private void PrepareUploader(Uploader currentUploader)
        {
            uploader = currentUploader;
            uploader.BufferSize = (int)Math.Pow(2, Program.Settings.BufferSizePower) * 1024;
            uploader.ProgressChanged += new Uploader.ProgressEventHandler(uploader_ProgressChanged);
        }

        private void uploader_ProgressChanged(ProgressManager progress)
        {
            if (progress != null)
            {
                Info.Progress = progress;

                if (threadWorker != null)
                {
                    threadWorker.InvokeAsync(OnUploadProgressChanged);
                }
                else
                {
                    OnUploadProgressChanged();
                }
            }
        }

        private void OnUploadPreparing()
        {
            Status = TaskStatus.Preparing;

            switch (Info.Job)
            {
                case TaskJob.ImageJob:
                case TaskJob.TextUpload:
                    Info.Status = "Preparing";
                    break;
                default:
                    Info.Status = "Starting";
                    break;
            }

            if (UploadPreparing != null)
            {
                UploadPreparing(this);
            }
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(this);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(this);
            }
        }

        private void OnUploadCompleted()
        {
            Status = TaskStatus.Completed;

            if (!IsStopped)
            {
                Info.Status = "Done";
            }
            else
            {
                Info.Status = "Stopped";
            }

            if (UploadCompleted != null)
            {
                UploadCompleted(this);
            }

            Dispose();
        }

        public void Dispose()
        {
            if (data != null) data.Dispose();
            if (tempImage != null) tempImage.Dispose();
        }
    }
}