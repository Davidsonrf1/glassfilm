﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Svg;
using Svg.Transforms;
using Svg.Pathing;
using System.IO;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace VectorView
{
    public enum VectorFitStyle
    {
        None, 
        Vertical,
        Horizontal,
        Both
    }

    public class VectorDocument
    {
        List<VectorPath> paths = new List<VectorPath>();

        Color normalLineColor = Color.Black;
        Color docLimitLineColor = Color.Black;
        Color selectedLineColor = Color.DarkBlue;
        Color docBackcolor = Color.White;

        Color rullerBorderColor = Color.LightGray;
        Color rullerBackColor = Color.WhiteSmoke;


        bool showRuller = false;
        float rullerWidth = 22f;

        string observacao = "";

        float docWidth = 0;
        float docHeight = 0;

        float offsetX = 0;
        float offsetY = 0;

        float scale = 1f;

        float ppi = 96;
        float ppmx = 37.7952728f;  // ppm = Pontos por milímetro
        float ppmy = 37.7952728f;

        bool showDocBorder = false;

        public List<VectorPath> Paths
        {
            get
            {
                return paths;
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
                normalLineColor = value;
            }
        }

        Pen AdjustPen(Pen p)
        {
            p.Width = 2 / scale;

            p.Alignment = PenAlignment.Inset;

            return p;
        }

        Pen normalLinePen = null;
        public Pen NormalLinePen
        {
            get
            {
                if (normalLinePen == null || normalLinePen.Color != normalLineColor)
                    normalLinePen = new Pen(normalLineColor);

                return AdjustPen(normalLinePen);
            }
        }

        Pen selectedLinePen = null;
        public Pen SelectedLinePen
        {
            get
            {
                if (selectedLinePen == null || selectedLinePen.Color != selectedLineColor)
                    selectedLinePen = new Pen(selectedLineColor);

                return AdjustPen(selectedLinePen);
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
                scale = value;
            }
        }

        public bool ShowRuller
        {
            get
            {
                return showRuller;
            }

            set
            {
                showRuller = value; 
            }
        }

        public float Width
        {
            get
            {
                return docWidth;
            }

            set
            {
                docWidth = value; viewBox.Width = docWidth * ppmx;
            }
        }

        public float Height
        {
            get
            {
                return docHeight;
            }

            set
            {
                docHeight = value; viewBox.Height = docHeight * ppmy;
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

        public string Observacao
        {
            get
            {
                return observacao;
            }

            set
            {
                observacao = value;
            }
        }

        public bool ConstraintOK
        {
            get
            {
                return constraintOK;
            }
        }

        public bool AutoCheckConstraints
        {
            get
            {
                return autoCheckConstraints;
            }

            set
            {
                autoCheckConstraints = value;
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

        internal VectorCutSheet CutSheet
        {
            get
            {
                return cutSheet;
            }
        }

        public float GetMinX()
        {
            float min = float.MaxValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.GetBoundRect();
                min = Math.Min(r.X, min);
            }

            return min;
        }

        public float GetMinY()
        {
            float min = float.MaxValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.GetBoundRect();
                min = Math.Min(r.Y, min);
            }

            return min;
        }

        public float GetMaxY()
        {
            float max = float.MinValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.GetBoundRect();
                max = Math.Max(r.Bottom, max);
            }

            return max;
        }

        public float GetMaxX()
        {
            float max = float.MinValue;

            foreach (VectorPath p in paths)
            {
                RectangleF r = p.GetBoundRect();
                max = Math.Max(r.Right, max);
            }

            return max;
        }

        bool autoCheckConstraints = false;                
        bool constraintOK = true;

        public void CheckConstraints()
        {
            float minx = GetMinX();
            float miny = GetMinY();

            constraintOK = true;

            if (minx < 0 || miny < 0)
                constraintOK = false;

            List<VectorPath> pl = new List<VectorPath>();
            foreach (VectorPath vp in paths)
            {
                vp.ResetConstraints();
                pl.Add(vp);
            }

            while(pl.Count > 0)
            {
                VectorPath a = pl[0];
                for (int j = 1; j < pl.Count; j++)
                {
                    VectorPath b = pl[j];

                    if (a.Intersect(b))
                    {
                        pl.Remove(b);

                        a.IsIntersecting = true;
                        b.IsIntersecting = true;

                        constraintOK = false;
                    }
                }

                pl.RemoveAt(0);
            }            
        }

        public VectorPath CreatePath()
        {
            VectorPath p = new VectorPath(this);
            paths.Add(p);
            return p;
        }

        SolidBrush docBackBrush = null;

        void BeginRender(Graphics g)
        {
            g.ResetTransform();

            if (normalLinePen == null)
                normalLinePen = new Pen(normalLineColor, 1);

            g.SmoothingMode = SmoothingMode.HighQuality;

            if (docBackBrush == null || docBackBrush.Color != docBackcolor)
                docBackBrush = new SolidBrush(docBackcolor);
        }

        void DrawRuller(Graphics g)
        {
            Pen rp = new Pen(rullerBorderColor, 0.1f);
            SolidBrush rb = new SolidBrush(rullerBackColor);

            g.FillRectangle(rb, 0, 0, g.ClipBounds.Right, rullerWidth);
            g.FillRectangle(rb, 0, 0, rullerWidth, g.ClipBounds.Bottom);

            g.DrawLine(rp, rullerWidth, rullerWidth, g.ClipBounds.Right, rullerWidth);
            g.DrawLine(rp, rullerWidth, rullerWidth, rullerWidth, g.ClipBounds.Bottom);

            float sx = (offsetX + rullerWidth);
            float sy = (offsetY + rullerWidth);

            float wx = g.ClipBounds.Width / scale;
            float wy = g.ClipBounds.Height / scale;

            g.DrawRectangle(Pens.Orange, sx, sy, g.ClipBounds.Width, g.ClipBounds.Height);
        }

        public void Render(Graphics g)
        {
            if (scale == float.NaN)
                return;

            BeginRender(g);

            float ox = 0, oy = 0;

            if (showRuller)
            {
                ox = rullerWidth;
                oy = rullerWidth;
            }

            ox += offsetX;
            oy += offsetY;

            if (scale <= 0)
                scale = 0.001f;

            g.TranslateTransform(ox, oy);
            g.ScaleTransform(scale, scale);

            if (showDocBorder)
                g.DrawRectangle(Pens.DarkGray, 0, 0, docWidth, docHeight);

            foreach (VectorPath p in paths)
            {
                if (!p.InCutSheet)
                    p.Render(g);
            }

            g.ResetTransform();

            if (showRuller)
            {
                DrawRuller(g);
            }
        }

        public string ToHPGL()
        {
            return ToHPGL(null, null);
        }

        class PathComparerX : IComparer<VectorPath>
        {
            public int Compare(VectorPath x, VectorPath y)
            {
                PointF p1 = x.GetMiddlePoint();
                PointF p2 = y.GetMiddlePoint();

                return p1.X < p2.X ? -1 : p1.X == p2.X ? 0 : 1;                
            }
        }

        void SortPaths()
        {
            paths.Sort(new PathComparerX());
        }

        public string ToHPGL(string sendBefore, string sendAfter)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("IN;\nIP;\nSP1;\nPA;\n");

            if (sendBefore != null)
                sb.Append(sendBefore);

            SortPaths();

            foreach (VectorPath s in paths)
            {
                if (s.InCutSheet)
                    continue;

                sb.Append(s.ToHPGL());
                sb.Append("\n");
            }

            if (sendAfter != null)
                sb.Append(sendAfter);

            sb.Append("PU0,0\nSP;");

            return sb.ToString();
        }

        void ParseSvgElement(SvgElement el)
        {
            VectorPath path = null;

            if (el is SvgPolygon)
            {
                SvgPolygon p = (SvgPolygon)el;
                path = CreatePath();

                int len = p.Points.Count;

                if (len % 2 != 0)
                {
                    len--;
                }

                for (int i = 0; i < len; i+=2)
                {
                    float x, y;

                    x = p.Points[i].Value / ppmx;
                    y = p.Points[i + 1].Value / ppmy;

                    if (i == 0)
                    {
                        path.MoveTo(x, y);
                    }
                    else
                    {
                        path.LineTo(x, y);
                    }
                }

                path.ClosePath();
            }

            if (el is SvgPath)
            {
                SvgPath p = (SvgPath)el;
                path = CreatePath();

                string tag, side;

                tag = "";
                side = "";

                path.Side = VectorPathSide.None;

                if (p.TryGetAttribute("gf-tag", out tag))
                  path.Tag = Encoding.UTF8.GetString(Convert.FromBase64String(tag));

                if (p.TryGetAttribute("gf-side", out side))
                {
                    if (side == "left")
                        path.Side = VectorPathSide.Left;

                    if (side == "right")
                        path.Side = VectorPathSide.Right;
                }

                float sx, sy, ex, ey;                

                foreach (SvgPathSegment seg in p.PathData)
                {
                    sx = (seg.Start.X / ppmx) * loadScale;
                    sy = (seg.Start.Y / ppmy) * loadScale;
                    ex = (seg.End.X / ppmx) * loadScale;
                    ey = (seg.End.Y / ppmy) * loadScale;

                    if (seg is SvgLineSegment)
                    {
                        path.LineTo(ex, ey);
                    }
                    else if (seg is SvgCubicCurveSegment)
                    {
                        SvgCubicCurveSegment q = (SvgCubicCurveSegment)seg;
                        path.CurveTo(ex, ey, (q.FirstControlPoint.X / ppmx) * loadScale, (q.FirstControlPoint.Y / ppmy) * loadScale, (q.SecondControlPoint.X / ppmx) * loadScale, (q.SecondControlPoint.Y / ppmy) * loadScale);
                    }
                    else if (seg is SvgQuadraticCurveSegment)
                    {
                        SvgQuadraticCurveSegment q = (SvgQuadraticCurveSegment)seg;
                        path.QCurveTo(ex, ey, (q.ControlPoint.X / ppmx) * loadScale, (q.ControlPoint.Y / ppmy) * loadScale);
                    }
                    else if (seg is SvgClosePathSegment)
                    {
                        path.ClosePath();
                    }
                    else if (seg is SvgMoveToSegment)
                    {
                        path.MoveTo(ex, ey);
                    }
                    else
                    {

                    }
                }

                path.ComputeArea(true);
            }
            else
            {

            }

            if (path != null)
            {
                path.ComputeMetrics();
                path.ComputeArea(true);
            }

            foreach (SvgElement n in el.Children)
            {
                ParseSvgElement(n);
            }
        }

        public void LoadSVGFromFile(string file, float scale=1)
        {
            string svg = File.ReadAllText(file, Encoding.UTF8);
            LoadSVG(svg, scale);
        }

        public float UnitToMilimeter(float value, SvgUnitType type)
        {
            float mm = 0;

            switch (type)
            {
                case SvgUnitType.User:
                case SvgUnitType.Pixel:
                    float inch = value / ppi;
                    mm = (inch * 2.54f) * 10;
                    break;
                case SvgUnitType.Inch:
                    mm = (value * 2.54f) * 10;
                    break;
                case SvgUnitType.Centimeter:
                    mm = value * 10;
                    break;
                case SvgUnitType.Millimeter:
                    mm = value;
                    break;
                case SvgUnitType.None:
                case SvgUnitType.Em:
                case SvgUnitType.Ex:
                case SvgUnitType.Percentage:
                case SvgUnitType.Pica:
                case SvgUnitType.Point:
                    throw new ArgumentOutOfRangeException("Unidade não suportada: " + type.ToString());
                default:
                    throw new ArgumentOutOfRangeException("Unidade não especificada: " + type.ToString());
            }

            return mm;
        }

        float loadScale = 1;

        public void Clear()
        {
            paths.Clear();
        }
        
        public void LoadSVG(string svg, float scale=1)
        {
            paths.Clear();

            loadScale = scale;

            XmlDocument xdoc = new XmlDocument();
            xdoc.XmlResolver = null;
            xdoc.LoadXml(svg);
            SvgDocument doc = SvgDocument.Open(xdoc);

            ppi = doc.Ppi;

            float vw = UnitToMilimeter(doc.Width.Value, doc.Width.Type);
            float vh = UnitToMilimeter(doc.Height.Value, doc.Height.Type);

            RectangleF vb = new RectangleF(doc.ViewBox.MinX, doc.ViewBox.MinY, doc.ViewBox.Width, doc.ViewBox.Height);

            ppmx = vb.Width / vw;
            ppmy = vb.Height / vh;

            docWidth = vw;
            docHeight = vh;

            foreach (SvgElement e in doc.Children)
            {
                ParseSvgElement(e);
            }

            Normalize();
        }

        public RectangleF GetBoundRect()
        {
            return GetBoundRect(false);
        }

        public RectangleF GetBoundRect(bool selection)
        {
            float minx, miny, maxx, maxy;

            minx = float.MaxValue;
            miny = float.MaxValue;
            maxx = float.MinValue;
            maxy = float.MinValue;

            foreach (VectorPath e in paths)
            {
                if (selection && !e.IsSelected)
                    continue;

                RectangleF r = e.GetBoundRect();

                minx = Math.Min(minx, r.X);
                miny = Math.Min(miny, r.Y);
                maxx = Math.Max(maxx, r.Right);
                maxy = Math.Max(maxy, r.Bottom);
            }

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        public VectorPath GetPathOnPoint(PointF p)
        {
            foreach (VectorPath path in paths)
            {
                if (path.IsPointInside(p))
                    return path;
            }

            return null;
        }

        public string ToSVG()
        {
            Normalize();

            StringBuilder sb = new StringBuilder();

            RectangleF r = Rectangle.Round(GetBoundRect());

            NumberFormatInfo ni = new NumberFormatInfo();
            ni.NumberDecimalSeparator = ".";

            sb.AppendFormat(ni, "<svg xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:cc=\"http://creativecommons.org/ns#\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:svg=\"http://www.w3.org/2000/svg\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {0:0.0000} {1:0.0000}\" width=\"{2:0.0000}mm\" height=\"{3:0.0000}mm\" version=\"1.1\">\n", viewBox.Width, viewBox.Height, docWidth, docHeight);

            foreach (VectorPath s in paths)
            {
                sb.Append("   " + s.ToSVGPath(ppmx, ppmy));
                sb.Append("\n");
            }

            sb.Append("</svg>");
            return sb.ToString();
        }

        public void SaveToSVGFile(string path)
        {
            string svg = ToSVG();
            File.WriteAllText(path, svg);
        }

        public void DrawRect(Graphics g, RectangleF r, Pen pen)
        {
            pen.Width = 1 / scale;
            g.DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
        }

        public void DrawPoint(Graphics g, PointF p, float width = 3)
        {
            float w = width / scale;
            g.FillEllipse(Brushes.OrangeRed, p.X - w / 2, p.Y - w / 2, w, w);
        }

        public void FillRect(Graphics g, RectangleF r, Color c)
        {
            g.FillRectangle(new SolidBrush(c), r.X, r.Y, r.Width, r.Height);
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

            x = dp.X  * scale + offsetX;
            y = dp.Y  * scale + offsetY;

            return new PointF(x, y);
        }

        public RectangleF DocRectToViewRect(RectangleF dr)
        {
            RectangleF r = new RectangleF();

            PointF vp = DocPointToViewPoint(new PointF(dr.X, dr.Y));
            r.X = vp.X;
            r.Y = vp.Y;
            vp = DocPointToViewPoint(new PointF(dr.Right, dr.Bottom));
            r.Width = vp.X - r.X;
            r.Height = vp.Y - r.Y;

            return r;
        }

        public List<VectorPath> GetPathsInsideRect(PointF p1, PointF p2)
        {
            List<VectorPath> ret = new List<VectorPath>();

            foreach (VectorPath p in paths)
            {
                PointF m = p.GetMiddlePoint();

                if ((m.X >= p1.X && m.X <= p2.X) && (m.Y >= p1.Y && m.Y <= p2.Y))
                {
                    ret.Add(p);
                }
            }

            return ret;
        }

        public int CountSoruce(VectorPath source)
        {
            int count = 0;

            foreach (VectorPath p in paths)
            {
                if (p.Source == source)
                    count++;
            }

            return count;
        }

        class CompareArea : IComparer<VectorPath>
        {
            public int Compare(VectorPath x, VectorPath y)
            {
                return x.Area > y.Area ? -1 : x.Area == y.Area ? 0 : 1;
            }
        }

        public void AutoNest()
        {
            List<VectorPath> pl = new List<VectorPath>();

            foreach (VectorPath p in paths)
            {
                pl.Add(p);
            }

            pl.Sort(new CompareArea());
            paths.Clear();

            while (pl.Count > 0)
            {
                VectorPath cur = pl[0];
                RectangleF r = cur.GetBoundRect();

                if (paths.Count == 0)
                {
                    cur.SetOrigin(new PointF(r.Width / 2, r.Height / 2));
                }
                else
                {
                    bool nested = false;
                    foreach (VectorPath vp in Paths)
                    {
                        RectangleF pr = vp.GetBoundRect();

                        cur.SetOrigin(new PointF(pr.X + r.Width / 2 + 1, pr.Bottom + r.Height / 2 + 1));
                        if (cur.CheckContraints())
                        {
                            nested = true;
                            break;
                        }

                        cur.SetOrigin(new PointF((pr.Width / 2 + pr.X) + r.Width / 2 + 1, pr.Bottom + r.Height / 2 + 1));
                        if (cur.CheckContraints())
                        {
                            nested = true;
                            break;
                        }

                        cur.SetOrigin(new PointF(pr.Left + r.Width / 2 + 1, pr.Top + r.Height / 2 + 1));
                        if (cur.CheckContraints())
                        {
                            nested = true;
                            break;
                        }
                    }

                    if (!nested)
                    {
                        float x = GetMaxX();
                        cur.SetOrigin(new PointF(x + r.Width / 2 + 2, r.Height / 2 + 2));
                    }
                }

                paths.Add(cur);
                pl.RemoveAt(0);
            }

            //docWidth = GetMaxX();
        }

        public VectorPath ImportPath(VectorPath p)
        {
            VectorPath d = CreatePath();
            d.Source = p;

            foreach (VectorEdge e in p.Edges)
            {
                VectorEdge c = e.Clone();
                d.AddEdge(c);
            }

            d.Tag = p.Tag;
            d.Side = p.Side;

            d.CopyMetrics(p);
            d.ComputeArea(false);

            return d;
        }

        VectorCutSheet cutSheet = null;

        void SendToCut(VectorPath p)
        {
            if (p.Document != this)
                return;

            VectorPath vp = ImportPath(p);
            vp.Source = p;

            if (cutSheet == null)
                cutSheet = new VectorCutSheet(this);

            cutSheet.AddPath(vp);
        }

        RectangleF viewBox = new RectangleF();

        public void Normalize()
        {
            RectangleF r = GetBoundRect();

            float dx = 0, dy = 0;

            dx = -r.X;
            dy = -r.Y;

            foreach (VectorPath p in paths)
            {
                p.BeginTransform(new PointF(0, 0));
                p.Move(dx, dy);
            }

            r = GetBoundRect();

            if (r.X > 0)
            {
                r.Width += r.X;
                r.X = 0;
            }

            if (r.Y > 0)
            {
                r.Height += r.Y;
                r.Y = 0;
            }

            viewBox = new RectangleF(0, 0, r.Width * ppmx, r.Height * ppmy);
            docWidth = r.Width; 
            docHeight = r.Height;
        }

        public void AutoFit(Rectangle size, VectorFitStyle style, bool center, bool fitContent)
        {
            int margin = 0;

            RectangleF bb;
            if (paths.Count == 0 || !fitContent)
            {
                bb = new RectangleF(0, 0, docWidth, docHeight);
            }
            else
            {
                bb = GetBoundRect();
                bb.Width += bb.X;
                bb.Height += bb.Y;
            }

            bb.Inflate(margin, margin);

            size.Width -= margin;
            size.Height -= margin;

            if (showRuller)
            {
                size.X += (int)rullerWidth + 2;
                size.Y += (int)rullerWidth + 2;

                size.Width -= (int)rullerWidth + 2;
                size.Height -= (int)rullerWidth + 2;
            }

            float s = 1;

            switch (style)
            {
                case VectorFitStyle.None:
                    return;
                case VectorFitStyle.Vertical:
                    s = size.Height / bb.Height;
                    break;
                case VectorFitStyle.Horizontal:
                    s = size.Width / bb.Width;
                    break;
                case VectorFitStyle.Both:

                    float sv = size.Height / bb.Height;
                    float sh = size.Width / bb.Width;

                    s = Math.Min(sv, sh);

                    break;
            }

            if (float.IsInfinity(s))
                return;

            scale = s;

            if (center)
            {
                offsetX = (size.Width - bb.Width * s) / 2 + margin *s / 2;
                offsetY = (size.Height - bb.Height * s) / 2 + margin *s / 2;

                if (showRuller)
                {
                    offsetX += rullerWidth;
                    offsetY += rullerWidth;
                }
            }
        }
    }
}
