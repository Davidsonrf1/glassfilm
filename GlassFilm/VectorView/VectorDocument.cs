using System;
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
using VectorView.Plotter;

namespace VectorView
{
    public enum VectorFitStyle
    {
        None, 
        Vertical,
        Horizontal,
        Both
    }

    public enum VectorFitRegion
    {
        None, 
        Document,
        Content,
        CutBox
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

        bool showConvexHull = false;

        string observacao = "";

        float docWidth = 0;
        float docHeight = 0;

        float offsetX = 0;
        float offsetY = 0;

        float scale = 1f;

        float ppi = 96;
        float ppmx = 37.7952728f;  // ppm = Pontos por milímetro
        float ppmy = 37.7952728f;

        float cutSize = 1520;
        bool drawCutBox = false;
        RectangleF cutBox = new RectangleF();

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

        public float CutSize
        {
            get
            {
                return cutSize;
            }

            set
            {
                cutSize = value; UpdateCutBox();
            }
        }

        public bool DrawCutBox
        {
            get
            {
                return drawCutBox;
            }

            set
            {
                drawCutBox = value;
            }
        }

        public RectangleF CutBox
        {
            get
            {
                return cutBox;
            }
        }

        public bool ShowConvexHull
        {
            get
            {
                return showConvexHull;
            }

            set
            {
                showConvexHull = value;
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

        public void UpdateCutBox()
        {
            RectangleF r = GetBoundRect();

            cutBox.X = 0;
            cutBox.Y = 0;
            cutBox.Width = r.Right;
            cutBox.Height = cutSize;
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
                vp.ResetContraints();
                vp.CheckBoundConstraints();
                pl.Add(vp);
            }
            int time = Environment.TickCount;

            while (pl.Count > 0)
            {
                VectorPath a = pl[0];
                List<VectorPath> toRemove = new List<VectorPath>(pl.Count);
                toRemove.Add(a);

                for (int j = 1; j < pl.Count; j++)
                {                    
                    if (!a.CheckIntersectionContraints(pl[j]))
                    {
                        constraintOK = false;
                        toRemove.Add(pl[j]);
                    }
                }

                foreach (VectorPath tr in toRemove)
                {
                    pl.Remove(tr);
                }

                toRemove.Clear();                
            }

            time = Environment.TickCount - time;
            if (System.Windows.Forms.Form.ActiveForm != null)
                System.Windows.Forms.Form.ActiveForm.Text = time.ToString();
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

        Pen cutPen = null;
        Font cutFont = null;

        public void Render(Graphics g)
        {
            if (scale == float.NaN)
                return;

            BeginRender(g);

            float ox = 0, oy = 0;

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
                p.Render(g);
            }

            if (drawCutBox)
            {
                if (cutPen == null)
                {
                    cutPen = new Pen(Color.Red, 1 * Scale);
                    cutPen.Alignment = PenAlignment.Outset;
                }

                g.DrawRectangle(cutPen, cutBox.X, cutBox.Y, cutBox.Width, cutBox.Height);

                RectangleF r = new RectangleF(cutBox.X, cutBox.Bottom, cutBox.Width, 48);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                if (cutFont == null)
                {
                    cutFont = new Font("Arial", 18, FontStyle.Bold);
                }

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                string str = "PLANO DE CORTE";

                SizeF size = g.MeasureString(str, cutFont);
                g.DrawString(str, cutFont, Brushes.DarkGray, r, sf);

                r.Y = -r.Height;
                str = "C O M P R I M E N T O";

                size = g.MeasureString(str, cutFont);
                g.DrawString(str, cutFont, Brushes.Black, r, sf);

                r.Width = cutBox.Height;

                g.TranslateTransform(0, cutBox.Height);
                g.RotateTransform(-90);

                str = "L A R G U R A";
                size = g.MeasureString(str, cutFont);
                g.DrawString(str, cutFont, Brushes.Black, r, sf);

            }

            g.ResetTransform();
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

        public string GeneratePlotterCommands(PlotterDriver driver)
        {
            return GeneratePlotterCommands(null, null, driver);
        }

        public string GeneratePlotterCommands(string sendBefore, string sendAfter, PlotterDriver driver)
        {
            SortPaths();

            driver.ClearPolygons();

            foreach (VectorPath p in paths)
            {
                p.GenerateCommands(driver);
            }

            return driver.GetCmdString(sendBefore, sendAfter);
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

                path.ClosePolygon();
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
                        path.ClosePolygon();
                    }
                    else if (seg is SvgMoveToSegment)
                    {
                        path.MoveTo(ex, ey);
                    }
                    else
                    {

                    }
                }
            }
            else
            {

            }

            if (path != null)
            {
                path.ClosePath();

                path.ComputeMetrics();
                path.ComputeArea(true);
            }

            foreach (SvgElement n in el.Children)
            {
                ParseSvgElement(n);
            }
        }


        public void LoadSVGFromFile(string file)
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

        public void FillPossibleLocations(List<PointF> points, VectorPath src, VectorPath dst)
        {
            RectangleF sr = src.GetBoundRect();
            RectangleF dr = dst.GetBoundRect();

            float hx = sr.Width / 2 + 1;
            float hy = sr.Height / 2 + 1;

            points.Add(new PointF(dr.Left - hx, dr.Top - hy));
            points.Add(new PointF(dr.Left, dr.Top - hy));
            points.Add(new PointF(dr.Left + hx, dr.Top - hy));

            points.Add(new PointF(dr.Left + dr.Width / 2 - hx, dr.Top - hy));
            points.Add(new PointF(dr.Left + dr.Width / 2, dr.Top - hy));
            points.Add(new PointF(dr.Left + dr.Width / 2 + hx, dr.Top - hy));

            points.Add(new PointF(dr.Right - hx, dr.Top - hy));
            points.Add(new PointF(dr.Right, dr.Top - hy));
            points.Add(new PointF(dr.Right + dr.Width / 2 + hx, dr.Top - hy));

            points.Add(new PointF(dr.Right + hx, dr.Top));
            points.Add(new PointF(dr.Right + hx, dr.Top + hy));

            points.Add(new PointF(dr.Right + hx, dr.Top + dr.Height / 2 - hy));
            points.Add(new PointF(dr.Right + hx, dr.Top + dr.Height / 2));
            points.Add(new PointF(dr.Right + hx, dr.Top + dr.Height / 2 + hy));

            points.Add(new PointF(dr.Right + hx, dr.Bottom - hy));
            points.Add(new PointF(dr.Right + hx, dr.Bottom));
            points.Add(new PointF(dr.Right + hx, dr.Bottom + hy));

            points.Add(new PointF(dr.Right, dr.Bottom + hy));
            points.Add(new PointF(dr.Right - hx, dr.Bottom + hy));

            points.Add(new PointF(dr.Right - dr.Width / 2 + hx, dr.Bottom + hy));
            points.Add(new PointF(dr.Right - dr.Width / 2, dr.Bottom + hy));
            points.Add(new PointF(dr.Right - dr.Width / 2 - hx, dr.Bottom + hy));

            points.Add(new PointF(dr.Left + hx, dr.Bottom + hy));
            points.Add(new PointF(dr.Left, dr.Bottom + hy));
            points.Add(new PointF(dr.Left - hx, dr.Bottom + hy));

            points.Add(new PointF(dr.Left - hx, dr.Top + dr.Height / 2 - hy));
            points.Add(new PointF(dr.Left - hx, dr.Top + dr.Height / 2));
            points.Add(new PointF(dr.Left - hx, dr.Top + dr.Height / 2 + hy));
        }

        public bool TryVerticalPos(VectorPath p, PointF pt, out float angle, out float maxx)
        {
            angle = 0;
            maxx = float.MinValue;

            RectangleF b = p.GetBoundRect();
            PointF pos = new PointF(b.X + b.Width/2, b.Y + b.Height / 2);

            p.BeginTransform(pos);
            
                        

            p.CancelTransform();

            return false;
        }

        public void AutoNest(VectorPath path)
        {
            RectangleF b = path.GetBoundRect();
            PointF center = new PointF(b.Width / 2 + 2, b.Height / 2 + 2);

            if (paths.Count == 1)
            {
                path.BeginTransform(center);

                if (b.Width < b.Height)
                {                                       
                    path.SetOrigin(center);
                }
                else
                {
                    path.Rotate(90, center);
                    b = path.GetBoundRect();
                    center = new PointF(b.Width / 2 + 2, b.Height / 2 + 2);
                    path.SetOrigin(center);
                }
            }
            else
            {
                List<PointF> pts = new List<PointF>();

                foreach (VectorPath p in paths)
                {
                    if (p != path)
                    {
                        FillPossibleLocations(pts, path, p);
                    }
                }

                PointF bestPos = new PointF();
                float bestAngle = 0;
                float maxx = 0;

                foreach (PointF pt in pts)
                {


                }
            }
        }

        public void AutoNest()
        {
            List<VectorPath> pl = new List<VectorPath>();

            foreach (VectorPath p in paths)
            {
                p.ResetContraints();
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
                    cur.SetOrigin(new PointF(r.Width / 2+3, r.Height / 2+3));
                }
                else
                {
                    bool nested = false;
                    foreach (VectorPath vp in Paths)
                    {
                        RectangleF pr = vp.GetBoundRect();

                        /*
                        cur.SetOrigin(new PointF(pr.X + r.Width / 2 + 1, pr.Bottom + r.Height / 2 + 1));
                        if (cur.CheckConstraints())
                        {
                            nested = true;
                            break;
                        }

                        cur.SetOrigin(new PointF((pr.Width / 2 + pr.X) + r.Width / 2 + 1, pr.Bottom + r.Height / 2 + 1));
                        if (cur.CheckConstraints())
                        {
                            nested = true;
                            break;
                        }

                        cur.SetOrigin(new PointF(pr.Left + r.Width / 2 + 1, pr.Top + r.Height / 2 + 1));
                        if (cur.CheckConstraints())
                        {
                            nested = true;
                            break;
                        }
                        */
                    }

                    if (!nested)
                    {
                        float x = GetMaxX();
                        cur.SetOrigin(new PointF(x + r.Width / 2 + 3, r.Height / 2 + 3));
                    }
                }

                paths.Add(cur);
                pl.RemoveAt(0);
            }


            foreach (VectorPath p in paths)
            {
                p.ResetContraints();
                pl.Add(p);
            }
            //docWidth = GetMaxX();
        }

        public VectorPath ImportPath(VectorPath p)
        {
            VectorPath d = CreatePath();
            d.Source = p;
            d.Clone(p);
            return d;
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

        public void AutoFit(Rectangle size, VectorFitStyle style, bool center, VectorFitRegion region, int margin)
        {
            RectangleF bb = new RectangleF();

            UpdateCutBox();

            switch (region)
            {
                case VectorFitRegion.None:
                case VectorFitRegion.Document:
                    bb = new RectangleF(0, 0, docWidth, docHeight);
                    break;
                case VectorFitRegion.Content:
                    bb = GetBoundRect();
                    bb.Width += bb.X;
                    bb.Height += bb.Y;
                    break;
                case VectorFitRegion.CutBox:
                    bb = new RectangleF(0, 0, cutBox.Width, cutBox.Height);
                    break;
            }

            bb.Inflate(margin, margin);

            //size.Width -= margin;
            //size.Height -= margin;

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
            }

            //UpdateLdTamanho();
        }
    }
}
