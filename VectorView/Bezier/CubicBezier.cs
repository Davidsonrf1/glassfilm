using System.Drawing;

namespace VectorView.Bezier
{
    public class CubicBezier : BezierCurve
    {
        PointF control1 = new PointF(0, 0);
        PointF control2 = new PointF(0, 0);

        public PointF Control1
        {
            get
            {
                return control1;
            }

            set
            {
                control1 = value; CalculatePoints();
            }
        }

        public PointF Control2
        {
            get
            {
                return control2;
            }

            set
            {
                control2 = value; CalculatePoints();
            }
        }

        protected override void InternalCalcPoint(float ratio, out PointF point)
        {
            PointF m1 = InterpolateLine(StartPoint, control1, ratio);
            PointF m2 = InterpolateLine(control1, control2, ratio);
            PointF m3 = InterpolateLine(control2, EndPoint, ratio);

            PointF s1 = InterpolateLine(m1, m2, ratio);
            PointF s2 = InterpolateLine(m2, m3, ratio);

            point = InterpolateLine(s1, s2, ratio);
        }
    }
}
