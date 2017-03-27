using System.Collections.Generic;
using System.Drawing;

namespace VectorView.Bezier
{
    public abstract class BezierCurve
    {
        int resolution = 460;

        PointF startPoint = new PointF(0, 0);
        PointF endPoint = new PointF(0, 0);

        public PointF StartPoint
        {
            get
            {
                return startPoint;
            }

            set
            {
                startPoint = value; CalculatePoints();
            }
        }

        public PointF EndPoint
        {
            get
            {
                return endPoint;
            }

            set
            {
                endPoint = value; CalculatePoints();
            }
        }

        public int Resolution
        {
            get
            {
                return resolution;
            }

            set
            {
                resolution = value; CalculatePoints();
            }
        }

        public PointF[] Points
        {
            get
            {
                return points;
            }
        }

        PointF[] points = null;

        public BezierCurve()
        {
            points = new PointF[resolution];
        }

        protected abstract void InternalCalcPoint(float ratio, out PointF point);
        public void CalculatePoints()
        {
            if (points == null)
                points = new PointF[resolution];

            if (points.Length != resolution)
                points = new PointF[resolution];

            float ratio = 0;
            float res = resolution;

            for (int i = 0; i < points.Length; i++)
            {
                ratio = 1 / res * i;

                if (points[i] == null)
                    points[i] = new PointF(0, 0);

                InternalCalcPoint(ratio, out points[i]);
            }
        }

        public PointF InterpolateLine(PointF p1, PointF p2, float ratio)
        {
            return new PointF(p1.X + ((p2.X - p1.X) * ratio), p1.Y + ((p2.Y - p1.Y) * ratio));
        }

        public IEnumerable<PointF> GetPoints()
        {
            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    yield return points[i];
                }
            }
        }
    }
}
