namespace MCART.Controls
{
    partial class RingGraph
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
            this.txtTitle = new System.Windows.Forms.Label();
            this.trvLabels = new System.Windows.Forms.TreeView();
            this.grdRoot = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(422, 26);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.Text = "label1";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // trvLabels
            // 
            this.trvLabels.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvLabels.Location = new System.Drawing.Point(0, 26);
            this.trvLabels.Name = "trvLabels";
            this.trvLabels.Size = new System.Drawing.Size(121, 376);
            this.trvLabels.TabIndex = 1;
            // 
            // grdRoot
            // 
            this.grdRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoot.Location = new System.Drawing.Point(121, 26);
            this.grdRoot.Name = "grdRoot";
            this.grdRoot.Size = new System.Drawing.Size(301, 376);
            this.grdRoot.TabIndex = 2;
            // 
            // RingGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdRoot);
            this.Controls.Add(this.trvLabels);
            this.Controls.Add(this.txtTitle);
            this.Name = "RingGraph";
            this.Size = new System.Drawing.Size(422, 402);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.TreeView trvLabels;
        private System.Windows.Forms.Panel grdRoot;
    }
}
