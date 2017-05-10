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

        GraphicsPath graphicPath = null;

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

        public void InvalidatePath()
        {
            if (graphicPath != null)
            {
                graphicPath.Dispose();
                graphicPath = null;
            }
        }

        void BuildPath()
        {
            if (graphicPath == null)
            {
                graphicPath = new GraphicsPath(FillMode.Alternate);
                graphicPath.StartFigure();

                foreach (VectorEdge e in edges)
                {
                    if (e is VectorMove)
                    {
                        graphicPath.CloseFigure();
                        continue;
                    }

                    if (e is VectorCurve)
                    {
                        if (e is VectorCubicBezier)
                        {
                            VectorCubicBezier c = (VectorCubicBezier)e;
                            graphicPath.AddBezier(c.StartX, c.StartY, c.Control1.X, c.Control1.Y, c.Control2.X, c.Control2.Y, c.EndX, c.EndY);
                        }
                        else
                        {
                            VectorQuadraticBezier c = (VectorQuadraticBezier)e;
                            graphicPath.AddBezier(c.StartX, c.StartY, c.Control.X, c.Control.Y, c.Control.X, c.Control.Y, c.EndX, c.EndY);
                        }
                    }
                    else if (e is VectorEdge)
                    {
                        graphicPath.AddLine(e.StartX, e.StartY, e.EndX, e.EndY);
                    }                    
                }

                graphicPath.CloseFigure();
            }
        }

        class PointComparer : IComparer<PointF>
        {
            public int Compare(PointF x, PointF y)
            {
                return x.X < y.X ? -1 : x.X == y.X ? 0 : 1;
            }
        }


        public float ComputeArea(float precicion=2)
        {
            area = 0;

            if (precicion <= 0)
                precicion = 1f;

            RectangleF r = GetBoundRect();
            float y = r.Y;

            while(y < r.Bottom)
            {
                float h = precicion;

                if (y + h > r.Bottom)
                    h = r.Bottom - y;

                List<PointF> pts = new List<PointF>();

                int ct = CrossPointCount(y, pts);

                pts.Sort(new PointComparer());

                for (int i = 0; i < pts.Count; i+=2)
                {
                    if (pts.Count - i >= 2)
                    {            
                        float va = (pts[i + 1].X - pts[i].X);

                        if (va < 0)
                        {

                        }

                        area += h * va;
                    }
                }

                y += precicion;
            }

            return area;
        }

        public GraphicsPath CopyPath()
        {
            if (graphicPath == null)
                BuildPath();

            GraphicsPath pathCopy = (GraphicsPath)graphicPath.Clone();
            return pathCopy;
        }

        Pen linePen = null;

        SolidBrush rSide = null;
        SolidBrush lSide = null;

        public void Render(Graphics g)
        {
            if (rSide == null)
            {
                rSide = new SolidBrush(Color.FromArgb(45, Color.LightBlue));
                lSide = new SolidBrush(Color.FromArgb(45, Color.LightGreen));
            }

            if (!isSelected)
                linePen = Document.NormalLinePen;
            else
                linePen = Document.SelectedLinePen;

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

                pl.AddRange(e.GetPoints());

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
                    //if (e is VectorMove)
                    //    continue;

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

            Matrix mt = new Matrix();
            mt.Rotate(10);

            return ret;
        }

        public RectangleF GetBoundRect()
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

            return new RectangleF(minx, miny, maxx-minx, maxy-miny);
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

        void WriteEdge(StringBuilder sb, VectorEdge edge, float ppmx, float ppmy)
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
            {
                WriteEdge(sb, e, ppmx, ppmy);
            }

            sb.Append("\" \n\t/>");

            return sb.ToString();
        }

        PointF transformOrigin = new PointF();
        Dictionary<VectorEdge, List<PointF>> origins = new Dictionary<VectorEdge, List<PointF>>();
        
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

            poligons = null;
        }

        public void Transform(Matrix mt, PointF origin)
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

            ComputeArea();
            poligons = null;
        }

        public void SetOrigin(PointF origin)
        {
            RectangleF bb = GetBoundRect();
            PointF center = VectorMath.GetBoxCenter(bb);

            BeginTransform(origin);
            Move(origin.X - center.X, origin.Y - center.Y);
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
        }
                
        public string ToHPGL()
        {
            StringBuilder sb = new StringBuilder();

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

            return sb.ToString();
        }
    }
}
