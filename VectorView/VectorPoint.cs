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
        float sx, sy;

        List<VectorEdge> linkedEdges = new List<VectorEdge>();

        static bool useShadowPoint = false;

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
                if (!useShadowPoint)
                {
                    return x;
                }
                else
                {
                    return sx;
                }
            }

            set
            {
                if (useShadowPoint)
                {
                    sx = value;
                    NotifyEdges();
                }
                else
                {
                    x = value;
                    NotifyEdges();
                }
            }
        }

        public float Y
        {
            get
            {
                if (!useShadowPoint)
                {
                    return y;
                }
                else
                {
                    return sy;
                }
            }

            set
            {
                if (useShadowPoint)
                {
                    sy = value;
                    NotifyEdges();
                }
                else
                {
                    y = value;
                    NotifyEdges();
                }
            }
        }

        public static bool UseShadowPoint
        {
            get
            {
                return useShadowPoint;
            }

            set
            {
                useShadowPoint = value;
            }
        }

        public void ClearShadow()
        {
            sx = x;
            sy = y;

            NotifyEdges();
        }

        public void ApplyShadow()
        {
            x = sx;
            y = sy;

            NotifyEdges();
        }

        PointF point;
        PointF shadowPoint;

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

        public PointF ShadowPoint
        {
            get
            {
                shadowPoint.X = sx;
                shadowPoint.Y = sy;

                return shadowPoint;
            }

            set
            {
                sx = value.X;
                sy = value.Y;

                shadowPoint.X = sx;
                shadowPoint.Y = sy;

                NotifyEdges();
            }
        }

        public override RectangleF GetBoundBox()
        {
            return new RectangleF(x - 2, y - 2, 4, 4);
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
    }
}
