using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            view.Dock = DockStyle.Fill;

            view.DocementMoved += View_DocementMoved;
            view.SelectionMoved += View_SelectionMoved;
            view.SelectionTransformed += View_SelectionTransformed;
            view.SelectionChanged += View_SelectionChanged;

            view.AllowScalePath = false;
            view.AllowMoveDocument = false;

            corte.Parent = this;
            corte.Visible = true;
            corte.Width = 600;
            corte.Height = 200;
            corte.BackColor = Color.White;
            corte.Dock = DockStyle.Bottom;
            corte.ShowDocumentLimit = true;
            
            corte.Document.Width = 600;
            corte.Document.Height = 300;
        }

        private void View_SelectionChanged(object sender, VectorEventArgs e)
        {
            UpdateDocInfo();
        }

        private void View_SelectionTransformed(object sender, VectorEventArgs e)
        {
            UpdateDocInfo();
        }

        private void View_SelectionMoved(object sender, VectorEventArgs e)
        {
            UpdateDocInfo();
        }

        void UpdateDocInfo()
        {
            VectorDocument d = view.Document;

            if (d != null)
            {
                docInfo.Text = string.Format("Peças: {0}, Scala Visual: {1:0.00}, X: {2:0.00} Y: {3:0.00}", d.Paths.Count, d.Scale, d.OffsetX, d.OffsetY);
                RectangleF r = d.GetBoundRect(true);
                selInfo.Text = string.Format("Largura: {2:0.00}mm,    Altura: {3:0.00}mm", r.X + r.Width / 2, r.Y + r.Height / 2, r.Width, r.Height);
            }
        }

        private void View_DocementMoved(object sender, VectorEventArgs e)
        {
            UpdateDocInfo();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (view.Document.Paths.Count > 0)
                view.Document.DrawPath(e.Graphics, Pens.Red, view.Document.Paths[0], new PointF(300, 300), .2f, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            view.AutoFit(VectorFitStyle.Both, true, true);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VectorDocument d = view.Document;
            d.LoadSVGFromFile("D:\\COROLLA SEDAN ANO 2009 A 2014.svg", 1);
            //d.LoadSVGFromFile(@"D:\teste.svg");
            //d.AdjustSizeToContent();
            view.AutoFit(VectorFitStyle.Both, true, true);
            view.ShowDocumentLimit = true;

            VectorPath p = corte.Document.ImportPath(d.Paths[0]);

            corte.AutoFit(VectorFitStyle.Both, true, true);

            d.Width = 600;
            d.Height = 300;
            
            VectorPath vp = d.Paths[0];

            float area = vp.ComputeArea(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = view.Document.ToHPGL();
            File.WriteAllText( @"D:\\teste.plt", s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            corte.ImportSelection(view);
        }
    }
}
