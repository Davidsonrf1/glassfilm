﻿namespace GlassFilm
{
    partial class FrnCadRolo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label3;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrnCadRolo));
            this.tabRegistros = new System.Windows.Forms.TabControl();
            this.pagLista = new System.Windows.Forms.TabPage();
            this.pnlGrade = new System.Windows.Forms.Panel();
            this.gridPrincipal = new System.Windows.Forms.DataGridView();
            this.pageManutencao = new System.Windows.Forms.TabPage();
            this.pnlManutencao = new System.Windows.Forms.Panel();
            this.txtLargura = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCodigo = new System.Windows.Forms.Panel();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPesquisa = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.toolBar = new System.Windows.Forms.StatusStrip();
            this.toolPrincipal = new System.Windows.Forms.ToolStrip();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
            this.toolStatus = new System.Windows.Forms.ToolStripLabel();
            label3 = new System.Windows.Forms.Label();
            this.tabRegistros.SuspendLayout();
            this.pagLista.SuspendLayout();
            this.pnlGrade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrincipal)).BeginInit();
            this.pageManutencao.SuspendLayout();
            this.pnlManutencao.SuspendLayout();
            this.pnlCodigo.SuspendLayout();
            this.pnlPesquisa.SuspendLayout();
            this.toolPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Verdana", 8.3F);
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(83, 14);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(56, 14);
            label3.TabIndex = 133;
            label3.Text = "Código:";
            // 
            // tabRegistros
            // 
            this.tabRegistros.Controls.Add(this.pagLista);
            this.tabRegistros.Controls.Add(this.pageManutencao);
            this.tabRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRegistros.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabRegistros.Location = new System.Drawing.Point(0, 64);
            this.tabRegistros.Name = "tabRegistros";
            this.tabRegistros.SelectedIndex = 0;
            this.tabRegistros.Size = new System.Drawing.Size(717, 332);
            this.tabRegistros.TabIndex = 16;
            // 
            // pagLista
            // 
            this.pagLista.Controls.Add(this.pnlGrade);
            this.pagLista.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagLista.ForeColor = System.Drawing.Color.Black;
            this.pagLista.ImageIndex = 0;
            this.pagLista.Location = new System.Drawing.Point(4, 22);
            this.pagLista.Name = "pagLista";
            this.pagLista.Padding = new System.Windows.Forms.Padding(3);
            this.pagLista.Size = new System.Drawing.Size(709, 306);
            this.pagLista.TabIndex = 0;
            this.pagLista.Text = "Listagem";
            this.pagLista.UseVisualStyleBackColor = true;
            // 
            // pnlGrade
            // 
            this.pnlGrade.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlGrade.Controls.Add(this.gridPrincipal);
            this.pnlGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrade.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGrade.Location = new System.Drawing.Point(3, 3);
            this.pnlGrade.Name = "pnlGrade";
            this.pnlGrade.Size = new System.Drawing.Size(703, 300);
            this.pnlGrade.TabIndex = 0;
            // 
            // gridPrincipal
            // 
            this.gridPrincipal.AllowUserToAddRows = false;
            this.gridPrincipal.AllowUserToDeleteRows = false;
            this.gridPrincipal.AllowUserToOrderColumns = true;
            this.gridPrincipal.AllowUserToResizeRows = false;
            this.gridPrincipal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPrincipal.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.gridPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPrincipal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPrincipal.ColumnHeadersHeight = 22;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPrincipal.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPrincipal.Location = new System.Drawing.Point(0, 0);
            this.gridPrincipal.MultiSelect = false;
            this.gridPrincipal.Name = "gridPrincipal";
            this.gridPrincipal.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPrincipal.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridPrincipal.RowHeadersVisible = false;
            this.gridPrincipal.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPrincipal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPrincipal.Size = new System.Drawing.Size(703, 300);
            this.gridPrincipal.TabIndex = 32;
            this.gridPrincipal.TabStop = false;
            this.gridPrincipal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCellClick);
            this.gridPrincipal.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCellDoubleClick);
            // 
            // pageManutencao
            // 
            this.pageManutencao.Controls.Add(this.pnlManutencao);
            this.pageManutencao.Controls.Add(this.pnlCodigo);
            this.pageManutencao.Controls.Add(this.panel1);
            this.pageManutencao.ForeColor = System.Drawing.Color.Black;
            this.pageManutencao.ImageIndex = 1;
            this.pageManutencao.Location = new System.Drawing.Point(4, 22);
            this.pageManutencao.Name = "pageManutencao";
            this.pageManutencao.Padding = new System.Windows.Forms.Padding(3);
            this.pageManutencao.Size = new System.Drawing.Size(709, 306);
            this.pageManutencao.TabIndex = 1;
            this.pageManutencao.Text = "Manutenção";
            this.pageManutencao.UseVisualStyleBackColor = true;
            // 
            // pnlManutencao
            // 
            this.pnlManutencao.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlManutencao.Controls.Add(this.txtLargura);
            this.pnlManutencao.Controls.Add(this.label4);
            this.pnlManutencao.Controls.Add(this.txtDescricao);
            this.pnlManutencao.Controls.Add(this.label1);
            this.pnlManutencao.Controls.Add(this.label2);
            this.pnlManutencao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManutencao.Enabled = false;
            this.pnlManutencao.Location = new System.Drawing.Point(3, 49);
            this.pnlManutencao.Name = "pnlManutencao";
            this.pnlManutencao.Size = new System.Drawing.Size(703, 254);
            this.pnlManutencao.TabIndex = 134;
            // 
            // txtLargura
            // 
            this.txtLargura.Location = new System.Drawing.Point(150, 85);
            this.txtLargura.Name = "txtLargura";
            this.txtLargura.Size = new System.Drawing.Size(147, 21);
            this.txtLargura.TabIndex = 0;
            this.txtLargura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLargura_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 14);
            this.label4.TabIndex = 118;
            this.label4.Text = "Largura:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(150, 58);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(502, 21);
            this.txtDescricao.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 115;
            this.label1.Text = "Descrição:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(19, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 111;
            this.label2.Text = "MOVIMENTAÇÃO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCodigo
            // 
            this.pnlCodigo.BackColor = System.Drawing.Color.DimGray;
            this.pnlCodigo.Controls.Add(this.btnCarregar);
            this.pnlCodigo.Controls.Add(this.txtCodigo);
            this.pnlCodigo.Controls.Add(label3);
            this.pnlCodigo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCodigo.Location = new System.Drawing.Point(3, 6);
            this.pnlCodigo.Name = "pnlCodigo";
            this.pnlCodigo.Size = new System.Drawing.Size(703, 43);
            this.pnlCodigo.TabIndex = 136;
            // 
            // btnCarregar
            // 
            this.btnCarregar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCarregar.FlatAppearance.BorderSize = 0;
            this.btnCarregar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCarregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregar.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.btnCarregar.ForeColor = System.Drawing.Color.White;
            this.btnCarregar.Image = global::GlassFilm.Properties.Resources.t;
            this.btnCarregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCarregar.Location = new System.Drawing.Point(256, 5);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(106, 32);
            this.btnCarregar.TabIndex = 132;
            this.btnCarregar.Text = "      Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtCodigo.Location = new System.Drawing.Point(150, 11);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(90, 21);
            this.txtCodigo.TabIndex = 131;
            this.txtCodigo.Tag = " ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(703, 3);
            this.panel1.TabIndex = 135;
            // 
            // pnlPesquisa
            // 
            this.pnlPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.pnlPesquisa.Controls.Add(this.button1);
            this.pnlPesquisa.Controls.Add(this.label19);
            this.pnlPesquisa.Controls.Add(this.txtFiltro);
            this.pnlPesquisa.Controls.Add(this.btnConsultar);
            this.pnlPesquisa.Controls.Add(this.label20);
            this.pnlPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPesquisa.Location = new System.Drawing.Point(0, 25);
            this.pnlPesquisa.Name = "pnlPesquisa";
            this.pnlPesquisa.Size = new System.Drawing.Size(717, 39);
            this.pnlPesquisa.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::GlassFilm.Properties.Resources.search__1_;
            this.button1.Location = new System.Drawing.Point(670, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 21);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(161, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 14);
            this.label19.TabIndex = 3;
            this.label19.Text = "Pesquisar:";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtFiltro.Location = new System.Drawing.Point(59, 9);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.ReadOnly = true;
            this.txtFiltro.Size = new System.Drawing.Size(93, 21);
            this.txtFiltro.TabIndex = 2;
            this.txtFiltro.TabStop = false;
            this.txtFiltro.Text = "DESCRICAO";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultar.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.btnConsultar.Location = new System.Drawing.Point(241, 9);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(456, 21);
            this.btnConsultar.TabIndex = 1;
            this.btnConsultar.TabStop = false;
            this.btnConsultar.TextChanged += new System.EventHandler(this.ConsultaAlteraTexto);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(13, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 14);
            this.label20.TabIndex = 0;
            this.label20.Text = "Filtro:";
            // 
            // toolBar
            // 
            this.toolBar.Location = new System.Drawing.Point(0, 396);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.toolBar.Size = new System.Drawing.Size(717, 22);
            this.toolBar.TabIndex = 17;
            // 
            // toolPrincipal
            // 
            this.toolPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo,
            this.toolStripSeparator1,
            this.btnEditar,
            this.toolStripSeparator3,
            this.btnSalvar,
            this.toolStripSeparator2,
            this.btnExcluir,
            this.toolStripSeparator4,
            this.btnCancelar,
            this.toolStripSeparator5,
            this.btnSair,
            this.toolStatus});
            this.toolPrincipal.Location = new System.Drawing.Point(0, 0);
            this.toolPrincipal.Name = "toolPrincipal";
            this.toolPrincipal.Size = new System.Drawing.Size(717, 25);
            this.toolPrincipal.TabIndex = 14;
            this.toolPrincipal.Text = "toolStrip1";
            // 
            // btnNovo
            // 
            this.btnNovo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(56, 22);
            this.btnNovo.Text = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.BotaoNovo);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEditar
            // 
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(57, 22);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.BotaoEditar);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Enabled = false;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(58, 22);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.BotaoSalvar);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(61, 22);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.BotaoExcluir);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.BotaoCancelar);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSair
            // 
            this.btnSair.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSair.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(46, 22);
            this.btnSair.Text = "Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.Click += new System.EventHandler(this.Sair);
            // 
            // toolStatus
            // 
            this.toolStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new System.Drawing.Size(16, 22);
            this.toolStatus.Text = "...";
            // 
            // FrnCadRolo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 418);
            this.ControlBox = false;
            this.Controls.Add(this.tabRegistros);
            this.Controls.Add(this.pnlPesquisa);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.toolPrincipal);
            this.Name = "FrnCadRolo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Rolo";
            this.Load += new System.EventHandler(this.Frncadrolo_Load);
            this.tabRegistros.ResumeLayout(false);
            this.pagLista.ResumeLayout(false);
            this.pnlGrade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPrincipal)).EndInit();
            this.pageManutencao.ResumeLayout(false);
            this.pnlManutencao.ResumeLayout(false);
            this.pnlManutencao.PerformLayout();
            this.pnlCodigo.ResumeLayout(false);
            this.pnlCodigo.PerformLayout();
            this.pnlPesquisa.ResumeLayout(false);
            this.pnlPesquisa.PerformLayout();
            this.toolPrincipal.ResumeLayout(false);
            this.toolPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TabControl tabRegistros;
        private System.Windows.Forms.TabPage pagLista;
        public System.Windows.Forms.Panel pnlGrade;
        private System.Windows.Forms.DataGridView gridPrincipal;
        private System.Windows.Forms.TabPage pageManutencao;
        public System.Windows.Forms.Panel pnlManutencao;
        private System.Windows.Forms.TextBox txtLargura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCodigo;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlPesquisa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.TextBox btnConsultar;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.StatusStrip toolBar;
        private System.Windows.Forms.ToolStrip toolPrincipal;
        private System.Windows.Forms.ToolStripButton btnNovo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSair;
        private System.Windows.Forms.ToolStripLabel toolStatus;

    }
}