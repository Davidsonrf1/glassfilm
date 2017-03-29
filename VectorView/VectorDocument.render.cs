using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    internal class RenderParams
    {
        VectorDocument doc = null;
        internal RenderParams(VectorDocument doc)
        {
            this.doc = doc;
        }

        Pen normalLinePen = new Pen(Color.DarkGray);
        Pen shadowLinePen = new Pen(Color.Red);
        Pen hilightLinePen = new Pen(Color.LightSalmon);

        Color normalLineColor = Color.DarkGray;
        Color shadowLineColor = Color.Red;
        Color hilightLineColor = Color.Blue;

        LineJoin lineJoinType = LineJoin.Miter;

        public Color NormalLineColor
        {
            get
            {
                return normalLineColor;
            }

            set
            {
                normalLineColor = value;
            }
        }

        public Color ShadowLineColor
        {
            get
            {
                return shadowLineColor;
            }

            set
            {
                shadowLineColor = value;
            }
        }

        public Color HilightLineColor
        {
            get
            {
                return hilightLineColor;
            }

            set
            {
                hilightLineColor = value;
            }
        }

        public Pen HilightLinePen
        {
            get
            {
                hilightLinePen.Width = 1 / doc.Scale;
                hilightLinePen.Color = hilightLineColor;
                hilightLinePen.LineJoin = lineJoinType;

                return hilightLinePen;
            }
        }

        public Pen ShadowLinePen
        {
            get
            {
                shadowLinePen.Width = 1 / doc.Scale;
                shadowLinePen.Color = shadowLineColor;
                shadowLinePen.LineJoin = lineJoinType;

                return shadowLinePen;
            }
        }

        public Pen NormalLinePen
        {
            get
            {
                normalLinePen.Width = 1 / doc.Scale;
                normalLinePen.Color = normalLineColor;
                normalLinePen.LineJoin = lineJoinType;

                return normalLinePen;
            }
        }

        public LineJoin LineJoinType
        {
            get
            {
                return lineJoinType;
            }
        }
    }

    public partial class VectorDocument: VectorObject
    {
        List<VectorShape> shapes = new List<VectorShape>();

        RenderParams renderParams = null;


        float scale = 1;
        float offsetX = 0;
        float offsetY = 0;

        bool needRedraw = true;

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

        public bool NeedRedraw
        {
            get
            {
                return needRedraw;
            }
        }

        internal RenderParams RenderParams
        {
            get
            {
                return renderParams;
            }
        }

        public Graphics Graphics
        {
            get
            {
                return curGraphic;
            }

            set
            {
                curGraphic = value;
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

        internal override void Render()
        {
            curGraphic.SmoothingMode = SmoothingMode.HighQuality;
            curGraphic.InterpolationMode = InterpolationMode.HighQualityBilinear;
            curGraphic.TextRenderingHint = TextRenderingHint.AntiAlias;

            curGraphic.ResetTransform();

            curGraphic.ScaleTransform(scale, scale);
            curGraphic.TranslateTransform(offsetX, offsetY);

            foreach (VectorShape s in shapes)
            {
                s.Render();
            }

            if (mouseHitShape != null)
            {
                curGraphic.DrawString("Dentro", new Font("Arial", 10), Brushes.Red, new Point(50, 50));
            }

            if (mouseHitPoint!= null)
            {
                curGraphic.FillRectangle(Brushes.Black, mouseHitPoint.X - 3, mouseHitPoint.Y - 3, 6, 6);
            }

            if (selection.Count > 0)
            {
                CalculateBoudingBox();

                curGraphic.DrawRectangle(Pens.AliceBlue, selectionBoundingBox.X, selectionBoundingBox.Y, selectionBoundingBox.Width, selectionBoundingBox.Height);
            }

            needRedraw = false;
        }

        public void RenderDocument(Graphics g)
        {
            curGraphic = g;
            Render();

            g.ResetTransform();
            RenderTools(g);
            DrawDebugPoints(g);
        }

        Graphics curGraphic = null;

        internal void DrawLine(float x1, float y1, float x2, float y2, bool hilight)
        {
            if (curGraphic == null)
                return;

            Pen p = renderParams.NormalLinePen;

            p = renderParams.HilightLinePen;           

            curGraphic.DrawLine(p, x1, y1, x2, y2);
        }
    }
}
