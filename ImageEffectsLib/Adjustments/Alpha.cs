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
using System.Drawing;

namespace ImageEffectsLib
{
    public class Alpha : IPluginItem
    {
        public override string Name { get { return "Alpha"; } }

        public override string Description { get { return "Alpha"; } }

        private float percentage;

        public float Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value;
                ChangePreviewText();
            }
        }

        private float addition;

        public float Addition
        {
            get
            {
                return addition;
            }
            set
            {
                addition = value;
                ChangePreviewText();
            }
        }

        private void ChangePreviewText()
        {
            OnPreviewTextChanged(string.Format("{0}% {1}", percentage, addition));
        }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixManager.Alpha(percentage, addition).Apply(img);
        }
    }
}