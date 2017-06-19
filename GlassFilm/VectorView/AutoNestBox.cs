using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView
{
    public class AutoNestBox
    {
        RectangleF nestBox = new RectangleF(0, 0, 1500, 200);

        float tileSize = 20;
        float halfTile = 10;

        class Tile
        {

        }

        class NestEntry
        {
            public float x = 0, y = 0;
            public float angle = 0;
            public ConvexHull polygon = null;
        }

        List<ConvexHull> polygons = new List<ConvexHull>();
        List<NestEntry> nest = new List<NestEntry>();

        public float X
        {
            get
            {
                return nestBox.X;
            }

            set
            {
                nestBox.X = value;
            }
        }

        public float Y
        {
            get
            {
                return nestBox.Y;
            }

            set
            {
                nestBox.Y = value;
            }
        }

        public float Size
        {
            get
            {
                return nestBox.Height;
            }

            set
            {
                nestBox.Height = value;
            }
        }

        public float TileSize
        {
            get
            {
                return tileSize;
            }

            set
            {
                tileSize = value; halfTile = value / 2;
            }
        }

        public float HalfTile
        {
            get
            {
                return halfTile;
            }
        }

        public void ClearNest()
        {
            nest.Clear();
        }

        public void AddPolygon(ConvexHull cv)
        {
            polygons.Add(cv);
        }

        public void ClearIntersect()
        {
            foreach (ConvexHull cv in polygons)
            {
                cv.IsIntersecting = false;
            }
        }

        public bool CheckConstraits(ConvexHull cv)
        {
            cv.IsOutside = false;
            cv.IsIntersecting = false;

            foreach (ConvexHull c in polygons)
            {
                if (c.Intersecting(cv))
                {
                    cv.IsIntersecting = true;
                    break;
                }
            }

            RectangleF rect = cv.GetBounds();

            if (rect.X < nestBox.X || rect.Y < nestBox.Y)
                cv.IsOutside = true;

            if (rect.Bottom > nestBox.Bottom)
                cv.IsOutside = true;

            return cv.IsIntersecting || cv.IsOutside;
        }

        public void CheckAllConstraints()
        {
            ClearIntersect();

            List<ConvexHull> t = new List<ConvexHull>();
            t.AddRange(polygons);

            while (t.Count > 0)
            {
                ConvexHull c1 = t[0];
                t.RemoveAt(0);

                for (int i = 0; i < t.Count; i++)
                {
                    ConvexHull c2 = t[i];

                    if (c1.Intersecting(c2))
                    {
                        c1.IsIntersecting = true;
                        c2.IsIntersecting = true;
                    }
                }
            }
        }

        public RectangleF GetNestBounds()
        {
            float xmin, xmax, ymin, ymax;

            xmin = float.MaxValue;
            ymin = float.MaxValue;
            xmax = float.MinValue;
            ymax = float.MinValue;

            foreach (NestEntry n in nest)
            {
                ConvexHull cv = n.polygon;

                RectangleF r = cv.GetBounds();

                xmin = Math.Min(r.X, xmin);
                ymin = Math.Min(r.Y, ymin);
                xmax = Math.Max(r.Right, xmax);
                ymax = Math.Max(r.Bottom, ymax);
            }

            return new RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        public void Reder(Graphics g)
        {
            foreach (ConvexHull cv in polygons)
            {
                cv.Render(g);

                RectangleF r = cv.GetBounds();
                g.DrawRectangle(Pens.Blue, r.X, r.Y, r.Width, r.Height);
            }

            g.DrawRectangle(Pens.Green, nestBox.X, nestBox.Y, nestBox.Width, nestBox.Height);
        }
    }
}
