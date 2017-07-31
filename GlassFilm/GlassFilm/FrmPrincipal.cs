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
using System.Diagnostics;
using VectorView.Plotter;
using System.Drawing.Printing;
using System.IO.Ports;
using GlassFilm.Sync;

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

            vvCorte.Document.DocWidth = 600;
            vvCorte.Document.DocHeight = 1520;

            vvCorte.AllowMoveDocument = true;

            AtualizaFilmes();

            cbFilme.Text = "";

            int roloPadrao = Convert.ToInt32(Program.Config.GetValue("FilmePadrao", "0"));

            for (int i = 0; i < cbFilme.Items.Count; i++)
            {
                Filme f = (Filme)cbFilme.Items[i];

                if (f.Id == roloPadrao)
                {
                    cbFilme.SelectedItem = f;
                    filmeAtual = f;

                    vvCorte.Document.DocHeight = filmeAtual.Largura;
                    break;
                }
            }

            UpdateLBTamanho();
        }

        Filme filmeAtual = null;

        NestManager nestManager = null;

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {            
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = ColorTranslator.FromHtml("#f7f6f6");
                }
            }

            if (!Debugger.IsAttached)
            {
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
                    toolCorte.Visible = true;
                }
            }
            else
            {
                pnlFiltroInfo.Visible = true;
                pnlprincipal.Visible = true;
                //pnlMapa.Visible = true;
                vvModelo.Visible = true;
                toolArquivo.Visible = true;
                toolCorte.Visible = true;
            }

            SyncManager.SyncTables.AddRange(new string[] { "MODELO", "MARCA", "MODELO_ANO", "ROLO", "!DESENHOS" });
            SyncManager.Synckeys.AddRange(new string[] { "CODIGO_MODELO", "CODIGO_MARCA", "CODIGO_ANO", "ID", "CODIGO_DESENHO" });

            SyncManager.CheckTables();
            
            if (Debugger.IsAttached)
            {
                SyncManager.Syncronize(SyncType.Outgoing);
            }

            //SyncManager.Syncronize(Sync.SyncType.Incoming);

            sel.AtualizaMarcas();
            cbMarca.Focus();
        }

        private void cortadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                ModeloAno m = (ModeloAno)cbAno.SelectedItem;

                vvModelo.Document.Clear();

                int codigo_desenho = 0;
                string svg = DBManager.CarregarDesenho(Convert.ToInt32(m.Codigo_ano), out codigo_desenho);
                if (svg != null)
                {
                    vvModelo.AllowTransforms = false;

                    vvModelo.Document.LoadSVG(svg);

                    vvModelo.AutoFit(VectorFitStyle.Both, true, true);
                }
            }
        }

        private void vvModelo_Resize(object sender, EventArgs e)
        {
            vvModelo.AutoFit();
            Invalidate();

            UpdateViewCorte();
        }

        public PlotterDriver GetPlotterCmdDriver()
        {
            if (Program.Config["PlotterLang"].Equals("HPGL"))
            {
                return new PlotterHPGL();
            }

            if (Program.Config["PlotterLang"].Equals("DMPL") && Debugger.IsAttached )
            {
                return new PlotterDMPL();
            }
            else
            {
                throw new NotImplementedException("Linguagem DMPL não implementada");
            }

            throw new NotImplementedException("Linguagem " + Program.Config["PlotterLang"] + " não implementada");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool invertXY = true;
            bool flip = false;

            if (vvModelo.Document == null)
            {
                MessageBox.Show("Nenhum desenho selecionado.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (vvModelo.Document.Paths.Count <= 0)
            {
                MessageBox.Show("Nenhum desenho selecionado.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(Program.Config["PlotterInterface"].Equals("SERIAL") || Program.Config["PlotterInterface"].Equals("PRINTER")))
            {
                if (MessageBox.Show("Plotter não configurada. Deseja configurar agora?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FrmConfigPlotter c = new GlassFilm.FrmConfigPlotter();
                    c.ShowDialog();
                }
                else
                {
                    MessageBox.Show("É necessário configurar a plotter antes de continuar", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (vvCorte.Document != null)
            {
                if (vvCorte.Document.Paths.Count == 0)
                {
                    MessageBox.Show("Nenhum desenho na área de corte.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmds = null;

                try
                {
                    cmds = vvCorte.Document.GeneratePlotterCommands(GetPlotterCmdDriver(), invertXY, flip);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Program.Config["PlotterInterface"].Equals("PRINTER"))
                {
                    PrinterSettings ps = null;

                    foreach (string p in PrinterSettings.InstalledPrinters)
                    {
                        if (p.Equals(Program.Config["PlotterName"]))
                        {
                            ps = new PrinterSettings();
                            ps.PrinterName = p;
                            break;
                        }
                    }

                    if (ps == null)
                    {
                        PrintDialog pd = new PrintDialog();
                        if (pd.ShowDialog() == DialogResult.OK)
                        {
                            ps = pd.PrinterSettings;
                            Program.Config["PlotterName"] = ps.PrinterName;
                        }
                    }

                    if (ps != null)
                    {
                        if (ps.IsValid)
                        {
                            RawPrinterHelper.SendStringToPrinter(ps.PrinterName, cmds);
                        }
                        else
                        {
                            PrintDialog pd = new PrintDialog();
                            if (pd.ShowDialog() == DialogResult.OK)
                            {
                                ps = pd.PrinterSettings;
                                Program.Config["PlotterName"] = ps.PrinterName;

                                RawPrinterHelper.SendStringToPrinter(ps.PrinterName, cmds);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Plotter inválda ou não especificada nos parâmetros", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else if (Program.Config["PlotterInterface"].Equals("SERIAL"))
                {
                    try
                    {
                        SerialPort port = FrmCadSerial.GetSerialPort();

                        if (port != null)
                        {
                            port.Open();    
                            port.Write(cmds);
                            port.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Falha ao enviar comandos para a Plotter", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Plotter inválda ou não especificada nos parâmetros", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                if (Debugger.IsAttached || File.Exists("teste_corte.plt"))
                {
                    File.Delete("teste_corte.plt");
                    File.WriteAllText("teste_corte.plt", cmds);
                }
            }
        }

        void UpdateImportCount()
        {
            if (vvModelo.Document == null)
                return;

            foreach (VectorPath p in vvModelo.Document.Paths)
            {
                p.ImportCount = vvCorte.Document.CountSource(p);
            }

            vvModelo.Refresh();

            UpdateLBTamanho();
            vvCorte.Width = splitCorte.Panel2.Width - toolCorte.Width;
        }

        void UpdateViewCorte()
        {
            vvCorte.ShowGrid = true;
            vvCorte.Width = (int)(vvModelo.Width * 0.75f);

            vvCorte.Document.ShowDocBorder = false;
            vvCorte.Document.ShowDocInfo = false;

            if (vvCorte.Document != null)
            {
                float w = vvCorte.Document.ViewPointToDocPoint(new PointF(vvCorte.Width, 0)).X;
                vvCorte.Document.DocWidth = w;

                vvCorte.Document.ShowDocBorder = true;
                vvCorte.Document.ShowDocInfo = true;
            }

            vvCorte.Left = vvModelo.Left + (vvModelo.Width - vvCorte.Width) / 2;
            vvCorte.Top = vvModelo.Bottom;

            bool bVis = vvCorte.Visible;

            if (vvCorte.Document.Paths.Count > 0)
                vvCorte.Visible = true;
            else
                vvCorte.Visible = false;

            if (bVis != vvCorte.Visible && bVis)
            {
                if (filmeAtual != null)
                {
                   
                }
            }

            vvCorte.Document.AutoFit(vvCorte.ClientRectangle, VectorFitStyle.Both, false, VectorFitRegion.Document, 80);
            vvCorte.Document.OffsetX = 20;
            vvCorte.Document.OffsetY = 20;

            vvCorte.BringToFront();
            vvCorte.Refresh();

            UpdateLBTamanho();

            vvCorte.Width = splitCorte.Panel2.Width - toolCorte.Width;
        }

        private void vvModelo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (filmeAtual == null)
            {
                Mensagens.Atencao("Nenhum filem selecionado!");
                return;
            }

            if (nestManager == null)
            {
                nestManager = new NestManager(filmeAtual.Largura);
            }

            //vvCorte.ImportSelection(vvModelo);

            foreach (VectorPath p in vvModelo.Document.Selection)
            {
                VectorPath ip = vvCorte.Document.ImportPath(p);

                nestManager.RegisterPath(ip);
                if (nestManager.NestPath(ip))
                {

                }
                else
                {

                }
            }

            if (vvCorte.Document.Paths.Count == 1)
                vvCorte.AutoFit();

            vvCorte.Refresh();

            UpdateImportCount();
            UpdateViewCorte();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nestManager != null)
            {
                int size = 1000;

                if (filmeAtual != null)
                    size = filmeAtual.Largura;

                nestManager.ResetSheet(size);
            }

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

                docInfo.Text = string.Format("Tamanho do Desenho (mm): {3} por {4}", d.Paths.Count, d.OffsetX, d.OffsetY, d.DocWidth, d.DocHeight);
                RectangleF r = d.GetBoundRect();

                float area = 0;
                foreach (VectorPath p in vvCorte.Document.Selection)
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

        public VectorPath GetPathByMaxArea(VectorDocument doc)
        {
            VectorPath p = null;

            foreach (VectorPath vi in doc.Paths)
            {
                if (!vi.Nested)
                {
                    if (p == null)
                    {
                        p = vi;
                    }
                    else
                    {
                        if (vi.Area > p.Area)
                            p = vi;
                    }
                }
            }

            return p;
        }

        public void NestPaths(VectorDocument doc, int size)
        {
            doc.DocHeight = size;

            foreach (VectorPath pi in doc.Paths)
            {
                pi.Nested = false;
            }

            VectorPath p = null;
            while ((p = GetPathByMaxArea(doc)) != null)
            {
                if (nestManager.NestPath(p))
                {

                }
                else
                {

                }
                p.Nested = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (filmeAtual == null)
            {
                Mensagens.Atencao("Nenhum filme selecionado!");
                return;
            }

            if (nestManager == null)
            {
                nestManager = new NestManager(filmeAtual.Largura);
            }
            else
            {
                nestManager.ResetSheet(filmeAtual.Largura);
            }

            NestPaths(vvCorte.Document, filmeAtual.Largura);

            vvCorte.Refresh();
            Refresh();
        }

        void AtualizaFilmes()
        {
            List<Filme> filmes = DBManager.CarregarRolos();

            cbFilme.Items.Clear();

            foreach (Filme f in filmes)
            {
                cbFilme.Items.Add(f);
            }
        }

        private void cadastroRoloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowDialog(new FrnCadRolo());
            AtualizaFilmes();
        }

        private void splitDesenho_Panel2_Resize(object sender, EventArgs e)
        {
            vvCorte.AutoFit();
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

        private void vvCorte_Resize(object sender, EventArgs e)
        {
            vvCorte.AutoFit();
        }

        void UpdateLBTamanho()
        {
            VectorDocument doc = vvCorte.Document;

            if (doc.DocWidth > 0)
                lbTamanho.Text = string.Format("{0:0.00} X {1:0.00}", doc.DocHeight, doc.DocWidth);
            else
                lbTamanho.Text = "";
        }

        private void vvCorte_SelectionTransformed_1(object sender, VectorEventArgs e)
        {
            //UpdateViewCorte();

            if (vvCorte.Document != null)
            {
                VectorDocument doc = vvCorte.Document;
            }

            UpdateLBTamanho();
        }

        private void vvCorte_SelectionMoved(object sender, VectorEventArgs e)
        {
            //UpdateViewCorte();

            if (vvCorte.Document != null)
            {
                VectorDocument doc = vvCorte.Document;
            }

            UpdateLBTamanho();
        }

        private void cbFilme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilme.SelectedItem != null)
            {
                Filme filme = (Filme)cbFilme.SelectedItem;

                Program.Config["FilmePadrao"] = filme.Id.ToString();

                filmeAtual = filme;

                vvCorte.Document.DocHeight = filmeAtual.Largura;

                if (nestManager != null)
                    nestManager.ResetSheet(filmeAtual.Largura);

                vvCorte.Refresh();
            }

            UpdateLBTamanho();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (vvCorte.Document != null)
            {
                vvCorte.Document.Scale += 0.05f;
                vvCorte.Refresh();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (vvCorte.Document != null)
            {
                vvCorte.Document.Scale -= 0.05f;

                if (vvCorte.Document.Scale < 0.02f)
                    vvCorte.Document.Scale = 0.02f;

                vvCorte.Refresh();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (vvCorte.Document != null)
            {
                vvCorte.Document.AutoFit(vvCorte.ClientRectangle, VectorFitStyle.Both, false, VectorFitRegion.Document, 80);
                vvCorte.Document.OffsetX = 20;
                vvCorte.Document.OffsetY = 20;
                vvCorte.Refresh();
            }
        }

        private void splitCorte_Panel2_Resize(object sender, EventArgs e)
        {
            vvCorte.Width = splitCorte.Panel2.Width - toolCorte.Width;
        }

        private void toolCorte_Resize(object sender, EventArgs e)
        {
            vvCorte.Width = splitCorte.Panel2.Width - toolCorte.Width;
        }

        private void FrmPrincipal_Shown(object sender, EventArgs e)
        {
            vvCorte.Width = splitCorte.Panel2.Width - toolCorte.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmConfigPlotter c = new GlassFilm.FrmConfigPlotter();
            c.ShowDialog();
        }
    }
}
