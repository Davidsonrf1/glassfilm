namespace GlassFilm
{
    partial class FrmCadastroDesenho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadastroDesenho));
            this.toolPrincipal = new System.Windows.Forms.ToolStrip();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
            this.toolStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.status = new System.Windows.Forms.StatusStrip();
            this.docInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.selInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlFiltroInfo = new System.Windows.Forms.Panel();
            this.btImportar = new System.Windows.Forms.Button();
            this.pbDesenho = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbPreferircad = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbEtiqueta = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDireita = new System.Windows.Forms.RadioButton();
            this.rbEsquerda = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbAnos = new System.Windows.Forms.CheckedListBox();
            this.vectorView = new VectorView.VectorViewCtr();
            this.toolPrincipal.SuspendLayout();
            this.status.SuspendLayout();
            this.pnlFiltroInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolPrincipal
            // 
            this.toolPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalvar,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.toolStripSeparator5,
            this.btnSair,
            this.toolStatus,
            this.toolStripButton2});
            this.toolPrincipal.Location = new System.Drawing.Point(0, 0);
            this.toolPrincipal.Name = "toolPrincipal";
            this.toolPrincipal.Size = new System.Drawing.Size(935, 25);
            this.toolPrincipal.TabIndex = 11;
            this.toolPrincipal.Text = "toolStrip1";
            this.toolPrincipal.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolPrincipal_ItemClicked);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(58, 22);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton1.Text = "Cancelar";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // toolStatus
            // 
            this.toolStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new System.Drawing.Size(16, 22);
            this.toolStatus.Text = "...";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton2.Image = global::GlassFilm.Properties.Resources.Settings_Backup_Sync_icon;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(0, 1, 60, 2);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(85, 22);
            this.toolStripButton2.Text = "Sincronizar";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.docInfo,
            this.selInfo});
            this.status.Location = new System.Drawing.Point(0, 531);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(935, 24);
            this.status.TabIndex = 12;
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Marca:";
            // 
            // cbMarca
            // 
            this.cbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(12, 26);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(200, 21);
            this.cbMarca.TabIndex = 0;
            this.cbMarca.SelectedIndexChanged += new System.EventHandler(this.cbMarca_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(223, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Modelo:";
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Location = new System.Drawing.Point(227, 26);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(595, 21);
            this.cbModelo.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel2.Location = new System.Drawing.Point(220, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 42);
            this.panel2.TabIndex = 16;
            // 
            // pnlFiltroInfo
            // 
            this.pnlFiltroInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.pnlFiltroInfo.Controls.Add(this.btImportar);
            this.pnlFiltroInfo.Controls.Add(this.panel2);
            this.pnlFiltroInfo.Controls.Add(this.cbModelo);
            this.pnlFiltroInfo.Controls.Add(this.label10);
            this.pnlFiltroInfo.Controls.Add(this.cbMarca);
            this.pnlFiltroInfo.Controls.Add(this.label9);
            this.pnlFiltroInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroInfo.Location = new System.Drawing.Point(0, 25);
            this.pnlFiltroInfo.Name = "pnlFiltroInfo";
            this.pnlFiltroInfo.Size = new System.Drawing.Size(935, 55);
            this.pnlFiltroInfo.TabIndex = 6;
            this.pnlFiltroInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFiltroInfo_Paint);
            // 
            // btImportar
            // 
            this.btImportar.BackColor = System.Drawing.Color.Gainsboro;
            this.btImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btImportar.Image = global::GlassFilm.Properties.Resources.import;
            this.btImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btImportar.Location = new System.Drawing.Point(828, 25);
            this.btImportar.Name = "btImportar";
            this.btImportar.Size = new System.Drawing.Size(101, 23);
            this.btImportar.TabIndex = 3;
            this.btImportar.Text = "Importar      ";
            this.btImportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btImportar.UseVisualStyleBackColor = false;
            this.btImportar.Click += new System.EventHandler(this.btImportar_Click);
            // 
            // pbDesenho
            // 
            this.pbDesenho.Location = new System.Drawing.Point(6, 520);
            this.pbDesenho.Name = "pbDesenho";
            this.pbDesenho.Size = new System.Drawing.Size(748, 8);
            this.pbDesenho.TabIndex = 15;
            this.pbDesenho.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbPreferircad);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(761, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 451);
            this.panel1.TabIndex = 17;
            // 
            // cbPreferircad
            // 
            this.cbPreferircad.AutoSize = true;
            this.cbPreferircad.Location = new System.Drawing.Point(6, 300);
            this.cbPreferircad.Name = "cbPreferircad";
            this.cbPreferircad.Size = new System.Drawing.Size(157, 17);
            this.cbPreferircad.TabIndex = 18;
            this.cbPreferircad.Text = "Preferir Posição Cadastrada";
            this.cbPreferircad.UseVisualStyleBackColor = true;
            this.cbPreferircad.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbEtiqueta);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 248);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 46);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Etiqueta";
            // 
            // tbEtiqueta
            // 
            this.tbEtiqueta.Enabled = false;
            this.tbEtiqueta.Location = new System.Drawing.Point(6, 20);
            this.tbEtiqueta.Name = "tbEtiqueta";
            this.tbEtiqueta.Size = new System.Drawing.Size(162, 20);
            this.tbEtiqueta.TabIndex = 4;
            this.tbEtiqueta.TextChanged += new System.EventHandler(this.tbEtiqueta_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDireita);
            this.groupBox2.Controls.Add(this.rbEsquerda);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 46);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lado";
            // 
            // rbDireita
            // 
            this.rbDireita.AutoSize = true;
            this.rbDireita.Enabled = false;
            this.rbDireita.Location = new System.Drawing.Point(92, 19);
            this.rbDireita.Name = "rbDireita";
            this.rbDireita.Size = new System.Drawing.Size(55, 17);
            this.rbDireita.TabIndex = 3;
            this.rbDireita.TabStop = true;
            this.rbDireita.Text = "Direita";
            this.rbDireita.UseVisualStyleBackColor = true;
            this.rbDireita.CheckedChanged += new System.EventHandler(this.rbEsquerda_CheckedChanged);
            // 
            // rbEsquerda
            // 
            this.rbEsquerda.AutoSize = true;
            this.rbEsquerda.Enabled = false;
            this.rbEsquerda.Location = new System.Drawing.Point(15, 19);
            this.rbEsquerda.Name = "rbEsquerda";
            this.rbEsquerda.Size = new System.Drawing.Size(70, 17);
            this.rbEsquerda.TabIndex = 2;
            this.rbEsquerda.TabStop = true;
            this.rbEsquerda.Text = "Esquerda";
            this.rbEsquerda.UseVisualStyleBackColor = true;
            this.rbEsquerda.CheckedChanged += new System.EventHandler(this.rbEsquerda_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbAnos);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 202);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Anos Compatíveis";
            // 
            // lbAnos
            // 
            this.lbAnos.CheckOnClick = true;
            this.lbAnos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAnos.FormattingEnabled = true;
            this.lbAnos.Location = new System.Drawing.Point(3, 16);
            this.lbAnos.Name = "lbAnos";
            this.lbAnos.Size = new System.Drawing.Size(168, 183);
            this.lbAnos.TabIndex = 0;
            this.lbAnos.SelectedIndexChanged += new System.EventHandler(this.lbAnos_SelectedValueChanged);
            // 
            // vectorView
            // 
            this.vectorView.AllowMoveDocument = false;
            this.vectorView.AllowTransforms = true;
            this.vectorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vectorView.BackColor = System.Drawing.Color.White;
            this.vectorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vectorView.ForeColor = System.Drawing.Color.Black;
            this.vectorView.GridColor = System.Drawing.Color.Green;
            this.vectorView.GridSize = 60;
            this.vectorView.Location = new System.Drawing.Point(5, 83);
            this.vectorView.Name = "vectorView";
            this.vectorView.ShowGrid = false;
            this.vectorView.Size = new System.Drawing.Size(750, 445);
            this.vectorView.TabIndex = 7;
            this.vectorView.SelectionChanged += new VectorView.VectorEventHandler(this.vectorView_SelectionChanged);
            // 
            // FrmCadastroDesenho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 555);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbDesenho);
            this.Controls.Add(this.status);
            this.Controls.Add(this.vectorView);
            this.Controls.Add(this.pnlFiltroInfo);
            this.Controls.Add(this.toolPrincipal);
            this.MinimizeBox = false;
            this.Name = "FrmCadastroDesenho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Desenhos";
            this.Load += new System.EventHandler(this.FrmCadastroDesenho_Load);
            this.Resize += new System.EventHandler(this.FrmCadastroDesenho_Resize);
            this.toolPrincipal.ResumeLayout(false);
            this.toolPrincipal.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.pnlFiltroInfo.ResumeLayout(false);
            this.pnlFiltroInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private VectorView.VectorViewCtr vectorView;
        private System.Windows.Forms.ToolStrip toolPrincipal;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSair;
        private System.Windows.Forms.ToolStripLabel toolStatus;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel docInfo;
        private System.Windows.Forms.ToolStripStatusLabel selInfo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btImportar;
        private System.Windows.Forms.Panel pnlFiltroInfo;
        private System.Windows.Forms.ProgressBar pbDesenho;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox lbAnos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDireita;
        private System.Windows.Forms.RadioButton rbEsquerda;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbEtiqueta;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.CheckBox cbPreferircad;
    }
}