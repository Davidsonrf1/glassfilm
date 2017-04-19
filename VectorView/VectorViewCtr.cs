using System;
using System.Drawing;
using System.Windows.Forms;

namespace VectorView
{
    public partial class VectorViewCtr : UserControl
    {
        VectorDocument document = null;
        int rullerWidth = 22;
        Color rullerLineColor = Color.DarkRed;
        Color rullerColor = Color.White;

        bool showRuller = false;

        VectorViewFitStyle fitStyle = VectorViewFitStyle.None;

        public VectorViewCtr()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        public VectorDocument Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
                Invalidate();
            }
        }

        public bool ShowRuller
        {
            get
            {
                return showRuller;
            }

            set
            {
                showRuller = value;
            }
        }

        public VectorViewFitStyle FitStyle
        {
            get
            {
                return fitStyle;
            }

            set
            {
                fitStyle = value; AutoFit();
            }
        }

        void DrawRuller(Graphics g, bool vertical, int len, float scale)
        {
            Rectangle r = new Rectangle(-1, -1, rullerWidth, rullerWidth);

            float x1, y1, x2, y2, l;
            x1 = r.Right;
            y1 = r.Bottom;

            Pen p = new Pen(rullerLineColor);
            p.Width = 1;

            if (!vertical)
            {
                r.Width = len + 2;
                x2 = r.Right;
                y2 = r.Bottom;
                l = len + r.Width;
            }
            else
            {
                r.Height = len + 2;
                x2 = r.Right;
                y2 = r.Bottom + 2;
                l = len + r.Height;
            }

            SolidBrush b = new SolidBrush(rullerColor);

            g.FillRectangle(b, r);
            g.DrawLine(p, x1, y1, x2, y2);     
            
            float step = 2f * 1;
            int stepCount = (int)(len / step) + 1;
            float start = 0;

            for (int i = 0; i < stepCount; i++)
            {
                float ll = rullerWidth / 7;

                if (i % 10 == 0)
                    ll = rullerWidth / 2;
                else if (i % 5 == 0)
                    ll = rullerWidth / 3;

                if (!vertical)
                {
                    start = 0;

                    x1 = x2 = i * step;
                    y1 = 0;
                    y2 = ll;
                }
                else
                {
                    start = 0;
                    y1 = y2 = i * step;
                    x1 = 0;
                    x2 = ll;
                }

                g.DrawLine(p, x1, y1, x2, y2);
            }
        }

        void DrawHRuller(Graphics g)
        {
            DrawRuller(g, false, Width, Document.Scale);
        }

        void DrawVRuller(Graphics g)
        {
            DrawRuller(g, true, Height, Document.Scale);
        }

        void DrawRullers(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            if (showRuller)
            {
                DrawVRuller(g);
                DrawHRuller(g);
            }

            Rectangle r = new Rectangle(-1, -1, rullerWidth, rullerWidth);
            SolidBrush b = new SolidBrush(rullerLineColor);
            Pen p = new Pen(rullerLineColor);
            p.Width = 0.5f;

            float x, y, w;

            x = r.X + r.Width / 2f;
            y = r.Y + r.Height / 2f;
            w = r.Width / 2f;

            //g.FillEllipse(b, x - 2, y - 2, 4f, 4f);

            //g.DrawLine(p, x - w / 2, y, x + w/2, y);
            //g.DrawLine(p, x, y - w / 2, x, y + w/2);

            //g.FillRectangle(Brushes.White, r);

            b.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (document != null)
            {
                document.RenderDocument(g);
            }
            
            g.ResetTransform();

            DrawRullers(g);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (document != null)
            {
                document.MouseDown(e.X, e.Y, e.Button);
                Invalidate();
            }            
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (document != null)
            {
                document.MouseUp(e.X, e.Y, e.Button);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (document != null)
            {
                document.MouseMove(e.X, e.Y);
                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (document != null)
            {
                document.MouseWheel(e.Delta, e.X, e.Y);
                Invalidate();
            }
        }

        private void VectorViewCtr_Load(object sender, EventArgs e)
        {

        }

        public void AutoFit(VectorViewFitStyle style=VectorViewFitStyle.Both)
        {
            float scale = 1;
            fitStyle = style;

            if (Document == null)
                return;

            RectangleF r = Document.GetDocSize();
            r.Inflate(100, 100);

            switch (fitStyle)
            {
                case VectorViewFitStyle.None:
                    return;
                case VectorViewFitStyle.Vertical:
                    scale = Height / r.Height;
                    break;
                case VectorViewFitStyle.Horizontal:
                    scale = Width / r.Width;
                    break;
                case VectorViewFitStyle.Both:

                    if (Width < Height)
                    {
                        scale = Width / r.Width;
                    }
                    else
                    {
                        scale = Height / r.Height;
                    }

                    break;
            }

            Document.Scale = scale;

            Document.OffsetX = ((Width / 2) - (r.Width / 2) * scale) * Document.InverseScale;
            Document.OffsetY = ((Height / 2) - (r.Height / 2) * scale) * Document.InverseScale;
        }

        private void VectorViewCtr_Resize(object sender, EventArgs e)
        {
            
        }
    }

    public enum VectorViewFitStyle { None, Vertical, Horizontal, Both }
}
