using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorQuadraticBezier : VectorCurve
    {
        VectorPoint control = null;
        QuadraticBezier bezier = new QuadraticBezier();

        public VectorPoint Control
        {
            get
            {
                UpdateControls();
                return control;
            }

            set
            {
                control = value;
            }
        }

        public VectorQuadraticBezier(VectorDocument doc, VectorShape shape) : base(doc, shape)
        {

        }

        void UpdateControls()
        {
            if (control == null)
            {
                if (Start != null && End != null && Shape != null)
                {
                    PointF cp = VectorMath.InterpolateLine(Start.Point, End.Point, 0.3f);
                    control = Shape.AddControlPoint(cp.X, cp.Y);

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
            bezier.Control = Control.Point;

            bezier.Resolution = Resolution;
            bezier.CalculatePoints();

            for (int i = 0; i < bezier.Points.Length; i++)
                SetPoint(i, bezier.Points[i].X, bezier.Points[i].Y);

            base.Recalculate();
        }

        public override void RestoreClone(VectorObject clone)
        {
            base.RestoreClone(clone);

            VectorQuadraticBezier e = (VectorQuadraticBezier)clone;

            Control.X = e.Control.X;
            Control.Y = e.Control.Y;

            Recalculate();
        }

        internal override void Render()
        {
            
            if (IsSelected)
            {
                Document.DrawControlLine(Start.X, Start.Y, Control.X, Control.Y);
                Document.DrawControlLine(End.X, End.Y, Control.X, Control.Y);

                Document.DrawControlPoint(Control.X, Control.Y);
            }
            base.Render();
        }
    }
}
