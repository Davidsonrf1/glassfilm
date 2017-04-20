using System;
using System.IO;
using System.Windows.Forms;
using VectorView;


namespace VectorViewTest
{
    public partial class Form1 : Form
    {
        VectorDocument doc = new VectorDocument();

        float mx, my;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mx = e.X;
            my = e.Y;

            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vectorViewCtr1.Document != null)
            {
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    string cmds = vectorViewCtr1.Document.ToHPGL();
                    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, cmds);

                    File.WriteAllText("teste.plt", cmds);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            vectorViewCtr1.Document = doc;
            vectorViewCtr1.ShowRuller = true;

            if (!File.Exists("Tech.svg"))
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.DefaultExt = ".svg";
                opf.InitialDirectory = Environment.CurrentDirectory;

                if (opf.ShowDialog() == DialogResult.OK)
                {
                    doc.LoadSVGFromFile(opf.FileName);
                }
            }
            else
            {
                doc.LoadSVGFromFile("Tech.svg");
            }

            doc.Scale = 0.5f;
            string s = doc.ToSVG();
            File.WriteAllText("D:\\teste.svg", s);
        }
    }
}
