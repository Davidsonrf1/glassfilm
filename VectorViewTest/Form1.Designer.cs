﻿namespace VectorViewTest
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
            this.vectorViewCtr1 = new VectorView.VectorViewContainerCtr();
            this.SuspendLayout();
            // 
            // vectorViewCtr1
            // 
            this.vectorViewCtr1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.vectorViewCtr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vectorViewCtr1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vectorViewCtr1.Document = null;
            this.vectorViewCtr1.Location = new System.Drawing.Point(0, 0);
            this.vectorViewCtr1.Name = "vectorViewCtr1";
            this.vectorViewCtr1.RullerWidth = 22;
            this.vectorViewCtr1.ShowRuller = true;
            this.vectorViewCtr1.Size = new System.Drawing.Size(898, 511);
            this.vectorViewCtr1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 511);
            this.Controls.Add(this.vectorViewCtr1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private VectorView.VectorViewContainerCtr vectorViewCtr1;
    }
}

