using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using VectorView.Bezier;
using VectorView.Plotter;

namespace VectorView
{
    public enum VectorPathSide { None, Left, Right }

    public class VectorPath
    {
        float posx, posy;
        float angle = 0;
        List<VectorSegment> segments = new List<VectorSegment>();
        PointF origin = new PointF(0, 0);
        RectangleF boundRect = new RectangleF();
        RectangleF limits = new RectangleF();
        VectorDocument document = null;
        List<PointF[]> polygons = null;
        VectorPathSide side = VectorPathSide.None;
        string tag = "";
        PointF testPoint = new PointF();
        float area = 0;
        bool testPointInside = false;
        bool selected = false;
        bool outOfLimits = false;

        int importCount = 0;

        VectorPath source = null;

        float width = 0, height = 0;

        List<VectorPath> intersectList = new List<VectorPath>();

        bool drawBoundBox = false;
        bool drawTestPoint = false;
        bool drawNormalizedPath = false;
        bool drawCenterPoint = false;
        bool drawCurGrid = false;
        bool fillOnPointInside = false;

        float lastGoodAngle = 0;
        PointF lastGoodPos = new PointF();

        uint id = 0;

        public float X
        {
            get
            {
                return posx;
            }

            set
            {
                float dx = value - posx;
                Translate(dx, 0);
            }
        }

        public float Y
        {
            get
            {
                return posy;
            }

            set
            {
                float dy = value - posy;
                Translate(dy, 0);
            }
        }

        public RectangleF BoundRect
        {
            get
            {
                return boundRect;
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

        public VectorDocument Document
        {
            get
            {
                return document;
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }

            internal set
            {
                angle = value;
            }
        }

        public bool DrawTestPoint
        {
            get
            {
                return drawTestPoint;
            }

            set
            {
                drawTestPoint = value;
            }
        }

        public bool DrawNormalizedPath
        {
            get
            {
                return drawNormalizedPath;
            }

            set
            {
                drawNormalizedPath = value;
            }
        }

        public bool DrawCenterPoint
        {
            get
            {
                return drawCenterPoint;
            }

            set
            {
                drawCenterPoint = value;
            }
        }

        public bool DrawBoundBox
        {
            get
            {
                return drawBoundBox;
            }

            set
            {
                drawBoundBox = value;
            }
        }

        public bool DrawCurGrid
        {
            get
            {
                return drawCurGrid;
            }

            set
            {
                drawCurGrid = value;
            }
        }

        public float Area
        {
            get
            {
                return area;
            }
        }

        public bool FillOnPointInside
        {
            get
            {
                return fillOnPointInside;
            }

            set
            {
                fillOnPointInside = value;
            }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }

            internal set
            {
                selected = value;
            }
        }

        public bool TestPointInside
        {
            get
            {
                return testPointInside;
            }
        }

        public RectangleF Limits
        {
            get
            {
                return limits;
            }
        }

        public uint Id
        {
            get
            {
                return id;
            }

            internal set
            {
                id = value;
            }
        }

        public bool IsValidPos
        {
            get
            {
                return intersectList.Count > 0 || outOfLimits;
            }
        }

        public float Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
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

        public VectorPath Source
        {
            get
            {
                return source;
            }

            set
            {
                source = value;
            }
        }

        public bool OutOfLimits
        {
            get
            {
                return outOfLimits;
            }
        }

        public void UpdateGoodPos()
        {
            if (!IsValidPos)
            {
                lastGoodAngle = angle;
                lastGoodPos.X = posx;
                lastGoodPos.Y = posy;
            }
        }
        
        public void RestoreGoodPos()
        {
            angle = lastGoodAngle;
            SetPos(lastGoodPos.X, lastGoodPos.Y);
            intersectList.Clear();
            outOfLimits = false;
        }

        internal VectorPath(VectorDocument doc)
        {
            document = doc;
        }

        public void SetPos(float x, float y)
        {
            posx = x;
            posy = y;
        }

        public void Translate(float dx, float dy)
        {
            posx += dx;
            posy += dy;
        }

        public void Rotate(float angle)
        {
            this.angle = angle;

            polygons = null;

            CalcLimits();
        }

        public List<PointF[]> GetTransfomedPolygons(bool translate=true)
        {
            if (polygons == null)
                BuildPolygons();

            Matrix mt = new Matrix();

            if (translate)
                mt.Translate(posx, posy);

            mt.Rotate(angle);

            List<PointF[]> poly = new List<PointF[]>();

            foreach (PointF[] pts in polygons)
            {
                PointF[] tp = new PointF[pts.Length];

                Array.Copy(pts, tp, pts.Length);
                mt.TransformPoints(tp);

                poly.Add(tp);
            }

            return poly;
        }

        void CalcLimits()
        {
            if (polygons == null)
                BuildPolygons();

            Matrix mt = new Matrix();
            mt.Rotate(angle);

            List<PointF[]> poly = new List<PointF[]>();

            foreach (PointF[] pts in polygons)
            {
                PointF[] tp = new PointF[pts.Length];

                Array.Copy(pts, tp, pts.Length);
                mt.TransformPoints(tp);

                poly.Add(tp);
            }

            limits = VectorMath.GetBoundBox(poly);

            width = limits.Width;
            height = limits.Height;
        }

        void AddPolyFromList(List<VectorSegment> segs)
        {
            if (segs.Count == 0)
                return;

            if (polygons == null)
                polygons = new List<PointF[]>();

            List<PointF> pts = new List<PointF>();
            pts.Add(segs[0].Start);

            foreach (VectorSegment s in segs)
            {
                if (pts.Count == 0)
                {
                    pts.Add(s.Start);
                }

                PointF[] segPoints = new PointF[s.Points.Length - 1];
                Array.Copy(s.Points, 1, segPoints, 0, segPoints.Length);

                pts.AddRange(s.Points);
            }

            polygons.Add(pts.ToArray());
        }

        public void BuildPolygons()
        {
            if (polygons != null)
                polygons.Clear();

            List<VectorSegment> curPoly = new List<VectorSegment>();

            foreach (VectorSegment s in segments)
            {
                if (s is VectorMoveSegment)
                {
                    AddPolyFromList(curPoly);
                    curPoly.Clear();
                    continue;
                }

                curPoly.Add(s);
            }

            if (curPoly.Count > 0)
                AddPolyFromList(curPoly);
        }

        public void Refresh()
        {
            foreach (VectorSegment s in segments)
            {
                s.ClearPoints();
            }
        }        

        public bool IsPointInside(PointF pt, bool onlyBound)
        {
            pt.X = pt.X - posx;
            pt.Y = pt.Y - posy;

            Matrix mt = new Matrix();
            mt.Rotate(-angle);

            PointF[] pts = new PointF[] { pt };
            mt.TransformPoints(pts);

            pt.X = pts[0].X;
            pt.Y = pts[0].Y;

            if ((pt.X >= boundRect.X && pt.X <= boundRect.Right) && (pt.Y >= boundRect.Y && pt.Y <= boundRect.Bottom))
            {
                if (onlyBound)
                    return true;
                
                if (map == null)
                    BuildMap();

                int x = (int)(pt.X - boundRect.X);
                int y = (int)(pt.Y - boundRect.Y);

                int mapIndex = y * mapW + x;

                if (map[mapIndex] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        GridEntry curGrid = null;
        public bool TestDocPoint(PointF docPoint, bool onlyBox = false)
        {
            testPointInside = false;

            testPoint.X = docPoint.X - posx;
            testPoint.Y = docPoint.Y - posy;

            Matrix mt = new Matrix();
            mt.Rotate(-angle);

            PointF[] pts = new PointF[] { testPoint };
            mt.TransformPoints(pts);

            testPoint.X = pts[0].X;
            testPoint.Y = pts[0].Y;

            curGrid = null;

            if ((testPoint.X >= boundRect.X && testPoint.X <= boundRect.Right) && (testPoint.Y >= boundRect.Y && testPoint.Y <= boundRect.Bottom))
            {
                if (onlyBox)
                {
                    testPointInside = true;
                    return true;
                }

                if (map == null)
                    BuildMap();

                int x = (int)(testPoint.X - boundRect.X);
                int y = (int)(testPoint.Y - boundRect.Y);

                int mapIndex = y * mapW + x;

                if (map[mapIndex] != 0)
                {
                    testPointInside = true;
                }
            }

            return testPointInside;
        }

        public uint GenerateCutShape(uint sheet)
        {
            float diagonal = ((float)Math.Sqrt((BoundRect.Width * BoundRect.Width) + (BoundRect.Height * BoundRect.Height))) + 1;
            float w = diagonal / 2;

            uint shape = CutLibWrapper.CreateShape(sheet, (uint)id);

            if (polygons == null)
                BuildPolygons();

            List<float> polyList = new List<float>();

            int count = 0;

            foreach (PointF[] pts in polygons)
            {
                count += pts.Length;

                foreach (PointF p in pts)
                {
                    polyList.Add(p.X);
                    polyList.Add(p.Y);
                }
            }

            IntPtr poly = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(float)) * count * 2);
            Marshal.Copy(polyList.ToArray(), 0, poly, polyList.Count);

            CutLibWrapper.BuildScansFromPolygon(sheet, shape, diagonal, diagonal, poly, count);

            Marshal.FreeHGlobal(poly);

            return shape;
        }

        /*
        public uint GenerateCutShape(uint sheet)
        {
            float diagonal = ((float)Math.Sqrt((BoundRect.Width * BoundRect.Width) + (BoundRect.Height * BoundRect.Height))) + 1;

            Bitmap buffer = new Bitmap(((int)diagonal), ((int)diagonal), PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(buffer);
            float w = diagonal / 2;

            float sa = Angle;
            float sx = posx;
            float sy = posy;

            Rectangle box = new Rectangle(0, 0, buffer.Width, buffer.Height);

            uint shape = CutLibWrapper.CreateShape(sheet, (uint)id);

            g.SmoothingMode = SmoothingMode.None;

            SetPos(w, w);
            SolidBrush fb = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
            Color cb = Color.FromArgb(0);

            for (int angle = 0; angle < 360; angle++)
            {
                Rotate(angle);
                List<PointF[]> polys = GetTransfomedPolygons();

                g.Clear(cb);
                foreach (PointF[] pts in polys)
                {
                    g.FillPolygon(fillBrush, pts);
                }

                BitmapData bd = buffer.LockBits(box, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                CutLibWrapper.AddAngle(sheet, (uint)id, angle, bd.Width, bd.Height, bd.Scan0);

                buffer.UnlockBits(bd);
            }

            SetPos(sx, sy);
            Rotate(sa);

            CutLibWrapper.SortAngles(sheet, shape);
            Angle = sa;

            return shape;
        }
        */

        public void Normalize()
        {
            BuildPolygons();

            RectangleF r = VectorMath.GetBoundBox(polygons);
            PointF center = new PointF(r.X + r.Width / 2, r.Y + r.Height / 2);

            float dx = -center.X;
            float dy = -center.Y;

            foreach (VectorSegment s in segments)
            {
                s.Move(dx, dy);
            }

            r.X += dx;
            r.Y += dy;

            boundRect = r;

            polygons = null;
            BuildPolygons();

            posx = center.X;
            posy = center.Y;
        }

        public void BeginPath()
        {
            curX = 0;
            curY = 0;
            startX = 0;
            startY = 0;
        }

        public void ClosePolygon()
        {
            VectorLineSegment line = new VectorLineSegment();

            line.Start = new PointF(curX, curY);
            line.End = new PointF(startX, startY);

            curX = startX;
            curY = startY;

            segments.Add(line);
        }

        class GridLine
        {
            PointF start, end;

            public PointF End
            {
                get
                {
                    return end;
                }

                set
                {
                    end = value;
                }
            }

            public PointF Start
            {
                get
                {
                    return start;
                }

                set
                {
                    start = value;
                }
            }
        }

        class GridEntry
        {
            RectangleF box = new RectangleF();
            List<GridLine> lines = new List<GridLine>();

            public GridEntry(RectangleF boud, int index, float height)
            {
                box.X = boud.X;
                box.Width = boud.Width;
                box.Height = height;

                box.Y = boud.Y + height * index;                                
            }

            public RectangleF Box
            {
                get
                {
                    return box;
                }
            }

            public List<GridLine> Lines
            {
                get
                {
                    return lines;
                }
            }

            public void TestPolygon(PointF[] pts)
            {
                PointF start = pts[0];

                for (int i = 1; i < pts.Length; i++)
                {
                    PointF end = pts[i];

                    if (VectorMath.LineInsideRect(box, start.X, start.Y, end.X, end.Y))
                    {
                        GridLine gl = new GridLine();

                        gl.Start = start;
                        gl.End = end;

                        lines.Add(gl);
                    }

                    start = end;                    
                }
            }
        }

        int gridHeight = 10;
        GridEntry[] grid = null;

        public void BuildGrid()
        {
            int gridCount = (int)boundRect.Height / gridHeight + 1;
            grid = new GridEntry[gridCount];

            if (polygons == null)
                BuildPolygons();

            for (int i = 0; i < gridCount; i++)
            {
                grid[i] = new GridEntry(boundRect, i, gridHeight);

                foreach (PointF[] poly in polygons)
                {
                    grid[i].TestPolygon(poly);
                }
            }
        }

        public void ClosePath()
        {
            Normalize();
            BuildPolygons();

            BuildGrid();
            BuildMap();
            ComputeArea();

            CalcLimits();

            document.Host.Invalidate();
        }

        float startX = 0;
        float startY = 0;
        float curX = 0;
        float curY = 0;

        void AddSegment(VectorSegment seg)
        {
            segments.Add(seg);
        }

        public void LineTo(float x, float y)
        {
            VectorLineSegment line = new VectorLineSegment();

            line.Start = new PointF(curX, curY);
            line.End = new PointF(x, y);

            curX = x;
            curY = y;

            AddSegment(line);
        }

        public void MoveTo(float x, float y)
        {
            VectorMoveSegment move = new VectorMoveSegment();

            move.Start = new PointF(curX, curY);
            move.End = new PointF(x, y);

            curX = x;
            curY = y;

            startX = x;
            startY = y;

            AddSegment(move);
        }

        public void CurveTo(float x, float y, float c1x, float c1y, float c2x, float c2y)
        {
            VectorCubicSegment c = new VectorCubicSegment();

            c.Start = new PointF(curX, curY);
            c.End = new PointF(x, y);

            curX = x;
            curY = y;

            c.Control1 = new PointF(c1x, c1y);
            c.Control2 = new PointF(c2x, c2y);

            AddSegment(c);
        }

        public void QCurveTo(float x, float y, float cx, float cy)
        {
            VectorQuadraticSegment c = new VectorQuadraticSegment();

            c.Start = new PointF(curX, curY);
            c.End = new PointF(x, y);

            curX = x;
            curY = y;

            c.Control = new PointF(cx, cy);

            AddSegment(c);
        }

        Pen linePen = null;

        public void RenderPolygons(Graphics g)
        {
            if (testPointInside)
            {
                linePen = document.hilightLine;
            }
            else
            {
                linePen = document.normalLine;
            }

            if (IsValidPos)
            {
                linePen = document.errorLine;
            }
            else if (selected)
            {
                linePen = document.selectedLine;
            }

            foreach (PointF[] pts in polygons)
            {
                if (document.Debbuging && fillOnPointInside && testPointInside)
                    g.FillPolygon(Brushes.LightBlue, pts);

                g.DrawPolygon(linePen, pts);
            }
        }

        byte[] map = null;
        int mapW = 0, mapH = 0;
        public void BuildMap()
        {
            Bitmap bmp = CreateBitmap();

            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            map = new byte[bmp.Width * bmp.Height];

            mapW = bmp.Width;
            mapH = bmp.Height;
          
            unsafe
            {
                int* p = (int*)bd.Scan0.ToPointer();

                for (int i = 0; i < bd.Height; i++)
                {
                    for (int j = 0; j < bd.Width; j++)
                    {
                        int idx = i * bmp.Width + j;
                        if (*p != 0)                        
                            map[idx] = 1;
                        else       
                            map[idx] = 0;                       

                        p++;
                    }
                }
            }

            bmp.UnlockBits(bd);
        }

        public void ComputeArea()
        {
            if (map == null)
                BuildMap();

            area = 0;

            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] != 0)
                    area++;
            }
        }

        SolidBrush fillBrush = null;        
        public Bitmap CreateBitmap()
        {
            Bitmap b = new Bitmap((int)boundRect.Width + 1, (int)boundRect.Height + 1, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);

            g.SmoothingMode = SmoothingMode.HighQuality;

            RenderSample(g, -boundRect.X, -boundRect.Y);

            return b;
        }

        public void RenderSample(Graphics g, float x, float y, float angle, float scale)
        {
            g.ResetTransform();
            g.Clear(Color.FromArgb(0));

            if (segments.Count > 0 && polygons.Count == 0)
                BuildPolygons();

            g.ScaleTransform(scale, scale);
            g.TranslateTransform(x, y);
            g.RotateTransform(angle);            

            if (fillBrush == null)
                fillBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

            foreach (PointF[] pts in polygons)
            {
                g.FillPolygon(fillBrush, pts);
            }
        }

        public void RenderSample(Graphics g, float x, float y)
        {
            g.ResetTransform();
            g.Clear(Color.FromArgb(0));

            if (segments.Count > 0 && polygons.Count == 0)
                BuildPolygons();

            g.TranslateTransform(x, y);

            if (fillBrush == null)
                fillBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

            foreach (PointF[] pts in polygons)
            {
                g.FillPolygon(fillBrush, pts);
            }
        }

        internal void ImportPath(VectorPath p)
        {
            segments.Clear();
            polygons = null;

            BeginPath();

            foreach (VectorSegment s in p.segments)
            {
                if (s is VectorMoveSegment)
                    MoveTo(s.End.X, s.End.Y);

                if (s is VectorLineSegment)
                    LineTo(s.End.X, s.End.Y);

                if (s is VectorCubicSegment)
                {
                    VectorCubicSegment c = (VectorCubicSegment)s;
                    CurveTo(c.End.X, c.End.Y, c.Control1.X, c.Control1.Y, c.Control2.X, c.Control2.Y);
                }

                if (s is VectorQuadraticSegment)
                {
                    VectorQuadraticSegment q = (VectorQuadraticSegment)s;
                    QCurveTo(q.End.X, q.End.Y, q.Control.X, q.Control.Y);
                }

                if (s is VectorCloseSegment)
                    ClosePolygon();
            }

            ClosePath();

            posx = p.posx + 100;
            posy = p.posy + 100;
            id = p.Id;
        }

        public PointF[] GetBoundPoints()
        {
            RectangleF r = BoundRect;

            PointF[] ret = new PointF[4];

            ret[0] = new PointF(r.X, r.Y);
            ret[1] = new PointF(r.Right, r.Y);
            ret[2] = new PointF(r.Right, r.Bottom);
            ret[3] = new PointF(r.X, r.Bottom);
            
            Matrix mt = new Matrix();

            mt.Rotate(angle);                        
            mt.TransformPoints(ret);

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i].X += posx;
                ret[i].Y += posy;
            }

            return ret;
        }

        public bool IsBoundInside(VectorPath path)
        {
            PointF[] bp = GetBoundPoints();

            foreach (PointF p in bp)
            {
                if (path.IsPointInside(p, true))
                    return true;
            }

            if (path.IsPointInside(new PointF(posx, posy), true))
                return true;

            if (IsPointInside(new PointF(path.posx, path.posy), true))
                return true;

            return false;
        }

        public void CheckDocumentLimits()
        {
            //CalcLimits();

            outOfLimits = false;

            RectangleF lim = new RectangleF(limits.X, limits.Y, limits.Width, limits.Height);

            lim.X += posx;
            lim.Y += posy;

            if (lim.X <= 0 || lim.Y <= 0)
                outOfLimits = true;

            if (lim.Bottom >= document.DocHeight)
                outOfLimits = true;
        }

        public bool Intersects(VectorPath path)
        {
            if (path == this)
                return false;

            if (polygons == null)
                BuildPolygons();

            intersectList.Remove(path);
            path.intersectList.Remove(this);

            if (IsBoundInside(path))
            {
                List<PointF[]> tpoly = GetTransfomedPolygons();
                foreach (PointF[] pts in tpoly)
                {
                    foreach (PointF p in pts)
                    {
                        if (path.IsPointInside(p, false))
                        {
                            intersectList.Add(path);
                            path.intersectList.Add(this);

                            return true;
                        }
                    }
                }

                tpoly = path.GetTransfomedPolygons();
                foreach (PointF[] pts in tpoly)
                {
                    foreach (PointF p in pts)
                    {
                        if (IsPointInside(p, false))
                        {
                            intersectList.Add(path);
                            path.intersectList.Add(this);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        const int HPGL_UNIT = 40; // 0.025 mm
        Point GetHPGLPoint(PointF p)
        {
            Point ret = new Point();

            ret.X = (int)Math.Round(p.X * HPGL_UNIT);
            ret.Y = (int)Math.Round(p.Y * HPGL_UNIT);

            return ret;
        }

        VectorSegment lastSegment = null;

        bool IsSamAsLast(VectorSegment seg)
        {
            if (lastSegment == null)
                return false;

            if (lastSegment is VectorMoveSegment && seg is VectorLineSegment)
                return true;

            return lastSegment.GetType().Equals(seg.GetType());
        }

        void WriteSvgSegment(StringBuilder sb, VectorSegment seg, float ppmx, float ppmy)
        {
            int sx, sy, ex, ey;

            Matrix mt = new Matrix();
            mt.Translate(posx, posy);
            mt.Rotate(angle);

            PointF[] pts = new PointF[] { seg.Start, seg.End };

            mt.TransformPoints(pts);

            sx = (int)Math.Round(pts[0].X * ppmx);
            sy = (int)Math.Round(pts[0].Y * ppmy);
            ex = (int)Math.Round(pts[1].X * ppmx);
            ey = (int)Math.Round(pts[1].Y * ppmy);

            if (seg is VectorMoveSegment)
            {
                sb.Append(" M");
                sb.AppendFormat("{0},{1}", ex, ey);
            }

            if (seg is VectorLineSegment)
            {
                if (!IsSamAsLast(seg))
                    sb.Append(" L");
                else
                    sb.Append(" ");

                sb.AppendFormat("{0},{1}", ex, ey);
            }

            if (seg is VectorCubicSegment)
            {
                if (!IsSamAsLast(seg))
                    sb.Append(" C");
                else
                    sb.Append(" ");

                VectorCubicSegment vc = (VectorCubicSegment)seg;
                int c1x, c1y, c2x, c2y;

                pts = new PointF[] { vc.Control1, vc.Control2 };

                mt.TransformPoints(pts);

                c1x = (int)Math.Round(pts[0].X * ppmx);
                c1y = (int)Math.Round(pts[0].Y * ppmy);
                c2x = (int)Math.Round(pts[1].X * ppmx);
                c2y = (int)Math.Round(pts[1].Y * ppmy);

                sb.AppendFormat("{0},{1} {2},{3} {4},{5}", c1x, c1y, c2x, c2y, ex, ey);
            }

            if (seg is VectorQuadraticSegment)
            {
                if (!IsSamAsLast(seg))
                    sb.Append(" Q");
                else
                    sb.Append(" ");

                VectorQuadraticSegment qc = (VectorQuadraticSegment)seg;
                int cx, cy;

                pts = new PointF[] { qc.Control };

                cx = (int)Math.Round(pts[0].X * ppmx);
                cy = (int)Math.Round(pts[0].Y * ppmy);

                sb.AppendFormat("{0},{1} {2},{3}", cx, cy, ex, ey);
            }

            if (seg is VectorCloseSegment)
            {
                sb.Append(" Z");
            }

            lastSegment = seg;
        }

        public string ToSVGPath(float ppmx, float ppmy)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytes = Encoding.UTF8.GetBytes(tag);
            string b64Tag = Convert.ToBase64String(bytes);

            sb.AppendFormat("<path gf-side=\"{0}\" gf-tag=\"{1}\" style=\"fill: none; stroke:#e30016;stroke-width:1\" \n\td=\"", side.ToString().ToLower(), b64Tag);

            foreach (VectorSegment e in segments)
            {
                WriteSvgSegment(sb, e, ppmx, ppmy);
            }

            sb.Append("\" />\n\t");
            return sb.ToString();
        }

        public string ToHPGL()
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            bool firstPoint = true;
            PointF hp;

            List<PointF[]> polyList = GetTransfomedPolygons();

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

            return sb.ToString();
        }

        public void GenerateCommands(PlotterDriver driver, bool invertXY, bool flip, float flipCenter)
        {
            List<PointF[]> polyList = GetTransfomedPolygons();

            if (invertXY || flip)
            {
                foreach (PointF[] pts in polyList)
                {
                    for (int i = 0; i < pts.Length; i++)
                    {
                        if (flip)
                        {
                            pts[i].Y = flipCenter + (flipCenter - pts[i].Y);
                        }

                        if (invertXY)
                        {
                            float tmp = pts[i].X;
                            pts[i].X = pts[i].Y;
                            pts[i].Y = tmp;
                        }
                    }
                }
            }

            driver.AddPolygon(polyList);
        }

        public void Render(Graphics g)
        {
            g.ResetTransform();

            if (segments.Count > 0 && polygons.Count == 0)
                BuildPolygons();

            g.TranslateTransform(document.OffsetX, document.OffsetY);

            float s;

            s = document.Scale;

            if (s < 0.04f)
                s = 0.04f;

            g.ScaleTransform(s, s);
            g.TranslateTransform(posx, posy);
            g.RotateTransform(angle);

            RenderPolygons(g);

            if (document.Debbuging)
            {
                Pen p = new Pen(Color.LightGreen);
                p.Width = 0.01f;
                p.DashStyle = DashStyle.DashDotDot;

                if (DrawBoundBox)
                    g.DrawRectangle(p, boundRect.X, boundRect.Y, boundRect.Width, boundRect.Height);

                if (drawCenterPoint)
                    document.DrawCircle(g, new PointF(0, 0), 3, Color.DarkGreen);

                if (drawCurGrid && curGrid != null)
                {
                    p.Color = Color.OrangeRed;
                    p.DashStyle = DashStyle.Solid;
                    g.DrawRectangle(p, curGrid.Box.X, curGrid.Box.Y, curGrid.Box.Width, curGrid.Box.Height);

                    foreach (GridLine gl in curGrid.Lines)
                    {
                        Pen pl = new Pen(Color.DarkOliveGreen);
                        pl.Width = 1;

                        g.DrawLine(pl, gl.Start, gl.End);
                    }
                }

                if (drawNormalizedPath)
                {
                    g.ResetTransform();
                    RenderPolygons(g);
                }

                if (drawTestPoint)
                {
                    g.ResetTransform();
                    document.DrawCircle(g, testPoint, 3, Color.OrangeRed);
                }
            }
        }
    }
}
