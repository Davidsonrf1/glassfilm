using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VectorView.Tools
{
    public abstract class VectorTool
    {
        VectorDocument doc = null;

        bool active = true;

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public VectorDocument Doc
        {
            get
            {
                return doc;
            }

            set
            {
                doc = value;
            }
        }

        public VectorTool(VectorDocument doc)
        {
            this.doc = doc;
        }

        public virtual void MouseMove()
        {

        }

        public virtual void Render(Graphics g)
        {

        }
    }
}
