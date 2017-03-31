using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorView.Tools
{
    public enum SelectionHitCorner { None, TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left }

    public class SelectionTool : VectorTool
    {
        bool isRotating = false;

        public void ToggleRotation()
        {
            isRotating = !isRotating;
        }

        float angle = 0;

        public SelectionTool(string name, VectorDocument doc) : base(name, doc)
        {
        }

        SelectionHitCorner hitCorner = SelectionHitCorner.None;

        RectangleF GetCornerRect(SelectionHitCorner corner, RectangleF r)
        {
            RectangleF cr = new RectangleF();

            float t = Document.HitTolerance + 2;

            switch (corner)
            {
                case SelectionHitCorner.None:
                    break;
                case SelectionHitCorner.TopLeft:
                    cr.X = r.X - t * 2;
                    cr.Y = r.Y - t * 2;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Top:
                    cr.X = r.X + (r.Width/2) - t * 2;
                    cr.Y = r.Y - t * 2;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.TopRight:
                    cr.X = r.Right;
                    cr.Y = r.Y - t * 2;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Right:
                    cr.X = r.Right;
                    cr.Y = r.Y + (r.Height / 2) - t * 2;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.BottomRight:
                    cr.X = r.Right;
                    cr.Y = r.Y + r.Height;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Bottom:
                    cr.X = r.X + (r.Width / 2) - t * 2;
                    cr.Y = r.Bottom;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.BottomLeft:
                    cr.X = r.X - t * 2;
                    cr.Y = r.Bottom;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Left:
                    cr.X = r.X - t * 2;
                    cr.Y = r.Y + (r.Height / 2) - t * 2;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                default:
                    break;
            }

            return cr;
        }

        void DrawCornerRect(SelectionHitCorner c, RectangleF r)
        {
            RectangleF rc = GetCornerRect(c, r);

            Brush b = c == hitCorner ? Brushes.Red : Brushes.DarkOliveGreen;

            if (!isRotating)
                Document.Graphics.FillRectangle(b, rc.X, rc.Y, rc.Width, rc.Height);
            else
                Document.Graphics.FillEllipse(b, rc.X, rc.Y, rc.Width, rc.Height);
        }

        bool TestHitCorner(float x, float y, SelectionHitCorner c, RectangleF rect)
        {
            RectangleF r = GetCornerRect(c, rect);

            if (x >= r.X && x <= r.Right && y >= r.Y && y <= r.Bottom)
                return true;

            return false;
        }

        SelectionHitCorner UpdateHitCorner ()
        {
            RectangleF r = Document.SelectionBoundingBox;

            float x = Document.MouseState.Pos.X;
            float y = Document.MouseState.Pos.Y;

            hitCorner = SelectionHitCorner.None;

            if (TestHitCorner(x, y, SelectionHitCorner.TopLeft, r)) hitCorner = SelectionHitCorner.TopLeft;
            if (!isRotating && TestHitCorner(x, y, SelectionHitCorner.Top, r)) hitCorner = SelectionHitCorner.Top;
            if (TestHitCorner(x, y, SelectionHitCorner.TopRight, r)) hitCorner = SelectionHitCorner.TopRight;
            if (!isRotating && TestHitCorner(x, y, SelectionHitCorner.Right, r)) hitCorner = SelectionHitCorner.Right;
            if (TestHitCorner(x, y, SelectionHitCorner.BottomRight, r)) hitCorner = SelectionHitCorner.BottomRight;
            if (!isRotating && TestHitCorner(x, y, SelectionHitCorner.Bottom, r)) hitCorner = SelectionHitCorner.Bottom;
            if (TestHitCorner(x, y, SelectionHitCorner.BottomLeft, r)) hitCorner = SelectionHitCorner.BottomLeft;
            if (!isRotating && TestHitCorner(x, y, SelectionHitCorner.Left, r)) hitCorner = SelectionHitCorner.Left;

            return hitCorner;
        }

        public override void Render()
        {
            base.Render();
            RectangleF r = Document.SelectionBoundingBox;

            if (Document.SelectionCount > 0)
            {
                Pen p = new Pen(Color.Blue, 1);
                p.DashStyle = DashStyle.Custom;
                p.DashPattern = new float[] { 1.0f, 4.0f };

                Document.Graphics.DrawRectangle(p, r.X, r.Y, r.Width, r.Height);

                DrawCornerRect(SelectionHitCorner.TopLeft, r);
                if (!isRotating) DrawCornerRect(SelectionHitCorner.Top, r);
                DrawCornerRect(SelectionHitCorner.TopRight, r);
                if (!isRotating) DrawCornerRect(SelectionHitCorner.Right, r);
                DrawCornerRect(SelectionHitCorner.BottomRight, r);
                if (!isRotating) DrawCornerRect(SelectionHitCorner.Bottom, r);
                DrawCornerRect(SelectionHitCorner.BottomLeft, r);
                if (!isRotating) DrawCornerRect(SelectionHitCorner.Left, r);
                
            }

            if (isTransformingSelection && isRotating)
            {
                Document.DrawControlLine(r.X + r.Width / 2, r.Y + r.Height / 2, Document.MouseState.Pos.X, Document.MouseState.Pos.Y);

                Document.Graphics.DrawString(angle.ToString(), new Font("Arial",12, FontStyle.Regular), Brushes.DarkKhaki, r.X + r.Width / 2, r.Y + r.Height / 2);
            }
        }

        public override void MouseMove()
        {
            base.MouseMove();

            if (Document.SelectionCount > 0)
                UpdateHitCorner();

            if (isTransformingSelection)
            {
                RectangleF r = Document.SelectionBoundingBox;
                float ox, oy, mx, my;

                ox = r.X + r.Width / 2;
                oy = r.Y + r.Height / 2;
                mx = Document.MouseState.Pos.X;
                my = Document.MouseState.Pos.Y;

                if (isRotating)
                {
                    angle = VectorMath.PointAngle(ox, oy, mx, my);

                    Matrix mt = new Matrix();
                    mt.RotateAt(100+angle, new PointF(ox, oy));
                    PointF[] pts = new PointF[1];
                    pts[0] = new PointF();

                    if (oList == null)
                        oList = Document.GetSelectionOrigin();

                    if (oList != null)
                    {
                        foreach (PointOrigin p in oList)
                        {
                            pts[0].X = p.Origin.X;
                            pts[0].Y = p.Origin.Y;

                            mt.TransformPoints(pts);

                            p.SetPoint(pts[0].X, pts[0].Y);
                        }
                    }

                    Document.CalculateSelectionBoudingBox();
                }
            }

            if (isMovingDocument)
            {
                Document.OffsetX -= Document.MouseState.RightDownPos.X - Document.MouseState.Pos.X;
                Document.OffsetY -= Document.MouseState.RightDownPos.Y - Document.MouseState.Pos.Y;
            }

            if (isMovingSelection)
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
        bool isMovingSelection = false;
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

        public bool IsRotating
        {
            get
            {
                return isRotating;
            }

            set
            {
                isRotating = value;
            }
        }

        public override void MouseUp(MouseButton bt)
        {
            base.MouseUp(bt);

            if (isTransformingSelection)
            {
                isTransformingSelection = false;
                return;
            }

            if (isMovingDocument)
            {
                isMovingDocument = false;
                return;
            }

            if (isMovingSelection)
            {
                isMovingSelection = false;

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
                    //Document.UnselectObject(selObj);
                    ToggleRotation();
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

            if (hitCorner != SelectionHitCorner.None)
            {
                isTransformingSelection = true;
                return;
            }

            if (Document.HasHitObject)
            {
                hasMove = false;

                startX = Document.MouseState.LeftDownPos.X;
                startY = Document.MouseState.LeftDownPos.Y;

                isMovingSelection = true;
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
