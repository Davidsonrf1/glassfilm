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
        VectorShape shape = null;
        VectorPointType type = VectorPointType.Normal;
        float x, y;

        List<VectorEdge> linkedEdges = new List<VectorEdge>();

        public VectorPoint(VectorDocument doc, VectorShape shape) : base(doc)
        {
            this.shape = shape;
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
                NotifyEdges();
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
                NotifyEdges();
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
                NotifyEdges();
            }
        }

        PointF point;

        public PointF Point
        {
            get
            {
                point.X = x;
                point.Y = y;

                return point;
            }

            set
            {
                x = value.X;
                y = value.Y;

                point.X = x;
                point.Y = y;

                NotifyEdges();
            }
        }

        public override RectangleF GetBoundBox()
        {
            return new RectangleF(x - 4, y - 4, 8, 8);
        }

        void NotifyEdges()
        {
            foreach (VectorEdge e in linkedEdges)
            {
                e.PointChangeNotify(this);
            }
        }

        public void LinkEdge(VectorEdge e)
        {
            foreach (VectorEdge le in linkedEdges)
            {
                if (le == e) 
                    return;
            }

            linkedEdges.Add(e);
            e.PointChangeNotify(this);
        }

        public void UnlinkEdge(VectorEdge e)
        {
            if (linkedEdges.Contains(e))
            {
                e.PointChangeNotify(this);
                linkedEdges.Remove(e);
            }
        }

        public override void RestoreClone(VectorObject clone)
        {
            VectorPoint p = (VectorPoint)clone;

            X = p.X;
            Y = p.Y;
        }
    }
}
