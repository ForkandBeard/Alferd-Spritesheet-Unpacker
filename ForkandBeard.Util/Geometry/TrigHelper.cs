using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace ForkandBeard.Util.Geometry
{
    public class TrigHelper
    {
        /// <summary>
        /// Just a fast way of getting distance to compare two distances.
        /// </summary>
        /// <param name="pobjPointA"></param>
        /// <param name="pobjPointB"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static float GetRelativeDistanceBetweenPoints(PointF pobjPointA, PointF pobjPointB)
        {
            float sngReturn = 0;
            PointF objOffset = default(PointF);

            objOffset = new PointF(pobjPointA.X - pobjPointB.X, pobjPointA.Y - pobjPointB.Y);
            objOffset.Y = Math.Abs(objOffset.Y);
            objOffset.X = Math.Abs(objOffset.X);

            sngReturn = (objOffset.X * objOffset.X) + (objOffset.Y * objOffset.Y);

            return sngReturn;
        }

        public static float GetRelativeDistanceBetweenPoints(PointF pobjPointA, PointF pobjPointB, float psngZA, float psngZB)
        {
            float sngReturn = 0;
            PointF objOffset = default(PointF);
            float zOffset = 0;

            objOffset = new PointF(pobjPointA.X - pobjPointB.X, pobjPointA.Y - pobjPointB.Y);
            objOffset.Y = Math.Abs(objOffset.Y);
            objOffset.X = Math.Abs(objOffset.X);
            zOffset = Math.Abs(psngZA - psngZB);

            sngReturn = (objOffset.X * objOffset.X) + (objOffset.Y * objOffset.Y) + (zOffset * zOffset);

            return sngReturn;
        }

        public static float GetDistanceBetweenPoints(PointF pobjPointA, PointF pobjPointB)
        {
            float sngReturn = 0;
            PointF objOffset = default(PointF);

            objOffset = new PointF(pobjPointA.X - pobjPointB.X, pobjPointA.Y - pobjPointB.Y);
            objOffset.Y = Math.Abs(objOffset.Y);
            objOffset.X = Math.Abs(objOffset.X);

            sngReturn = Convert.ToSingle(Math.Sqrt((objOffset.X * objOffset.X) + (objOffset.Y * objOffset.Y)));

            return sngReturn;
        }

        public static PointF Rotate(PointF pobjPoint, float psngAngle)
        {
            double radian = 0;
            double radianCos = 0;
            double radianSin = 0;
            float newX = 0;
            float newY = 0;

            radian = (psngAngle * (Math.PI / 180));
            radianCos = Math.Cos(radian);
            radianSin = Math.Sin(radian);

            newX = Convert.ToSingle(((pobjPoint.X * radianCos) - (pobjPoint.Y * radianSin)));
            newY = Convert.ToSingle(((pobjPoint.X * radianSin) + (pobjPoint.Y * radianCos)));

            return new PointF(newX, newY);
        }

        public static double GetOppositeSide(float psngAngle, float psngAdjacentLength)
        {
            return Math.Tan(DegreeToRadian(psngAngle)) * psngAdjacentLength;
        }

        public static double GetAdjacentSide(float psngAngle, float psngHypotenuse)
        {
            return Math.Cos(DegreeToRadian(psngAngle)) * psngHypotenuse;
        }

        public static double GetAngle(float psngOpposite, float psngAdjacent)
        {
            return Math.Atan(psngAdjacent / psngOpposite);
        }

        public static PointF GetPointOffsetByAngle(PointF pobjPoint, float psngAngle, float psngDistance)
        {
            PointF objReturn = default(PointF);

            if (float.IsNaN(psngAngle))
            {
                return pobjPoint;
            }

            objReturn = new PointF(Convert.ToSingle(psngDistance * (Math.Sin(TrigHelper.DegreeToRadian(Convert.ToDouble(psngAngle))))), Convert.ToSingle(psngDistance * (Math.Cos(TrigHelper.DegreeToRadian(Convert.ToDouble(psngAngle * -1))))) * -1);

            return GetPointOffset(objReturn, pobjPoint);
        }

        private static PointF GetPointOffset(PointF pobjPoint, PointF pobjOffset)
        {

            return GetPointOffset(pobjPoint, pobjOffset.X, pobjOffset.Y);
        }

        private static PointF GetPointOffset(PointF pobjPoint, float psngX, float psngY)
        {
            PointF objReturn = default(PointF);

            objReturn = new PointF(pobjPoint.X + psngX, pobjPoint.Y + psngY);

            return objReturn;
        }

        public static float GetAngleBetweenPoints(PointF pobjSource, PointF pobjTarget)
        {
            double sngO = (pobjSource.Y - pobjTarget.Y);
            double sngA = (pobjSource.X - pobjTarget.X);
            float sngTan = Convert.ToSingle((sngO / sngA));
            float sngAngle = Convert.ToSingle(TrigHelper.RadianToDegree(Math.Atan(Convert.ToDouble(sngTan))));
            if ((sngA < 0))
            {
                sngAngle = (sngAngle + 90f);
            }
            else
            {
                sngAngle = (sngAngle - 90f);
            }
            return Convert.ToSingle(TrigHelper.Get0to360AngleValue(Convert.ToDouble(sngAngle)));
        }

        public static double RadianToDegree(double pdblRadion)
        {
            return (pdblRadion * 57.2957795130823);
        }

        public static double DegreeToRadian(double pdblDegree)
        {
            return ((3.14159265358979 * pdblDegree) / 180);
        }

        public static double Get0to360AngleValue(double pdblAngle)
        {
            double dblReturnAngle = pdblAngle;
            if ((pdblAngle > 360))
            {
                dblReturnAngle = (pdblAngle - (Convert.ToInt32(Math.Round(Convert.ToDouble((pdblAngle / 360)))) * 360));
            }
            if ((pdblAngle < -360))
            {
                dblReturnAngle = (pdblAngle + (Convert.ToInt32(Math.Round(Convert.ToDouble(((pdblAngle * -1) / 360)))) * 360));
            }
            if ((pdblAngle < 0))
            {
                dblReturnAngle = (360 + pdblAngle);
            }
            return dblReturnAngle;
        }


        public static float GetXAsFractionOfY(PointF pobjPointF)
        {
            float sngXDistanceAsFractionOfY = 0;
            sngXDistanceAsFractionOfY = pobjPointF.X / pobjPointF.Y;

            if (sngXDistanceAsFractionOfY > 1)
            {
                sngXDistanceAsFractionOfY = 1 - (1 / sngXDistanceAsFractionOfY);
            }

            return sngXDistanceAsFractionOfY;
        }

        public static double GetAngleBetweenAngles(double pdblAngle1, double pdblAngle2)
        {
            double dblDifference = 0;

            pdblAngle1 = Get0to360AngleValue(pdblAngle1);
            pdblAngle2 = Get0to360AngleValue(pdblAngle2);

            dblDifference = Math.Abs(pdblAngle1 - pdblAngle2);

            if (dblDifference < 180)
            {
                return dblDifference;
            }
            else
            {
                return 360 - dblDifference;
            }
        }

        public static bool IsPointInSquareBoundedPolygon(List<Point> pcolPoints, float psngX, float psngY)
        {
            float sngMinX = float.MaxValue;
            float sngMaxX = float.MinValue;
            float sngMinY = float.MaxValue;
            float sngMaxY = float.MinValue;

            foreach (Point objPoint in pcolPoints)
            {
                sngMaxX = Math.Max(objPoint.X, sngMaxX);
                sngMinX = Math.Min(objPoint.X, sngMinX);

                sngMaxY = Math.Max(objPoint.Y, sngMaxY);
                sngMinY = Math.Min(objPoint.Y, sngMinY);
            }

            return new RectangleF(sngMinX, sngMinY, sngMaxX - sngMinX, sngMaxY - sngMinY).Contains(new PointF(psngX, psngY));
        }
    }
}