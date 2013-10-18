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

using System.Drawing;
using System.Drawing.Drawing2D;

namespace HelpersLib
{
    public class ColorBox : ColorUserControl
    {
        public ColorBox()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            Name = "ColorBox";
            Size = new Size(260, 260);
            base.Initialize();
        }

        protected override void DrawCrosshair(Graphics g)
        {
            DrawEllipse(g, Pens.Black, 6);
            DrawEllipse(g, Pens.White, 5);
        }

        private void DrawEllipse(Graphics g, Pen pen, int size)
        {
            g.DrawEllipse(pen, new Rectangle(new Point(lastPos.X - size, lastPos.Y - size), new Size(size * 2, size * 2)));
        }

        // X = Saturation 0 -> 100
        // Y = Brightness 100 -> 0
        protected override void DrawHue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB(SelectedColor.HSB.Hue, 0.0, 0.0);
                HSB end = new HSB(SelectedColor.HSB.Hue, 1.0, 0.0);

                for (int y = 0; y < height; y++)
                {
                    start.Brightness = end.Brightness = 1.0 - (double)y / (height - 1);

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, width, 1));
                    }
                }
            }
        }

        // X = Hue 0 -> 360
        // Y = Brightness 100 -> 0
        protected override void DrawSaturation()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB(0.0, SelectedColor.HSB.Saturation, 1.0);
                HSB end = new HSB(0.0, SelectedColor.HSB.Saturation, 0.0);

                for (int x = 0; x < width; x++)
                {
                    start.Hue = end.Hue = (double)x / (height - 1);

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, height), start, end, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, new Rectangle(x, 0, 1, height));
                    }
                }
            }
        }

        // X = Hue 0 -> 360
        // Y = Saturation 100 -> 0
        protected override void DrawBrightness()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                HSB start = new HSB(0.0, 1.0, SelectedColor.HSB.Brightness);
                HSB end = new HSB(0.0, 0.0, SelectedColor.HSB.Brightness);

                for (int x = 0; x < width; x++)
                {
                    start.Hue = end.Hue = (double)x / (height - 1);

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, height), start, end, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, new Rectangle(x, 0, 1, height));
                    }
                }
            }
        }

        // X = Blue 0 -> 255
        // Y = Green 255 -> 0
        protected override void DrawRed()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                RGB start = new RGB(SelectedColor.RGB.Red, 0, 0);
                RGB end = new RGB(SelectedColor.RGB.Red, 0, 255);

                for (int y = 0; y < height; y++)
                {
                    start.Green = end.Green = Round(255 - (255 * (double)y / (height - 1)));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, width, 1));
                    }
                }
            }
        }

        // X = Blue 0 -> 255
        // Y = Red 255 -> 0
        protected override void DrawGreen()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                RGB start = new RGB(0, SelectedColor.RGB.Green, 0);
                RGB end = new RGB(0, SelectedColor.RGB.Green, 255);

                for (int y = 0; y < height; y++)
                {
                    start.Red = end.Red = Round(255 - (255 * (double)y / (height - 1)));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, width, 1));
                    }
                }
            }
        }

        // X = Red 0 -> 255
        // Y = Green 255 -> 0
        protected override void DrawBlue()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                RGB start = new RGB(0, 0, SelectedColor.RGB.Blue);
                RGB end = new RGB(255, 0, SelectedColor.RGB.Blue);

                for (int y = 0; y < height; y++)
                {
                    start.Green = end.Green = Round(255 - (255 * (double)y / (height - 1)));

                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1), start, end, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, new Rectangle(0, y, width, 1));
                    }
                }
            }
        }
    }
}