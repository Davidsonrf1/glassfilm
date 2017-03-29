using System.Drawing;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorCubicBezier : VectorCurve
    {
        CubicBezier bezier = new CubicBezier();

        VectorPoint control1 = null;
        VectorPoint control2 = null;

        public VectorCubicBezier(VectorDocument doc, VectorShape shape): base(doc, shape)
        {
            
        }

        public VectorPoint Control1
        {
            get
            {
                UpdateControls();
                return control1;
            }
        }

        public VectorPoint Control2
        {
            get
            {
                UpdateControls();
                return control2;
            }
        }

        void UpdateControls()
        {
            if (control1 == null)
            {
                if (Start != null && End != null && Shape != null)
                {
                    PointF cp = VectorMath.InterpolateLine(Start.Point, End.Point, 0.3f);
                    control1 = Shape.AddControlPoint(cp.X, cp.Y);

                    Recalculate();
                }
            }

            if (control2 == null)
            {
                if (Start != null && End != null && Shape != null)
                {
                    PointF cp = VectorMath.InterpolateLine(Start.Point, End.Point, 0.6f);
                    control2 = Shape.AddControlPoint(cp.X, cp.Y);

                    Recalculate();
                }
            }
        }

        public override void Recalculate()
        {
            if (Start == null || End == null)
                return;

            bezier.StartPoint = Start.Point;
            bezier.EndPoint = End.Point;
            bezier.Control1 = Control1.Point;
            bezier.Control2 = Control2.Point;

            bezier.Resolution = Resolution;
            bezier.CalculatePoints();

            for (int i = 0; i < bezier.Points.Length; i++)
                SetPoint(i, bezier.Points[i].X, bezier.Points[i].Y);

            base.Recalculate();
        }

        public override void RestoreClone(VectorObject clone)
        {
            base.RestoreClone(clone);

            VectorCubicBezier e = (VectorCubicBezier)clone;

            Control1.X = e.Control1.X;
            Control1.Y = e.Control1.Y;
            Control2.X = e.Control2.X;
            Control2.Y = e.Control2.Y;

            Recalculate();
        }
    }
}
