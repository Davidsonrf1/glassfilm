﻿namespace GlassFilm
{
    partial class FrmCadModelo
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadModelo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imagensIcons = new System.Windows.Forms.ImageList(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tabRegistros = new System.Windows.Forms.TabControl();
            this.pagLista = new System.Windows.Forms.TabPage();
            this.pnlGrade = new System.Windows.Forms.Panel();
            this.gridPrincipal = new System.Windows.Forms.DataGridView();
            this.pageManutencao = new System.Windows.Forms.TabPage();
            this.pnlManutencao = new System.Windows.Forms.Panel();
            this.txtCodigoAno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescCodigoMarca = new System.Windows.Forms.TextBox();
            this.txtCodigoMarca = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCodigo = new System.Windows.Forms.Panel();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPesquisa = new System.Windows.Forms.Panel();
            this.toolStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolBar = new System.Windows.Forms.StatusStrip();
            this.toolPrincipal = new System.Windows.Forms.ToolStrip();
            this.cbAnos = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnBuscaMarca = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
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
            // imagensIcons
            // 
            this.imagensIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagensIcons.ImageStream")));
            this.imagensIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imagensIcons.Images.SetKeyName(0, "list.png");
            this.imagensIcons.Images.SetKeyName(1, "pencil-tool.png");
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
            this.txtFiltro.Text = "MODELO";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultar.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.btnConsultar.Location = new System.Drawing.Point(241, 9);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(715, 21);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tabRegistros
            // 
            this.tabRegistros.Controls.Add(this.pagLista);
            this.tabRegistros.Controls.Add(this.pageManutencao);
            this.tabRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRegistros.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabRegistros.ImageList = this.imagensIcons;
            this.tabRegistros.Location = new System.Drawing.Point(0, 64);
            this.tabRegistros.Name = "tabRegistros";
            this.tabRegistros.SelectedIndex = 0;
            this.tabRegistros.Size = new System.Drawing.Size(976, 370);
            this.tabRegistros.TabIndex = 16;
            // 
            // pagLista
            // 
            this.pagLista.Controls.Add(this.pnlGrade);
            this.pagLista.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagLista.ForeColor = System.Drawing.Color.Black;
            this.pagLista.ImageIndex = 0;
            this.pagLista.Location = new System.Drawing.Point(4, 23);
            this.pagLista.Name = "pagLista";
            this.pagLista.Padding = new System.Windows.Forms.Padding(3);
            this.pagLista.Size = new System.Drawing.Size(968, 343);
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
            this.pnlGrade.Size = new System.Drawing.Size(962, 337);
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPrincipal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.gridPrincipal.ColumnHeadersHeight = 22;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPrincipal.DefaultCellStyle = dataGridViewCellStyle14;
            this.gridPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPrincipal.Location = new System.Drawing.Point(0, 0);
            this.gridPrincipal.MultiSelect = false;
            this.gridPrincipal.Name = "gridPrincipal";
            this.gridPrincipal.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPrincipal.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.gridPrincipal.RowHeadersVisible = false;
            this.gridPrincipal.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPrincipal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPrincipal.Size = new System.Drawing.Size(962, 337);
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
            this.pageManutencao.Location = new System.Drawing.Point(4, 23);
            this.pageManutencao.Name = "pageManutencao";
            this.pageManutencao.Padding = new System.Windows.Forms.Padding(3);
            this.pageManutencao.Size = new System.Drawing.Size(968, 343);
            this.pageManutencao.TabIndex = 1;
            this.pageManutencao.Text = "Manutenção";
            this.pageManutencao.UseVisualStyleBackColor = true;
            // 
            // pnlManutencao
            // 
            this.pnlManutencao.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlManutencao.Controls.Add(this.btnAdd);
            this.pnlManutencao.Controls.Add(this.btnDel);
            this.pnlManutencao.Controls.Add(this.cbAnos);
            this.pnlManutencao.Controls.Add(this.txtCodigoAno);
            this.pnlManutencao.Controls.Add(this.label5);
            this.pnlManutencao.Controls.Add(this.btnBuscaMarca);
            this.pnlManutencao.Controls.Add(this.txtDescCodigoMarca);
            this.pnlManutencao.Controls.Add(this.txtCodigoMarca);
            this.pnlManutencao.Controls.Add(this.label4);
            this.pnlManutencao.Controls.Add(this.txtDescricao);
            this.pnlManutencao.Controls.Add(this.label1);
            this.pnlManutencao.Controls.Add(this.label2);
            this.pnlManutencao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManutencao.Enabled = false;
            this.pnlManutencao.Location = new System.Drawing.Point(3, 49);
            this.pnlManutencao.Name = "pnlManutencao";
            this.pnlManutencao.Size = new System.Drawing.Size(962, 291);
            this.pnlManutencao.TabIndex = 134;
            // 
            // txtCodigoAno
            // 
            this.txtCodigoAno.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtCodigoAno.Location = new System.Drawing.Point(157, 117);
            this.txtCodigoAno.Name = "txtCodigoAno";
            this.txtCodigoAno.Size = new System.Drawing.Size(66, 21);
            this.txtCodigoAno.TabIndex = 2;
            this.txtCodigoAno.Tag = "ANO";
            this.txtCodigoAno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoAno_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 14);
            this.label5.TabIndex = 125;
            this.label5.Text = "Ano:";
            // 
            // txtDescCodigoMarca
            // 
            this.txtDescCodigoMarca.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtDescCodigoMarca.Location = new System.Drawing.Point(225, 63);
            this.txtDescCodigoMarca.Name = "txtDescCodigoMarca";
            this.txtDescCodigoMarca.ReadOnly = true;
            this.txtDescCodigoMarca.Size = new System.Drawing.Size(434, 21);
            this.txtDescCodigoMarca.TabIndex = 123;
            this.txtDescCodigoMarca.TabStop = false;
            this.txtDescCodigoMarca.Tag = " ";
            // 
            // txtCodigoMarca
            // 
            this.txtCodigoMarca.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtCodigoMarca.Location = new System.Drawing.Point(157, 63);
            this.txtCodigoMarca.Name = "txtCodigoMarca";
            this.txtCodigoMarca.Size = new System.Drawing.Size(66, 21);
            this.txtCodigoMarca.TabIndex = 0;
            this.txtCodigoMarca.Tag = "HISTORICO";
            this.txtCodigoMarca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoMarca_KeyPress);
            this.txtCodigoMarca.Leave += new System.EventHandler(this.txtCodigoMarca_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 14);
            this.label4.TabIndex = 118;
            this.label4.Text = "Código Marca:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(157, 90);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(502, 21);
            this.txtDescricao.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 93);
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
            this.pnlCodigo.Controls.Add(this.button2);
            this.pnlCodigo.Controls.Add(this.txtCodigo);
            this.pnlCodigo.Controls.Add(label3);
            this.pnlCodigo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCodigo.Location = new System.Drawing.Point(3, 6);
            this.pnlCodigo.Name = "pnlCodigo";
            this.pnlCodigo.Size = new System.Drawing.Size(962, 43);
            this.pnlCodigo.TabIndex = 136;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtCodigo.Location = new System.Drawing.Point(150, 11);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(90, 21);
            this.txtCodigo.TabIndex = 131;
            this.txtCodigo.Tag = " ";
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 3);
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
            this.pnlPesquisa.Size = new System.Drawing.Size(976, 39);
            this.pnlPesquisa.TabIndex = 15;
            // 
            // toolStatus
            // 
            this.toolStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new System.Drawing.Size(16, 22);
            this.toolStatus.Text = "...";
            // 
            // toolBar
            // 
            this.toolBar.Location = new System.Drawing.Point(0, 434);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.toolBar.Size = new System.Drawing.Size(976, 22);
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
            this.toolPrincipal.Size = new System.Drawing.Size(976, 25);
            this.toolPrincipal.TabIndex = 14;
            this.toolPrincipal.Text = "toolStrip1";
            // 
            // cbAnos
            // 
            this.cbAnos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAnos.FormattingEnabled = true;
            this.cbAnos.Location = new System.Drawing.Point(263, 117);
            this.cbAnos.Name = "cbAnos";
            this.cbAnos.Size = new System.Drawing.Size(99, 21);
            this.cbAnos.TabIndex = 126;
            this.cbAnos.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::GlassFilm.Properties.Resources.add_button_inside_black_circle;
            this.btnAdd.Location = new System.Drawing.Point(225, 111);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(34, 32);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDel.FlatAppearance.BorderSize = 0;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Image = global::GlassFilm.Properties.Resources.minus;
            this.btnDel.Location = new System.Drawing.Point(365, 111);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(34, 32);
            this.btnDel.TabIndex = 127;
            this.btnDel.TabStop = false;
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnBuscaMarca
            // 
            this.btnBuscaMarca.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscaMarca.FlatAppearance.BorderSize = 0;
            this.btnBuscaMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscaMarca.Image = global::GlassFilm.Properties.Resources.magnifying_glass_search_button;
            this.btnBuscaMarca.Location = new System.Drawing.Point(660, 57);
            this.btnBuscaMarca.Name = "btnBuscaMarca";
            this.btnBuscaMarca.Size = new System.Drawing.Size(34, 32);
            this.btnBuscaMarca.TabIndex = 124;
            this.btnBuscaMarca.TabStop = false;
            this.btnBuscaMarca.UseVisualStyleBackColor = false;
            this.btnBuscaMarca.Click += new System.EventHandler(this.btnBuscaMarca_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = global::GlassFilm.Properties.Resources.t;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(256, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 32);
            this.button2.TabIndex = 132;
            this.button2.Text = "      Carregar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.button1.Location = new System.Drawing.Point(929, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 21);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = false;
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
            // FrmCadModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 456);
            this.ControlBox = false;
            this.Controls.Add(this.tabRegistros);
            this.Controls.Add(this.pnlPesquisa);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.toolPrincipal);
            this.Name = "FrmCadModelo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Modelo";
            this.Load += new System.EventHandler(this.Frmcadmodelo_Load);
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

        private System.Windows.Forms.ImageList imagensIcons;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.TextBox btnConsultar;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolStripButton btnNovo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnSair;
        public System.Windows.Forms.TabControl tabRegistros;
        private System.Windows.Forms.TabPage pagLista;
        public System.Windows.Forms.Panel pnlGrade;
        private System.Windows.Forms.DataGridView gridPrincipal;
        private System.Windows.Forms.TabPage pageManutencao;
        public System.Windows.Forms.Panel pnlManutencao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlCodigo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlPesquisa;
        private System.Windows.Forms.ToolStripLabel toolStatus;
        private System.Windows.Forms.StatusStrip toolBar;
        private System.Windows.Forms.ToolStrip toolPrincipal;
        private System.Windows.Forms.Button btnBuscaMarca;
        private System.Windows.Forms.TextBox txtDescCodigoMarca;
        private System.Windows.Forms.TextBox txtCodigoMarca;
        private System.Windows.Forms.TextBox txtCodigoAno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ComboBox cbAnos;
    }
}