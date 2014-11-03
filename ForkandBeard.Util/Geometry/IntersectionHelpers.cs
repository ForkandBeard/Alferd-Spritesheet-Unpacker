using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Drawing;

namespace ForkandBeard.Util.Geometry
{
    public class IntersectionHelpers
    {

        /// <summary>
        /// This is based off an explanation and expanded math presented by Paul Bourke:
        /// 
        /// It takes two lines as inputs and returns true if they intersect, false if they 
        /// don't.
        /// If they do, ptIntersection returns the point where the two lines intersect.  
        /// </summary>
        /// <param name="L1">The first line</param>
        /// <param name="L2">The second line</param>
        /// <param name="ptIntersection">The point where both lines intersect (if they do).</param>
        /// <returns></returns>
        /// <remarks>See http:'local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/</remarks>
        public static bool DoLinesIntersect(Line L1, Line L2, ref PointF ptIntersection)
        {
            // Denominator for ua and ub are the same, so store this calculation
            decimal d = Convert.ToDecimal((L2.Y2 - L2.Y1) * (L1.X2 - L1.X1) - (L2.X2 - L2.X1) * (L1.Y2 - L1.Y1));

            //n_a and n_b are calculated as seperate values for readability
            decimal n_a = Convert.ToDecimal((L2.X2 - L2.X1) * (L1.Y1 - L2.Y1) - (L2.Y2 - L2.Y1) * (L1.X1 - L2.X1));

            decimal n_b = Convert.ToDecimal((L1.X2 - L1.X1) * (L1.Y1 - L2.Y1) - (L1.Y2 - L1.Y1) * (L1.X1 - L2.X1));

            // Make sure there is not a division by zero - this also indicates that
            // the lines are parallel.  
            // If n_a and n_b were both equal to zero the lines would be on top of each 
            // other (coincidental).  This check is not done because it is not 
            // necessary for this implementation (the parallel check accounts for this).
            if (d == 0)
                return false;

            // Calculate the intermediate fractional point that the lines potentially intersect.
            decimal ua = n_a / d;
            decimal ub = n_b / d;

            // The fractional point will be between 0 and 1 inclusive if the lines
            // intersect.  If the fractional calculation is larger than 1 or smaller
            // than 0 the lines would need to be longer to intersect.
            if (ua >= 0m && ua <= 1m && ub >= 0m && ub <= 1m)
            {
                ptIntersection.X = Convert.ToSingle(Convert.ToDecimal(L1.X1) + (ua * Convert.ToDecimal(L1.X2 - L1.X1)));
                ptIntersection.Y = Convert.ToSingle(Convert.ToDecimal(L1.Y1) + (ua * Convert.ToDecimal(L1.Y2 - L1.Y1)));
                return true;
            }

            return false;
        }

        public static bool DoesLineIntersectRectangle(RectangleF pobjRectangle, Line pobjLine, ref Line pobjIntersectingPortionOfLine)
        {
            RectangleF objLineAsRectangle = default(RectangleF);
            objLineAsRectangle = pobjLine.ToRectangle();

            if (!pobjRectangle.IntersectsWith(objLineAsRectangle))
            {
                return false;
            }
            else
            {
                bool blnIsPointAIn = false;
                bool blnIsPointBIn = false;

                blnIsPointAIn = pobjRectangle.Contains(pobjLine.PointA);
                blnIsPointBIn = pobjRectangle.Contains(pobjLine.PointB);

                if (blnIsPointAIn && blnIsPointBIn)
                {
                    pobjIntersectingPortionOfLine = pobjLine;
                }
                else
                {
                    PointF[] colIntersections = new PointF[2];
                    // Either one or two intersections.
                    List<Line> colLines = null;
                    colLines = GeometryHelper.RectangleTo4Lines(pobjRectangle);

                    foreach (Line objLine in colLines)
                    {
                        if (colIntersections[0].IsEmpty)
                        {
                            DoLinesIntersect(objLine, pobjLine, ref colIntersections[0]);
                        }
                        else if (colIntersections[1].IsEmpty)
                        {
                            DoLinesIntersect(objLine, pobjLine, ref colIntersections[1]);
                        }
                    }

                    if (!blnIsPointAIn && !blnIsPointBIn)
                    {
                        pobjIntersectingPortionOfLine = new Line(colIntersections[0], colIntersections[1]);
                    }
                    else
                    {
                        if (blnIsPointAIn)
                        {
                            pobjIntersectingPortionOfLine = new Line(colIntersections[0], pobjLine.PointA);
                        }
                        else
                        {
                            pobjIntersectingPortionOfLine = new Line(colIntersections[0], pobjLine.PointB);
                        }
                    }
                }

                return true;
            }
        }

        public static bool DoesLineIntersectRectangle(RectangleF pobjRectangle, Line pobjLine)
        {
            RectangleF objLineAsRectangle = default(RectangleF);
            objLineAsRectangle = pobjLine.ToRectangle();

            if (!pobjRectangle.IntersectsWith(objLineAsRectangle))
            {
                return false;

            }
            else
            {
                if (pobjRectangle.Contains(pobjLine.PointA) && pobjRectangle.Contains(pobjLine.PointB))
                {
                    return true;
                }

                List<Line> colLines = null;
                colLines = GeometryHelper.RectangleTo4Lines(pobjRectangle);
                PointF toRef = new PointF();
                foreach (Line objLine in colLines)
                {
                    if (DoLinesIntersect(pobjLine, objLine, ref toRef))
                    {
                        return true;
                    }
                }

                // False when line just misses a corner of the rectangle.
                return false;
            }
        }

    }
}