using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorCubicSegment: VectorCurveSegment
    {
        PointF control1 = new PointF();
        PointF control2 = new PointF();

        public PointF Control1
        {
            get
            {
                return control1;
            }

            set
            {
                control1 = value;
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
                control2 = value;
            }
        }

        public override void Move(float dx, float dy)
        {
            control1.X += dx;
            control1.Y += dy;
            control2.X += dx;
            control2.Y += dy;

            base.Move(dx, dy);
        }

        protected override List<PointF> GetPoints()
        {
            CubicBezier cb = new CubicBezier();

            cb.StartPoint = Start;
            cb.EndPoint = End;

            cb.Control1 = control1;
            cb.Control2 = control2;

            cb.Resolution = 100;
            cb.CalculatePoints();

            List<PointF> ret = new List<PointF>();

            ret.AddRange(cb.Points);
            return ret;
        }
    }
}
