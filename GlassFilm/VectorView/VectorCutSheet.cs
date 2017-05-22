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
        }
    }
}
