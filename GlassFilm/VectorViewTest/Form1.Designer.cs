namespace VectorViewTest
{
    partial class Form1
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
            this.vv = new VectorView.VectorViewCtr();
            this.SuspendLayout();
            // 
            // vv
            // 
            this.vv.AllowMoveDocument = false;
            this.vv.AllowTransforms = false;
            this.vv.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.vv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vv.GridColor = System.Drawing.Color.DarkGray;
            this.vv.GridSize = 8;
            this.vv.Location = new System.Drawing.Point(0, 0);
            this.vv.Name = "vv";
            this.vv.ShowGrid = false;
            this.vv.Size = new System.Drawing.Size(535, 288);
            this.vv.TabIndex = 0;
            this.vv.SelectionChanged += new VectorView.VectorEventHandler(this.vv_SelectionChanged);
            this.vv.Load += new System.EventHandler(this.vv_Load);
            this.vv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.vv_MouseClick);
            this.vv.MouseMove += new System.Windows.Forms.MouseEventHandler(this.vv_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 288);
            this.Controls.Add(this.vv);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VectorView.VectorViewCtr vv;
    }
}

