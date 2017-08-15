namespace TestApp_Win32
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.ms1 = new System.Windows.Forms.MenuStrip();
            this.mnu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu2a = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestPw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetPw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenPw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenPinPw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenPwPw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenSecurePw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu2b = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConTsk = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWndTsk = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPluginBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAboutMCART = new System.Windows.Forms.ToolStripMenuItem();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.ms1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            this.SuspendLayout();
            // 
            // ms1
            // 
            this.ms1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu1,
            this.mnu2,
            this.mnuPlugins,
            this.mnu3});
            this.ms1.Location = new System.Drawing.Point(0, 0);
            this.ms1.Name = "ms1";
            this.ms1.Size = new System.Drawing.Size(509, 24);
            this.ms1.TabIndex = 0;
            this.ms1.Text = "menuStrip1";
            // 
            // mnu1
            // 
            this.mnu1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScreenshot,
            this.mnuSep1,
            this.mnuExit});
            this.mnu1.Name = "mnu1";
            this.mnu1.Size = new System.Drawing.Size(60, 20);
            this.mnu1.Text = "&Archivo";
            // 
            // mnuScreenshot
            // 
            this.mnuScreenshot.Name = "mnuScreenshot";
            this.mnuScreenshot.Size = new System.Drawing.Size(186, 22);
            this.mnuScreenshot.Text = "&Captura de pantalla...";
            this.mnuScreenshot.Click += new System.EventHandler(this.MnuScreenshot_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(183, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(186, 22);
            this.mnuExit.Text = "&Salir";
            this.mnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // mnu2
            // 
            this.mnu2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu2a,
            this.mnu2b});
            this.mnu2.Name = "mnu2";
            this.mnu2.Size = new System.Drawing.Size(61, 20);
            this.mnu2.Text = "&Pruebas";
            // 
            // mnu2a
            // 
            this.mnu2a.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTestPw,
            this.mnuSetPw,
            this.mnuGenPw});
            this.mnu2a.Name = "mnu2a";
            this.mnu2a.Size = new System.Drawing.Size(152, 22);
            this.mnu2a.Text = "&Contraseñas";
            // 
            // mnuTestPw
            // 
            this.mnuTestPw.Name = "mnuTestPw";
            this.mnuTestPw.Size = new System.Drawing.Size(197, 22);
            this.mnuTestPw.Text = "Escribir &contraseña...";
            this.mnuTestPw.Click += new System.EventHandler(this.MnuTestPw_Click);
            // 
            // mnuSetPw
            // 
            this.mnuSetPw.Name = "mnuSetPw";
            this.mnuSetPw.Size = new System.Drawing.Size(197, 22);
            this.mnuSetPw.Text = "&Establecer contraseña...";
            // 
            // mnuGenPw
            // 
            this.mnuGenPw.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenPinPw,
            this.mnuGenPwPw,
            this.mnuGenSecurePw});
            this.mnuGenPw.Name = "mnuGenPw";
            this.mnuGenPw.Size = new System.Drawing.Size(197, 22);
            this.mnuGenPw.Text = "&Generar contraseña";
            // 
            // mnuGenPinPw
            // 
            this.mnuGenPinPw.Name = "mnuGenPinPw";
            this.mnuGenPinPw.Size = new System.Drawing.Size(181, 22);
            this.mnuGenPinPw.Text = "&Pin...";
            this.mnuGenPinPw.Click += new System.EventHandler(this.MnuGenPin_Click);
            // 
            // mnuGenPwPw
            // 
            this.mnuGenPwPw.Name = "mnuGenPwPw";
            this.mnuGenPwPw.Size = new System.Drawing.Size(181, 22);
            this.mnuGenPwPw.Text = "&Contraseña...";
            this.mnuGenPwPw.Click += new System.EventHandler(this.MnuGenPwPw_Click);
            // 
            // mnuGenSecurePw
            // 
            this.mnuGenSecurePw.Name = "mnuGenSecurePw";
            this.mnuGenSecurePw.Size = new System.Drawing.Size(181, 22);
            this.mnuGenSecurePw.Text = "Contrasena &segura...";
            this.mnuGenSecurePw.Click += new System.EventHandler(this.MnuGenSecurePw_Click);
            // 
            // mnu2b
            // 
            this.mnu2b.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConTsk,
            this.mnuWndTsk});
            this.mnu2b.Name = "mnu2b";
            this.mnu2b.Size = new System.Drawing.Size(145, 22);
            this.mnu2b.Text = "ITaskReporter";
            // 
            // mnuConTsk
            // 
            this.mnuConTsk.Name = "mnuConTsk";
            this.mnuConTsk.Size = new System.Drawing.Size(172, 22);
            this.mnuConTsk.Text = "Tarea de consola...";
            // 
            // mnuWndTsk
            // 
            this.mnuWndTsk.Name = "mnuWndTsk";
            this.mnuWndTsk.Size = new System.Drawing.Size(172, 22);
            this.mnuWndTsk.Text = "Tarea de ventana...";
            // 
            // mnuPlugins
            // 
            this.mnuPlugins.Enabled = false;
            this.mnuPlugins.Name = "mnuPlugins";
            this.mnuPlugins.Size = new System.Drawing.Size(58, 20);
            this.mnuPlugins.Text = "P&lugins";
            // 
            // mnu3
            // 
            this.mnu3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPluginBrowser,
            this.mnuSep2,
            this.mnuAboutMCART});
            this.mnu3.Name = "mnu3";
            this.mnu3.Size = new System.Drawing.Size(53, 20);
            this.mnu3.Text = "A&yuda";
            // 
            // mnuPluginBrowser
            // 
            this.mnuPluginBrowser.Name = "mnuPluginBrowser";
            this.mnuPluginBrowser.Size = new System.Drawing.Size(197, 22);
            this.mnuPluginBrowser.Text = "&Explorador de plugins...";
            // 
            // mnuSep2
            // 
            this.mnuSep2.Name = "mnuSep2";
            this.mnuSep2.Size = new System.Drawing.Size(194, 6);
            // 
            // mnuAboutMCART
            // 
            this.mnuAboutMCART.Name = "mnuAboutMCART";
            this.mnuAboutMCART.Size = new System.Drawing.Size(197, 22);
            this.mnuAboutMCART.Text = "&Acerca de MCART...";
            // 
            // pb1
            // 
            this.pb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb1.Image = global::TestApp_Win32.Properties.Resources.MCART;
            this.pb1.Location = new System.Drawing.Point(0, 24);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(509, 287);
            this.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb1.TabIndex = 1;
            this.pb1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 311);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.ms1);
            this.MainMenuStrip = this.ms1;
            this.Name = "Form1";
            this.Text = "MCART Test Application";
            this.ms1.ResumeLayout(false);
            this.ms1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ms1;
        private System.Windows.Forms.ToolStripMenuItem mnu1;
        private System.Windows.Forms.ToolStripMenuItem mnuScreenshot;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnu2;
        private System.Windows.Forms.ToolStripMenuItem mnu2a;
        private System.Windows.Forms.ToolStripMenuItem mnuTestPw;
        private System.Windows.Forms.ToolStripMenuItem mnuSetPw;
        private System.Windows.Forms.ToolStripMenuItem mnuGenPw;
        private System.Windows.Forms.ToolStripMenuItem mnuGenPinPw;
        private System.Windows.Forms.ToolStripMenuItem mnuGenPwPw;
        private System.Windows.Forms.ToolStripMenuItem mnuGenSecurePw;
        private System.Windows.Forms.ToolStripMenuItem mnu2b;
        private System.Windows.Forms.ToolStripMenuItem mnuConTsk;
        private System.Windows.Forms.ToolStripMenuItem mnuWndTsk;
        private System.Windows.Forms.ToolStripMenuItem mnuPlugins;
        private System.Windows.Forms.ToolStripMenuItem mnu3;
        private System.Windows.Forms.ToolStripMenuItem mnuPluginBrowser;
        private System.Windows.Forms.ToolStripSeparator mnuSep2;
        private System.Windows.Forms.ToolStripMenuItem mnuAboutMCART;
        private System.Windows.Forms.PictureBox pb1;
    }
}

