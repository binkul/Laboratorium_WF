namespace Laboratorium.Login.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.LblLoging = new System.Windows.Forms.Label();
            this.PanelBlack = new System.Windows.Forms.Panel();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LblPassword = new System.Windows.Forms.Label();
            this.CmbLogin = new System.Windows.Forms.ComboBox();
            this.LblLogin = new System.Windows.Forms.Label();
            this.PanelBlack.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblLoging
            // 
            this.LblLoging.AutoSize = true;
            this.LblLoging.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblLoging.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.LblLoging.ForeColor = System.Drawing.SystemColors.Window;
            this.LblLoging.Location = new System.Drawing.Point(130, 14);
            this.LblLoging.Name = "LblLoging";
            this.LblLoging.Size = new System.Drawing.Size(188, 46);
            this.LblLoging.TabIndex = 0;
            this.LblLoging.Text = "Logowanie";
            // 
            // PanelBlack
            // 
            this.PanelBlack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(197)))), ((int)(((byte)(130)))));
            this.PanelBlack.Controls.Add(this.BtnRegister);
            this.PanelBlack.Controls.Add(this.BtnSubmit);
            this.PanelBlack.Controls.Add(this.TxtPassword);
            this.PanelBlack.Controls.Add(this.LblPassword);
            this.PanelBlack.Controls.Add(this.CmbLogin);
            this.PanelBlack.Controls.Add(this.LblLogin);
            this.PanelBlack.Controls.Add(this.LblLoging);
            this.PanelBlack.Location = new System.Drawing.Point(21, 24);
            this.PanelBlack.Name = "PanelBlack";
            this.PanelBlack.Size = new System.Drawing.Size(449, 452);
            this.PanelBlack.TabIndex = 1;
            this.PanelBlack.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBlack_Paint);
            // 
            // BtnRegister
            // 
            this.BtnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(106)))), ((int)(((byte)(211)))));
            this.BtnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(106)))), ((int)(((byte)(211)))));
            this.BtnRegister.FlatAppearance.BorderSize = 0;
            this.BtnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnRegister.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnRegister.Location = new System.Drawing.Point(216, 387);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(204, 44);
            this.BtnRegister.TabIndex = 5;
            this.BtnRegister.Text = "Rejestracja";
            this.BtnRegister.UseVisualStyleBackColor = false;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(93)))), ((int)(((byte)(106)))));
            this.BtnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(93)))), ((int)(((byte)(106)))));
            this.BtnSubmit.FlatAppearance.BorderSize = 0;
            this.BtnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnSubmit.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnSubmit.Location = new System.Drawing.Point(122, 275);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(204, 44);
            this.BtnSubmit.TabIndex = 4;
            this.BtnSubmit.Text = "Zaloguj się";
            this.BtnSubmit.UseVisualStyleBackColor = false;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtPassword.Location = new System.Drawing.Point(19, 216);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(411, 34);
            this.TxtPassword.TabIndex = 3;
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblPassword.ForeColor = System.Drawing.SystemColors.Window;
            this.LblPassword.Location = new System.Drawing.Point(15, 181);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(77, 32);
            this.LblPassword.TabIndex = 2;
            this.LblPassword.Text = "Hasło";
            // 
            // CmbLogin
            // 
            this.CmbLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CmbLogin.FormattingEnabled = true;
            this.CmbLogin.Location = new System.Drawing.Point(19, 126);
            this.CmbLogin.Name = "CmbLogin";
            this.CmbLogin.Size = new System.Drawing.Size(411, 36);
            this.CmbLogin.TabIndex = 2;
            // 
            // LblLogin
            // 
            this.LblLogin.AutoSize = true;
            this.LblLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblLogin.ForeColor = System.Drawing.SystemColors.Window;
            this.LblLogin.Location = new System.Drawing.Point(15, 91);
            this.LblLogin.Name = "LblLogin";
            this.LblLogin.Size = new System.Drawing.Size(75, 32);
            this.LblLogin.TabIndex = 1;
            this.LblLogin.Text = "Login";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.BtnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(197)))), ((int)(((byte)(130)))));
            this.ClientSize = new System.Drawing.Size(491, 498);
            this.Controls.Add(this.PanelBlack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logowanie";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.PanelBlack.ResumeLayout(false);
            this.PanelBlack.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblLoging;
        private System.Windows.Forms.Panel PanelBlack;
        private System.Windows.Forms.Label LblLogin;
        private System.Windows.Forms.ComboBox CmbLogin;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label LblPassword;
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.Button BtnRegister;

    }
}