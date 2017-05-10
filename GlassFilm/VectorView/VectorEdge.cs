using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public enum VectorEdgeType {  None, Move, Line, Curve, QCurve, Close }

    public class VectorEdge
    {
        VectorPath path = null;
        float sx = 0, sy = 0;
        float ex = 0, ey = 0;

        VectorEdgeType type = VectorEdgeType.Line;

        public float StartX
        {
            get
            {
                return sx;
            }

            set
            {
                sx = value; UpdatePoits();
            }
        }

        public float StartY
        {
            get
            {
                return sy;
            }

            set
            {
                sy = value; UpdatePoits();
            }
        }

        public float EndX
        {
            get
            {
                return ex;
            }

            set
            {
                ex = value; UpdatePoits();
            }
        }

        public float EndY
        {
            get
            {
                return ey;
            }

            set
            {
                ey = value; UpdatePoits(); 
            }
        }

        public VectorPath Path
        {
            get
            {
                return path;
            }
            
            internal set
            {
                path = value;
            }
        }

        public VectorEdgeType Type
        {
            get
            {
                return type;
            }

            protected set
            {
                type = value;
            }
        }

        public void InvalidatePath()
        {
            if (path != null)
            {
                path.InvalidatePath();
            }
        }

        protected virtual void UpdatePoits()
        {
            InvalidatePath();
        }

        public float PointDistance(float x, float y)
        {
            return VectorMath.PointToLineDistance(x, y, sx, sy, ex, ey);
        }

        internal virtual VectorEdge Clone()
        {
            return new VectorEdge(path, sx, sy, ex, ey);
        }

        public virtual int CrossPointCount(float hline, List<PointF> crossPoints = null)
        {
            PointF p = new PointF();
            float x, y;

            if (VectorMath.CrossPoint(hline, new PointF(sx, sy), new PointF(ex, ey), out x, out y))
            {

                p.X = x;
                p.Y = y;

                if (crossPoints != null)
                {
                    crossPoints.Add(p);
                }

                return 1;
            }
            
            return 0;
        }

        internal VectorEdge(VectorPath path, float startx, float starty, float endx, float endy)
        {
            sx = startx;
            sy = starty;
            ex = endx;
            ey = endy;

            if (ex == 0)
            {

            }

            this.path = path;
        }

        public virtual void FillPointList(List<PointF> pts, bool ignoreStart)
        {
            if (!ignoreStart)
                pts.Add(new PointF(sx, sy));

            pts.Add(new PointF(ex, ey));
        }

        public virtual void Move(float distanceX, float distanceY)
        {
            sx += distanceX;
            sy += distanceY;
            ex += distanceX;
            ey += distanceY;

            InvalidatePath();
        }

        public virtual List<PointF> GetPoints()
        {
            List<PointF> points = new List<PointF>();

            points.Add(new PointF(sx, sy));
            points.Add(new PointF(ex, ey));

            return points;
        }
        
        public virtual void CopyPoints(List<PointF> pl)
        {
            pl.Add(new PointF(sx, sy));
            pl.Add(new PointF(ex, ey));
        }

        public virtual void SetPoints(List<PointF> pl)
        {
            StartX = pl[0].X;
            StartY = pl[0].Y;

            EndX = pl[1].X;
            EndY = pl[1].Y;

            InvalidatePath();
        }

        public RectangleF GetBoundRect()
        {
            float minx, miny, maxx, maxy;

            minx = float.MaxValue;
            miny = float.MaxValue;
            maxx = float.MinValue;
            maxy = float.MinValue;

            List<PointF> pts = GetPoints();

            foreach (PointF p in pts)
            {
                minx = Math.Min(minx, p.X);
                miny = Math.Min(miny, p.Y);
                maxx = Math.Max(maxx, p.X);
                maxy = Math.Max(maxy, p.Y);
            }

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        public override string ToString()
        {
            return string.Format("{4} SX: {0}, SY{1}, EX: {2}, EY: {3}", sx, sy, ex, ey, GetType().ToString());
        }
    }
}
