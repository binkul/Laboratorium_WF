
namespace Laboratorium.Project.Forms
{
    partial class FindProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindProjectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.DgvProject = new System.Windows.Forms.DataGridView();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnAnuluj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvProject)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(141, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wyszukaj projekt i zatwierdż klawiszem";
            // 
            // DgvProject
            // 
            this.DgvProject.AllowUserToAddRows = false;
            this.DgvProject.AllowUserToDeleteRows = false;
            this.DgvProject.AllowUserToResizeRows = false;
            this.DgvProject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvProject.Location = new System.Drawing.Point(12, 136);
            this.DgvProject.MultiSelect = false;
            this.DgvProject.Name = "DgvProject";
            this.DgvProject.ReadOnly = true;
            this.DgvProject.RowHeadersWidth = 51;
            this.DgvProject.RowTemplate.Height = 24;
            this.DgvProject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvProject.Size = new System.Drawing.Size(648, 332);
            this.DgvProject.TabIndex = 1;
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(538, 474);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(58, 30);
            this.BtnOk.TabIndex = 2;
            this.BtnOk.Text = "Ok";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnAnuluj
            // 
            this.BtnAnuluj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAnuluj.Location = new System.Drawing.Point(602, 476);
            this.BtnAnuluj.Name = "BtnAnuluj";
            this.BtnAnuluj.Size = new System.Drawing.Size(58, 30);
            this.BtnAnuluj.TabIndex = 3;
            this.BtnAnuluj.Text = "Anuluj";
            this.BtnAnuluj.UseVisualStyleBackColor = true;
            this.BtnAnuluj.Click += new System.EventHandler(this.BtnAnuluj_Click);
            // 
            // FindProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 518);
            this.Controls.Add(this.BtnAnuluj);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.DgvProject);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wyszukiwanie projektu";
            this.Load += new System.EventHandler(this.FindProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvProject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DgvProject;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnAnuluj;
    }
}