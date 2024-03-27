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
            this.TabLabo = new System.Windows.Forms.TabControl();
            this.TbLabBook = new System.Windows.Forms.TabPage();
            this.BtnFilterByProject = new System.Windows.Forms.Button();
            this.BtnFilterCancel = new System.Windows.Forms.Button();
            this.TxtFilterUser = new System.Windows.Forms.TextBox();
            this.TxtFilterTitle = new System.Windows.Forms.TextBox();
            this.TxtFilterNumD = new System.Windows.Forms.TextBox();
            this.DgvLabo = new System.Windows.Forms.DataGridView();
            this.TbBasicData = new System.Windows.Forms.TabPage();
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
            this.LblProject = new System.Windows.Forms.Label();
            this.TbConclusion = new System.Windows.Forms.TabPage();
            this.BtnChangeProject = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BindingNavigatorLabo)).BeginInit();
            this.BindingNavigatorLabo.SuspendLayout();
            this.TabLabo.SuspendLayout();
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
            this.BindingNavigatorLabo.Location = new System.Drawing.Point(0, 709);
            this.BindingNavigatorLabo.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.BindingNavigatorLabo.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.BindingNavigatorLabo.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.BindingNavigatorLabo.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.BindingNavigatorLabo.Name = "BindingNavigatorLabo";
            this.BindingNavigatorLabo.PositionItem = this.bindingNavigatorPositionItem;
            this.BindingNavigatorLabo.Size = new System.Drawing.Size(1289, 27);
            this.BindingNavigatorLabo.TabIndex = 1;
            this.BindingNavigatorLabo.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 24);
            this.bindingNavigatorCountItem.Text = "z {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Suma elementów";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Przenieś pierwszy";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 24);
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
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(49, 27);
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
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveNextItem.Text = "Przenieś następny";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveLastItem.Text = "Przenieś ostatni";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // TabLabo
            // 
            this.TabLabo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabLabo.Controls.Add(this.TbLabBook);
            this.TabLabo.Controls.Add(this.TbBasicData);
            this.TabLabo.Controls.Add(this.TbConclusion);
            this.TabLabo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TabLabo.Location = new System.Drawing.Point(0, 174);
            this.TabLabo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TabLabo.Name = "TabLabo";
            this.TabLabo.SelectedIndex = 0;
            this.TabLabo.Size = new System.Drawing.Size(1289, 530);
            this.TabLabo.TabIndex = 10;
            // 
            // TbLabBook
            // 
            this.TbLabBook.BackColor = System.Drawing.SystemColors.Control;
            this.TbLabBook.Controls.Add(this.BtnFilterByProject);
            this.TbLabBook.Controls.Add(this.BtnFilterCancel);
            this.TbLabBook.Controls.Add(this.TxtFilterUser);
            this.TbLabBook.Controls.Add(this.TxtFilterTitle);
            this.TbLabBook.Controls.Add(this.TxtFilterNumD);
            this.TbLabBook.Controls.Add(this.DgvLabo);
            this.TbLabBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TbLabBook.Location = new System.Drawing.Point(4, 29);
            this.TbLabBook.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TbLabBook.Name = "TbLabBook";
            this.TbLabBook.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TbLabBook.Size = new System.Drawing.Size(1281, 497);
            this.TbLabBook.TabIndex = 0;
            this.TbLabBook.Tag = "1";
            this.TbLabBook.Text = "Strona główna";
            // 
            // BtnFilterByProject
            // 
            this.BtnFilterByProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnFilterByProject.Location = new System.Drawing.Point(827, 10);
            this.BtnFilterByProject.Name = "BtnFilterByProject";
            this.BtnFilterByProject.Size = new System.Drawing.Size(121, 29);
            this.BtnFilterByProject.TabIndex = 5;
            this.BtnFilterByProject.Text = "-- Wszystko --";
            this.BtnFilterByProject.UseVisualStyleBackColor = true;
            this.BtnFilterByProject.Click += new System.EventHandler(this.BtnFilterByProject_Click);
            // 
            // BtnFilterCancel
            // 
            this.BtnFilterCancel.BackgroundImage = global::Laboratorium.Properties.Resources.delete;
            this.BtnFilterCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFilterCancel.ForeColor = System.Drawing.Color.Red;
            this.BtnFilterCancel.Location = new System.Drawing.Point(7, 10);
            this.BtnFilterCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFilterCancel.Name = "BtnFilterCancel";
            this.BtnFilterCancel.Size = new System.Drawing.Size(32, 30);
            this.BtnFilterCancel.TabIndex = 4;
            this.BtnFilterCancel.UseVisualStyleBackColor = true;
            this.BtnFilterCancel.Click += new System.EventHandler(this.BtnFilterCancel_Click);
            // 
            // TxtFilterUser
            // 
            this.TxtFilterUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterUser.Location = new System.Drawing.Point(691, 11);
            this.TxtFilterUser.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFilterUser.Name = "TxtFilterUser";
            this.TxtFilterUser.Size = new System.Drawing.Size(113, 29);
            this.TxtFilterUser.TabIndex = 3;
            this.TxtFilterUser.TextChanged += new System.EventHandler(this.TxtFilterNumD_TextChanged);
            this.TxtFilterUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // TxtFilterTitle
            // 
            this.TxtFilterTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterTitle.Location = new System.Drawing.Point(221, 11);
            this.TxtFilterTitle.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFilterTitle.Name = "TxtFilterTitle";
            this.TxtFilterTitle.Size = new System.Drawing.Size(460, 29);
            this.TxtFilterTitle.TabIndex = 2;
            this.TxtFilterTitle.TextChanged += new System.EventHandler(this.TxtFilterNumD_TextChanged);
            this.TxtFilterTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // TxtFilterNumD
            // 
            this.TxtFilterNumD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtFilterNumD.Location = new System.Drawing.Point(72, 11);
            this.TxtFilterNumD.Margin = new System.Windows.Forms.Padding(4);
            this.TxtFilterNumD.Name = "TxtFilterNumD";
            this.TxtFilterNumD.Size = new System.Drawing.Size(139, 29);
            this.TxtFilterNumD.TabIndex = 1;
            this.TxtFilterNumD.TextChanged += new System.EventHandler(this.TxtFilterNumD_TextChanged);
            this.TxtFilterNumD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // DgvLabo
            // 
            this.DgvLabo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvLabo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvLabo.Location = new System.Drawing.Point(3, 46);
            this.DgvLabo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DgvLabo.Name = "DgvLabo";
            this.DgvLabo.RowHeadersWidth = 51;
            this.DgvLabo.RowTemplate.Height = 24;
            this.DgvLabo.Size = new System.Drawing.Size(1275, 450);
            this.DgvLabo.TabIndex = 0;
            this.DgvLabo.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DgvLabo_ColumnWidthChanged);
            // 
            // TbBasicData
            // 
            this.TbBasicData.BackColor = System.Drawing.SystemColors.Control;
            this.TbBasicData.Location = new System.Drawing.Point(4, 29);
            this.TbBasicData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TbBasicData.Name = "TbBasicData";
            this.TbBasicData.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TbBasicData.Size = new System.Drawing.Size(1281, 497);
            this.TbBasicData.TabIndex = 1;
            this.TbBasicData.Text = "Dane Podstawowe";
            // 
            // TxtTitle
            // 
            this.TxtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TxtTitle.Location = new System.Drawing.Point(225, 95);
            this.TxtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.TxtTitle.Name = "TxtTitle";
            this.TxtTitle.Size = new System.Drawing.Size(758, 30);
            this.TxtTitle.TabIndex = 0;
            this.TxtTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTitle_KeyPress);
            // 
            // LblTitle
            // 
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblTitle.Location = new System.Drawing.Point(143, 98);
            this.LblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(60, 25);
            this.LblTitle.TabIndex = 6;
            this.LblTitle.Text = "Tytuł";
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackgroundImage = global::Laboratorium.Properties.Resources._new;
            this.BtnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdd.Location = new System.Drawing.Point(8, 34);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(4);
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
            this.LabMainMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.LabMainMenuStrip.Size = new System.Drawing.Size(1289, 31);
            this.LabMainMenuStrip.TabIndex = 6;
            this.LabMainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(51, 27);
            this.FileToolStripMenuItem.Text = "Plik";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(73, 27);
            this.EditToolStripMenuItem.Text = "Edycja";
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackgroundImage = global::Laboratorium.Properties.Resources.delete;
            this.BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDelete.Location = new System.Drawing.Point(66, 34);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(50, 50);
            this.BtnDelete.TabIndex = 21;
            this.BtnDelete.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.BackgroundImage = global::Laboratorium.Properties.Resources.Save;
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSave.Location = new System.Drawing.Point(124, 35);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(50, 50);
            this.BtnSave.TabIndex = 22;
            this.BtnSave.UseVisualStyleBackColor = true;
            // 
            // LblDateCreated
            // 
            this.LblDateCreated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDateCreated.AutoSize = true;
            this.LblDateCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblDateCreated.ForeColor = System.Drawing.Color.Blue;
            this.LblDateCreated.Location = new System.Drawing.Point(1046, 99);
            this.LblDateCreated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDateCreated.Name = "LblDateCreated";
            this.LblDateCreated.Size = new System.Drawing.Size(228, 24);
            this.LblDateCreated.TabIndex = 7;
            this.LblDateCreated.Text = "Utworzenie: 00-00-0000";
            // 
            // LblDateModified
            // 
            this.LblDateModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDateModified.AutoSize = true;
            this.LblDateModified.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblDateModified.ForeColor = System.Drawing.Color.Blue;
            this.LblDateModified.Location = new System.Drawing.Point(1041, 135);
            this.LblDateModified.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDateModified.Name = "LblDateModified";
            this.LblDateModified.Size = new System.Drawing.Size(233, 24);
            this.LblDateModified.TabIndex = 8;
            this.LblDateModified.Text = "Modyfikacja: 00-00-0000";
            // 
            // LblNrD
            // 
            this.LblNrD.AutoSize = true;
            this.LblNrD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblNrD.ForeColor = System.Drawing.Color.Blue;
            this.LblNrD.Location = new System.Drawing.Point(3, 97);
            this.LblNrD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblNrD.Name = "LblNrD";
            this.LblNrD.Size = new System.Drawing.Size(110, 29);
            this.LblNrD.TabIndex = 5;
            this.LblNrD.Text = "D-10000";
            // 
            // LblProject
            // 
            this.LblProject.AutoSize = true;
            this.LblProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LblProject.ForeColor = System.Drawing.Color.Blue;
            this.LblProject.Location = new System.Drawing.Point(143, 134);
            this.LblProject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblProject.Name = "LblProject";
            this.LblProject.Size = new System.Drawing.Size(92, 25);
            this.LblProject.TabIndex = 23;
            this.LblProject.Text = "Projekt: ";
            // 
            // TbConclusion
            // 
            this.TbConclusion.BackColor = System.Drawing.SystemColors.Control;
            this.TbConclusion.Location = new System.Drawing.Point(4, 29);
            this.TbConclusion.Name = "TbConclusion";
            this.TbConclusion.Size = new System.Drawing.Size(1281, 497);
            this.TbConclusion.TabIndex = 2;
            this.TbConclusion.Text = "Uwagi";
            // 
            // BtnChangeProject
            // 
            this.BtnChangeProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnChangeProject.ForeColor = System.Drawing.Color.Red;
            this.BtnChangeProject.Location = new System.Drawing.Point(225, 134);
            this.BtnChangeProject.Name = "BtnChangeProject";
            this.BtnChangeProject.Size = new System.Drawing.Size(105, 25);
            this.BtnChangeProject.TabIndex = 24;
            this.BtnChangeProject.Text = "<- Zmień";
            this.BtnChangeProject.UseVisualStyleBackColor = true;
            this.BtnChangeProject.Click += new System.EventHandler(this.BtnChangeProject_Click);
            // 
            // LabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 736);
            this.Controls.Add(this.BtnChangeProject);
            this.Controls.Add(this.LblProject);
            this.Controls.Add(this.LblNrD);
            this.Controls.Add(this.LblDateModified);
            this.Controls.Add(this.LblDateCreated);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.TxtTitle);
            this.Controls.Add(this.TabLabo);
            this.Controls.Add(this.BindingNavigatorLabo);
            this.Controls.Add(this.LabMainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LabForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LabForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabForm_FormClosing);
            this.Load += new System.EventHandler(this.LabForm_Load);
            this.Resize += new System.EventHandler(this.LabForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.BindingNavigatorLabo)).EndInit();
            this.BindingNavigatorLabo.ResumeLayout(false);
            this.BindingNavigatorLabo.PerformLayout();
            this.TabLabo.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl TabLabo;
        private System.Windows.Forms.TabPage TbLabBook;
        private System.Windows.Forms.DataGridView DgvLabo;
        private System.Windows.Forms.TabPage TbBasicData;
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
        private System.Windows.Forms.Label LblProject;
        private System.Windows.Forms.Button BtnFilterByProject;
        private System.Windows.Forms.TabPage TbConclusion;
        private System.Windows.Forms.Button BtnChangeProject;
    }
}