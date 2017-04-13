using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorView;


namespace VectorViewTest
{
    public partial class Form1 : Form
    {
        VectorDocument doc = new VectorDocument();

        float x1 = 600, y1 = 10, x2 = 10, y2 = 300;
        //float x1 = 130, y1 = 60, x2 = 10, y2 = 60;
        float mx, my;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawLine(Pens.OrangeRed, x1, y1, x2, y2);

            PointF cross;

            if (VectorMath.HorizontalCrossPoint(x1, y1, x2, y2, my, out cross))
            {
                e.Graphics.FillEllipse(Brushes.Black, cross.X - 3, cross.Y - 3, 6, 6);
                e.Graphics.DrawString(cross.ToString(), Font, Brushes.Red, 10, 10);

                e.Graphics.DrawLine(Pens.Gray, 0, my, Width, my);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mx = e.X;
            my = e.Y;

            Invalidate();
        }

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            vectorViewCtr1.Document = doc;

            /*
            OpenFileDialog opf = new OpenFileDialog();
            opf.DefaultExt = ".svg";
            opf.InitialDirectory = Environment.CurrentDirectory;

            if (opf.ShowDialog() == DialogResult.OK)
            {
                doc.LoadSVGFromFile(opf.FileName);
            }
            */

            doc.LoadSVGFromFile("PALIO 4 PORTAS  ANO  2011 A  2016.svg");
            doc.Scale = 0.05f;
        }
    }
}
