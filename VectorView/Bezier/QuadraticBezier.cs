using System.Drawing;

namespace VectorView.Bezier
{
    public class QuadraticBezier : BezierCurve
    {
        PointF control = new PointF(0, 0);

        public PointF Control
        {
            get
            {
                return control;
            }

            set
            {
                control = value; CalculatePoints();
            }
        }

        protected override void InternalCalcPoint(float ratio, out PointF point)
        {
            PointF m1 = InterpolateLine(StartPoint, control, ratio);
            PointF m2 = InterpolateLine(control, EndPoint, ratio);

            point = InterpolateLine(m1, m2, ratio);
        }
    }
}
