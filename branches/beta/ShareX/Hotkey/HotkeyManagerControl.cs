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
using System.Windows.Forms;

namespace ShareX
{
    public partial class HotkeyManagerControl : UserControl
    {
        public HotkeySelectionControl Selected { get; private set; }

        private HotkeyManager manager;

        public HotkeyManagerControl()
        {
            InitializeComponent();
        }

        public void PrepareHotkeys(HotkeyManager hotkeyManager)
        {
            if (manager == null)
            {
                manager = hotkeyManager;
                AddControls();
            }
        }

        private void AddControls()
        {
            flpHotkeys.Controls.Clear();

            foreach (HotkeySetting setting in manager.Hotkeys)
            {
                HotkeySelectionControl control = new HotkeySelectionControl(setting);
                control.Margin = new Padding(0, 0, 0, 2);
                control.SelectedChanged += control_SelectedChanged;
                control.HotkeyChanged += new EventHandler(control_HotkeyChanged);
                flpHotkeys.Controls.Add(control);
            }
        }

        private HotkeySelectionControl FindSelectionControl(HotkeySetting hotkeySetting)
        {
            foreach (Control control in flpHotkeys.Controls)
            {
                HotkeySelectionControl hsc = (HotkeySelectionControl)control;
                if (hsc.Setting == hotkeySetting) return hsc;
            }

            return null;
        }

        private void control_SelectedChanged(object sender, EventArgs e)
        {
            Selected = (HotkeySelectionControl)sender;
            UpdateCheckStates();
        }

        private void UpdateCheckStates()
        {
            foreach (Control control in flpHotkeys.Controls)
            {
                ((HotkeySelectionControl)control).Selected = Selected == control;
            }
        }

        private void control_HotkeyChanged(object sender, EventArgs e)
        {
            HotkeySelectionControl control = (HotkeySelectionControl)sender;
            manager.UpdateHotkey(control.Setting);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            HotkeySetting hotkeySetting = new HotkeySetting();
            manager.Hotkeys.Add(hotkeySetting);
            HotkeySelectionControl control = new HotkeySelectionControl(hotkeySetting);
            control.Margin = new Padding(0, 0, 0, 2);
            control.Selected = true;
            control.SelectedChanged += control_SelectedChanged;
            control.HotkeyChanged += new EventHandler(control_HotkeyChanged);
            flpHotkeys.Controls.Add(control);
            Selected = control;
            UpdateCheckStates();
            control.Focus();
            EditSelected();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                manager.RemoveHotkey(Selected.Setting);
                HotkeySelectionControl hsc = FindSelectionControl(Selected.Setting);
                if (hsc != null) flpHotkeys.Controls.Remove(hsc);
                Selected = null;
            }
        }

        private void EditSelected()
        {
            if (Selected != null)
            {
                using (TaskSettingsForm taskSettingsForm = new TaskSettingsForm(Selected.Setting.TaskSettings))
                {
                    if (taskSettingsForm.ShowDialog() == DialogResult.OK)
                    {
                        Selected.UpdateDescription();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelected();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            manager.ResetHotkeys();
            manager.RunHotkeys();
            AddControls();
        }

        private void flpHotkeys_Layout(object sender, LayoutEventArgs e)
        {
            foreach (Control control in flpHotkeys.Controls)
            {
                control.ClientSize = new Size(flpHotkeys.ClientSize.Width, control.ClientSize.Height);
            }
        }
    }
}