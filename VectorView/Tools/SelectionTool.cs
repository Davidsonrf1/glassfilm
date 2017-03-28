using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Forms;

namespace VectorView.Tools
{
    public class SelectionTool : VectorTool
    {
        List<VectorObject> selection = new List<VectorObject>();
        
        public SelectionTool(string name, VectorDocument doc) : base(name, doc)
        {
        }

        public override void MouseMove()
        {
            base.MouseMove();
        }

        public override void MouseDown(MouseButton bt)
        {
            base.MouseDown(bt);

            VectorObject selObj = null;

            if (Doc.MouseHitPoint != null)
            {
                selObj = Doc.MouseHitPoint;
            }
            else if (Doc.MouseHitEdge != null)
            {
                selObj = Doc.MouseHitEdge;
            }
            else if (Doc.MouseHitShape != null)
            {
                selObj = Doc.MouseHitShape;
            }
        }
    }
}
