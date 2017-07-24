using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VectorView.Bezier;

namespace VectorView
{
    public abstract class VectorCurveSegment : VectorSegment
    {
        List<PointF> curvePoints = new List<PointF>();
        protected abstract List<PointF> GetPoints();
        protected override void FillPoints(List<PointF> pts)
        {
            pts.AddRange(GetPoints());
        }
    }
}
