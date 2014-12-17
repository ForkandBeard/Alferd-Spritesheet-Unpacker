using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ASU.BO
{
    public class RegionUnpacker : IDisposable
    {
        public static object Wait = new object();
        public static int Counter;
        private Color BackgroundColour;
        private Bitmap Image;
        private Rectangle Region;

        public List<Rectangle> Boxes = new List<Rectangle>();
        public RegionUnpacker(Bitmap image, Rectangle region, Color backgroundColour)
        {
            this.Image = image;
            this.Region = region;
            this.BackgroundColour = backgroundColour;
        }

        public void UnpackRegion()
        {
            this.Boxes = CreateBoxes(this.Image, this.Region, this.BackgroundColour);
            CombineBoxes(ref this.Boxes, this.BackgroundColour, this.Image);
            lock ((Wait))
            {
                Counter += 1;
                System.Threading.Monitor.PulseAll(Wait);
            }
        }

        public Bitmap GetImage()
        {
            return this.Image;
        }

        private static List<Rectangle> CreateBoxes(Bitmap image, Rectangle region, Color background)
        {
            List<Rectangle> boxes = new List<Rectangle>();
            Point presentPixel;
            Rectangle newBox;
            int x2 = 0;
            int y2 = 0;
            Color presentColour;

            for (int y = region.Top; y <= region.Bottom; y++)
            {

                for (int x = region.Left; x <= region.Right; x++)
                {
                    if (x > 0 && x < image.Width && y > 0 && y < image.Height)
                    {
                        presentPixel = new Point(x, y);

                        presentColour = image.GetPixel(x, y);

                        if (presentColour != background)
                        {
                            newBox = new Rectangle(presentPixel, new Size(0, 0));
                            x2 = x;

                            while (x2 < (image.Width - 1) && image.GetPixel(x2, y) != background)
                            {
                                x2 += 1;
                                newBox = new Rectangle(newBox.X, newBox.Y, newBox.Width + 1, newBox.Height);
                            }

                            y2 = y;
                            while (y2 < (image.Height - 1) && image.GetPixel(x2, y2) != background)
                            {
                                y2 += 1;
                                newBox = new Rectangle(newBox.X, newBox.Y, newBox.Width, newBox.Height + 1);
                            }

                            y2 = y + newBox.Height;
                            while (y2 < (image.Height - 1) && image.GetPixel(x, y2) != background)
                            {
                                y2 += 1;
                                newBox = new Rectangle(newBox.X, newBox.Y, newBox.Width, newBox.Height + 1);
                            }

                            boxes.Add(newBox);

                            x += (newBox.Width + 1);
                        }
                    }
                }
            }

            return boxes;
        }

        public static void CombineBoxes(ref List<Rectangle> boxes, Color background, Bitmap image)
        {
            int index = 0;
            do
            {
                index = CombineFirstOverlappingBox(ref boxes, background, image, index);
                // There is a bug here where -1 is returned even when boxes still need to be combined so just a hack to try again even if 
                // index is -1. Keep trying.
                if(index == -1)
                {
                    index = 0;
                    index = CombineFirstOverlappingBox(ref boxes, background, image, index);
                }
            } while (index != -1);
        }

        private static int CombineFirstOverlappingBox(ref List<Rectangle> boxes, Color background, Bitmap image, int startIndex)
        {
            Rectangle newBox = Rectangle.Empty;
            List<Rectangle> oldBoxes = new List<Rectangle>();
            int returnIndex = -1;
            Rectangle box;
            
            for (int i = startIndex; i <= boxes.Count - 1; i++)
            {
                box = boxes[i];

                foreach (Rectangle collider in boxes)
                {
                    if (box != collider)
                    {
                        if (DoBoxesContainAdjacentOrOverlappingPixels(box, collider, background, image))
                        {
                            newBox = box;

                            returnIndex = i;
                            if (collider.Right > newBox.Right)
                            {
                                newBox.Width = collider.Right - newBox.Left;
                            }

                            if (collider.Left < newBox.Left)
                            {
                                newBox.Width += newBox.Left - collider.Left;
                            }

                            if (collider.Bottom > newBox.Bottom)
                            {
                                newBox.Height = collider.Bottom - newBox.Top;
                            }

                            if (collider.Top < newBox.Top)
                            {
                                newBox.Height += newBox.Top - collider.Top;
                            }

                            newBox.X = Math.Min(newBox.X, collider.X);
                            newBox.Y = Math.Min(collider.Y, newBox.Y);

                            oldBoxes.Add(box);
                            oldBoxes.Add(collider);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }

                if (newBox != Rectangle.Empty)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            if (newBox != Rectangle.Empty)
            {
                foreach (Rectangle oldBox in oldBoxes)
                {
                    boxes.Remove(oldBox);
                }
                boxes.Add(newBox);
            }

            return returnIndex;
        }

        public static void DeleteAllTempFiles()
        {
            try
            {
                Console.WriteLine("Deleting from " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                foreach (string file in System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "asu_temp_spritesheet*", System.IO.SearchOption.TopDirectoryOnly))
                {
                    Console.WriteLine("Deleting " + file);
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ignore)
            {
                Console.WriteLine(ignore.ToString());
            }
        }

        private static bool DoBoxesContainAdjacentOrOverlappingPixels(Rectangle box1, Rectangle box2, Color background, Bitmap image)
        {
            Rectangle intersection;

            if (box1.IntersectsWith(box2))
            {
                intersection = Rectangle.Intersect(box1, box2);
                for (int x = intersection.X; x <= intersection.Right; x++)
                {
                    for (int y = intersection.Y; y <= intersection.Bottom; y++)
                    {
                        if (image.GetPixel(x, y) != background)
                        {
                            return true;
                        }
                    }
                }

            }


            if (ForkandBeard.Util.Geometry.GeometryHelper.GetXGapBetweenRectangles(box1, box2) <= UI.MainForm.DistanceBetweenTiles)
            {
                for (int y = box1.Y - UI.MainForm.DistanceBetweenTiles; y <= box1.Bottom + UI.MainForm.DistanceBetweenTiles; y++)
                {

                    if (y >= box2.Top && y <= box2.Bottom)
                    {
                        if (box2.Left > box1.Right)
                        {
                            if (image.GetPixel(box1.Right, y) != background)
                            {
                                if (image.GetPixel(box2.Left, y) != background)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (image.GetPixel(box1.Left, y) != background)
                            {
                                if (image.GetPixel(box2.Right, y) != background)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
            }


            if (ForkandBeard.Util.Geometry.GeometryHelper.GetYGapBetweenRectangles(box1, box2) <= UI.MainForm.DistanceBetweenTiles)
            {
                for (int x = box1.Left - UI.MainForm.DistanceBetweenTiles; x <= box1.Right + UI.MainForm.DistanceBetweenTiles; x++)
                {

                    if (x >= box2.Left && x <= box2.Right)
                    {
                        if (box2.Top > box1.Bottom)
                        {
                            if (image.GetPixel(x, box1.Bottom) != background)
                            {
                                if (image.GetPixel(x, box2.Top) != background)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (image.GetPixel(x, box1.Top) != background)
                            {
                                if (image.GetPixel(x, box2.Bottom) != background)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (this.Image != null)
                    {
                        //Me.Image.Dispose()
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
