using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class CuttingEntry
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

        public CuttingEntry(VectorPath p)
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
}
