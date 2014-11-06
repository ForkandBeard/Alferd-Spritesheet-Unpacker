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
        public RegionUnpacker(Bitmap pimgImage, Rectangle pobjRegion, Color pobjBackgroundColour)
        {
            this.Image = pimgImage;
            this.Region = pobjRegion;
            this.BackgroundColour = pobjBackgroundColour;
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

        private static List<Rectangle> CreateBoxes(Bitmap pobjImage, Rectangle pobjRegion, Color pobjBackground)
        {
            List<Rectangle> colReturn = new List<Rectangle>();
            Point objPresentPixel = default(Point);
            Rectangle objNewBox = default(Rectangle);
            int x2 = 0;
            int y2 = 0;
            Color objPresentColour = default(Color);

            for (int y = pobjRegion.Top; y <= pobjRegion.Bottom; y++)
            {

                for (int x = pobjRegion.Left; x <= pobjRegion.Right; x++)
                {
                    if (x > 0 && x < pobjImage.Width && y > 0 && y < pobjImage.Height)
                    {
                        objPresentPixel = new Point(x, y);

                        objPresentColour = pobjImage.GetPixel(x, y);

                        if (objPresentColour != pobjBackground)
                        {
                            objNewBox = new Rectangle(objPresentPixel, new Size(0, 0));
                            x2 = x;

                            while (x2 < (pobjImage.Width - 1) && pobjImage.GetPixel(x2, y) != pobjBackground)
                            {
                                x2 += 1;
                                objNewBox = new Rectangle(objNewBox.X, objNewBox.Y, objNewBox.Width + 1, objNewBox.Height);
                            }

                            y2 = y;
                            while (y2 < (pobjImage.Height - 1) && pobjImage.GetPixel(x2, y2) != pobjBackground)
                            {
                                y2 += 1;
                                objNewBox = new Rectangle(objNewBox.X, objNewBox.Y, objNewBox.Width, objNewBox.Height + 1);
                            }

                            y2 = y + objNewBox.Height;
                            while (y2 < (pobjImage.Height - 1) && pobjImage.GetPixel(x, y2) != pobjBackground)
                            {
                                y2 += 1;
                                objNewBox = new Rectangle(objNewBox.X, objNewBox.Y, objNewBox.Width, objNewBox.Height + 1);
                            }

                            colReturn.Add(objNewBox);

                            x += (objNewBox.Width + 1);
                        }
                    }
                }
            }

            return colReturn;
        }

        public static void CombineBoxes(ref List<Rectangle> pcolBoxes, Color pobjBackground, Bitmap pobjImage)
        {
            int intIndex = 0;
            do
            {
                intIndex = CombineFirstOverlappingBox(ref pcolBoxes, pobjBackground, pobjImage, intIndex);
            } while (intIndex != -1);
        }

        private static int CombineFirstOverlappingBox(ref List<Rectangle> pcolBoxes, Color pobjBackground, Bitmap pobjImage, int pintStartIndex)
        {
            Rectangle objNewBox = Rectangle.Empty;
            List<Rectangle> colOldBoxes = new List<Rectangle>();
            int intReturn = -1;
            Rectangle objBox = default(Rectangle);

            // Each objBox As Rectangle In pcolBoxes
            for (int i = pintStartIndex; i <= pcolBoxes.Count - 1; i++)
            {
                objBox = pcolBoxes[i];

                foreach (Rectangle objCollider in pcolBoxes)
                {

                    if (objBox != objCollider)
                    {

                        if (DoBoxesContainAdjacentOrOverlappingPixels(objBox, objCollider, pobjBackground, pobjImage))
                        {
                            objNewBox = objBox;

                            intReturn = i;
                            if (objCollider.Right > objNewBox.Right)
                            {
                                objNewBox.Width = objCollider.Right - objNewBox.Left;
                            }

                            if (objCollider.Left < objNewBox.Left)
                            {
                                objNewBox.Width += objNewBox.Left - objCollider.Left;
                            }

                            if (objCollider.Bottom > objNewBox.Bottom)
                            {
                                objNewBox.Height = objCollider.Bottom - objNewBox.Top;
                            }

                            if (objCollider.Top < objNewBox.Top)
                            {
                                objNewBox.Height += objNewBox.Top - objCollider.Top;
                            }

                            objNewBox.X = Math.Min(objNewBox.X, objCollider.X);
                            objNewBox.Y = Math.Min(objCollider.Y, objNewBox.Y);

                            colOldBoxes.Add(objBox);
                            colOldBoxes.Add(objCollider);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }

                if (objNewBox != Rectangle.Empty)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            if (objNewBox != Rectangle.Empty)
            {
                foreach (Rectangle objBox2 in colOldBoxes)
                {
                    pcolBoxes.Remove(objBox2);
                }
                pcolBoxes.Add(objNewBox);
            }

            return intReturn;
        }

        public static void DeleteAllTempFiles()
        {
            try
            {
                Console.WriteLine("Deleting from " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                foreach (string strFile in System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "asu_temp_spritesheet*", System.IO.SearchOption.TopDirectoryOnly))
                {
                    Console.WriteLine("Deleting " + strFile);
                    System.IO.File.Delete(strFile);
                }
            }
            catch (Exception ignore)
            {
                Console.WriteLine(ignore.ToString());
            }
        }

        private static bool DoBoxesContainAdjacentOrOverlappingPixels(Rectangle pobjBox1, Rectangle pobjBox2, Color pobjBackground, Bitmap pobjImage)
        {
            Rectangle objIntersection = default(Rectangle);

            if (pobjBox1.IntersectsWith(pobjBox2))
            {
                objIntersection = Rectangle.Intersect(pobjBox1, pobjBox2);
                for (int x = objIntersection.X; x <= objIntersection.Right; x++)
                {
                    for (int y = objIntersection.Y; y <= objIntersection.Bottom; y++)
                    {
                        if (pobjImage.GetPixel(x, y) != pobjBackground)
                        {
                            return true;
                        }
                    }
                }

            }


            if (ForkandBeard.Util.Geometry.GeometryHelper.GetXGapBetweenRectangles(pobjBox1, pobjBox2) <= UI.MainForm.DistanceBetweenTiles)
            {
                for (int y = pobjBox1.Y - UI.MainForm.DistanceBetweenTiles; y <= pobjBox1.Bottom + UI.MainForm.DistanceBetweenTiles; y++)
                {

                    if (y >= pobjBox2.Top && y <= pobjBox2.Bottom)
                    {
                        if (pobjBox2.Left > pobjBox1.Right)
                        {
                            if (pobjImage.GetPixel(pobjBox1.Right, y) != pobjBackground)
                            {
                                if (pobjImage.GetPixel(pobjBox2.Left, y) != pobjBackground)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (pobjImage.GetPixel(pobjBox1.Left, y) != pobjBackground)
                            {
                                if (pobjImage.GetPixel(pobjBox2.Right, y) != pobjBackground)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
            }


            if (ForkandBeard.Util.Geometry.GeometryHelper.GetYGapBetweenRectangles(pobjBox1, pobjBox2) <= UI.MainForm.DistanceBetweenTiles)
            {
                for (int x = pobjBox1.Left - UI.MainForm.DistanceBetweenTiles; x <= pobjBox1.Right + UI.MainForm.DistanceBetweenTiles; x++)
                {

                    if (x >= pobjBox2.Left && x <= pobjBox2.Right)
                    {
                        if (pobjBox2.Top > pobjBox1.Bottom)
                        {
                            if (pobjImage.GetPixel(x, pobjBox1.Bottom) != pobjBackground)
                            {
                                if (pobjImage.GetPixel(x, pobjBox2.Top) != pobjBackground)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (pobjImage.GetPixel(x, pobjBox1.Top) != pobjBackground)
                            {
                                if (pobjImage.GetPixel(x, pobjBox2.Bottom) != pobjBackground)
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
