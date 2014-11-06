using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace ForkandBeard.Util.Geometry
{
    public class Line
    {
        public float X1;
        public float X2;
        public float Y1;
        public float Y2;
        public PointF PointA;
        public PointF PointB;
        private PointF _PointLeft;
        private PointF _PointRight;
        private PointF _PointTop;
        private PointF _PointBottom;
        private float _Angle = -1;

        private float _Length = -1;
        public PointF PointLeft
        {
            get
            {
                if (this._PointLeft.IsEmpty)
                {
                    if (this.PointA.X < this.PointB.X)
                    {
                        this._PointLeft = this.PointA;
                    }
                    else
                    {
                        this._PointLeft = this.PointB;
                    }
                }

                return this._PointLeft;
            }
        }

        public PointF PointRight
        {
            get
            {
                if (this._PointRight.IsEmpty)
                {
                    if (this.PointA.X > this.PointB.X)
                    {
                        this._PointRight = this.PointA;
                    }
                    else
                    {
                        this._PointRight = this.PointB;
                    }
                }

                return this._PointRight;
            }
        }

        public PointF PointTop
        {
            get
            {
                if (this._PointTop.IsEmpty)
                {
                    if (this.PointA.Y < this.PointB.Y)
                    {
                        this._PointTop = this.PointA;
                    }
                    else
                    {
                        this._PointTop = this.PointB;
                    }
                }

                return this._PointTop;
            }
        }

        public PointF PointBottom
        {
            get
            {
                if (this._PointBottom.IsEmpty)
                {
                    if (this.PointA.Y > this.PointB.Y)
                    {
                        this._PointBottom = this.PointA;
                    }
                    else
                    {
                        this._PointBottom = this.PointB;
                    }
                }

                return this._PointBottom;
            }
        }

        public Point IntPointA
        {
            get { return Point.Truncate(this.PointA); }
            set { this.PointA = value; }
        }

        public Point IntPointB
        {
            get { return Point.Truncate(this.PointB); }
            set { this.PointB = value; }
        }

        public float Angle
        {
            get
            {
                if (this._Angle == -1)
                {
                    this._Angle = TrigHelper.GetAngleBetweenPoints(this.PointA, this.PointB);
                }
                return this._Angle;
            }
        }

        public float Length
        {
            get
            {
                if (this._Length == -1)
                {
                    this._Length = TrigHelper.GetDistanceBetweenPoints(this.PointA, this.PointB);
                }
                return this._Length;
            }
        }

        public Line(PointF pobjPointA, PointF pobjPointB)
        {
            this.X1 = pobjPointA.X;
            this.X2 = pobjPointB.X;
            this.Y1 = pobjPointA.Y;
            this.Y2 = pobjPointB.Y;
            this.PointA = pobjPointA;
            this.PointB = pobjPointB;
        }

        public Line()
        {
        }

        public Line CreateOffset(PointF pobjOffset)
        {
            return new Line(new PointF(this.PointA.X + pobjOffset.X, this.PointA.Y + pobjOffset.Y), new PointF(this.PointB.X + pobjOffset.X, this.PointB.Y + pobjOffset.Y));
        }

        public float GetTop()
        {
            return Math.Min(this.Y1, this.Y2);
        }

        public float GetLeft()
        {
            return Math.Min(this.X1, this.X2);
        }

        public float GetRight()
        {
            return Math.Max(this.X2, this.X1);
        }

        public float GetBottom()
        {
            return Math.Max(this.Y2, this.Y1);
        }

        public RectangleF ToRectangle()
        {
            return new RectangleF(this.GetLeft(), this.GetTop(), this.GetRight() - this.GetLeft(), this.GetBottom() - this.GetTop());
        }

        public PointF GetTopLeft()
        {
            return new PointF(this.GetLeft(), this.GetTop());
        }

        public PointF GetBottomRight()
        {
            return new PointF(this.GetRight(), this.GetBottom());
        }

        public override string ToString()
        {
            return String.Format("Line PointA[{0}] PointB[{1}]", this.PointA.ToString(), this.PointB.ToString());
        }
    }
}