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
        int id = 0;

        static int curID = 0;
        public VectorObject(VectorDocument doc)
        {
            id = ++curID;
            document = doc;
        }

        protected VectorObject(VectorObject obj)
        {
            document = obj.document;
            id = obj.Id;
        }

        public virtual VectorObject GetClone()
        {
            return null;
        }

        public abstract void RestoreClone(VectorObject clone);
        public abstract RectangleF GetBoundBox();

        internal virtual void Render()
        {

        }

        bool isSelect = false;

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

        public bool IsSelected
        {
            get
            {
                return isSelect;
            }

            internal set
            {
                isSelect = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        protected virtual bool InternalHitTest(float x, float y)
        {
            return false;
        }

        public virtual void FillOriginList(List<PointOrigin> ol)
        {

        }

        public bool HitTest(float x, float y)
        {
            isHit = InternalHitTest(x, y);
            return isHit;
        }

        public void MoveTo(float x, float y)
        {
            List<PointOrigin> oList = new List<VectorView.PointOrigin>();
            FillOriginList(oList);

            RectangleF r = GetBoundBox();

            float dx, dy;

            dx = x - r.X;
            dy = y - r.Y;

            foreach (PointOrigin p in oList)
            {
                p.SetDistance(dx, dy);
            }
        }
    }
}
