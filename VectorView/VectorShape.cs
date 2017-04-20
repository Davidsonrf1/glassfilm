using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Text;

namespace VectorView
{
    public class VectorShape : VectorObject
    {
        Dictionary<int, VectorPoint> points = new Dictionary<int, VectorPoint>();
        Dictionary<int, VectorEdge> edges = new Dictionary<int, VectorEdge>();
        List<VectorEdge> edgeOrder = new List<VectorEdge>();

        RectangleF boundingBox = new RectangleF();
        bool isCheck = false;

        private int shapeID = 0;

        public IEnumerable<VectorEdge> Edges()
        {
            foreach (VectorEdge e in edges.Values)
            {
                yield return e;
            }
        }

        public IEnumerable<VectorPoint> Points()
        {
            foreach (VectorPoint p in points.Values)
            {
                yield return p;
            }
        }

        XmlNode svgNode = null;

        public XmlNode SvgNode
        {
            get
            {
                return svgNode;
            }

            internal set
            {
                svgNode = value;
            }
        }

        public void Clear()
        {
            points.Clear();
            edges.Clear();
            edgeOrder.Clear();
        }

        VectorPoint lastPoint = null;

        public PointF LastPoint
        {
            get
            {
                if (lastPoint == null)
                    return new PointF(0, 0);

                return new PointF(lastPoint.X, lastPoint.Y);
            }
        }

        public int ShapeID
        {
            get
            {
                return shapeID;
            }

            internal set
            {
                shapeID = value;
            }
        }

        bool updating = false;
        public void BeginUpdate()
        {
            updating = true;
        }

        public void EndUpdate()
        {
            updating = false;
            RecalculateShape();
        }

        public void RecalculateShape()
        {
            foreach (VectorEdge e in edges.Values)
            {
                e.Recalculate();
            }
        }

        internal void UpdatePoint(VectorPoint p)
        {
            if (!updating)
            {
                RecalculateShape();
            }
        }

        public VectorPoint AddPoint(float x, float y)
        {
            VectorPoint p = new VectorPoint(Document, this);

            p.X = x;
            p.Y = y;

            curPoint = p;
            points.Add(p.Id, p);

            if (!updating)
                RecalculateShape();

            return p;
        }

        public VectorPoint AddControlPoint(float x, float y, VectorPoint controled)
        {
            VectorPoint p = new VectorPoint(Document, this, controled);

            p.X = x;
            p.Y = y;
            p.Type = VectorPointType.Control;

            points.Add(p.Id, p);

            if (!updating)
                RecalculateShape();

            return p;
        }

        public void AddEdge(VectorPoint start, VectorPoint end, VectorEdge e)
        {
            e.Start = start;
            e.End = end;

            edgeOrder.Add(e);

            edges.Add(e.Id, e);

            if (!updating)
                RecalculateShape();
        }

        VectorPoint startPath = null;
        VectorPoint curPoint = null;

        public void BeginPath(float x, float y)
        {
            points.Clear();
            edges.Clear();

            BeginUpdate();

            startPath = AddPoint(x, y);
        }

        public void MoveTo(float x, float y)
        {
            AddPoint(x, y);
        }

        public void LineTo(float x, float y)
        {
            VectorEdge e = new VectorEdge(Document, this);

            VectorPoint start = curPoint;
            VectorPoint end = AddPoint(x, y);

            AddEdge(start, end, e);
        }

        public void CurveTo(float c1x, float c1y, float c2x, float c2y, float x, float y)
        {
            VectorCubicBezier e = new VectorCubicBezier(Document, this);

            VectorPoint start = curPoint;
            VectorPoint end = AddPoint(x, y);

            AddEdge(start, end, e);

            e.Control1.X = c1x;
            e.Control1.Y = c1y;

            e.Control2.X = c2x;
            e.Control2.Y = c2y;
        }

        public void QCurveTo(float cx, float cy, float x, float y)
        {
            VectorQuadraticBezier e = new VectorQuadraticBezier(Document, this);

            VectorPoint start = curPoint;
            VectorPoint end = AddPoint(x, y);

            AddEdge(start, end, e);

            e.Control.X = cx;
            e.Control.Y = cy;
        }

        public void EndPath(bool close = true)
        {
            if (close)
            {
                VectorEdge e = new VectorEdge(Document, this);

                VectorPoint start = curPoint;
                VectorPoint end = startPath;

                AddEdge(start, end, e);
            }

            EndUpdate();
        }

        public PointF GetAbsolutePoint(float x, float y)
        {
            PointF p = new PointF();

            p.X = curPoint.X + x;
            p.Y = curPoint.Y + y;

            return p;
        }

        public VectorShape(VectorDocument doc) : base(doc)
        {

        }

        public override RectangleF GetBoundBox()
        {
            if (edges.Count == 0)
            {
                return new RectangleF();
            }
            VectorObject first = null;
            foreach (VectorObject t in edges.Values)
            {
                first = t;
                break;
            }

            RectangleF r = first.GetBoundBox();

            float minx = r.Left;
            float miny = r.Top;
            float maxx = r.Right;
            float maxy = r.Bottom;

            foreach (VectorEdge e in edges.Values)
            {
                r = e.GetBoundBox();

                minx = Math.Min(r.Left, minx);
                miny = Math.Min(r.Top, miny);
                maxx = Math.Max(r.Right, maxx);
                maxy = Math.Max(r.Bottom, maxy);
            }

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        public static VectorShape CreateRectangle(VectorDocument doc, float x, float y, float w, float h)
        {
            return null;
        }

        internal override void Render()
        {
            bool old = Document.RenderParams.UseHilightPen;

            if (IsSelected)
                Document.RenderParams.UseHilightPen = true;

            foreach (VectorEdge e in edges.Values)
            {
                e.Render();
            }

            Document.RenderParams.UseHilightPen = old;
        }

        float pointSize = 6f;

        protected override bool InternalHitTest(float x, float y)
        {
            RectangleF bb = GetBoundBox();

            if ((x >= bb.X - 2 && x <= bb.X + bb.Width + 2) && (y >= bb.Y - 2 && y <= bb.Y + bb.Height + 2))
            {
                PointF p = new PointF(x, y);

                if (IsPointInside(p))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool IsClosedShape()
        {
            return true;
        }

        public bool IsPointInside(PointF pt)
        {
            int cross = 0;

            if (!IsClosedShape())
                return false;

            foreach (VectorEdge e in edges.Values)
            {
                List<PointF> pts = new List<PointF>();

                if (e.CrossPointCount(pt.Y, pts) > 0)
                {
                    foreach (PointF p in pts)
                    {
                        Document.AddDebugPoint(p);

                        if (p.X < pt.X)
                            cross++;
                    }
                }
            }

            return cross % 2 == 0 ? false : true;
        }

        internal virtual void EdgeChangeNotify(VectorEdge edge)
        {
            if (Document != null)
                Document.ShapeChangeNotify(this);
        }

        public override void RestoreClone(VectorObject clone)
        {
            VectorShape s = (VectorShape)clone;

            foreach (VectorPoint p in s.points.Values)
            {
                VectorPoint cp = null;

                if (points.TryGetValue(p.Id, out cp))
                    cp.RestoreClone(s);
            }

            foreach (VectorEdge e in s.edges.Values)
            {
                VectorPoint ce = null;

                if (points.TryGetValue(e.Id, out ce))
                    ce.RestoreClone(e);
            }
        }

        public override void FillOriginList(List<PointOrigin> ol)
        {
            base.FillOriginList(ol);

            foreach (VectorPoint p in points.Values)
            {
                ol.Add(p.GetOrigin());
            }
        }

        const int HPGL_UNIT = 40; // 0.025 mm

        Point GetHPGLPoint(VectorPoint p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * HPGL_UNIT);
            ret.Y = (int)Math.Round(p.Y * HPGL_UNIT);

            return ret;
        }

        Point GetHPGLPoint(PointF p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * HPGL_UNIT);
            ret.Y = (int)Math.Round(p.Y * HPGL_UNIT);

            return ret;
        }

        public virtual List<PointF> GetPolyline()
        {
            List<PointF> pl = new List<PointF>();

            pl.Add(new PointF(edgeOrder[0].Start.X, edgeOrder[0].Start.Y));

            foreach (VectorEdge e in edgeOrder)
            {
                e.FillPolyline(pl);
            }

            return pl;
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

        void WriteEdgeList(StringBuilder sb, List<VectorEdge> list)
        {
            bool wroteCmd = false;

            if (list.Count == 0)
                return;

            foreach (VectorObject ob in list)
            {
                if (ob.GetType().Equals(typeof(VectorEdge)))
                {
                    if (!wroteCmd)
                    {
                        sb.Append(" L");
                        wroteCmd = true;
                    }

                    VectorEdge ve = (VectorEdge)ob;

                    sb.AppendFormat(" {0},{1}", (int)Math.Round(ve.End.X), (int)Math.Round(ve.End.Y));
                }

                if (ob.GetType().Equals(typeof(VectorCubicBezier)))
                {
                    if (!wroteCmd)
                    {
                        sb.Append(" C");
                        wroteCmd = true;
                    }

                    VectorCubicBezier vc = (VectorCubicBezier)ob;

                    sb.AppendFormat(" {0},{1} {2},{3} {4},{5}", (int)Math.Round(vc.Control1.X), (int)Math.Round(vc.Control1.Y), (int)Math.Round(vc.Control2.X), (int)Math.Round(vc.Control2.Y), (int)Math.Round(vc.End.X), (int)Math.Round(vc.End.Y));
                }

                if (ob.GetType().Equals(typeof(VectorQuadraticBezier)))
                {
                    if (!wroteCmd)
                    {
                        sb.Append(" Q");
                        wroteCmd = true;
                    }

                    VectorQuadraticBezier vq = (VectorQuadraticBezier)ob;

                    sb.AppendFormat(" {0},{1} {2},{3}", (int)Math.Round(vq.Control.X), (int)Math.Round(vq.Control.Y), (int)Math.Round(vq.End.X), (int)Math.Round(vq.End.Y));
                }
            }
        }

        public string ToSVGPath()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<path style=\"fill: none; stroke:#e30016;stroke-width:0.56690001;stroke-linecap:butt;stroke-linejoin:miter;stroke-dasharray:none\" d=\"");
            sb.AppendFormat("M{0},{1}", (int)Math.Round(edgeOrder[0].Start.X), (int)Math.Round(edgeOrder[0].Start.Y));

            Type last = typeof(VectorEdge);

            List<VectorEdge> list = new List<VectorEdge>();

            foreach (VectorEdge e in edgeOrder)
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

        public void CloneShape(VectorShape shape)
        {
            BeginPath(shape.edgeOrder[0].Start.X, shape.edgeOrder[0].Start.Y);

            foreach (VectorEdge e in shape.edgeOrder)
            {
                if (e is VectorQuadraticBezier)
                {
                    VectorQuadraticBezier q = (VectorQuadraticBezier)e;
                    QCurveTo(q.Control.X, q.Control.Y, q.End.X, q.End.Y);
                }
                else if (e is VectorCubicBezier)
                {
                    VectorCubicBezier c = (VectorCubicBezier)e;
                    CurveTo(c.Control1.X, c.Control1.Y, c.Control2.X, c.Control2.Y, e.End.X, e.End.Y);
                }
                else
                {
                    LineTo(e.End.X, e.End.Y);
                }
            }

            EndPath(true);
        }
    }
}
