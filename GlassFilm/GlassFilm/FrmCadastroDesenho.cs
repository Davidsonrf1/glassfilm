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

            vectorView.AllowTransforms = false;
            vectorView.AllowMoveDocument = false;

            OpenFileDialog opf = new OpenFileDialog();
            opf.DefaultExt = ".svg";
            opf.InitialDirectory = Environment.CurrentDirectory;

            if (opf.ShowDialog() == DialogResult.OK)
            {
                vectorView.Document.LoadSVGFromFile(opf.FileName);
                vectorView.AutoFit(VectorView.VectorFitStyle.Both, true, true);
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (sel.VeiculoAtual == null)
                return;

            VectorView.VectorDocument doc = vectorView.Document;

            if (doc != null)
            {
                if (doc.Paths.Count > 0)
                {
                    string svg = doc.ToSVG();
                    DBManager.SalvarDesenho(sel.VeiculoAtual.Id, svg);

                    Mensagens.Informacao("Desenho Salvo com Sucesso!");
                    toolStripButton1_Click(sender, e);
                    cbMarca.Focus();
                }
            }            
        }

        private void FrmCadastroDesenho_Load(object sender, EventArgs e)
        {
            sel.AtualizaMarcas();
            cbMarca.Focus();
            // Impelmentação do Autofit, permitir bloquear transformações.
            /*
            vectorView.Document = new VectorView.VectorDocument();
            vectorView.Document.AllowTransforms = false;
            vectorView.Document.AllowMove = false;
            vectorView.Document.AllowZoom = false;
            vectorView.Document.LoadSVGFromFile(@"D:\tmp\PALIO-4-PORTAS-ANO-2011-A-2016.svg");
            vectorView.AutoFit(VectorView.VectorViewFitStyle.Vertical);
            */
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

                    vectorView.AllowTransforms = false;

                    vectorView.Document.LoadSVG(svg);

                    vectorView.AutoFit(VectorView.VectorFitStyle.Both, true, true);
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

        private void FrmCadastroDesenho_Resize(object sender, EventArgs e)
        {
            vectorView.AutoFit(VectorView.VectorFitStyle.Both, true, true);
        }
    }
}
