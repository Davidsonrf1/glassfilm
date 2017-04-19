using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VectorView.Tools
{
    public class ZoomTool : VectorTool
    {
        bool allowZoom = true;


        public ZoomTool(string name, VectorDocument doc) : base(name, doc)
        {
        }

        public bool AllowZoom
        {
            get
            {
                return allowZoom;
            }

            set
            {
                allowZoom = value;
            }
        }

        public override void MouseWeel(float delta, float x, float y)
        {
            base.MouseWeel(delta, x, y);

            if (delta == 0 || !allowZoom)
                return;

            float ds = (delta / Math.Abs(delta));
            float d = (0.04f * ds);

            PointF p1 = Document.DocumentToViewPoint(x, y);
            Document.Scale += d;
            PointF p2 = Document.DocumentToViewPoint(x, y);

            Document.OffsetX -= Math.Abs(p2.X - p1.X) * Document.InverseScale * ds;
            Document.OffsetY -= Math.Abs(p2.Y - p1.Y) * Document.InverseScale * ds;

        }
    }
}
