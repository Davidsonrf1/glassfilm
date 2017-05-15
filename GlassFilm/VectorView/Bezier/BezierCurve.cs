using System.Collections.Generic;
using System.Drawing;

namespace VectorView.Bezier
{
    public abstract class BezierCurve
    {
        int resolution = 50;

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
                startPoint = value;
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
                endPoint = value;
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
                resolution = value;
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
