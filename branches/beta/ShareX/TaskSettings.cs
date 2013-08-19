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
using ScreenCapture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UploadersLib;

namespace ShareX
{
    public class TaskSettings
    {
        public bool UseDefaultAfterCaptureJob { get; set; }
        public AfterCaptureTasks AfterCaptureJob { get; set; }

        public bool UseDefaultAfterUploadJob { get; set; }
        public AfterUploadTasks AfterUploadJob { get; set; }

        public bool UseDefaultDestinations { get; set; }
        public ImageDestination ImageDestination { get; set; }
        public TextDestination TextDestination { get; set; }
        public FileDestination FileDestination { get; set; }
        public UrlShortenerType URLShortenerDestination { get; set; }
        public SocialNetworkingService SocialNetworkingServiceDestination { get; set; }

        public bool DisableNotifications { get; set; }

        #region Image / Quality

        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 1024;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;
        public bool UseImageFormat2FileUpload = false;

        #endregion Image / Quality

        #region Image / Resize

        public bool ImageAutoResize = false;
        public bool ImageKeepAspectRatio = false;
        public bool ImageUseSmoothScaling = true;
        public ImageScaleType ImageScaleType = ImageScaleType.Percentage;
        public int ImageScalePercentageWidth = 100;
        public int ImageScalePercentageHeight = 100;
        public int ImageScaleToWidth = 100;
        public int ImageScaleToHeight = 100;
        public int ImageScaleSpecificWidth = 100;
        public int ImageScaleSpecificHeight = 100;

        #endregion Image / Resize

        #region Image / Effects

        public WatermarkConfig WatermarkConfig = new WatermarkConfig();

        public bool ImageEffectOnlyRegionCapture = true;
        public BorderType BorderType = BorderType.Outside;
        public XmlColor BorderColor = Color.Black;
        public int BorderSize = 1;
        public float ShadowDarkness = 0.6f;
        public int ShadowSize = 9;
        public Point ShadowOffset = new Point(0, 0);

        #endregion Image / Effects

        #region Capture / General

        public bool ShowCursor = false;
        public bool CaptureTransparent = true;
        public bool CaptureShadow = true;
        public int CaptureShadowOffset = 20;
        public bool CaptureClientArea = false;
        public bool IsDelayScreenshot = false;
        public decimal DelayScreenshot = 2.0m;
        public bool CaptureAutoHideTaskbar = false;

        #endregion Capture / General

        #region Capture / Shape capture

        public SurfaceOptions SurfaceOptions = new SurfaceOptions();

        #endregion Capture / Shape capture

        #region Capture / Screen recorder

        public bool ScreenRecorderHotkeyStartInstantly = false;

        #endregion Capture / Screen recorder

        #region Actions

        public List<ExternalProgram> ExternalPrograms = new List<ExternalProgram>();

        #endregion Actions

        #region Upload / Name pattern

        public string NameFormatPattern = "%y-%mo-%d_%h-%mi-%s"; // Test: %y %mo %mon %mon2 %d %h %mi %s %ms %w %w2 %pm %rn %ra %width %height %app %ver
        public string NameFormatPatternActiveWindow = "%t_%y-%mo-%d_%h-%mi-%s";
        public int AutoIncrementNumber = 0;
        public bool FileUploadUseNamePattern = false;
        public string ClipboardFormat = "<a href=\"%url\"><img src=\"%thumbnailurl\" alt=\"\" title\"\" /></a>";

        #endregion Upload / Name pattern

        #region Upload / Clipboard upload

        public bool ShowClipboardContentViewer = true;
        public bool ClipboardUploadAutoDetectURL = true;
        public bool ClipboardUploadUseAfterCaptureTasks = false;
        public bool ClipboardUploadExcludeImageEffects = true;

        #endregion Upload / Clipboard upload

        #region Upload / Watch folder

        public bool WatchFolderEnabled = false;
        public List<WatchFolder> WatchFolderList = new List<WatchFolder>();

        #endregion Upload / Watch folder

        #region ScreenRecord Form

        public int ScreenRecordFPS = 5;
        public bool ScreenRecordFixedDuration = true;
        public float ScreenRecordDuration = 3f;
        public ScreenRecordOutput ScreenRecordOutput = ScreenRecordOutput.GIF;
        public bool ScreenRecordAutoUpload = true;

        public string ScreenRecordCommandLinePath = "x264.exe";
        public string ScreenRecordCommandLineArgs = "--output %output %input";
        public string ScreenRecordCommandLineOutputExtension = "mp4";

        #endregion ScreenRecord Form

        public TaskSettings(bool useDefaultSettings = false)
        {
            SetDefaultSettings(true);
            UseDefaultAfterCaptureJob = useDefaultSettings;
            UseDefaultAfterUploadJob = useDefaultSettings;
            UseDefaultDestinations = useDefaultSettings;
        }

        public bool SetDefaultSettings(bool forceDefaultSettings = false)
        {
            if (Program.Settings != null)
            {
                if (UseDefaultAfterCaptureJob || forceDefaultSettings)
                {
                    AfterCaptureJob = Program.Settings.AfterCaptureTasks;
                }

                if (UseDefaultAfterUploadJob || forceDefaultSettings)
                {
                    AfterUploadJob = Program.Settings.AfterUploadTasks;
                }

                if (UseDefaultDestinations || forceDefaultSettings)
                {
                    ImageDestination = Program.Settings.ImageUploaderDestination;
                    TextDestination = Program.Settings.TextUploaderDestination;
                    FileDestination = Program.Settings.FileUploaderDestination;
                    URLShortenerDestination = Program.Settings.URLShortenerDestination;
                    SocialNetworkingServiceDestination = Program.Settings.SocialServiceDestination;
                }

                return true;
            }

            return false;
        }

        public TaskSettings Clone()
        {
            return (TaskSettings)MemberwiseClone();
        }
    }
}