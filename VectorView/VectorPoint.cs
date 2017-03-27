using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public enum VectorPointType { Normal, Control, Origin }

    public class VectorPoint: VectorObject
    {
        VectorPointType type = VectorPointType.Normal;
        float x, y;

        public VectorPoint(VectorDocument doc) : base(doc)
        {
        }

        public PointF ToPoint()
        {
            return new PointF(x, y);
        }

        public VectorPointType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public override RectangleF GetBoundBox()
        {
            return new RectangleF(x - 2, y - 2, 4, 4);
        }
    }
}
