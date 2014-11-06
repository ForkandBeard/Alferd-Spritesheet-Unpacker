using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ASU.BO
{
    /// <summary>
    /// Not sure where I fot this code from, but it isn't mine.
    /// </summary>
    internal static class PointMath
    {
        private static double DegreeToRadian(double angle)
        {
            return 3.14159265358979 * angle / 180;
        }

        public static Rectangle GetBounds(PointF[] pnts)
        {
            RectangleF boundsF = PointMath.GetBoundsF(pnts);
            Rectangle rectangle = new Rectangle((int)Math.Round((double)boundsF.Left), (int)Math.Round((double)boundsF.Top), (int)Math.Round((double)boundsF.Width), (int)Math.Round((double)boundsF.Height));
            return rectangle;
        }

        public static RectangleF GetBoundsF(PointF[] pnts)
        {
            float left = pnts[0].X;
            float right = pnts[0].X;
            float top = pnts[0].Y;
            float bottom = pnts[0].Y;
            for (int i = 1; i < (int)pnts.Length; i++)
            {
                if (pnts[i].X < left)
                {
                    left = pnts[i].X;
                }
                else if (pnts[i].X > right)
                {
                    right = pnts[i].X;
                }
                if (pnts[i].Y < top)
                {
                    top = pnts[i].Y;
                }
                else if (pnts[i].Y > bottom)
                {
                    bottom = pnts[i].Y;
                }
            }
            RectangleF rectangleF = new RectangleF(left, top, (float)Math.Abs(right - left), (float)Math.Abs(bottom - top));
            return rectangleF;
        }

        public static PointF RotatePoint(PointF pnt, double degreeAngle)
        {
            PointF pointF = PointMath.RotatePoint(pnt, new PointF(0f, 0f), degreeAngle);
            return pointF;
        }

        public static PointF RotatePoint(PointF pnt, PointF origin, double degreeAngle)
        {
            double radAngle = PointMath.DegreeToRadian(degreeAngle);
            PointF newPoint = new PointF();
            double deltaX = (double)(pnt.X - origin.X);
            double deltaY = (double)(pnt.Y - origin.Y);
            newPoint.X = (float)((double)origin.X + (Math.Cos(radAngle) * deltaX - Math.Sin(radAngle) * deltaY));
            newPoint.Y = (float)((double)origin.Y + (Math.Sin(radAngle) * deltaX + Math.Cos(radAngle) * deltaY));
            return newPoint;
        }

        public static void RotatePoints(PointF[] pnts, double degreeAngle)
        {
            for (int i = 0; i < (int)pnts.Length; i++)
            {
                pnts[i] = PointMath.RotatePoint(pnts[i], degreeAngle);
            }
        }

        public static void RotatePoints(PointF[] pnts, PointF origin, double degreeAngle)
        {
            for (int i = 0; i < (int)pnts.Length; i++)
            {
                pnts[i] = PointMath.RotatePoint(pnts[i], origin, degreeAngle);
            }
        }
    }
}
