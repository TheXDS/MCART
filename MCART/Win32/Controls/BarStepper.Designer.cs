namespace MCART.Controls
{
    partial class BarStepper : MCART.Types.TaskReporter.TaskReporterControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        void InitializeComponent()
        {
            this.pbrProgres = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.flpRoot = new System.Windows.Forms.FlowLayoutPanel();
            this.flpRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbrProgres
            // 
            this.pbrProgres.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbrProgres.Location = new System.Drawing.Point(3, 3);
            this.pbrProgres.Name = "pbrProgres";
            this.pbrProgres.Size = new System.Drawing.Size(100, 16);
            this.pbrProgres.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.Location = new System.Drawing.Point(106, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(132, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // flpRoot
            // 
            this.flpRoot.Controls.Add(this.pbrProgres);
            this.flpRoot.Controls.Add(this.btnCancel);
            this.flpRoot.Controls.Add(this.lblStatus);
            this.flpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRoot.Location = new System.Drawing.Point(0, 0);
            this.flpRoot.Name = "flpRoot";
            this.flpRoot.Size = new System.Drawing.Size(150, 24);
            this.flpRoot.TabIndex = 3;
            // 
            // BarStepper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpRoot);
            this.MaximumSize = new System.Drawing.Size(0, 24);
            this.MinimumSize = new System.Drawing.Size(150, 24);
            this.Name = "BarStepper";
            this.Size = new System.Drawing.Size(150, 24);
            this.flpRoot.ResumeLayout(false);
            this.flpRoot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.ProgressBar pbrProgres;
        System.Windows.Forms.Button btnCancel;
        System.Windows.Forms.Label lblStatus;
        System.Windows.Forms.FlowLayoutPanel flpRoot;
    }
}
