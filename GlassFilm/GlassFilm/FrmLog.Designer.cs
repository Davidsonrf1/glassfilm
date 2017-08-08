namespace GlassFilm
{
    partial class FrmLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolPrincipal = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolLogs = new System.Windows.Forms.ToolStripLabel();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
            this.toolBar = new System.Windows.Forms.StatusStrip();
            this.pnlPesquisa = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabRegistros = new System.Windows.Forms.TabControl();
            this.pagLista = new System.Windows.Forms.TabPage();
            this.pnlGrade = new System.Windows.Forms.Panel();
            this.gridPrincipal = new System.Windows.Forms.DataGridView();
            this.imagensIcons = new System.Windows.Forms.ImageList(this.components);
            this.toolPrincipal.SuspendLayout();
            this.pnlPesquisa.SuspendLayout();
            this.tabRegistros.SuspendLayout();
            this.pagLista.SuspendLayout();
            this.pnlGrade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrincipal)).BeginInit();
            this.SuspendLayout();
            // 
            // toolPrincipal
            // 
            this.toolPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnSair,
            this.toolLogs});
            this.toolPrincipal.Location = new System.Drawing.Point(0, 0);
            this.toolPrincipal.Name = "toolPrincipal";
            this.toolPrincipal.Size = new System.Drawing.Size(1134, 25);
            this.toolPrincipal.TabIndex = 3;
            this.toolPrincipal.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolLogs
            // 
            this.toolLogs.Name = "toolLogs";
            this.toolLogs.Size = new System.Drawing.Size(265, 22);
            this.toolLogs.Text = "Log de todos os cortes realizados nesta máquina.";
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
            // toolBar
            // 
            this.toolBar.Location = new System.Drawing.Point(0, 436);
            this.toolBar.Name = "toolBar";
            this.toolBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.toolBar.Size = new System.Drawing.Size(1134, 22);
            this.toolBar.TabIndex = 5;
            // 
            // pnlPesquisa
            // 
            this.pnlPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.pnlPesquisa.Controls.Add(this.button1);
            this.pnlPesquisa.Controls.Add(this.label19);
            this.pnlPesquisa.Controls.Add(this.txtFiltro);
            this.pnlPesquisa.Controls.Add(this.txtPesquisa);
            this.pnlPesquisa.Controls.Add(this.label20);
            this.pnlPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPesquisa.Location = new System.Drawing.Point(0, 25);
            this.pnlPesquisa.Name = "pnlPesquisa";
            this.pnlPesquisa.Size = new System.Drawing.Size(1134, 39);
            this.pnlPesquisa.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = global::GlassFilm.Properties.Resources.magnifying_glass_search_button;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(1087, 9);
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
            this.txtFiltro.Text = "usuario";
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPesquisa.Font = new System.Drawing.Font("Verdana", 8.3F);
            this.txtPesquisa.Location = new System.Drawing.Point(241, 9);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(873, 21);
            this.txtPesquisa.TabIndex = 1;
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);
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
            // tabRegistros
            // 
            this.tabRegistros.Controls.Add(this.pagLista);
            this.tabRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRegistros.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabRegistros.ImageList = this.imagensIcons;
            this.tabRegistros.Location = new System.Drawing.Point(0, 64);
            this.tabRegistros.Name = "tabRegistros";
            this.tabRegistros.SelectedIndex = 0;
            this.tabRegistros.Size = new System.Drawing.Size(1134, 372);
            this.tabRegistros.TabIndex = 11;
            // 
            // pagLista
            // 
            this.pagLista.Controls.Add(this.pnlGrade);
            this.pagLista.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagLista.ImageIndex = 0;
            this.pagLista.Location = new System.Drawing.Point(4, 23);
            this.pagLista.Name = "pagLista";
            this.pagLista.Padding = new System.Windows.Forms.Padding(3);
            this.pagLista.Size = new System.Drawing.Size(1126, 345);
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
            this.pnlGrade.Size = new System.Drawing.Size(1120, 339);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPrincipal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridPrincipal.ColumnHeadersHeight = 22;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPrincipal.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPrincipal.Location = new System.Drawing.Point(0, 0);
            this.gridPrincipal.MultiSelect = false;
            this.gridPrincipal.Name = "gridPrincipal";
            this.gridPrincipal.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 8.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPrincipal.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridPrincipal.RowHeadersVisible = false;
            this.gridPrincipal.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPrincipal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPrincipal.Size = new System.Drawing.Size(1120, 339);
            this.gridPrincipal.TabIndex = 32;
            this.gridPrincipal.TabStop = false;
            this.gridPrincipal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPrincipal_CellClick);
            // 
            // imagensIcons
            // 
            this.imagensIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagensIcons.ImageStream")));
            this.imagensIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imagensIcons.Images.SetKeyName(0, "list.png");
            this.imagensIcons.Images.SetKeyName(1, "pencil-tool.png");
            this.imagensIcons.Images.SetKeyName(2, "pencil-and-filled-paper-sheet.png");
            this.imagensIcons.Images.SetKeyName(3, "edit-profile.png");
            this.imagensIcons.Images.SetKeyName(4, "pencil-tool (1).png");
            this.imagensIcons.Images.SetKeyName(5, "writing-message (1).png");
            this.imagensIcons.Images.SetKeyName(6, "clip.png");
            this.imagensIcons.Images.SetKeyName(7, "car.png");
            // 
            // FrmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 458);
            this.ControlBox = false;
            this.Controls.Add(this.tabRegistros);
            this.Controls.Add(this.pnlPesquisa);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.toolPrincipal);
            this.Name = "FrmLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log de Cortes";
            this.Load += new System.EventHandler(this.FrmLog_Load);
            this.toolPrincipal.ResumeLayout(false);
            this.toolPrincipal.PerformLayout();
            this.pnlPesquisa.ResumeLayout(false);
            this.pnlPesquisa.PerformLayout();
            this.tabRegistros.ResumeLayout(false);
            this.pagLista.ResumeLayout(false);
            this.pnlGrade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPrincipal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolPrincipal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSair;
        public System.Windows.Forms.ToolStripLabel toolLogs;
        private System.Windows.Forms.StatusStrip toolBar;
        private System.Windows.Forms.Panel pnlPesquisa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TabControl tabRegistros;
        private System.Windows.Forms.TabPage pagLista;
        public System.Windows.Forms.Panel pnlGrade;
        public System.Windows.Forms.DataGridView gridPrincipal;
        private System.Windows.Forms.ImageList imagensIcons;
    }
}