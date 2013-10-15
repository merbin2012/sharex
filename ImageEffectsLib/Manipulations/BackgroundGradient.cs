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
using System.Drawing.Drawing2D;

namespace ImageEffectsLib
{
    internal class BackgroundGradient : ImageEffect
    {
        [DefaultValue(typeof(Color), "White")]
        public Color FromColor { get; set; }

        [DefaultValue(typeof(Color), "Black")]
        public Color ToColor { get; set; }

        [DefaultValue(LinearGradientMode.ForwardDiagonal)]
        public LinearGradientMode GradientType { get; set; }

        public BackgroundGradient()
        {
            this.ApplyDefaultPropertyValues();
        }

        public override Image Apply(Image img)
        {
            return ImageHelpers.FillImageBackground(img, FromColor, ToColor, GradientType);
        }
    }
}