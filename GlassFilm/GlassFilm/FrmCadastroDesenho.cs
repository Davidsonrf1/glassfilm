using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GlassFilm.Class;

namespace GlassFilm
{
    public partial class FrmCadastroDesenho : Form
    {
        SeletorVeiculo sel = null;

        public FrmCadastroDesenho()
        {
            InitializeComponent();

            sel = new SeletorVeiculo();
            sel.ListaTodas = true;

            sel.CbMarcas = cbMarca;
            sel.CbModelos = cbModelo;
            sel.CbVeiculos = cbAno;
        }

        private void btImportar_Click(object sender, EventArgs e)
        {
            vectorView.Document = new VectorView.VectorDocument();
            vectorView.ShowRuller = false;

            OpenFileDialog opf = new OpenFileDialog();
            opf.DefaultExt = ".svg";
            opf.InitialDirectory = Environment.CurrentDirectory;

            if (opf.ShowDialog() == DialogResult.OK)
            {
                vectorView.Document.LoadSVGFromFile(opf.FileName);
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (sel.VeiculoAtual == null)
                return;

            VectorView.VectorDocument doc = vectorView.Document;

            if (doc != null)
            {
                if (doc.ShapeCount > 0)
                {
                    string svg = doc.ToSVG();
                    DBManager.SalvarDesenho(sel.VeiculoAtual.Id, svg);
                }
            }
        }

        private void FrmCadastroDesenho_Load(object sender, EventArgs e)
        {
            sel.AtualizaMarcas();
            cbMarca.Focus();
        }

        private void cbAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAno.SelectedItem != null)
            {
                Veiculo v = (Veiculo)cbAno.SelectedItem;

                vectorView.Document = null;               

                string svg = DBManager.CarregarDesenho(v.Id);
                if (svg != null)
                {
                    vectorView.Document = new VectorView.VectorDocument();
                    vectorView.Document.LoadSVG(svg);
                }
            }
        }

        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            vectorView.Document = null;
        }

        private void cbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            vectorView.Document = null;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            cbAno.SelectedIndex = -1;
            cbModelo.SelectedIndex = -1;
            cbMarca.SelectedIndex = -1;
            vectorView.Refresh();
        }
    }
}
