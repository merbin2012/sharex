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
using System.ComponentModel;
using System.Drawing.Design;

namespace IndexerLib
{
    public class IndexerSettings
    {
        [Category("Indexer"), DefaultValue(IndexerOutput.Html), Description("Indexer output type")]
        public IndexerOutput Output { get; set; }

        [Category("Indexer"), DefaultValue(true), Description("Don't index hidden folders")]
        public bool SkipHiddenFolders { get; set; }

        [Category("Indexer"), DefaultValue(true), Description("Don't index hidden files")]
        public bool SkipHiddenFiles { get; set; }

        [Category("Indexer / Text"), DefaultValue("|___"), Description("Padding text to show indentation in the folder hierarchy")]
        public string IndentationText { get; set; }

        [Category("Indexer / Text"), DefaultValue(false), Description("Adds empty line after folders")]
        public bool AddEmptyLineAfterFolders { get; set; }

        [Category("Indexer / XHTML"), DefaultValue("IndexerDefault.css"), Description("Cascading Style Sheet file path")]
        [EditorAttribute(typeof(CssFileNameEditor), typeof(UITypeEditor))]
        public string CssFilePath { get; set; }

        [Category("Indexer / XHTML"), DefaultValue(false), Description("Add W3C validation icons. The W3C validation icons may be used on documents that successfully passed validation for a specific technology, using the W3C validation services.")]
        public bool AddValidationIcons { get; set; }

        public IndexerSettings()
        {
            Helpers.ApplyDefaultPropertyValues(this);
        }
    }
}