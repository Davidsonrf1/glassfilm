using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace VectorView
{
    public enum SelectionHitCorner { None, TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left }
    public enum TransformType { Rotaing, Moving }

    public partial class VectorDocument
    {
        List<VectorPath> selection = new List<VectorPath>();
        bool isTransforming = false;

        Dictionary<VectorPath, PointF> origins = new Dictionary<VectorPath, PointF>();
        Dictionary<VectorPath, float> angles = new Dictionary<VectorPath, float>();

        TransformType transformType =  TransformType.Rotaing;

        float hitCornerSize = 8;
        float hitCornerMargin = 5;
        bool cursorInsideSelection = false;

        SelectionHitCorner hitCorner = SelectionHitCorner.None;

        public int SelectionCount
        {
            get
            {
                return selection.Count;
            }
        }

        public bool IsTransforming
        {
            get
            {
                return isTransforming;
            }
        }

        public float HitCornerSize
        {
            get
            {
                return hitCornerSize;
            }

            set
            {
                hitCornerSize = value;
            }
        }

        public float HitCornerMargin
        {
            get
            {
                return hitCornerMargin;
            }

            set
            {
                hitCornerMargin = value;
            }
        }

        public PointF ViewCursorPos
        {
            get
            {
                return viewCursorPos;
            }
        }

        public bool CursorInsideSelection
        {
            get
            {
                return cursorInsideSelection;
            }
        }

        public PointF SelCenter
        {
            get
            {
                return selCenter;
            }
        }

        public List<VectorPath> Selection
        {
            get
            {
                return selection;
            }
        }

        public SelectionHitCorner HitCorner
        {
            get
            {
                return hitCorner;
            }
        }

        public void UnselectPath(VectorPath p)
        {
            if (p.Document != this || isTransforming)
                return;

            selection.Remove(p);

            p.Selected = false;

            host.OnSelectionChanged();
        }

        public void SelectPath(VectorPath p)
        {
            if (p.Document != this || isTransforming)
                return;

            selection.Add(p);
            p.Selected = true;

            host.OnSelectionChanged();
        }

        public void Clear()
        {
            foreach (VectorPath p in paths)
            {
                p.Imported = false;
            }

            ClearSelection();
            paths.Clear();



            host.Invalidate();
        }

        public void DeleteSelection()
        {
            foreach (VectorPath p in selection)
            {
                paths.Remove(p);

                if (CountSource(p.Source) == 0)
                {
                    p.Source.Imported = false;
                }
            }

            ClearSelection();
            Host.Invalidate();
        }

        public void ClearSelection()
        {
            if (isTransforming)
                return;

            selection.Clear();

            foreach (VectorPath p in paths)
            {
                p.Selected = false;
            }

            host.OnSelectionChanged();
        }

        float selCenterX = 0;
        float selCenterY = 0;

        bool validPosBeforeTransform = false;
        bool validPosAfterTransform = false;

        public void BeginTransform()
        {
            isTransforming = true;

            origins.Clear();
            angles.Clear();

            validPosBeforeTransform = false;
            validPosAfterTransform = false;

            foreach (VectorPath p in selection)
            {
                origins.Add(p, new PointF(p.X, p.Y));
                angles.Add(p, p.Angle);

                if (p.IsValidPos)
                    validPosBeforeTransform = true; 
            }

            RectangleF rsel = GetSelBox();

            selCenterX = rsel.X + rsel.Width / 2;
            selCenterY = rsel.Y + rsel.Height / 2;

            selCenter.X = selCenterX;
            selCenter.Y = selCenterY;
        }

        public void EndTransform(bool cancel)
        {
            isTransforming = false;

            foreach (VectorPath p in selection)
            {
                if (p.IsValidPos)
                    validPosAfterTransform = true;

                p.GenerateCurrentScan();
            }

            if (!validPosBeforeTransform && validPosAfterTransform)
            {
                foreach (VectorPath p in selection)
                {
                    p.RestoreGoodPos();
                }
            }

            if (cancel)
            {
                for (int i = 0; i < origins.Count; i++)
                {
                    //selection[i].X = origins[i].X;
                    //selection[i].Y = origins[i].Y;
                    //selection[i].Angle = angles[i];
                }
            }

            host.Refresh();
        }

        public void MoveSelection(float dx, float dy)
        {
            foreach (VectorPath p in selection)
            {
                float px, py;

                px = origins[p].X + dx;
                py = origins[p].Y + dy;

                p.SetPos(px, py);
            }

            foreach (VectorPath p in selection)
            {
                p.CheckDocumentLimits();
                CheckIntersection(p);                
            }
        }

        public void RotateSelection(float angle)
        {
            VectorPath[] paths = new VectorPath[origins.Count];
            PointF[] o = new PointF[origins.Count];
            float[] a = new float[origins.Count];

            int i = 0;
            foreach (VectorPath p in origins.Keys)
            {
                paths[i] = p;
                o[i] = origins[p];
                a[i] = angles[p];

                i++;
            }

            Matrix mt = new Matrix();

            mt.RotateAt(angle, new PointF(selCenterX, selCenterY));
            mt.TransformPoints(o);

            for (i = 0; i < paths.Length; i++)
            {
                paths[i].SetPos(o[i].X, o[i].Y);
                paths[i].Rotate(a[i] + angle);
            }

            foreach (VectorPath p in selection)
            {
                p.CheckDocumentLimits();
                CheckIntersection(p);  
            }
                      
        }

        public bool CheckIntersection(VectorPath p)
        {
            bool i = false;
            
            foreach (VectorPath tp in paths)
            {
                if (tp == p)
                    continue;

                p.Intersects(tp);

                if (p.IsValidPos)
                    i = true;
            }

            if (!i)
            {
                foreach (VectorPath tp in paths)
                {
                    p.UpdateGoodPos();
                }
            }

            return p.IsValidPos;
        }

        public List<VectorPath> FindHitShapes()
        {
            List<VectorPath> ret = new List<VectorPath>();

            foreach (VectorPath p in DocPaths())
            {
                if (p.TestPointInside)
                {
                    ret.Add(p);
                }
            }

            return ret;
        }

        PointF viewCursorPos = new PointF();

        RectangleF GetHitBox(SelectionHitCorner corner)
        {
            RectangleF hit = RectangleF.Empty;

            if (selection.Count > 0)
            {
                RectangleF r = GetViewSelBox();

                float halfSize = hitCornerSize / 2;

                switch (corner)
                {
                    case SelectionHitCorner.TopLeft:
                        hit.X = r.X - hitCornerMargin - halfSize;
                        hit.Y = r.Y - hitCornerMargin - halfSize;                                
                        break;
                    case SelectionHitCorner.TopRight:
                        hit.X = r.Right + hitCornerMargin - halfSize;
                        hit.Y = r.Y - hitCornerMargin - halfSize;
                        break;
                    case SelectionHitCorner.BottomRight:
                        hit.X = r.Right + hitCornerMargin - halfSize;
                        hit.Y = r.Bottom + hitCornerMargin - halfSize;
                        break;
                    case SelectionHitCorner.BottomLeft:
                        hit.X = r.X - hitCornerMargin - halfSize;
                        hit.Y = r.Bottom + hitCornerMargin - halfSize;
                        break;
                }

                hit.Width = hitCornerSize;
                hit.Height = hitCornerSize;
            }

            return hit;
        }

        public void TestSelectionPoint()
        {
            RectangleF box = GetViewSelBox();

            hitCorner = SelectionHitCorner.None;

            if (VectorMath.PointInsideBox(viewCursorPos, GetHitBox(SelectionHitCorner.TopLeft)))
                hitCorner = SelectionHitCorner.TopLeft;

            if (VectorMath.PointInsideBox(viewCursorPos, GetHitBox(SelectionHitCorner.TopRight)))
                hitCorner = SelectionHitCorner.TopRight;

            if (VectorMath.PointInsideBox(viewCursorPos, GetHitBox(SelectionHitCorner.BottomRight)))
                hitCorner = SelectionHitCorner.BottomRight;

            if (VectorMath.PointInsideBox(viewCursorPos, GetHitBox(SelectionHitCorner.BottomLeft)))
                hitCorner = SelectionHitCorner.BottomLeft;

            cursorInsideSelection = false;
            if (VectorMath.PointInsideBox(viewCursorPos, box))
            {
                cursorInsideSelection = true;
            }
        }


        public void SetCursorPos(Point p)
        {
            viewCursorPos = p;
            cursorPos = ViewPointToDocPoint(p);

            foreach (VectorPath path in DocPaths())
            {                 
                 path.TestDocPoint(cursorPos);                    
            }

            TestSelectionPoint();

        }

        RectangleF GetViewSelBox()
        {
            RectangleF r = GetSelBox();

            PointF tc = DocPointToViewPoint(new PointF(r.X, r.Y));
            PointF bc = DocPointToViewPoint(new PointF(r.Right, r.Bottom));

            r.Location = tc;
            r.Width = bc.X - tc.X;
            r.Height = bc.Y - tc.Y;

            r.Inflate(4, 4);

            return r;
        }

        PointF selCenter = new PointF();

        public RectangleF GetSelBox()
        {
            RectangleF sb = RectangleF.Empty;

            if (selection.Count == 0)
                return sb;

            float minx = float.MaxValue;
            float miny = float.MaxValue;
            float maxx = float.MinValue;
            float maxy = float.MinValue;

            foreach (VectorPath s in selection)
            {
                RectangleF r = s.Limits;

                r.X += s.X;
                r.Y += s.Y;

                minx = Math.Min(minx, r.X);
                miny = Math.Min(miny, r.Y);
                maxx = Math.Max(maxx, r.Right);
                maxy = Math.Max(maxy, r.Bottom);
            }

            sb.X = minx;
            sb.Y = miny;
            sb.Width = maxx - minx;
            sb.Height = maxy - miny;

            return sb;
        }

        Pen selPen = null;
        SolidBrush selBrush = null;

        void RenderSelBox(Graphics g)
        {
            g.ResetTransform();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            RectangleF r = GetViewSelBox();
          
            if (selPen == null)
            {
                selPen = new Pen(Color.Black);
                selPen.Width = 1;
                selPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            }

            if (selBrush == null)
            {
                selBrush = new SolidBrush(Color.Black);
            }

            g.DrawRectangle(selPen, r.X, r.Y, r.Width, r.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (transformType == TransformType.Moving)
            {
                selBrush.Color = (hitCorner == SelectionHitCorner.TopLeft) ? Color.OrangeRed : Color.Black;
                g.FillRectangle(selBrush, GetHitBox(SelectionHitCorner.TopLeft));
                selBrush.Color = (hitCorner == SelectionHitCorner.TopRight) ? Color.OrangeRed : Color.Black;
                g.FillRectangle(selBrush, GetHitBox(SelectionHitCorner.TopRight));
                selBrush.Color = (hitCorner == SelectionHitCorner.BottomRight) ? Color.OrangeRed : Color.Black;
                g.FillRectangle(selBrush, GetHitBox(SelectionHitCorner.BottomRight));
                selBrush.Color = (hitCorner == SelectionHitCorner.BottomLeft) ? Color.OrangeRed : Color.Black;
                g.FillRectangle(selBrush, GetHitBox(SelectionHitCorner.BottomLeft));
            }
            else
            {
                selBrush.Color = (hitCorner == SelectionHitCorner.TopLeft) ? Color.OrangeRed : Color.Black;
                g.FillEllipse(selBrush, GetHitBox(SelectionHitCorner.TopLeft));
                selBrush.Color = (hitCorner == SelectionHitCorner.TopRight) ? Color.OrangeRed : Color.Black;
                g.FillEllipse(selBrush, GetHitBox(SelectionHitCorner.TopRight));
                selBrush.Color = (hitCorner == SelectionHitCorner.BottomRight) ? Color.OrangeRed : Color.Black;
                g.FillEllipse(selBrush, GetHitBox(SelectionHitCorner.BottomRight));
                selBrush.Color = (hitCorner == SelectionHitCorner.BottomLeft) ? Color.OrangeRed : Color.Black;
                g.FillEllipse(selBrush, GetHitBox(SelectionHitCorner.BottomLeft));
            }
        }
    }
}
