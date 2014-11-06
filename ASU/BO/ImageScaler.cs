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
            Bitmap imgNew;
            BitmapData objWriteData;
            BitmapData objReadData;
            IntPtr ptrWriteScan0Address;
            IntPtr ptrReadScan0Address;
            int intWriteByteLength = 0;
            int intReadByteLength = 0;
            int intPixelByteLength = 0;

            switch (image.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    intPixelByteLength = 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    intPixelByteLength = 4;
                    break;
                default:
                    throw new Exception("Format not supported.");
            }

            objReadData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

            //http://msdn.microsoft.com/en-us/library/system.drawing.bitmapdata.aspx
            imgNew = new Bitmap(Convert.ToInt32(image.Width * scale), Convert.ToInt32(image.Height * scale), image.PixelFormat);

            objWriteData = imgNew.LockBits(new Rectangle(0, 0, imgNew.Width, imgNew.Height), ImageLockMode.WriteOnly, image.PixelFormat);

            ptrWriteScan0Address = objWriteData.Scan0;
            ptrReadScan0Address = objReadData.Scan0;

            intWriteByteLength = Math.Abs(objWriteData.Stride) * imgNew.Height;
            intReadByteLength = Math.Abs(objReadData.Stride) * image.Height;

            byte[] arrWriteRGBValues = null;
            List<byte> colWriteRGBBytes = new List<byte>();
            byte[] arrReadRGBValues = new byte[intReadByteLength];
            int intReadPadding = 0;

            System.Runtime.InteropServices.Marshal.Copy(ptrReadScan0Address, arrReadRGBValues, 0, intReadByteLength);

            intReadPadding = (objReadData.Stride - (image.Width * intPixelByteLength));
            // 3 = R,G,B. 4 = A,R,G,B.
            List<byte> colStrideBytes = new List<byte>();

            for (int i = 0; i <= arrReadRGBValues.Length - 1; i += intPixelByteLength)
            {
                for (int scaleCounter = 0; scaleCounter <= scale - 1; scaleCounter++)
                {
                    for (int _rgb = 0; _rgb <= intPixelByteLength - 1; _rgb++)
                    {
                        colStrideBytes.Add(arrReadRGBValues[i + _rgb]);
                    }
                }

                // Strides are rounded up to four bytes...

                if (Math.Ceiling((double) colStrideBytes.Count / 4) == (objWriteData.Stride / 4))
                {
                    if (colStrideBytes.Count != objWriteData.Stride)
                    {
                        // ... so pad short rows.
                        for (int k = 0; k <= (objWriteData.Stride - colStrideBytes.Count) - 1; k++)
                        {
                            colStrideBytes.Add(0);
                        }
                    }

                    for (int scaleCounter = 0; scaleCounter <= scale - 1; scaleCounter++)
                    {
                        colWriteRGBBytes.AddRange(colStrideBytes);
                    }
                    colStrideBytes = new List<byte>();
                    // Read strides are also rounded up to four bytes, so skip the padded bytes.
                    i += intReadPadding;
                }
            }

            arrWriteRGBValues = colWriteRGBBytes.ToArray();

            System.Runtime.InteropServices.Marshal.Copy(arrWriteRGBValues, 0, ptrWriteScan0Address, intWriteByteLength);

            // Unlock the bits.
            imgNew.UnlockBits(objWriteData);
            image.UnlockBits(objReadData);

            return imgNew;
        }
    }
}
