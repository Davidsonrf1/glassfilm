using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace VectorView
{
    public partial class VectorDocument
    {
        Color normalLineColor   = Color.Black;
        Color selectedLineColor = Color.Blue;
        Color hilightLineColor  = Color.DarkBlue;
        Color docBackcolor      = Color.White;
        Color includedLineColor = Color.Black;
        Color errorLineColor    = Color.Red;

        bool showDocBorder = false;
        bool showDocInfo = false;

        float lineWidth = 2;

        internal void DrawCircle(Graphics g, PointF center, float radius, Color c)
        {
            using (SolidBrush sb = new SolidBrush(c))
            {
                g.FillEllipse(sb, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            }
        }

        internal Pen normalLine = null;
        internal Pen selectedLine = null;
        internal Pen hilightLine = null;
        internal Pen errorLine = null;

        public float LineWidth
        {
            get
            {
                return lineWidth;
            }

            set
            {
                lineWidth = value; Host.Refresh();
            }
        }

        public Color NormalLineColor
        {
            get
            {
                return normalLineColor;
            }

            set
            {
                normalLineColor = value; Host.Refresh();
            }
        }

        public Color SelectedLineColor
        {
            get
            {
                return selectedLineColor;
            }

            set
            {
                selectedLineColor = value; Host.Refresh();
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
                hilightLineColor = value; Host.Refresh();
            }
        }

        public Color DocBackcolor
        {
            get
            {
                return docBackcolor;
            }

            set
            {
                docBackcolor = value; Host.Refresh();
            }
        }

        public Color IncludedLineColor
        {
            get
            {
                return includedLineColor;
            }

            set
            {
                includedLineColor = value; Host.Refresh();
            }
        }

        public Color ErrorLineColor
        {
            get
            {
                return errorLineColor;
            }

            set
            {
                errorLineColor = value; Host.Refresh();
            }
        }

        public bool ShowDocBorder
        {
            get
            {
                return showDocBorder;
            }

            set
            {
                showDocBorder = value;
            }
        }

        public bool ShowDocInfo
        {
            get
            {
                return showDocInfo;
            }

            set
            {
                showDocInfo = value;
            }
        }

        public bool DrawSelBox
        {
            get
            {
                return drawSelBox;
            }

            set
            {
                drawSelBox = value;
            }
        }

        void CheckPen(ref Pen p, Color c)
        {
            if (p != null && p.Color != c)
                p = null;

            p = new Pen(c);

            p.Width = lineWidth / scale;
            p.Alignment = PenAlignment.Inset;
            p.LineJoin = LineJoin.Round;
        }

        SolidBrush includedLine = null;
        Pen docBorderPen = null;
        Color docBorderColor = Color.Red;
        Font infFont = null;

        bool drawSelBox = true;

        public void Render(Graphics g)
        {
            if (includedLine == null)
                includedLine = new SolidBrush(includedLineColor);

            CheckPen(ref normalLine, normalLineColor);
            CheckPen(ref selectedLine, selectedLineColor);
            CheckPen(ref hilightLine, hilightLineColor);
            CheckPen(ref errorLine, errorLineColor);

            hilightLine.Width *= 1.5f;

            if (scale < 0.04f)
            {
                scale = 0.04f;
            }

            foreach (VectorPath p in paths)
            {
                p.Render(g);
            }

            if (selection.Count > 0 && Host.AllowTransforms && drawSelBox)
            {
                RenderSelBox(g);
            }

            if (showDocBorder)
            {
                g.ResetTransform();

                g.TranslateTransform(offsetX, offsetY);
                g.ScaleTransform(scale, scale);

                float right = GetMaxX();

                if (docBorderPen == null)
                {
                    docBorderPen = new Pen(docBorderColor, 0.02f);
                }
                else if (docBorderPen.Color != docBorderColor)
                {
                    docBorderPen = new Pen(docBorderColor, 0.02f);
                }

                RectangleF docBox = new RectangleF();

                docBox.X = 0;
                docBox.Y = 0;
                docBox.Width = right;
                docBox.Height = docHeight;

                g.DrawRectangle(docBorderPen, 0, 0, docBox.Width, docHeight);

                if (showDocInfo)
                {
                    RectangleF r = new RectangleF(docBox.X, docBox.Bottom, docBox.Width, 48);

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    if (infFont == null)
                    {
                        infFont = new Font("Arial", 24, FontStyle.Bold);
                    }

                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                    string str = "PLANO DE CORTE";

                    SizeF size = g.MeasureString(str, infFont);
                    g.DrawString(str, infFont, Brushes.DarkGray, r, sf);

                    r.Y = -r.Height;
                    str = "C O M P R I M E N T O";

                    size = g.MeasureString(str, infFont);
                    g.DrawString(str, infFont, Brushes.Black, r, sf);

                    r.Width = docHeight;

                    g.TranslateTransform(0, docHeight);
                    g.RotateTransform(-90);

                    str = "L A R G U R A";
                    size = g.MeasureString(str, infFont);
                    g.DrawString(str, infFont, Brushes.Black, r, sf);
                }
            }

            if (debbuging)
            {
                g.ResetTransform();
                
                g.TranslateTransform(offsetX, offsetY);
                g.ScaleTransform(scale, scale);
                g.DrawLine(Pens.Red, 0, docHeight, 1000, docHeight);

                DrawCircle(g, cursorPos, 3 / scale, Color.Orange);

                DebugRender(g);                
            }           
        }

        public PointF ViewPointToDocPoint(PointF vp)
        {
            float x, y;

            x = (vp.X - offsetX) / scale;
            y = (vp.Y - offsetY) / scale;

            return new PointF(x, y);
        }

        public PointF DocPointToViewPoint(PointF dp)
        {
            float x, y;

            x = dp.X * scale + offsetX;
            y = dp.Y * scale + offsetY;

            return new PointF(x, y);
        }

        public float GetMinY()
        {
            float min = float.MaxValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.Limits;

                r.X += p.X;
                r.Y += p.Y;

                min = Math.Min(r.Y, min);
            }

            return min;
        }

        public float GetMaxY()
        {
            float max = float.MinValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.Limits;

                r.X += p.X;
                r.Y += p.Y;

                max = Math.Max(r.Bottom, max);
            }

            return max;
        }

        public float GetMaxX()
        {
            float max = float.MinValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.Limits;

                r.X += p.X;
                r.Y += p.Y;

                max = Math.Max(r.Right, max);
            }

            return max;
        }

        public float GetMinX()
        {
            float min = float.MaxValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.Limits;

                r.X += p.X;
                r.Y += p.Y;

                min = Math.Min(r.X, min);
            }

            return min;
        }
    }
}
