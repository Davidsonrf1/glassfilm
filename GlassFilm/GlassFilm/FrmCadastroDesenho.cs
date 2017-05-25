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

            cbModelo.SelectedIndexChanged += cbModelo_SelectedIndexChanged;

            vectorView.AllowTransforms = false;
        }

        void EnableControls(bool enable)
        {
            cbMarca.Enabled = enable;
            cbModelo.Enabled = enable;

            lbAnos.Enabled = enable;

            nEscala.Enabled = enable;
        }

        private void btImportar_Click(object sender, EventArgs e)
        {
            if (lbAnos.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Selecione um ou mais anos para importar o desenho", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                float scale = (float)(100 / nEscala.Value);

                vectorView.Document.LoadSVGFromFile(opf.FileName, scale);
                vectorView.Document.Normalize();

                vectorView.AutoFit(false);               

                EnableControls(false);
            }

            UpdateDocInfo();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (lbAnos.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Nenhum ano selecionado", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            VectorDocument doc = vectorView.Document;

            pbDesenho.Value = 0;
            pbDesenho.Maximum = lbAnos.CheckedItems.Count;
            pbDesenho.Visible = true;

            Application.DoEvents();

            if (doc != null)
            {
                if (doc.Paths.Count > 0)
                {
                    string svg = doc.ToSVG();

                    foreach (object i in lbAnos.CheckedItems)
                    {
                        Veiculo v = (Veiculo)i;
                        DBManager.SalvarDesenho(v.Id, svg);

                        pbDesenho.Value++;
                        Application.DoEvents();
                    }

                   // File.WriteAllText("d:\\teste_save.svg", svg);

                    Mensagens.Informacao("Desenho Salvo com Sucesso!");
                    pbDesenho.Visible = false;

                    toolStripButton1_Click(sender, e);
                    cbMarca.Focus();
                }
            }
            else
            {
                MessageBox.Show("Nenhum desenho carregado!", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            pbDesenho.Visible = false;
            lbAnos.Items.Clear();

            rbEsquerda.Checked = false;
            rbDireita.Checked = false;
            tbEtiqueta.Text = "";
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
            /*
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
            */
        }

        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            vectorView.Document = null;
        }

        void MontarListaAnos()
        {
            if (sel.ModeloAtual == null)
                return;

            List<Veiculo> veic = DBManager.CarregarVeiculos(sel.ModeloAtual.Id, true);
            
            lbAnos.Items.Clear();
            foreach (Veiculo v in veic)
            {
                lbAnos.Items.Add(v);
            }
        }

        private void cbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            vectorView.Document = null;
            MontarListaAnos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            cbModelo.SelectedIndex = -1;
            cbMarca.SelectedIndex = -1;
            vectorView.Refresh();

            EnableControls(true);
            vectorView.Clear();
        }

        private void FrmCadastroDesenho_Resize(object sender, EventArgs e)
        {
            vectorView.AutoFit();
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

        VectorPath curPath = null;

        private void vectorView_SelectionChanged(object sender, VectorView.VectorEventArgs e)
        {
            UpdateDocInfo();

            curPath = null;

            rbEsquerda.Checked = false;
            rbDireita.Checked = false;
            tbEtiqueta.Text = "";

            if (vectorView.SelecionCount == 1)
            {
                rbEsquerda.Enabled = true;
                rbDireita.Enabled = true;
                tbEtiqueta.Enabled = true;

                foreach (VectorPath v in vectorView.Selection())
                {
                    curPath = v;
                }

                if (curPath != null)
                {
                    switch (curPath.Side)
                    {
                        case VectorPathSide.None:
                            rbEsquerda.Checked = false;
                            rbDireita.Checked = false;
                            break;
                        case VectorPathSide.Left:
                            rbEsquerda.Checked = true;
                            break;
                        case VectorPathSide.Right:
                            rbDireita.Checked = true;
                            break;
                    }

                    tbEtiqueta.Text = curPath.Tag;
                }
            }
            else
            {
                rbEsquerda.Enabled = false;
                rbDireita.Enabled = false;
                tbEtiqueta.Enabled = false;
            }
        }

        private void pnlFiltroInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbAnos_SelectedValueChanged(object sender, EventArgs e)
        {
            vectorView.Clear();

            if (lbAnos.SelectedItem != null)
            {
                Veiculo v = (Veiculo)lbAnos.SelectedItem;

                string svg = DBManager.CarregarDesenho(v.Id);
                if (svg != null)
                {
                    vectorView.Document.LoadSVG(svg);
                    vectorView.AllowTransforms = false;

                    vectorView.AutoFit(false);
                    UpdateDocInfo();
                }
            }
        }

        private void rbEsquerda_CheckedChanged(object sender, EventArgs e)
        {
            if (curPath != null)
            {
                curPath.Side = VectorPathSide.None;
                if (rbEsquerda.Checked)
                {
                    curPath.Side = VectorPathSide.Left;
                }

                if (rbDireita.Checked)
                {
                    curPath.Side = VectorPathSide.Right;
                }
            }

            vectorView.Refresh();
        }

        private void tbEtiqueta_TextChanged(object sender, EventArgs e)
        {
            if (curPath != null)
            {
                curPath.Tag = tbEtiqueta.Text;
            }

            vectorView.Refresh();
        }
    }
}
