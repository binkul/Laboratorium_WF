
namespace Laboratorium.Composition.Forms
{
    partial class InsertRecipeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertRecipeForm));
            this.DgvLabo = new System.Windows.Forms.DataGridView();
            this.TxtFindNumber = new System.Windows.Forms.TextBox();
            this.TxtFindName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLabo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgvLabo
            // 
            this.DgvLabo.AllowUserToAddRows = false;
            this.DgvLabo.AllowUserToDeleteRows = false;
            this.DgvLabo.AllowUserToResizeRows = false;
            this.DgvLabo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvLabo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvLabo.Location = new System.Drawing.Point(5, 97);
            this.DgvLabo.Name = "DgvLabo";
            this.DgvLabo.RowHeadersWidth = 51;
            this.DgvLabo.RowTemplate.Height = 24;
            this.DgvLabo.Size = new System.Drawing.Size(930, 399);
            this.DgvLabo.TabIndex = 3;
            this.DgvLabo.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DgvLabo_ColumnWidthChanged);
            // 
            // TxtFindNumber
            // 
            this.TxtFindNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFindNumber.Location = new System.Drawing.Point(5, 64);
            this.TxtFindNumber.Name = "TxtFindNumber";
            this.TxtFindNumber.Size = new System.Drawing.Size(163, 27);
            this.TxtFindNumber.TabIndex = 1;
            this.TxtFindNumber.TextChanged += new System.EventHandler(this.TxtFindNumber_TextChanged);
            this.TxtFindNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFindNumber_KeyPress);
            // 
            // TxtFindName
            // 
            this.TxtFindName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFindName.Location = new System.Drawing.Point(183, 64);
            this.TxtFindName.Name = "TxtFindName";
            this.TxtFindName.Size = new System.Drawing.Size(623, 27);
            this.TxtFindName.TabIndex = 2;
            this.TxtFindName.TextChanged += new System.EventHandler(this.TxtFindName_TextChanged);
            this.TxtFindName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFindNumber_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(99, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(730, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "UWAGA! Bieżąca receptura zostanie zastąpiona recepturą wybraną z listy.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(929, 30);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(838, 502);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(97, 37);
            this.BtnOk.TabIndex = 5;
            this.BtnOk.Text = "Wybierz";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // InsertRecipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 546);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.TxtFindName);
            this.Controls.Add(this.TxtFindNumber);
            this.Controls.Add(this.DgvLabo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InsertRecipeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wstaw recepturę";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InsertRecipeForm_FormClosing);
            this.Load += new System.EventHandler(this.InsertRecipeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvLabo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvLabo;
        private System.Windows.Forms.TextBox TxtFindNumber;
        private System.Windows.Forms.TextBox TxtFindName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BtnOk;
    }
}