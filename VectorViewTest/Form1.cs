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

        public Form1()
        {
            InitializeComponent();

            //doc.LoadFromFile("G:\\teste.svg");

            VectorShape r = doc.CreateShape();

            r.BeginPath(10, 10);
            r.LineTo(200, 10);
            r.LineTo(200, 400);
            r.LineTo(600, 145);
            r.LineTo(12, 40);
            r.LineTo(10, 200);
            r.ClosePath();

            //doc.Scale = .14f;
            //doc.OffsetX = -100;
            //doc.OffsetY = 100;

            vectorViewCtr1.Document = doc;            
        }
    }
}
