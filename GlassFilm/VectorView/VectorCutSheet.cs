using System.Collections.Generic;
using System.Drawing;

namespace VectorView
{
    public class VectorCutSheet
    {
        class SheetEntry
        {
            VectorPath path = null;

            PointF origin = new PointF();
            float angle = 0;

            ConvexHull convexHull = null;

            public PointF Origin
            {
                get
                {
                    return origin;
                }

                set
                {
                    origin = value;
                }
            }

            public float Angle
            {
                get
                {
                    return angle;
                }

                set
                {
                    angle = value;
                }
            }

            public VectorPath Path
            {
                get
                {
                    return path;
                }
                
            }

            public SheetEntry(VectorPath p)
            {
                path = p;
            }

            public void MakeHull()
            {
                convexHull = new ConvexHull();

                List<PointF> pl = new List<PointF>();

                foreach (PointF[] pts in path.OriginalPoligons)
                {
                    pl.AddRange(pts);
                }

                convexHull.MakeConvexHull(pl);
            }

            public void ApplyTransform()
            {
                PointF o = new PointF(0, 0);

                path.SetOrigin(o);
                path.Rotate(angle, o);
                path.SetOrigin(origin);

                convexHull.SetCenter(0, 0);
                convexHull.Rotate(angle);
                convexHull.SetCenter(origin.X, origin.Y);
            }
        }

        float height = 1520;
        float x, y;
        float scale = 1;

        List<SheetEntry> paths = new List<SheetEntry>();
        VectorDocument document = null;

        public VectorDocument Document
        {
            get
            {
                return document;
            }
        }

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
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

        internal VectorCutSheet(VectorDocument doc)
        {
            document = doc;
        }       

        public void AddPath(VectorPath p)
        {
            SheetEntry se = new SheetEntry(p);

            paths.Add(se);

            p.InCutSheet = true;
            p.BuildOriginalPolygons();

            se.MakeHull();

            PointF m = p.GetMiddlePoint();            

            p.BeginTransform(m);
            p.Scale(scale, scale, m);

            m = p.GetMiddlePoint();
            RectangleF r = p.GetBoundRect();

            p.SetOrigin(new PointF(x,y));
        }

        public void SetScale(float scale)
        {
            foreach (SheetEntry e in paths)
            {
                PointF m = e.Path.GetMiddlePoint();
                e.Path.CloneSource();

                e.Path.BeginTransform(m);
                e.Path.Scale(scale, scale, m);
                e.Path.SetOrigin(e.Origin);
                e.Path.Rotate(e.Angle, e.Origin);
            }
        }
    }
}
