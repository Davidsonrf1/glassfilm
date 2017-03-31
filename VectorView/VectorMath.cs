using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorView
{
    public class VectorMath
    {
        const float Rad2Deg = (float)(180.0 / Math.PI);
        const float Deg2Rad = (float)(Math.PI / 180.0);

        public static float PointAngle(float x1, float y1, float x2, float y2)
        {
            return -(float)(Math.Atan2(y1 - y2, x1 - x2) * Rad2Deg);
        }

        public static PointF InterpolateLine(PointF p1, PointF p2, float ratio)
        {
            return new PointF(p1.X + ((p2.X - p1.X) * ratio), p1.Y + ((p2.Y - p1.Y) * ratio));
        }

        public static PointF InterpolateLine(float x1, float y1, float x2, float y2, float ratio)
        {
            return new PointF(x1 + ((x2 - x1) * ratio), y1 + ((y2 - y1) * ratio));
        }

        public static float PointDistance(PointF p1, PointF p2)
        {
            float d1 = p1.X - p2.X;
            float d2 = p1.Y - p2.Y;

            return (float)Math.Sqrt(d1 * d1 + d2 * d2);
        }

        public static float PointDistance(float x1, float y1, float x2, float y2)
        {
            float d1 = x1 - x2;
            float d2 = y1 - y2;

            return (float)Math.Sqrt(d1 * d1 + d2 * d2);
        }

        public static float PointToLineDistance(PointF point, PointF start, PointF end)
        {
            return PointToLineDistance(point.X, point.Y, start.X, start.Y, end.X, end.Y);
        }

        public static float PointToLineDistance(float px, float py, float x1, float y1, float x2, float y2)
        {
            return PointDistance(px, py, x2, y2) + PointDistance(px, py, x1, y1) - PointDistance(x1, y1, x2, y2);
        }

        public static bool HorizontalCrossPoint(float x1, float y1, float x2, float y2, float hline, out PointF point)
        {
            point = new PointF();

            if (y1 == y2 && y2 == hline)
            {
                point.X = x1;
                point.Y = y1;

                return true;
            }

            float maxy, miny, maxx, minx;

            maxy = Math.Max(y1, y2);
            miny = Math.Min(y1, y2);
            maxx = Math.Max(x1, x2);
            minx = Math.Min(x1, x2);

            if (hline >= miny && hline <= maxy)
            {
                if (x1 == x2)
                {
                    point.X = x1;
                    point.Y = hline;

                    return true;
                }

                float dx = (maxx - minx) / (maxy - miny);

                point.Y = hline;

                //if (y2 - y1 >= 0)
                    point.X = maxx - (hline - miny) * dx;
                //else
                  //  point.X = maxx - (hline - miny) * dx;

                return true;
            }

            return false;
        }
    }
}
