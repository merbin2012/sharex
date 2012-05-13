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
using System.IO;
using System.Linq;
using System.Text;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public class SharedFolderUploader : FileUploader
    {
        LocalhostAccount Account;

        public SharedFolderUploader(LocalhostAccount account)
        {
            this.Account = account;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();

            string filePath = Account.GetLocalhostPath(fileName);
            string destDir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                if (TransferData(stream, fs))
                {
                    result.LocalFilePath = filePath;
                    result.URL = Account.GetUriPath(Path.GetFileName(fileName));
                }
            }

            return result;
        }
    }
}