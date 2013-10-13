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
using System.Drawing;

namespace ImageEffectsLib
{
    public class Resize : IImageEffect
    {
        [DefaultValue(250), Description("Use 0 width for aspect ratio to height.")]
        public int Width { get; set; }

        [DefaultValue(0), Description("Use 0 height for keep aspect ratio to width.")]
        public int Height { get; set; }

        public Resize()
        {
            this.ApplyDefaultPropertyValues();
        }

        public Image Apply(Image img)
        {
            if (Width <= 0 && Height <= 0) return img;

            int width = Width <= 0 ? (int)((float)Height / img.Height * img.Width) : Width;
            int height = Height <= 0 ? (int)((float)Width / img.Width * img.Height) : Height;

            return ImageHelpers.ResizeImage(img, width, height);
        }
    }
}