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
            this.pnlFiltroInfo = new System.Windows.Forms.Panel();
            this.btImportar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbAno = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.toolPrincipal = new System.Windows.Forms.ToolStrip();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
            this.toolStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolBar = new System.Windows.Forms.StatusStrip();
            this.vectorView = new VectorView.VectorViewCtr();
            this.pnlFiltroInfo.SuspendLayout();
            this.toolPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFiltroInfo
            // 
            this.pnlFiltroInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(104)))), ((int)(((byte)(46)))));
            this.pnlFiltroInfo.Controls.Add(this.btImportar);
            this.pnlFiltroInfo.Controls.Add(this.panel1);
            this.pnlFiltroInfo.Controls.Add(this.panel2);
            this.pnlFiltroInfo.Controls.Add(this.cbModelo);
            this.pnlFiltroInfo.Controls.Add(this.label10);
            this.pnlFiltroInfo.Controls.Add(this.cbMarca);
            this.pnlFiltroInfo.Controls.Add(this.label9);
            this.pnlFiltroInfo.Controls.Add(this.cbAno);
            this.pnlFiltroInfo.Controls.Add(this.label8);
            this.pnlFiltroInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroInfo.Location = new System.Drawing.Point(0, 25);
            this.pnlFiltroInfo.Name = "pnlFiltroInfo";
            this.pnlFiltroInfo.Size = new System.Drawing.Size(847, 55);
            this.pnlFiltroInfo.TabIndex = 6;
            // 
            // btImportar
            // 
            this.btImportar.BackColor = System.Drawing.Color.Gainsboro;
            this.btImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btImportar.Image = global::GlassFilm.Properties.Resources.import;
            this.btImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btImportar.Location = new System.Drawing.Point(676, 25);
            this.btImportar.Name = "btImportar";
            this.btImportar.Size = new System.Drawing.Size(101, 23);
            this.btImportar.TabIndex = 3;
            this.btImportar.Text = "Importar      ";
            this.btImportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btImportar.UseVisualStyleBackColor = false;
            this.btImportar.Click += new System.EventHandler(this.btImportar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel1.Location = new System.Drawing.Point(563, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 42);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel2.Location = new System.Drawing.Point(220, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 42);
            this.panel2.TabIndex = 16;
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Location = new System.Drawing.Point(227, 26);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(330, 21);
            this.cbModelo.TabIndex = 1;
            this.cbModelo.SelectedIndexChanged += new System.EventHandler(this.cbModelo_SelectedIndexChanged);
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
            // cbAno
            // 
            this.cbAno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAno.FormattingEnabled = true;
            this.cbAno.Location = new System.Drawing.Point(573, 26);
            this.cbAno.Name = "cbAno";
            this.cbAno.Size = new System.Drawing.Size(89, 21);
            this.cbAno.TabIndex = 2;
            this.cbAno.SelectedIndexChanged += new System.EventHandler(this.cbAno_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(570, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Ano:";
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
            this.toolStatus});
            this.toolPrincipal.Location = new System.Drawing.Point(0, 0);
            this.toolPrincipal.Name = "toolPrincipal";
            this.toolPrincipal.Size = new System.Drawing.Size(847, 25);
            this.toolPrincipal.TabIndex = 11;
            this.toolPrincipal.Text = "toolStrip1";
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
            // toolBar
            // 
            this.toolBar.Location = new System.Drawing.Point(0, 533);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.toolBar.Size = new System.Drawing.Size(847, 22);
            this.toolBar.TabIndex = 14;
            // 
            // vectorView
            // 
            this.vectorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vectorView.BackColor = System.Drawing.Color.White;
            this.vectorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vectorView.Document = null;
            this.vectorView.FitStyle = VectorView.VectorViewFitStyle.Both;
            this.vectorView.ForeColor = System.Drawing.Color.Black;
            this.vectorView.Location = new System.Drawing.Point(5, 83);
            this.vectorView.Name = "vectorView";

            this.vectorView.Size = new System.Drawing.Size(836, 447);
            this.vectorView.TabIndex = 7;
            // 
            // FrmCadastroDesenho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 555);
            this.ControlBox = false;
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.vectorView);
            this.Controls.Add(this.pnlFiltroInfo);
            this.Controls.Add(this.toolPrincipal);
            this.MinimizeBox = false;
            this.Name = "FrmCadastroDesenho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Desenhos";
            this.Load += new System.EventHandler(this.FrmCadastroDesenho_Load);
            this.Resize += new System.EventHandler(this.FrmCadastroDesenho_Resize);
            this.pnlFiltroInfo.ResumeLayout(false);
            this.pnlFiltroInfo.PerformLayout();
            this.toolPrincipal.ResumeLayout(false);
            this.toolPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlFiltroInfo;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbAno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private VectorView.VectorViewCtr vectorView;
        private System.Windows.Forms.Button btImportar;
        private System.Windows.Forms.ToolStrip toolPrincipal;
        private System.Windows.Forms.ToolStripButton btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSair;
        private System.Windows.Forms.ToolStripLabel toolStatus;
        private System.Windows.Forms.StatusStrip toolBar;
    }
}