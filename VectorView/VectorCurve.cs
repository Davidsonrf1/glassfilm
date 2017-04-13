using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public class VectorCurve : VectorEdge
    {
        int resolution = 128;
        PointF[] calcPoints = null;

        void UpdateCalcPoints()
        {
            if (calcPoints == null || calcPoints.Length != resolution)
            {
                if (resolution == 0)
                {
                    calcPoints = null;
                }

                calcPoints = new PointF[resolution];

                for (int i = 0; i < resolution; i++)
                {
                    calcPoints[i] = new PointF();
                }

                Recalculate();
            }
        }

        public VectorCurve(VectorDocument doc, VectorShape shape) : base(doc, shape)
        {
            UpdateCalcPoints();
        }

        public int Resolution
        {
            get
            {
                return resolution;
            }

            set
            {
                resolution = value;
                UpdateCalcPoints();
            }
        }

        public void SetPoint(int index, float x, float y)
        {
            if (index < 0 || index >= resolution)
            {
                if (Debugger.IsAttached)
                {
                    throw new IndexOutOfRangeException("Tentativa de alterar um ponto inválido na curva");
                }
                else
                {
                    return;
                }
            }

            if (calcPoints[index] == null)
            {
                calcPoints[index] = new PointF();
            }

            calcPoints[index].X = x;
            calcPoints[index].Y = y;
        }

        protected PointF[] CalcPoints
        {
            get
            {
                return calcPoints;
            }
        }

        internal override void Render()
        {
            if (Shape == null)
                return;

            Recalculate();

            PointF s = Start.Point;
            for (int i = 0; i < calcPoints.Length; i++)
            {
                Document.DrawLine(s.X, s.Y, calcPoints[i].X, calcPoints[i].Y);
                s = calcPoints[i];
            }

            Document.DrawLine(s.X, s.Y, End.Point.X, End.Point.Y);

            if (IsSelected || Shape.IsSelected)
            {
                Document.DrawPoint(Start.X, Start.Y);
                Document.DrawPoint(End.X, End.Y);
            }
        }

        public override int CrossPointCount(float hline, List<PointF> crossPoints = null)
        {
            int count = 0;

            if (Shape == null)
                return count;

            PointF cp;
            PointF s = Start.Point;
            for (int i = 0; i < calcPoints.Length; i++)
            {
                if (VectorMath.HorizontalCrossPoint(s.X, s.Y, calcPoints[i].X, calcPoints[i].Y, hline, out cp))
                {
                    if (crossPoints != null)
                        crossPoints.Add(cp);

                    count++;
                }

                s = calcPoints[i];
            }

            if (VectorMath.HorizontalCrossPoint(s.X, s.Y, End.Point.X, End.Point.Y, hline, out cp))
            {
                if (crossPoints != null)
                    crossPoints.Add(cp);

                count++;
            }

            if (count == 0)
                return 0; // Só para colocar um break point abaixo;

            return count;
        }

        public override RectangleF GetBoundBox()
        {
            float minx, miny, maxx, maxy;

            minx = Math.Min(Start.X, End.X);
            miny = Math.Min(Start.Y, End.Y);

            maxx = Math.Max(Start.X, End.X);
            maxy = Math.Max(Start.Y, End.Y);

            for (int i = 0; i < calcPoints.Length; i++)
            {
                minx = Math.Min(minx, calcPoints[i].X);
                miny = Math.Min(miny, calcPoints[i].Y);

                maxx = Math.Max(maxx, calcPoints[i].X);
                maxy = Math.Max(maxy, calcPoints[i].Y);
            }

            return new RectangleF(minx, miny, maxx - minx, maxy - miny);
        }

        protected override bool InternalHitTest(float x, float y)
        {
            float mind = float.MaxValue;

            if (calcPoints.Length <= 0)
                return base.InternalHitTest(x, y);

            PointF p = new PointF(x, y);

            mind = Math.Min(VectorMath.PointToLineDistance(p, Start.Point, calcPoints[0]), mind);

            int i = 1;
            for (; i < calcPoints.Length; i++)
            {
                mind = Math.Min(VectorMath.PointToLineDistance(p, calcPoints[i - 1], calcPoints[i]), mind);
            }

            mind = Math.Min(VectorMath.PointToLineDistance(p, calcPoints[i - 1], End.Point), mind);

            return mind < Document.HitTolerance;
        }

        public override void FillPolyline(List<PointF> polyline)
        {
            foreach (PointF p in calcPoints)
            {
                polyline.Add(p);
            }

            polyline.Add(new PointF(End.X, End.Y));
        }
    }
}
