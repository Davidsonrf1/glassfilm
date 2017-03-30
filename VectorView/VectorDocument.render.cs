﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{

    internal enum LineType { Normal, SelectionLine, ControlLine }
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
        Pen controlPointPen = new Pen(Color.DarkGoldenrod);
        SolidBrush normalPointBrush = new SolidBrush(Color.Black);
        SolidBrush controlPointBrush = new SolidBrush(Color.Black);

        Color normalLineColor = Color.DarkGray;
        Color shadowLineColor = Color.Red;
        Color hilightLineColor = Color.Blue;
        Color controlPointColor = Color.Brown;
        Color normalPointColor = Color.Black;

        LineJoin lineJoinType = LineJoin.Miter;

        float pointSize = 6f;

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

        public Pen ControlPointPen
        {
            get
            {
                controlPointPen.Width = 1 / doc.Scale;
                controlPointPen.Color = normalLineColor;
                controlPointPen.LineJoin = lineJoinType;

                return controlPointPen;
            }
            
        }

        public Color ControlPointColor
        {
            get
            {
                return controlPointColor;
            }

            set
            {
                controlPointColor = value;
            }
        }

        public SolidBrush NormalPointBrush
        {
            get
            {
                normalPointBrush.Color = normalPointColor;

                return normalPointBrush;
            }
        }

        public Color NormalPointColor
        {
            get
            {
                return normalPointColor;
            }

            set
            {
                normalPointColor = value;
            }
        }

        public float PointSize
        {
            get
            {
                return pointSize;
            }

            set
            {
                pointSize = value;
            }
        }

        public SolidBrush ControlPointBrush
        {
            get
            {
                controlPointBrush.Color = controlPointColor;
                return controlPointBrush;
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
                return graphics;
            }

            set
            {
                graphics = value;
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
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            graphics.ResetTransform();

            graphics.ScaleTransform(scale, scale);
            graphics.TranslateTransform(offsetX, offsetY);

            foreach (VectorShape s in shapes)
            {
                s.Render();
            }

            needRedraw = false;
        }

        public void RenderDocument(Graphics g)
        {
            graphics = g;

            Render();
            RenderTools(g);
        }

        Graphics graphics = null;

        internal void DrawLine(float x1, float y1, float x2, float y2)
        {
            if (graphics == null)
                return;

            graphics.DrawLine(renderParams.HilightLinePen, x1, y1, x2, y2);
        }

        internal void DrawControlLine(float x1, float y1, float x2, float y2)
        {
            if (graphics == null)
                return;

            graphics.DrawLine(renderParams.ControlPointPen, x1, y1, x2, y2);
        }

        internal void DrawPoint(float x, float y)
        {
            if (graphics == null)
                return;

            float size = renderParams.PointSize * (1 / Scale);

            graphics.FillRectangle(renderParams.NormalPointBrush, x - size / 2, y - size / 2, size, size);
        }

        internal void DrawControlPoint(float x, float y)
        {
            if (graphics == null)
                return;

            float size = renderParams.PointSize * (1 / Scale);

            graphics.FillRectangle(renderParams.ControlPointBrush, x - size / 2, y - size / 2, size, size);
        }
    }
}
