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
            }
        }

        public override void Move(float distanceX, float distanceY)
        {
            base.Move(distanceX, distanceY);

            bezier.StartPoint = new PointF(StartX, StartY);
            bezier.EndPoint = new PointF(EndX, EndY);
            bezier.Control = new PointF(bezier.Control.X + distanceX, bezier.Control.Y + distanceY);

            bezier.CalculatePoints();
        }

        protected override void UpdatePoits()
        {
            //float dx = 0, dy = 0;

            //dx = Osx - StartX;
            //dy = Osy - StartY;
            //bezier.Control = new PointF(bezier.Control.X + dx, bezier.Control.Y + dy);

            base.UpdatePoits();
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
