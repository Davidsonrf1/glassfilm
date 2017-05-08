using System;
using System.Collections.Generic;
using System.Drawing;

namespace VectorView
{
    public class Geometry
    {
        PointF[] minMaxCorners;
        RectangleF minMaxBox;
        PointF[] nonCulledPoints;

        public PointF[] MinMaxCorners
        {
            get
            {
                return minMaxCorners;
            }            
        }

        public RectangleF MinMaxBox
        {
            get
            {
                return minMaxBox;
            }
        }

        public PointF[] NonCulledPoints
        {
            get
            {
                return nonCulledPoints;
            }
        }

        void GetMinMaxCorners(List<PointF> points, ref PointF ul, ref PointF ur, ref PointF ll, ref PointF lr)
        {
            ul = points[0];
            ur = ul;
            ll = ul;
            lr = ul;

            foreach (PointF pt in points)
            {
                if (-pt.X - pt.Y > -ul.X - ul.Y) ul = pt;
                if (pt.X - pt.Y > ur.X - ur.Y) ur = pt;
                if (-pt.X + pt.Y > -ll.X + ll.Y) ll = pt;
                if (pt.X + pt.Y > lr.X + lr.Y) lr = pt;
            }

            minMaxCorners = new PointF[] { ul, ur, lr, ll }; 
        }

        private RectangleF GetMinMaxBox(List<PointF> points)
        {
            PointF ul = new PointF(0, 0), ur = ul, ll = ul, lr = ul;
            GetMinMaxCorners(points, ref ul, ref ur, ref ll, ref lr);

            float xmin, xmax, ymin, ymax;
            xmin = ul.X;
            ymin = ul.Y;

            xmax = ur.X;
            if (ymin < ur.Y) ymin = ur.Y;

            if (xmax > lr.X) xmax = lr.X;
            ymax = lr.Y;

            if (xmin < ll.X) xmin = ll.X;
            if (ymax > ll.Y) ymax = ll.Y;

            RectangleF result = new RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
            minMaxBox = result;
            return result;
        }

        private List<PointF> HullCull(List<PointF> points)
        {
            RectangleF culling_box = GetMinMaxBox(points);

            List<PointF> results = new List<PointF>();
            foreach (PointF pt in points)
            {
                if (pt.X <= culling_box.Left ||
                    pt.X >= culling_box.Right ||
                    pt.Y <= culling_box.Top ||
                    pt.Y >= culling_box.Bottom)
                {
                    results.Add(pt);
                }
            }

            nonCulledPoints = new PointF[results.Count];  
            results.CopyTo(nonCulledPoints);             
            return results;
        }

        public List<PointF> MakeConvexHull(List<PointF> points)
        {
            points = HullCull(points);

            PointF best_pt = points[0];
            foreach (PointF pt in points)
            {
                if ((pt.Y < best_pt.Y) ||
                   ((pt.Y == best_pt.Y) && (pt.X < best_pt.X)))
                {
                    best_pt = pt;
                }
            }

            List<PointF> hull = new List<PointF>();
            hull.Add(best_pt);
            points.Remove(best_pt);

            float sweep_angle = 0;
            for (;;)
            {
                float X = hull[hull.Count - 1].X;
                float Y = hull[hull.Count - 1].Y;
                best_pt = points[0];
                float best_angle = 3600;

                foreach (PointF pt in points)
                {
                    float test_angle = AngleValue(X, Y, pt.X, pt.Y);
                    if ((test_angle >= sweep_angle) &&
                        (best_angle > test_angle))
                    {
                        best_angle = test_angle;
                        best_pt = pt;
                    }
                }

                float first_angle = AngleValue(X, Y, hull[0].X, hull[0].Y);
                if ((first_angle >= sweep_angle) &&
                    (best_angle >= first_angle))
                {
                    break;
                }

                hull.Add(best_pt);
                points.Remove(best_pt);

                sweep_angle = best_angle;

                if (points.Count == 0) break;
            }

            return hull;
        }

        private float AngleValue(float x1, float y1, float x2, float y2)
        {
            float dx, dy, ax, ay, t;

            dx = x2 - x1;
            ax = Math.Abs(dx);
            dy = y2 - y1;
            ay = Math.Abs(dy);
            if (ax + ay == 0)
            {
                t = 360f / 9f;
            }
            else
            {
                t = dy / (ax + ay);
            }
            if (dx < 0)
            {
                t = 2 - t;
            }
            else if (dy < 0)
            {
                t = 4 + t;
            }
            return t * 90;
        }
    }

}
