using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VectorView.Tools
{
    public class SelectionTool : VectorTool
    {
        List<VectorObject> selection = new List<VectorObject>();
        
        public SelectionTool(string name, VectorDocument doc) : base(name, doc)
        {
        }

        public override void Render(Graphics g)
        {
            base.Render(g);
        }

        public override void MouseMove()
        {
            base.MouseMove();
        }

        public override void MouseDown(MouseButton bt)
        {
            base.MouseDown(bt);

            VectorObject selObj = null;

            if (Document.MouseHitPoint != null)
            {
                selObj = Document.MouseHitPoint;
            }
            else if (Document.MouseHitEdge != null)
            {
                selObj = Document.MouseHitEdge;
            }
            else if (Document.MouseHitShape != null)
            {
                selObj = Document.MouseHitShape;
            }

            if (selObj is VectorPoint)
            {
                if (selection.Count > 0 && selection[0] is VectorEdge)
                    selObj = selection[0];
            }

            if (selObj is VectorEdge)
            {
                if (selection.Count > 0 && selection[0] is VectorShape)
                    selObj = selection[0];
            }

            if (selObj != null)
            {
                if (selObj.IsSelected)
                {
                    Document.UnselectObject(selObj);
                }
                else
                {
                    if (Document.MouseState.ModifierKeys != Keys.Shift)
                    {
                        Document.ClearSelection();
                    }

                    Document.SelectObject(selObj);
                }
            }
            else
            {
                Document.ClearSelection();
            }
        }
    }
}
