using System.Collections.Generic;
using System.Drawing;

namespace VectorView
{
    public class VectorCutSheet
    {
        float height = 1520;
        float ox, oy;
        float scale = 1;

        List<CuttingEntry> paths = new List<CuttingEntry>();
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
                return ox;
            }

            set
            {
                ox = value;
            }
        }

        public float Y
        {
            get
            {
                return oy;
            }

            set
            {
                oy = value;
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

        CuttingEntry FindEntryByPath(VectorPath vp)
        {
            foreach (CuttingEntry e in paths)
            {
                if (e.Path == vp)
                    return e;
            }

            return null;
        }

        void FindBestPos(VectorPath vp, out float x, out float y, out float angle)
        {
            x = ox;
            y = oy;
            angle = 0;
        }

        public void AddPath(VectorPath p)
        {
            CuttingEntry se = new CuttingEntry(p);

            paths.Add(se);

            p.InCutSheet = true;
            p.BuildOriginalPolygons();

            se.MakeHull();

            PointF m = p.GetMiddlePoint();            

            p.BeginTransform(m);
            p.Scale(scale, scale, m);

            m = p.GetMiddlePoint();
            RectangleF r = p.GetBoundRect();

            float x, y, a;

            FindBestPos(p, out x, out y, out a);

            p.SetOrigin(new PointF(x, y));
        }

        public void SetScale(float scale)
        {
            foreach (CuttingEntry e in paths)
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
