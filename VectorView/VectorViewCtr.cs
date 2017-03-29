using System;
using System.Windows.Forms;

namespace VectorView
{
    public partial class VectorViewCtr : UserControl
    {
        VectorDocument document = null;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (document != null)
            {
                document.RenderDocument(e.Graphics);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (document != null)
            {
                document.MouseDown(e.X, e.Y, e.Button);

                //if (document.NeedRedraw)
                    Invalidate();
            }            
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (document != null)
            {
                document.MouseUp(e.X, e.Y, e.Button);
                //if (document.NeedRedraw)
                    Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (document != null)
            {
                document.MouseMove(e.X, e.Y);
                //if (document.NeedRedraw)
                    Invalidate();
            }
        }

        private void VectorViewCtr_Load(object sender, EventArgs e)
        {

        }
    }
}
