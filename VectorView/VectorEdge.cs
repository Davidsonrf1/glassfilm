using System;
using System.Collections.Generic;
using System.Drawing;

namespace VectorView
{
    public class VectorEdge : VectorObject
    {
        VectorPoint start = null;
        VectorPoint end = null;

        VectorShape shape = null;

        Color lineColor = Color.DarkGray;
        float lineWidth = 1.0f;

        public VectorEdge(VectorDocument doc, VectorShape shape) : base(doc)
        {
            this.shape = shape;
        }

        public Color LineColor
        {
            get
            {
                return lineColor;
            }

            set
            {
                lineColor = value;
            }
        }

        public float LineWidth
        {
            get
            {
                return lineWidth;
            }

            set
            {
                lineWidth = value;
            }
        }

        public VectorPoint Start
        {
            get
            {
                return start;
            }

            set
            {
                if (start != null) start.UnlinkEdge(this);
                start = value;
                if (start != null) start.LinkEdge(this);

                Recalculate();
                PointChangeNotify(start);
            }
        }

        public VectorPoint End
        {
            get
            {
                return end;
            }

            set
            {
                if (end != null) end.UnlinkEdge(this);
                end = value;
                if (end != null) end.LinkEdge(this);

                Recalculate();
                PointChangeNotify(end);
            }
        }

        public VectorShape Shape
        {
            get
            {
                return shape;
            }
        }

        public override RectangleF GetBoundBox()
        {
            float minx, miny, maxx, maxy;

            minx = Math.Min(start.X, end.X);
            miny = Math.Min(start.Y, end.Y);

            maxx = Math.Max(start.X, end.X);
            maxy = Math.Max(start.Y, end.Y);

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        internal override void Render()
        {
            Document.DrawLine(start.X, start.Y, end.X, end.Y);

            if (IsSelected || Shape.IsSelected || IsHit)
            {
                Document.DrawPoint(start.X, start.Y);
                Document.DrawPoint(end.X, end.Y);
            }
            else
            {
                if (start.IsSelected)
                    start.Render();

                if (end.IsSelected)
                    end.Render();
            }
        }

        public float PointDistance(float x, float y)
        {
            return VectorMath.PointToLineDistance(x, y, start.X, start.Y, end.X, end.Y);
        }

        public virtual int CrossPointCount(float hline, List<PointF> crossPoints = null)
        {
            float x1, y1, x2, y2;

            x1 = start.X;
            y1 = start.Y;
            x2 = end.X;
            y2 = end.Y;

            if ((y1 < hline && y2 < hline) || (y1 > hline && y2 > hline))
                return 0;

            float dy;

            dy = y2 - y1;

            if (dy == 0)
                return 0;

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

        public virtual void PointChangeNotify(VectorPoint p)
        {
            Recalculate();

            if (Shape != null)
                Shape.EdgeChangeNotify(this);
        }

        public virtual void Recalculate()
        {

        }

        public override void RestoreClone(VectorObject clone)
        {
            VectorEdge e = (VectorEdge)clone;

            Start.X = e.Start.X;
            Start.Y = e.Start.Y;
            End.X = e.End.X;
            End.Y = e.End.Y;
        }

        public override void FillOriginList(List<PointOrigin> ol)
        {
            base.FillOriginList(ol);

            if (start != null)
                ol.Add(start.GetOrigin());

            if (end != null)
                ol.Add(end.GetOrigin());
        }

        protected override bool InternalHitTest(float x, float y)
        {
            float d = PointDistance(x, y);
            return d < Document.HitTolerance;
        }

        public virtual List<PointF> GetPoints()
        {
            List<PointF> pl = new List<PointF>();

            pl.Add(new PointF(Start.X, Start.Y));
            pl.Add(new PointF(End.X, End.Y));

            return pl;
        }
    }
}
