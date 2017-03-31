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

        VectorPoint controledPoint = null;

        public VectorPoint(VectorDocument doc, VectorShape shape) : base(doc)
        {
            this.shape = shape;
        }

        public VectorPoint(VectorDocument doc, VectorShape shape, VectorPoint controledPoint) : base(doc)
        {
            this.shape = shape;
            this.controledPoint = controledPoint;
            type = VectorPointType.Control;
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

        public List<VectorEdge> LinkedEdges
        {
            get
            {
                return linkedEdges;
            }
        }

        public VectorPoint ControledPoint
        {
            get
            {
                return controledPoint;
            }

            set
            {
                controledPoint = value;
            }
        }

        public override RectangleF GetBoundBox()
        {
            return new RectangleF(x - 4, y - 4, 8, 8);
        }

        void NotifyEdges()
        {
            foreach (VectorEdge e in LinkedEdges)
            {
                e.PointChangeNotify(this);
            }
        }

        public void LinkEdge(VectorEdge e)
        {
            foreach (VectorEdge le in LinkedEdges)
            {
                if (le == e) 
                    return;
            }

            LinkedEdges.Add(e);
            e.PointChangeNotify(this);
        }

        public void UnlinkEdge(VectorEdge e)
        {
            if (LinkedEdges.Contains(e))
            {
                e.PointChangeNotify(this);
                LinkedEdges.Remove(e);
            }
        }

        public override void RestoreClone(VectorObject clone)
        {
            VectorPoint p = (VectorPoint)clone;

            X = p.X;
            Y = p.Y;
        }

        internal override void Render()
        {
            base.Render();

            if (IsSelected)
            {
                if (type == VectorPointType.Control)
                    Document.DrawControlPoint(x, y);

                if (type == VectorPointType.Normal)
                    Document.DrawPoint(x, y);
            }
        }

        public PointOrigin GetOrigin()
        {
            return new PointOrigin(this);
        }

        public override void FillOriginList(List<PointOrigin> ol)
        {
            base.FillOriginList(ol);
            ol.Add(GetOrigin());
        }
    }
}
