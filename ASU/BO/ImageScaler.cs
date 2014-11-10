using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ASU.BO
{
    class ImageScaler
    {
        public static Bitmap IncreaseScale(Bitmap image, int scale)
        {
            Bitmap newImage;
            BitmapData writeData;
            BitmapData readData;
            IntPtr writeScan0Address;
            IntPtr readScan0Address;
            int writeByteLength = 0;
            int readByteLength = 0;
            int pixelByteLength = 0;

            switch (image.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    pixelByteLength = 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    pixelByteLength = 4;
                    break;
                default:
                    throw new ArgumentException(String.Format("ImageFormat [{0}] not supported.", image.PixelFormat.ToString()));
            }

            readData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

            //http://msdn.microsoft.com/en-us/library/system.drawing.bitmapdata.aspx
            newImage = new Bitmap(Convert.ToInt32(image.Width * scale), Convert.ToInt32(image.Height * scale), image.PixelFormat);

            writeData = newImage.LockBits(new Rectangle(0, 0, newImage.Width, newImage.Height), ImageLockMode.WriteOnly, image.PixelFormat);

            writeScan0Address = writeData.Scan0;
            readScan0Address = readData.Scan0;

            writeByteLength = Math.Abs(writeData.Stride) * newImage.Height;
            readByteLength = Math.Abs(readData.Stride) * image.Height;

            byte[] writeRGBValues = null;
            List<byte> writeRGBBytes = new List<byte>();
            byte[] readRGBValues = new byte[readByteLength];
            int readPadding = 0;

            System.Runtime.InteropServices.Marshal.Copy(readScan0Address, readRGBValues, 0, readByteLength);

            readPadding = (readData.Stride - (image.Width * pixelByteLength));
            // 3 = R,G,B. 4 = A,R,G,B.
            List<byte> strideBytes = new List<byte>();

            for (int i = 0; i <= readRGBValues.Length - 1; i += pixelByteLength)
            {
                for (int scaleCounter = 0; scaleCounter <= scale - 1; scaleCounter++)
                {
                    for (int _rgb = 0; _rgb <= pixelByteLength - 1; _rgb++)
                    {
                        strideBytes.Add(readRGBValues[i + _rgb]);
                    }
                }

                // Strides are rounded up to four bytes...
                if (Math.Ceiling((double) strideBytes.Count / 4) == (writeData.Stride / 4))
                {
                    if (strideBytes.Count != writeData.Stride)
                    {   // ... so pad short rows.
                        for (int k = 0; k <= (writeData.Stride - strideBytes.Count) - 1; k++)
                        {
                            strideBytes.Add(0);
                        }
                    }

                    for (int scaleCounter = 0; scaleCounter <= scale - 1; scaleCounter++)
                    {
                        writeRGBBytes.AddRange(strideBytes);
                    }
                    strideBytes = new List<byte>();
                    // Read strides are also rounded up to four bytes, so skip the padded bytes.
                    i += readPadding;
                }
            }

            writeRGBValues = writeRGBBytes.ToArray();

            System.Runtime.InteropServices.Marshal.Copy(writeRGBValues, 0, writeScan0Address, writeByteLength);

            // Unlock the bits.
            newImage.UnlockBits(writeData);
            image.UnlockBits(readData);

            return newImage;
        }
    }
}
