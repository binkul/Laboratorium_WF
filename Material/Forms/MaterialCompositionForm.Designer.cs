
namespace Laboratorium.Material.Forms
{
    partial class MaterialCompositionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialCompositionForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnUp = new System.Windows.Forms.Button();
            this.BtnDown = new System.Windows.Forms.Button();
            this.LblMaterial = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DgvCompound = new System.Windows.Forms.DataGridView();
            this.DgvComposition = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCompound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvComposition)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.BtnDelete, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnSave, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnUp, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnDown, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblMaterial, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DgvComposition, 7, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(774, 546);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // BtnDelete
            // 
            this.BtnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnDelete.BackgroundImage = global::Laboratorium.Properties.Resources.delete;
            this.BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDelete.Location = new System.Drawing.Point(5, 5);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(50, 50);
            this.BtnDelete.TabIndex = 3;
            this.BtnDelete.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnSave.BackgroundImage = global::Laboratorium.Properties.Resources.Save;
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(65, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(50, 50);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.UseVisualStyleBackColor = true;
            // 
            // BtnUp
            // 
            this.BtnUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnUp.BackgroundImage = global::Laboratorium.Properties.Resources.arrow_up;
            this.BtnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnUp.Location = new System.Drawing.Point(128, 5);
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Size = new System.Drawing.Size(29, 50);
            this.BtnUp.TabIndex = 5;
            this.BtnUp.UseVisualStyleBackColor = true;
            // 
            // BtnDown
            // 
            this.BtnDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnDown.BackgroundImage = global::Laboratorium.Properties.Resources.arrow_down;
            this.BtnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDown.Location = new System.Drawing.Point(163, 5);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(29, 50);
            this.BtnDown.TabIndex = 6;
            this.BtnDown.UseVisualStyleBackColor = true;
            // 
            // LblMaterial
            // 
            this.LblMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMaterial.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LblMaterial, 3);
            this.LblMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblMaterial.ForeColor = System.Drawing.Color.Blue;
            this.LblMaterial.Location = new System.Drawing.Point(198, 17);
            this.LblMaterial.Name = "LblMaterial";
            this.LblMaterial.Size = new System.Drawing.Size(573, 25);
            this.LblMaterial.TabIndex = 7;
            this.LblMaterial.Text = "Surowiec";
            this.LblMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 6);
            this.panel1.Controls.Add(this.DgvCompound);
            this.panel1.Location = new System.Drawing.Point(3, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 475);
            this.panel1.TabIndex = 8;
            // 
            // DgvCompound
            // 
            this.DgvCompound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvCompound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCompound.Location = new System.Drawing.Point(0, 26);
            this.DgvCompound.Name = "DgvCompound";
            this.DgvCompound.RowHeadersWidth = 51;
            this.DgvCompound.RowTemplate.Height = 24;
            this.DgvCompound.Size = new System.Drawing.Size(289, 445);
            this.DgvCompound.TabIndex = 0;
            // 
            // DgvComposition
            // 
            this.DgvComposition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvComposition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvComposition.Location = new System.Drawing.Point(328, 68);
            this.DgvComposition.Name = "DgvComposition";
            this.DgvComposition.RowHeadersWidth = 51;
            this.DgvComposition.RowTemplate.Height = 24;
            this.DgvComposition.Size = new System.Drawing.Size(443, 475);
            this.DgvComposition.TabIndex = 9;
            // 
            // MaterialCompositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 563);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialCompositionForm";
            this.Text = "Skład";
            this.Load += new System.EventHandler(this.MaterialCompositionForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvCompound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvComposition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnUp;
        private System.Windows.Forms.Button BtnDown;
        private System.Windows.Forms.Label LblMaterial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DgvCompound;
        private System.Windows.Forms.DataGridView DgvComposition;
    }
}