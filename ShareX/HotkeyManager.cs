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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ShareX
{
    public delegate void HotkeyTriggerEventHandler(HotkeySettings hotkeySetting);

    public class HotkeyManager
    {
        public List<HotkeySettings> Hotkeys { get; private set; }

        private HotkeyForm hotkeyForm;
        private HotkeyTriggerEventHandler triggerAction;

        public HotkeyManager(HotkeyForm hotkeyForm, List<HotkeySettings> hotkeys, HotkeyTriggerEventHandler action)
        {
            this.hotkeyForm = hotkeyForm;
            Hotkeys = hotkeys;
            triggerAction = action;

            if (Hotkeys.Count == 0)
            {
                ResetHotkeys();
            }
            else
            {
                RegisterAllHotkeys();
            }

            ShowFailedHotkeys();
        }

        public void RegisterHotkey(HotkeySettings hotkeySetting)
        {
            UnregisterHotkey(hotkeySetting);

            if (hotkeySetting.HotkeyInfo.Status != HotkeyStatus.Registered && hotkeySetting.HotkeyInfo.IsValidHotkey)
            {
                if (hotkeySetting.HotkeyInfo.HotkeyPress == null)
                {
                    hotkeySetting.HotkeyInfo.HotkeyPress = () => triggerAction(hotkeySetting);
                }

                hotkeySetting.HotkeyInfo = hotkeyForm.RegisterHotkey(hotkeySetting.HotkeyInfo);

                if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Registered)
                {
                    DebugHelper.WriteLine("Hotkey registered: " + hotkeySetting.ToString());
                }
                else if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Failed)
                {
                    DebugHelper.WriteLine("Hotkey register failed: " + hotkeySetting.ToString());
                }
            }

            if (!Hotkeys.Contains(hotkeySetting))
            {
                Hotkeys.Add(hotkeySetting);
            }
        }

        public void RegisterAllHotkeys()
        {
            foreach (HotkeySettings hotkeySetting in Hotkeys.ToArray())
            {
                RegisterHotkey(hotkeySetting);
            }
        }

        public void UnregisterHotkey(HotkeySettings hotkeySetting)
        {
            if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Registered)
            {
                hotkeyForm.UnregisterHotkey(hotkeySetting.HotkeyInfo);

                if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.NotConfigured)
                {
                    DebugHelper.WriteLine("Hotkey unregistered: " + hotkeySetting.ToString());
                }
                else if (hotkeySetting.HotkeyInfo.Status == HotkeyStatus.Failed)
                {
                    DebugHelper.WriteLine("Hotkey unregister failed: " + hotkeySetting.ToString());
                }
            }

            Hotkeys.Remove(hotkeySetting);
        }

        public void UnregisterAllHotkeys()
        {
            foreach (HotkeySettings hotkeySetting in Hotkeys.ToArray())
            {
                UnregisterHotkey(hotkeySetting);
            }
        }

        public void ShowFailedHotkeys()
        {
            IEnumerable<HotkeySettings> failedHotkeysList = Hotkeys.Where(x => x.HotkeyInfo.Status == HotkeyStatus.Failed);

            if (failedHotkeysList.Count() > 0)
            {
                string failedHotkeys = string.Join("\r\n", failedHotkeysList.Select(x => x.TaskSettings.ToString() + ": " + x.HotkeyInfo.ToString()).ToArray());
                string text = string.Format("Unable to register hotkey{0}:\r\n\r\n{1}\r\n\r\nPlease select a different hotkey or quit the conflicting application and reopen ShareX.",
                    failedHotkeysList.Count() > 1 ? "s" : "", failedHotkeys);

                MessageBox.Show(text, "ShareX - Hotkey registration failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ResetHotkeys()
        {
            UnregisterAllHotkeys();
            Hotkeys.AddRange(GetDefaultHotkeyList());
            RegisterAllHotkeys();
        }

        private HotkeySettings[] GetDefaultHotkeyList()
        {
            return new HotkeySettings[]
            {
                new HotkeySettings(HotkeyType.ClipboardUpload, Keys.Control | Keys.PageUp),
                new HotkeySettings(HotkeyType.FileUpload, Keys.Shift | Keys.PageUp),
                new HotkeySettings(HotkeyType.PrintScreen, Keys.PrintScreen),
                new HotkeySettings(HotkeyType.ActiveWindow, Keys.Alt | Keys.PrintScreen),
                new HotkeySettings(HotkeyType.ActiveMonitor, Keys.Control | Keys.Alt | Keys.PrintScreen),
                new HotkeySettings(HotkeyType.WindowRectangle, Keys.Shift | Keys.PrintScreen),
                new HotkeySettings(HotkeyType.RectangleRegion, Keys.Control | Keys.PrintScreen),
                new HotkeySettings(HotkeyType.ScreenRecorder, Keys.Shift | Keys.F11)
            };
        }
    }
}