using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows;

namespace VectorView
{
    public class VectorMath
    {
        const float Rad2Deg = (float)(180.0 / Math.PI);
        const float Deg2Rad = (float)(Math.PI / 180.0);

        public static PointF GetBoxCenter(RectangleF box)
        {
            if (box.Width == 0 || box.Height == 0)
                return new PointF(0, 0);

            return new PointF(box.X + box.Width / 2, box.Y + box.Height / 2);
        }

        public static float DegreeToRadian(double angle)
        {
            return (float)(Math.PI * angle / 180.0);
        }

        public static float RadianToDegree(double angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }

        public static float AngleBetween(PointF origin, PointF target)
        {
            Vector t = new Vector(target.X, target.Y);
            Vector o = new Vector(origin.X, origin.Y);

            return (float)Vector.AngleBetween(new Vector(1, 0), t - o);
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

        public static bool CrossPoint(float hline, PointF a, PointF b, out float x, out float y)
        {
            y = hline;
            x = (b.Y - a.Y);

            if (x == 0)
                return false;

            float r = (b.Y - hline) / x;

            if (r >= 0 && r < 1)
            {
                x = b.X + ((a.X - b.X) * r);
                return true;
            }

            return false;
        }

        public static float GetArea(RectangleF r)
        {
            return r.Width * r.Height;
        }

    }
}
