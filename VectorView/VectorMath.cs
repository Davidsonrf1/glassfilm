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

                float dx = (x2 - x1) / (maxy - miny);

                point.Y = hline;
                point.X = x1 + (hline - miny) * dx;

                return true;
            }

            return false;
        }
    }
}
