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

        public Form1()
        {
            InitializeComponent();

            view.Parent = this;
            view.Visible = true;
            view.Width = 600;
            view.Height = 300;
            view.BackColor = Color.White;
            view.Dock = DockStyle.Fill;

            VectorDocument d = view.Document;
            
            //d.Scale = 0.03f;
            d.LoadSVGFromFile("H:\\desenhos\\PEUGEOT 206 E 207  4 PORTAS ANO 2001 A  2015.svg");
            //d.LoadSVGFromFile("H:\\teste.svg");
            
            d.Width = 600;
            d.Height = 300;
                        
            d.AdjustSizeToContent();
            view.AutoFit(VectorFitStyle.Both, true, false);
            view.ShowDocumentLimit = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            view.AutoFit(VectorFitStyle.Both, true, false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
