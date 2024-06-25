namespace Laboratorium.Register.Forms
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.PanelBlack = new System.Windows.Forms.Panel();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.TxtRepeatPassword = new System.Windows.Forms.TextBox();
            this.LblRepeatPassword = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LblPassword = new System.Windows.Forms.Label();
            this.TxtLogin = new System.Windows.Forms.TextBox();
            this.LblLogin = new System.Windows.Forms.Label();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.LblEmail = new System.Windows.Forms.Label();
            this.TxtSurname = new System.Windows.Forms.TextBox();
            this.LblNazwisko = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.LblImie = new System.Windows.Forms.Label();
            this.LblLoging = new System.Windows.Forms.Label();
            this.PanelBlack.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelBlack
            // 
            this.PanelBlack.Controls.Add(this.BtnRegister);
            this.PanelBlack.Controls.Add(this.TxtRepeatPassword);
            this.PanelBlack.Controls.Add(this.LblRepeatPassword);
            this.PanelBlack.Controls.Add(this.TxtPassword);
            this.PanelBlack.Controls.Add(this.LblPassword);
            this.PanelBlack.Controls.Add(this.TxtLogin);
            this.PanelBlack.Controls.Add(this.LblLogin);
            this.PanelBlack.Controls.Add(this.TxtEmail);
            this.PanelBlack.Controls.Add(this.LblEmail);
            this.PanelBlack.Controls.Add(this.TxtSurname);
            this.PanelBlack.Controls.Add(this.LblNazwisko);
            this.PanelBlack.Controls.Add(this.TxtName);
            this.PanelBlack.Controls.Add(this.LblImie);
            this.PanelBlack.Controls.Add(this.LblLoging);
            this.PanelBlack.Location = new System.Drawing.Point(21, 24);
            this.PanelBlack.Name = "PanelBlack";
            this.PanelBlack.Size = new System.Drawing.Size(449, 722);
            this.PanelBlack.TabIndex = 0;
            this.PanelBlack.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBlack_Paint);
            // 
            // BtnRegister
            // 
            this.BtnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(93)))), ((int)(((byte)(106)))));
            this.BtnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(93)))), ((int)(((byte)(106)))));
            this.BtnRegister.FlatAppearance.BorderSize = 0;
            this.BtnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnRegister.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnRegister.Location = new System.Drawing.Point(122, 641);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(204, 44);
            this.BtnRegister.TabIndex = 14;
            this.BtnRegister.Text = "Zarejestruj";
            this.BtnRegister.UseVisualStyleBackColor = false;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // TxtRepeatPassword
            // 
            this.TxtRepeatPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtRepeatPassword.Location = new System.Drawing.Point(19, 576);
            this.TxtRepeatPassword.Name = "TxtRepeatPassword";
            this.TxtRepeatPassword.PasswordChar = '*';
            this.TxtRepeatPassword.Size = new System.Drawing.Size(411, 34);
            this.TxtRepeatPassword.TabIndex = 13;
            // 
            // LblRepeatPassword
            // 
            this.LblRepeatPassword.AutoSize = true;
            this.LblRepeatPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblRepeatPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblRepeatPassword.ForeColor = System.Drawing.SystemColors.Window;
            this.LblRepeatPassword.Location = new System.Drawing.Point(13, 541);
            this.LblRepeatPassword.Name = "LblRepeatPassword";
            this.LblRepeatPassword.Size = new System.Drawing.Size(167, 32);
            this.LblRepeatPassword.TabIndex = 12;
            this.LblRepeatPassword.Text = "Powtórz hasło";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtPassword.Location = new System.Drawing.Point(19, 486);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.Size = new System.Drawing.Size(411, 34);
            this.TxtPassword.TabIndex = 11;
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblPassword.ForeColor = System.Drawing.SystemColors.Window;
            this.LblPassword.Location = new System.Drawing.Point(15, 451);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(76, 32);
            this.LblPassword.TabIndex = 10;
            this.LblPassword.Text = "Hasło";
            // 
            // TxtLogin
            // 
            this.TxtLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtLogin.Location = new System.Drawing.Point(19, 396);
            this.TxtLogin.Name = "TxtLogin";
            this.TxtLogin.Size = new System.Drawing.Size(411, 34);
            this.TxtLogin.TabIndex = 9;
            // 
            // LblLogin
            // 
            this.LblLogin.AutoSize = true;
            this.LblLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblLogin.ForeColor = System.Drawing.SystemColors.Window;
            this.LblLogin.Location = new System.Drawing.Point(15, 361);
            this.LblLogin.Name = "LblLogin";
            this.LblLogin.Size = new System.Drawing.Size(74, 32);
            this.LblLogin.TabIndex = 8;
            this.LblLogin.Text = "Login";
            // 
            // TxtEmail
            // 
            this.TxtEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtEmail.Location = new System.Drawing.Point(19, 306);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(411, 34);
            this.TxtEmail.TabIndex = 7;
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblEmail.ForeColor = System.Drawing.SystemColors.Window;
            this.LblEmail.Location = new System.Drawing.Point(15, 271);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(83, 32);
            this.LblEmail.TabIndex = 7;
            this.LblEmail.Text = "e-mail";
            // 
            // TxtSurname
            // 
            this.TxtSurname.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtSurname.Location = new System.Drawing.Point(19, 216);
            this.TxtSurname.Name = "TxtSurname";
            this.TxtSurname.Size = new System.Drawing.Size(411, 34);
            this.TxtSurname.TabIndex = 6;
            // 
            // LblNazwisko
            // 
            this.LblNazwisko.AutoSize = true;
            this.LblNazwisko.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblNazwisko.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblNazwisko.ForeColor = System.Drawing.SystemColors.Window;
            this.LblNazwisko.Location = new System.Drawing.Point(15, 181);
            this.LblNazwisko.Name = "LblNazwisko";
            this.LblNazwisko.Size = new System.Drawing.Size(117, 32);
            this.LblNazwisko.TabIndex = 5;
            this.LblNazwisko.Text = "Nazwisko";
            // 
            // TxtName
            // 
            this.TxtName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtName.Location = new System.Drawing.Point(19, 126);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(411, 34);
            this.TxtName.TabIndex = 4;
            // 
            // LblImie
            // 
            this.LblImie.AutoSize = true;
            this.LblImie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblImie.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.LblImie.ForeColor = System.Drawing.SystemColors.Window;
            this.LblImie.Location = new System.Drawing.Point(15, 91);
            this.LblImie.Name = "LblImie";
            this.LblImie.Size = new System.Drawing.Size(61, 32);
            this.LblImie.TabIndex = 2;
            this.LblImie.Text = "Imie";
            // 
            // LblLoging
            // 
            this.LblLoging.AutoSize = true;
            this.LblLoging.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(55)))));
            this.LblLoging.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.LblLoging.ForeColor = System.Drawing.SystemColors.Window;
            this.LblLoging.Location = new System.Drawing.Point(131, 14);
            this.LblLoging.Name = "LblLoging";
            this.LblLoging.Size = new System.Drawing.Size(186, 46);
            this.LblLoging.TabIndex = 1;
            this.LblLoging.Text = "Rejestracja";
            // 
            // RegisterForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(197)))), ((int)(((byte)(130)))));
            this.ClientSize = new System.Drawing.Size(491, 770);
            this.Controls.Add(this.PanelBlack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rejestracja";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterForm_FormClosing);
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.PanelBlack.ResumeLayout(false);
            this.PanelBlack.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelBlack;
        private System.Windows.Forms.Label LblLoging;
        private System.Windows.Forms.Label LblImie;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Label LblNazwisko;
        private System.Windows.Forms.TextBox TxtSurname;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.TextBox TxtRepeatPassword;
        private System.Windows.Forms.Label LblRepeatPassword;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label LblPassword;
        private System.Windows.Forms.TextBox TxtLogin;
        private System.Windows.Forms.Label LblLogin;
        private System.Windows.Forms.Button BtnRegister;

    }
}