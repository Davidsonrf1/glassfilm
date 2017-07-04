using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace VectorView
{
    public enum VectorPathSide { None, Left, Right }

    public class VectorPath
    {
        VectorPath source = null;

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

        int importCount = 0;

        string tag = "";
        VectorPathSide side = VectorPathSide.None;

        float centerX = 0;
        float centerY = 0;

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

        public VectorPath Source
        {
            get
            {
                return source;
            }

            internal set
            {
                source = value;
            }
        }

        public bool InvalidConstraints
        {
            get
            {
                return invalidConstraints;
            }
        }

        public int ImportCount
        {
            get
            {
                return importCount;
            }

            set
            {
                importCount = value;
            }
        }


        public List<PointF[]> OriginalPoligons
        {
            get
            {
                return originalPoligons;
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

        public void ClosePolygon()
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

        List<PointF[]> originPolygons = null;

        public void ClosePath()
        {
            RectangleF b = GetBoundRect(true);
            PointF center = VectorMath.GetBoxCenter(b);

            centerX = center.X;
            centerY = center.Y;

            BuildPolygons();
            originPolygons = poligons;
            poligons = null;

            foreach (PointF[] poly in originPolygons)
            {
                for (int i = 0; i < poly.Length; i++)
                {
                    poly[i].X -= center.X;
                    poly[i].Y -= center.Y;
                }
            }
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

        public void CloneSource()
        {
            Clone(source);
        }

        public void Clone(VectorPath source)
        {
            if (source == null)
                return;

            edges.Clear();

            foreach (VectorEdge e in source.Edges)
            {
                VectorEdge c = e.Clone();
                AddEdge(c);
            }

            Tag = source.Tag;
            Side = source.Side;

            CopyMetrics(source);
            ComputeArea(false);
        }

        class PointComparer : IComparer<PointF>
        {
            public int Compare(PointF x, PointF y)
            {
                return x.X < y.X ? -1 : x.X == y.X ? 0 : 1;
            }
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

        SolidBrush errorBrush = null;

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

            if (importCount > 0)
            {
                linePen.Color = Color.Red;
                linePen.Width = 3;
            }

            if (poligons == null)
                BuildPolygons();

            foreach (PointF[] poly in poligons)
            {
                if (Side != VectorPathSide.None || importCount > 0)
                {
                    SolidBrush sb = null;

                    if (Side == VectorPathSide.Left)
                        sb = lSide;
                    else
                        sb = rSide;

                    Color oldSbColor = sb.Color;
                    if (importCount > 0)
                    {
                        sb.Color = Color.Black;
                    }

                    g.FillPolygon(sb, poly);

                    sb.Color = oldSbColor;
                }

                Color oldColor = linePen.Color;

                if (invalidConstraints && document.AutoCheckConstraints)
                {
                    Color ic = Color.FromArgb(128, Color.Red);

                    if (errorBrush == null)
                        errorBrush = new SolidBrush(ic);

                    linePen.Color = Color.Red;
                    g.FillPolygon(errorBrush, poly);
                }

                g.DrawPolygon(linePen, poly);

                if (linePen.Color != oldColor)
                    linePen.Color = oldColor;
            }

            if (Debugger.IsAttached && false)
            {
                if (originPolygons != null)
                {
                    PointF[] pts = new PointF[originPolygons[0].Length];

                    Array.Copy(originPolygons[0], pts, pts.Length);

                    Matrix mt = new Matrix();
                    mt.Translate(centerX, centerY);
                    //mt.Scale(scalex, scaley);
                    mt.Rotate(angle);

                    mt.TransformPoints(pts);

                    g.DrawPolygon(Pens.Red, pts);
                }
            }

            if (document.ShowConvexHull)
            {
                hull.Render(g);
            }
        }

        List<PointF[]> originalPoligons = null;
        public void BuildOriginalPolygons()
        {
            if (source != null)
            {
                originalPoligons = source.BuildPolygons();
                source.poligons = null;
            }
        }

        ConvexHull hull = null;
        List<PointF[]> poligons = null;
        public virtual List<PointF[]> BuildPolygons()
        {
            if (poligons != null)
                return poligons;

            poligons = new List<PointF[]>();

            List<PointF> pl = null;
            List<PointF> hl = new List<PointF>();

            hull = new ConvexHull();

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
                    hl.AddRange(pl);
                    poligons.Add(pl.ToArray());
                    pl = null;
                }
            }

            if (pl != null)
            {
                hl.AddRange(pl);
                poligons.Add(pl.ToArray());
            }

            hull.MakeConvexHull(hl);
            return poligons;
        }

        const int HPGL_UNIT = 40; // 0.025 mm
        Point GetHPGLPoint(PointF p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * HPGL_UNIT);
            ret.Y = (int)Math.Round(p.Y * HPGL_UNIT);

            return ret;
        }

        bool invalidConstraints = false;

        public void ResetContraints()
        {
            invalidConstraints = false;
        }

        public bool CheckContraints()
        {
            invalidConstraints = false;
            return !invalidConstraints;
        }

        internal bool GetDocIntersections(List<VectorPath> intersections)
        {
            RectangleF bb = GetBoundRect();
            intersections.Clear();

            if (hull == null)
                BuildPolygons();

            foreach (VectorPath p in document.Paths)
            {
                if (p == this)
                    continue;

                if (p.hull == null)
                    p.BuildPolygons();

                RectangleF pb = p.GetBoundRect();
                RectangleF ib = RectangleF.Intersect(bb, pb);

                if (!ib.IsEmpty)
                {
                    if (hull.Intersecting(p.hull))
                    {
                        if (CheckIntersectionContraints(p))
                            intersections.Add(p);
                    }
                }                
            }

            if (intersections.Count > 0)
                return true;

            return false;
        }

        internal bool CheckIntersectionContraints(VectorPath p)
        {
            RectangleF mb = GetBoundRect();

            if (p == this)
                return false;

            RectangleF pb = p.GetBoundRect();
            RectangleF ir = new RectangleF();

            if (poligons == null)
                BuildPolygons();

            ir = RectangleF.Intersect(mb, pb);

            if (!ir.IsEmpty )
            {
                // Verifica se algum dos pontos do path atual estão dentro do path testado
                foreach (PointF[] i in poligons)
                {
                    foreach (PointF pt in i)
                    {
                        if (pt.Y >= ir.Y && pt.Y <= ir.Bottom)
                        {
                            if (p.IsPointInside(pt))
                            {
                                return true;
                            }
                        }
                    }
                }

                if (p.poligons == null)
                    p.BuildPolygons();

                // Verifica se algum dos pontos do path sendo testado estão dentro do path atual
                foreach (PointF[] i in p.poligons)
                {
                    foreach (PointF pt in i)
                    {
                        if (pt.Y >= ir.Y && pt.Y <= ir.Bottom)
                        {
                            if (IsPointInside(pt))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        internal bool CheckBoundConstraints()
        {
            RectangleF r = GetBoundRect();

            if (r.X < 0 || r.Y < 0)            
                return invalidConstraints = true;            

            if (r.Bottom > document.CutBox.Bottom)
                return invalidConstraints = true;        

            return true;
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

        float oldAngle = 0;
        
        public void BeginTransform(PointF origin)
        {
            origins.Clear();

            oldAngle = angle;

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

            angle = oldAngle;

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

        float angle = 0;

        public void Rotate(float a, PointF origin)
        {
            Matrix mt = new Matrix();
            mt.Rotate(a);

            angle = oldAngle + a;

            Transform(mt, origin);

            if (document.AutoCheckConstraints)
                Check();
        }

        float scalex = 1;
        float scaley = 1;
        public void Scale(float scalex, float scaley, PointF origin)
        {
            Matrix mt = new Matrix();
            mt.Scale(scalex, scaley);

            Transform(mt, origin);
            this.scalex = scalex;
            this.scaley = scaley;

            if (originalScans != null && scans != null)
            {
                Array.Copy(originalScans, scans, scans.Length);
                mt.TransformPoints(scans);
            }

            if (document.AutoCheckConstraints)
                Check();

            ComputeArea(true);
        }

        void Check()
        {
            //if (!document.AutoCheckConstraints)
            //    return;

            invalidConstraints = false;

            RectangleF r = GetBoundRect();

            if (r.X < 0 || r.Y < 0)
                invalidConstraints = true;

            if (r.Bottom > document.CutSize)
                invalidConstraints = true;

            if (!invalidConstraints)
            {
                List<VectorPath> intersections = new List<VectorPath>();
                if (GetDocIntersections(intersections))
                {
                    invalidConstraints = true;
                }
            }
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

            if (document.AutoCheckConstraints)
                Check();

            poligons = null;
            AfterTransforms();

            PointF center = VectorMath.GetBoxCenter(boundBox);
            centerX = center.X;
            centerY = center.Y;
        }
        
        RectangleF GetPolygonBox(List<PointF[]> polygons)
        {
            RectangleF r = new RectangleF();

            float minx = float.MaxValue;
            float miny = float.MaxValue;
            float maxx = float.MinValue;
            float maxy = float.MinValue;

            foreach (PointF[] pts in poligons)
            {
                for (int i = 0; i < pts.Length; i++)
                {
                    minx = Math.Min(minx, pts[i].X);
                    miny = Math.Min(miny, pts[i].Y);
                    maxx = Math.Max(maxx, pts[i].X);
                    maxy = Math.Max(maxy, pts[i].Y);
                }                
            }

            r.X = minx;
            r.Y = miny;
            r.Width = maxx - minx;
            r.Height = maxy - miny;

            return r;
        }

        public string ToHPGL(bool flip)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            bool firstPoint = true;
            PointF hp;

            List<PointF[]> polyList = BuildPolygons();

            if (flip)
            {
                float y = document.GetMaxY() / 2;

                foreach (PointF[] pts in poligons)
                {
                    for (int i = 0; i < pts.Length; i++)
                    {
                        pts[i].Y = y + (y - pts[i].Y);
                    }
                }

                poligons = null;
            }

            Matrix mt = new Matrix();

            mt.Translate(document.GetMaxY(), 0);
            mt.Rotate(90);

            foreach (PointF[] polyline in polyList)
            {
                first = true;
                firstPoint = true;

                // Move o desenho para a posição ideal de corte
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

            // Reseta o desenho
            poligons = null;

            return sb.ToString();
        }
    }
}
