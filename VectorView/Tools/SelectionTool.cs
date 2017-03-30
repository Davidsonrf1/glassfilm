using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorView.Tools
{
    public class SelectionTool : VectorTool
    {      
        public SelectionTool(string name, VectorDocument doc) : base(name, doc)
        {
        }

        public override void Render()
        {
            base.Render();

            if (Document.SelectionCount > 0)
            {
                //CalculateSelectionBoudingBox();

                Pen p = new Pen(Color.Blue, 1);
                p.DashStyle = DashStyle.Custom;
                p.DashPattern = new float[] { 1.0f, 4.0f };

                RectangleF r = Document.SelectionBoundingBox;

                Document.Graphics.DrawRectangle(p, r.X, r.Y, r.Width, r.Height);
            }
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
                //if (selection.Count > 0 && selection[0] is VectorEdge)
                //    selObj = selection[0];
                

                if(((VectorPoint)selObj).Type != VectorPointType.Normal)
                {
                   // return;
                }
            }

            if (selObj is VectorEdge)
            {
                //if (selection.Count > 0 && selection[0] is VectorShape)
                //    selObj = selection[0];
            }
  
            if (selObj != null)
            {

                Document.UnselectObject(selObj);
                if (selObj.IsSelected)
                {
                    
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
