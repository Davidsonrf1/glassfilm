using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VectorView;

namespace VectorViewTeste
{
    public partial class Form1 : Form
    {
        VectorViewCtr view = new VectorViewCtr();
        VectorViewCtr corte = new VectorViewCtr();

        public Form1()
        {
            InitializeComponent();

            view.Parent = this;
            view.Visible = true;
            view.Width = 600;
            view.Height = 300;
            view.Left = 400;
            view.BackColor = Color.White;
            view.Dock = DockStyle.Top;

            corte.Parent = this;
            corte.Visible = true;
            corte.Width = 600;
            corte.Height = 200;
            corte.BackColor = Color.White;
            corte.Dock = DockStyle.Bottom;
            corte.ShowDocumentLimit = true;
            
            corte.Document.Width = 600;
            corte.Document.Height = 300;

            VectorDocument d = view.Document;

            
            //d.LoadSVGFromFile("D:\\tmp\\PALIO-4-PORTAS-ANO-2011-A-2016.svg");
            d.LoadSVGFromFile("D:\\teste.svg");

            //VectorPath p = d.ImportPath(d.Paths[0]);

            VectorPath p = corte.Document.ImportPath(d.Paths[1]);
            corte.Document.AdjustSizeToContent();
            corte.AutoFit(VectorFitStyle.Both, true, true);
            p.SetOrigin(new PointF(30, 60));

            d.Width = 600;
            d.Height = 300;                        
            
            view.AutoFit(VectorFitStyle.Both, true, false);
            view.ShowDocumentLimit = true;

            d.AdjustSizeToContent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            view.Document.DrawPath(e.Graphics, Pens.Red, view.Document.Paths[0], new PointF(300, 300), .2f, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            view.AutoFit(VectorFitStyle.Both, true, false);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
