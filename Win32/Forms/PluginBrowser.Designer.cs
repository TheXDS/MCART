namespace MCART.Forms
{
    partial class PluginBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsBeta = new System.Windows.Forms.CheckBox();
            this.chkUnstable = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtCopyright = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstInterfaces = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtMinVer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVeredict = new System.Windows.Forms.Label();
            this.txtTgtVer = new System.Windows.Forms.TextBox();
            this.tabInteractions = new System.Windows.Forms.TabPage();
            this.mnuInteractions = new System.Windows.Forms.MenuStrip();
            this.trvAsm = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabInteractions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(497, 326);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Versión";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Descripción";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Copyright";
            // 
            // chkIsBeta
            // 
            this.chkIsBeta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsBeta.AutoSize = true;
            this.chkIsBeta.Enabled = false;
            this.chkIsBeta.Location = new System.Drawing.Point(228, 29);
            this.chkIsBeta.Name = "chkIsBeta";
            this.chkIsBeta.Size = new System.Drawing.Size(48, 17);
            this.chkIsBeta.TabIndex = 4;
            this.chkIsBeta.Text = "Beta";
            this.chkIsBeta.UseVisualStyleBackColor = true;
            // 
            // chkUnstable
            // 
            this.chkUnstable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUnstable.AutoSize = true;
            this.chkUnstable.Enabled = false;
            this.chkUnstable.Location = new System.Drawing.Point(283, 29);
            this.chkUnstable.Name = "chkUnstable";
            this.chkUnstable.Size = new System.Drawing.Size(69, 17);
            this.chkUnstable.TabIndex = 5;
            this.chkUnstable.Text = "Inestable";
            this.chkUnstable.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(88, 0);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(264, 20);
            this.txtName.TabIndex = 6;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(88, 27);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(134, 20);
            this.txtVersion.TabIndex = 7;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(6, 72);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.Size = new System.Drawing.Size(346, 20);
            this.txtDesc.TabIndex = 8;
            // 
            // txtCopyright
            // 
            this.txtCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopyright.Location = new System.Drawing.Point(88, 98);
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.ReadOnly = true;
            this.txtCopyright.Size = new System.Drawing.Size(264, 20);
            this.txtCopyright.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabInteractions);
            this.tabControl1.Location = new System.Drawing.Point(3, 124);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(349, 184);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLicense);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(341, 158);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Licencia";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtLicense
            // 
            this.txtLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLicense.Location = new System.Drawing.Point(3, 3);
            this.txtLicense.Multiline = true;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.ReadOnly = true;
            this.txtLicense.Size = new System.Drawing.Size(335, 152);
            this.txtLicense.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lstInterfaces);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(341, 158);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Interfaces";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstInterfaces
            // 
            this.lstInterfaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInterfaces.FormattingEnabled = true;
            this.lstInterfaces.Location = new System.Drawing.Point(3, 3);
            this.lstInterfaces.Name = "lstInterfaces";
            this.lstInterfaces.Size = new System.Drawing.Size(335, 152);
            this.lstInterfaces.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(341, 158);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Compatibilidad";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtMinVer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblVeredict, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtTgtVer, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 152);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtMinVer
            // 
            this.txtMinVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinVer.Location = new System.Drawing.Point(170, 15);
            this.txtMinVer.Name = "txtMinVer";
            this.txtMinVer.ReadOnly = true;
            this.txtMinVer.Size = new System.Drawing.Size(162, 20);
            this.txtMinVer.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 50);
            this.label5.TabIndex = 2;
            this.label5.Text = "Versión mínima de MCART";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 50);
            this.label6.TabIndex = 3;
            this.label6.Text = "Versión objetivo de MCART";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVeredict
            // 
            this.lblVeredict.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblVeredict, 2);
            this.lblVeredict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVeredict.Location = new System.Drawing.Point(3, 100);
            this.lblVeredict.Name = "lblVeredict";
            this.lblVeredict.Size = new System.Drawing.Size(329, 52);
            this.lblVeredict.TabIndex = 4;
            this.lblVeredict.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTgtVer
            // 
            this.txtTgtVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTgtVer.Location = new System.Drawing.Point(170, 65);
            this.txtTgtVer.Name = "txtTgtVer";
            this.txtTgtVer.ReadOnly = true;
            this.txtTgtVer.Size = new System.Drawing.Size(162, 20);
            this.txtTgtVer.TabIndex = 1;
            // 
            // tabInteractions
            // 
            this.tabInteractions.BackColor = System.Drawing.SystemColors.Control;
            this.tabInteractions.Controls.Add(this.mnuInteractions);
            this.tabInteractions.Location = new System.Drawing.Point(4, 22);
            this.tabInteractions.Name = "tabInteractions";
            this.tabInteractions.Padding = new System.Windows.Forms.Padding(3);
            this.tabInteractions.Size = new System.Drawing.Size(341, 158);
            this.tabInteractions.TabIndex = 3;
            this.tabInteractions.Text = "Interacciones";
            // 
            // mnuInteractions
            // 
            this.mnuInteractions.Location = new System.Drawing.Point(3, 3);
            this.mnuInteractions.Name = "mnuInteractions";
            this.mnuInteractions.Size = new System.Drawing.Size(335, 24);
            this.mnuInteractions.TabIndex = 0;
            this.mnuInteractions.Text = "menuStrip1";
            // 
            // trvAsm
            // 
            this.trvAsm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.trvAsm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvAsm.Location = new System.Drawing.Point(0, 0);
            this.trvAsm.Name = "trvAsm";
            this.trvAsm.Size = new System.Drawing.Size(204, 308);
            this.trvAsm.TabIndex = 0;
            this.trvAsm.Click += new System.EventHandler(this.TrvAsm_Click);
            this.trvAsm.Layout += new System.Windows.Forms.LayoutEventHandler(this.TrvAsm_Layout);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvAsm);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.txtCopyright);
            this.splitContainer1.Panel2.Controls.Add(this.txtDesc);
            this.splitContainer1.Panel2.Controls.Add(this.txtVersion);
            this.splitContainer1.Panel2.Controls.Add(this.txtName);
            this.splitContainer1.Panel2.Controls.Add(this.chkUnstable);
            this.splitContainer1.Panel2.Controls.Add(this.chkIsBeta);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(560, 308);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 0;
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // PluginBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnClose);
            this.MainMenuStrip = this.mnuInteractions;
            this.Name = "PluginBrowser";
            this.Text = "PluginBrowser";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabInteractions.ResumeLayout(false);
            this.tabInteractions.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.Button btnClose;
        System.Windows.Forms.Label label1;
        System.Windows.Forms.Label label2;
        System.Windows.Forms.Label label3;
        System.Windows.Forms.Label label4;
        System.Windows.Forms.CheckBox chkIsBeta;
        System.Windows.Forms.CheckBox chkUnstable;
        System.Windows.Forms.TextBox txtName;
        System.Windows.Forms.TextBox txtVersion;
        System.Windows.Forms.TextBox txtDesc;
        System.Windows.Forms.TextBox txtCopyright;
        System.Windows.Forms.TabControl tabControl1;
        System.Windows.Forms.TabPage tabPage1;
        System.Windows.Forms.TextBox txtLicense;
        System.Windows.Forms.TabPage tabPage2;
        System.Windows.Forms.TreeView trvAsm;
        System.Windows.Forms.SplitContainer splitContainer1;
        System.Windows.Forms.ListBox lstInterfaces;
        System.Windows.Forms.TabPage tabPage3;
        System.Windows.Forms.TabPage tabInteractions;
        System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        System.Windows.Forms.TextBox txtMinVer;
        System.Windows.Forms.Label label5;
        System.Windows.Forms.Label label6;
        System.Windows.Forms.Label lblVeredict;
        System.Windows.Forms.TextBox txtTgtVer;
        System.Windows.Forms.MenuStrip mnuInteractions;
        System.Windows.Forms.ErrorProvider errProvider;
    }
}