using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class VectorSegment
    {
        PointF[] points = null;
        PointF start, end;

        public PointF End
        {
            get
            {
                return end;
            }

            set
            {
                float dx, dy;

                dx = start.X - value.X;
                dy = start.Y - value.Y;

                end = value;

                UpdateEnd(dx, dy);
            }
        }

        public PointF Start
        {
            get
            {
                return start;
            }

            set
            {
                float dx, dy;

                dx = start.X - value.X;
                dy = start.Y - value.Y;

                start = value;

                UpdateStart(dx, dy);
            }
        }

        protected virtual void FillPoints(List<PointF> pts)
        {

        }

        public PointF[] Points
        {
            get
            {
                if (points == null)
                {
                    List<PointF> pts = new List<PointF>();

                    pts.Add(start);
                    FillPoints(pts);
                    pts.Add(end);

                    points = pts.ToArray();
                }                

                return points;
            }
        }

        public void ClearPoints()
        {
            points = null;
        }

        public virtual void GetMinMax(out float minx, out float miny, out float maxx, out float maxy)
        {
            minx = float.MaxValue;
            miny = float.MaxValue;

            minx = Math.Min(minx, start.X);
            miny = Math.Min(miny, start.Y);

            minx = Math.Min(minx, end.X);
            miny = Math.Min(miny, end.Y);

            maxx = float.MinValue;
            maxy = float.MinValue;

            maxx = Math.Max(maxx, start.X);
            maxy = Math.Max(maxy, start.Y);

            maxx = Math.Max(maxx, end.X);
            maxy = Math.Max(maxy, end.Y);
        }

        public virtual void UpdateStart(float dx, float dy)
        {

        }

        public virtual void UpdateEnd(float dx, float dy)
        {

        }

        public virtual void Move(float dx, float dy)
        {
            start.X += dx;
            end.X += dx;
            start.Y += dy;
            end.Y += dy;

            ClearPoints();
        }

        public virtual void Rotate(float angle)
        {

        }
    }
}
