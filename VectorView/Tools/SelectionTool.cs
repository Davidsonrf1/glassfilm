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
                    cr.X = r.X + (r.Width/2) - t;
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
                    cr.Y = r.Y + (r.Height / 2) - t;
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
                    cr.X = r.X + (r.Width / 2) - t;
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
                    cr.Y = r.Y + ((float)r.Height / 2) - t;
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

                if (Document.GetSelectionType() != typeof(VectorPoint))
                {
                    DrawCornerRect(SelectionHitCorner.TopLeft, r);
                    if (!isRotating) DrawCornerRect(SelectionHitCorner.Top, r);
                    DrawCornerRect(SelectionHitCorner.TopRight, r);
                    if (!isRotating) DrawCornerRect(SelectionHitCorner.Right, r);
                    DrawCornerRect(SelectionHitCorner.BottomRight, r);
                    if (!isRotating) DrawCornerRect(SelectionHitCorner.Bottom, r);
                    DrawCornerRect(SelectionHitCorner.BottomLeft, r);
                    if (!isRotating) DrawCornerRect(SelectionHitCorner.Left, r);
                }                
            }

            if (isTransformingSelection && isRotating)
            {
                Document.DrawControlLine(originX, originY, startX, startY);
                Document.DrawControlLine(originX, originY, Document.MouseState.Pos.X, Document.MouseState.Pos.Y);
                float sa = VectorMath.PointAngle(originX, originY, originX + 100, originY, startX, startY);

                Document.Graphics.DrawArc(Pens.Red, originX - 90, originY - 90, 180, 180, sa, angle);               
                Document.Graphics.DrawString(angle.ToString("00.00°"), new Font("Arial", 12, FontStyle.Regular), Brushes.DarkKhaki, originX + 10, originY);
            }
        }


        float ta = 0;
        public override void MouseMove()
        {
            base.MouseMove();

            if (Document.SelectionCount > 0)
                UpdateHitCorner();

            if (isTransformingSelection)
            {
                RectangleF r = Document.SelectionBoundingBox;
                float mx, my;
                float ox, oy;

                mx = Document.MouseState.Pos.X;
                my = Document.MouseState.Pos.Y;

                if (isRotating)
                {
                    ox = startBoundingBox.X + startBoundingBox.Width / 2;
                    oy = startBoundingBox.Y + startBoundingBox.Height / 2;

                    angle = VectorMath.PointAngle(ox, oy, startX, startY, mx, my);

                    if (oList == null)
                        oList = Document.GetSelectionOrigin();

                    PointF[] pts = null;

                    if (oList != null)
                    {
                        pts = new PointF[oList.Count];
                        int i = 0;

                        foreach (PointOrigin p in oList)
                        {
                            pts[i] = p.Origin;

                            pts[i].X -= ox;
                            pts[i].Y -= oy;

                            i++;
                        }

                        Matrix mt = new Matrix();
                        mt.Rotate(angle);
                        mt.TransformPoints(pts);

                        i = 0;
                        foreach (PointOrigin p in oList)
                        {
                            p.SetPoint(pts[i].X + ox, pts[i].Y + oy);
                            i++;
                        }
                    }
                }
                else
                {
                    float dx, dy;                    

                    ox = startBoundingBox.X + startBoundingBox.Width / 2;
                    oy = startBoundingBox.Y + startBoundingBox.Height / 2;

                    if (oList == null)
                        oList = Document.GetSelectionOrigin();

                    PointF[] pts = null;

                    if (oList != null)
                    {
                        pts = new PointF[oList.Count];
                        int i = 0;

                        foreach (PointOrigin p in oList)
                        {
                            pts[i] = p.Origin;

                            pts[i].X -= ox;
                            pts[i].Y -= oy;

                            i++;
                        }

                        dx = ((mx - ox) / (startX - ox));
                        dy = ((my - oy) / (startY - oy));

                        if (Control.ModifierKeys != Keys.Control)
                        {
                            dx = (dx - 1) / 2 + 1;
                            dy = (dy - 1) / 2 + 1;
                        }

                        if (Control.ModifierKeys == Keys.Shift)
                        {
                            dx = Math.Min(dx, dy);
                            dy = dx;

                            if (startCorner == SelectionHitCorner.Left || startCorner == SelectionHitCorner.Right || startCorner == SelectionHitCorner.Top || startCorner == SelectionHitCorner.Bottom)
                            {
                                dy = dx = 1;
                            }
                        }

                        if (startCorner == SelectionHitCorner.Left || startCorner == SelectionHitCorner.Right)
                            dy = 1;

                        if (startCorner == SelectionHitCorner.Top || startCorner == SelectionHitCorner.Bottom)
                            dx = 1;

                        Matrix mt = new Matrix();
                        mt.Scale(dx, dy);
                        mt.TransformPoints(pts);

                        dx = startBoundingBox.Width / 2f * dx - startBoundingBox.Width / 2;
                        if (startCorner == SelectionHitCorner.Left || startCorner == SelectionHitCorner.BottomLeft || startCorner == SelectionHitCorner.TopLeft)
                            dx = -dx;

                        dy = startBoundingBox.Height / 2f * dy - startBoundingBox.Height / 2;
                        if (startCorner == SelectionHitCorner.Top || startCorner == SelectionHitCorner.TopLeft || startCorner == SelectionHitCorner.TopRight)
                            dy = -dy;

                        if (Control.ModifierKeys == Keys.Control)
                        {
                            dx = 0;
                            dy = 0;
                        }

                        i = 0;
                        foreach (PointOrigin p in oList)
                        {
                            p.SetPoint(pts[i].X + ox + dx, pts[i].Y + oy + dy);
                            i++;
                        }
                    }
                }

                Document.CalculateSelectionBoudingBox();
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

        float originX = 0;
        float originY = 0;

        float startAngle = 0;

        RectangleF startBoundingBox;
        SelectionHitCorner startCorner = SelectionHitCorner.None;
        
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
                oList.Clear();
                oList = null;
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
                oList = Document.GetSelectionOrigin();

                if (oList != null && oList.Count > 0)
                {
                    isTransformingSelection = true;

                    startCorner = hitCorner;

                    startBoundingBox = Document.SelectionBoundingBox;

                    originX = startBoundingBox.X + startBoundingBox.Width / 2;
                    originY = startBoundingBox.Y + startBoundingBox.Height / 2;

                    startX = Document.MouseState.Pos.X;
                    startY = Document.MouseState.Pos.Y;

                    startAngle = VectorMath.PointAngle(originX, originY, originX + 200, originY, startX, startY);

                    return;
                }
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
