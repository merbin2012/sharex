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

using System.Drawing;

namespace HelpersLib
{
    public static class GraphicsExtensions
    {
        public static void DrawRectangleProper(this Graphics g, Pen pen, Rectangle rect)
        {
            rect = rect.SizeOffset(-1);

            if (rect.Width > 1 && rect.Height > 1)
            {
                g.DrawRectangle(pen, rect);
            }
        }

        public static void DrawCrossRectangle(this Graphics g, Pen pen, Rectangle rect, int crossSize)
        {
            rect = rect.SizeOffset(-1);

            if (rect.Width > 0 && rect.Height > 0)
            {
                // Top
                g.DrawLine(pen, rect.X - crossSize, rect.Y, rect.Right + crossSize, rect.Y);

                // Right
                g.DrawLine(pen, rect.Right, rect.Y - crossSize, rect.Right, rect.Bottom + crossSize);

                // Bottom
                g.DrawLine(pen, rect.X - crossSize, rect.Bottom, rect.Right + crossSize, rect.Bottom);

                // Left
                g.DrawLine(pen, rect.X, rect.Y - crossSize, rect.X, rect.Bottom + crossSize);
            }
        }
    }
}