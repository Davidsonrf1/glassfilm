using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace VectorView
{
    public class VectorPath
    {
        VectorDocument document = null;
        internal VectorPath(VectorDocument doc)
        {
            document = doc;
        }

        List<VectorEdge> edges = new List<VectorEdge>();
        float curX, curY;
        bool isSelected = false;
        bool fillPath = false;
        Color fillColor = Color.Lime;

        bool drawMiddlePoint = true;

        GraphicsPath gPath = null;

        public VectorDocument Document
        {
            get
            {
                return document;
            }
        }

        internal Pen LinePen
        {
            get
            {
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

        VectorEdge curStart = null;

        void AddEdge(VectorEdge e)
        {
            if (curStart == null)
                curStart = e;

            edges.Add(e);
        }

        public void ClosePath()
        {
            if(curStart != null)
            {
                LineTo(curStart.StartX, curStart.StartY);
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

        Pen linePen = null;
        public void Render(Graphics g)
        {
            if (!isSelected)
                linePen = Document.NormalLinePen;
            else
                linePen = Document.SelectedLinePen;

            foreach (VectorEdge e in edges)
            {
                if (e is VectorMove)
                    continue;

                e.Render(g);
            }

            if (drawMiddlePoint )
            {
                float w = 3 / Document.Scale;

                PointF m = GetMiddlePoint();
                g.FillEllipse(Brushes.OrangeRed, m.X - w/2, m.Y - w/2, w, w);
            }
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

        public RectangleF GetBoundRect()
        {
            float minx, miny, maxx, maxy;

            minx = float.MaxValue;
            miny = float.MaxValue;
            maxx = float.MinValue;
            maxy = float.MinValue;

            foreach (VectorEdge e in edges)
            {
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

        void WriteEdgeList(StringBuilder sb, List<VectorEdge> list)
        {
            bool wroteCmd = false;

            if (list.Count == 0)
                return;

            foreach (VectorEdge ob in list)
            {
                if (ob.GetType().Equals(typeof(VectorEdge)))
                {
                    if (!wroteCmd)
                    {
                        sb.Append(" L");
                        wroteCmd = true;
                    }

                    sb.AppendFormat(" {0},{1}", (int)Math.Round(ob.EndX), (int)Math.Round(ob.EndY));
                }

                if (ob.GetType().Equals(typeof(VectorCubicBezier)))
                {
                    if (!wroteCmd)
                    {
                        sb.Append(" C");
                        wroteCmd = true;
                    }

                    VectorCubicBezier vc = (VectorCubicBezier)ob;

                    sb.AppendFormat(" {0},{1} {2},{3} {4},{5}", (int)Math.Round(vc.Control1.X), (int)Math.Round(vc.Control1.Y), (int)Math.Round(vc.Control2.X), (int)Math.Round(vc.Control2.Y), (int)Math.Round(vc.EndX), (int)Math.Round(vc.EndY));
                }

                if (ob.GetType().Equals(typeof(VectorQuadraticBezier)))
                {
                    if (!wroteCmd)
                    {
                        sb.Append(" Q");
                        wroteCmd = true;
                    }

                    VectorQuadraticBezier vq = (VectorQuadraticBezier)ob;

                    sb.AppendFormat(" {0},{1} {2},{3}", (int)Math.Round(vq.Control.X), (int)Math.Round(vq.Control.Y), (int)Math.Round(vq.EndX), (int)Math.Round(vq.EndY));
                }
            }
        }

        public string ToSVGPath()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<path style=\"fill: none; stroke:#e30016;stroke-width:0.56690001;stroke-linecap:butt;stroke-linejoin:miter;stroke-dasharray:none\" d=\"");
            sb.AppendFormat("M{0},{1}", (int)Math.Round(edges[0].StartX), (int)Math.Round(edges[0].StartY));

            Type last = typeof(VectorEdge);
            List<VectorEdge> list = new List<VectorEdge>();

            foreach (VectorEdge e in edges)
            {
                if (last.Equals(e.GetType()))
                {
                    list.Add(e);
                }
                else
                {
                    WriteEdgeList(sb, list);

                    list.Clear();
                    list.Add(e);
                    last = e.GetType();
                }
            }

            WriteEdgeList(sb, list);
            sb.Append(" Z\" />");

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
