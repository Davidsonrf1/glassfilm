using System;
using System.Collections.Generic;
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

        public static bool LineInsideRect(RectangleF rect, float x1, float y1, float x2, float y2)
        {
            if (PointInsideBox(x1, y1, rect) || PointInsideBox(x1, y1, rect))
                return true;

            if (HorizontalLineCross(rect.Top, x1, y1, x2, y2))
                return true;

            if (HorizontalLineCross(rect.Bottom, x1, y1, x2, y2))
                return true;

            if (VerticalLineCross(rect.Left, x1, y1, x2, y2))
                return true;

            if (VerticalLineCross(rect.Right, x1, y1, x2, y2))
                return true;

            return false;
        }

        public static bool HorizontalLineCross(float hline, float x1, float y1, float x2, float y2, out PointF pt)
        {
            float mx1, my1, mx2, my2;

            mx1 = Math.Min(x1, x2);
            my1 = Math.Min(y1, y2);
            mx2 = Math.Max(x1, x2);
            my2 = Math.Max(y1, y2);

            pt = new PointF();

            if (hline >= my1 && hline <= my2)
            {
                float pos = hline - my1;
                float ratio = pos / (my2 - my1);

                PointF p = InterpolateLine(mx1, my1, mx2, my2, ratio);

                if (p.X >= mx1 && p.X <= mx2)
                {
                    pt.X = p.X;
                    pt.Y = p.Y;
                    return true;
                }
            }

            return false;
        }

        public static bool HorizontalLineCross(float hline, float x1, float y1, float x2, float y2)
        {
            float mx1, my1, mx2, my2;

            mx1 = Math.Min(x1, x2);
            my1 = Math.Min(y1, y2);
            mx2 = Math.Max(x1, x2);
            my2 = Math.Max(y1, y2);

            if (hline >= my1 && hline <= my2)
            {
                float pos = hline - my1;
                float ratio = pos / (my2 - my1);

                PointF p = InterpolateLine(mx1, my1, mx2, my2, ratio);

                if (p.X >= mx1 && p.X <= mx2)
                    return true;
            }

            return false;
        }

        public static bool VerticalLineCross(float vline, float x1, float y1, float x2, float y2, out PointF pt)
        {
            float mx1, my1, mx2, my2;

            mx1 = Math.Min(x1, x2);
            my1 = Math.Min(y1, y2);
            mx2 = Math.Max(x1, x2);
            my2 = Math.Max(y1, y2);

            pt = new PointF();

            if (vline >= mx1 && vline <= mx2)
            {
                float pos = vline - mx1;
                float ratio = pos / (mx2 - mx1);

                PointF p = InterpolateLine(mx1, my1, mx2, my2, ratio);

                if (p.X >= mx1 && p.X <= mx2)
                {
                    pt.X = p.X;
                    pt.Y = p.Y;
                    return true;
                }
            }

            return false;
        }

        public static bool VerticalLineCross(float vline, float x1, float y1, float x2, float y2)
        {
            float mx1, my1, mx2, my2;

            mx1 = Math.Min(x1, x2);
            my1 = Math.Min(y1, y2);
            mx2 = Math.Max(x1, x2);
            my2 = Math.Max(y1, y2);

            if (vline >= mx1 && vline <= mx2)
            {
                float pos = vline - mx1;
                float ratio = pos / (mx2 - mx1);

                PointF p = InterpolateLine(mx1, my1, mx2, my2, ratio);

                if (p.X >= mx1 && p.X <= mx2)
                    return true;
            }

            return false;
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

        public static bool PointInsideBox(float x, float y, RectangleF r)
        {
            if ((x >= r.X && x <= r.Right) && (y >= r.Y && y <= r.Bottom))
                return true;

            return false;
        }

        public static bool PointInsideBox(PointF pt, RectangleF r)
        {
            if ((pt.X >= r.X && pt.X <= r.Right) && (pt.Y >= r.Y && pt.Y <= r.Bottom))
                return true;

            return false;
        }

        public static RectangleF GetBoundBox(List<PointF[]> polygons)
        {
            float minx = float.MaxValue;
            float miny = float.MaxValue;
            float maxx = float.MinValue;
            float maxy = float.MinValue;

            foreach (PointF[] pts in polygons)
            {
                for (int i = 0; i < pts.Length; i++)
                {
                    minx = Math.Min(minx, pts[i].X);
                    miny = Math.Min(miny, pts[i].Y);
                    maxx = Math.Max(maxx, pts[i].X);
                    maxy = Math.Max(maxy, pts[i].Y);
                }
            }

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        public static bool RectIntersection(RectangleF r1, RectangleF r2, out RectangleF result)
        {
            result = RectangleF.Empty;
            PointF p = new PointF();

            p.X = r2.X;
            p.Y = r2.Y;
            if (PointInsideBox(p, r1))
            {
                result.X = p.X;
                result.Y = p.Y;
                result.Width = r2.Right - p.X;
                result.Height = r2.Bottom - p.Y;

                return true;
            }

            p.X = r2.Right;
            p.Y = r2.Y;
            if (PointInsideBox(p, r1))
            {
                result.X = r1.X;
                result.Y = r2.Y;
                result.Width = r1.Right - result.X;
                result.Height = r1.Bottom - result.Y;

                return true;
            }

            p.X = r2.Right;
            p.Y = r2.Bottom;
            if (PointInsideBox(p, r1))
            {
                result.X = r1.X;
                result.Y = r1.Y;
                result.Width = r2.Right - result.X;
                result.Height = r2.Bottom - result.Y;

                return true;
            }

            p.X = r2.X;
            p.Y = r2.Bottom;
            if (PointInsideBox(p, r1))
            {
                result.X = r2.X;
                result.Y = r1.Y;
                result.Width = r1.Right - result.X;
                result.Height = r2.Bottom - result.Y;

                return true;
            }

            return false;
        }

    }
}
