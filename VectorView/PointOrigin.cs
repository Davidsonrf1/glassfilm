using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VectorView
{
    public class PointOrigin
    {
        VectorPoint point = null;
        PointF origin;

        internal PointOrigin(VectorPoint p)
        {
            point = p;
            origin = p.Point;
        }

        public void Restore()
        {
            if (point == null)
                return;

            point.X = origin.X;
            point.Y = origin.Y;
        }

        public void SetPoint(float x, float y)
        {
            if (point == null)
                return;

            point.X = x;
            point.Y = y;
        }

        public void SetDistance(float dx, float dy)
        {
            if (point == null)
                return;

            point.X = origin.X - dx;
            point.Y = origin.Y - dy;
        }
    }
}
