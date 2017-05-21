﻿using System;
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
            vvCorte.Document.Width = 600;
            vvCorte.Document.Height = 1520;
            vvCorte.AllowScalePath = false;
            vvCorte.AllowMoveDocument = true;

            //vvModelo.DoubleClick += VvModelo_DoubleClick;
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
            frm.ShowInTaskbar = false;
            frm.ShowDialog();

            if (frm.autorizado)
            {
                pnlFiltroInfo.Visible = true;
                pnlprincipal.Visible = true;
                //pnlMapa.Visible = true;
                vvModelo.Visible = true;
                toolArquivo.Visible = true;
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

        void AtualizaCombos()
        {
            sel.Limpar();
            sel.AtualizaMarcas();
            vvModelo.Clear();
        }

        private void cadastrarDesenhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrmCadastroDesenho());
            AtualizaCombos();
        }

        private void cadastrarMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrmCadMarca());
            AtualizaCombos();
        }

        private void cadastroModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrmCadModelo());
            AtualizaCombos();
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

            UpdateViewCorte();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (vvCorte.Document != null)
            {
                if (vvCorte.Document.Paths.Count == 0)
                {
                    MessageBox.Show("Nenhum desenho na área de corte.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    string cmds = vvCorte.Document.ToHPGL();
                    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, cmds);

                    File.WriteAllText("teste_CORTE.plt", cmds);
                }

                //vvCorte.AutoFit(VectorFitStyle.Both, true, true);
            }
        }

        void UpdateImportCount()
        {
            foreach (VectorPath p in vvModelo.Document.Paths)
            {
                p.ImportCount = vvCorte.Document.CountSoruce(p);
            }

            vvModelo.Refresh();
        }

        void UpdateViewCorte()
        {
            vvCorte.ShowGrid = true;

            vvCorte.Width = (int)(vvModelo.Width * 0.75f);
            //vvCorte.Height = (int)(vvModelo.Height * 0.2f);

            if (vvCorte.Document != null)
            {
                float w = vvCorte.Document.ViewPointToDocPoint(new PointF(vvCorte.Width, 0)).X;
                vvCorte.Document.Width = w;

                vvCorte.Document.ShowDocBorder = true;
            }

            vvCorte.Left = vvModelo.Left + (vvModelo.Width - vvCorte.Width) / 2;
            vvCorte.Top = vvModelo.Bottom;// + 10;// - vvCorte.Height - (int)(vvModelo.Height * 0.05f);

            if (vvCorte.Document.Paths.Count > 0)
                vvCorte.Visible = true;
            else
                vvCorte.Visible = false;

            vvCorte.BringToFront();
            vvCorte.Refresh();

            vvCorte.AutoFit(VectorFitStyle.Vertical, false, false);
        }

        private void vvModelo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            vvCorte.ImportSelection(vvModelo);
            //vvCorte.AutoFit(VectorFitStyle.Horizontal, false, true);
            vvCorte.Refresh();

            UpdateImportCount();
            UpdateViewCorte();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vvCorte.Clear();
            vvCorte.Refresh();

            UpdateImportCount();

            UpdateViewCorte();
        }

        private void vvCorte_KeyPress(object sender, KeyPressEventArgs e)
        {
                
        }

        private void vvCorte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                vvCorte.DeleteSelection();
                UpdateImportCount();
                UpdateViewCorte();
            }

            if (e.KeyCode == Keys.Left)
                vvCorte.MoveSelecion(-1, 0);

            if (e.KeyCode == Keys.Right)
                vvCorte.MoveSelecion(+1, 0);

            if (e.KeyCode == Keys.Up)
                vvCorte.MoveSelecion(0, -1);

            if (e.KeyCode == Keys.Left)
                vvCorte.MoveSelecion(0, +1);

            Invalidate();
        }


        void UpdateDocInfo()
        {
            VectorDocument d = vvCorte.Document;

            if (d != null)
            {
                docInfo.Text = "";
                selInfo.Text = "";

                docInfo.Text = string.Format("Tamanho do Desenho (mm): {3} por {4}", d.Paths.Count, d.OffsetX, d.OffsetY, d.Width, d.Height);
                RectangleF r = d.GetBoundRect(true);

                float area = 0;
                foreach (VectorPath p in vvCorte.Selection())
                {
                    area = p.Area;
                }

                if (!float.IsInfinity(r.Width))
                    selInfo.Text = string.Format("Objeto - Tamanho (mm): {0:0.00} por {1:0.00} Área (em mm²): {2:0.00}", r.Width, r.Height, area);
            }
        }

        private void vvCorte_SelectionChanged(object sender, VectorEventArgs e)
        {
            UpdateDocInfo();
        }

        private void vvCorte_SelectionTransformed(object sender, VectorEventArgs e)
        {
            UpdateDocInfo();
            Application.DoEvents();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vvCorte.Document.AutoNest();
        }

        private void cadastroRoloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrnCadRolo());
            AtualizaCombos();
        }

        private void splitDesenho_Panel2_Resize(object sender, EventArgs e)
        {
            vvCorte.AutoFit(VectorFitStyle.Vertical, false, true);
        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            vvCorte_KeyDown(sender, e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
                vvCorte.MoveSelecion(-1, 0);

            if (keyData == Keys.Right)
                vvCorte.MoveSelecion(+1, 0);

            if (keyData == Keys.Up)
                vvCorte.MoveSelecion(0, -1);

            if (keyData == Keys.Down)
                vvCorte.MoveSelecion(0, +1);

            Invalidate();

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void FrmPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
