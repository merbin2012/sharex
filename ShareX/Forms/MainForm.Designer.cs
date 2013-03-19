﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2008-2011 ShareX Developers

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

namespace ShareX
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbFileUpload = new System.Windows.Forms.ToolStripButton();
            this.tsddbCapture = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindowRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRoundedRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTriangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiamond = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFreeHand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLastRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbAfterCaptureTasks = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbAfterUploadTasks = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbDestinations = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiImageUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTextUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiURLShorteners = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSocialServices = new System.Windows.Forms.ToolStripMenuItem();
            this.tssDestinations1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUploadersConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDebug = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiTestImageUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestTextUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestFileUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestURLShortener = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestShapeCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.tssMain1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiScreenRecorder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScreenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHashCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbScreenshotsFolder = new System.Windows.Forms.ToolStripButton();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbImageHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbDonate = new System.Windows.Forms.ToolStripButton();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.lblSplitter = new System.Windows.Forms.Label();
            this.lvUploads = new HelpersLib.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chElapsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRemaining = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUploaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbPreview = new HelpersLib.MyPictureBox();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrayClipboardUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFileUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayWindowRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayRoundedRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayTriangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayDiamond = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFreeHand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayLastRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayAfterCaptureTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayAfterUploadTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayDestinations = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayImageUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayTextUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFileUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayURLShorteners = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTraySocialServices = new System.Windows.Forms.ToolStripMenuItem();
            this.tssTrayDestinations1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayUploadersConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tssTray1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayScreenRecorder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayScreenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScreenshotsFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayImageHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTraySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tssTray2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUploadInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiStopUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenShortenedURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenThumbnailURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenDeletionURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tssOpen1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyShortenedURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyThumbnailURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyDeletionURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyText = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyHTMLLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyHTMLImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyHTMLLinkedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyForumLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyForumImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyForumLinkedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyFilePath = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFileNameWithExtension = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowErrors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowResponse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUploadSelectedFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearList = new System.Windows.Forms.ToolStripMenuItem();
            this.tssUploadInfo1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUploadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayHashCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.cmsTray.SuspendLayout();
            this.cmsUploadInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.CanOverflow = false;
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClipboardUpload,
            this.tsbFileUpload,
            this.tsddbCapture,
            this.tsddbAfterCaptureTasks,
            this.tsddbAfterUploadTasks,
            this.tsddbDestinations,
            this.tsbDebug,
            this.tssMain1,
            this.tsddbTools,
            this.tsbScreenshotsFolder,
            this.tsbHistory,
            this.tsbImageHistory,
            this.tsbSettings,
            this.tsbAbout,
            this.tsbDonate});
            this.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(6);
            this.tsMain.ShowItemToolTips = false;
            this.tsMain.Size = new System.Drawing.Size(160, 362);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbClipboardUpload
            // 
            this.tsbClipboardUpload.Image = global::ShareX.Properties.Resources.clipboard_plus;
            this.tsbClipboardUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(147, 20);
            this.tsbClipboardUpload.Text = "Clipboard upload...";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsbFileUpload
            // 
            this.tsbFileUpload.Image = global::ShareX.Properties.Resources.folder_plus;
            this.tsbFileUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(147, 20);
            this.tsbFileUpload.Text = "File upload...";
            this.tsbFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsddbCapture
            // 
            this.tsddbCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFullscreen,
            this.tsmiWindow,
            this.tsmiWindowRectangle,
            this.tsmiRectangle,
            this.tsmiRoundedRectangle,
            this.tsmiEllipse,
            this.tsmiTriangle,
            this.tsmiDiamond,
            this.tsmiPolygon,
            this.tsmiFreeHand,
            this.tsmiLastRegion});
            this.tsddbCapture.Image = global::ShareX.Properties.Resources.camera;
            this.tsddbCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbCapture.Name = "tsddbCapture";
            this.tsddbCapture.Size = new System.Drawing.Size(147, 20);
            this.tsddbCapture.Text = "Capture";
            this.tsddbCapture.DropDownOpening += new System.EventHandler(this.tsddbCapture_DropDownOpening);
            // 
            // tsmiFullscreen
            // 
            this.tsmiFullscreen.Image = global::ShareX.Properties.Resources.Fullscreen;
            this.tsmiFullscreen.Name = "tsmiFullscreen";
            this.tsmiFullscreen.Size = new System.Drawing.Size(186, 22);
            this.tsmiFullscreen.Text = "Fullscreen";
            this.tsmiFullscreen.Click += new System.EventHandler(this.tsmiFullscreen_Click);
            // 
            // tsmiWindow
            // 
            this.tsmiWindow.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiWindow.Name = "tsmiWindow";
            this.tsmiWindow.Size = new System.Drawing.Size(186, 22);
            this.tsmiWindow.Text = "Window";
            // 
            // tsmiWindowRectangle
            // 
            this.tsmiWindowRectangle.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiWindowRectangle.Name = "tsmiWindowRectangle";
            this.tsmiWindowRectangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiWindowRectangle.Text = "Window && Rectangle";
            this.tsmiWindowRectangle.Click += new System.EventHandler(this.tsmiWindowRectangle_Click);
            // 
            // tsmiRectangle
            // 
            this.tsmiRectangle.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiRectangle.Name = "tsmiRectangle";
            this.tsmiRectangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiRectangle.Text = "Rectangle";
            this.tsmiRectangle.Click += new System.EventHandler(this.tsmiRectangle_Click);
            // 
            // tsmiRoundedRectangle
            // 
            this.tsmiRoundedRectangle.Image = global::ShareX.Properties.Resources.RoundedRectangle;
            this.tsmiRoundedRectangle.Name = "tsmiRoundedRectangle";
            this.tsmiRoundedRectangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiRoundedRectangle.Text = "Rounded Rectangle";
            this.tsmiRoundedRectangle.Click += new System.EventHandler(this.tsmiRoundedRectangle_Click);
            // 
            // tsmiEllipse
            // 
            this.tsmiEllipse.Image = global::ShareX.Properties.Resources.Ellipse;
            this.tsmiEllipse.Name = "tsmiEllipse";
            this.tsmiEllipse.Size = new System.Drawing.Size(186, 22);
            this.tsmiEllipse.Text = "Ellipse";
            this.tsmiEllipse.Click += new System.EventHandler(this.tsmiEllipse_Click);
            // 
            // tsmiTriangle
            // 
            this.tsmiTriangle.Image = global::ShareX.Properties.Resources.Triangle;
            this.tsmiTriangle.Name = "tsmiTriangle";
            this.tsmiTriangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiTriangle.Text = "Triangle";
            this.tsmiTriangle.Click += new System.EventHandler(this.tsmiTriangle_Click);
            // 
            // tsmiDiamond
            // 
            this.tsmiDiamond.Image = global::ShareX.Properties.Resources.Diamond;
            this.tsmiDiamond.Name = "tsmiDiamond";
            this.tsmiDiamond.Size = new System.Drawing.Size(186, 22);
            this.tsmiDiamond.Text = "Diamond";
            this.tsmiDiamond.Click += new System.EventHandler(this.tsmiDiamond_Click);
            // 
            // tsmiPolygon
            // 
            this.tsmiPolygon.Image = global::ShareX.Properties.Resources.Polygon;
            this.tsmiPolygon.Name = "tsmiPolygon";
            this.tsmiPolygon.Size = new System.Drawing.Size(186, 22);
            this.tsmiPolygon.Text = "Polygon";
            this.tsmiPolygon.Click += new System.EventHandler(this.tsmiPolygon_Click);
            // 
            // tsmiFreeHand
            // 
            this.tsmiFreeHand.Image = global::ShareX.Properties.Resources.FreeHand;
            this.tsmiFreeHand.Name = "tsmiFreeHand";
            this.tsmiFreeHand.Size = new System.Drawing.Size(186, 22);
            this.tsmiFreeHand.Text = "Free Hand";
            this.tsmiFreeHand.Click += new System.EventHandler(this.tsmiFreeHand_Click);
            // 
            // tsmiLastRegion
            // 
            this.tsmiLastRegion.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiLastRegion.Name = "tsmiLastRegion";
            this.tsmiLastRegion.Size = new System.Drawing.Size(186, 22);
            this.tsmiLastRegion.Text = "Last Region";
            this.tsmiLastRegion.Click += new System.EventHandler(this.tsmiLastRegion_Click);
            // 
            // tsddbAfterCaptureTasks
            // 
            this.tsddbAfterCaptureTasks.Image = global::ShareX.Properties.Resources.image_export;
            this.tsddbAfterCaptureTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbAfterCaptureTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbAfterCaptureTasks.Name = "tsddbAfterCaptureTasks";
            this.tsddbAfterCaptureTasks.Size = new System.Drawing.Size(147, 20);
            this.tsddbAfterCaptureTasks.Text = "After capture";
            // 
            // tsddbAfterUploadTasks
            // 
            this.tsddbAfterUploadTasks.Image = global::ShareX.Properties.Resources.upload_cloud;
            this.tsddbAfterUploadTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbAfterUploadTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbAfterUploadTasks.Name = "tsddbAfterUploadTasks";
            this.tsddbAfterUploadTasks.Size = new System.Drawing.Size(147, 20);
            this.tsddbAfterUploadTasks.Text = "After upload";
            // 
            // tsddbDestinations
            // 
            this.tsddbDestinations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImageUploaders,
            this.tsmiTextUploaders,
            this.tsmiFileUploaders,
            this.tsmiURLShorteners,
            this.tsmiSocialServices,
            this.tssDestinations1,
            this.tsmiUploadersConfig});
            this.tsddbDestinations.Image = global::ShareX.Properties.Resources.drive_globe;
            this.tsddbDestinations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestinations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestinations.Name = "tsddbDestinations";
            this.tsddbDestinations.Size = new System.Drawing.Size(147, 20);
            this.tsddbDestinations.Text = "Destinations";
            this.tsddbDestinations.DropDownOpened += new System.EventHandler(this.tsddbDestinations_DropDownOpened);
            // 
            // tsmiImageUploaders
            // 
            this.tsmiImageUploaders.Image = global::ShareX.Properties.Resources.image;
            this.tsmiImageUploaders.Name = "tsmiImageUploaders";
            this.tsmiImageUploaders.Size = new System.Drawing.Size(212, 22);
            this.tsmiImageUploaders.Text = "Image uploaders";
            // 
            // tsmiTextUploaders
            // 
            this.tsmiTextUploaders.Image = global::ShareX.Properties.Resources.notebook;
            this.tsmiTextUploaders.Name = "tsmiTextUploaders";
            this.tsmiTextUploaders.Size = new System.Drawing.Size(212, 22);
            this.tsmiTextUploaders.Text = "Text uploaders";
            // 
            // tsmiFileUploaders
            // 
            this.tsmiFileUploaders.Image = global::ShareX.Properties.Resources.application_block;
            this.tsmiFileUploaders.Name = "tsmiFileUploaders";
            this.tsmiFileUploaders.Size = new System.Drawing.Size(212, 22);
            this.tsmiFileUploaders.Text = "File uploaders";
            // 
            // tsmiURLShorteners
            // 
            this.tsmiURLShorteners.Image = global::ShareX.Properties.Resources.edit_scale;
            this.tsmiURLShorteners.Name = "tsmiURLShorteners";
            this.tsmiURLShorteners.Size = new System.Drawing.Size(212, 22);
            this.tsmiURLShorteners.Text = "URL shorteners";
            // 
            // tsmiSocialServices
            // 
            this.tsmiSocialServices.Image = global::ShareX.Properties.Resources.globe_share;
            this.tsmiSocialServices.Name = "tsmiSocialServices";
            this.tsmiSocialServices.Size = new System.Drawing.Size(212, 22);
            this.tsmiSocialServices.Text = "Social networking services";
            // 
            // tssDestinations1
            // 
            this.tssDestinations1.Name = "tssDestinations1";
            this.tssDestinations1.Size = new System.Drawing.Size(209, 6);
            // 
            // tsmiUploadersConfig
            // 
            this.tsmiUploadersConfig.Image = global::ShareX.Properties.Resources.gear;
            this.tsmiUploadersConfig.Name = "tsmiUploadersConfig";
            this.tsmiUploadersConfig.Size = new System.Drawing.Size(212, 22);
            this.tsmiUploadersConfig.Text = "Outputs configuration...";
            this.tsmiUploadersConfig.Click += new System.EventHandler(this.tsddbUploadersConfig_Click);
            // 
            // tsbDebug
            // 
            this.tsbDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestImageUpload,
            this.tsmiTestTextUpload,
            this.tsmiTestFileUpload,
            this.tsmiTestURLShortener,
            this.tsmiTestUploaders,
            this.tsmiTestShapeCapture});
            this.tsbDebug.Image = global::ShareX.Properties.Resources.block;
            this.tsbDebug.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDebug.Name = "tsbDebug";
            this.tsbDebug.Size = new System.Drawing.Size(147, 20);
            this.tsbDebug.Text = "Debug";
            // 
            // tsmiTestImageUpload
            // 
            this.tsmiTestImageUpload.Image = global::ShareX.Properties.Resources.image;
            this.tsmiTestImageUpload.Name = "tsmiTestImageUpload";
            this.tsmiTestImageUpload.Size = new System.Drawing.Size(173, 22);
            this.tsmiTestImageUpload.Text = "Test image upload";
            this.tsmiTestImageUpload.Click += new System.EventHandler(this.tsmiTestImageUpload_Click);
            // 
            // tsmiTestTextUpload
            // 
            this.tsmiTestTextUpload.Image = global::ShareX.Properties.Resources.notebook;
            this.tsmiTestTextUpload.Name = "tsmiTestTextUpload";
            this.tsmiTestTextUpload.Size = new System.Drawing.Size(173, 22);
            this.tsmiTestTextUpload.Text = "Test text upload";
            this.tsmiTestTextUpload.Click += new System.EventHandler(this.tsmiTestTextUpload_Click);
            // 
            // tsmiTestFileUpload
            // 
            this.tsmiTestFileUpload.Image = global::ShareX.Properties.Resources.application_block;
            this.tsmiTestFileUpload.Name = "tsmiTestFileUpload";
            this.tsmiTestFileUpload.Size = new System.Drawing.Size(173, 22);
            this.tsmiTestFileUpload.Text = "Test file upload";
            this.tsmiTestFileUpload.Click += new System.EventHandler(this.tsmiTestFileUpload_Click);
            // 
            // tsmiTestURLShortener
            // 
            this.tsmiTestURLShortener.Image = global::ShareX.Properties.Resources.edit_scale;
            this.tsmiTestURLShortener.Name = "tsmiTestURLShortener";
            this.tsmiTestURLShortener.Size = new System.Drawing.Size(173, 22);
            this.tsmiTestURLShortener.Text = "Test URL shortener";
            this.tsmiTestURLShortener.Click += new System.EventHandler(this.tsmiTestURLShortener_Click);
            // 
            // tsmiTestUploaders
            // 
            this.tsmiTestUploaders.Image = global::ShareX.Properties.Resources.application_browser;
            this.tsmiTestUploaders.Name = "tsmiTestUploaders";
            this.tsmiTestUploaders.Size = new System.Drawing.Size(173, 22);
            this.tsmiTestUploaders.Text = "Test uploaders";
            this.tsmiTestUploaders.Click += new System.EventHandler(this.tsmiTestUploaders_Click);
            // 
            // tsmiTestShapeCapture
            // 
            this.tsmiTestShapeCapture.Image = global::ShareX.Properties.Resources.camera;
            this.tsmiTestShapeCapture.Name = "tsmiTestShapeCapture";
            this.tsmiTestShapeCapture.Size = new System.Drawing.Size(173, 22);
            this.tsmiTestShapeCapture.Text = "Test shape capture";
            this.tsmiTestShapeCapture.Click += new System.EventHandler(this.tsmiTestShapeCapture_Click);
            // 
            // tssMain1
            // 
            this.tssMain1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tssMain1.Name = "tssMain1";
            this.tssMain1.Size = new System.Drawing.Size(147, 6);
            // 
            // tsddbTools
            // 
            this.tsddbTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiScreenRecorder,
            this.tsmiScreenColorPicker,
            this.tsmiHashCheck});
            this.tsddbTools.Image = global::ShareX.Properties.Resources.toolbox;
            this.tsddbTools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbTools.Name = "tsddbTools";
            this.tsddbTools.Size = new System.Drawing.Size(147, 20);
            this.tsddbTools.Text = "Tools";
            // 
            // tsmiScreenRecorder
            // 
            this.tsmiScreenRecorder.Image = global::ShareX.Properties.Resources.camcorder_image;
            this.tsmiScreenRecorder.Name = "tsmiScreenRecorder";
            this.tsmiScreenRecorder.Size = new System.Drawing.Size(183, 22);
            this.tsmiScreenRecorder.Text = "Screen recorder...";
            this.tsmiScreenRecorder.Click += new System.EventHandler(this.tsmiScreenRecorderGIF_Click);
            // 
            // tsmiScreenColorPicker
            // 
            this.tsmiScreenColorPicker.Image = global::ShareX.Properties.Resources.cursor_question;
            this.tsmiScreenColorPicker.Name = "tsmiScreenColorPicker";
            this.tsmiScreenColorPicker.Size = new System.Drawing.Size(183, 22);
            this.tsmiScreenColorPicker.Text = "Screen color picker...";
            this.tsmiScreenColorPicker.Click += new System.EventHandler(this.tsmiCursorHelper_Click);
            // 
            // tsmiHashCheck
            // 
            this.tsmiHashCheck.Image = global::ShareX.Properties.Resources.application_task;
            this.tsmiHashCheck.Name = "tsmiHashCheck";
            this.tsmiHashCheck.Size = new System.Drawing.Size(183, 22);
            this.tsmiHashCheck.Text = "Hash check...";
            this.tsmiHashCheck.Click += new System.EventHandler(this.tsmiHashCheck_Click);
            // 
            // tsbScreenshotsFolder
            // 
            this.tsbScreenshotsFolder.Image = global::ShareX.Properties.Resources.folder_open_image;
            this.tsbScreenshotsFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbScreenshotsFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScreenshotsFolder.Name = "tsbScreenshotsFolder";
            this.tsbScreenshotsFolder.Size = new System.Drawing.Size(147, 20);
            this.tsbScreenshotsFolder.Text = "Screenshots folder...";
            this.tsbScreenshotsFolder.Click += new System.EventHandler(this.tsbScreenshotsFolder_Click);
            // 
            // tsbHistory
            // 
            this.tsbHistory.Image = global::ShareX.Properties.Resources.address_book_blue;
            this.tsbHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(147, 20);
            this.tsbHistory.Text = "History...";
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // tsbImageHistory
            // 
            this.tsbImageHistory.Image = global::ShareX.Properties.Resources.application_icon_large;
            this.tsbImageHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbImageHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImageHistory.Name = "tsbImageHistory";
            this.tsbImageHistory.Size = new System.Drawing.Size(147, 20);
            this.tsbImageHistory.Text = "Image history...";
            this.tsbImageHistory.Click += new System.EventHandler(this.tsbImageHistory_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.Image = global::ShareX.Properties.Resources.application_form;
            this.tsbSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(147, 20);
            this.tsbSettings.Text = "Settings...";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Image = global::ShareX.Properties.Resources.application_browser;
            this.tsbAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(147, 20);
            this.tsbAbout.Text = "About...";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsbDonate
            // 
            this.tsbDonate.Image = global::ShareX.Properties.Resources.present;
            this.tsbDonate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(147, 20);
            this.tsbDonate.Text = "Donate...";
            this.tsbDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(160, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.lblSplitter);
            this.scMain.Panel1.Controls.Add(this.lvUploads);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.pbPreview);
            this.scMain.Panel2Collapsed = true;
            this.scMain.Size = new System.Drawing.Size(784, 362);
            this.scMain.SplitterDistance = 500;
            this.scMain.TabIndex = 1;
            this.scMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scMain_SplitterMoved);
            // 
            // lblSplitter
            // 
            this.lblSplitter.BackColor = System.Drawing.Color.Black;
            this.lblSplitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSplitter.Location = new System.Drawing.Point(0, 0);
            this.lblSplitter.Name = "lblSplitter";
            this.lblSplitter.Size = new System.Drawing.Size(1, 362);
            this.lblSplitter.TabIndex = 2;
            // 
            // lvUploads
            // 
            this.lvUploads.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chStatus,
            this.chProgress,
            this.chSpeed,
            this.chElapsed,
            this.chRemaining,
            this.chUploaderType,
            this.chHost,
            this.chURL});
            this.lvUploads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(0, 0);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.ShowItemToolTips = true;
            this.lvUploads.Size = new System.Drawing.Size(784, 362);
            this.lvUploads.TabIndex = 0;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            this.lvUploads.SelectedIndexChanged += new System.EventHandler(this.lvUploads_SelectedIndexChanged);
            this.lvUploads.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvUploads_KeyDown);
            this.lvUploads.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvUploads_MouseDoubleClick);
            this.lvUploads.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvUploads_MouseUp);
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            // 
            // chProgress
            // 
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 120;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Speed";
            this.chSpeed.Width = 70;
            // 
            // chElapsed
            // 
            this.chElapsed.Text = "Elapsed";
            this.chElapsed.Width = 50;
            // 
            // chRemaining
            // 
            this.chRemaining.Text = "Remaining";
            this.chRemaining.Width = 50;
            // 
            // chUploaderType
            // 
            this.chUploaderType.Text = "Type";
            this.chUploaderType.Width = 50;
            // 
            // chHost
            // 
            this.chHost.Text = "Host";
            this.chHost.Width = 100;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 234;
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.White;
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(96, 100);
            this.pbPreview.TabIndex = 0;
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmsTray;
            this.niTray.Text = "ShareX";
            this.niTray.BalloonTipClicked += new System.EventHandler(this.niTray_BalloonTipClicked);
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayClipboardUpload,
            this.tsmiTrayFileUpload,
            this.tsmiTrayCapture,
            this.tsmiTrayAfterCaptureTasks,
            this.tsmiTrayAfterUploadTasks,
            this.tsmiTrayDestinations,
            this.tssTray1,
            this.tsmiTrayTools,
            this.tsmiScreenshotsFolder,
            this.tsmiTrayHistory,
            this.tsmiTrayImageHistory,
            this.tsmiTraySettings,
            this.tsmiTrayAbout,
            this.tsmiTrayDonate,
            this.tssTray2,
            this.tsmiTrayExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(181, 346);
            // 
            // tsmiTrayClipboardUpload
            // 
            this.tsmiTrayClipboardUpload.Image = global::ShareX.Properties.Resources.clipboard_plus;
            this.tsmiTrayClipboardUpload.Name = "tsmiTrayClipboardUpload";
            this.tsmiTrayClipboardUpload.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayClipboardUpload.Text = "Clipboard upload...";
            this.tsmiTrayClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsmiTrayFileUpload
            // 
            this.tsmiTrayFileUpload.Image = global::ShareX.Properties.Resources.folder_plus;
            this.tsmiTrayFileUpload.Name = "tsmiTrayFileUpload";
            this.tsmiTrayFileUpload.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayFileUpload.Text = "File upload...";
            this.tsmiTrayFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsmiTrayCapture
            // 
            this.tsmiTrayCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayFullscreen,
            this.tsmiTrayWindow,
            this.tsmiTrayWindowRectangle,
            this.tsmiTrayRectangle,
            this.tsmiTrayRoundedRectangle,
            this.tsmiTrayEllipse,
            this.tsmiTrayTriangle,
            this.tsmiTrayDiamond,
            this.tsmiTrayPolygon,
            this.tsmiTrayFreeHand,
            this.tsmiTrayLastRegion});
            this.tsmiTrayCapture.Image = global::ShareX.Properties.Resources.camera;
            this.tsmiTrayCapture.Name = "tsmiTrayCapture";
            this.tsmiTrayCapture.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayCapture.Text = "Capture";
            this.tsmiTrayCapture.DropDownOpening += new System.EventHandler(this.tsmiCapture_DropDownOpening);
            // 
            // tsmiTrayFullscreen
            // 
            this.tsmiTrayFullscreen.Image = global::ShareX.Properties.Resources.Fullscreen;
            this.tsmiTrayFullscreen.Name = "tsmiTrayFullscreen";
            this.tsmiTrayFullscreen.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayFullscreen.Text = "Fullscreen";
            this.tsmiTrayFullscreen.Click += new System.EventHandler(this.tsmiTrayFullscreen_Click);
            // 
            // tsmiTrayWindow
            // 
            this.tsmiTrayWindow.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiTrayWindow.Name = "tsmiTrayWindow";
            this.tsmiTrayWindow.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayWindow.Text = "Window";
            // 
            // tsmiTrayWindowRectangle
            // 
            this.tsmiTrayWindowRectangle.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiTrayWindowRectangle.Name = "tsmiTrayWindowRectangle";
            this.tsmiTrayWindowRectangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayWindowRectangle.Text = "Window && Rectangle";
            this.tsmiTrayWindowRectangle.Click += new System.EventHandler(this.tsmiTrayWindowRectangle_Click);
            // 
            // tsmiTrayRectangle
            // 
            this.tsmiTrayRectangle.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiTrayRectangle.Name = "tsmiTrayRectangle";
            this.tsmiTrayRectangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayRectangle.Text = "Rectangle";
            this.tsmiTrayRectangle.Click += new System.EventHandler(this.tsmiTrayRectangle_Click);
            // 
            // tsmiTrayRoundedRectangle
            // 
            this.tsmiTrayRoundedRectangle.Image = global::ShareX.Properties.Resources.RoundedRectangle;
            this.tsmiTrayRoundedRectangle.Name = "tsmiTrayRoundedRectangle";
            this.tsmiTrayRoundedRectangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayRoundedRectangle.Text = "Rounded Rectangle";
            this.tsmiTrayRoundedRectangle.Click += new System.EventHandler(this.tsmiTrayRoundedRectangle_Click);
            // 
            // tsmiTrayEllipse
            // 
            this.tsmiTrayEllipse.Image = global::ShareX.Properties.Resources.Ellipse;
            this.tsmiTrayEllipse.Name = "tsmiTrayEllipse";
            this.tsmiTrayEllipse.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayEllipse.Text = "Ellipse";
            this.tsmiTrayEllipse.Click += new System.EventHandler(this.tsmiTrayEllipse_Click);
            // 
            // tsmiTrayTriangle
            // 
            this.tsmiTrayTriangle.Image = global::ShareX.Properties.Resources.Triangle;
            this.tsmiTrayTriangle.Name = "tsmiTrayTriangle";
            this.tsmiTrayTriangle.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayTriangle.Text = "Triangle";
            this.tsmiTrayTriangle.Click += new System.EventHandler(this.tsmiTrayTriangle_Click);
            // 
            // tsmiTrayDiamond
            // 
            this.tsmiTrayDiamond.Image = global::ShareX.Properties.Resources.Diamond;
            this.tsmiTrayDiamond.Name = "tsmiTrayDiamond";
            this.tsmiTrayDiamond.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayDiamond.Text = "Diamond";
            this.tsmiTrayDiamond.Click += new System.EventHandler(this.tsmiTrayDiamond_Click);
            // 
            // tsmiTrayPolygon
            // 
            this.tsmiTrayPolygon.Image = global::ShareX.Properties.Resources.Polygon;
            this.tsmiTrayPolygon.Name = "tsmiTrayPolygon";
            this.tsmiTrayPolygon.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayPolygon.Text = "Polygon";
            this.tsmiTrayPolygon.Click += new System.EventHandler(this.tsmiTrayPolygon_Click);
            // 
            // tsmiTrayFreeHand
            // 
            this.tsmiTrayFreeHand.Image = global::ShareX.Properties.Resources.FreeHand;
            this.tsmiTrayFreeHand.Name = "tsmiTrayFreeHand";
            this.tsmiTrayFreeHand.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayFreeHand.Text = "Free Hand";
            this.tsmiTrayFreeHand.Click += new System.EventHandler(this.tsmiTrayFreeHand_Click);
            // 
            // tsmiTrayLastRegion
            // 
            this.tsmiTrayLastRegion.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiTrayLastRegion.Name = "tsmiTrayLastRegion";
            this.tsmiTrayLastRegion.Size = new System.Drawing.Size(186, 22);
            this.tsmiTrayLastRegion.Text = "Last Region";
            this.tsmiTrayLastRegion.Click += new System.EventHandler(this.tsmiTrayLastRegion_Click);
            // 
            // tsmiTrayAfterCaptureTasks
            // 
            this.tsmiTrayAfterCaptureTasks.Image = global::ShareX.Properties.Resources.image_export;
            this.tsmiTrayAfterCaptureTasks.Name = "tsmiTrayAfterCaptureTasks";
            this.tsmiTrayAfterCaptureTasks.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayAfterCaptureTasks.Text = "After capture";
            // 
            // tsmiTrayAfterUploadTasks
            // 
            this.tsmiTrayAfterUploadTasks.Image = global::ShareX.Properties.Resources.upload_cloud;
            this.tsmiTrayAfterUploadTasks.Name = "tsmiTrayAfterUploadTasks";
            this.tsmiTrayAfterUploadTasks.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayAfterUploadTasks.Text = "After upload";
            // 
            // tsmiTrayDestinations
            // 
            this.tsmiTrayDestinations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayImageUploaders,
            this.tsmiTrayTextUploaders,
            this.tsmiTrayFileUploaders,
            this.tsmiTrayURLShorteners,
            this.tsmiTraySocialServices,
            this.tssTrayDestinations1,
            this.tsmiTrayUploadersConfig});
            this.tsmiTrayDestinations.Image = global::ShareX.Properties.Resources.drive_globe;
            this.tsmiTrayDestinations.Name = "tsmiTrayDestinations";
            this.tsmiTrayDestinations.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayDestinations.Text = "Destinations";
            this.tsmiTrayDestinations.DropDownOpened += new System.EventHandler(this.tsddbDestinations_DropDownOpened);
            // 
            // tsmiTrayImageUploaders
            // 
            this.tsmiTrayImageUploaders.Image = global::ShareX.Properties.Resources.image;
            this.tsmiTrayImageUploaders.Name = "tsmiTrayImageUploaders";
            this.tsmiTrayImageUploaders.Size = new System.Drawing.Size(212, 22);
            this.tsmiTrayImageUploaders.Text = "Image uploaders";
            // 
            // tsmiTrayTextUploaders
            // 
            this.tsmiTrayTextUploaders.Image = global::ShareX.Properties.Resources.notebook;
            this.tsmiTrayTextUploaders.Name = "tsmiTrayTextUploaders";
            this.tsmiTrayTextUploaders.Size = new System.Drawing.Size(212, 22);
            this.tsmiTrayTextUploaders.Text = "Text uploaders";
            // 
            // tsmiTrayFileUploaders
            // 
            this.tsmiTrayFileUploaders.Image = global::ShareX.Properties.Resources.application_block;
            this.tsmiTrayFileUploaders.Name = "tsmiTrayFileUploaders";
            this.tsmiTrayFileUploaders.Size = new System.Drawing.Size(212, 22);
            this.tsmiTrayFileUploaders.Text = "File uploaders";
            // 
            // tsmiTrayURLShorteners
            // 
            this.tsmiTrayURLShorteners.Image = global::ShareX.Properties.Resources.edit_scale;
            this.tsmiTrayURLShorteners.Name = "tsmiTrayURLShorteners";
            this.tsmiTrayURLShorteners.Size = new System.Drawing.Size(212, 22);
            this.tsmiTrayURLShorteners.Text = "URL shorteners";
            // 
            // tsmiTraySocialServices
            // 
            this.tsmiTraySocialServices.Image = global::ShareX.Properties.Resources.globe_share;
            this.tsmiTraySocialServices.Name = "tsmiTraySocialServices";
            this.tsmiTraySocialServices.Size = new System.Drawing.Size(212, 22);
            this.tsmiTraySocialServices.Text = "Social networking services";
            // 
            // tssTrayDestinations1
            // 
            this.tssTrayDestinations1.Name = "tssTrayDestinations1";
            this.tssTrayDestinations1.Size = new System.Drawing.Size(209, 6);
            // 
            // tsmiTrayUploadersConfig
            // 
            this.tsmiTrayUploadersConfig.Image = global::ShareX.Properties.Resources.gear;
            this.tsmiTrayUploadersConfig.Name = "tsmiTrayUploadersConfig";
            this.tsmiTrayUploadersConfig.Size = new System.Drawing.Size(212, 22);
            this.tsmiTrayUploadersConfig.Text = "Outputs configuration...";
            this.tsmiTrayUploadersConfig.Click += new System.EventHandler(this.tsddbUploadersConfig_Click);
            // 
            // tssTray1
            // 
            this.tssTray1.Name = "tssTray1";
            this.tssTray1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiTrayTools
            // 
            this.tsmiTrayTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayScreenRecorder,
            this.tsmiTrayScreenColorPicker,
            this.tsmiTrayHashCheck});
            this.tsmiTrayTools.Image = global::ShareX.Properties.Resources.toolbox;
            this.tsmiTrayTools.Name = "tsmiTrayTools";
            this.tsmiTrayTools.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayTools.Text = "Tools";
            // 
            // tsmiTrayScreenRecorder
            // 
            this.tsmiTrayScreenRecorder.Image = global::ShareX.Properties.Resources.camcorder_image;
            this.tsmiTrayScreenRecorder.Name = "tsmiTrayScreenRecorder";
            this.tsmiTrayScreenRecorder.Size = new System.Drawing.Size(183, 22);
            this.tsmiTrayScreenRecorder.Text = "Screen recorder...";
            this.tsmiTrayScreenRecorder.Click += new System.EventHandler(this.tsmiScreenRecorderGIF_Click);
            // 
            // tsmiTrayScreenColorPicker
            // 
            this.tsmiTrayScreenColorPicker.Image = global::ShareX.Properties.Resources.cursor_question;
            this.tsmiTrayScreenColorPicker.Name = "tsmiTrayScreenColorPicker";
            this.tsmiTrayScreenColorPicker.Size = new System.Drawing.Size(183, 22);
            this.tsmiTrayScreenColorPicker.Text = "Screen color picker...";
            this.tsmiTrayScreenColorPicker.Click += new System.EventHandler(this.tsmiCursorHelper_Click);
            // 
            // tsmiScreenshotsFolder
            // 
            this.tsmiScreenshotsFolder.Image = global::ShareX.Properties.Resources.folder_open_image;
            this.tsmiScreenshotsFolder.Name = "tsmiScreenshotsFolder";
            this.tsmiScreenshotsFolder.Size = new System.Drawing.Size(180, 22);
            this.tsmiScreenshotsFolder.Text = "Screenshots folder...";
            this.tsmiScreenshotsFolder.Click += new System.EventHandler(this.tsbScreenshotsFolder_Click);
            // 
            // tsmiTrayHistory
            // 
            this.tsmiTrayHistory.Image = global::ShareX.Properties.Resources.address_book_blue;
            this.tsmiTrayHistory.Name = "tsmiTrayHistory";
            this.tsmiTrayHistory.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayHistory.Text = "History...";
            this.tsmiTrayHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // tsmiTrayImageHistory
            // 
            this.tsmiTrayImageHistory.Image = global::ShareX.Properties.Resources.application_icon_large;
            this.tsmiTrayImageHistory.Name = "tsmiTrayImageHistory";
            this.tsmiTrayImageHistory.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayImageHistory.Text = "Image history...";
            this.tsmiTrayImageHistory.Click += new System.EventHandler(this.tsbImageHistory_Click);
            // 
            // tsmiTraySettings
            // 
            this.tsmiTraySettings.Image = global::ShareX.Properties.Resources.application_form;
            this.tsmiTraySettings.Name = "tsmiTraySettings";
            this.tsmiTraySettings.Size = new System.Drawing.Size(180, 22);
            this.tsmiTraySettings.Text = "Settings...";
            this.tsmiTraySettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsmiTrayAbout
            // 
            this.tsmiTrayAbout.Image = global::ShareX.Properties.Resources.application_browser;
            this.tsmiTrayAbout.Name = "tsmiTrayAbout";
            this.tsmiTrayAbout.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayAbout.Text = "About...";
            this.tsmiTrayAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsmiTrayDonate
            // 
            this.tsmiTrayDonate.Image = global::ShareX.Properties.Resources.present;
            this.tsmiTrayDonate.Name = "tsmiTrayDonate";
            this.tsmiTrayDonate.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayDonate.Text = "Donate...";
            this.tsmiTrayDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            // 
            // tssTray2
            // 
            this.tssTray2.Name = "tssTray2";
            this.tssTray2.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiTrayExit
            // 
            this.tsmiTrayExit.Image = global::ShareX.Properties.Resources.cross_button;
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(180, 22);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.tsmiTrayExit_Click);
            // 
            // cmsUploadInfo
            // 
            this.cmsUploadInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStopUpload,
            this.tsmiOpen,
            this.tsmiCopy,
            this.tsmiShowErrors,
            this.tsmiShowResponse,
            this.tsmiUploadSelectedFile,
            this.tsmiClearList,
            this.tssUploadInfo1,
            this.tsmiUploadFile,
            this.tsmiShowPreview});
            this.cmsUploadInfo.Name = "cmsHistory";
            this.cmsUploadInfo.ShowImageMargin = false;
            this.cmsUploadInfo.Size = new System.Drawing.Size(164, 208);
            // 
            // tsmiStopUpload
            // 
            this.tsmiStopUpload.Name = "tsmiStopUpload";
            this.tsmiStopUpload.Size = new System.Drawing.Size(163, 22);
            this.tsmiStopUpload.Text = "Stop upload";
            this.tsmiStopUpload.Click += new System.EventHandler(this.tsmiStopUpload_Click);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenURL,
            this.tsmiOpenShortenedURL,
            this.tsmiOpenThumbnailURL,
            this.tsmiOpenDeletionURL,
            this.tssOpen1,
            this.tsmiOpenFile,
            this.tsmiOpenFolder});
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(163, 22);
            this.tsmiOpen.Text = "Open";
            // 
            // tsmiOpenURL
            // 
            this.tsmiOpenURL.Name = "tsmiOpenURL";
            this.tsmiOpenURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenURL.Text = "URL";
            this.tsmiOpenURL.Click += new System.EventHandler(this.tsmiOpenURL_Click);
            // 
            // tsmiOpenShortenedURL
            // 
            this.tsmiOpenShortenedURL.Name = "tsmiOpenShortenedURL";
            this.tsmiOpenShortenedURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenShortenedURL.Text = "Shortened URL";
            this.tsmiOpenShortenedURL.Click += new System.EventHandler(this.tsmiOpenShortenedURL_Click);
            // 
            // tsmiOpenThumbnailURL
            // 
            this.tsmiOpenThumbnailURL.Name = "tsmiOpenThumbnailURL";
            this.tsmiOpenThumbnailURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenThumbnailURL.Text = "Thumbnail URL";
            this.tsmiOpenThumbnailURL.Click += new System.EventHandler(this.tsmiOpenThumbnailURL_Click);
            // 
            // tsmiOpenDeletionURL
            // 
            this.tsmiOpenDeletionURL.Name = "tsmiOpenDeletionURL";
            this.tsmiOpenDeletionURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenDeletionURL.Text = "Deletion URL";
            this.tsmiOpenDeletionURL.Click += new System.EventHandler(this.tsmiOpenDeletionURL_Click);
            // 
            // tssOpen1
            // 
            this.tssOpen1.Name = "tssOpen1";
            this.tssOpen1.Size = new System.Drawing.Size(153, 6);
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenFile.Text = "File";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            // 
            // tsmiOpenFolder
            // 
            this.tsmiOpenFolder.Name = "tsmiOpenFolder";
            this.tsmiOpenFolder.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenFolder.Text = "Folder";
            this.tsmiOpenFolder.Click += new System.EventHandler(this.tsmiOpenFolder_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyURL,
            this.tsmiCopyShortenedURL,
            this.tsmiCopyThumbnailURL,
            this.tsmiCopyDeletionURL,
            this.tssCopy1,
            this.tsmiCopyFile,
            this.tsmiCopyImage,
            this.tsmiCopyText,
            this.tssCopy2,
            this.tsmiCopyHTMLLink,
            this.tsmiCopyHTMLImage,
            this.tsmiCopyHTMLLinkedImage,
            this.tssCopy3,
            this.tsmiCopyForumLink,
            this.tsmiCopyForumImage,
            this.tsmiCopyForumLinkedImage,
            this.tssCopy4,
            this.tsmiCopyFilePath,
            this.tsmiCopyFileName,
            this.tsmiCopyFileNameWithExtension,
            this.tsmiCopyFolder});
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(163, 22);
            this.tsmiCopy.Text = "Copy";
            // 
            // tsmiCopyURL
            // 
            this.tsmiCopyURL.Name = "tsmiCopyURL";
            this.tsmiCopyURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyURL.Text = "URL";
            this.tsmiCopyURL.Click += new System.EventHandler(this.tsmiCopyURL_Click);
            // 
            // tsmiCopyShortenedURL
            // 
            this.tsmiCopyShortenedURL.Name = "tsmiCopyShortenedURL";
            this.tsmiCopyShortenedURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyShortenedURL.Text = "Shortened URL";
            this.tsmiCopyShortenedURL.Click += new System.EventHandler(this.tsmiCopyShortenedURL_Click);
            // 
            // tsmiCopyThumbnailURL
            // 
            this.tsmiCopyThumbnailURL.Name = "tsmiCopyThumbnailURL";
            this.tsmiCopyThumbnailURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyThumbnailURL.Text = "Thumbnail URL";
            this.tsmiCopyThumbnailURL.Click += new System.EventHandler(this.tsmiCopyThumbnailURL_Click);
            // 
            // tsmiCopyDeletionURL
            // 
            this.tsmiCopyDeletionURL.Name = "tsmiCopyDeletionURL";
            this.tsmiCopyDeletionURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyDeletionURL.Text = "Deletion URL";
            this.tsmiCopyDeletionURL.Click += new System.EventHandler(this.tsmiCopyDeletionURL_Click);
            // 
            // tssCopy1
            // 
            this.tssCopy1.Name = "tssCopy1";
            this.tssCopy1.Size = new System.Drawing.Size(230, 6);
            // 
            // tsmiCopyFile
            // 
            this.tsmiCopyFile.Name = "tsmiCopyFile";
            this.tsmiCopyFile.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFile.Text = "File";
            this.tsmiCopyFile.Click += new System.EventHandler(this.tsmiCopyFile_Click);
            // 
            // tsmiCopyImage
            // 
            this.tsmiCopyImage.Name = "tsmiCopyImage";
            this.tsmiCopyImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyImage.Text = "Image (Bitmap)";
            this.tsmiCopyImage.Click += new System.EventHandler(this.tsmiCopyImage_Click);
            // 
            // tsmiCopyText
            // 
            this.tsmiCopyText.Name = "tsmiCopyText";
            this.tsmiCopyText.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyText.Text = "Text";
            this.tsmiCopyText.Click += new System.EventHandler(this.tsmiCopyText_Click);
            // 
            // tssCopy2
            // 
            this.tssCopy2.Name = "tssCopy2";
            this.tssCopy2.Size = new System.Drawing.Size(230, 6);
            // 
            // tsmiCopyHTMLLink
            // 
            this.tsmiCopyHTMLLink.Name = "tsmiCopyHTMLLink";
            this.tsmiCopyHTMLLink.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLLink.Text = "HTML link";
            this.tsmiCopyHTMLLink.Click += new System.EventHandler(this.tsmiCopyHTMLLink_Click);
            // 
            // tsmiCopyHTMLImage
            // 
            this.tsmiCopyHTMLImage.Name = "tsmiCopyHTMLImage";
            this.tsmiCopyHTMLImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLImage.Text = "HTML image";
            this.tsmiCopyHTMLImage.Click += new System.EventHandler(this.tsmiCopyHTMLImage_Click);
            // 
            // tsmiCopyHTMLLinkedImage
            // 
            this.tsmiCopyHTMLLinkedImage.Name = "tsmiCopyHTMLLinkedImage";
            this.tsmiCopyHTMLLinkedImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLLinkedImage.Text = "HTML linked image";
            this.tsmiCopyHTMLLinkedImage.Click += new System.EventHandler(this.tsmiCopyHTMLLinkedImage_Click);
            // 
            // tssCopy3
            // 
            this.tssCopy3.Name = "tssCopy3";
            this.tssCopy3.Size = new System.Drawing.Size(230, 6);
            // 
            // tsmiCopyForumLink
            // 
            this.tsmiCopyForumLink.Name = "tsmiCopyForumLink";
            this.tsmiCopyForumLink.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumLink.Text = "Forum (BBCode) link";
            this.tsmiCopyForumLink.Click += new System.EventHandler(this.tsmiCopyForumLink_Click);
            // 
            // tsmiCopyForumImage
            // 
            this.tsmiCopyForumImage.Name = "tsmiCopyForumImage";
            this.tsmiCopyForumImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumImage.Text = "Forum (BBCode) image";
            this.tsmiCopyForumImage.Click += new System.EventHandler(this.tsmiCopyForumImage_Click);
            // 
            // tsmiCopyForumLinkedImage
            // 
            this.tsmiCopyForumLinkedImage.Name = "tsmiCopyForumLinkedImage";
            this.tsmiCopyForumLinkedImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumLinkedImage.Text = "Forum (BBCode) linked image";
            this.tsmiCopyForumLinkedImage.Click += new System.EventHandler(this.tsmiCopyForumLinkedImage_Click);
            // 
            // tssCopy4
            // 
            this.tssCopy4.Name = "tssCopy4";
            this.tssCopy4.Size = new System.Drawing.Size(230, 6);
            // 
            // tsmiCopyFilePath
            // 
            this.tsmiCopyFilePath.Name = "tsmiCopyFilePath";
            this.tsmiCopyFilePath.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFilePath.Text = "File path";
            this.tsmiCopyFilePath.Click += new System.EventHandler(this.tsmiCopyFilePath_Click);
            // 
            // tsmiCopyFileName
            // 
            this.tsmiCopyFileName.Name = "tsmiCopyFileName";
            this.tsmiCopyFileName.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFileName.Text = "File name";
            this.tsmiCopyFileName.Click += new System.EventHandler(this.tsmiCopyFileName_Click);
            // 
            // tsmiCopyFileNameWithExtension
            // 
            this.tsmiCopyFileNameWithExtension.Name = "tsmiCopyFileNameWithExtension";
            this.tsmiCopyFileNameWithExtension.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFileNameWithExtension.Text = "File name with extension";
            this.tsmiCopyFileNameWithExtension.Click += new System.EventHandler(this.tsmiCopyFileNameWithExtension_Click);
            // 
            // tsmiCopyFolder
            // 
            this.tsmiCopyFolder.Name = "tsmiCopyFolder";
            this.tsmiCopyFolder.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFolder.Text = "Folder";
            this.tsmiCopyFolder.Click += new System.EventHandler(this.tsmiCopyFolder_Click);
            // 
            // tsmiShowErrors
            // 
            this.tsmiShowErrors.Name = "tsmiShowErrors";
            this.tsmiShowErrors.Size = new System.Drawing.Size(163, 22);
            this.tsmiShowErrors.Text = "Show errors";
            this.tsmiShowErrors.Click += new System.EventHandler(this.tsmiShowErrors_Click);
            // 
            // tsmiShowResponse
            // 
            this.tsmiShowResponse.Name = "tsmiShowResponse";
            this.tsmiShowResponse.Size = new System.Drawing.Size(163, 22);
            this.tsmiShowResponse.Text = "Show response";
            this.tsmiShowResponse.Click += new System.EventHandler(this.tsmiShowResponse_Click);
            // 
            // tsmiUploadSelectedFile
            // 
            this.tsmiUploadSelectedFile.Name = "tsmiUploadSelectedFile";
            this.tsmiUploadSelectedFile.Size = new System.Drawing.Size(163, 22);
            this.tsmiUploadSelectedFile.Text = "Upload";
            this.tsmiUploadSelectedFile.Click += new System.EventHandler(this.tsmiUploadSelectedFile_Click);
            // 
            // tsmiClearList
            // 
            this.tsmiClearList.Name = "tsmiClearList";
            this.tsmiClearList.Size = new System.Drawing.Size(163, 22);
            this.tsmiClearList.Text = "Clear list";
            this.tsmiClearList.Click += new System.EventHandler(this.tsmiClearList_Click);
            // 
            // tssUploadInfo1
            // 
            this.tssUploadInfo1.Name = "tssUploadInfo1";
            this.tssUploadInfo1.Size = new System.Drawing.Size(160, 6);
            // 
            // tsmiUploadFile
            // 
            this.tsmiUploadFile.Name = "tsmiUploadFile";
            this.tsmiUploadFile.Size = new System.Drawing.Size(163, 22);
            this.tsmiUploadFile.Text = "Upload file...";
            this.tsmiUploadFile.Click += new System.EventHandler(this.tsmiUploadFile_Click);
            // 
            // tsmiShowPreview
            // 
            this.tsmiShowPreview.Name = "tsmiShowPreview";
            this.tsmiShowPreview.Size = new System.Drawing.Size(163, 22);
            this.tsmiShowPreview.Text = "Show preview section";
            this.tsmiShowPreview.Click += new System.EventHandler(this.tsmiShowPreview_Click);
            // 
            // tsmiTrayHashCheck
            // 
            this.tsmiTrayHashCheck.Image = global::ShareX.Properties.Resources.application_task;
            this.tsmiTrayHashCheck.Name = "tsmiTrayHashCheck";
            this.tsmiTrayHashCheck.Size = new System.Drawing.Size(183, 22);
            this.tsmiTrayHashCheck.Text = "Hash check...";
            this.tsmiTrayHashCheck.Click += new System.EventHandler(this.tsmiHashCheck_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 362);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(910, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShareX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.cmsTray.ResumeLayout(false);
            this.cmsUploadInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private HelpersLib.MyListView lvUploads;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chProgress;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chUploaderType;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbClipboardUpload;
        private System.Windows.Forms.ToolStripButton tsbFileUpload;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripSeparator tssMain1;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chRemaining;
        private System.Windows.Forms.ColumnHeader chElapsed;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTextUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiURLShorteners;
        private System.Windows.Forms.ToolStripDropDownButton tsddbDestinations;
        private System.Windows.Forms.ToolStripSeparator tssDestinations1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadersConfig;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayClipboardUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFileUpload;
        private System.Windows.Forms.ToolStripSeparator tssTray1;
        public System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.ToolStripDropDownButton tsddbCapture;
        private System.Windows.Forms.ToolStripMenuItem tsmiFullscreen;
        private System.Windows.Forms.ToolStripMenuItem tsmiRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiRoundedRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiEllipse;
        private System.Windows.Forms.ToolStripMenuItem tsmiTriangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiDiamond;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolygon;
        private System.Windows.Forms.ToolStripMenuItem tsmiFreeHand;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindowRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiTraySettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayAbout;
        private System.Windows.Forms.ToolStripSeparator tssTray2;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayCapture;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFullscreen;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayWindowRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayRoundedRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayTriangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayDiamond;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayPolygon;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFreeHand;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayEllipse;
        private System.Windows.Forms.ToolStripDropDownButton tsbDebug;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestImageUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestTextUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestShapeCapture;
        private System.Windows.Forms.ToolStripMenuItem tsmiLastRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayLastRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestFileUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestURLShortener;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayDestinations;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayImageUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayTextUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFileUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayURLShorteners;
        private System.Windows.Forms.ToolStripSeparator tssTrayDestinations1;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayUploadersConfig;
        private System.Windows.Forms.ContextMenuStrip cmsUploadInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenShortenedURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenThumbnailURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenDeletionURL;
        private System.Windows.Forms.ToolStripSeparator tssOpen1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyShortenedURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyThumbnailURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyDeletionURL;
        private System.Windows.Forms.ToolStripSeparator tssCopy1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyText;
        private System.Windows.Forms.ToolStripSeparator tssCopy2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLLinkedImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy3;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumLinkedImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy4;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFilePath;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFileName;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFileNameWithExtension;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopUpload;
        private HelpersLib.MyPictureBox pbPreview;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowErrors;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowResponse;
        private System.Windows.Forms.ToolStripMenuItem tsmiScreenshotsFolder;
        private System.Windows.Forms.ToolStripDropDownButton tsddbAfterCaptureTasks;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayAfterCaptureTasks;
        private System.Windows.Forms.ToolStripMenuItem tsmiSocialServices;
        private System.Windows.Forms.ToolStripMenuItem tsmiTraySocialServices;
        private System.Windows.Forms.ToolStripDropDownButton tsddbAfterUploadTasks;
        private System.Windows.Forms.ToolStripButton tsbScreenshotsFolder;
        private System.Windows.Forms.Label lblSplitter;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayAfterUploadTasks;
        private System.Windows.Forms.ToolStripSeparator tssUploadInfo1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPreview;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadSelectedFile;
        private System.Windows.Forms.ToolStripButton tsbImageHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayImageHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayScreenColorPicker;
        private System.Windows.Forms.ToolStripDropDownButton tsddbTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiScreenColorPicker;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearList;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiScreenRecorder;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayScreenRecorder;
        private System.Windows.Forms.ToolStripButton tsbDonate;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayDonate;
        private System.Windows.Forms.ToolStripMenuItem tsmiHashCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayHashCheck;
    }
}