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

        //float x1 = 600, y1 = 10, x2 = 10, y2 = 300;
        float x1 = 10, y1 = 600, x2 = 600, y2 = 10;
        float mx, my;

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

            vectorViewCtr1.Visible = false;

            //doc.LoadFromFile("G:\\teste.svg");

            VectorShape r = doc.CreateShape();

            r.BeginPath(10, 10);
            r.LineTo(200, 10);
            r.LineTo(200, 400);
            r.LineTo(600, 145);
            r.LineTo(12, 40);
            r.LineTo(10, 200);
            r.EndPath();

            doc.Scale = 1f;
            //doc.OffsetX = -100;
            //doc.OffsetY = 100;

            vectorViewCtr1.Document = doc;            
        }
    }
}
