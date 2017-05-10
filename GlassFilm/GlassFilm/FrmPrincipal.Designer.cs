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
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarMarcaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastroModeloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarDesenhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recortarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cortadoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlprincipal = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.splitDesenho = new System.Windows.Forms.SplitContainer();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vvModelo = new VectorView.VectorViewCtr();
            this.vvCorte = new VectorView.VectorViewCtr();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlprincipal.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlFiltroInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDesenho)).BeginInit();
            this.splitDesenho.Panel1.SuspendLayout();
            this.splitDesenho.Panel2.SuspendLayout();
            this.splitDesenho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(104)))), ((int)(((byte)(46)))));
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.organizarToolStripMenuItem,
            this.recortarToolStripMenuItem,
            this.transformarToolStripMenuItem,
            this.configuraçãoToolStripMenuItem,
            this.ajudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1347, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "Menu";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarMarcaToolStripMenuItem,
            this.cadastroModeloToolStripMenuItem,
            this.cadastrarDesenhoToolStripMenuItem});
            this.arquivoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
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
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // organizarToolStripMenuItem
            // 
            this.organizarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.organizarToolStripMenuItem.Name = "organizarToolStripMenuItem";
            this.organizarToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.organizarToolStripMenuItem.Text = "Organizar";
            // 
            // recortarToolStripMenuItem
            // 
            this.recortarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.recortarToolStripMenuItem.Name = "recortarToolStripMenuItem";
            this.recortarToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.recortarToolStripMenuItem.Text = "Recortar";
            // 
            // transformarToolStripMenuItem
            // 
            this.transformarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.transformarToolStripMenuItem.Name = "transformarToolStripMenuItem";
            this.transformarToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.transformarToolStripMenuItem.Text = "Transformar";
            // 
            // configuraçãoToolStripMenuItem
            // 
            this.configuraçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cortadoraToolStripMenuItem});
            this.configuraçãoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.configuraçãoToolStripMenuItem.Name = "configuraçãoToolStripMenuItem";
            this.configuraçãoToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.configuraçãoToolStripMenuItem.Text = "Configuração";
            // 
            // cortadoraToolStripMenuItem
            // 
            this.cortadoraToolStripMenuItem.Name = "cortadoraToolStripMenuItem";
            this.cortadoraToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cortadoraToolStripMenuItem.Text = "Cortadora";
            this.cortadoraToolStripMenuItem.Click += new System.EventHandler(this.cortadoraToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 688);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1347, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(250, 19);
            this.toolStripStatusLabel1.Text = "TESTE";
            // 
            // pnlprincipal
            // 
            this.pnlprincipal.Controls.Add(this.button4);
            this.pnlprincipal.Controls.Add(this.button3);
            this.pnlprincipal.Controls.Add(this.button2);
            this.pnlprincipal.Controls.Add(this.panel3);
            this.pnlprincipal.Controls.Add(this.groupBox3);
            this.pnlprincipal.Controls.Add(this.groupBox2);
            this.pnlprincipal.Controls.Add(this.panel1);
            this.pnlprincipal.Controls.Add(this.groupBox1);
            this.pnlprincipal.Controls.Add(this.pictureBox1);
            this.pnlprincipal.Controls.Add(this.panel2);
            this.pnlprincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlprincipal.Location = new System.Drawing.Point(0, 24);
            this.pnlprincipal.Name = "pnlprincipal";
            this.pnlprincipal.Size = new System.Drawing.Size(1347, 110);
            this.pnlprincipal.TabIndex = 4;
            this.pnlprincipal.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel3.Location = new System.Drawing.Point(713, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 97);
            this.panel3.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.numericUpDown2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(720, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 88);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Outros";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Location = new System.Drawing.Point(248, 51);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(84, 20);
            this.numericUpDown2.TabIndex = 6;
            this.numericUpDown2.Value = new decimal(new int[] {
            27,
            0,
            0,
            131072});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(248, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(62, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Pull Cut";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "%";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(98, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(81, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "64.51";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(98, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "4.60";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Film Utilizado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "SQ Ft Usado:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(375, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 88);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mídia";
            // 
            // comboBox1
            // 
            this.comboBox1.ForeColor = System.Drawing.Color.Black;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "40 Inch"});
            this.comboBox1.Location = new System.Drawing.Point(76, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "40 Inch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Largura";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel1.Location = new System.Drawing.Point(368, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 97);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbTamanho);
            this.groupBox1.Location = new System.Drawing.Point(192, 8);
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
            this.pnlFiltroInfo.Size = new System.Drawing.Size(1347, 65);
            this.pnlFiltroInfo.TabIndex = 5;
            this.pnlFiltroInfo.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1347, 5);
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
            // splitDesenho
            // 
            this.splitDesenho.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitDesenho.Location = new System.Drawing.Point(0, 196);
            this.splitDesenho.Name = "splitDesenho";
            this.splitDesenho.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitDesenho.Panel1
            // 
            this.splitDesenho.Panel1.Controls.Add(this.vvModelo);
            // 
            // splitDesenho.Panel2
            // 
            this.splitDesenho.Panel2.Controls.Add(this.vvCorte);
            this.splitDesenho.Size = new System.Drawing.Size(1346, 492);
            this.splitDesenho.SplitterDistance = 266;
            this.splitDesenho.TabIndex = 13;
            this.splitDesenho.Visible = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(128)))), ((int)(((byte)(44)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = global::GlassFilm.Properties.Resources.scissors;
            this.button4.Location = new System.Drawing.Point(1247, 18);
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
            this.button3.Location = new System.Drawing.Point(1166, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 80);
            this.button3.TabIndex = 9;
            this.button3.Text = "Auto Ajuste";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(152)))), ((int)(((byte)(248)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = global::GlassFilm.Properties.Resources.rubber;
            this.button2.Location = new System.Drawing.Point(1085, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 80);
            this.button2.TabIndex = 8;
            this.button2.Text = "Limpar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::GlassFilm.Properties.Resources.heigth;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(224, 50);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 23);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GlassFilm.Properties.Resources.locked;
            this.pictureBox2.Location = new System.Drawing.Point(301, 38);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 28);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GlassFilm.Properties.Resources._1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(7, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(177, 110);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // vvModelo
            // 
            this.vvModelo.AllowMoveDocument = false;
            this.vvModelo.AllowRotatePath = false;
            this.vvModelo.AllowScalePath = false;
            this.vvModelo.AllowTransforms = false;
            this.vvModelo.BackColor = System.Drawing.Color.White;
            this.vvModelo.BackgroundImage = global::GlassFilm.Properties.Resources.tumblr_inline_nn85tpbQkq1rewzq7_500;
            this.vvModelo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.vvModelo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vvModelo.Document = null;
            this.vvModelo.DrawSelecionBox = true;
            this.vvModelo.GridColor = System.Drawing.Color.DarkRed;
            this.vvModelo.GridSize = 15;
            this.vvModelo.Location = new System.Drawing.Point(0, 0);
            this.vvModelo.Name = "vvModelo";
            this.vvModelo.SelctionMargin = 6F;
            this.vvModelo.ShowDocumentLimit = false;
            this.vvModelo.ShowPointer = false;
            this.vvModelo.Size = new System.Drawing.Size(1346, 266);
            this.vvModelo.TabIndex = 0;
            this.vvModelo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.vvModelo_MouseDoubleClick);
            this.vvModelo.Resize += new System.EventHandler(this.vvModelo_Resize);
            // 
            // vvCorte
            // 
            this.vvCorte.AllowMoveDocument = true;
            this.vvCorte.AllowRotatePath = true;
            this.vvCorte.AllowScalePath = true;
            this.vvCorte.AllowTransforms = true;
            this.vvCorte.BackColor = System.Drawing.Color.White;
            this.vvCorte.BackgroundImage = global::GlassFilm.Properties.Resources.tracos3;
            this.vvCorte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.vvCorte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vvCorte.Document = null;
            this.vvCorte.DrawSelecionBox = true;
            this.vvCorte.GridColor = System.Drawing.Color.Green;
            this.vvCorte.GridSize = 60;
            this.vvCorte.Location = new System.Drawing.Point(0, 0);
            this.vvCorte.Name = "vvCorte";
            this.vvCorte.SelctionMargin = 6F;
            this.vvCorte.ShowDocumentLimit = false;
            this.vvCorte.ShowPointer = false;
            this.vvCorte.Size = new System.Drawing.Size(1346, 222);
            this.vvCorte.TabIndex = 0;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1347, 712);
            this.Controls.Add(this.splitDesenho);
            this.Controls.Add(this.pnlFiltroInfo);
            this.Controls.Add(this.pnlprincipal);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Glass Film - Bem vindo!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlprincipal.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlFiltroInfo.ResumeLayout(false);
            this.pnlFiltroInfo.PerformLayout();
            this.splitDesenho.Panel1.ResumeLayout(false);
            this.splitDesenho.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDesenho)).EndInit();
            this.splitDesenho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recortarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlprincipal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTamanho;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
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
        private System.Windows.Forms.ToolStripMenuItem cortadoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarDesenhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarMarcaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastroModeloToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitDesenho;
        private VectorView.VectorViewCtr vvModelo;
        private VectorView.VectorViewCtr vvCorte;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}