using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public partial class VectorDocument: VectorObject
    {
        List<VectorShape> shapes = new List<VectorShape>();

        float scale = 1;
        float offsetX = 0;
        float offsetY = 0;

        float width=80, height=120;
        public float Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
        public float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value > 0.01f ? value : 0.01f;
            }
        }

        public float OffsetX
        {
            get
            {
                return offsetX;
            }

            set
            {
                offsetX = value;
            }
        }

        public float OffsetY
        {
            get
            {
                return offsetY;
            }

            set
            {
                offsetY = value;
            }
        }

        public override RectangleF GetBoundBox()
        {
            return new RectangleF(0, 0, width, height);
        }

        public VectorShape CreateShape()
        {
            VectorShape s = new VectorShape(this);

            shapes.Add(s);

            return s;
        }

        internal override void Render(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.ResetTransform();

            g.ScaleTransform(scale, scale);
            g.TranslateTransform(offsetX, offsetY);

            foreach (VectorShape s in shapes)
            {
                s.Render(g);
            }
        }

        public void RenderDocument(Graphics g)
        {
            Render(g);

            g.ResetTransform();
            RenderTools(g);
        }
    }
}
