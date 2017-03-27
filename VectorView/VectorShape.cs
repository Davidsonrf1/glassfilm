using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace VectorView
{
    public class VectorShape : VectorObject
    {
        List<VectorPoint> points = new List<VectorPoint>();
        List<VectorEdge> edges = new List<VectorEdge>();

        public IEnumerable<VectorEdge> Edges()
        {
            foreach (VectorEdge e in edges)
            {
                yield return e;
            }
        }


        public IEnumerable<VectorPoint> Points()
        {
            foreach (VectorPoint p in points)
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

        public VectorPoint AddPoint(float x, float y)
        {
            VectorPoint p = new VectorPoint(Document);

            p.X = x;
            p.Y = y;

            curPoint = p;
            points.Add(p);

            return p;
        }

        public void AddEdge(VectorPoint start, VectorPoint end, VectorEdge e)
        {
            e.Start = start;
            e.End = end;

            edges.Add(e);
        }

        VectorPoint startPath = null;
        VectorPoint curPoint = null;

        public void BeginPath(float x, float y)
        {
            points.Clear();
            edges.Clear();

            startPath = AddPoint(x, y);
        }

        public void MoveTo(float x, float y)
        {
            AddPoint(x, y);
        }

        public void LineTo(float x, float y)
        {
            VectorEdge e = new VectorEdge(Document);

            VectorPoint start = curPoint;
            VectorPoint end = AddPoint(x, y);

            AddEdge(start, end, e);
        }

        public void ClosePath()
        {
            VectorEdge e = new VectorEdge(Document);

            VectorPoint start = curPoint;
            VectorPoint end = startPath;

            AddEdge(start, end, e);
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
            if (points.Count == 0)
            {
                return new RectangleF();
            }

            float minx = points[0].X;
            float miny = points[0].Y;
            float maxx = points[0].X;
            float maxy = points[0].Y;

            foreach (VectorPoint p in points)
            {
                minx = Math.Min(p.X, minx);
                miny = Math.Min(p.Y, miny);
                maxx = Math.Max(p.X, maxx);
                maxy = Math.Max(p.Y, maxy);
            }

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        public static VectorShape CreateRectangle(VectorDocument doc, float x, float y, float w, float h)
        {
            return null;   
        }

        internal override void Render(Graphics g)
        {
            foreach (VectorEdge e in edges)
            {
                e.Render(g);
            }

            if (IsHit)
            {
                RenderPoints(g);
            }
        }

        float pointSize = 6f;

        protected void RenderPoints(Graphics g)
        {
            Brush b = new SolidBrush(Color.Black);

            float w = pointSize * (1 / Document.Scale); 

            foreach (VectorPoint p in points)
                g.FillRectangle(b, p.X - w / 2, p.Y - w / 2, w, w);

            b.Dispose();
        }

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

            foreach (VectorEdge e in edges)
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
    }
}
