using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorQuadraticBezier : VectorCurve
    {
        QuadraticBezier bezier = new QuadraticBezier();

        public VectorQuadraticBezier(VectorPath path, float startx, float starty, float endx, float endy) : base(path, startx, starty, endx, endy)
        {
            Type = VectorEdgeType.QCurve;
        }

        public PointF Control
        {
            get
            {
                return bezier.Control;
            }

            set
            {
                bezier.Control = value;

                bezier.StartPoint = new PointF(StartX, StartY);
                bezier.EndPoint = new PointF(EndX, EndY);
                bezier.CalculatePoints();

                InvalidatePath();
            }
        }

        public override void Move(float distanceX, float distanceY)
        {
            base.Move(distanceX, distanceY);

            bezier.StartPoint = new PointF(StartX, StartY);
            bezier.EndPoint = new PointF(EndX, EndY);
            bezier.Control = new PointF(bezier.Control.X + distanceX, bezier.Control.Y + distanceY);

            bezier.CalculatePoints();

            InvalidatePath();
        }

        protected override void UpdatePoits()
        {
            base.UpdatePoits();

            InvalidatePath();
        }

        public override void CopyPoints(List<PointF> pl)
        {
            base.CopyPoints(pl);

            pl.Add(new PointF(bezier.Control.X, bezier.Control.Y));
        }

        public override void SetPoints(List<PointF> pl)
        {
            base.SetPoints(pl);
            Control = new PointF(pl[2].X, pl[2].Y);

            InvalidatePath();
        }

        internal override VectorEdge Clone()
        {
            VectorQuadraticBezier q = new VectorQuadraticBezier(Path, StartX, StartY, EndX, EndY);
            q.Control = new PointF(Control.X, Control.Y);
            return q;
        }

        protected override void FillCurvePoints(List<PointF> pts)
        {
            bezier.StartPoint = new PointF(StartX, StartY);
            bezier.EndPoint = new PointF(EndX, EndY);

            bezier.CalculatePoints();

            foreach (PointF p in bezier.Points)
            {
                pts.Add(p);
            }
        }
    }
}
