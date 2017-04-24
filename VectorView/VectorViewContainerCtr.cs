using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VectorView
{
    public partial class VectorViewContainerCtr : UserControl
    {
        int rullerWidth = 25;
        Color rullerLineColor = Color.DarkRed;
        Color rullerColor = Color.White;
        Pen rullerLine = new Pen(Color.DarkRed);
        
        bool showRuller = true;

        public VectorViewContainerCtr()
        {
            InitializeComponent();
            AdjustView();

            DoubleBuffered = true;
        }

        public VectorViewCtr View
        {
            get
            {
                return view;
            }
        } 

        public VectorDocument Document
        {
            get
            {
                return view.Document;
            }

            set
            {
                view.Document = value;
            }
        }

        public int RullerWidth
        {
            get
            {
                return rullerWidth;
            }

            set
            {
                rullerWidth = value;
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
                showRuller = value; AdjustView();
            }
        }

        void AdjustView()
        {
            if (showRuller)
            {
                view.Left = rullerWidth;
                view.Top = rullerWidth;
                view.Width = Width - rullerWidth;
                view.Height = Height - rullerWidth;
            }
            else
            {
                view.Left = 0;
                view.Top = 0;
                view.Width = Width;
                view.Height = Height;
            }
        }



        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustView();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustView();
        }

        void DrawRuller(PointF origin, bool vertical)
        {
            float o = 0;

            if (!vertical)
            {
                o = -origin.X;
            }
            else
            {

            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (Document != null)
            {
                if (showRuller)
                {
                    PointF p = Document.MouseState.Pos;
                    p = Document.DocumentToViewPoint(p.X, p.Y);

                    PointF o = new PointF(Document.OffsetX, Document.OffsetY);

                    Pen pen = new Pen(Brushes.LightCoral);
                    pen.Width = 0.1f;

                    DrawRuller(o, false);

                    g.DrawLine(pen, p.X + rullerWidth, 0, p.X + rullerWidth, rullerWidth);
                    g.DrawLine(pen, 0, p.Y + rullerWidth, rullerWidth, p.Y + rullerWidth);

                    pen.Color = Color.LightBlue;

                    if (o.X >= 0)
                        g.DrawLine(pen, o.X + rullerWidth, 0, o.X + rullerWidth, rullerWidth);

                    if (o.Y >= 0)
                        g.DrawLine(pen, 0, o.Y + rullerWidth, rullerWidth, o.Y + rullerWidth);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate();
        }

        private void view_MouseMove(object sender, MouseEventArgs e)
        {
            Invalidate();
        }

        private void view_MouseEnter(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void view_MouseLeave(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
