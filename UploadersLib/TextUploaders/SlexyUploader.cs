﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows to you take screenshots and share any file type
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

using System.Collections.Generic;
using System.ComponentModel;

namespace UploadersLib.TextUploaders
{
    public sealed class SlexyUploader : TextUploader
    {
        private const string APIURL = "http://slexy.org/index.php/submit";

        private SlexySettings settings;

        public SlexyUploader()
        {
            settings = new SlexySettings();
        }

        public SlexyUploader(SlexySettings settings)
        {
            this.settings = settings;
        }

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("raw_paste", text);
                arguments.Add("author", settings.Author);
                arguments.Add("comment", "");
                arguments.Add("desc", settings.Description);
                arguments.Add("expire", settings.Expiration);
                arguments.Add("language", settings.TextFormat);
                arguments.Add("linenumbers", settings.LineNumbers ? "1" : "0");
                arguments.Add("permissions", settings.Visibility == Privacy.Private ? "1" : "0");
                arguments.Add("submit", "Submit Paste");
                arguments.Add("tabbing", "true");
                arguments.Add("tabtype", "real");

                return SendPostRequest(APIURL, arguments, ResponseType.RedirectionURL);
            }

            return null;
        }
    }

    public class SlexySettings
    {
        /// <summary>language</summary>
        public string TextFormat { get; set; }

        /// <summary>author</summary>
        public string Author { get; set; }

        /// <summary>permissions</summary>
        public Privacy Visibility { get; set; }

        /// <summary>desc</summary>
        public string Description { get; set; }

        /// <summary>linenumbers</summary>
        public bool LineNumbers { get; set; }

        /// <summary>expire</summary>
        [Description("Expiration time with seconds. Example: 0 = Forever, 60 = 1 minutes, 3600 = 1 hour, 2592000 = 1 month")]
        public string Expiration { get; set; }

        public SlexySettings()
        {
            TextFormat = "text";
            Author = "";
            Visibility = Privacy.Private;
            Description = "";
            LineNumbers = true;
            Expiration = "2592000";
        }
    }
}