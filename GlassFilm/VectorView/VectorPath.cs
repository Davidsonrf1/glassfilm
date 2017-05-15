using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace VectorView
{
    public enum VectorPathSide { None, Left, Right }

    public class VectorPath
    {
        VectorDocument document = null;
        internal VectorPath(VectorDocument doc)
        {
            document = doc;
        }

        float area = 0;

        List<VectorEdge> edges = new List<VectorEdge>();
        float curX, curY;
        bool isSelected = false;
        bool fillPath = false;
        Color fillColor = Color.Lime;

        string tag = "";
        VectorPathSide side = VectorPathSide.None;

        public VectorDocument Document
        {
            get
            {
                return document;
            }
        }

        public Pen LinePen
        {
            get
            {
                if (!isSelected)
                    linePen = Document.NormalLinePen;
                else
                    linePen = Document.SelectedLinePen;

                return linePen;
            }
        }

        public List<VectorEdge> Edges
        {
            get
            {
                return edges;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
            }
        }

        public bool FillPath
        {
            get
            {
                return fillPath;
            }

            set
            {
                fillPath = value;
            }
        }

        public float Area
        {
            get
            {
                if (area < 0)
                    ComputeArea(true);

                return area;
            }
        }

        public string Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
            }
        }

        public VectorPathSide Side
        {
            get
            {
                return side;
            }

            set
            {
                side = value;
            }
        }

        public bool IsIntersecting
        {
            get
            {
                return isIntersecting;
            }

            internal set
            {
                isIntersecting = value;
            }
        }


        VectorEdge curStart = null;       

        internal void AddEdge(VectorEdge e)
        {
            if (curStart == null)
                curStart = e;

            e.Path = this;
            edges.Add(e);
        }

        public void ClosePath()
        {
            if(curStart != null)
            {
                VectorClose e = new VectorClose(this, curX, curY, curStart.StartX, curStart.StartY);
                curX = curStart.StartX;
                curY = curStart.StartY;

                AddEdge(e);
                curStart = null;
            }

            curY = 0;
            curX = 0;
        }

        public void LineTo(float x, float y)
        {
            VectorEdge e = new VectorEdge(this, curX, curY, x, y);
            curX = x;
            curY = y;

            AddEdge(e);
        }

        public void MoveTo(float x, float y)
        {
            VectorMove m = new VectorMove(this, x, y, x, y);
            curX = x;
            curY = y;
            AddEdge(m);
        }

        public void CurveTo(float x, float y, float c1x, float c1y, float c2x, float c2y)
        {
            VectorCubicBezier c = new VectorCubicBezier(this, curX, curY, x, y);

            curX = x;
            curY = y;

            c.Control1 = new PointF(c1x, c1y);
            c.Control2 = new PointF(c2x, c2y);

            AddEdge(c);
        }

        public void QCurveTo(float x, float y, float cx, float cy)
        {
            VectorQuadraticBezier c = new VectorQuadraticBezier(this, curX, curY, x, y);

            curX = x;
            curY = y;

            c.Control = new PointF(cx, cy);

            AddEdge(c);
        }


        class PointComparer : IComparer<PointF>
        {
            public int Compare(PointF x, PointF y)
            {
                return x.X < y.X ? -1 : x.X == y.X ? 0 : 1;
            }
        }

        public void CheckContraints()
        {

        }
        
        float bestAngle = 0;
        PointF[] scans = null;
        float scanPrecision = 1;

        internal void CopyMetrics(VectorPath p)
        {
            bestAngle = p.bestAngle;
            scans = new PointF[p.scans.Length];
            Array.Copy(p.scans, scans, scans.Length);

            scanPrecision = p.scanPrecision;
        }

        public void ComputeMetrics(float precision = 2)
        {
            if (precision <= 0)
                precision = 1f;

            RectangleF r = GetBoundRect();
            float y = r.Y;

            PointF center = VectorMath.GetBoxCenter(r);
            List<PointF> sl = new List<PointF>();

            while (y < r.Bottom)
            {
                float h = precision;

                if (y + h > r.Bottom)
                    h = r.Bottom - y;

                List<PointF> pts = new List<PointF>();

                int ct = CrossPointCount(y, pts);
                pts.Sort(new PointComparer());

                for (int i = 0; i < pts.Count; i += 2)
                {
                    if (pts.Count - i >= 2)
                    {
                        sl.Add(new PointF(pts[i].X, y));
                        sl.Add(new PointF(pts[i + 1].X, y));
                    }
                }

                y += precision;
            }

            scanPrecision = precision;
            scans = sl.ToArray();

            Matrix mt = new Matrix();
            mt.Translate(-center.X, -center.Y);
            mt.TransformPoints(scans);
        }

        public float ComputeArea(bool force, float precision=1)
        {
            if (area > 0 && !force)
                return area;

            area = 0;

            if (scans == null)
                ComputeMetrics(precision);

            if (scans == null)
                return 0;

            if (precision <= 0)
                precision = 1f;

            area = 0;

            float h = ((scans[scans.Length - 1].Y - scans[0].Y) / scans.Length) * scanPrecision;

            for (int i = 0; i < scans.Length; i+=2)
                area += h * Math.Abs(scans[i + 1].X - scans[i].X);
            
            return area;
        }

        List<PointF>[] horizontalScans = null;

        void BuildHorizontalScans (int numScans = 100)
        {
            RectangleF r = GetBoundRect();

            horizontalScans = new List<PointF>[numScans];

            if (poligons == null)
                BuildPolygons();

            foreach (PointF[] p in poligons)
            {
                foreach (PointF pt in p)
                {
                    int idx = (int)(pt.X - r.X) / numScans;

                    if (horizontalScans[idx] == null)
                        horizontalScans[idx] = new List<PointF>();

                    float min = idx * (r.Height / numScans);
                    float max = (idx+1) * (r.Height / numScans);
                    
                    if (pt.Y >= min || pt.Y <= max)
                    {
                        //horizontalScans[idx]
                    }
                }
            }
        }

        Pen linePen = null;

        SolidBrush rSide = null;
        SolidBrush lSide = null;

        public void Render(Graphics g)
        {
            if (rSide == null)
            {
                rSide = new SolidBrush(Color.FromArgb(85, Color.Blue));
                lSide = new SolidBrush(Color.FromArgb(85, Color.Green));
            }

            if (!isSelected)
                linePen = Document.NormalLinePen;
            else
                linePen = Document.SelectedLinePen;

            if (isIntersecting)
            {
                linePen.DashStyle = DashStyle.DashDotDot;
            }

            if (poligons == null)
                BuildPolygons();

            foreach (PointF[] poly in poligons)
            {
                if (Side != VectorPathSide.None)
                {
                    SolidBrush sb = null;

                    if (Side == VectorPathSide.Left)
                        sb = lSide;
                    else
                        sb = rSide;

                    g.FillPolygon(sb, poly);
                }

                g.DrawPolygon(linePen, poly);
            }

            /*
            foreach (VectorEdge e in edges)
            {
                if (e is VectorMove)
                {
                    continue;
                }

                if (e is VectorCurve)
                {
                    if (e is VectorCubicBezier)
                    {
                        VectorCubicBezier c = (VectorCubicBezier)e;
                        g.DrawBezier(linePen, c.StartX, c.StartY, c.Control1.X, c.Control1.Y, c.Control2.X, c.Control2.Y, c.EndX, c.EndY);
                    }
                    else
                    {
                        VectorQuadraticBezier c = (VectorQuadraticBezier)e;
                        List<PointF> pts = c.GetPoints();
                        g.DrawLines(linePen, pts.ToArray());
                    }
                }
                else if (e is VectorEdge)
                {
                    g.DrawLine(linePen, e.StartX, e.StartY, e.EndX, e.EndY);
                }
            }
            */
            /*if (drawMiddlePoint)
            {
                float w = 3 / Document.Scale;

                PointF m = GetMiddlePoint();
                g.FillEllipse(Brushes.OrangeRed, m.X - w / 2, m.Y - w / 2, w, w);
            }*/
        }

        List<PointF[]> poligons = null;

        public virtual List<PointF[]> BuildPolygons()
        {
            if (poligons != null)
                return poligons;

            poligons = new List<PointF[]>();

            List<PointF> pl = null;

            foreach (VectorEdge e in edges)
            {
                if (pl == null)
                {
                    pl = new List<PointF>();
                }

                List<PointF> tp = e.GetPoints(false);
                pl.AddRange(tp);

                if (e is VectorClose)
                {
                    poligons.Add(pl.ToArray());
                    pl = null;
                }
            }

            if (pl != null)
                poligons.Add(pl.ToArray());

            return poligons;
        }

        public virtual List<PointF> GetPolyline()
        {
            List<PointF> pl = new List<PointF>();

            if (edges.Count > 0)
            {
                pl.Add(new PointF(edges[0].StartX, edges[0].StartY));

                foreach (VectorEdge e in edges)
                {
                    e.FillPointList(pl, true);
                }
            }

            return pl;
        }

        const int HPGL_UNIT = 40; // 0.025 mm
        Point GetHPGLPoint(PointF p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * HPGL_UNIT);
            ret.Y = (int)Math.Round(p.Y * HPGL_UNIT);

            return ret;
        }
        
        internal void ResetConstraints()
        {
            isIntersecting = false;
        }

        bool isIntersecting = false;

        public bool Intersect(VectorPath p)
        {
            if (p == null)
                return false;

            RectangleF r1 = GetBoundRect();
            RectangleF r2 = p.GetBoundRect();

            PointF c1 = VectorMath.GetBoxCenter(r1);
            PointF c2 = VectorMath.GetBoxCenter(r2);

            if (!RectangleF.Intersect(r1, r2).IsEmpty)
            {
                if (r1.Contains(c2) || r2.Contains(c1))
                    return true;

                if (poligons == null)
                    BuildPolygons();

                foreach (PointF[] pl in poligons)
                {
                    foreach (PointF pt in pl)
                    {
                        if (r2.Contains(pt))
                        {
                            if (p.IsPointInside(pt))
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        RectangleF boundBox = RectangleF.Empty;

        public RectangleF GetBoundRect(bool force = false)
        {
            if (boundBox == RectangleF.Empty || force)
            {
                float minx, miny, maxx, maxy;

                minx = float.MaxValue;
                miny = float.MaxValue;
                maxx = float.MinValue;
                maxy = float.MinValue;

                foreach (VectorEdge e in edges)
                {
                    if (e is VectorMove)
                        continue;

                    RectangleF r = e.GetBoundRect();

                    minx = Math.Min(minx, r.X);
                    miny = Math.Min(miny, r.Y);
                    maxx = Math.Max(maxx, r.Right);
                    maxy = Math.Max(maxy, r.Bottom);
                }

                boundBox = new RectangleF(minx, miny, maxx - minx, maxy - miny);
            }

            return boundBox;
        }

        public virtual int CrossPointCount(float hline, List<PointF> crossPoints = null)
        {
            int count = 0;

            foreach (VectorEdge e in edges)
            {
                count += e.CrossPointCount(hline, crossPoints);
            }

            return count;
        }

        public bool IsPointInside(PointF pt)
        {
            int cross = 0;

            foreach (VectorEdge e in edges)
            {
                List<PointF> pts = new List<PointF>();

                if (e.CrossPointCount(pt.Y, pts) > 0)
                {
                    foreach (PointF p in pts)
                    {
                        if (p.X < pt.X)
                            cross++;
                    }
                }
            }

            return cross % 2 == 0 ? false : true;
        }

        public PointF GetMiddlePoint()
        {
            RectangleF r = GetBoundRect();

            return new PointF(r.X + r.Width / 2, r.Y + r.Height / 2);
        }

        VectorEdge lastEdge = null;

        bool IsSamAsLast(VectorEdge edge)
        {
            if (lastEdge == null)
                return false;

            if (lastEdge.Type == VectorEdgeType.Move && edge.Type == VectorEdgeType.Line)
                return true;

            return lastEdge.Type == edge.Type;
        }

        void WriteSvgEdge(StringBuilder sb, VectorEdge edge, float ppmx, float ppmy)
        {
            int sx, sy, ex, ey;

            sx = (int)Math.Round(edge.StartX * ppmx);
            sy = (int)Math.Round(edge.StartY * ppmy);
            ex = (int)Math.Round(edge.EndX * ppmx);
            ey = (int)Math.Round(edge.EndY * ppmy);

            switch (edge.Type)
            {
                case VectorEdgeType.Move:
                    sb.Append(" M");
                    sb.AppendFormat("{0},{1}", ex, ey);
                    break;
                case VectorEdgeType.Line:
                    if (!IsSamAsLast(edge))
                        sb.Append(" L");
                    else
                        sb.Append(" ");

                    sb.AppendFormat("{0},{1}", ex, ey);
                    break;
                case VectorEdgeType.Curve:
                    if (!IsSamAsLast(edge))
                        sb.Append(" C");
                    else
                        sb.Append(" ");

                    VectorCubicBezier vc = (VectorCubicBezier)edge;
                    int c1x, c1y, c2x, c2y;

                    c1x = (int)Math.Round(vc.Control1.X * ppmx);
                    c1y = (int)Math.Round(vc.Control1.Y * ppmy);
                    c2x = (int)Math.Round(vc.Control2.X * ppmx);
                    c2y = (int)Math.Round(vc.Control2.Y * ppmy);

                    sb.AppendFormat("{0},{1} {2},{3} {4},{5}", c1x, c1y, c2x, c2y, ex, ey);

                    break;
                case VectorEdgeType.QCurve:
                    if (!IsSamAsLast(edge))
                        sb.Append(" Q");
                    else
                        sb.Append(" ");

                    VectorQuadraticBezier qc = (VectorQuadraticBezier)edge;
                    int cx, cy;

                    cx = (int)Math.Round(qc.Control.X * ppmx);
                    cy = (int)Math.Round(qc.Control.Y * ppmy);

                    sb.AppendFormat("{0},{1} {2},{3}", cx, cy, ex, ey);
                    break;
                case VectorEdgeType.Close:
                    sb.Append(" Z");
                    break;
            }

            lastEdge = edge;
        }
        
        public string ToSVGPath(float ppmx, float ppmy)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytes = Encoding.UTF8.GetBytes(tag);
            string b64Tag = Convert.ToBase64String(bytes);

            sb.AppendFormat("<path gf-side=\"{0}\" gf-tag=\"{1}\" style=\"fill: none; stroke:#e30016;stroke-width:1\" \n\td=\"", side.ToString().ToLower(), b64Tag);

            foreach (VectorEdge e in edges)
                WriteSvgEdge(sb, e, ppmx, ppmy);

            sb.Append("\" \n\t/>");

            return sb.ToString();
        }

        PointF transformOrigin = new PointF();
        Dictionary<VectorEdge, List<PointF>> origins = new Dictionary<VectorEdge, List<PointF>>();
        PointF[] originalScans = null;
        
        public void BeginTransform(PointF origin)
        {
            origins.Clear();

            foreach (VectorEdge e in edges)
            {
                List<PointF> pl = new List<PointF>();
                e.CopyPoints(pl);
                origins.Add(e, pl);
            }

            transformOrigin.X = origin.X;
            transformOrigin.Y = origin.Y;

            if (scans != null)
            {
                originalScans = new PointF[scans.Length];
                Array.Copy(scans, originalScans, scans.Length);
            }
        }

        public void CancelTransform()
        {
            if (origins.Count > 0)
            {
                foreach (VectorEdge e in edges)
                {
                    e.SetPoints(origins[e]);
                }

                origins.Clear();
            }

            scans = originalScans;
            originalScans = null;

            poligons = null;

            AfterTransforms();
        }

        void Transform(Matrix mt, PointF origin)
        {
            foreach (VectorEdge e in edges)
            {
                List<PointF> tl = new List<PointF>();
                List<PointF> o = origins[e];

                foreach (PointF pt in o)
                {
                    PointF[] pa = o.ToArray();

                    for (int i = 0; i < pa.Length; i++)
                    {
                        pa[i].X -= origin.X;
                        pa[i].Y -= origin.Y;
                    }

                    mt.TransformPoints(pa);

                    for (int i = 0; i < pa.Length; i++)
                    {
                        pa[i].X += origin.X;
                        pa[i].Y += origin.Y;

                        tl.Add(pa[i]);
                    }
                }

                e.SetPoints(tl);
            }

            poligons = null;
            AfterTransforms();
        }



        void AfterTransforms()
        {
            if (document != null)
            {
                if (document.AutoCheckConstraints)
                {
                    document.CheckConstraints();
                }
            }

            GetBoundRect(true);
            ComputeArea(true);
        }

        public void SetOrigin(PointF origin)
        {
            RectangleF bb = GetBoundRect();
            PointF center = VectorMath.GetBoxCenter(bb);

            BeginTransform(origin);
            Move(origin.X - center.X, origin.Y - center.Y);

            AfterTransforms();
        }

        public void Flip(bool vertical, bool horizontal)
        {
            RectangleF r = GetBoundRect();
            PointF center = new PointF(r.X + r.Width / 2, r.Y + r.Height / 2);

            BeginTransform(center);

            foreach (VectorEdge e in edges)
            {
                List<PointF> tl = new List<PointF>();

                foreach (PointF pt in origins[e])
                {
                    float x=pt.X, y=pt.Y;

                    if (horizontal)
                        x = -(pt.X - transformOrigin.X) + transformOrigin.X;

                    if (vertical)
                        y = -(pt.Y - transformOrigin.Y) + transformOrigin.Y;

                    tl.Add(new PointF(x, y));
                }

                e.SetPoints(tl);
            }

            poligons = null;
            AfterTransforms();
        }

        public void Flip()
        {
            Flip(true, true);
        }

        public void VerticalFlip()
        {
            Flip(true, false);
        }

        public void HorizontalFlip()
        {
            Flip(false, true);
        }

        public void Rotate(float angle, PointF origin)
        {
            Matrix mt = new Matrix();
            mt.Rotate(angle);

            Transform(mt, origin);
        }

        public void Scale(float scalex, float scaley, PointF origin)
        {
            Matrix mt = new Matrix();
            mt.Scale(scalex, scaley);

            Transform(mt, origin);

            if (originalScans != null && scans != null)
            {
                Array.Copy(originalScans, scans, scans.Length);
                mt.TransformPoints(scans);
            }

            ComputeArea(true);
        }

        public void Move(float dx, float dy)
        {
            foreach (VectorEdge e in edges)
            {
                List<PointF> tl = new List<PointF>();

                foreach (PointF pt in origins[e])
                    tl.Add(new PointF(pt.X + dx, pt.Y + dy));

                e.SetPoints(tl);
            }

            poligons = null;
            AfterTransforms();
        }
        
        public string ToHPGL()
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            bool firstPoint = true;
            PointF hp;

            List<PointF[]> polyList = BuildPolygons();

            Matrix mt = new Matrix();

            mt.Rotate(90);
            mt.Translate(0, -document.Height);            

            foreach (PointF[] polyline in polyList)
            {
                first = true;
                firstPoint = true;

                mt.TransformPoints(polyline);

                foreach (PointF p in polyline)
                {
                    if (first)
                    {
                        first = false;
                        hp = GetHPGLPoint(p);
                        sb.Append(string.Format("PU{0},{1};", hp.X, hp.Y));

                        sb.Append("PD");
                        continue;
                    }

                    if (!firstPoint)
                    {
                        sb.Append(',');
                    }

                    firstPoint = false;

                    hp = GetHPGLPoint(p);
                    sb.Append(string.Format("{0},{1}", hp.X, hp.Y));
                }
            }
                        
            /*
            bool first = true;
            bool firstPoint = true;
            Point p = new Point();

            List<PointF> polyline = GetPolyline();

            foreach (PointF pl in polyline)
            {
                if (first)
                {
                    first = false;
                    p = GetHPGLPoint(pl);
                    sb.Append(string.Format("PU{0},{1};", p.X, p.Y));

                    sb.Append("PD");

                    continue;
                }

                if (!firstPoint)
                {
                    sb.Append(',');
                }

                firstPoint = false;

                p = GetHPGLPoint(pl);
                sb.Append(string.Format("{0},{1}", p.X, p.Y));
            }
            */


            return sb.ToString();
        }
    }
}
