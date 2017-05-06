using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ASU.BO
{
    public class ImageUnpacker
    {
        public bool IsLarge { get; set; }

        private Bitmap original;
        private object originalLock = new object();
        private int pcComplete = 0;
        private Color? backgroundColour = null;
        private object boxesLock = new object();
        private List<Rectangle> boxes;
        private int areaUnpacked;
        private object areaUnpackedLock = new object();
        private int areaToUnpack;
        private int threadCounter = 0;
        private object threadCompleteCounterLock = new object();
        private int threadCompleteCounter = 0;
        private bool areAllThreadsCreated;
        private bool isUnpackingComplete = false;
        private Size originalSize;
        private System.Drawing.Imaging.ColorPalette pallette = null;
        private bool _isBackgroundColourSet = false;
        private bool _isUnpacking = false;
        private const int INT_MAX_REGION_WIDTH = 400;

        public string FileName { get; set; }
        public int ColoursCount = 0;

        public event UnpackingCompleteEventHandler UnpackingComplete;        
        public event PcCompleteChangedEventHandler PcCompleteChanged;

        public delegate void PcCompleteChangedEventHandler(int pcComplete);
        public delegate void UnpackingCompleteEventHandler();

        public ImageUnpacker(Bitmap image, string fileName, bool removeTransparency)
        {
            if (image.Palette.Entries.Length > 0)
            {
                this.pallette = image.Palette;
            }
            this.original = new Bitmap((Bitmap)image.Clone());
            this.originalSize = image.Size;
            if (removeTransparency)
            {
                this.original = this.RemoveTransparencyFromImage(this.original);
            }
            this.boxes = new List<Rectangle>();
            this.FileName = fileName;
            this.IsLarge = (this.original.Width * this.original.Height) > (800 * 800);
        }

        private Bitmap RemoveTransparencyFromImage(Bitmap image)
        {
            Dictionary<int, Color> coloursByArgb = new Dictionary<int, Color>();
            Dictionary<int, Color> transparentPixelsByCoord = new Dictionary<int, Color>();
            Dictionary<Color, Color> opaquedByTransparent = new Dictionary<Color, Color>();
            Color pixel;
            bool containsTransparency = false;
            Color transparentPixel;
            Color opaquedPixel;

            for(int x = 0; x < image.Width; x++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    pixel = image.GetPixel(x, y);

                    if (!coloursByArgb.ContainsKey(pixel.ToArgb()))
                    {
                        coloursByArgb.Add(pixel.ToArgb(), pixel);     
                    }
                    if (pixel.A < 255)
                    {
                        containsTransparency = true;
                        transparentPixelsByCoord.Add(GetHashFromCoord(new Point(x, y)), pixel);
                    }
                }
            }

            if (containsTransparency)
            {
                foreach (int coordHash in transparentPixelsByCoord.Keys)
                {
                    Point coord = GetCoordFromHash(coordHash);

                    transparentPixel = transparentPixelsByCoord[coordHash];
                    if (opaquedByTransparent.ContainsKey(transparentPixel))
                    {
                        opaquedPixel = opaquedByTransparent[transparentPixel];
                    }
                    else
                    {
                        opaquedPixel = Color.FromArgb(255, transparentPixel);

                        do
                        {
                            if (transparentPixel.R > (255 / 2))
                            {
                                opaquedPixel = Color.FromArgb(opaquedPixel.R - 1, opaquedPixel.G, opaquedPixel.B);
                            }
                            else
                            {
                                opaquedPixel = Color.FromArgb(opaquedPixel.R + 1, opaquedPixel.G, opaquedPixel.B);
                            }
                        } while (coloursByArgb.ContainsKey(opaquedPixel.ToArgb()));

                        coloursByArgb.Remove(transparentPixel.ToArgb());
                        coloursByArgb.Add(opaquedPixel.ToArgb(), opaquedPixel);
                        opaquedByTransparent.Add(transparentPixel, opaquedPixel);
                    }
                    image.SetPixel(coord.X, coord.Y, opaquedPixel);
                }
            }

            return image;
        }

        public System.Drawing.Imaging.ColorPalette GetPallette()
        {
            return this.pallette;
        }

        public Size GetSize()
        {
            return this.originalSize;
        }

        public bool IsUnpacking()
        {
            return this._isUnpacking;
        }

        public bool IsBackgroundColourSet()
        {
            return this._isBackgroundColourSet;
        }

        public Color GetBackgroundColour()
        {
            if (this.backgroundColour.HasValue)
            {
                return this.backgroundColour.Value;
            }
            else
            {
                return Color.Black;
            }
        }

        public List<Rectangle> GetBoxes()
        {
            return new List<Rectangle>(this.boxes);
        }

        public bool IsUnpacked()
        {
            return this.isUnpackingComplete;
        }

        public int GetPcComplete()
        {
            return this.pcComplete;
        }

        public Bitmap GetOriginalClone()
        {
            Bitmap clone = default(Bitmap);

            lock ((this.originalLock))
            {
                clone = new Bitmap((Bitmap)this.original.Clone());
            }

            return clone;
        }

        private void SetPcComplete(int pcComplete)
        {
            if(pcComplete > 100)
            {   // TODO: Fix this bug.
                pcComplete = 100;
            }
            if (pcComplete > this.pcComplete)
            {
                this.pcComplete = pcComplete;
                if (PcCompleteChanged != null)
                {
                    PcCompleteChanged(this.pcComplete);
                }
            }
        }

        public void StartUnpacking()
        {
            System.Threading.Thread newThread = new System.Threading.Thread(this.Unpack);
            this.isUnpackingComplete = false;
            this.pcComplete = 0;
            this.boxes.Clear();
            this._isUnpacking = true;
            this.threadCounter = 0;
            this.threadCompleteCounter = 0;
            newThread.IsBackground = true;
            newThread.Start();
        }

        public static List<Rectangle> OrderBoxes(List<Rectangle> boxes, Enums.SelectAllOrder selectAllOrder, Size spriteSheetSize)
        {
            SortedDictionary<int, List<Rectangle>> orderedBoxes = new SortedDictionary<int, List<Rectangle>>();
            int location = 0;
            List<Rectangle> returnedOrder = new List<Rectangle>();


            foreach (Rectangle box in boxes)
            {
                switch (selectAllOrder)
                {
                    case Enums.SelectAllOrder.TopLeft:
                        location = box.X + (box.Y * spriteSheetSize.Width);
                        break;
                    case Enums.SelectAllOrder.BottomLeft:
                        location = box.X + ((box.Y + box.Height) * spriteSheetSize.Width);
                        break;
                    case Enums.SelectAllOrder.Centre:
                        location = Convert.ToInt32((box.X + (box.Width / 2)) + ((box.Y + (box.Height / 2)) * spriteSheetSize.Width));
                        break;
                }

                if (!orderedBoxes.ContainsKey(location))
                {
                    orderedBoxes.Add(location, new List<Rectangle>());
                }
                orderedBoxes[location].Add(box);
            }

            foreach (int locationKey in orderedBoxes.Keys)
            {
                foreach (Rectangle box in orderedBoxes[locationKey])
                {
                    returnedOrder.Add(box);
                }
            }

            return returnedOrder;
        }

        private void Unpack(object state)
        {
            try
            {
                int subRegionCount;
                int xSize = 0;
                int ySize = 0;
                decimal totalSize;
                Rectangle region = default(Rectangle);
                List<System.Threading.Thread> regionThreads = new List<System.Threading.Thread>();
                List<Rectangle> regions = new List<Rectangle>();
                System.Threading.Thread regionThread = null;

                this.areAllThreadsCreated = false;
                if (!this.backgroundColour.HasValue)
                {
                    this.SetBackgroundColour(this.GetOriginalClone());
                }
                this.SetPcComplete(10);

                if(Environment.ProcessorCount > 1)
                {
                    subRegionCount = 4;
                }
                else
                {
                    subRegionCount = 2;
                }

                ySize = Convert.ToInt32(Math.Ceiling((double)this.originalSize.Height / (double)subRegionCount));
                xSize = Convert.ToInt32(Math.Ceiling((double)this.originalSize.Width / (double)subRegionCount));

                this.areaToUnpack = this.originalSize.Width * this.originalSize.Height;

                totalSize = this.originalSize.Height * this.originalSize.Width;
                
                for (int y = 0; y < this.originalSize.Height; y += ySize)
                {
                    for (int x = 0; x < this.originalSize.Width; x += xSize)
                    {
                        region = new Rectangle(x, y, Math.Min(xSize+1, (this.originalSize.Width - x) - 1), Math.Min(ySize+1, (this.originalSize.Height - y) - 1));
                        regionThread = new System.Threading.Thread(this.HandleDividedAreaThread);
                        regionThread.Name = "Region thread " + (y * (xSize * 4)) + x;
                        this.threadCounter += 1;
                        regionThread.IsBackground = true;
                        
                        regionThreads.Add(regionThread);
                        regions.Add(region);                        
                    }
                }

                for (int k = 0; k < regionThreads.Count; k++)
                {
                    regionThreads[k].Start(regions[k]);
                    this.SetPcComplete(10 + Convert.ToInt32((((Math.Min(regions[k].Y, this.originalSize.Height) * (this.originalSize.Width - 1)) + Math.Min(regions[k].X, this.originalSize.Width)) / totalSize) * 10));
                }

                this.SetPcComplete(20);
                this.areAllThreadsCreated = true;
                lock ((this.threadCompleteCounterLock))
                {
                    if (this.threadCompleteCounter == this.threadCounter)
                    {
                        this.HandleUnpackComplete();
                    }
                }
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }

        private void HandleDividedAreaThread(object regionObject)
        {
            try
            {
                if (Environment.ProcessorCount == 1)
                {
                    System.Threading.Thread.Sleep(25);
                }

                this.HandleDividedArea((Rectangle)regionObject, true, this.GetOriginalClone());
            }
            catch (Exception ex)
            {
                ForkandBeard.Logic.ExceptionHandler.HandleException(ex, "cat@forkandbeard.co.uk");
            }
        }


        private void HandleDividedArea(Rectangle region, bool updateCounter, Bitmap image)
        {
            if (region.Width > INT_MAX_REGION_WIDTH || region.Height > INT_MAX_REGION_WIDTH)
            {
                List<Rectangle> quarterRegions = new List<Rectangle>();

                // Top left.
                quarterRegions.Add(new Rectangle(region.X, region.Y, Convert.ToInt32(region.Width / 2), Convert.ToInt32(region.Height / 2)));
                // Top right.
                quarterRegions.Add(new Rectangle(region.X + Convert.ToInt32(region.Width / 2), region.Y, Convert.ToInt32(region.Width / 2) + 1, Convert.ToInt32(region.Height / 2)));
                // Bottom left.
                quarterRegions.Add(new Rectangle(region.X, region.Y + Convert.ToInt32(region.Height / 2), Convert.ToInt32(region.Width / 2), Convert.ToInt32(region.Height / 2) + 1));
                // Bottom right.
                quarterRegions.Add(new Rectangle(region.X + Convert.ToInt32(region.Width / 2), region.Y + Convert.ToInt32(region.Height / 2), Convert.ToInt32(region.Width / 2) + 1, Convert.ToInt32(region.Height / 2) + 1));
                foreach (Rectangle quarter in quarterRegions)
                {
                    this.HandleDividedArea(quarter, false, image);
                }
            }
            else
            {
                using (RegionUnpacker unpacker = new RegionUnpacker(image, region, this.backgroundColour.Value))
                {
                    unpacker.UnpackRegion();
                    lock ((this.areaUnpackedLock))
                    {
                        this.areaUnpacked += Convert.ToInt32((double)(region.Width * region.Height) * 0.8f);
                    }

                    this.SetPcComplete(20 + Convert.ToInt32(((double)this.areaUnpacked / (double)this.areaToUnpack) * 75f));

                    lock ((this.boxesLock))
                    {
                        this.boxes.AddRange(unpacker.Boxes);
                    }
                }

                lock ((this.areaUnpackedLock))
                {
                    this.areaUnpacked += Convert.ToInt32((double)(region.Width * region.Height) * 0.2f);
                }

                this.SetPcComplete(20 + Convert.ToInt32(((double)this.areaUnpacked / (double)this.areaToUnpack) * 80f));
            }

            if (updateCounter)
            {
                lock ((this.threadCompleteCounterLock))
                {
                    this.threadCompleteCounter += 1;
                    if (this.areAllThreadsCreated && this.threadCompleteCounter == this.threadCounter)
                    {
                        this.HandleUnpackComplete();
                    }
                }
            }
        }

        private void SetBackgroundColour(Bitmap image)
        {
            Dictionary<int, int> colourCountsByArgb = new Dictionary<int, int>();
            Color presentColour; 
            int maxCount = 0;

            for (int x = 0; x <= this.originalSize.Width - 1; x++)
            {
                for (int y = 0; y <= this.originalSize.Height - 1; y++)
                {
                    presentColour = image.GetPixel(x, y);

                    if (!colourCountsByArgb.ContainsKey(presentColour.ToArgb()))
                    {
                        colourCountsByArgb.Add(presentColour.ToArgb(), 1);
                    }
                    else
                    {
                        colourCountsByArgb[presentColour.ToArgb()] += 1;
                    }

                    if ((x + y) % 100 == 0)
                    {
                        decimal total = 0;

                        total = this.originalSize.Width * this.originalSize.Height;
                        this.SetPcComplete(Convert.ToInt32((((x * (this.originalSize.Height - 1)) + y) / total) * 10));
                    }
                }
            }

            foreach (int colourArgb in colourCountsByArgb.Keys)
            {
                if (colourCountsByArgb[colourArgb] >= maxCount)
                {
                    maxCount = colourCountsByArgb[colourArgb];
                    this.backgroundColour = Color.FromArgb(colourArgb);
                }
            }
            image.Dispose();
            this._isBackgroundColourSet = true;
            this.ColoursCount = colourCountsByArgb.Count - 1;
        }

        private void HandleUnpackComplete()
        {
            using (Bitmap img = this.GetOriginalClone())
            {
                lock ((this.boxesLock))
                {
                    RegionUnpacker.CombineBoxes(ref this.boxes, this.backgroundColour.Value, img);
                }
            }

            this.isUnpackingComplete = true;
            this._isUnpacking = false;
            this.SetPcComplete(100);
            if (UnpackingComplete != null)
            {
                UnpackingComplete();
            }
        }

        private Point GetCoordFromHash(int hash)
        {
            return new Point((int)(hash >> 16), (int)(hash & 0x0000FFFF));
        }

        private int GetHashFromCoord(Point point)
        {
            return (point.X << 16) + point.Y;
        }
    }
}
