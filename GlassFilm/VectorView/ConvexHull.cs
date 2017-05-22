using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorView
{
    public class ConvexHull
    {
        PointF[] minMaxCorners;
        RectangleF minMaxBox;
        PointF[] nonCulledPoints;

        PointF[] convexPoints = null;
        float[] angles = null;

        PointF testPoint;
        PointF nearLeft;
        PointF nearRight;
        float angle = 0;
        float minWidthAngle = 0;

        bool hasPointInside = false;

        PointF center = new PointF();
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

        public PointF Center
        {
            get
            {
                return center;
            }

            set
            {
                center = value;
            }
        }

        public PointF[] ConvexPoints
        {
            get
            {
                return convexPoints;
            }
        }

        public PointF TestPoint
        {
            get
            {
                return testPoint;
            }
        }

        public PointF NearLeft
        {
            get
            {
                return nearLeft;
            }
        }

        public PointF NearRight
        {
            get
            {
                return nearRight;
            }
        }

        public bool HasPointInside
        {
            get
            {
                return hasPointInside;
            }
        }

        public float Angle
        {
            get
            {
                return angle;
            }
        }

        public float MinWidthAngle
        {
            get
            {
                return minWidthAngle;
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

        RectangleF GetBounds(PointF[] pts)
        {
            float xmin, xmax, ymin, ymax;

            xmin = float.MaxValue;
            ymin = float.MaxValue;
            xmax = float.MinValue;
            ymax = float.MinValue;

            foreach (PointF p in pts)
            {
                xmin = Math.Min(p.X, xmin);
                ymin = Math.Min(p.Y, ymin);
                xmax = Math.Max(p.X, xmax);
                ymax = Math.Max(p.Y, ymax);
            }

            return new RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        private List<PointF> HullCull(List<PointF> points)
        {
            RectangleF cb = GetMinMaxBox(points);

            List<PointF> results = new List<PointF>();
            foreach (PointF pt in points)
            {
                if (pt.X <= cb.Left || pt.X >= cb.Right || pt.Y <= cb.Top || pt.Y >= cb.Bottom)
                    results.Add(pt);
            }

            nonCulledPoints = new PointF[results.Count];
            results.CopyTo(nonCulledPoints);
            return results;
        }

        class PointAngleComparer : IComparer<PointF>
        {
            PointF center = new PointF();

            internal PointAngleComparer(PointF center)
            {
                this.center.X = center.X;
                this.center.Y = center.Y;
            }

            public int Compare(PointF x, PointF y)
            {
                float av1 = AngleValue(center.X, center.Y, x.X, x.Y);
                float av2 = AngleValue(center.X, center.Y, y.X, y.Y);

                return av1 > av2 ? 1 : av1 == av2 ? 0 : -1;
            }
        }

        void UpdateAngles()
        {
            angles = new float[convexPoints.Length];

            for (int i = 0; i < convexPoints.Length; i++)
            {
                angles[i] = AngleValue(center.X, center.Y, convexPoints[i].X, convexPoints[i].Y);
            }
        }

        public List<PointF> MakeConvexHull(List<PointF> points)
        {
            points = HullCull(points);

            PointF bestPoint = points[0];
            foreach (PointF pt in points)
            {
                if ((pt.Y < bestPoint.Y) || ((pt.Y == bestPoint.Y) && (pt.X < bestPoint.X)))
                    bestPoint = pt;
            }

            List<PointF> hull = new List<PointF>();
            hull.Add(bestPoint);
            points.Remove(bestPoint);

            float sweepAngle = 0;
            for (;;)
            {
                float X = hull[hull.Count - 1].X;
                float Y = hull[hull.Count - 1].Y;
                bestPoint = points[0];
                float best_angle = 3600;

                foreach (PointF pt in points)
                {
                    float testAngle = AngleValue(X, Y, pt.X, pt.Y);
                    if ((testAngle >= sweepAngle) && (best_angle > testAngle))
                    {
                        best_angle = testAngle;
                        bestPoint = pt;
                    }
                }

                float firstAngle = AngleValue(X, Y, hull[0].X, hull[0].Y);
                if ((firstAngle >= sweepAngle) && (best_angle >= firstAngle))
                {
                    break;
                }

                hull.Add(bestPoint);
                points.Remove(bestPoint);

                sweepAngle = best_angle;

                if (points.Count == 0) break;
            }

            convexPoints = hull.ToArray();

            RectangleF r = GetBounds(convexPoints);
            center.X = r.X + r.Width / 2;
            center.Y = r.Y + r.Height / 2;

            Array.Sort(convexPoints, new PointAngleComparer(center));

            UpdateAngles();

            return hull;
        }

        public void GetNearPoints(PointF pt, out PointF p1, out PointF p2)
        {
            float angle = AngleValue(center.X, center.Y, pt.X, pt.Y);

            p1 = new PointF(float.MinValue, float.MinValue);
            p2 = p1;

            for (int i = 0; i < angles.Length; i++)
            {
                if (angles[i] >= angle)
                {
                    if (angles[i] == angle)
                    {
                        p1 = convexPoints[i];
                        p2 = convexPoints[i];
                    }
                    else
                    {
                        if (i == 0)
                        {
                            p1 = convexPoints[convexPoints.Length - 1];
                            p2 = convexPoints[0];
                        }
                        else
                        {
                            p1 = convexPoints[i - 1];
                            p2 = convexPoints[i];
                        }
                    }
                    break;
                }
            }

            if (p1.X == float.MinValue)
            {
                p1 = convexPoints[convexPoints.Length - 1];
                p2 = convexPoints[0];
            }
        }

        float PlaneSign(PointF p1, PointF p2, PointF p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        bool PointInTriangle(PointF pt, PointF v1, PointF v2, PointF v3)
        {
            bool b1, b2, b3;

            b1 = PlaneSign(pt, v1, v2) < 0.0f;
            b2 = PlaneSign(pt, v2, v3) < 0.0f;
            b3 = PlaneSign(pt, v3, v1) < 0.0f;

            return ((b1 == b2) && (b2 == b3));
        }

        public bool IsPointInside(PointF pt)
        {
            testPoint.X = pt.X;
            testPoint.Y = pt.Y;

            PointF p1, p2;
            GetNearPoints(pt, out p1, out p2);

            nearLeft.X = p1.X;
            nearLeft.Y = p1.Y;
            nearRight.X = p2.X;
            nearRight.Y = p2.Y;

            hasPointInside = PointInTriangle(pt, center, p1, p2);

            return hasPointInside;
        }

        public static float AngleValue(float x1, float y1, float x2, float y2)
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

        public void Rotate(float angle)
        {
            Matrix mt = new Matrix();

            mt.RotateAt(angle, center);
            mt.TransformPoints(convexPoints);

            Array.Sort(convexPoints, new PointAngleComparer(center));
            UpdateAngles();

            this.angle = angle;
        }

        public void Scale(float sx, float sy)
        {
            float ox = center.X;
            float oy = center.Y;

            SetCenter(ox, oy);

            Matrix mt = new Matrix();

            mt.Scale(sx, sy);
            mt.TransformPoints(convexPoints);

            RectangleF r = GetBounds(convexPoints);
            center.X = r.X + r.Width / 2;
            center.Y = r.Y + r.Height / 2;

            SetCenter(ox, oy);

            UpdateAngles();
        }

        public void SetCenter(float x, float y)
        {
            float dx = x - center.X;
            float dy = y - center.Y;

            Matrix mt = new Matrix();
            mt.Translate(dx, dy);

            mt.TransformPoints(convexPoints);

            center.X = x;
            center.Y = y;
        }

        public float ComputeMinWidthAngle()
        {
            float oldAngle = angle;
            float minw = float.MaxValue;

            for (int i = 0; i < 360; i++)
            {
                Rotate(i);

                RectangleF r = GetBounds(convexPoints);
                minw = Math.Min(r.Width, minw);

                if (r.Width <= minw)
                {
                    minWidthAngle = i;
                    minw = r.Width;
                }
            }

            return minWidthAngle;
        }
    }
}
