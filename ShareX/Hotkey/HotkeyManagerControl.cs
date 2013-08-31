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

            foreach (HotkeySetting hotkeySetting in manager.Hotkeys)
            {
                AddHotkeySelectionControl(hotkeySetting);
            }
        }

        private void UpdateButtons()
        {
            btnEdit.Enabled = btnRemove.Enabled = btnDuplicate.Enabled = Selected != null;
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
            UpdateButtons();
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

        private HotkeySelectionControl AddHotkeySelectionControl(HotkeySetting hotkeySetting)
        {
            HotkeySelectionControl control = new HotkeySelectionControl(hotkeySetting);
            control.Margin = new Padding(0, 0, 0, 2);
            control.SelectedChanged += control_SelectedChanged;
            control.HotkeyChanged += control_HotkeyChanged;
            control.LabelDoubleClick += control_LabelDoubleClick;
            flpHotkeys.Controls.Add(control);
            return control;
        }

        private void Edit(HotkeySelectionControl selectionControl)
        {
            using (TaskSettingsForm taskSettingsForm = new TaskSettingsForm(selectionControl.Setting.TaskSettings))
            {
                taskSettingsForm.ShowDialog();
                selectionControl.UpdateDescription();
            }
        }

        private void control_LabelDoubleClick(object sender, EventArgs e)
        {
            Edit((HotkeySelectionControl)sender);
        }

        private void EditSelected()
        {
            if (Selected != null)
            {
                Edit(Selected);
            }
        }

        private void flpHotkeys_Layout(object sender, LayoutEventArgs e)
        {
            foreach (Control control in flpHotkeys.Controls)
            {
                control.ClientSize = new Size(flpHotkeys.ClientSize.Width, control.ClientSize.Height);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            HotkeySetting hotkeySetting = new HotkeySetting();
            hotkeySetting.TaskSettings = TaskSettings.GetDefaultTaskSettings();
            manager.Hotkeys.Add(hotkeySetting);
            HotkeySelectionControl control = AddHotkeySelectionControl(hotkeySetting);
            control.Selected = true;
            Selected = control;
            UpdateButtons();
            UpdateCheckStates();
            control.Focus();
            EditSelected();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                manager.UnregisterHotkey(Selected.Setting);
                HotkeySelectionControl hsc = FindSelectionControl(Selected.Setting);
                if (hsc != null) flpHotkeys.Controls.Remove(hsc);
                Selected = null;
                UpdateButtons();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelected();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                HotkeySetting hotkeySetting = new HotkeySetting();
                hotkeySetting.TaskSettings = Selected.Setting.TaskSettings.Copy();
                manager.Hotkeys.Add(hotkeySetting);
                HotkeySelectionControl control = AddHotkeySelectionControl(hotkeySetting);
                control.Selected = true;
                Selected = control;
                UpdateCheckStates();
                control.Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            manager.ResetHotkeys();
            manager.RegisterAllHotkeys();
            AddControls();
        }
    }
}