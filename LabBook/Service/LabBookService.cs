using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Tables;
using Laboratorium.Commons;
using Laboratorium.LabBook.Forms;
using Laboratorium.LabBook.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Service
{
    public class LabBookService
    {
        private const int HEADER_WIDTH = 35;
        private const string FORM_DATA = "LabBookForm";


        private readonly LabBookRepository _repository;
        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly LabForm _form;

        private IList<LaboDto> _laboList;
        private BindingSource _laboBinding;
        private LaboDto _currentLabBook;


        private IDictionary<string, double> _formData = CommonFunction.LoadWindowsDataAsDictionary(FORM_DATA);
        private int GetCurrentLabBookId => Convert.ToInt32(_currentLabBook.Id);

        public LabBookService(SqlConnection connection, UserDto user, LabForm form)
        {
            _connection = connection;
            _user = user;
            _form = form;
            _repository = new LabBookRepository(_connection, SqlIndex.LaboIndex, Table.LABO_TABLE);
        }

        #region Open/Close form 

        public void FormClose(FormClosingEventArgs e)
        {



            if (!e.Cancel)
            {
                SaveFormData();
            }
        }

        private void SaveFormData()
        {
            IDictionary<string, double> list = new Dictionary<string, double>();

            foreach (DataGridViewColumn column in _form.GetDgvLabo.Columns)
            {
                if (column.Visible)
                {
                    list.Add(column.Name, column.Width);
                }
            }

            CommonFunction.WriteWindowsData(list, FORM_DATA);
        }

        #endregion


        public void PrepareAllData()
        {
            #region Tables/Views/Bindings

            LoadLaboData();

            #endregion

            #region Prepare DgvLabBook

            PrepareDgvLabo();

            #endregion

            #region Prepare others control

            _form.GetTxtTitle.DataBindings.Clear();


            _form.GetTxtTitle.DataBindings.Add("Text", _laboBinding, "Title");

            #endregion

            LaboBinding_PositionChanged(null, null);
        }

        private void LoadLaboData()
        {
            _laboList = _repository.GetAll();
            _laboBinding = new BindingSource
            {
                DataSource = _laboList
            };
            _form.GetNavigatorLabo.BindingSource = _laboBinding;
            _laboBinding.PositionChanged += LaboBinding_PositionChanged;
        }

        private void PrepareDgvLabo()
        {
            DataGridView view = _form.GetDgvLabo;
            view.DataSource = _laboBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.RowHeadersWidth = HEADER_WIDTH;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = false;
            view.AllowUserToAddRows = false;
            view.AllowUserToDeleteRows = false;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("Project");
            view.Columns.Remove("Goal");
            view.Columns.Remove("Conclusion");
            view.Columns.Remove("Observation");
            view.Columns.Remove("DateCreated");
            view.Columns.Remove("DateUpdated");
            view.Columns["IsDeleted"].Visible = false;
            view.Columns["GetRowState"].Visible = false;

            view.Columns["Id"].HeaderText = "Nr D";
            view.Columns["Id"].ReadOnly = true;
            view.Columns["Id"].DisplayIndex = 0;
            view.Columns["Id"].Width = _formData.ContainsKey("Id") ? (int)_formData["Id"] : 100;
            view.Columns["Id"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["Title"].HeaderText = "Tytuł";
            view.Columns["Title"].DisplayIndex = 1;
            view.Columns["Title"].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns["Density"].HeaderText = "Gęstość";
            view.Columns["Density"].DisplayIndex = 2;
            view.Columns["Density"].Width = _formData.ContainsKey("Density") ? (int)_formData["Density"] : 100; ;
            view.Columns["Density"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Density"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int width;
            if (view.ScrollBars == ScrollBars.Vertical || view.ScrollBars == ScrollBars.Both)
            {
                width = view.Width - HEADER_WIDTH - view.Columns["Id"].Width - view.Columns["Density"].Width - SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                width = view.Width - HEADER_WIDTH - view.Columns["Id"].Width - view.Columns["Density"].Width;
            }

            view.Columns["Title"].Width = _formData.ContainsKey("Title") ? (int)_formData["Title"] : width - 2;
        }

        #region Current/Binkding/Navigation/DataTable

        private void LaboBinding_PositionChanged(object sender, EventArgs e)
        {
            int id = 0;

            #region Get Current

            if (_laboBinding.Count > 0)
            {
                _currentLabBook = (LaboDto)_laboBinding.Current;
                id = GetCurrentLabBookId;
            }
            else
            {
                _currentLabBook = null;
            }

            #endregion

            #region Set Current Controls

            if (_currentLabBook != null)
            {
                DateTime date = Convert.ToDateTime(_currentLabBook.DateCreated);
                string show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateCreated.Text = "Utworzenie: " + show;
                _form.GetLblDateCreated.Left = _form.ClientSize.Width - _form.GetLblDateCreated.Width - 2;
                date = Convert.ToDateTime(_currentLabBook.DateUpdated);
                show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateModified.Text = "Modyfikacja: " + show;
                _form.GetLblDateModified.Left = _form.ClientSize.Width - _form.GetLblDateModified.Width - 2;

                string nr = "D-" + GetCurrentLabBookId.ToString();
                _form.GetLblNrD.Text = nr;
            }
            else
            {
                _form.GetLblNrD.Text = "D-Brak";
                _form.GetLblDateCreated.Text = "Utworzenie: Brak";
                _form.GetLblDateModified.Text = "Modyfikacja: Brak";
            }

            #endregion

        }

        #endregion

    }
}
