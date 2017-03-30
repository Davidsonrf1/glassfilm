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
                VectorPoint p = (VectorPoint)selObj;

                foreach (VectorEdge e in p.LinkedEdges)
                {
                    if (e.IsSelected || e.Shape.IsSelected)
                        return;
                }

                if (p.Type == VectorPointType.Control)
                {
                   return;
                }
            }

            if (selObj is VectorEdge)
            {
                VectorEdge e = (VectorEdge)selObj;

                if (e.Shape.IsSelected)
                    return;
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
