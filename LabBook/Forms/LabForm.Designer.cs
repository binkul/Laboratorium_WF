namespace Laboratorium.LabBook.Forms
{
    partial class LabForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabForm));
            this.BindingNavigatorLabo = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TbLabBook = new System.Windows.Forms.TabPage();
            this.DgvLabo = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TxtTitle = new System.Windows.Forms.TextBox();
            this.LblTitle = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.LabMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LblDateCreated = new System.Windows.Forms.Label();
            this.LblDateModified = new System.Windows.Forms.Label();
            this.LblNrD = new System.Windows.Forms.Label();
            this.TxtFilterNumD = new System.Windows.Forms.TextBox();
            this.TxtFilterTitle = new System.Windows.Forms.TextBox();
            this.TxtFilterUser = new System.Windows.Forms.TextBox();
            this.BtnFilterCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BindingNavigatorLabo)).BeginInit();
            this.BindingNavigatorLabo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TbLabBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLabo)).BeginInit();
            this.LabMainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BindingNavigatorLabo
            // 
            this.BindingNavigatorLabo.AddNewItem = null;
            this.BindingNavigatorLabo.CountItem = this.bindingNavigatorCountItem;
            this.BindingNavigatorLabo.DeleteItem = null;
            this.BindingNavigatorLabo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BindingNavigatorLabo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.BindingNavigatorLabo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.BindingNavigatorLabo.Location = new System.Drawing.Point(0, 571);
            this.BindingNavigatorLabo.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.BindingNavigatorLabo.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.BindingNavigatorLabo.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.BindingNavigatorLabo.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.BindingNavigatorLabo.Name = "BindingNavigatorLabo";
            this.BindingNavigatorLabo.PositionItem = this.bindingNavigatorPositionItem;
            this.BindingNavigatorLabo.Size = new System.Drawing.Size(1190, 27);
            this.BindingNavigatorLabo.TabIndex = 1;
            this.BindingNavigatorLabo.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorCountItem.Text = "z {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Suma elementów";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Przenieś pierwszy";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Przenieś poprzedni";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Pozycja";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(38, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Bieżąca pozycja";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveNextItem.Text = "Przenieś następny";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveLastItem.Text = "Przenieś ostatni";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TbLabBook);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.Location = new System.Drawing.Point(0, 141);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1190, 431);
            this.tabControl1.TabIndex = 10;
            // 
            // TbLabBook
            // 
            this.TbLabBook.BackColor = System.Drawing.SystemColors.Control;
            this.TbLabBook.Controls.Add(this.BtnFilterCancel);
            this.TbLabBook.Controls.Add(this.TxtFilterUser);
            this.TbLabBook.Controls.Add(this.TxtFilterTitle);
            this.TbLabBook.Controls.Add(this.TxtFilterNumD);
            this.TbLabBook.Controls.Add(this.DgvLabo);
            this.TbLabBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TbLabBook.Location = new System.Drawing.Point(4, 26);
            this.TbLabBook.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TbLabBook.Name = "TbLabBook";
            this.TbLabBook.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TbLabBook.Size = new System.Drawing.Size(1182, 401);
            this.TbLabBook.TabIndex = 0;
            this.TbLabBook.Tag = "1";
            this.TbLabBook.Text = "Strona główna";
            // 
            // DgvLabo
            // 
            this.DgvLabo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvLabo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvLabo.Location = new System.Drawing.Point(2, 37);
            this.DgvLabo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DgvLabo.Name = "DgvLabo";
            this.DgvLabo.RowHeadersWidth = 51;
            this.DgvLabo.RowTemplate.Height = 24;
            this.DgvLabo.Size = new System.Drawing.Size(1180, 366);
            this.DgvLabo.TabIndex = 0;
            this.DgvLabo.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DgvLabo_ColumnWidthChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(1182, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // TxtTitle
            // 
            this.TxtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtTitle.Location = new System.Drawing.Point(170, 88);
            this.TxtTitle.Name = "TxtTitle";
            this.TxtTitle.Size = new System.Drawing.Size(793, 26);
            this.TxtTitle.TabIndex = 0;
            this.TxtTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblTitle.Location = new System.Drawing.Point(115, 91);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(48, 20);
            this.LblTitle.TabIndex = 6;
            this.LblTitle.Text = "Tytuł";
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackgroundImage = global::Laboratorium.Properties.Resources._new;
            this.BtnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdd.Location = new System.Drawing.Point(6, 28);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(50, 50);
            this.BtnAdd.TabIndex = 20;
            this.BtnAdd.UseVisualStyleBackColor = true;
            // 
            // LabMainMenuStrip
            // 
            this.LabMainMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabMainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.LabMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem});
            this.LabMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.LabMainMenuStrip.Name = "LabMainMenuStrip";
            this.LabMainMenuStrip.Size = new System.Drawing.Size(1190, 25);
            this.LabMainMenuStrip.TabIndex = 6;
            this.LabMainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(41, 21);
            this.FileToolStripMenuItem.Text = "Plik";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.EditToolStripMenuItem.Text = "Edycja";
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackgroundImage = global::Laboratorium.Properties.Resources.delete;
            this.BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDelete.Location = new System.Drawing.Point(62, 28);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(50, 50);
            this.BtnDelete.TabIndex = 21;
            this.BtnDelete.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.BackgroundImage = global::Laboratorium.Properties.Resources.Save;
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSave.Location = new System.Drawing.Point(119, 28);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(50, 50);
            this.BtnSave.TabIndex = 22;
            this.BtnSave.UseVisualStyleBackColor = true;
            // 
            // LblDateCreated
            // 
            this.LblDateCreated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDateCreated.AutoSize = true;
            this.LblDateCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblDateCreated.ForeColor = System.Drawing.Color.Blue;
            this.LblDateCreated.Location = new System.Drawing.Point(986, 91);
            this.LblDateCreated.Name = "LblDateCreated";
            this.LblDateCreated.Size = new System.Drawing.Size(201, 20);
            this.LblDateCreated.TabIndex = 7;
            this.LblDateCreated.Text = "Utworzenie: 00-00-0000";
            // 
            // LblDateModified
            // 
            this.LblDateModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDateModified.AutoSize = true;
            this.LblDateModified.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblDateModified.ForeColor = System.Drawing.Color.Blue;
            this.LblDateModified.Location = new System.Drawing.Point(982, 119);
            this.LblDateModified.Name = "LblDateModified";
            this.LblDateModified.Size = new System.Drawing.Size(205, 20);
            this.LblDateModified.TabIndex = 8;
            this.LblDateModified.Text = "Modyfikacja: 00-00-0000";
            // 
            // LblNrD
            // 
            this.LblNrD.AutoSize = true;
            this.LblNrD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblNrD.ForeColor = System.Drawing.Color.Red;
            this.LblNrD.Location = new System.Drawing.Point(4, 89);
            this.LblNrD.Name = "LblNrD";
            this.LblNrD.Size = new System.Drawing.Size(86, 24);
            this.LblNrD.TabIndex = 5;
            this.LblNrD.Text = "D-10000";
            // 
            // TxtFilterNumD
            // 
            this.TxtFilterNumD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterNumD.Location = new System.Drawing.Point(54, 9);
            this.TxtFilterNumD.Name = "TxtFilterNumD";
            this.TxtFilterNumD.Size = new System.Drawing.Size(105, 24);
            this.TxtFilterNumD.TabIndex = 1;
            this.TxtFilterNumD.TextChanged += new System.EventHandler(this.TxtFilterNumD_TextChanged);
            this.TxtFilterNumD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // TxtFilterTitle
            // 
            this.TxtFilterTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterTitle.Location = new System.Drawing.Point(166, 9);
            this.TxtFilterTitle.Name = "TxtFilterTitle";
            this.TxtFilterTitle.Size = new System.Drawing.Size(346, 24);
            this.TxtFilterTitle.TabIndex = 2;
            this.TxtFilterTitle.TextChanged += new System.EventHandler(this.TxtFilterNumD_TextChanged);
            this.TxtFilterTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // TxtFilterUser
            // 
            this.TxtFilterUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterUser.Location = new System.Drawing.Point(518, 9);
            this.TxtFilterUser.Name = "TxtFilterUser";
            this.TxtFilterUser.Size = new System.Drawing.Size(86, 24);
            this.TxtFilterUser.TabIndex = 3;
            this.TxtFilterUser.TextChanged += new System.EventHandler(this.TxtFilterNumD_TextChanged);
            this.TxtFilterUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // BtnFilterCancel
            // 
            this.BtnFilterCancel.BackgroundImage = global::Laboratorium.Properties.Resources.delete;
            this.BtnFilterCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFilterCancel.ForeColor = System.Drawing.Color.Red;
            this.BtnFilterCancel.Location = new System.Drawing.Point(5, 8);
            this.BtnFilterCancel.Name = "BtnFilterCancel";
            this.BtnFilterCancel.Size = new System.Drawing.Size(24, 24);
            this.BtnFilterCancel.TabIndex = 4;
            this.BtnFilterCancel.UseVisualStyleBackColor = true;
            this.BtnFilterCancel.Click += new System.EventHandler(this.BtnFilterCancel_Click);
            // 
            // LabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 598);
            this.Controls.Add(this.LblNrD);
            this.Controls.Add(this.LblDateModified);
            this.Controls.Add(this.LblDateCreated);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.TxtTitle);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BindingNavigatorLabo);
            this.Controls.Add(this.LabMainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LabForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LabForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabForm_FormClosing);
            this.Load += new System.EventHandler(this.LabForm_Load);
            this.Resize += new System.EventHandler(this.LabForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.BindingNavigatorLabo)).EndInit();
            this.BindingNavigatorLabo.ResumeLayout(false);
            this.BindingNavigatorLabo.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.TbLabBook.ResumeLayout(false);
            this.TbLabBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLabo)).EndInit();
            this.LabMainMenuStrip.ResumeLayout(false);
            this.LabMainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingNavigator BindingNavigatorLabo;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TbLabBook;
        private System.Windows.Forms.DataGridView DgvLabo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox TxtTitle;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.MenuStrip LabMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label LblDateCreated;
        private System.Windows.Forms.Label LblDateModified;
        private System.Windows.Forms.Label LblNrD;
        private System.Windows.Forms.TextBox TxtFilterNumD;
        private System.Windows.Forms.TextBox TxtFilterTitle;
        private System.Windows.Forms.TextBox TxtFilterUser;
        private System.Windows.Forms.Button BtnFilterCancel;
    }
}