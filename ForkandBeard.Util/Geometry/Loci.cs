using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace ForkandBeard.Util.Geometry
{
    public class Loci
    {
        private RectangleF Rectangle;
        private Line Line;
        private PointF Point;

        private LociType _Type;
        /// <summary>
        /// Default constructor creates an empty Loci.
        /// </summary>
        /// <remarks></remarks>
        public Loci()
        {
            this._Type = LociType.Empty;
        }

        public Loci(PointF pobjPoint)
        {
            this.Point = pobjPoint;
            this._Type = LociType.Point;
        }

        public Loci(RectangleF pobjRectangle)
        {
            this.Rectangle = pobjRectangle;
            this._Type = LociType.Rectangle;
        }

        public Loci(Line pobjLine)
        {
            this.Line = pobjLine;
            this._Type = LociType.Line;
        }

        public LociType Type
        {
            get { return this._Type; }
        }

        public bool IsEmpty
        {
            get
            {
                switch (this.Type)
                {
                    case LociType.Line:
                        return this.Line == null;
                    case LociType.Point:
                        return this.Point.IsEmpty;
                    case LociType.Rectangle:
                        return this.Rectangle.IsEmpty;
                    case LociType.Empty:
                        return true;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public RectangleF ToRectangle()
        {
            switch (this.Type)
            {
                case LociType.Line:
                    return this.Line.ToRectangle();
                case LociType.Point:
                    return new RectangleF(this.Point.X - 0.5f, this.Point.Y - 0.5f, 1, 1);
                case LociType.Rectangle:
                    return this.Rectangle;
                case LociType.Empty:
                    return new RectangleF();
                default:
                    throw new NotSupportedException();
            }
        }

        public object GetRectangleLineOrPoint()
        {
            switch (this.Type)
            {
                case LociType.Line:
                    return this.Line;
                case LociType.Point:
                    return this.Point;
                case LociType.Rectangle:
                    return this.Rectangle;
                default:
                    return null;
            }
        }

        public Loci OffsetToNewCentre(PointF pobjNewCentre)
        {
            switch (this.Type)
            {
                case LociType.Line:
                    PointF objCentre = default(PointF);
                    objCentre = this.GetCentrePoint();
                    return new Loci(this.Line.CreateOffset(new PointF(pobjNewCentre.X - objCentre.X, pobjNewCentre.Y - objCentre.Y)));
                case LociType.Point:
                    return new Loci(pobjNewCentre);
                case LociType.Rectangle:
                    return new Loci(new RectangleF(pobjNewCentre.X - (this.Rectangle.Width / 2), pobjNewCentre.Y - (this.Rectangle.Height / 2), this.Rectangle.Width, this.Rectangle.Height));
            }

            throw new NotSupportedException();
        }

        public PointF GetBottomRightPoint()
        {
            switch (this.Type)
            {
                case LociType.Line:
                    return this.Line.GetBottomRight();
                case LociType.Point:
                    return this.Point;
                case LociType.Rectangle:
                    return GeometryHelper.GetBottomRightOfRectangle(this.Rectangle);
                case LociType.Empty:
                    return new PointF();
                default:
                    throw new NotSupportedException();
            }
        }

        public PointF GetTopLeftPoint()
        {
            switch (this.Type)
            {
                case LociType.Point:
                    return this.Point;
                case LociType.Line:
                    return this.Line.GetTopLeft();
                case LociType.Rectangle:
                    return GeometryHelper.GetTopLeftOfRectangle(this.Rectangle);
                case LociType.Empty:
                    return new PointF();
                default:
                    throw new NotSupportedException();
            }
        }

        public PointF GetCentrePoint()
        {
            switch (this.Type)
            {
                case LociType.Line:
                    return GeometryHelper.GetCentreOfRectangle(this.Line.ToRectangle());
                case LociType.Point:
                    return this.Point;
                case LociType.Rectangle:
                    return GeometryHelper.GetCentreOfRectangle(this.Rectangle);
                case LociType.Empty:
                    return new PointF();
                default:
                    throw new NotSupportedException();
            }
        }

        public void SetCentrePoint(PointF pobjCentrePoint)
        {
            switch (this.Type)
            {
                case LociType.Line:
                    PointF objPresentCentre = default(PointF);
                    objPresentCentre = this.GetCentrePoint();
                    this.Line = new Line(new PointF(this.Line.PointA.X + (pobjCentrePoint.X - objPresentCentre.X), this.Line.PointA.Y + (pobjCentrePoint.Y - objPresentCentre.Y)), new PointF(this.Line.PointB.X + (pobjCentrePoint.X - objPresentCentre.X), this.Line.PointB.Y + (pobjCentrePoint.Y - objPresentCentre.Y)));
                    break;
                case LociType.Point:
                    this.Point = pobjCentrePoint;
                    break;
                case LociType.Rectangle:
                    this.Rectangle = new RectangleF(pobjCentrePoint.X - (this.Rectangle.Width / 2), pobjCentrePoint.Y - (this.Rectangle.Height / 2), this.Rectangle.Width, this.Rectangle.Height);
                    break;
            }
        }

        public bool DoesLocusIntersect(Loci pobjTarget)
        {
            PointF point = new PointF(0, 0);
            switch (this.Type)
            {
                case LociType.Line:
                    switch (pobjTarget.Type)
                    {
                        case LociType.Line:
                            return IntersectionHelpers.DoLinesIntersect(this.Line, pobjTarget.Line, ref point);
                        case LociType.Point:
                            return IntersectionHelpers.DoLinesIntersect(this.Line, pobjTarget.CreateLineFromPoint(), ref point);
                        case LociType.Rectangle:
                            return IntersectionHelpers.DoesLineIntersectRectangle(pobjTarget.Rectangle, this.Line);
                    }
                    break;
                case LociType.Point:
                    switch (pobjTarget.Type)
                    {
                        case LociType.Line:
                            return IntersectionHelpers.DoLinesIntersect(pobjTarget.Line, this.CreateLineFromPoint(), ref point);
                        case LociType.Point:
                            return this.Point.Equals(pobjTarget.Point);
                        case LociType.Rectangle:
                            return pobjTarget.Rectangle.Contains(this.Point);
                    }
                    break;
                case LociType.Rectangle:
                    switch (pobjTarget.Type)
                    {
                        case LociType.Line:
                            return IntersectionHelpers.DoesLineIntersectRectangle(this.Rectangle, pobjTarget.Line);
                        case LociType.Point:
                            return this.Rectangle.Contains(pobjTarget.Point);
                        case LociType.Rectangle:
                            return pobjTarget.Rectangle.IntersectsWith(this.Rectangle);
                    }
                    break;
            }

            throw new NotSupportedException();
        }

        /// <returns>Returns the loci which describes the intersection. WARNING: Where points are concerned it is assumed the point does intersect so the point simply gets returned. This is to prevent multiple calls to DoesLocusIntersect.</returns>
        public Loci GetLocusIntersection(Loci pobjTarget)
        {
            switch (this.Type)
            {
                case LociType.Line:
                    switch (pobjTarget.Type)
                    {
                        case LociType.Line:
                            PointF objReturn = PointF.Empty;
                            IntersectionHelpers.DoLinesIntersect(this.Line, pobjTarget.Line, ref objReturn);
                            return new Loci(objReturn);
                        case LociType.Point:
                            // Need to change this to return New Loci() if not intersecting.
                            return new Loci(pobjTarget.Point);
                        case LociType.Rectangle:
                            Line objReturn2 = null;
                            if (IntersectionHelpers.DoesLineIntersectRectangle(pobjTarget.Rectangle, this.Line, ref objReturn2))
                            {
                                return new Loci(objReturn2);
                            }
                            else
                            {
                                return new Loci();
                            }
                            break;
                    }
                    break;
                case LociType.Point:
                    switch (pobjTarget.Type)
                    {
                        case LociType.Line:
                            return new Loci(this.Point);
                        case LociType.Point:
                            return new Loci(this.Point);
                        case LociType.Rectangle:
                            return new Loci(this.Point);
                    }
                    break;
                case LociType.Rectangle:
                    switch (pobjTarget.Type)
                    {
                        case LociType.Line:
                            Line objReturn = null;
                            if (IntersectionHelpers.DoesLineIntersectRectangle(this.Rectangle, pobjTarget.Line, ref objReturn))
                            {
                                return new Loci(objReturn);
                            }
                            else
                            {
                                return new Loci();
                            }
                            break;
                        case LociType.Point:
                            return new Loci(pobjTarget.Point);
                        case LociType.Rectangle:
                            RectangleF objRectangle = new RectangleF(pobjTarget.Rectangle.Location, pobjTarget.Rectangle.Size);
                            objRectangle.Intersect(this.Rectangle);
                            return new Loci(objRectangle);
                    }
                    break;
            }

            throw new NotSupportedException();
        }

        //TODO: Bit of a HACK which could be replaced because it seems intensive to create line just because we already have a do lines intersect method. See if it creates a bottleneck before optimising.
        private Line CreateLineFromPoint()
        {
            return new Line(new PointF(this.Point.X - 0.1f, this.Point.Y - 0.1f), new PointF(this.Point.X + 0.1f, this.Point.Y + 0.1f));
        }

        public override string ToString()
        {
            return this.GetRectangleLineOrPoint().ToString();
        }

        public string CoordPlotterText
        {
            get
            {
                switch (this.Type)
                {
                    case LociType.Line:
                        return string.Format("{0}, {1}, {2}, {3}", this.Line.PointA.X, this.Line.PointA.Y, this.Line.PointB.X, this.Line.PointB.Y);
                    case LociType.Point:
                        return string.Format("{0}, {1}", this.Point.X, this.Point.Y);
                    case LociType.Rectangle:
                        return string.Format("{0}, {1}, {2}, {3}", this.Rectangle.Location.X, this.Rectangle.Location.Y, GeometryHelper.GetBottomRightOfRectangle(this.Rectangle).X, GeometryHelper.GetBottomRightOfRectangle(this.Rectangle).Y);
                    default:
                        return string.Empty;
                }
            }
        }
    }
}