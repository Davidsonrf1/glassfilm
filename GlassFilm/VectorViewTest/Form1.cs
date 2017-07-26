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
            //vv.Document.LoadSVGFromFile("d:\\teste_desenho.svg");

            DoubleBuffered = true;
            vv.Document.LoadSVGFromFile("D:\\COROLLA SEDAN ANO 2009 A 2014 (16).svg");

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
                //p.DrawNormalizedPath = true;
                //p.DrawTestPoint = true;
                //p.DrawCenterPoint = true;
                //p.DrawBoundBox = true;
                //p.DrawCurGrid = true;
                //p.FillOnPointInside = true;

                p.GenerateCutShape(sheet);
                path = p;

                //cs.GenerateFromPath(p);

                break;
            }

            time = Environment.TickCount - time;

            MessageBox.Show(time.ToString());

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

            res.resultOK = false;
            CutLibWrapper.TestShape(sheet, shape, ref res);
            
            if (res.resultOK)
            {
                CutLibWrapper.Plot(sheet, shape, res.angle, res.x, res.y);

                path.SetPos(res.x, res.y);
                path.Rotate(res.angle);                
            }
            

            res.resultOK = false;
            CutLibWrapper.TestShape(sheet, shape, ref res);

            if (res.resultOK)
            {
                CutLibWrapper.Plot(sheet, shape, res.angle, res.x, res.y);

                path.SetPos(res.x, res.y);
                path.Rotate(res.angle);                
            }
            

            res.resultOK = false;
            CutLibWrapper.TestShape(sheet, shape, ref res);
            
            if (res.resultOK)
            {
                CutLibWrapper.Plot(sheet, shape, res.angle, res.x, res.y);

                path.SetPos(res.x, res.y);
                path.Rotate(res.angle);

                limits = path.Limits;
            }
            
            i = Environment.TickCount - i;

            Text = i.ToString();

            vv.AllowMoveDocument = true;
            vv.AutoFit();

            //vv.Document.AutoFit(vv.ClientRectangle, VectorFitStyle.Both, true, VectorFitRegion.Document, 100);
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

            Pen p = new Pen(Color.Yellow);
            p.LineJoin = System.Drawing.Drawing2D.LineJoin.Miter;
            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            List<PointF[]> poly = path.GetTransfomedPolygons();
            foreach (PointF[] pts in poly)
            {
                e.Graphics.DrawPolygon(Pens.Yellow, pts);
            }
            
            //e.Graphics.DrawRectangle(Pens.Orange, limits.X+path.X, limits.Y+path.Y, limits.Width, limits.Height);
            e.Graphics.FillEllipse(Brushes.YellowGreen,  path.X-3, path.Y-3, 6, 6);
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

            Refresh();
            */
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
    }
}
