using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class VectorEdge
    {
        VectorPath path = null;
        float sx = 0, sy = 0;
        float ex = 0, ey = 0;

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
            
        }

        protected virtual void UpdatePoits()
        {

        }

        public float PointDistance(float x, float y)
        {
            return VectorMath.PointToLineDistance(x, y, sx, sy, ex, ey);
        }

        public virtual int CrossPointCount(float hline, List<PointF> crossPoints = null)
        {
            float x1, y1, x2, y2;

            x1 = sx;
            y1 = sy;
            x2 = ex;
            y2 = ey;

            if ((y1 < hline && y2 < hline) || (y1 > hline && y2 > hline))
                return 0;

            float dy;

            dy = y2 - y1;

            if (dy == 0)
            {
                return 0;
            }

            if (crossPoints != null)
            {
                crossPoints.Clear();

                PointF p = new PointF();
                p.Y = hline;
                p.X = (hline - y1) * ((x2 - x1) / dy) + x1;

                crossPoints.Add(p);
            }

            return 1;
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

        internal virtual void Render(Graphics g)
        {
            g.DrawLine(path.LinePen, StartX, StartY, EndX, EndY);
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
