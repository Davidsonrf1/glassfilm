using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace VectorView
{
    public partial class VectorViewCtr : UserControl
    {
        VectorDocument document = null;

        bool allowTransforms = false;
        bool allowMoveDocument = false;
        bool movingDoc = false;
        bool movingSel = false;
        bool rotatingSel = false;

        PointF mouseStart = new PointF();
        PointF mousePos = new PointF();

        float startOffsetX = 0;
        float startOffsetY = 0;

        public VectorViewCtr()
        {
            InitializeComponent();
            DoubleBuffered = true;

            document = new VectorDocument(this);
        }

        public VectorDocument Document
        {
            get
            {
                return document;
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

        public Color GridColor
        {
            get
            {
                return gridColor;
            }

            set
            {
                gridColor = value;
            }
        }

        public int GridSize
        {
            get
            {
                return gridSize;
            }

            set
            {
                gridSize = value;
            }
        }

        public bool ShowGrid
        {
            get
            {
                return showGrid;
            }

            set
            {
                showGrid = value;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        public void DeleteSelection()
        {
            document.DeleteSelection();
        }

        public void MoveSelecion(float dx, float dy)
        {
            Document.MoveSelection(dx, dy);
            Invalidate();

            OnSelectionMoved();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Invalidate();
        }

        float curAngle = 0;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mousePos.X = e.X;
            mousePos.Y = e.Y;

            document.SetCursorPos(e.Location);

            if (movingDoc)
            {
                float dx = e.X - mouseStart.X;
                float dy = e.Y - mouseStart.Y;

                document.OffsetX = startOffsetX + dx;
                document.OffsetY = startOffsetY + dy;

                document.DrawSelBox = false;

                return;
            }

            if (rotatingSel)
            {
                float ma = VectorMath.AngleBetween(rotateCenter, mousePos);
                float angle = startAngle - ma;

                curAngle = -angle;
                Parent.Text = curAngle.ToString("0.00") + "  " + startAngle.ToString("0.00") + "  " + ma.ToString("0.00");

                document.RotateSelection(curAngle);

                document.DrawSelBox = false;
            }

            if (movingSel && document.SelectionCount > 0)
            {
                float dx = 0;
                float dy = 0;

                PointF ds, de;

                ds = document.ViewPointToDocPoint(mouseStart);
                de = document.ViewPointToDocPoint(mousePos);

                dx = de.X - ds.X;
                dy = de.Y - ds.Y;

                document.MoveSelection(dx, dy);

                document.DrawSelBox = false;
            }

            if (document.CursorInsideSelection)
            {
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Cursor = Cursors.Arrow;
            }

            Invalidate();
        }

        float startAngle = 0;
        PointF rotateCenter = new PointF();

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!AllowTransforms)
                return;

            if (document != null && AllowTransforms)
            {
                PointF op = document.ViewPointToDocPoint(new PointF(e.X, e.Y));

                if (e.Delta == 0)
                    return;

                float ds = (e.Delta / Math.Abs(e.Delta));
                float d = (0.04f * ds);

                PointF p1 = Document.DocPointToViewPoint(new PointF(op.X, op.Y));
                Document.Scale += d;

                if (Document.Scale < 0.02f)
                    Document.Scale = 0.02f;

                PointF p2 = Document.DocPointToViewPoint(new PointF(op.X, op.Y));

                float x = p2.X - p1.X;
                float y = p2.Y - p1.Y;

                Document.OffsetX -= x;
                Document.OffsetY -= y;

                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (movingDoc || document.IsTransforming)
                return;

            if (document.SelectionCount == 0)
            {
                if (!AllowTransforms)
                    return;

                List<VectorPath> hits = document.FindHitShapes();

                if (hits.Count > 0)
                {
                    document.SelectPath(hits[0]);

                    movingSel = true;
                    document.BeginTransform();
                    Capture = true;

                    mouseStart.X = e.X;
                    mouseStart.Y = e.Y;

                    return;
                }
            }
            else if(ModifierKeys != Keys.Control)
            {
                List<VectorPath> hits = document.FindHitShapes();

                if (hits.Count > 0)
                {
                    if (!hits[0].Selected)
                    {
                        if (!AllowTransforms)
                            return;

                        document.ClearSelection();
                        document.SelectPath(hits[0]);

                        movingSel = true;
                        document.BeginTransform();
                        Capture = true;

                        mouseStart.X = e.X;
                        mouseStart.Y = e.Y;

                        return;
                    }
                }
            }

            if (document.CursorInsideSelection && document.SelectionCount > 0)
            {
                if (!AllowTransforms)
                    return;

                movingSel = true;
                document.BeginTransform();
                Capture = true;

                mouseStart.X = e.X;
                mouseStart.Y = e.Y;

                return;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (!AllowTransforms)
                    return;

                if (document.HitCorner != SelectionHitCorner.None && document.SelectionCount > 0)
                {
                    document.BeginTransform();

                    mouseStart.X = e.X;
                    mouseStart.Y = e.Y;

                    rotateCenter = document.DocPointToViewPoint(document.SelCenter);

                    rotatingSel = true;
                    startAngle = VectorMath.AngleBetween(rotateCenter, mouseStart);

                    curAngle = startAngle;
                }
            }

            List<VectorPath> hit = document.FindHitShapes();

            if(e.Button == MouseButtons.Right)
            {
                if (allowMoveDocument)
                {
                    mouseStart.X = e.X;
                    mouseStart.Y = e.Y;

                    startOffsetX = document.OffsetX;
                    startOffsetY = document.OffsetY;

                    movingDoc = true;
                    Capture = true;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (movingDoc && e.Button == MouseButtons.Right)
            {
                movingDoc = false;
                Capture = false;
                document.DrawSelBox = true;
                return;
            }

            if (movingSel && e.Button == MouseButtons.Left)
            {
                movingSel = false;
                Capture = false;
                document.DrawSelBox = true;

                document.EndTransform(false);

                OnSelectionMoved();

                return;
            }

            if (rotatingSel && e.Button == MouseButtons.Left)
            {
                rotatingSel = false;
                Capture = false;
                document.DrawSelBox = true;

                document.EndTransform(false);
                OnSelectionTransformed();

                return;
            }

            if (movingDoc || document.IsTransforming)
                return;

            List<VectorPath> hit = document.FindHitShapes();

            if (e.Button == MouseButtons.Left)
            {
                if (hit.Count > 0)
                {
                    List<VectorPath> tmp = new List<VectorPath>();

                    foreach (VectorPath p in hit)
                    {
                        if (p.Selected)
                            tmp.Add(p);
                    }

                    foreach (VectorPath p in tmp)
                    {
                        hit.Remove(p);
                    }

                    if (ModifierKeys != Keys.Control && hit.Count > 0)
                    {
                        document.ClearSelection();
                    }

                    if (document.SelectionCount == 0)
                    {
                        
                    }
                    else
                    {

                    }

                    foreach (VectorPath p in hit)
                    {
                        document.SelectPath(p);
                    }

                    foreach (VectorPath p in tmp)
                    {
                        //document.UnselectPath(p);
                    }
                }
                else
                {
                    document.ClearSelection();
                }
            }
        }

        Color gridColor = Color.DarkGray;
        int gridSize = 8;
        bool showGrid = false;

        void DrawGrid(Graphics g)
        {
            Pen p = new Pen(Color.FromArgb(15, GridColor));

            for (int i = 0; i < Width; i += gridSize)
            {
                g.DrawLine(p, i, 0, i, Height);
            }

            for (int i = 0; i < Height; i += gridSize)
            {
                g.DrawLine(p, 0, i, Width, i);
            }
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

            try
            {
                g.DrawArc(a, origin.X - size / 2, origin.Y - size / 2, size, size, 0, angle);
            }
            catch
            {

            }

            SolidBrush sb = new SolidBrush(color);

            g.DrawString(angle.ToString("0.00"), Font, sb, new PointF(origin.X + size, origin.Y));
            g.FillEllipse(sb, origin.X - 4 / 2, origin.Y - 4 / 2, 4, 4);
        }

        public void ImportSelection(VectorViewCtr src)
        {
            VectorDocument d = src.Document;

            if (d != null)
            {
                foreach (VectorPath p in src.Document.Selection)
                {
                    VectorPath path = document.ImportPath(p);
                }
            }

            

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(BackColor);

            if (showGrid)
                DrawGrid(g);

            document.Render(e.Graphics);

            g.ResetTransform();

            if (rotatingSel)
            {
                DrawAngle(g, rotateCenter, 50, curAngle, Color.OrangeRed);
            }
        }

        public event VectorEventHandler DocumentMoved;
        public virtual void OnDocumentMoved()
        {
            if (DocumentMoved != null)
                DocumentMoved(this, new VectorEventArgs(document, null));
        }

        public event VectorEventHandler SelectionMoved;
        public virtual void OnSelectionMoved()
        {
            if (SelectionMoved != null)
                SelectionMoved(this, new VectorEventArgs(document, null));
        }

        public event VectorEventHandler SelectionTransformed;
        public virtual void OnSelectionTransformed()
        {
            if (SelectionTransformed != null)
                SelectionTransformed(this, new VectorEventArgs(document, null));
        }

        public event VectorEventHandler SelectionChanged;
        public virtual void OnSelectionChanged()
        {
            if (SelectionChanged != null)
                SelectionChanged(this, new VectorEventArgs(document, null));
        }

        public void AutoFit()
        {
            document.AutoFit(ClientRectangle, VectorFitStyle.Both, true, VectorFitRegion.Document, 15);
        }

        public void AutoFit(VectorFitStyle style, bool center, bool fitContent)
        {
            if (document != null)
            {
                document.AutoFit(new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 4, ClientRectangle.Height - 4), style, center, fitContent ? VectorFitRegion.Content : VectorFitRegion.Document, 10);
                Refresh();
            }
        }

        public void Clear()
        {
            document.Clear();
        }

        private void VectorViewCtr_Load(object sender, EventArgs e)
        {

        }
    }

    public delegate void VectorEventHandler(object sender, VectorEventArgs e);

    public class VectorEventArgs : EventArgs
    {
        VectorDocument document = null;
        VectorPath path = null;

        public VectorEventArgs(VectorDocument doc, VectorPath path)
        {
            document = doc;
            this.path = path;
        }

        public VectorDocument Document
        {
            get
            {
                return document;
            }

            internal set
            {
                document = value;
            }
        }

        public VectorPath Path
        {
            get
            {
                return path;
            }

            internal set
            {
                path = value;
            }
        }
    }
}
