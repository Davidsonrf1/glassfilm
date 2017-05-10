using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using GlassFilm.Class;
using System.IO;
using VectorView;

namespace GlassFilm
{
    public partial class FrmPrincipal : Form
    {
        SeletorVeiculo sel = null;

        public FrmPrincipal()
        {
            InitializeComponent();

            sel = new SeletorVeiculo();
            sel.ListaTodas = false;

            sel.CbMarcas = cbMarca;
            sel.CbModelos = cbModelo;
            sel.CbVeiculos = cbAno;

            vvCorte.Document = new VectorDocument();

            vvModelo.DoubleClick += VvModelo_DoubleClick;
        }

        private void VvModelo_DoubleClick(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {           
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = ColorTranslator.FromHtml("#f7f6f6");
                }
            }

            FrmLogin frm = new FrmLogin();            
            frm.ShowDialog();

            if (frm.autorizado)
            {
                pnlFiltroInfo.Visible = true;
                pnlprincipal.Visible = true;
                //pnlMapa.Visible = true;
                splitDesenho.Visible = true;
            }

            sel.AtualizaMarcas();
            cbMarca.Focus();
        }

        private void cortadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfigPlotter conf = new FrmConfigPlotter();

            if (conf.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void cadastrarDesenhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrmCadastroDesenho());
        }

        private void cadastrarMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrmCadMarca());  
        }

        private void cadastroModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrmCadModelo());
        }       

        private void cbAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAno.SelectedItem != null)
            {
                Veiculo v = (Veiculo)cbAno.SelectedItem;

                vvModelo.Document = null;

                string svg = DBManager.CarregarDesenho(v.Id);
                if (svg != null)
                {
                    vvModelo.Document = new VectorView.VectorDocument();

                    vvModelo.AllowTransforms = false;

                    vvModelo.Document.LoadSVG(svg);

                    vvModelo.AutoFit(VectorFitStyle.Both, true, true);
                }
            }
        }

        private void vvModelo_Resize(object sender, EventArgs e)
        {
            vvModelo.AutoFit(VectorFitStyle.Both, true, true);
            Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (vvModelo.Document != null)
            {
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    string cmds = vvModelo.Document.ToHPGL();
                    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, cmds);

                    File.WriteAllText("teste.plt", cmds);
                }
            }
        }

        private void vvModelo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            vvCorte.ImportSelection(vvModelo);
            vvCorte.AutoFit(VectorFitStyle.Vertical, false, true);
        }
    }
}
