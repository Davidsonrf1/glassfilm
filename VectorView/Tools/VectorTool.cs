using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace VectorView.Tools
{
    public abstract class VectorTool
    {
        VectorDocument doc = null;
        string name = null;

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

        public VectorDocument Document
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

        public string Name
        {
            get
            {
                return name;
            }
        }

        public virtual void Apply()
        {

        }

        public VectorTool(string name, VectorDocument doc)
        {
            this.doc = doc;
            this.name = name;
        }

        public virtual void MouseMove()
        {

        }

        public virtual void MouseDown(MouseButton bt)
        {

        }

        public virtual void MouseUp(MouseButton bt)
        {

        }

        public virtual void Render()
        {

        }
    }
}
