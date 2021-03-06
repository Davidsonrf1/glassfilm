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
using System.Threading;
using GlassFilm.Validacoes;
using System.Net;

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
            sel.TipoDesenho = TipoDesenho.WindowTint;

            cbTipo.SelectedIndex = 0;

            sel.CbMarcas = cbMarca;
            sel.CbModelos = cbModelo;
            sel.CbVeiculos = cbAno;
            sel.CbTipo = cbTipo;

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

            //if (!Debugger.IsAttached)
            //{
                FrmLogin frm = new FrmLogin();
                frm.ShowInTaskbar = false;
                frm.ShowDialog();

                if (frm.autorizado)
                {
                    pnlFiltroInfo.Visible = true;
                    pnlprincipal.Visible = true;
                    //pnlMapa.Visible = true;
                    vvModelo.Visible = true;                   
                    toolCorte.Visible = true;

                    if (Glass.usuario.master!=null && Glass.usuario.master!="null" && Glass.usuario.master.Equals("S"))
                    {
                        toolArquivo.Visible = true;
                        toollSincronizacao.Visible = true;
                    }

                    Glass.usuario.nome = frm.txtNome.Text;

                    string mensagemRetorno = ConexaoValidaLogin.mensalidadeAVencer();
                    if (mensagemRetorno.Length > 0)
                    {
                        Mensagens.Informacao(mensagemRetorno);
                    }

                    mensagemRetorno = ConexaoValidaLogin.mensalidadeVencida();
                    if (mensagemRetorno.Length > 0)
                    {
                        Mensagens.Informacao(mensagemRetorno);
                    }

                    mensagemRetorno = ConexaoValidaLogin.mensalidadeAtrasada();
                    if (mensagemRetorno.Length > 0)
                    {                        
                        Mensagens.Atencao(mensagemRetorno);
                        Application.Exit();
                    }
                    
                }
            //}
            //else
            //{
            //    pnlFiltroInfo.Visible = true;
            //    pnlprincipal.Visible = true;
            //    //pnlMapa.Visible = true;
            //    vvModelo.Visible = true;
            //    toolCorte.Visible = true;
            //    toolArquivo.Visible = true;
            //    toollSincronizacao.Visible = true;

            //}


            DBManager.VerificaTabelasAuxiliares();

            /*
            SyncManager.SyncTables.AddRange(new string[] { "ELIMINA_REGISTRO", "MODELO", "MARCA", "MODELO_ANO", "ROLO", "!DESENHOS" });
            SyncManager.Synckeys.AddRange(new string[] { "ID", "CODIGO_MODELO", "ID", "CODIGO_ANO", "ID", "VEICULO" });

            SyncManager.SyncStatus = new UpdateSyncStatus(SyncStatusProc);
            SyncManager.CheckTables();

            if (Debugger.IsAttached)
            {
                //SyncManager.Syncronize(SyncType.Outgoing);
            }            
            */

            lbQtde.Text = sel.AtualizaMarcas() + " desenhos cadastrados sendo\n" + DBManager.GetNumVeiculoMarca();

            cbMarca.Focus();
            calculapalavra();
        }

        private void calculapalavra()
        {
            int w = (pnlCalculando.Size.Width/2)- lbCalculando.Size.Width;
            lbCalculando.Location = new Point(w, lbCalculando.Location.Y);           
        }

        private void SyncStatusProc(SyncStatus status)
        {
            /*
            if (status.Show && !pbSync.Visible)
            {
                lbStatusSyn.Visible = true;
                pbSync.Visible = true;

                pbSync.Maximum = 100;
                pbSync.Value = 0;

                lbStatusSyn.Text = "Sincronizando...";
            }
            else
            {
                lbStatusSyn.Visible = false;
                pbSync.Visible = false;
            }

            if (status.Total > 0)
            {
                pbSync.Maximum = status.Total;
                pbSync.Value = status.Atual;

                lbStatusSyn.Text = "Sincronizando...";

                pbSync.Invalidate();
            }

            Application.DoEvents();
            */
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

        string svgPPV = null;
        string svgWindowtint = null;

        void CarregaSvg(string svg)
        {
            vvModelo.Clear();

            if (svg != null)
            {
                string marca = "";
                string modelo = "";
                string ano = "";

                marca = cbMarca.Text;
                modelo = cbModelo.Text;
                ano = cbAno.Text;

                vvModelo.AllowTransforms = false;

                vvModelo.Document.LoadSVG(svg);
                vvModelo.Document.Marca = marca;
                vvModelo.Document.Modelo = modelo;
                vvModelo.Document.Ano = ano;

                lbQtde.Text = DBManager.GetNumDesenhos() + " desenhos cadastrados sendo\n" + DBManager.GetNumVeiculoMarca();

                vvModelo.AutoFit(VectorFitStyle.Both, true, true);
            }
        }

        private void cbAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAno.SelectedItem != null)
            {
                loadpanel("Carregando novo Modelo de Corte...");

                ModeloAno m = (ModeloAno)cbAno.SelectedItem;

                vvModelo.Document.Clear();

                int codigo_desenho = 0;

                string svg = DBManager.CarregarDesenho(Convert.ToInt32(m.Codigo_ano), out codigo_desenho, sel.TipoDesenho);
                CarregaSvg(svg);

                pnlCalculando.Visible = false;
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

            if (Program.Config["PlotterLang"].Equals("DMPL"))
            {
                return new PlotterDMPL();
            }

            throw new NotImplementedException("Linguagem " + Program.Config["PlotterLang"] + " não implementada");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pnlprincipal.Enabled = false;           
            loadpanel("Enviando para Corte, Aguarde...");

            try
            {
                bool invertXY = true;
                bool flip = false;

                try { invertXY = bool.Parse(Program.Config["RotateCut"]); } catch { }
                try { flip = bool.Parse(Program.Config["FlipX"]); } catch { }

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
                            loadpanelRecortar();

                            DBManager.GravaLogCorte(vvCorte.Document);
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
                                loadpanelRecortar();
                                DBManager.GravaLogCorte(vvCorte.Document);
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Falha ao enviar comandos para a Plotter\n" + ex.Message, "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch
            {

            }
            finally
            {
                pnlprincipal.Enabled = true;
                pnlCalculando.Visible = false;
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
                int margin = 0;
                try { margin = 2 * int.Parse(Program.Config["margin"]); } catch { }

                nestManager = new NestManager(filmeAtual.Largura - margin);
            }

            //vvCorte.ImportSelection(vvModelo);

            foreach (VectorPath p in vvModelo.Document.Selection)
            {
                Cursor = Cursors.WaitCursor;
                pnlprincipal.Enabled = false;
                vvCorte.Enabled = false;              
                loadpanel("Adicionando Peça ao Mapa, Aguarde...");

                VectorPath ip = vvCorte.Document.ImportPath(p);
                nestManager.RegisterPath(ip);

                bool forceNest = false;
                try { forceNest = bool.Parse(Program.Config["forceAutoNest"]); } catch { }
                bool forceAngle = false;

                if (forceNest)
                {
                    forceAngle = false; // Isso vai fazer o autonest procurar em todos os ângulos
                }
                else
                {
                    forceAngle = p.ForceAngle; // Respeita o que está defindo no cadastro.
                }

                ip.ForceAngle = forceAngle;

                if (!nestManager.NestPath(ip))
                {
                    vvCorte.Document.Paths.Remove(ip);
                    Mensagens.Atencao("Peça muito grande para ser cortada neste filme!");

                    ip.Source.Imported = false;

                    continue;                    
                }

                UpdateDocInfo();
                pnlprincipal.Enabled = true;
                pnlCalculando.Visible = false;
            }

            Cursor = Cursors.Arrow;
            vvCorte.Enabled = true;
            pnlprincipal.Enabled = true;

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
                nestManager.ClearSheet();

                int size = 1000;

                if (filmeAtual != null)
                    size = filmeAtual.Largura;

                int margin = 0;
                try { margin = 2 * int.Parse(Program.Config["margin"]); } catch { }

                nestManager.ResetSheet(size - margin);
            }

            foreach (VectorPath p in vvCorte.Document.Paths)
            {
                if (p.Source != null)
                    p.Source.Imported = false;
            }

            vvCorte.Clear();
            vvCorte.Refresh();
            vvModelo.Refresh();

            UpdateImportCount();
            UpdateViewCorte();
            UpdateDocInfo();

            GC.Collect();
            GC.WaitForPendingFinalizers();
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

                vvModelo.Refresh();

                if (nestManager != null)
                    nestManager.Replot(vvCorte.Document.Paths);

                UpdateDocInfo();
            }

            Invalidate();
        }
        
        void UpdateDocInfo()
        {
            VectorDocument d = vvCorte.Document;

            if (d != null)
            {
                d.CalcMetrics();

                lbTamanho.Text = string.Format("Largura: {0:0.00}m   Comprimento: {1:0.00}m", d.DocHeight / 1000, d.GetMaxX() / 1000);
                lbAreaFilme.Text = string.Format("Filme total: {0:0.00}m\xB2", d.DocArea / 1000000);
                lbAreaUsada.Text = string.Format("Filme usado: {0:0.00}m\xB2  Eficiência: {1:0.00}%", d.UsedArea / 1000000, d.Efficiency*100);
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
                bool forceNest = false;
                try { forceNest = bool.Parse(Program.Config["forceAutoNest"]); } catch { }
                bool forceAngle = false;
                bool oldForceAngle = p.ForceAngle;

                if (forceNest)
                {
                    forceAngle = false; // Isso vai fazer o autonest procurar em todos os ângulos
                }
                else
                {
                    forceAngle = p.ForceAngle; // Respeita o que está defindo no cadastro.
                }

                p.ForceAngle = forceAngle;

                if (nestManager.NestPath(p))
                {
                    p.Nested = true;
                }

                p.ForceAngle = oldForceAngle;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            pnlprincipal.Enabled = false;            
            loadpanel("Ajustando Peças no Mapa, Aguarde...");

            if (filmeAtual == null)
            {
                Mensagens.Atencao("Nenhum filme selecionado!");
                pnlCalculando.Visible = false;
                return;
            }

            if (nestManager == null)
            {
                nestManager = new NestManager(filmeAtual.Largura);
            }
            else
            {
                int margin = 0;
                try { margin = 2 * int.Parse(Program.Config["margin"]); } catch { }
                
                nestManager.ResetSheet(filmeAtual.Largura - margin);
            }

            NestPaths(vvCorte.Document, filmeAtual.Largura);

            vvCorte.Refresh();
            Refresh();

            pnlprincipal.Enabled = true;
            pnlCalculando.Visible = false;

            UpdateDocInfo();
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


        private void vvCorte_SelectionTransformed_1(object sender, VectorEventArgs e)
        {
            UpdateViewCorte();

            if (vvCorte.Document != null)
            {
                VectorDocument doc = vvCorte.Document;
            }

            if (nestManager != null)
                nestManager.Replot(vvCorte.Document.Paths);

            UpdateDocInfo();
        }

        private void vvCorte_SelectionMoved(object sender, VectorEventArgs e)
        {
            //UpdateViewCorte();

            if (vvCorte.Document != null)
            {
                VectorDocument doc = vvCorte.Document;
            }

            if (nestManager != null)
                nestManager.Replot(vvCorte.Document.Paths);   

            UpdateDocInfo();
        }

        private void cbFilme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilme.SelectedItem != null)
            {
                pnlprincipal.Enabled = false;                
                loadpanel("Alterando o Tamanho do Mapa, Aguarde...");

                Filme filme = (Filme)cbFilme.SelectedItem;

                Program.Config["FilmePadrao"] = filme.Id.ToString();

                filmeAtual = filme;

                vvCorte.Document.DocHeight = filmeAtual.Largura;

                if (nestManager != null)
                {
                    int margin = 0;
                    try { margin = 2 * int.Parse(Program.Config["margin"]); } catch { }

                    nestManager.ResetSheet(filmeAtual.Largura - margin);
                }

                if (nestManager != null)
                {
                    button3_Click(null, null);
                }

                pnlprincipal.Enabled = true;
                pnlCalculando.Visible = false;

                vvCorte.Refresh();
            }
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
            lbQtde.Text = sel.AtualizaMarcas() + " desenhos cadastrados sendo\n" + DBManager.GetNumVeiculoMarca();

            if (Sync.SyncFullDatabase.VerificaAtualizacoesSistema())
            {
                if (MessageBox.Show("Existe uma atualização do sistema. Deseja obter esta atualização agora?", "A T U A L I Z A Ç Ã O", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start("http://www.cutfilm.com.br/instalador/CutFilmeSetup.exe");
                    Application.Exit();
                }
            }

            FrmNovosVeiculos.MostraAtualizacoes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmConfigPlotter c = new GlassFilm.FrmConfigPlotter();
            c.ShowInTaskbar = false;
            c.ShowDialog();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void lbAreaUsada_Click(object sender, EventArgs e)
        {

        }

        private void pnlprincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            return base.ProcessKeyPreview(ref m);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);



            if (vvCorte.Document.SelectionCount > 0)
            {
                int amount = 4;

                if (e.Modifiers == Keys.Shift)
                    amount = 1;

                vvCorte.Document.BeginTransform();

                if (e.KeyCode == Keys.Left)
                {
                    vvCorte.MoveSelection(-amount, 0);
                    e.Handled = true;
                }

                if (e.KeyCode == Keys.Right)
                {
                    vvCorte.MoveSelection(+amount, 0);
                    e.Handled = true;
                }

                if (e.KeyCode == Keys.Up)
                {
                    vvCorte.MoveSelection(0, -amount);
                    e.Handled = true;
                }

                if (e.KeyCode == Keys.Down)
                {
                    vvCorte.MoveSelection(0, +amount);
                    e.Handled = true;
                }

                vvCorte.Document.EndTransform(false);
                
                UpdateDocInfo();
            }
        }       

        private void toollSincronizacao_Click(object sender, EventArgs e)
        {
            if (!ValidaInternet.existeInternet())
            {
                Mensagens.Atencao("Não existe conexão com a internet");
                return;
            }

            string oldCalculando = lbCalculando.Text;
            lbCalculando.Text = "CRIPTOGRAFANDO A BASE, ISSO PODE DEMORAR ALGUNS MINUTOS. POR FAVOR AGUARDE...";
            pnlCalculando.Visible = true;
            pbCalc.Visible = true;
            lbCalculando.Location = new Point(0, 0);
            lbCalculando.Width = lbCalculando.Parent.Width;

            Color oldColor = pnlCalculando.BackColor;
            pnlCalculando.BackColor = Color.WhiteSmoke;

            lbCalculando.Enabled = true;

            this.Enabled = false;
            SyncFullDatabase.CriptografarBase(pbCalc);

            pnlCalculando.BackColor = oldColor;
            pnlCalculando.Visible = false;
            pbCalc.Visible = false; 

            this.Enabled = true;

            lbCalculando.Text = oldCalculando;

            DBManager.CloseDatabases();

            if(Mensagens.PeruntaSimNao("Deseja Enviar todas as Alterações para o Servidor?\nTudo enviado será compartilhado com os Clientes.") == DialogResult.Yes)
                FrmSync.ShowSync(true, false, false);

            DBManager.InitDB();
        }        

        private void loadpanel(string desc)
        {
            calculapalavra();
            pnlCalculando.Visible = true;
            pnlCalculando.BackColor = ColorTranslator.FromHtml("#333333");
            lbCalculando.Text = desc;
            Application.DoEvents();
        }

        private void loadpanelRecortar()
        {
            calculapalavra();
            pnlCalculando.Visible = true;
            pnlCalculando.BackColor = ColorTranslator.FromHtml("#2c802c");
            lbCalculando.Text = "Enviando para corte ...";                                   
            Application.DoEvents();
            Thread.Sleep(3000);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            FrmLog c = new FrmLog();
            c.ShowInTaskbar = false;
            c.ShowDialog();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            bool possuiDesenhos = FrmNovosVeiculos.PossuiDesenhos();

            DBManager.CloseDatabases();

            try
            {
                bool force = false;

                if (MessageBox.Show("Deseja efetuar a atualização do sistema?\n(Isso poderá levar alguns minutos)", "A T E N Ç Ã O", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    //force = true;
                    return;
                }

                vvCorte.Width = splitCorte.Panel2.Width - toolCorte.Width;

                FrmSync.ShowSync(false, true, force);

                if (possuiDesenhos && !force)
                    FrmNovosVeiculos.MostraAtualizacoes();
            }
            catch
            {

            }
            finally
            {
                DBManager.InitDB();
                DBManager.VerificaTabelasAuxiliares();

                sel.AtualizaMarcas();

                lbQtde.Text = DBManager.GetNumDesenhos() + " desenhos cadastrados sendo\n" + DBManager.GetNumVeiculoMarca();
            }            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.techconsultoria.com.br/");
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.techconsultoria.com.br/");
        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            if (cbAno.SelectedItem != null)
            {
                ModeloAno m = (ModeloAno)cbAno.SelectedItem;

                FrmDetalheView dv = new FrmDetalheView();
                dv.CodigoVeculo = Convert.ToInt32(m.Codigo_ano);
                dv.StartPosition = FormStartPosition.CenterScreen;

                dv.ShowDialog();
            }
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnArquitetura.Visible = false;

            sel.Limpar();
            sel.TipoDesenho = TipoDesenho.WindowTint;

            switch (cbTipo.SelectedIndex)
            {
                case 0:
                    {
                        sel.TipoDesenho = TipoDesenho.WindowTint;
                        vvModelo.Clear();
                    }
                    break;
                case 1:
                    {
                        sel.TipoDesenho = TipoDesenho.PPV;
                        vvModelo.Clear();
                    }
                    break;
                case 2:
                    {
                        vvModelo.Clear();
                        pnArquitetura.Visible = true;
                    }
                    break;
            }
        }

        private void numLargura_ValueChanged(object sender, EventArgs e)
        {
            vvCorte.Clear();

            int largura = (int)numLargura.Value;
            int altura = (int)numAltura.Value;

            if (largura * altura > 0)
            {
                int l = 10;
                int t = 10;
                int r = 10+largura;
                int b = 10+altura;

                string svga = $"	<svg xmlns:dc=\"http://purl.org/dc/elements/1.1/\" 								                        "
                            + $"		 xmlns:cc=\"http://creativecommons.org/ns#\" 								                        "
                            + $"		 xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" 					                        "
                            + $"		 xmlns:svg=\"http://www.w3.org/2000/svg\" 									                        "
                            + $"		 xmlns=\"http://www.w3.org/2000/svg\" 										                        "
                            + $"		 viewBox=\"0 0 {r+10} {b+10}\" width=\"{r+10}mm\" height=\"{b+10}mm\" version=\"1.1\">	"
                            + $"	   <path style=\"fill: none; stroke:#e30016;stroke-width:0\" 					                        "
                            + $"		 d=\" M{l},{t} {r},{t} {r},{b} {l},{b} z\" />											"
                            + $"	</svg>																			                        ";

                if (Debugger.IsAttached)
                {
                    try
                    {
                        File.WriteAllText("D:\\svg\\svga.svg", svga);
                    }
                    catch
                    {

                    }
                }

                CarregaSvg(svga);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmNovosVeiculos novos = new FrmNovosVeiculos();
            novos.ShowDialog();
        }        
    }
}
