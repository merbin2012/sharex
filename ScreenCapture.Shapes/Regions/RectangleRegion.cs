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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class RectangleRegion : Surface
    {
        public AreaManager AreaManager { get; private set; }

        private bool drawCrosshair = true;
        private bool drawMagnifier = true;
        private int magnifierPixelCount = 15;
        private int magnifierPixelSize = 10;

        public RectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            AreaManager = new AreaManager(this);
            KeyDown += new KeyEventHandler(RectangleRegion_KeyDown);
        }

        private void RectangleRegion_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Add:
                    if (magnifierPixelCount < 35) magnifierPixelCount += 2;
                    break;
                case Keys.Subtract:
                    if (magnifierPixelCount > 3) magnifierPixelCount -= 2;
                    break;
                case Keys.Control | Keys.Add:
                    if (magnifierPixelSize < 30) magnifierPixelSize++;
                    break;
                case Keys.Control | Keys.Subtract:
                    if (magnifierPixelSize > 3) magnifierPixelSize--;
                    break;
            }
        }

        public override void Prepare()
        {
            base.Prepare();

            if (Config != null)
            {
                AreaManager.WindowCaptureMode |= Config.ForceWindowCapture;

                if (AreaManager.WindowCaptureMode)
                {
                    WindowsListAdvanced wla = new WindowsListAdvanced();
                    wla.IgnoreWindows.Add(Handle);
                    wla.IncludeChildWindows = Config.IncludeControls;
                    AreaManager.Windows = wla.GetWindowsRectangleList();
                }
            }
        }

        protected override void Update()
        {
            base.Update();
            AreaManager.Update();
        }

        protected override void Draw(Graphics g)
        {
            if (drawCrosshair)
            {
                DrawCrosshair(g);
            }

            List<Rectangle> areas = AreaManager.GetValidAreas;

            if (areas.Count > 0 || !AreaManager.CurrentHoverArea.IsEmpty)
            {
                UpdateRegionPath();

                using (Region region = new Region(regionDrawPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    g.ResetClip();
                }

                borderDotPen.DashOffset = (float)timer.Elapsed.TotalSeconds * 10;
                borderDotPen2.DashOffset = 5 + (float)timer.Elapsed.TotalSeconds * 10;

                g.DrawPath(borderPen, regionDrawPath);

                if (areas.Count > 1)
                {
                    Rectangle totalArea = AreaManager.CombineAreas();
                    g.DrawCrossRectangle(borderPen, totalArea, 15);
                    CaptureHelpers.DrawTextWithOutline(g, string.Format("X:{0}, Y:{1}, Width:{2}, Height:{3}", totalArea.X, totalArea.Y,
                        totalArea.Width, totalArea.Height), new PointF(totalArea.X + 5, totalArea.Y - 20), textFont, Color.White, Color.Black);
                }

                if (AreaManager.IsCurrentHoverAreaValid)
                {
                    GraphicsPath hoverFillPath = new GraphicsPath() { FillMode = FillMode.Winding };
                    AddShapePath(hoverFillPath, AreaManager.CurrentHoverArea);

                    g.FillPath(lightBrush, hoverFillPath);

                    GraphicsPath hoverDrawPath = new GraphicsPath() { FillMode = FillMode.Winding };
                    AddShapePath(hoverDrawPath, AreaManager.CurrentHoverArea.SizeOffset(-1));

                    g.DrawPath(borderDotPen, hoverDrawPath);
                    g.DrawPath(borderDotPen2, hoverDrawPath);
                }

                if (AreaManager.IsCurrentAreaValid)
                {
                    g.DrawRectangleProper(borderDotPen, AreaManager.CurrentArea);
                    g.DrawRectangleProper(borderDotPen2, AreaManager.CurrentArea);
                    DrawObjects(g);
                }

                foreach (Rectangle area in areas)
                {
                    if (area.Width > 100 && area.Height > 20)
                    {
                        g.Clip = new Region(area);

                        CaptureHelpers.DrawTextWithOutline(g, string.Format("X:{0}, Y:{1}, Width:{2}, Height:{3}", area.X, area.Y, area.Width, area.Height),
                            new PointF(area.X + 5, area.Y + 5), textFont, Color.White, Color.Black);
                    }
                }

                g.ResetClip();
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            if (drawMagnifier)
            {
                DrawMagnifier(g);
            }
        }

        private void DrawCrosshair(Graphics g)
        {
            Point mousePos = InputManager.MousePosition0Based;
            using (Pen crosshairPen = new Pen(Color.FromArgb(100, Color.Black)))
            {
                g.DrawLine(crosshairPen, new Point(mousePos.X, 0), new Point(mousePos.X, ScreenRectangle0Based.Height - 1));
                g.DrawLine(crosshairPen, new Point(0, mousePos.Y), new Point(ScreenRectangle0Based.Width - 1, mousePos.Y));
            }
        }

        private void DrawMagnifier(Graphics g)
        {
            Point mousePos = InputManager.MousePosition0Based;
            int offset = 25;

            using (Bitmap magnifier = Magnifier(SurfaceImage, mousePos, magnifierPixelCount, magnifierPixelCount, magnifierPixelSize))
            {
                int x = mousePos.X + offset;

                if (x + magnifier.Width > ScreenRectangle0Based.Width)
                {
                    x = mousePos.X - offset - magnifier.Width;
                }

                int y = mousePos.Y + offset;

                if (y + magnifier.Height > ScreenRectangle0Based.Height)
                {
                    y = mousePos.Y - offset - magnifier.Height;
                }

                g.DrawImage(magnifier, new Point(x, y));
            }
        }

        private Bitmap Magnifier(Image img, Point position, int horizontalPixelCount, int verticalPixelCount, int pixelSize)
        {
            if (horizontalPixelCount % 2 == 0 || verticalPixelCount % 2 == 0)
            {
                throw new Exception("Pixel count must be odd.");
            }

            int width = horizontalPixelCount * pixelSize;
            int height = verticalPixelCount * pixelSize;
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                g.DrawImage(SurfaceImage, new Rectangle(Point.Empty, new Size(width, height)),
                    new Rectangle(position.X - horizontalPixelCount / 2, position.Y - verticalPixelCount / 2, horizontalPixelCount, verticalPixelCount), GraphicsUnit.Pixel);

                g.PixelOffsetMode = PixelOffsetMode.None;

                using (SolidBrush crosshairBrush = new SolidBrush(Color.FromArgb(100, Color.Red)))
                {
                    g.FillRectangle(crosshairBrush, new Rectangle(0, (height - pixelSize) / 2, (width - pixelSize) / 2, pixelSize)); // Left
                    g.FillRectangle(crosshairBrush, new Rectangle((width + pixelSize) / 2, (height - pixelSize) / 2, (width - pixelSize) / 2, pixelSize)); // Right
                    g.FillRectangle(crosshairBrush, new Rectangle((width - pixelSize) / 2, 0, pixelSize, (height - pixelSize) / 2)); // Top
                    g.FillRectangle(crosshairBrush, new Rectangle((width - pixelSize) / 2, (height + pixelSize) / 2, pixelSize, (height - pixelSize) / 2)); // Bottom
                }

                using (Pen borderPen = new Pen(Color.FromArgb(100, Color.Black)))
                {
                    for (int x = 1; x < horizontalPixelCount; x++)
                    {
                        g.DrawLine(borderPen, new Point(x * pixelSize, 0), new Point(x * pixelSize, height));
                    }

                    for (int y = 1; y < verticalPixelCount; y++)
                    {
                        g.DrawLine(borderPen, new Point(0, y * pixelSize), new Point(width, y * pixelSize));
                    }
                }

                g.DrawRectangle(Pens.Black, 0, 0, width - 1, height - 1);
                g.DrawRectangle(Pens.Black, (width - pixelSize) / 2, (height - pixelSize) / 2, pixelSize, pixelSize);
            }

            return bmp;
        }

        public void UpdateRegionPath()
        {
            regionFillPath = new GraphicsPath() { FillMode = FillMode.Winding };
            regionDrawPath = new GraphicsPath() { FillMode = FillMode.Winding };

            foreach (Rectangle area in AreaManager.GetValidAreas)
            {
                AddShapePath(regionFillPath, area);
                AddShapePath(regionDrawPath, area.SizeOffset(-1));
            }
        }

        protected virtual void AddShapePath(GraphicsPath graphicsPath, Rectangle rect)
        {
            graphicsPath.AddRectangle(rect);
        }
    }
}