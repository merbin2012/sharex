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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ScreenCapture
{
    public class AVICache : IDisposable
    {
        public bool IsWorking { get; private set; }
        public string OutputPath { get; private set; }
        public int FPS { get; private set; }
        public Size Size { get; private set; }

        private BlockingCollection<Image> imageQueue;

        private Task task;

        public AVICache(string outputPath, int fps, Size size)
        {
            OutputPath = outputPath;
            FPS = fps;
            Size = size;
            imageQueue = new BlockingCollection<Image>();
            StartConsumerThread();
        }

        private void StartConsumerThread()
        {
            if (!IsWorking)
            {
                IsWorking = true;

                task = TaskEx.Run(() =>
                {
                    int count = 0;

                    using (AVIWriter aviWriter = new AVIWriter(OutputPath, FPS, Size.Width, Size.Height))
                    {
                        while (!imageQueue.IsCompleted)
                        {
                            Image img = null;

                            try
                            {
                                img = imageQueue.Take();

                                if (img != null)
                                {
                                    count++;
                                    //img.Save("Test\\" + count + ".bmp", ImageFormat.Bmp);
                                    aviWriter.AddFrame((Bitmap)img);
                                }
                            }
                            catch (InvalidOperationException) { }
                            finally
                            {
                                if (img != null) img.Dispose();
                            }
                        }
                    }

                    IsWorking = false;
                });
            }
        }

        public void AddImageAsync(Image img)
        {
            if (IsWorking)
            {
                imageQueue.Add(img);
            }
        }

        public void Finish()
        {
            imageQueue.CompleteAdding();
            task.Wait();
        }

        public void Dispose()
        {
            if (imageQueue != null)
            {
                imageQueue.Dispose();
            }
        }
    }
}