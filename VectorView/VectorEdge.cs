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
            Pen p = new Pen(lineColor, lineWidth);
            g.DrawLine(p, start.X, start.Y, end.X, end.Y);
        }
    }
}
