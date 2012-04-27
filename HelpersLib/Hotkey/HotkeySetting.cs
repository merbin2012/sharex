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
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace HelpersLib.Hotkey
{
    public class HotkeySetting
    {
        public Keys Hotkey { get; set; }

        public Keys HotkeyDefault { get; set; }

        [XmlIgnore, JsonIgnore]
        public int Tag { get; set; }

        [XmlIgnore, JsonIgnore]
        public Action Action { get; set; }

        [XmlIgnore, JsonIgnore]
        public ToolStripMenuItem MenuItem { get; set; }

        [XmlIgnore, JsonIgnore]
        public HotkeyStatus HotkeyStatus { get; set; }

        [XmlIgnore, JsonIgnore]
        public string Description { get; set; }

        public HotkeySetting()
        {
            Hotkey = Keys.None;
        }

        public HotkeySetting(Keys hotkey)
        {
            Hotkey = hotkey;
            HotkeyDefault = hotkey;
        }

        public HotkeySetting(Keys hotkey, int tag, Action action, ToolStripMenuItem menuItem = null)
            : this(hotkey)
        {
            Tag = tag;
            Action = action;
            MenuItem = menuItem;
        }

        public void UpdateMenuItemShortcut()
        {
            if (MenuItem != null)
            {
                MenuItem.ShortcutKeyDisplayString = new KeyInfo(Hotkey).ToString();
            }
        }

        public override string ToString()
        {
            return new KeyInfo(Hotkey).ToString();
        }
    }
}