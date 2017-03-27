using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public abstract class VectorObject
    {
        VectorDocument document = null;

        public VectorObject(VectorDocument doc)
        {
            document = doc;
        }

        public VectorObject()
        {
            document = null;
        }

        public abstract RectangleF GetBoundBox();

        internal virtual void Render(Graphics g)
        {

        }
    }
}
