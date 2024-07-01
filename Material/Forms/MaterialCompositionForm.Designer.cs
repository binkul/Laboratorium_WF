
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnUp = new System.Windows.Forms.Button();
            this.BtnDown = new System.Windows.Forms.Button();
            this.LblMaterial = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtFilterCas = new System.Windows.Forms.TextBox();
            this.DgvCompound = new System.Windows.Forms.DataGridView();
            this.TxtFilerName = new System.Windows.Forms.TextBox();
            this.DgvComposition = new System.Windows.Forms.DataGridView();
            this.BtnAddOne = new System.Windows.Forms.Button();
            this.BtnRemoveOne = new System.Windows.Forms.Button();
            this.BtnRemoveAll = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76F));
            this.tableLayoutPanel1.Controls.Add(this.BtnSave, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnUp, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnDown, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblMaterial, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DgvComposition, 8, 2);
            this.tableLayoutPanel1.Controls.Add(this.BtnAddOne, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.BtnRemoveOne, 6, 5);
            this.tableLayoutPanel1.Controls.Add(this.BtnRemoveAll, 6, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(866, 581);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnSave.BackgroundImage = global::Laboratorium.Properties.Resources.Save;
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(5, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(50, 50);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnUp
            // 
            this.BtnUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnUp.BackgroundImage = global::Laboratorium.Properties.Resources.arrow_up;
            this.BtnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnUp.Location = new System.Drawing.Point(68, 5);
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Size = new System.Drawing.Size(29, 50);
            this.BtnUp.TabIndex = 5;
            this.BtnUp.UseVisualStyleBackColor = true;
            this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // BtnDown
            // 
            this.BtnDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnDown.BackgroundImage = global::Laboratorium.Properties.Resources.arrow_down;
            this.BtnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDown.Location = new System.Drawing.Point(103, 5);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(29, 50);
            this.BtnDown.TabIndex = 6;
            this.BtnDown.UseVisualStyleBackColor = true;
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // LblMaterial
            // 
            this.LblMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMaterial.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LblMaterial, 5);
            this.LblMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblMaterial.ForeColor = System.Drawing.Color.Blue;
            this.LblMaterial.Location = new System.Drawing.Point(138, 17);
            this.LblMaterial.Name = "LblMaterial";
            this.LblMaterial.Size = new System.Drawing.Size(725, 25);
            this.LblMaterial.TabIndex = 7;
            this.LblMaterial.Text = "Surowiec";
            this.LblMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 5);
            this.panel1.Controls.Add(this.TxtFilterCas);
            this.panel1.Controls.Add(this.DgvCompound);
            this.panel1.Controls.Add(this.TxtFilerName);
            this.panel1.Location = new System.Drawing.Point(3, 68);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 6);
            this.panel1.Size = new System.Drawing.Size(282, 510);
            this.panel1.TabIndex = 8;
            // 
            // TxtFilterCas
            // 
            this.TxtFilterCas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterCas.Location = new System.Drawing.Point(201, 10);
            this.TxtFilterCas.Name = "TxtFilterCas";
            this.TxtFilterCas.Size = new System.Drawing.Size(78, 27);
            this.TxtFilterCas.TabIndex = 15;
            this.TxtFilterCas.TextChanged += new System.EventHandler(this.TxtFilterCas_TextChanged);
            this.TxtFilterCas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFilerName_KeyPress);
            // 
            // DgvCompound
            // 
            this.DgvCompound.AllowUserToAddRows = false;
            this.DgvCompound.AllowUserToDeleteRows = false;
            this.DgvCompound.AllowUserToResizeRows = false;
            this.DgvCompound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvCompound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCompound.Location = new System.Drawing.Point(0, 43);
            this.DgvCompound.Name = "DgvCompound";
            this.DgvCompound.RowHeadersWidth = 51;
            this.DgvCompound.RowTemplate.Height = 24;
            this.DgvCompound.Size = new System.Drawing.Size(282, 467);
            this.DgvCompound.TabIndex = 0;
            this.DgvCompound.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DgvCompound_ColumnWidthChanged);
            // 
            // TxtFilerName
            // 
            this.TxtFilerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilerName.Location = new System.Drawing.Point(6, 10);
            this.TxtFilerName.Name = "TxtFilerName";
            this.TxtFilerName.Size = new System.Drawing.Size(174, 27);
            this.TxtFilerName.TabIndex = 14;
            this.TxtFilerName.TextChanged += new System.EventHandler(this.TxtFilerName_TextChanged);
            this.TxtFilerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFilerName_KeyPress);
            // 
            // DgvComposition
            // 
            this.DgvComposition.AllowUserToAddRows = false;
            this.DgvComposition.AllowUserToDeleteRows = false;
            this.DgvComposition.AllowUserToResizeRows = false;
            this.DgvComposition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvComposition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvComposition.Location = new System.Drawing.Point(381, 68);
            this.DgvComposition.Name = "DgvComposition";
            this.DgvComposition.RowHeadersWidth = 51;
            this.tableLayoutPanel1.SetRowSpan(this.DgvComposition, 6);
            this.DgvComposition.RowTemplate.Height = 24;
            this.DgvComposition.Size = new System.Drawing.Size(482, 510);
            this.DgvComposition.TabIndex = 9;
            this.DgvComposition.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvComposition_CellValueChanged);
            // 
            // BtnAddOne
            // 
            this.BtnAddOne.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnAddOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnAddOne.ForeColor = System.Drawing.Color.Blue;
            this.BtnAddOne.Location = new System.Drawing.Point(298, 263);
            this.BtnAddOne.Name = "BtnAddOne";
            this.BtnAddOne.Size = new System.Drawing.Size(70, 34);
            this.BtnAddOne.TabIndex = 10;
            this.BtnAddOne.Text = ">";
            this.BtnAddOne.UseVisualStyleBackColor = true;
            this.BtnAddOne.Click += new System.EventHandler(this.BtnAddOne_Click);
            // 
            // BtnRemoveOne
            // 
            this.BtnRemoveOne.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnRemoveOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnRemoveOne.ForeColor = System.Drawing.Color.Blue;
            this.BtnRemoveOne.Location = new System.Drawing.Point(298, 308);
            this.BtnRemoveOne.Name = "BtnRemoveOne";
            this.BtnRemoveOne.Size = new System.Drawing.Size(70, 34);
            this.BtnRemoveOne.TabIndex = 11;
            this.BtnRemoveOne.Text = "<";
            this.BtnRemoveOne.UseVisualStyleBackColor = true;
            this.BtnRemoveOne.Click += new System.EventHandler(this.BtnRemoveOne_Click);
            // 
            // BtnRemoveAll
            // 
            this.BtnRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnRemoveAll.ForeColor = System.Drawing.Color.Blue;
            this.BtnRemoveAll.Location = new System.Drawing.Point(298, 348);
            this.BtnRemoveAll.Name = "BtnRemoveAll";
            this.BtnRemoveAll.Size = new System.Drawing.Size(70, 34);
            this.BtnRemoveAll.TabIndex = 13;
            this.BtnRemoveAll.Text = "<<";
            this.BtnRemoveAll.UseVisualStyleBackColor = true;
            this.BtnRemoveAll.Click += new System.EventHandler(this.BtnRemoveAll_Click);
            // 
            // MaterialCompositionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(872, 598);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialCompositionForm";
            this.Text = "Skład";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MaterialCompositionForm_FormClosing);
            this.Load += new System.EventHandler(this.MaterialCompositionForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCompound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvComposition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnUp;
        private System.Windows.Forms.Button BtnDown;
        private System.Windows.Forms.Label LblMaterial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DgvCompound;
        private System.Windows.Forms.DataGridView DgvComposition;
        private System.Windows.Forms.Button BtnAddOne;
        private System.Windows.Forms.Button BtnRemoveOne;
        private System.Windows.Forms.Button BtnRemoveAll;
        private System.Windows.Forms.TextBox TxtFilerName;
        private System.Windows.Forms.TextBox TxtFilterCas;
    }
}