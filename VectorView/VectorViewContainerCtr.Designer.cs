namespace VectorView
{
    partial class VectorViewContainerCtr
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.view = new VectorView.VectorViewCtr();
            this.SuspendLayout();
            // 
            // view
            // 
            this.view.BackColor = System.Drawing.Color.White;
            this.view.Document = null;
            this.view.FitStyle = VectorView.VectorViewFitStyle.Both;
            this.view.Location = new System.Drawing.Point(125, 28);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(332, 281);
            this.view.TabIndex = 0;
            // 
            // VectorViewContainerCtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.view);
            this.Name = "VectorViewContainerCtr";
            this.Size = new System.Drawing.Size(485, 322);
            this.ResumeLayout(false);

        }

        #endregion

        private VectorViewCtr view;
    }
}
