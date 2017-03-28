using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorView.Bezier;

namespace VectorView
{
    public class VectorCubicBezier : VectorCurve
    {
        CubicBezier bezier = new CubicBezier();

        VectorPoint start = null;
        VectorPoint end = null;

        public override void Recalculate()
        {
            base.Recalculate();
        }

        public VectorCubicBezier(VectorDocument doc, VectorShape shape) : base(doc, shape)
        {
            //start = shape.AddPoint()

        }
    }
}
