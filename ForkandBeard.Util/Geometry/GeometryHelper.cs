using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace ForkandBeard.Util.Geometry
{
    public class GeometryHelper
    {
        public static PointF GetBottomLeftOfRectangle(RectangleF pobjRectangle)
        {
            return new PointF(pobjRectangle.Left, pobjRectangle.Bottom);
        }

        public static PointF GetTopRightOfRectangle(RectangleF pobjRectangle)
        {
            return new PointF(pobjRectangle.Right, pobjRectangle.Top);
        }

        public static PointF GetTopLeftOfRectangle(RectangleF pobjRectangle)
        {
            return pobjRectangle.Location;
        }

        public static PointF GetBottomRightOfRectangle(RectangleF pobjRectangle)
        {
            return new PointF(pobjRectangle.Right, pobjRectangle.Bottom);
        }

        public static float GetXGapBetweenRectangles(RectangleF pobjRectangleA, RectangleF pobjRectangleB)
        {
            float sngReturn = 0;

            if (pobjRectangleA.Right >= pobjRectangleB.Left)
            {
                sngReturn = pobjRectangleA.Left - pobjRectangleB.Right;
            }
            else
            {
                sngReturn = pobjRectangleB.Left - pobjRectangleA.Right;
            }

            sngReturn = Math.Max(0, sngReturn);

            return sngReturn;
        }

        public static float GetYGapBetweenRectangles(RectangleF pobjRectangleA, RectangleF pobjRectangleB)
        {
            float sngReturn = 0;

            if (pobjRectangleA.Bottom >= pobjRectangleB.Top)
            {
                sngReturn = pobjRectangleA.Top - pobjRectangleB.Bottom;
            }
            else
            {
                sngReturn = pobjRectangleB.Top - pobjRectangleA.Bottom;
            }

            sngReturn = Math.Max(0, sngReturn);

            return sngReturn;
        }

        public static List<Line> RectangleTo4Lines(RectangleF pobjRectangle)
        {
            List<Line> colReturn = new List<Line>();

            colReturn.Add(new Line(GetTopLeftOfRectangle(pobjRectangle), new PointF(pobjRectangle.Right, pobjRectangle.Top)));
            colReturn.Add(new Line(GetTopLeftOfRectangle(pobjRectangle), new PointF(pobjRectangle.Left, pobjRectangle.Bottom)));
            colReturn.Add(new Line(new PointF(pobjRectangle.Left, pobjRectangle.Bottom), new PointF(pobjRectangle.Right, pobjRectangle.Bottom)));
            colReturn.Add(new Line(new PointF(pobjRectangle.Right, pobjRectangle.Bottom), new PointF(pobjRectangle.Right, pobjRectangle.Top)));

            return colReturn;
        }

        public static PointF GetCornerOfRectangleNearestPoint(RectangleF pobjRectangle, PointF pobjPoint)
        {
            if (Math.Abs(pobjRectangle.X - pobjPoint.X) < Math.Abs(pobjRectangle.Right - pobjPoint.X))
            {
                // Left side is nearer.
                if (Math.Abs(pobjRectangle.Y - pobjPoint.Y) < Math.Abs(pobjRectangle.Bottom - pobjPoint.Y))
                {
                    // TopLeft side is nearer.
                    return GetTopLeftOfRectangle(pobjRectangle);
                }
                else
                {
                    // BottomLeft side is nearer.
                    return GetBottomLeftOfRectangle(pobjRectangle);
                }
            }
            else
            {
                // Right side is nearer.
                if (Math.Abs(pobjRectangle.Y - pobjPoint.Y) < Math.Abs(pobjRectangle.Bottom - pobjPoint.Y))
                {
                    // TopRight side is nearer.
                    return GetTopRightOfRectangle(pobjRectangle);
                }
                else
                {
                    // BottomRight side is nearer.
                    return GetBottomRightOfRectangle(pobjRectangle);
                }
            }
        }

        public static PointF GetCentreOfRectangle(RectangleF pobjRectangle)
        {
            return new PointF(pobjRectangle.X + (pobjRectangle.Width / 2), pobjRectangle.Y + (pobjRectangle.Height / 2));
        }

        public static List<RectangleF> CreateSubRectanglesWithinRectangle(RectangleF pobjOuterRectangle, int pintRowCount, int pintColumnCount)
        {
            List<RectangleF> colReturn = new List<RectangleF>();
            float sngWidth = 0;
            float sngHeight = 0;

            sngHeight = pobjOuterRectangle.Height / pintRowCount;
            sngWidth = pobjOuterRectangle.Width / pintColumnCount;

            for (int r = 0; r <= pintRowCount - 1; r++)
            {
                for (int c = 0; c <= pintColumnCount - 1; c++)
                {
                    colReturn.Add(new RectangleF(pobjOuterRectangle.X + (c * sngWidth), pobjOuterRectangle.Y + (r * sngHeight), sngWidth, sngHeight));
                }
            }

            return colReturn;
        }

        public static Rectangle CreateShrunkRectangle(Rectangle pobjRectangle, float psngFactor)
        {
            Rectangle objReturn = default(Rectangle);

            objReturn = new Rectangle(0, 0, Convert.ToInt32(pobjRectangle.Width * psngFactor), Convert.ToInt32(pobjRectangle.Height * psngFactor));

            objReturn.X = Convert.ToInt32(pobjRectangle.X - ((pobjRectangle.Width - objReturn.Width) / 2));
            objReturn.Y = Convert.ToInt32(pobjRectangle.Y - ((pobjRectangle.Height - objReturn.Height) / 2));

            return objReturn;
        }
    }
}