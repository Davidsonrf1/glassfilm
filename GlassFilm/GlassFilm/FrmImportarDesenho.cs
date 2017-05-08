using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlassFilm
{
    public partial class FrmImportarDesenho : Form
    {
        public FrmImportarDesenho()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.DefaultExt = ".svg";
            ofd.RestoreDirectory = true;
            ofd.Filter = "Arquivo SVG|*.svg";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void FrmImportarDesenho_Load(object sender, EventArgs e)
        {
            
        }
    }
}
