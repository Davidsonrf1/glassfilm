using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorCubicBezier : VectorCurve
    {
        CubicBezier bezier = new CubicBezier();
                
        public VectorCubicBezier(VectorPath path, float startx, float starty, float endx, float endy) : base(path, startx, starty, endx, endy)
        {
            bezier.Resolution = 100;
            Type = VectorEdgeType.Curve;
        }

        public PointF Control1
        {
            get
            {
                return bezier.Control1;
            }

            set
            {
                bezier.Control1 = value;

                bezier.StartPoint = new PointF(StartX, StartY);
                bezier.EndPoint = new PointF(EndX, EndY);
                bezier.CalculatePoints();

                InvalidatePath();
            }
        }

        public PointF Control2
        {
            get
            {
                return bezier.Control2;
            }

            set
            {
                bezier.Control2 = value;

                bezier.StartPoint = new PointF(StartX, StartY);
                bezier.EndPoint = new PointF(EndX, EndY);
                bezier.CalculatePoints();

                InvalidatePath();
            }
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

        protected override void UpdatePoits()
        {
            float dx=0, dy=0;

            dx = Osx - StartX;
            dy = Osy - StartY;
            bezier.Control1 = new PointF(bezier.Control1.X + dx, bezier.Control1.Y + dy);

            dx = Oex - EndX;
            dy = Oey - EndY;
            bezier.Control2 = new PointF(bezier.Control2.X + dx, bezier.Control2.Y + dy);

            base.UpdatePoits();
        }

        public override void CopyPoints(List<PointF> pl)
        {
            base.CopyPoints(pl);

            pl.Add(new PointF(bezier.Control1.X, bezier.Control1.Y));
            pl.Add(new PointF(bezier.Control2.X, bezier.Control2.Y));
        }

        public override void SetPoints(List<PointF> pl)
        {
            base.SetPoints(pl);

            Control1 = new PointF(pl[2].X, pl[2].Y);
            Control2 = new PointF(pl[3].X, pl[3].Y);

            InvalidatePath();
        }

        internal override VectorEdge Clone()
        {
            VectorCubicBezier q = new VectorCubicBezier(Path, StartX, StartY, EndX, EndY);
            q.Control1 = new PointF(Control1.X, Control1.Y);
            q.Control2 = new PointF(Control2.X, Control2.Y);

            return q;
        }

        public override void Move(float distanceX, float distanceY)
        {
            base.Move(distanceX, distanceY);

            bezier.StartPoint = new PointF(StartX, StartY);
            bezier.EndPoint = new PointF(EndX, EndY);

            bezier.Control1 = new PointF(bezier.Control1.X + distanceX, bezier.Control1.Y + distanceY);
            bezier.Control2 = new PointF(bezier.Control2.X + distanceX, bezier.Control2.Y + distanceY);

            bezier.CalculatePoints();

            InvalidatePath();
        }

    }
}
