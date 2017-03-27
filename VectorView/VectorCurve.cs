using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public class VectorCurve : VectorEdge
    {
        int resolution = 128;

        public VectorCurve(VectorDocument doc, VectorShape shape) : base(doc, shape)
        {

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
            }
        }

        public override RectangleF GetBoundBox()
        {
            throw new NotImplementedException();
        }
    }
}
