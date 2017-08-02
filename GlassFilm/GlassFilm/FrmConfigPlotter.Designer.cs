namespace GlassFilm
{
    partial class FrmConfigPlotter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbInterface = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbPlotterName = new System.Windows.Forms.Label();
            this.cbRotate = new System.Windows.Forms.CheckBox();
            this.cbFlip = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnEntrar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 51);
            this.panel1.TabIndex = 8;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(250, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEntrar
            // 
            this.btnEntrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntrar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.Black;
            this.btnEntrar.Location = new System.Drawing.Point(331, 13);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(75, 23);
            this.btnEntrar.TabIndex = 2;
            this.btnEntrar.Text = "Gravar";
            this.btnEntrar.UseVisualStyleBackColor = true;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 10;
            this.label1.Text = "Interface";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbInterface
            // 
            this.cbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterface.FormattingEnabled = true;
            this.cbInterface.Items.AddRange(new object[] {
            "PRINTER",
            "SERIAL"});
            this.cbInterface.Location = new System.Drawing.Point(111, 36);
            this.cbInterface.Name = "cbInterface";
            this.cbInterface.Size = new System.Drawing.Size(268, 21);
            this.cbInterface.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Linguagem";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbLang
            // 
            this.cbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Items.AddRange(new object[] {
            "HPGL",
            "DMPL"});
            this.cbLang.Location = new System.Drawing.Point(111, 9);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(299, 21);
            this.cbLang.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(381, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbPlotterName
            // 
            this.lbPlotterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPlotterName.Location = new System.Drawing.Point(11, 80);
            this.lbPlotterName.Name = "lbPlotterName";
            this.lbPlotterName.Size = new System.Drawing.Size(399, 23);
            this.lbPlotterName.TabIndex = 16;
            this.lbPlotterName.Text = "SELECIONE O NOME OU PORTA DA PLOTTER";
            this.lbPlotterName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbRotate
            // 
            this.cbRotate.AutoSize = true;
            this.cbRotate.Location = new System.Drawing.Point(11, 60);
            this.cbRotate.Name = "cbRotate";
            this.cbRotate.Size = new System.Drawing.Size(142, 17);
            this.cbRotate.TabIndex = 17;
            this.cbRotate.Text = "Rotacionar Corte em 90º";
            this.cbRotate.UseVisualStyleBackColor = true;
            // 
            // cbFlip
            // 
            this.cbFlip.AutoSize = true;
            this.cbFlip.Location = new System.Drawing.Point(315, 60);
            this.cbFlip.Name = "cbFlip";
            this.cbFlip.Size = new System.Drawing.Size(95, 17);
            this.cbFlip.TabIndex = 18;
            this.cbFlip.Text = "Espelhar Corte";
            this.cbFlip.UseVisualStyleBackColor = true;
            // 
            // FrmConfigPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(419, 159);
            this.Controls.Add(this.cbFlip);
            this.Controls.Add(this.cbRotate);
            this.Controls.Add(this.lbPlotterName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbLang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbInterface);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfigPlotter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parâmetros da Cortadora";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbInterface;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLang;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbPlotterName;
        private System.Windows.Forms.CheckBox cbRotate;
        private System.Windows.Forms.CheckBox cbFlip;
    }
}