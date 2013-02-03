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
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class CustomImageUploader : ImageUploader
    {
        private CustomUploaderItem customUploader;

        public CustomImageUploader(CustomUploaderItem customUploaderItem)
        {
            customUploader = customUploaderItem;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            if (customUploader.RequestType != CustomUploaderRequestType.POST) throw new Exception("'Request type' must be 'POST' when using custom image uploader.");
            if (string.IsNullOrEmpty(customUploader.FileFormName)) throw new Exception("'File form name' must be not empty when using custom image uploader.");
            if (string.IsNullOrEmpty(customUploader.RequestURL)) throw new Exception("'Request URL' must be not empty.");

            UploadResult result = UploadData(stream, customUploader.RequestURL, fileName, customUploader.FileFormName, customUploader.ParseArguments());

            if (result.IsSuccess)
            {
                customUploader.Parse(result.Response);

                result.URL = customUploader.ResultURL;
                result.ThumbnailURL = customUploader.ResultThumbnailURL;
                result.DeletionURL = customUploader.ResultDeletionURL;
            }

            return result;
        }
    }
}