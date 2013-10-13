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
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace HelpersLib
{
    public static class ColorMatrixManager
    {
        #region Grayscale values

        private const float rw = 0.212671f;
        private const float gw = 0.715160f;
        private const float bw = 0.072169f;

        /*
        private const float rw = 0.3086f;
        private const float gw = 0.6094f;
        private const float bw = 0.0820f;
        */

        #endregion Grayscale values

        public static Image Apply(this ColorMatrix matrix, Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            using (ImageAttributes ia = new ImageAttributes())
            {
                ia.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }

            return bmp;
        }

        /// <param name="value">1 = No change (Min 0.1, Max 5.0)</param>
        public static Image ChangeGamma(Image img, float value)
        {
            value = value.Between(0.1f, 5.0f);

            Bitmap bmp = new Bitmap(img.Width, img.Height, img.PixelFormat);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            using (ImageAttributes ia = new ImageAttributes())
            {
                ia.SetGamma(value, ColorAdjustType.Bitmap);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }

            return bmp;
        }

        public static ColorMatrix Identity()
        {
            return new ColorMatrix(new[]{
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }

        public static ColorMatrix Inverse()
        {
            return new ColorMatrix(new[]{
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}});
        }

        /// <param name="value">1 = No change</param>
        /// <param name="add">0 = No change</param>
        public static ColorMatrix Alpha(float value, float add)
        {
            return new ColorMatrix(new[]{
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, value, 0},
                new float[] {0, 0, 0, add, 1}});
        }

        /// <param name="value">0 = No change</param>
        public static ColorMatrix Brightness(float value)
        {
            return new ColorMatrix(new[]{
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {value, value, value, 0, 1}});
        }

        /// <param name="value">1 = No change</param>
        public static ColorMatrix Contrast(float value)
        {
            return new ColorMatrix(new[]{
                new float[] {value, 0, 0, 0, 0},
                new float[] {0, value, 0, 0, 0},
                new float[] {0, 0, value, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }

        /// <param name="value">1 = No change</param>
        public static ColorMatrix Grayscale(float value = 1)
        {
            return new ColorMatrix(new[]{
                new float[] {rw * value, rw * value, rw * value, 0, 0},
                new float[] {gw * value, gw * value, gw * value, 0, 0},
                new float[] {bw * value, bw * value, bw * value, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }

        /// <param name="angle">0 = No change</param>
        public static ColorMatrix Hue(float angle)
        {
            float a = angle * (float)(Math.PI / 180);
            float c = (float)Math.Cos(a);
            float s = (float)Math.Sin(a);

            return new ColorMatrix(new[]{
                new float[] {(rw + (c * (1 - rw))) + (s * -rw), (rw + (c * -rw)) + (s * 0.143f), (rw + (c * -rw)) + (s * -(1 - rw)), 0, 0},
                new float[] {(gw + (c * -gw)) + (s * -gw), (gw + (c * (1 - gw))) + (s * 0.14f), (gw + (c * -gw)) + (s * gw), 0, 0},
                new float[] {(bw + (c * -bw)) + (s * (1 - bw)), (bw + (c * -bw)) + (s * -0.283f), (bw + (c * (1 - bw))) + (s * bw), 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }

        /// <param name="value">1 = No change</param>
        public static ColorMatrix Saturation(float value)
        {
            return new ColorMatrix(new[]{
                new float[] {(1.0f - value) * rw + value, (1.0f - value) * rw, (1.0f - value) * rw, 0, 0},
                new float[] {(1.0f - value) * gw, (1.0f - value) * gw + value, (1.0f - value) * gw, 0, 0},
                new float[] {(1.0f - value) * bw, (1.0f - value) * bw, (1.0f - value) * bw + value, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }

        /// <param name="value">0 = No change</param>
        public static ColorMatrix Colorize(Color color, float value)
        {
            float r = (float)color.R / 255;
            float g = (float)color.G / 255;
            float b = (float)color.B / 255;
            float inv_amount = 1 - value;

            return new ColorMatrix(new[]{
                new float[] {inv_amount + value * r * rw, value * g * rw, value * b * rw, 0, 0},
                new float[] {value * r * gw, inv_amount + value * g * gw, value * b * gw, 0, 0},
                new float[] {value * r * bw, value * g * bw, inv_amount + value * b * bw, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}});
        }
    }
}