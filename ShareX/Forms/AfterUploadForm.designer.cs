﻿namespace ShareX
{
    partial class AfterUploadForm
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
            this.tvMain = new System.Windows.Forms.TreeView();
            this.pbPreview = new HelpersLib.MyPictureBox();
            this.btnCopyImage = new System.Windows.Forms.Button();
            this.btnCopyLink = new System.Windows.Forms.Button();
            this.btnOpenLink = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tmrClose = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvMain
            // 
            this.tvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvMain.Location = new System.Drawing.Point(312, 8);
            this.tvMain.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.tvMain.Name = "tvMain";
            this.tvMain.Size = new System.Drawing.Size(384, 296);
            this.tvMain.TabIndex = 7;
            this.tvMain.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMain_NodeMouseDoubleClick);
            // 
            // pbPreview
            // 
            this.pbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbPreview.BackColor = System.Drawing.Color.White;
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.DrawCheckeredBackground = true;
            this.pbPreview.FullscreenOnClick = true;
            this.pbPreview.Location = new System.Drawing.Point(8, 8);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(296, 296);
            this.pbPreview.TabIndex = 6;
            // 
            // btnCopyImage
            // 
            this.btnCopyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyImage.AutoSize = true;
            this.btnCopyImage.Location = new System.Drawing.Point(8, 312);
            this.btnCopyImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnCopyImage.Name = "btnCopyImage";
            this.btnCopyImage.Size = new System.Drawing.Size(88, 32);
            this.btnCopyImage.TabIndex = 0;
            this.btnCopyImage.Text = "Copy image";
            this.btnCopyImage.UseVisualStyleBackColor = true;
            this.btnCopyImage.Click += new System.EventHandler(this.btnCopyImage_Click);
            // 
            // btnCopyLink
            // 
            this.btnCopyLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyLink.AutoSize = true;
            this.btnCopyLink.Location = new System.Drawing.Point(96, 312);
            this.btnCopyLink.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCopyLink.Name = "btnCopyLink";
            this.btnCopyLink.Size = new System.Drawing.Size(88, 32);
            this.btnCopyLink.TabIndex = 1;
            this.btnCopyLink.Text = "Copy link";
            this.btnCopyLink.UseVisualStyleBackColor = true;
            this.btnCopyLink.Click += new System.EventHandler(this.btnCopyLink_Click);
            // 
            // btnOpenLink
            // 
            this.btnOpenLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenLink.AutoSize = true;
            this.btnOpenLink.Location = new System.Drawing.Point(184, 312);
            this.btnOpenLink.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenLink.Name = "btnOpenLink";
            this.btnOpenLink.Size = new System.Drawing.Size(88, 32);
            this.btnOpenLink.TabIndex = 2;
            this.btnOpenLink.Text = "Open link...";
            this.btnOpenLink.UseVisualStyleBackColor = true;
            this.btnOpenLink.Click += new System.EventHandler(this.btnOpenLink_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.AutoSize = true;
            this.btnOpenFile.Location = new System.Drawing.Point(272, 312);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(88, 32);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Open file...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFolder.AutoSize = true;
            this.btnOpenFolder.Location = new System.Drawing.Point(360, 312);
            this.btnOpenFolder.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(88, 32);
            this.btnOpenFolder.TabIndex = 4;
            this.btnOpenFolder.Text = "Open folder...";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnFolderOpen_Click);
            // 
            // tmrClose
            // 
            this.tmrClose.Enabled = true;
            this.tmrClose.Interval = 60000;
            this.tmrClose.Tick += new System.EventHandler(this.tmrClose_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(608, 312);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AfterUploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 350);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpenLink);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnCopyLink);
            this.Controls.Add(this.btnCopyImage);
            this.Controls.Add(this.tvMain);
            this.Controls.Add(this.pbPreview);
            this.MinimumSize = new System.Drawing.Size(600, 310);
            this.Name = "AfterUploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShareX - After upload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvMain;
        private HelpersLib.MyPictureBox pbPreview;
        private System.Windows.Forms.Timer tmrClose;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnCopyImage;
        private System.Windows.Forms.Button btnOpenLink;
        private System.Windows.Forms.Button btnCopyLink;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnClose;
    }
}