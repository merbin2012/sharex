﻿namespace UploadersLib.Forms
{
    partial class DropboxFilesForm
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
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbSelectFolder = new System.Windows.Forms.ToolStripButton();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmsDropbox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDownloadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.lvDropboxFiles = new HelpersLib.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsMenu.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.cmsDropbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMenu
            // 
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectFolder});
            this.tsMenu.Location = new System.Drawing.Point(2, 2);
            this.tsMenu.Margin = new System.Windows.Forms.Padding(2);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(555, 22);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbSelectFolder
            // 
            this.tsbSelectFolder.Image = global::UploadersLib.Properties.Resources.folder;
            this.tsbSelectFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectFolder.Name = "tsbSelectFolder";
            this.tsbSelectFolder.Size = new System.Drawing.Size(160, 19);
            this.tsbSelectFolder.Text = "Select current folder path";
            this.tsbSelectFolder.Click += new System.EventHandler(this.tsbSelectFolder_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tsMenu, 0, 0);
            this.tlpMain.Controls.Add(this.lvDropboxFiles, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(559, 491);
            this.tlpMain.TabIndex = 3;
            // 
            // cmsDropbox
            // 
            this.cmsDropbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDownloadFile});
            this.cmsDropbox.Name = "cmsDropbox";
            this.cmsDropbox.ShowImageMargin = false;
            this.cmsDropbox.Size = new System.Drawing.Size(123, 26);
            // 
            // tsmiDownloadFile
            // 
            this.tsmiDownloadFile.Name = "tsmiDownloadFile";
            this.tsmiDownloadFile.Size = new System.Drawing.Size(122, 22);
            this.tsmiDownloadFile.Text = "Download file";
            this.tsmiDownloadFile.Click += new System.EventHandler(this.tsmiDownloadFile_Click);
            // 
            // lvDropboxFiles
            // 
            this.lvDropboxFiles.AutoFillColumn = true;
            this.lvDropboxFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chSize,
            this.chModified});
            this.lvDropboxFiles.ContextMenuStrip = this.cmsDropbox;
            this.lvDropboxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDropboxFiles.FullRowSelect = true;
            this.lvDropboxFiles.GridLines = true;
            this.lvDropboxFiles.Location = new System.Drawing.Point(3, 29);
            this.lvDropboxFiles.Name = "lvDropboxFiles";
            this.lvDropboxFiles.Size = new System.Drawing.Size(553, 459);
            this.lvDropboxFiles.TabIndex = 0;
            this.lvDropboxFiles.UseCompatibleStateImageBehavior = false;
            this.lvDropboxFiles.View = System.Windows.Forms.View.Details;
            this.lvDropboxFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDropboxFiles_MouseDoubleClick);
            // 
            // chFilename
            // 
            this.chFilename.Text = "File Name";
            this.chFilename.Width = 275;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 80;
            // 
            // chModified
            // 
            this.chModified.Text = "Modified";
            this.chModified.Width = 200;
            // 
            // DropboxFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 491);
            this.Controls.Add(this.tlpMain);
            this.Name = "DropboxFilesForm";
            this.ShowIcon = false;
            this.Text = "Dropbox";
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.cmsDropbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HelpersLib.MyListView lvDropboxFiles;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chModified;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbSelectFolder;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ContextMenuStrip cmsDropbox;
        private System.Windows.Forms.ToolStripMenuItem tsmiDownloadFile;
    }
}