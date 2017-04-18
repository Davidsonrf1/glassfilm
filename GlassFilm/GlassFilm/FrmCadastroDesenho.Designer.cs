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
            this.pnlFiltroInfo = new System.Windows.Forms.Panel();
            this.btImportar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbMarca = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbAno = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.vectorView = new VectorView.VectorViewCtr();
            this.pnlFiltroInfo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFiltroInfo
            // 
            this.pnlFiltroInfo.BackColor = System.Drawing.Color.White;
            this.pnlFiltroInfo.Controls.Add(this.btImportar);
            this.pnlFiltroInfo.Controls.Add(this.panel1);
            this.pnlFiltroInfo.Controls.Add(this.panel2);
            this.pnlFiltroInfo.Controls.Add(this.panel8);
            this.pnlFiltroInfo.Controls.Add(this.cbModelo);
            this.pnlFiltroInfo.Controls.Add(this.label10);
            this.pnlFiltroInfo.Controls.Add(this.cbMarca);
            this.pnlFiltroInfo.Controls.Add(this.label9);
            this.pnlFiltroInfo.Controls.Add(this.cbAno);
            this.pnlFiltroInfo.Controls.Add(this.label8);
            this.pnlFiltroInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltroInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlFiltroInfo.Name = "pnlFiltroInfo";
            this.pnlFiltroInfo.Size = new System.Drawing.Size(769, 55);
            this.pnlFiltroInfo.TabIndex = 6;
            // 
            // btImportar
            // 
            this.btImportar.Location = new System.Drawing.Point(676, 25);
            this.btImportar.Name = "btImportar";
            this.btImportar.Size = new System.Drawing.Size(81, 23);
            this.btImportar.TabIndex = 18;
            this.btImportar.Text = "Importar";
            this.btImportar.UseVisualStyleBackColor = true;
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
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.panel8.Location = new System.Drawing.Point(12, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1400, 1);
            this.panel8.TabIndex = 14;
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Location = new System.Drawing.Point(227, 26);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(330, 21);
            this.cbModelo.TabIndex = 12;
            this.cbModelo.SelectedIndexChanged += new System.EventHandler(this.cbModelo_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(223, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Modelo:";
            // 
            // cbMarca
            // 
            this.cbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarca.FormattingEnabled = true;
            this.cbMarca.Location = new System.Drawing.Point(12, 26);
            this.cbMarca.Name = "cbMarca";
            this.cbMarca.Size = new System.Drawing.Size(200, 21);
            this.cbMarca.TabIndex = 9;
            this.cbMarca.SelectedIndexChanged += new System.EventHandler(this.cbMarca_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Marca:";
            // 
            // cbAno
            // 
            this.cbAno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAno.FormattingEnabled = true;
            this.cbAno.Location = new System.Drawing.Point(573, 26);
            this.cbAno.Name = "cbAno";
            this.cbAno.Size = new System.Drawing.Size(89, 21);
            this.cbAno.TabIndex = 3;
            this.cbAno.SelectedIndexChanged += new System.EventHandler(this.cbAno_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(570, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Ano:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(104)))), ((int)(((byte)(46)))));
            this.panel3.Controls.Add(this.btnCancelar);
            this.panel3.Controls.Add(this.btnEntrar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 523);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(769, 51);
            this.panel3.TabIndex = 9;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(600, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnEntrar
            // 
            this.btnEntrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntrar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.Black;
            this.btnEntrar.Location = new System.Drawing.Point(681, 13);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(75, 23);
            this.btnEntrar.TabIndex = 2;
            this.btnEntrar.Text = "Gravar";
            this.btnEntrar.UseVisualStyleBackColor = true;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // vectorView
            // 
            this.vectorView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vectorView.BackColor = System.Drawing.Color.White;
            this.vectorView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vectorView.Document = null;
            this.vectorView.Location = new System.Drawing.Point(0, 55);
            this.vectorView.Name = "vectorView";
            this.vectorView.ShowRuller = false;
            this.vectorView.Size = new System.Drawing.Size(769, 468);
            this.vectorView.TabIndex = 7;
            // 
            // FrmCadastroDesenho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 574);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.vectorView);
            this.Controls.Add(this.pnlFiltroInfo);
            this.Name = "FrmCadastroDesenho";
            this.Text = "Cadastro de Desenhos";
            this.Load += new System.EventHandler(this.FrmCadastroDesenho_Load);
            this.pnlFiltroInfo.ResumeLayout(false);
            this.pnlFiltroInfo.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFiltroInfo;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbMarca;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbAno;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private VectorView.VectorViewCtr vectorView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Button btImportar;
    }
}