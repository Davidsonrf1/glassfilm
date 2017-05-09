using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GlassFilm.Class;
using VectorView;

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

        void EnableControls(bool enable)
        {
            cbAno.Enabled = enable;
            cbMarca.Enabled = enable;
            cbModelo.Enabled = enable;
        }

        private void btImportar_Click(object sender, EventArgs e)
        {
            if (sel.VeiculoAtual == null)
            {
                MessageBox.Show("Selecione o veiculo para importar o desenho.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            vectorView.Document = new VectorView.VectorDocument();

            vectorView.AllowTransforms = false;
            vectorView.AllowMoveDocument = false;
            vectorView.AllowTransforms = false;

            OpenFileDialog opf = new OpenFileDialog();
            opf.DefaultExt = ".svg";
            opf.RestoreDirectory = true;

            if (opf.ShowDialog() == DialogResult.OK)
            {
                float scale = (float)(100 / numEscala.Value);

                vectorView.Document.LoadSVGFromFile(opf.FileName, scale);
                vectorView.AutoFit(VectorView.VectorFitStyle.Both, true, true);

                EnableControls(false);
            }

            UpdateDocInfo();
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

                    File.WriteAllText("d:\\teste_save.svg", svg);

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
                    UpdateDocInfo();
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

            EnableControls(true);
            vectorView.Clear();
        }

        private void FrmCadastroDesenho_Resize(object sender, EventArgs e)
        {
            vectorView.AutoFit(VectorView.VectorFitStyle.Both, true, true);
        }

        void UpdateDocInfo()
        {
            VectorDocument d = vectorView.Document;

            if (d != null)
            {
                docInfo.Text = "";
                selInfo.Text = "";
                
                docInfo.Text = string.Format("Tamanho do Desenho (mm): {3} por {4}", d.Paths.Count, d.OffsetX, d.OffsetY, d.Width, d.Height);
                RectangleF r = d.GetBoundRect(true);

                float area = 0;
                foreach (VectorPath p in vectorView.Selection())
                {
                    area = p.Area;
                }

                if (!float.IsInfinity(r.Width))
                    selInfo.Text = string.Format("Objeto - Tamanho (mm): {0:0.00} por {1:0.00} Área (em mm²): {2:0.00}", r.Width, r.Height, area);
            }
        }

        private void vectorView_SelectionChanged(object sender, VectorView.VectorEventArgs e)
        {
            UpdateDocInfo();
        }
    }
}
