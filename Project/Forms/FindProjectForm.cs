using Laboratorium.ADO.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Project.Forms
{
    public partial class FindProjectForm : Form
    {
        private const int HEADER_WIDTH = 35;
        private IList<ProjectDto> _projects;

        public ProjectDto Result { get; private set; }
        public bool Ok { get; private set; } = false;

        public FindProjectForm(IList<ProjectDto> projects)
        {
            InitializeComponent();
            _projects = projects;
        }

        private void FindProjectForm_Load(object sender, EventArgs e)
        {
            #region Prepare DataGridView

            _projects.OrderBy(i => i.Id);

            DgvProject.DataSource = _projects;
            DgvProject.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvProject.RowsDefaultCellStyle.Font = new Font(DgvProject.DefaultCellStyle.Font.Name, 9, FontStyle.Regular);
            DgvProject.ColumnHeadersDefaultCellStyle.Font = new Font(DgvProject.DefaultCellStyle.Font.Name, 9, FontStyle.Bold);
            DgvProject.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            DgvProject.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DgvProject.RowHeadersWidth = HEADER_WIDTH;
            DgvProject.DefaultCellStyle.ForeColor = Color.Black;
            DgvProject.AutoGenerateColumns = false;

            DgvProject.Columns.Remove("Comments");
            DgvProject.Columns.Remove("IsArchive");
            DgvProject.Columns.Remove("IsLabo");
            DgvProject.Columns.Remove("IsAuction");
            DgvProject.Columns.Remove("LocalDisk");
            DgvProject.Columns.Remove("UserId");
            DgvProject.Columns.Remove("User");
            DgvProject.Columns.Remove("UserShortcut");
            DgvProject.Columns.Remove("GetRowState");
            DgvProject.Columns.Remove("DateCreated");

            DgvProject.Columns["Id"].HeaderText = "Nr";
            DgvProject.Columns["Id"].DisplayIndex = 0;
            DgvProject.Columns["Id"].Width = 50;
            DgvProject.Columns["Id"].SortMode = DataGridViewColumnSortMode.NotSortable;

            DgvProject.Columns["Title"].HeaderText = "Tytuł";
            DgvProject.Columns["Title"].DisplayIndex = 1;
            DgvProject.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DgvProject.Columns["Title"].SortMode = DataGridViewColumnSortMode.NotSortable;

            #endregion
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Ok = true;
            int id = Convert.ToInt32(DgvProject.SelectedRows[0].Cells["Id"].Value);
            Result = _projects.Where(i => i.Id == id).FirstOrDefault();
            Close();
        }

        private void BtnAnuluj_Click(object sender, EventArgs e)
        {
            Ok = false;
            Close();
        }
    }
}
