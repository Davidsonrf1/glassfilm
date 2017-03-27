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

        public abstract RectangleF GetBoundBox();

        internal virtual void Render(Graphics g)
        {

        }

        bool isHit = false;
        public bool IsHit
        {
            get
            {
                return isHit;
            }
        }

        public VectorDocument Document
        {
            get
            {
                return document;
            }
        }

        protected virtual bool InternalHitTest(float x, float y)
        {
            return false;
        }

        public bool HitTest(float x, float y)
        {
            return isHit = InternalHitTest(x, y);
        }
    }
}
