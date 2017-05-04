using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class VectorCurve : VectorEdge
    {
        float osx, osy, oex, oey;

        public float Osx
        {
            get
            {
                return osx;
            }
        }

        public float Osy
        {
            get
            {
                return osy;
            }
        }

        public float Oex
        {
            get
            {
                return oex;
            }
        }

        public float Oey
        {
            get
            {
                return oey;
            }
        }

        protected override void UpdatePoits()
        {
            base.UpdatePoits();

            osx = StartX;
            osy = StartY;
            oex = EndX;
            oey = EndY;

            InvalidatePath();
        }

        public VectorCurve(VectorPath path, float startx, float starty, float endx, float endy) : base(path, startx, starty, endx, endy)
        {
        }

        protected virtual void FillCurvePoints(List<PointF> pts)
        {

        }            

        public override void FillPointList(List<PointF> pts, bool ignoreStart)
        {
            if (!ignoreStart)
                pts.Add(new PointF(StartX, StartY));

            FillCurvePoints(pts);

            pts.Add(new PointF(EndX, EndY));
        }

        public override List<PointF> GetPoints()
        {
            List<PointF> pts = new List<PointF>();
            FillPointList(pts, false);
            return pts;
        }

        public override int CrossPointCount(float hline, List<PointF> crossPoints = null)
        {
            int count = 0;

            List<PointF> pts = GetPoints();

            PointF pt = new PointF();

            bool first = true;

            foreach (PointF p in pts)
            {
                if (first)
                {
                    pt = p;
                    first = false;
                    continue;
                }

                PointF cross = new PointF();
                float x, y;

                if (VectorMath.CrossPoint(hline, pt, p, out x, out y))
                {
                    cross.X = x;
                    cross.Y = y;
                    crossPoints.Add(cross);
                    count++;
                }

                pt = p;
            }

            return count;
        }

    }
}
