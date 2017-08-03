namespace GlassFilm
{
    partial class FrmSync
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
            this.pb = new System.Windows.Forms.ProgressBar();
            this.lbAtualizando = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(-1, 142);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(400, 14);
            this.pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb.TabIndex = 0;
            // 
            // lbAtualizando
            // 
            this.lbAtualizando.AutoSize = true;
            this.lbAtualizando.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbAtualizando.Location = new System.Drawing.Point(5, 124);
            this.lbAtualizando.Name = "lbAtualizando";
            this.lbAtualizando.Size = new System.Drawing.Size(59, 13);
            this.lbAtualizando.TabIndex = 2;
            this.lbAtualizando.Text = "Iniciando...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GlassFilm.Properties.Resources.logoComputer2;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 143);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(200)))), ((int)(((byte)(203)))));
            this.label1.Location = new System.Drawing.Point(80, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sincronizando";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 10;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // FrmSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(400, 155);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.lbAtualizando);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSync";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sincronização";
            this.Load += new System.EventHandler(this.FrmSync_Load);
            this.Shown += new System.EventHandler(this.FrmSync_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Label lbAtualizando;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrUpdate;
    }
}