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

using System;
using System.Drawing;

namespace HelpersLib
{
    public struct HSB
    {
        private double hue;
        private double saturation;
        private double brightness;
        private int alpha;

        public double Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = ColorHelpers.CheckColor(value);
            }
        }

        public double Hue360
        {
            get
            {
                return hue * 360;
            }
            set
            {
                hue = ColorHelpers.CheckColor(value / 360);
            }
        }

        public double Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = ColorHelpers.CheckColor(value);
            }
        }

        public double Saturation100
        {
            get
            {
                return saturation * 100;
            }
            set
            {
                saturation = ColorHelpers.CheckColor(value / 100);
            }
        }

        public double Brightness
        {
            get
            {
                return brightness;
            }
            set
            {
                brightness = ColorHelpers.CheckColor(value);
            }
        }

        public double Brightness100
        {
            get
            {
                return brightness * 100;
            }
            set
            {
                brightness = ColorHelpers.CheckColor(value / 100);
            }
        }

        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = ColorHelpers.CheckColor(value);
            }
        }

        public HSB(double hue, double saturation, double brightness, int alpha = 255)
            : this()
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
            Alpha = alpha;
        }

        public HSB(int hue, int saturation, int brightness, int alpha = 255)
            : this()
        {
            Hue360 = hue;
            Saturation100 = saturation;
            Brightness100 = brightness;
            Alpha = alpha;
        }

        public HSB(Color color)
        {
            this = RGBA.ToHSB(color);
        }

        public static implicit operator HSB(Color color)
        {
            return RGBA.ToHSB(color);
        }

        public static implicit operator Color(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator RGBA(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator CMYK(HSB color)
        {
            return color.ToColor();
        }

        public static bool operator ==(HSB left, HSB right)
        {
            return (left.Hue == right.Hue) && (left.Saturation == right.Saturation) && (left.Brightness == right.Brightness);
        }

        public static bool operator !=(HSB left, HSB right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return String.Format("Hue: {0:0.0}°, Saturation: {1:0.0}%, Brightness: {2:0.0}%", Hue360, Saturation100, Brightness100);
        }

        public static Color ToColor(HSB hsb)
        {
            return ColorHelpers.HSBToColor(hsb);
        }

        public static Color ToColor(double hue, double saturation, double brightness)
        {
            return ToColor(new HSB(hue, saturation, brightness));
        }

        public Color ToColor()
        {
            return ToColor(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}