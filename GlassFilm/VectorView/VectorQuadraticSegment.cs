using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorQuadraticSegment: VectorCurveSegment
    {
        PointF control = new PointF();

        public PointF Control
        {
            get
            {
                return control;
            }

            set
            {
                control = value;
            }
        }

        public override void Move(float dx, float dy)
        {
            control.X += dx;
            control.Y += dy;

            base.Move(dx, dy);
        }

        protected override List<PointF> GetPoints()
        {
            QuadraticBezier qb = new QuadraticBezier();

            qb.StartPoint = Start;
            qb.EndPoint = End;

            qb.Control = control;

            qb.Resolution = 100;
            qb.CalculatePoints();

            List<PointF> pts = new List<PointF>();

            pts.AddRange(qb.Points);

            return pts;
        }
    }
}
