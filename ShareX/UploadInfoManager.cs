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
using ShareX.Properties;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShareX
{
    public class UploadInfoManager
    {
        public UploadInfoStatus[] SelectedItems { get; private set; }

        private ListView lv;

        public UploadInfoManager(ListView listView)
        {
            lv = listView;
        }

        private UploadInfoStatus[] GetSelectedItems()
        {
            if (lv != null && lv.SelectedItems.Count > 0)
            {
                return lv.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as Task).Where(x => x != null && x.Info != null).Select(x => new UploadInfoStatus(x.Info)).ToArray();
            }

            return null;
        }

        private void CopyTexts(IEnumerable<string> texts)
        {
            if (texts != null && texts.Count() > 0)
            {
                string urls = string.Join("\r\n", texts.ToArray());

                if (!string.IsNullOrEmpty(urls))
                {
                    Helpers.CopyTextSafely(urls);
                }
            }
        }

        public bool IsSelectedItemsValid()
        {
            return SelectedItems != null && SelectedItems.Length > 0;
        }

        public bool RefreshSelectedItems()
        {
            SelectedItems = GetSelectedItems();
            return SelectedItems != null;
        }

        #region Open

        public void OpenURL()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsURLExist) Helpers.LoadBrowserAsync(SelectedItems[0].Info.Result.URL);
        }

        public void OpenShortenedURL()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsShortenedURLExist) Helpers.LoadBrowserAsync(SelectedItems[0].Info.Result.ShortenedURL);
        }

        public void OpenThumbnailURL()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsThumbnailURLExist) Helpers.LoadBrowserAsync(SelectedItems[0].Info.Result.ThumbnailURL);
        }

        public void OpenDeletionURL()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsDeletionURLExist) Helpers.LoadBrowserAsync(SelectedItems[0].Info.Result.DeletionURL);
        }

        public void OpenFile()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsFileExist) Helpers.LoadBrowserAsync(SelectedItems[0].Info.FilePath);
        }

        public void OpenFolder()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsFileExist) Helpers.OpenFolderWithFile(SelectedItems[0].Info.FilePath);
        }

        public void TryOpen()
        {
            if (IsSelectedItemsValid())
            {
                if (SelectedItems[0].IsShortenedURLExist)
                {
                    Helpers.LoadBrowserAsync(SelectedItems[0].Info.Result.ShortenedURL);
                }
                else if (SelectedItems[0].IsURLExist)
                {
                    Helpers.LoadBrowserAsync(SelectedItems[0].Info.Result.URL);
                }
                else if (SelectedItems[0].IsFileExist)
                {
                    Helpers.LoadBrowserAsync(SelectedItems[0].Info.FilePath);
                }
            }
        }

        #endregion Open

        #region Copy

        public void CopyURL()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsURLExist).Select(x => x.Info.Result.URL));
        }

        public void CopyShortenedURL()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsShortenedURLExist).Select(x => x.Info.Result.ShortenedURL));
        }

        public void CopyThumbnailURL()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsThumbnailURLExist).Select(x => x.Info.Result.ThumbnailURL));
        }

        public void CopyDeletionURL()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsDeletionURLExist).Select(x => x.Info.Result.DeletionURL));
        }

        public void CopyFile()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsFileExist) Helpers.CopyFileToClipboard(SelectedItems[0].Info.FilePath);
        }

        public void CopyImage()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsImageFile) Helpers.CopyImageFileToClipboard(SelectedItems[0].Info.FilePath);
        }

        public void CopyText()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsTextFile) Helpers.CopyTextFileToClipboard(SelectedItems[0].Info.FilePath);
        }

        public void CopyHTMLLink()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsURLExist).Select(x => string.Format("<a href=\"{0}\">{0}</a>", x.Info.Result.URL)));
        }

        public void CopyHTMLImage()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsImageURL).Select(x => string.Format("<img src=\"{0}\"/>", x.Info.Result.URL)));
        }

        public void CopyHTMLLinkedImage()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsImageURL && x.IsThumbnailURLExist).
                Select(x => string.Format("<a href=\"{0}\"><img src=\"{1}\"/></a>", x.Info.Result.URL, x.Info.Result.ThumbnailURL)));
        }

        public void CopyForumLink()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsURLExist).Select(x => string.Format("[url]{0}[/url]", x.Info.Result.URL)));
        }

        public void CopyForumImage()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsImageURL).Select(x => string.Format("[img]{0}[/img]", x.Info.Result.URL)));
        }

        public void CopyForumLinkedImage()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsImageURL && x.IsThumbnailURLExist).
                Select(x => string.Format("[url={0}][img]{1}[/img][/url]", x.Info.Result.URL, x.Info.Result.ThumbnailURL)));
        }

        public void CopyFilePath()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsFilePathValid).Select(x => x.Info.FilePath));
        }

        public void CopyFileName()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsFilePathValid).Select(x => Path.GetFileNameWithoutExtension(x.Info.FilePath)));
        }

        public void CopyFileNameWithExtension()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsFilePathValid).Select(x => Path.GetFileName(x.Info.FilePath)));
        }

        public void CopyFolder()
        {
            if (IsSelectedItemsValid()) CopyTexts(SelectedItems.Where(x => x.IsFilePathValid).Select(x => Path.GetDirectoryName(x.Info.FilePath)));
        }

        #endregion Copy

        #region Other

        public void ShowImagePreview()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsImageFile) ImageViewer.ShowImage(SelectedItems[0].Info.FilePath);
        }

        public void ShowErrors()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].Info.Result != null && SelectedItems[0].Info.Result.IsError)
            {
                string errors = SelectedItems[0].Info.Result.ErrorsToString();

                if (!string.IsNullOrEmpty(errors))
                {
                    using (ErrorForm form = new ErrorForm(Application.ProductName, "Upload errors", errors, Program.MyLogger, Program.LogFilePath, Links.URL_ISSUES))
                    {
                        form.Icon = Resources.ShareX;
                        form.ShowDialog();
                    }
                }
            }
        }

        public void ShowResponse()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].Info.Result != null && !string.IsNullOrEmpty(SelectedItems[0].Info.Result.Response))
            {
                using (ResponseForm form = new ResponseForm(SelectedItems[0].Info.Result.Response))
                {
                    form.Icon = Resources.ShareX;
                    form.ShowDialog();
                }
            }
        }

        public void Upload()
        {
            if (IsSelectedItemsValid() && SelectedItems[0].IsFileExist) UploadManager.UploadFile(SelectedItems[0].Info.FilePath);
        }

        #endregion Other
    }
}