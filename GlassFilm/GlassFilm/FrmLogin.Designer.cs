namespace GlassFilm
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pnlAguardando = new System.Windows.Forms.Panel();
            this.lbMensagem = new System.Windows.Forms.Label();
            this.pnlToken = new System.Windows.Forms.Panel();
            this.btnCancelarToken = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnValidarToken = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlLogin.SuspendLayout();
            this.pnlAguardando.SuspendLayout();
            this.pnlToken.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.pnlLogin.Controls.Add(this.pnlAguardando);
            this.pnlLogin.Controls.Add(this.pnlToken);
            this.pnlLogin.Controls.Add(this.pictureBox3);
            this.pnlLogin.Controls.Add(this.pictureBox2);
            this.pnlLogin.Controls.Add(this.txtSenha);
            this.pnlLogin.Controls.Add(this.label2);
            this.pnlLogin.Controls.Add(this.txtNome);
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Controls.Add(this.btnCancelar);
            this.pnlLogin.Controls.Add(this.btnEntrar);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogin.Location = new System.Drawing.Point(0, 215);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(358, 93);
            this.pnlLogin.TabIndex = 7;
            // 
            // pnlAguardando
            // 
            this.pnlAguardando.Controls.Add(this.lbMensagem);
            this.pnlAguardando.Location = new System.Drawing.Point(6, 9);
            this.pnlAguardando.Name = "pnlAguardando";
            this.pnlAguardando.Size = new System.Drawing.Size(349, 77);
            this.pnlAguardando.TabIndex = 13;
            // 
            // lbMensagem
            // 
            this.lbMensagem.AutoSize = true;
            this.lbMensagem.ForeColor = System.Drawing.Color.White;
            this.lbMensagem.Location = new System.Drawing.Point(117, 33);
            this.lbMensagem.Name = "lbMensagem";
            this.lbMensagem.Size = new System.Drawing.Size(16, 13);
            this.lbMensagem.TabIndex = 0;
            this.lbMensagem.Text = "...";
            // 
            // pnlToken
            // 
            this.pnlToken.Controls.Add(this.btnCancelarToken);
            this.pnlToken.Controls.Add(this.pictureBox4);
            this.pnlToken.Controls.Add(this.txtToken);
            this.pnlToken.Controls.Add(this.label3);
            this.pnlToken.Controls.Add(this.btnValidarToken);
            this.pnlToken.Location = new System.Drawing.Point(6, 5);
            this.pnlToken.Name = "pnlToken";
            this.pnlToken.Size = new System.Drawing.Size(348, 84);
            this.pnlToken.TabIndex = 12;
            this.pnlToken.Visible = false;
            // 
            // btnCancelarToken
            // 
            this.btnCancelarToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.btnCancelarToken.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarToken.ForeColor = System.Drawing.Color.Black;
            this.btnCancelarToken.Location = new System.Drawing.Point(280, 28);
            this.btnCancelarToken.Name = "btnCancelarToken";
            this.btnCancelarToken.Size = new System.Drawing.Size(62, 23);
            this.btnCancelarToken.TabIndex = 16;
            this.btnCancelarToken.Text = "Cancelar";
            this.btnCancelarToken.UseVisualStyleBackColor = false;
            this.btnCancelarToken.Click += new System.EventHandler(this.btnCancelarToken_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::GlassFilm.Properties.Resources.identification_tokens;
            this.pictureBox4.Location = new System.Drawing.Point(13, 22);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(28, 33);
            this.pictureBox4.TabIndex = 15;
            this.pictureBox4.TabStop = false;
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(102, 30);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(94, 20);
            this.txtToken.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(51, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Token";
            // 
            // btnValidarToken
            // 
            this.btnValidarToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.btnValidarToken.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidarToken.ForeColor = System.Drawing.Color.Black;
            this.btnValidarToken.Location = new System.Drawing.Point(202, 28);
            this.btnValidarToken.Name = "btnValidarToken";
            this.btnValidarToken.Size = new System.Drawing.Size(75, 23);
            this.btnValidarToken.TabIndex = 13;
            this.btnValidarToken.Text = "Validar";
            this.btnValidarToken.UseVisualStyleBackColor = false;
            this.btnValidarToken.Click += new System.EventHandler(this.btnValidarToken_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(24, 17);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(26, 28);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GlassFilm.Properties.Resources._12;
            this.pictureBox2.Location = new System.Drawing.Point(24, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 29);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(112, 55);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(144, 20);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(58, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Senha";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(112, 21);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(144, 20);
            this.txtNome.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(59, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Login";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.btnCancelar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(262, 54);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.btnEntrar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.Black;
            this.btnEntrar.Location = new System.Drawing.Point(262, 20);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(75, 23);
            this.btnEntrar.TabIndex = 2;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GlassFilm.Properties.Resources.logoComputer2;
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(351, 202);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(358, 308);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bem vindo ao Glass Film";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlAguardando.ResumeLayout(false);
            this.pnlAguardando.PerformLayout();
            this.pnlToken.ResumeLayout(false);
            this.pnlToken.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel pnlToken;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnValidarToken;
        private System.Windows.Forms.Panel pnlAguardando;
        private System.Windows.Forms.Button btnCancelarToken;
        private System.Windows.Forms.Label lbMensagem;
    }
}

