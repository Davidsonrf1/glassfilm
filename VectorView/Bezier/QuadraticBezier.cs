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
            PointF m1 = VectorMath.InterpolateLine(StartPoint, control, ratio);
            PointF m2 = VectorMath.InterpolateLine(control, EndPoint, ratio);

            point = VectorMath.InterpolateLine(m1, m2, ratio);
        }
    }
}
