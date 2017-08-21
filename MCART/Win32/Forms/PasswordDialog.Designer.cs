namespace MCART.Forms
{
    partial class PasswordDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHint = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.prScore = new MCART.Controls.ProgressRing();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblMorInfo = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(12, 25);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(210, 20);
            this.txtUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña";
            // 
            // txtPw
            // 
            this.txtPw.Location = new System.Drawing.Point(12, 64);
            this.txtPw.Name = "txtPw";
            this.txtPw.PasswordChar = '●';
            this.txtPw.Size = new System.Drawing.Size(181, 20);
            this.txtPw.TabIndex = 3;
            this.txtPw.TextChanged += new System.EventHandler(this.TxtPw_TextChanged);
            this.txtPw.Leave += new System.EventHandler(this.CheckPw);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Confirmar contraseña";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(12, 103);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.Size = new System.Drawing.Size(210, 20);
            this.txtConfirm.TabIndex = 5;
            this.txtConfirm.TextChanged += new System.EventHandler(this.WarnCheck);
            this.txtConfirm.Leave += new System.EventHandler(this.CheckPw);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Indicio de contraseña";
            // 
            // txtHint
            // 
            this.txtHint.Location = new System.Drawing.Point(12, 142);
            this.txtHint.Name = "txtHint";
            this.txtHint.Size = new System.Drawing.Size(210, 20);
            this.txtHint.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Seguridad";
            // 
            // prScore
            // 
            this.prScore.Angle = 0F;
            this.prScore.Fill = System.Drawing.SystemColors.Highlight;
            this.prScore.IsIndeterminate = false;
            this.prScore.Location = new System.Drawing.Point(12, 181);
            this.prScore.Maximum = 100F;
            this.prScore.Minimum = 0F;
            this.prScore.Name = "prScore";
            this.prScore.Radius = 32F;
            this.prScore.RingColor = System.Drawing.SystemColors.ControlDark;
            this.prScore.Size = new System.Drawing.Size(64, 64);
            this.prScore.Sweep = MCART.Controls.ProgressRing.SweepDirection.Clockwise;
            this.prScore.TabIndex = 9;
            this.prScore.Text = "progressRing1";
            this.prScore.TextFormat = "{0:0}%";
            this.prScore.Thickness = 6F;
            this.prScore.Value = 0F;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(87, 263);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(76, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Aceptar";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // lblMorInfo
            // 
            this.lblMorInfo.Location = new System.Drawing.Point(84, 181);
            this.lblMorInfo.Name = "lblMorInfo";
            this.lblMorInfo.Size = new System.Drawing.Size(138, 64);
            this.lblMorInfo.TabIndex = 11;
            this.lblMorInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(199, 62);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(23, 23);
            this.btnGo.TabIndex = 12;
            this.btnGo.Text = "➜";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // PasswordDialog
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 298);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblMorInfo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.prScore);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PasswordDialog";
            this.TextChanged += new System.EventHandler(this.WarnCheck);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Label label5;
        private Controls.ProgressRing prScore;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblMorInfo;
        private System.Windows.Forms.Button btnGo;
    }
}