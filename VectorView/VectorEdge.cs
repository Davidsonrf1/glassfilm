using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public class VectorEdge : VectorObject
    {
        VectorPoint start = null;
        VectorPoint end = null;

        Color lineColor = Color.DarkGray;
        float lineWidth = 1.0f;

        public VectorEdge(VectorDocument doc) : base(doc)
        {
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
                start = value;
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
                end = value;
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

        internal override void Render(Graphics g)
        {
            Pen p = new Pen(lineColor, lineWidth * (1 / Document.Scale));

            p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

            if (this == Document.MouseHitEdge)
            {
                p.Color = Color.Black;
                p.Width *= 2;
            }

            g.DrawLine(p, start.X, start.Y, end.X, end.Y);
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
    }
}
