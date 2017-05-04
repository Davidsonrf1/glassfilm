using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace VectorView
{
    public enum SelectionHitCorner { None, TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left }

    public partial class VectorViewCtr : UserControl
    {
        VectorDocument document = new VectorDocument();      

        bool isMovingSel = false;
        bool isMovingDoc = false;
        bool showPointer = false;
        bool drawMultiSelectionBox = false;

        bool showScaleCorner = true;

        bool isScaling = false;
        bool isRotating = false;

        bool allowMoveDocument = true;
        bool allowRotatePath = true;
        bool allowScalePath = true;

        List<VectorPath> selection = new List<VectorPath>();
       
        public VectorViewCtr()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        PointF mousePos = new PointF();
        PointF mouseDownPos = new PointF();
        PointF startOffset = new PointF();

        void ClearSelection()
        {
            foreach (VectorPath p in selection)
            {
                p.IsSelected = false;
            }

            selection.Clear();
        }

        RectangleF selBox = new RectangleF();
        PointF selMiddle = new PointF();

        RectangleF BuildSelBox()
        {
            RectangleF sb = new RectangleF();

            if (selection.Count > 0)
            {
                float minx = float.MaxValue;
                float miny = float.MaxValue;
                float maxx = float.MinValue;
                float maxy = float.MinValue;

                foreach (VectorPath p in selection)
                {
                    RectangleF bb = p.GetBoundRect();

                    minx = Math.Min(bb.X, minx);
                    miny = Math.Min(bb.Y, miny);
                    maxx = Math.Max(bb.Right, maxx);
                    maxy = Math.Max(bb.Bottom, maxy);
                }

                selBox.X = sb.X = minx;
                selBox.Y = sb.Y = miny;
                selBox.Width = sb.Width = (maxx - minx);
                selBox.Height = sb.Height = (maxy - miny);
            }
            
            return sb;
        }

        float startAngle = 0;
        PointF transformCenter = new PointF(0, 0);
        RectangleF transformBox = new RectangleF();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (document == null)
                return;

            mousePos.X = e.X;
            mousePos.Y = e.Y;

            mouseDownPos.X = e.X;
            mouseDownPos.Y = e.Y;

            if (hitCorner != SelectionHitCorner.None && allowTransforms)
            {
                BuildSelBox();

                float cx = selBox.X + selBox.Width / 2;
                float cy = selBox.Y + selBox.Height / 2;

                transformCenter = document.DocPointToViewPoint(new PointF(cx, cy));

                transformBox.X = selBox.X;
                transformBox.Y = selBox.Y;
                transformBox.Width = selBox.Width;
                transformBox.Height = selBox.Height;

                foreach (VectorPath pp in selection)
                {
                    pp.BeginTransform(selMiddle);
                }

                if (showScaleCorner)
                {
                    isScaling = true;
                }
                else
                {
                    isRotating = true;
                    startAngle = VectorMath.AngleBetween(transformCenter, mouseDownPos);
                }

                Refresh();
                return;
            }

            VectorPath p = Document.GetPathOnPoint(document.ViewPointToDocPoint(mousePos));

            if (e.Button == MouseButtons.Left)
            {
                if (p == null )
                {
                    drawMultiSelectionBox = true;
                }
                else
                {
                    if (selection.Contains(p))
                    {
                        if (allowRotatePath && allowScalePath)
                        {
                            showScaleCorner = !showScaleCorner;
                        }
                        else
                        {
                            if (allowScalePath)
                                showScaleCorner = true;

                            if (allowRotatePath)
                                showScaleCorner = false;
                        }
                    }
                    else
                    {
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            SelectPath(p);
                        }
                        else
                        {
                            ClearSelection();
                            SelectPath(p);
                        }
                    }

                    BuildSelBox();

                    selMiddle.X = mousePos.X + selBox.Width / 2;
                    selMiddle.Y = mousePos.Y + selBox.Height / 2;

                    foreach (VectorPath pp in selection)
                    {
                        pp.BeginTransform(selMiddle);
                    }

                    isMovingSel = true;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                //if (p == null && allowMoveDocument)
                {
                    isMovingDoc = true;

                    startOffset.X = document.OffsetX;
                    startOffset.Y = document.OffsetY;
                }
            }

            Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape)
            {
                if (document == null)
                    return;

                if (isMovingSel || isRotating || isScaling)
                {
                    foreach (VectorPath p in selection)
                    {
                        p.CancelTransform();
                    }

                    isMovingSel = false;
                    isRotating = false;
                    isScaling = false;

                    Refresh();

                    return;
                }

                ClearSelection();

                if (isMovingDoc)
                {
                    document.OffsetX = startOffset.X;
                    document.OffsetY = startOffset.Y;

                    isMovingDoc = false;
                }
            }

            Refresh();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (document == null)
                return;

            UpdateHitCorner();

            mousePos.X = e.X;
            mousePos.Y = e.Y;

            if (isMovingDoc)
            {
                float dx = 0;
                float dy = 0;

                dx = mouseDownPos.X - e.X;
                dy = mouseDownPos.Y - e.Y;
                
                document.OffsetX = startOffset.X - dx;
                document.OffsetY = startOffset.Y - dy;
            }

            if (isMovingSel)
            {
                float dx = (mouseDownPos.X - mousePos.X) / document.Scale;
                float dy = (mouseDownPos.Y - mousePos.Y) / document.Scale;

                foreach (VectorPath p in selection)
                {
                    p.Move(-dx, -dy);
                }
            }

            if (isScaling)
            {
                Matrix mt = new Matrix();

                float dx = Math.Abs(transformCenter.X - mousePos.X);
                float dy = Math.Abs(transformCenter.Y - mousePos.Y);

                dx = dx / Math.Abs(mouseDownPos.X - transformCenter.X);
                dy = dy / Math.Abs(mouseDownPos.Y - transformCenter.Y);

                float scale = Math.Max(dx, dy);

                mt.Scale(scale, scale);

                foreach (VectorPath p in selection)
                {
                    p.Transform(mt, document.ViewPointToDocPoint(transformCenter));
                }
            }

            if (isRotating)
            {
                Matrix mt = new Matrix();

                float angle = VectorMath.AngleBetween(transformCenter, mousePos) - startAngle;
                mt.Rotate(angle);

                foreach (VectorPath p in selection)
                {
                    p.Transform(mt, document.ViewPointToDocPoint(transformCenter));
                }                      
            }

            Refresh();
        }

        void SelectPath(VectorPath p)
        {
            p.IsSelected = true;

            if (!selection.Contains(p))
                selection.Add(p);
        }            

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (document == null)
                return;

            isMovingDoc = false;
            isMovingSel = false;
            isRotating = false;
            isScaling = false;

            VectorPath p = Document.GetPathOnPoint(document.ViewPointToDocPoint(mousePos));

            if (drawMultiSelectionBox)
            {
                float x = Math.Min(mousePos.X, mouseDownPos.X);
                float y = Math.Min(mousePos.Y, mouseDownPos.Y);

                float w = Math.Abs(mousePos.X - mouseDownPos.X);
                float h = Math.Abs(mousePos.Y - mouseDownPos.Y);

                PointF p1 = document.ViewPointToDocPoint(new PointF(x, y));
                PointF p2 = document.ViewPointToDocPoint(new PointF(x + w, y + h));

                ClearSelection();

                List<VectorPath> sel = document.GetPathsInsideRect(p1, p2);

                foreach (VectorPath ps in sel)
                {
                    SelectPath(ps);
                }

                drawMultiSelectionBox = false;
            }
        }

        float hitCornerTolerance = 2f;
        bool allowTransforms = true;
        SelectionHitCorner hitCorner = SelectionHitCorner.None;

        RectangleF GetCornerRect(SelectionHitCorner corner, RectangleF r)
        {
            RectangleF cr = new RectangleF();

            float t = (hitCornerTolerance + 2);
            float cornerDistance = 3;

            switch (corner)
            {
                case SelectionHitCorner.None:
                    break;
                case SelectionHitCorner.TopLeft:
                    cr.X = r.X - t * 2 - cornerDistance;
                    cr.Y = r.Y - t * 2 - cornerDistance;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Top:
                    cr.X = r.X + (r.Width / 2) - t;
                    cr.Y = r.Y - t * 2 - cornerDistance;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.TopRight:
                    cr.X = r.Right + cornerDistance;
                    cr.Y = r.Y - t * 2 - cornerDistance;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Right:
                    cr.X = r.Right + cornerDistance;
                    cr.Y = r.Y + (r.Height / 2) - t;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.BottomRight:
                    cr.X = r.Right + cornerDistance;
                    cr.Y = r.Y + r.Height + cornerDistance;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Bottom:
                    cr.X = r.X + (r.Width / 2) - t;
                    cr.Y = r.Bottom + cornerDistance;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.BottomLeft:
                    cr.X = r.X - t * 2 - cornerDistance;
                    cr.Y = r.Bottom + cornerDistance;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                case SelectionHitCorner.Left:
                    cr.X = r.X - t * 2 - cornerDistance;
                    cr.Y = r.Y + ((float)r.Height / 2) - t;
                    cr.Width = t * 2;
                    cr.Height = t * 2;
                    break;
                default:
                    break;
            }

            return cr;
        }

        void DrawCornerRect(Graphics g, SelectionHitCorner c, RectangleF r)
        {
            RectangleF rc = GetCornerRect(c, r);

            Brush b = c == hitCorner ? Brushes.Red : Brushes.DarkOliveGreen;

            if (showScaleCorner)
                g.FillRectangle(b, rc.X, rc.Y, rc.Width, rc.Height);
            else
                g.FillEllipse(b, rc.X, rc.Y, rc.Width, rc.Height);
        }

        public void DrawAngle(Graphics g, PointF origin, float size, float angle, Color color)
        {
            Pen p = new Pen(color);
            p.EndCap = LineCap.ArrowAnchor;
            p.Width = 0.001f;

            Pen a = new Pen(color);
            a.Width = p.Width;
            a.DashPattern = new float[] { 2f, 3f };

            float dx = origin.X + (float)Math.Cos(VectorMath.DegreeToRadian(angle)) * size;
            float dy = origin.Y + (float)Math.Sin(VectorMath.DegreeToRadian(angle)) * size;

            g.DrawLine(p, origin.X, origin.Y, dx, dy);
            g.DrawLine(p, origin, new PointF(origin.X + size, origin.Y));

            g.DrawArc(a, origin.X - size / 2, origin.Y - size / 2, size, size, 0, angle);

            SolidBrush sb = new SolidBrush(color);

            g.DrawString(angle.ToString("0.00"), Font, sb, new PointF(origin.X + size, origin.Y));
            g.FillEllipse(sb, origin.X - 4 / 2, origin.Y - 4 / 2, 4, 4);
        }

        bool TestHitCorner(float x, float y, SelectionHitCorner c, RectangleF rect)
        {
            RectangleF r = GetCornerRect(c, rect);

            if (x >= r.X && x <= r.Right && y >= r.Y && y <= r.Bottom)
                return true;

            return false;
        }

        SelectionHitCorner UpdateHitCorner()
        {
            if (!allowTransforms)
                return SelectionHitCorner.None;

            RectangleF r = document.DocRectToViewRect(selBox);
            r.Inflate(selectionMargin, selectionMargin);

            PointF dp = mousePos;

            float x = dp.X;
            float y = dp.Y;

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

        public VectorDocument Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value; Invalidate();
            }
        }

        public bool ShowDocumentLimit
        {
            get
            {
                if (document != null)
                {
                    return document.ShowDocumentLimit;
                }

                return false;
            }

            set
            {
                if (document != null)
                {
                    document.ShowDocumentLimit = value;
                }

                Refresh();
            }
        }

        public bool ShowPointer
        {
            get
            {
                return showPointer;
            }

            set
            {
                showPointer = value; Refresh();
            }
        }

        public bool AllowMoveDocument
        {
            get
            {
                return allowMoveDocument;
            }

            set
            {
                allowMoveDocument = value;
            }
        }

        public bool DrawSelecionBox
        {
            get
            {
                return drawSelecionBox;
            }

            set
            {
                drawSelecionBox = value;
            }
        }

        public float SelctionMargin
        {
            get
            {
                return selectionMargin;
            }

            set
            {
                selectionMargin = value;
            }
        }

        public bool AllowRotatePath
        {
            get
            {
                return allowRotatePath;
            }

            set
            {
                allowRotatePath = value;
            }
        }

        public bool AllowScalePath
        {
            get
            {
                return allowScalePath;
            }

            set
            {
                allowScalePath = value;
            }
        }

        public bool AllowTransforms
        {
            get
            {
                return allowTransforms;
            }

            set
            {
                allowTransforms = value;
            }
        }
        
        bool drawSelecionBox = true;
        float selectionMargin =6f;
        float selDashSize = 3f;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            if (document == null)
            {
                return;
            }

            if (!(allowRotatePath && allowScalePath))
            {
                if (allowScalePath)
                    showScaleCorner = true;

                if (allowRotatePath)
                    showScaleCorner = false;
            }

            document.Render(e.Graphics);

            if (showPointer)
            {
                PointF p = document.ViewPointToDocPoint(mousePos);
                document.DrawPoint(e.Graphics, p);
            }

            if (isMovingSel)
            {

            }

            g.ResetTransform();

            Pen selPen = new Pen(Color.FromArgb(BackColor.ToArgb() ^ 0xffffff));
            selPen.Width = 1f;

            selPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            selPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

            selPen.DashPattern = new float[] { 2, selDashSize, 2, selDashSize };

            if ((selection.Count > 0 && drawSelecionBox) && allowTransforms && !isRotating)
            {
                BuildSelBox();

                RectangleF r = document.DocRectToViewRect(selBox);

                r.Inflate(selectionMargin, selectionMargin);

                g.DrawRectangle(selPen, r.X, r.Y, r.Width, r.Height);

                if (allowTransforms)
                {
                    DrawCornerRect(g, SelectionHitCorner.TopLeft, r);
                    //if (!isRotating) DrawCornerRect(g, SelectionHitCorner.Top, r);
                    DrawCornerRect(g, SelectionHitCorner.TopRight, r);
                    //if (!isRotating) DrawCornerRect(g, SelectionHitCorner.Right, r);
                    DrawCornerRect(g, SelectionHitCorner.BottomRight, r);
                    //if (!isRotating) DrawCornerRect(g, SelectionHitCorner.Bottom, r);
                    DrawCornerRect(g, SelectionHitCorner.BottomLeft, r);
                    //if (!isRotating) DrawCornerRect(g, SelectionHitCorner.Left, r);
                }
            }

            if (isRotating)
            {
                float angle = VectorMath.AngleBetween(transformCenter, mousePos);
                DrawAngle(g, transformCenter, 50, angle, Color.DarkGreen);
            }

            if (drawMultiSelectionBox && !isMovingSel)
            {
                float x = Math.Min(mousePos.X, mouseDownPos.X);
                float y = Math.Min(mousePos.Y, mouseDownPos.Y);

                float w = Math.Abs(mousePos.X - mouseDownPos.X);
                float h = Math.Abs(mousePos.Y - mouseDownPos.Y);

                g.DrawRectangle(selPen, x, y, w, h);
            }

        }

        private void VectorViewCtr_Load(object sender, EventArgs e)
        {

        }

        public VectorPath ImportPath(VectorPath p)
        {
            VectorPath d = null;

            if (document != null)
            {
                d = document.ImportPath(p);
            }

            Invalidate();
            return d;
        }

        public void AutoFit(VectorFitStyle style, bool center, bool fitContent)
        {
            if (document != null)
            {
                document.AutoFit(new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 4, ClientRectangle.Height - 4), style, center, fitContent);
                Refresh();
            }
        }
    }
}
