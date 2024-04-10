
namespace Laboratorium.LabBook.Forms
{
    partial class AddSeriesLaboForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSeriesLaboForm));
            this.RdCopyI = new System.Windows.Forms.RadioButton();
            this.RdCopyIII = new System.Windows.Forms.RadioButton();
            this.RdCopyIV = new System.Windows.Forms.RadioButton();
            this.RdCopyV = new System.Windows.Forms.RadioButton();
            this.RdCopyVI = new System.Windows.Forms.RadioButton();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.LblCurrentNrD = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.RdCopyII = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // RdCopyI
            // 
            this.RdCopyI.AutoSize = true;
            this.RdCopyI.Checked = true;
            this.RdCopyI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RdCopyI.Location = new System.Drawing.Point(12, 113);
            this.RdCopyI.Name = "RdCopyI";
            this.RdCopyI.Size = new System.Drawing.Size(204, 24);
            this.RdCopyI.TabIndex = 0;
            this.RdCopyI.TabStop = true;
            this.RdCopyI.Text = "Skopiuj 1x poprzedni";
            this.RdCopyI.UseVisualStyleBackColor = true;
            this.RdCopyI.CheckedChanged += new System.EventHandler(this.RdCopyI_CheckedChanged);
            // 
            // RdCopyIII
            // 
            this.RdCopyIII.AutoSize = true;
            this.RdCopyIII.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RdCopyIII.Location = new System.Drawing.Point(12, 223);
            this.RdCopyIII.Name = "RdCopyIII";
            this.RdCopyIII.Size = new System.Drawing.Size(416, 24);
            this.RdCopyIII.TabIndex = 1;
            this.RdCopyIII.Text = "Powiel bierzący i zachowaj tytuł we wszytkich";
            this.RdCopyIII.UseVisualStyleBackColor = true;
            this.RdCopyIII.CheckedChanged += new System.EventHandler(this.RdCopyIII_CheckedChanged);
            // 
            // RdCopyIV
            // 
            this.RdCopyIV.AutoSize = true;
            this.RdCopyIV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RdCopyIV.Location = new System.Drawing.Point(12, 278);
            this.RdCopyIV.Name = "RdCopyIV";
            this.RdCopyIV.Size = new System.Drawing.Size(506, 24);
            this.RdCopyIV.TabIndex = 2;
            this.RdCopyIV.Text = "Powiel bierzący i zachowaj tytuł I-szego, reszta \'PUSTY\'";
            this.RdCopyIV.UseVisualStyleBackColor = true;
            this.RdCopyIV.CheckedChanged += new System.EventHandler(this.RdCopyIV_CheckedChanged);
            // 
            // RdCopyV
            // 
            this.RdCopyV.AutoSize = true;
            this.RdCopyV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RdCopyV.Location = new System.Drawing.Point(12, 333);
            this.RdCopyV.Name = "RdCopyV";
            this.RdCopyV.Size = new System.Drawing.Size(320, 24);
            this.RdCopyV.TabIndex = 3;
            this.RdCopyV.Text = "Powiel bierzący z tytułem \'PUSTY\'";
            this.RdCopyV.UseVisualStyleBackColor = true;
            this.RdCopyV.CheckedChanged += new System.EventHandler(this.RdCopyV_CheckedChanged);
            // 
            // RdCopyVI
            // 
            this.RdCopyVI.AutoSize = true;
            this.RdCopyVI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RdCopyVI.Location = new System.Drawing.Point(12, 388);
            this.RdCopyVI.Name = "RdCopyVI";
            this.RdCopyVI.Size = new System.Drawing.Size(319, 24);
            this.RdCopyVI.TabIndex = 4;
            this.RdCopyVI.TabStop = true;
            this.RdCopyVI.Text = "Wstaw same \'PUSTY\' bez niczego";
            this.RdCopyVI.UseVisualStyleBackColor = true;
            this.RdCopyVI.CheckedChanged += new System.EventHandler(this.RdCopyVI_CheckedChanged);
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCurrent.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrent.Location = new System.Drawing.Point(12, 60);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(95, 25);
            this.lblCurrent.TabIndex = 5;
            this.lblCurrent.Text = "Bierzący";
            // 
            // LblCurrentNrD
            // 
            this.LblCurrentNrD.AutoSize = true;
            this.LblCurrentNrD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblCurrentNrD.ForeColor = System.Drawing.Color.Blue;
            this.LblCurrentNrD.Location = new System.Drawing.Point(12, 26);
            this.LblCurrentNrD.Name = "LblCurrentNrD";
            this.LblCurrentNrD.Size = new System.Drawing.Size(95, 25);
            this.LblCurrentNrD.TabIndex = 6;
            this.LblCurrentNrD.Text = "D-10000";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnCancel.Location = new System.Drawing.Point(779, 395);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(86, 34);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnOk.Location = new System.Drawing.Point(687, 395);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(86, 34);
            this.BtnOk.TabIndex = 8;
            this.BtnOk.Text = "Ok";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // NumericUpDown
            // 
            this.NumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.NumericUpDown.Location = new System.Drawing.Point(748, 219);
            this.NumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown.Name = "NumericUpDown";
            this.NumericUpDown.Size = new System.Drawing.Size(117, 30);
            this.NumericUpDown.TabIndex = 10;
            this.NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(675, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ilość";
            // 
            // RdCopyII
            // 
            this.RdCopyII.AutoSize = true;
            this.RdCopyII.Checked = true;
            this.RdCopyII.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RdCopyII.Location = new System.Drawing.Point(12, 168);
            this.RdCopyII.Name = "RdCopyII";
            this.RdCopyII.Size = new System.Drawing.Size(193, 24);
            this.RdCopyII.TabIndex = 12;
            this.RdCopyII.TabStop = true;
            this.RdCopyII.Text = "Skopiuj 1x bierzący";
            this.RdCopyII.UseVisualStyleBackColor = true;
            this.RdCopyII.CheckedChanged += new System.EventHandler(this.RdCopyII_CheckedChanged);
            // 
            // AddSeriesLaboForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 441);
            this.Controls.Add(this.RdCopyII);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NumericUpDown);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblCurrentNrD);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.RdCopyVI);
            this.Controls.Add(this.RdCopyV);
            this.Controls.Add(this.RdCopyIV);
            this.Controls.Add(this.RdCopyIII);
            this.Controls.Add(this.RdCopyI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSeriesLaboForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddSeriesLaboForm_FormClosing);
            this.Load += new System.EventHandler(this.AddSeriesLaboForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RdCopyI;
        private System.Windows.Forms.RadioButton RdCopyIII;
        private System.Windows.Forms.RadioButton RdCopyIV;
        private System.Windows.Forms.RadioButton RdCopyV;
        private System.Windows.Forms.RadioButton RdCopyVI;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label LblCurrentNrD;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.NumericUpDown NumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RdCopyII;
    }
}