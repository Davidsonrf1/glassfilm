namespace GlassFilm
{
    partial class FrmNovosVeiculos
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
            this.cbVisualizado = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lvUpdate = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cbVisualizado);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 345);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 37);
            this.panel1.TabIndex = 1;
            // 
            // cbVisualizado
            // 
            this.cbVisualizado.AutoSize = true;
            this.cbVisualizado.Location = new System.Drawing.Point(12, 12);
            this.cbVisualizado.Name = "cbVisualizado";
            this.cbVisualizado.Size = new System.Drawing.Size(446, 17);
            this.cbVisualizado.TabIndex = 0;
            this.cbVisualizado.Text = "Não mostrar essa tela novamente (Será exibida somente em caso de novas atualizaçõ" +
    "es)";
            this.cbVisualizado.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(879, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lvUpdate
            // 
            this.lvUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUpdate.Location = new System.Drawing.Point(0, 0);
            this.lvUpdate.Name = "lvUpdate";
            this.lvUpdate.Size = new System.Drawing.Size(963, 339);
            this.lvUpdate.TabIndex = 2;
            this.lvUpdate.UseCompatibleStateImageBehavior = false;
            // 
            // FrmNovosVeiculos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 382);
            this.Controls.Add(this.lvUpdate);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmNovosVeiculos";
            this.Text = "Novas atualizações";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbVisualizado;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView lvUpdate;
    }
}