using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                document.MouseDown(e.X, e.Y);
            }

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (document != null)
            {
                document.MouseUp(e.X, e.Y);
            }

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (document != null)
            {
                document.MouseMove(e.X, e.Y);
            }

            Invalidate();
        }

        private void VectorViewCtr_Load(object sender, EventArgs e)
        {

        }
    }
}
