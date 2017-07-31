using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VectorView;

namespace VectorViewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        uint sheet = 0;
        uint shape = 0;

        VectorPath path = null;
        CutLibWrapper.CutTestResult res = new CutLibWrapper.CutTestResult();
        public static ImageList imgList = new ImageList();
        RectangleF limits = new RectangleF();

        private void Form1_Load(object sender, EventArgs e)
        {
            vv.Document.LoadSVGFromFile("d:\\teste_desenho.svg");

            DoubleBuffered = true;
            //vv.Document.LoadSVGFromFile("D:\\COROLLA SEDAN ANO 2009 A 2014 (16).svg");

            int time = Environment.TickCount;
            File.WriteAllText("D:\\out.svg", vv.Document.ToSVG());
            time = Environment.TickCount - time;

            vv.Document.OffsetX = 0;
            vv.Document.OffsetY = 0;

            vv.Document.Debbuging = false;
            vv.Document.ShowDocBorder = true;
            vv.Document.ShowDocInfo = true;

            vv.AllowTransforms = true;
            vv.ShowGrid = true;

            sheet = CutLibWrapper.CreateSheet(600);
            shape = CutLibWrapper.CreateShape(sheet, 1);

            time = Environment.TickCount;

            foreach (VectorPath p in vv.Document.DocPaths())
            {
                p.Shape = p.GenerateCutShape(sheet);
                p.Sheet = sheet;
            }

            time = Environment.TickCount - time;

            //MessageBox.Show(time.ToString());

            res.x = 123;
            res.y = 321;

            //pictureBox1.Image = images[0];
            int i = Environment.TickCount;

            //CutLibWrapper.Plot(sheet, shape, 0, 100, 40);
            //CutLibWrapper.Plot(sheet, shape, 90, 370, 120);
            //CutLibWrapper.Plot(sheet, shape, 180, 470, 120);

            //path.Translate(340, 120);
            //path.SetPos(340, 120);

            /*
            CutLibWrapper.TestShape(sheet, shape, ref res);

            if (res.resultOK)
            {
                CutLibWrapper.Plot(sheet, shape, res.angle, res.x, res.y);
                path.SetPos(res.x, res.y);
                path.Rotate(res.angle);
            }
            */

            i = Environment.TickCount - i;

            Text = i.ToString();

            vv.AllowMoveDocument = true;
            vv.AutoFit();

            //vv.Document.AutoFit(vv.ClientRectangle, VectorFitStyle.Both, true, VectorFitRegion.Document, 100);
        }

        public VectorPath GetPathByMaxArea(VectorDocument doc)
        {
            VectorPath p = null;

            foreach (VectorPath vi in doc.Paths)
            {
                if (!vi.Nested)
                {
                    if (p == null)
                    {
                        p = vi;
                    }
                    else
                    {
                        if (vi.Area > p.Area)
                            p = vi;
                    }
                }
            }

            return p;
        }

        public void NestPaths(VectorDocument doc, int size, uint sheet)
        {
            CutLibWrapper.CutTestResult res = new CutLibWrapper.CutTestResult();

            CutLibWrapper.ResetSheet(sheet, size);

            doc.DocHeight = size;

            foreach (VectorPath pi in doc.Paths)
            {
                pi.Nested = false;
            }

            VectorPath p = null;

            while ((p = GetPathByMaxArea(doc)) != null)
            {
                res.resultOK = false;
                CutLibWrapper.TestShape(sheet, p.Shape, ref res);

                if (res.resultOK)
                {
                    CutLibWrapper.Plot(sheet, p.Shape, res.angle, res.x, res.y);

                    p.SetPos(res.x, res.y);
                    p.Rotate(res.angle);
                }

                p.Nested = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            IntPtr dc = e.Graphics.GetHdc();

            CutLibWrapper.RenderSheet(sheet, 0, 00, dc);
            //CutLibWrapper.RenderScan(sheet, shape, 0, 150, 150, dc);
            e.Graphics.ReleaseHdc(dc);

            //e.Graphics.RotateTransform(res.angle);
            //e.Graphics.TranslateTransform(path.X, path.Y-120);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        }

        List<Bitmap> images = new List<Bitmap>();

        private void vectorViewCtr1_Load(object sender, EventArgs e)
        {

        }

        private void vv_Load(object sender, EventArgs e)
        {

        }


        private void vv_MouseClick(object sender, MouseEventArgs e)
        {


        }

        int angle = 0;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            
            /*
            CutLibWrapper.ResetSheet(sheet, 1280);
            CutLibWrapper.Plot(sheet, shape, angle, 150, 190);

            path.SetPos(150, 190);
            path.Rotate(angle);

            limits = path.Limits;

            angle = ++angle % 360;

            Refresh();*/
            
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

        }

        private void vv_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (VectorPath p in vv.Document.DocPaths())
            {
               //p.Rotate(-e.X);
               //p.Translate(1, 0);
            }

        }

        private void vv_SelectionChanged(object sender, VectorEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NestPaths(vv.Document, 150, sheet);
            vv.Refresh();

            Refresh();
        }
    }
}
