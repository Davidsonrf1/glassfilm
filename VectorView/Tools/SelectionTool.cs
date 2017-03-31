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

            if (isTransformingSelection)
            {
                float dx = Document.MouseState.Pos.X - startX;
                float dy = Document.MouseState.Pos.Y - startY;

                //Document.DrawControlLine(startX, startY, Document.MouseState.Pos.X, Document.MouseState.Pos.Y);
            }
        }

        public override void MouseMove()
        {
            base.MouseMove();

            if (isMovingDocument)
            {
                Document.OffsetX -= Document.MouseState.RightDownPos.X - Document.MouseState.Pos.X;
                Document.OffsetY -= Document.MouseState.RightDownPos.Y - Document.MouseState.Pos.Y;
            }

            if (isTransformingSelection)
            {
                hasMove = true;

                float dx = startX - Document.MouseState.Pos.X;
                float dy = startY - Document.MouseState.Pos.Y;
                
                if (oList != null)
                {
                    foreach (PointOrigin o in oList)
                    {
                        o.SetDistance(dx, dy);
                    }
                }

                Document.CalculateSelectionBoudingBox();
            }
        }

        bool isMovingDocument = false;
        bool isTransformingSelection = false;

        float startX = 0;
        float startY = 0;
        
        VectorObject GetHitObject()
        {
            VectorObject hitObj = null;

            if (Document.MouseHitPoint != null)
            {
                hitObj = Document.MouseHitPoint;
            }
            else if (Document.MouseHitEdge != null)
            {
                hitObj = Document.MouseHitEdge;
            }
            else if (Document.MouseHitShape != null)
            {
                hitObj = Document.MouseHitShape;
            }

            return hitObj;
        }

        List<PointOrigin> oList = null;

        bool hasMove = false;

        public override void MouseUp(MouseButton bt)
        {
            base.MouseUp(bt);

            if (isMovingDocument)
            {
                isMovingDocument = false;
                return;
            }

            if (isTransformingSelection)
            {
                isTransformingSelection = false;

                if (oList != null)
                    oList.Clear();

                oList = null;

                if (hasMove)
                {
                    hasMove = false;
                    return;
                }

                hasMove = false;
            }

            if (bt != MouseButton.Left)
                return;

            VectorObject selObj = GetHitObject();

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

        bool NeedClearSelection(VectorObject o)
        {
            if (o is VectorPoint)
            {
                VectorPoint p = (VectorPoint)o;

                if (p.Type == VectorPointType.Control)
                    p = p.ControledPoint;

                foreach (VectorEdge e in p.LinkedEdges)
                {
                    if (e.IsSelected || e.Shape.IsSelected)
                        return false;
                }
            }
            else if (o is VectorEdge)
            {
                VectorEdge e = (VectorEdge)o;

                if (e.Shape.IsSelected)
                    return false;
            }

            return true;
        }

        public override void MouseDown(MouseButton bt)
        {
            base.MouseDown(bt);

            if (bt == MouseButton.Right)
            {
                isMovingDocument = true;
                return;
            }

            if (Document.HasHitObject)
            {
                hasMove = false;

                startX = Document.MouseState.LeftDownPos.X;
                startY = Document.MouseState.LeftDownPos.Y;

                isTransformingSelection = true;
                VectorObject hitObj = GetHitObject();

                if (hitObj != null)
                {
                    if (hitObj is VectorPoint /*&& ((VectorPoint)hitObj).Type == VectorPointType.Control*/)
                    {
                        oList = new List<VectorView.PointOrigin>();
                        hitObj.FillOriginList(oList);
                    }
                    else
                    {
                        if (!hitObj.IsSelected && NeedClearSelection(hitObj))
                        {
                            Document.ClearSelection();

                            oList = new List<VectorView.PointOrigin>();
                            hitObj.FillOriginList(oList);
                        }
                        else
                        {
                            oList = Document.GetSelectionOrigin();
                        }
                    }

                    //Document.SelectObject(hitObj);
                    //oList = Document.GetSelectionOrigin();
                }
            }
        }
    }
}
