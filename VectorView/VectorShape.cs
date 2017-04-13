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

        public VectorShape(VectorDocument doc): base(doc)
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
            foreach (VectorEdge e in edges.Values)
            {
                e.Render();
            }
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

                if(e.CrossPointCount(pt.Y, pts) > 0)
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

        public string ToHPGL()
        {
            StringBuilder sb = new StringBuilder();

            bool first = true;
            Point p = new Point();

            sb.Append("IN;\nIP;\nSP1;");

            foreach (VectorEdge e in edgeOrder)
            {
                if (first)
                {
                    first = false;
                    p = GetHPGLPoint(e.Start);

                    sb.Append(string.Format("PU{0}{1}", p.X, p.Y));
                }

                p = GetHPGLPoint(e.End);
                sb.Append(string.Format("PD{0}{1}", p.X, p.Y));
            }

            return sb.ToString();
        }
    }
}
