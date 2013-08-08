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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ShareX
{
    public delegate void HotkeyTriggerEventHandler(HotkeySetting hotkeySetting);

    public class HotkeyManager
    {
        public List<HotkeySetting> Settings = new List<HotkeySetting>();

        private HotkeyForm hotkeyForm;

        public HotkeyManager(HotkeyForm hotkeyForm)
        {
            this.hotkeyForm = hotkeyForm;
        }

        public void AddHotkey(HotkeySetting hotkeySetting)
        {
            AddHotkey(hotkeySetting, (int)hotkeySetting.Job);
        }

        private void AddHotkey(HotkeySetting hotkeySetting, int hotkeyId)
        {
            hotkeySetting.Tag = hotkeyId;
            Settings.Add(hotkeySetting);
            hotkeySetting.UpdateMenuItemShortcut();
            hotkeySetting.HotkeyStatus = hotkeyForm.RegisterHotkey(hotkeySetting.Hotkey, () => hotkeySetting.Action(hotkeySetting), hotkeyId);
        }

        public HotkeyStatus UpdateHotkey(HotkeySetting setting)
        {
            setting.UpdateMenuItemShortcut();
            setting.HotkeyStatus = hotkeyForm.ChangeHotkey(setting.Tag, setting.Hotkey, () => setting.Action(setting));
            return setting.HotkeyStatus;
        }

        public bool IsHotkeyRegisterFailed(out string failedHotkeys)
        {
            failedHotkeys = null;
            bool status = false;
            var failedHotkeysList = Settings.Where(x => x.HotkeyStatus == HotkeyStatus.Failed);
            if (status = failedHotkeysList.Count() > 0)
            {
                failedHotkeys = string.Join("\r\n", failedHotkeysList.Select(x => x.Description + ": " + x.ToString()).ToArray());
            }
            return status;
        }
    }
}