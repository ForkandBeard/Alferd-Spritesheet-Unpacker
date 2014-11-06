using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ASU.BO
{
    public class Rotate
    {
        public Rotate()
        {
        }

        public static Bitmap RotateImage(Image inputImg, double degreeAngle)
        {
            PointF[] pointF = new PointF[] { new PointF(0f, 0f), new PointF((float)inputImg.Width, 0f), new PointF(0f, (float)inputImg.Height), new PointF((float)inputImg.Width, (float)inputImg.Height) };
            PointF[] rotationPoints = pointF;
            PointMath.RotatePoints(rotationPoints, new PointF((float)inputImg.Width / 2f, (float)inputImg.Height / 2f), degreeAngle);
            Rectangle bounds = PointMath.GetBounds(rotationPoints);
            Bitmap rotatedBitmap = new Bitmap(bounds.Width, bounds.Height);
            Graphics g = Graphics.FromImage(rotatedBitmap);
            try
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                Matrix m = new Matrix();
                m.RotateAt((float)degreeAngle, new PointF((float)inputImg.Width / 2f, (float)inputImg.Height / 2f));
                m.Translate((float)(-bounds.Left), (float)(-bounds.Top), MatrixOrder.Append);
                g.Transform = m;
                g.DrawImage(inputImg, 0, 0);
            }
            finally
            {
                if (g != null)
                {
                    ((IDisposable)g).Dispose();
                }
            }
            return rotatedBitmap;
        }
    }
}
