namespace GlassFilm
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarMarcaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroModeloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarDesenhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroRoloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlprincipal = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbFilme = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbTamanho = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlFiltroInfo = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbAno = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.StatusStrip();
            this.docInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.selInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.splitCorte = new System.Windows.Forms.SplitContainer();
            this.vvModelo = new VectorView.VectorViewCtr();
            this.toolCorte = new System.Windows.Forms.ToolStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.vvCorte = new VectorView.VectorViewCtr();
            this.menuStrip1.SuspendLayout();
            this.pnlprincipal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlFiltroInfo.SuspendLayout();
            this.status.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCorte)).BeginInit();
            this.splitCorte.Panel1.SuspendLayout();
            this.splitCorte.Panel2.SuspendLayout();
            this.splitCorte.SuspendLayout();
            this.toolCorte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolArquivo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1335, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "Menu";
            // 
            // toolArquivo
            // 
            this.toolArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarMarcaToolStripMenuItem,
            this.cadastroModeloToolStripMenuItem,
            this.cadastrarDesenhoToolStripMenuItem,
            this.cadastroRoloToolStripMenuItem});
            this.toolArquivo.ForeColor = System.Drawing.Color.White;
            this.toolArquivo.Name = "toolArquivo";
            this.toolArquivo.Size = new System.Drawing.Size(69, 20);
            this.toolArquivo.Text = "Arquivo";
            this.toolArquivo.Visible = false;
            // 
            // cadastrarMarcaToolStripMenuItem
            // 
            this.cadastrarMarcaToolStripMenuItem.Name = "cadastrarMarcaToolStripMenuItem";
            this.cadastrarMarcaToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cadastrarMarcaToolStripMenuItem.Text = "Cadastrar Marca";
            this.cadastrarMarcaToolStripMenuItem.Click += new System.EventHandler(this.cadastrarMarcaToolStripMenuItem_Click);
            // 
            // cadastroModeloToolStripMenuItem
            // 
            this.cadastroModeloToolStripMenuItem.Name = "cadastroModeloToolStripMenuItem";
            this.cadastroModeloToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cadastroModeloToolStripMenuItem.Text = "Cadastrar Modelo";
            this.cadastroModeloToolStripMenuItem.Click += new System.EventHandler(this.cadastroModeloToolStripMenuItem_Click);
            // 
            // cadastrarDesenhoToolStripMenuItem
            // 
            this.cadastrarDesenhoToolStripMenuItem.Name = "cadastrarDesenhoToolStripMenuItem";
            this.cadastrarDesenhoToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cadastrarDesenhoToolStripMenuItem.Text = "Cadastrar Desenho";
            this.cadastrarDesenhoToolStripMenuItem.Click += new System.EventHandler(this.cadastrarDesenhoToolStripMenuItem_Click);
            // 
            // cadastroRoloToolStripMenuItem
            // 
            this.cadastroRoloToolStripMenuItem.Name = "cadastroRoloToolStripMenuItem";
            this.cadastroRoloToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.cadastroRoloToolStripMenuItem.Text = "Cadastro Rolo";
            this.cadastroRoloToolStripMenuItem.Click += new System.EventHandler(this.cadastroRoloToolStripMenuItem_Click);
            // 
            // pnlprincipal
            // 
            this.pnlprincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.pnlprincipal.Controls.Add(this.button1);
            this.pnlprincipal.Controls.Add(this.button4);
            this.pnlprincipal.Controls.Add(this.button3);
            this.pnlprincipal.Controls.Add(this.button2);
            this.pnlprincipal.Controls.Add(this.panel3);
            this.pnlprincipal.Controls.Add(this.groupBox2);
            this.pnlprincipal.Controls.Add(this.panel1);
            this.pnlprincipal.Controls.Add(this.groupBox1);
            this.pnlprincipal.Controls.Add(this.pictureBox1);
            this.pnlprincipal.Controls.Add(this.panel2);
            this.pnlprincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlprincipal.Location = new System.Drawing.Point(0, 24);
            this.pnlprincipal.Name = "pnlprincipal";
            this.pnlprincipal.Size = new System.Drawing.Size(1335, 110);
            this.pnlprincipal.TabIndex = 4;
            this.pnlprincipal.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel3.Location = new System.Drawing.Point(875, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 97);
            this.panel3.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbFilme);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(560, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 88);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mídia";
            // 
            // cbFilme
            // 
            this.cbFilme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilme.ForeColor = System.Drawing.Color.Black;
            this.cbFilme.FormattingEnabled = true;
            this.cbFilme.Items.AddRange(new object[] {
            "40 Inch"});
            this.cbFilme.Location = new System.Drawing.Point(61, 40);
            this.cbFilme.Name = "cbFilme";
            this.cbFilme.Size = new System.Drawing.Size(240, 21);
            this.cbFilme.TabIndex = 1;
            this.cbFilme.SelectedIndexChanged += new System.EventHandler(this.cbFilme_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Largura";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel1.Location = new System.Drawing.Point(553, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 97);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbTamanho);
            this.groupBox1.Location = new System.Drawing.Point(377, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 90);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Tamanho: ";
            // 
            // lbTamanho
            // 
            this.lbTamanho.AutoSize = true;
            this.lbTamanho.Location = new System.Drawing.Point(27, 45);
            this.lbTamanho.Name = "lbTamanho";
            this.lbTamanho.Size = new System.Drawing.Size(111, 13);
            this.lbTamanho.TabIndex = 0;
            this.lbTamanho.Text = "16.51inX27.76in -- TO";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 110);
            this.panel2.TabIndex = 1;
            // 
            // pnlFiltroInfo
            // 
            this.pnlFiltroInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.pnlFiltroInfo.Controls.Add(this.panel4);
            this.pnlFiltroInfo.Controls.Add(this.panel7);
            this.pnlFiltroInfo.Controls.Add(this.cbModelo);
            this.pnlFiltroInfo.Controls.Add(this.label10);
            this.pnlFiltroInfo.Controls.Add(this.panel6);
            this.pnlFiltroInfo.Controls.Add(this.cbMarca);
            this.pnlFiltroInfo.Controls.Add(this.label9);
            this.pnlFiltroInfo.Controls.Add(this.panel5);
            this.pnlFiltroInfo.Controls.Add(this.cbAno);
            this.pnlFiltroInfo.Controls.Add(this.label8);
            this.pnlFiltroInfo.Controls.Add(this.comboBox2);
            this.pnlFiltroInfo.Controls.Add(this.label7);
            this.pnlFiltroInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroInfo.Location = new System.Drawing.Point(0, 134);
            this.pnlFiltroInfo.Name = "pnlFiltroInfo";
            this.pnlFiltroInfo.Size = new System.Drawing.Size(1335, 65);
            this.pnlFiltroInfo.TabIndex = 5;
            this.pnlFiltroInfo.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1335, 5);
            this.panel4.TabIndex = 14;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Gainsboro;
            this.panel7.Location = new System.Drawing.Point(366, 7);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1, 42);
            this.panel7.TabIndex = 13;
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.IntegralHeight = false;
            this.cbModelo.Location = new System.Drawing.Point(377, 26);
            this.cbModelo.MaxDropDownItems = 30;
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(144, 21);
            this.cbModelo.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(377, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Modelo:";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Gainsboro;
            this.panel6.Location = new System.Drawing.Point(144, 7);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1, 42);
            this.panel6.TabIndex = 10;
            // 
            // cbMarca
            // 
            this.cbMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.IntegralHeight = false;
            this.cbMarca.Location = new System.Drawing.Point(155, 26);
            this.cbMarca.MaxDropDownItems = 30;
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(200, 21);
            this.cbMarca.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(155, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Marca:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.Location = new System.Drawing.Point(532, 7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1, 42);
            this.panel5.TabIndex = 7;
            // 
            // cbAno
            // 
            this.cbAno.BackColor = System.Drawing.Color.White;
            this.cbAno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAno.FormattingEnabled = true;
            this.cbAno.IntegralHeight = false;
            this.cbAno.Location = new System.Drawing.Point(543, 26);
            this.cbAno.MaxDropDownItems = 30;
            this.cbAno.Name = "cbAno";
            this.cbAno.Size = new System.Drawing.Size(89, 21);
            this.cbAno.TabIndex = 4;
            this.cbAno.SelectedIndexChanged += new System.EventHandler(this.cbAno_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(543, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Ano:";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(12, 26);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.Text = "Window Tint";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tipo:";
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.docInfo,
            this.selInfo});
            this.status.Location = new System.Drawing.Point(0, 545);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1335, 24);
            this.status.TabIndex = 15;
            this.status.Text = "statusStrip1";
            // 
            // docInfo
            // 
            this.docInfo.AutoSize = false;
            this.docInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.docInfo.Name = "docInfo";
            this.docInfo.Size = new System.Drawing.Size(350, 19);
            // 
            // selInfo
            // 
            this.selInfo.AutoSize = false;
            this.selInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.selInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.selInfo.Name = "selInfo";
            this.selInfo.Size = new System.Drawing.Size(550, 19);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.splitCorte);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1335, 569);
            this.panel8.TabIndex = 19;
            // 
            // splitCorte
            // 
            this.splitCorte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitCorte.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitCorte.Location = new System.Drawing.Point(3, 197);
            this.splitCorte.Name = "splitCorte";
            this.splitCorte.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCorte.Panel1
            // 
            this.splitCorte.Panel1.Controls.Add(this.vvModelo);
            // 
            // splitCorte.Panel2
            // 
            this.splitCorte.Panel2.Controls.Add(this.toolCorte);
            this.splitCorte.Panel2.Controls.Add(this.vvCorte);
            this.splitCorte.Panel2.Resize += new System.EventHandler(this.splitCorte_Panel2_Resize);
            this.splitCorte.Size = new System.Drawing.Size(1329, 345);
            this.splitCorte.SplitterDistance = 189;
            this.splitCorte.TabIndex = 18;
            // 
            // vvModelo
            // 
            this.vvModelo.AllowMoveDocument = false;

            this.vvModelo.AllowTransforms = false;
            this.vvModelo.BackColor = System.Drawing.Color.White;
            this.vvModelo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vvModelo.Dock = System.Windows.Forms.DockStyle.Fill;

            this.vvModelo.GridColor = System.Drawing.Color.Black;
            this.vvModelo.GridSize = 10;
            this.vvModelo.Location = new System.Drawing.Point(0, 0);
            this.vvModelo.Name = "vvModelo";

            this.vvModelo.ShowGrid = false;

            this.vvModelo.Size = new System.Drawing.Size(1329, 189);
            this.vvModelo.TabIndex = 19;
            this.vvModelo.Visible = false;
            this.vvModelo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.vvModelo_MouseDoubleClick);
            this.vvModelo.Resize += new System.EventHandler(this.vvModelo_Resize);
            // 
            // toolCorte
            // 
            this.toolCorte.AutoSize = false;
            this.toolCorte.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolCorte.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolCorte.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolCorte.Location = new System.Drawing.Point(1309, 0);
            this.toolCorte.Name = "toolCorte";
            this.toolCorte.Size = new System.Drawing.Size(47, 152);
            this.toolCorte.TabIndex = 21;
            this.toolCorte.Visible = false;
            this.toolCorte.Resize += new System.EventHandler(this.toolCorte_Resize);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::GlassFilm.Properties.Resources.Gears_icon;
            this.button1.Location = new System.Drawing.Point(1248, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 80);
            this.button1.TabIndex = 11;
            this.button1.Text = "Configurar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(44)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = global::GlassFilm.Properties.Resources.scissors;
            this.button4.Location = new System.Drawing.Point(1047, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 80);
            this.button4.TabIndex = 10;
            this.button4.Text = "Recortar";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(2)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Image = global::GlassFilm.Properties.Resources.resizing_tool;
            this.button3.Location = new System.Drawing.Point(966, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 80);
            this.button3.TabIndex = 9;
            this.button3.Text = "Auto Ajuste";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(248)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = global::GlassFilm.Properties.Resources.rubber;
            this.button2.Location = new System.Drawing.Point(885, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 80);
            this.button2.TabIndex = 8;
            this.button2.Text = "Limpar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GlassFilm.Properties.Resources.logoComputer3001;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(7, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 110);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::GlassFilm.Properties.Resources.zoommais1;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(38, 38);
            this.toolStripButton1.ToolTipText = "Aumentar Zoom";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::GlassFilm.Properties.Resources.zoommenos1;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(38, 38);
            this.toolStripButton2.ToolTipText = "Diminuir Zoom";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.AutoSize = false;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::GlassFilm.Properties.Resources.zoomduo2;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(38, 38);
            this.toolStripButton3.ToolTipText = "Ajustar na tela";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // vvCorte
            // 
            this.vvCorte.AllowMoveDocument = true;

            this.vvCorte.AllowTransforms = true;
            this.vvCorte.BackColor = System.Drawing.Color.White;
            this.vvCorte.BackgroundImage = global::GlassFilm.Properties.Resources.tracos3;
            this.vvCorte.Dock = System.Windows.Forms.DockStyle.Left;

            this.vvCorte.GridColor = System.Drawing.Color.Black;
            this.vvCorte.GridSize = 10;
            this.vvCorte.Location = new System.Drawing.Point(0, 0);
            this.vvCorte.Name = "vvCorte";

            this.vvCorte.ShowGrid = false;

            this.vvCorte.Size = new System.Drawing.Size(1302, 152);
            this.vvCorte.TabIndex = 1;
            this.vvCorte.Visible = false;
            this.vvCorte.SelectionMoved += new VectorView.VectorEventHandler(this.vvCorte_SelectionMoved);
            this.vvCorte.SelectionTransformed += new VectorView.VectorEventHandler(this.vvCorte_SelectionTransformed_1);
            this.vvCorte.Resize += new System.EventHandler(this.vvCorte_Resize);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1335, 569);
            this.Controls.Add(this.status);
            this.Controls.Add(this.pnlFiltroInfo);
            this.Controls.Add(this.pnlprincipal);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Computer Cut Brasil - Bem vindo!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.Shown += new System.EventHandler(this.FrmPrincipal_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPrincipal_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPrincipal_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlprincipal.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlFiltroInfo.ResumeLayout(false);
            this.pnlFiltroInfo.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.splitCorte.Panel1.ResumeLayout(false);
            this.splitCorte.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCorte)).EndInit();
            this.splitCorte.ResumeLayout(false);
            this.toolCorte.ResumeLayout(false);
            this.toolCorte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolArquivo;
        private System.Windows.Forms.Panel pnlprincipal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTamanho;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbFilme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlFiltroInfo;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbAno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem cadastrarDesenhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarMarcaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastroModeloToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel docInfo;
        private System.Windows.Forms.ToolStripStatusLabel selInfo;
        private System.Windows.Forms.ToolStripMenuItem cadastroRoloToolStripMenuItem;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.SplitContainer splitCorte;
        private VectorView.VectorViewCtr vvCorte;
        private VectorView.VectorViewCtr vvModelo;
        private System.Windows.Forms.ToolStrip toolCorte;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Button button1;
    }
}