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
using System.Drawing;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class PrintForm : Form
    {
        private PrintHelper printHelper;
        private PrintSettings printSettings;

        public PrintForm(Image img, PrintSettings settings)
        {
            InitializeComponent();
            printHelper = new PrintHelper(img);
            printHelper.Settings = printSettings = settings;
            LoadSettings();
        }

        private void LoadSettings()
        {
            nudMargin.Value = printSettings.Margin;
            cbAutoRotate.Checked = printSettings.AutoRotateImage;
            cbAutoScale.Checked = printSettings.AutoScaleImage;
            cbAllowEnlarge.Checked = printSettings.AllowEnlargeImage;
            cbCenterImage.Checked = printSettings.CenterImage;

            cbAllowEnlarge.Enabled = printSettings.AutoScaleImage;
            cbCenterImage.Enabled = printSettings.AutoScaleImage;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printHelper.Print();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnShowPreview_Click(object sender, EventArgs e)
        {
            printHelper.ShowPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void nudMargin_ValueChanged(object sender, EventArgs e)
        {
            printSettings.Margin = (int)nudMargin.Value;
        }

        private void cbAutoRotate_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.AutoRotateImage = cbAutoRotate.Checked;
        }

        private void cbAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.AutoScaleImage = cbAutoScale.Checked;

            cbAllowEnlarge.Enabled = printSettings.AutoScaleImage;
            cbCenterImage.Enabled = printSettings.AutoScaleImage;
        }

        private void cbAllowEnlarge_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.AllowEnlargeImage = cbAllowEnlarge.Checked;
        }

        private void cbCenterImage_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.CenterImage = cbCenterImage.Checked;
        }
    }
}