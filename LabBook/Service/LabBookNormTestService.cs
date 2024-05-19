using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.LabBook.Forms;
using Laboratorium.LabBook.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.LabBook.Service
{
    public class LabBookNormTestService : IDgvService, IService
    {
        private readonly SqlConnection _connection;
        private readonly UserDto _user;
        private readonly LabForm _form;
        private readonly IService _service;
        private IList<LaboDataNormTestDto> _laboNormTestList;
        private BindingSource _laboNormTestBinding;
        private readonly IBasicCRUD<LaboDataNormTestDto> _repositoryNormTest;

        public LabBookNormTestService(SqlConnection connection, UserDto user, LabForm form, IService service)
        {
            _connection = connection;
            _user = user;
            _form = form;
            _service = service;
            _repositoryNormTest = new LabBookNormTestRepository(_connection, this);
        }

        public bool Modify(RowState state)
        {
            return _laboNormTestList
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();
        }

        public void PrepareData()
        {
            _laboNormTestList = _repositoryNormTest.GetAll();
            _laboNormTestBinding = new BindingSource();
            _laboNormTestBinding.DataSource = _laboNormTestList;

            PrepareDgvNormTest();
            PrepareNormMenu();
        }

        private void PrepareDgvNormTest()
        {
            DataGridView view = _form.GetDgvNormTest;
            LabBookService service = (LabBookService)_service;
            IDictionary<string, double> formData = service.GetFormData;

            view.DataSource = _laboNormTestBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.RowHeadersWidth = CommonData.HEADER_WIDTH_ADMIN;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.ReadOnly = false;
            view.AllowUserToAddRows = false;
            view.AllowUserToDeleteRows = false;
            view.AutoGenerateColumns = false;

            view.Columns.Remove("GetRowState");
            view.Columns.Remove("CrudState");

            view.Columns["Id"].Visible = false;
            view.Columns["LaboId"].Visible = false;
            view.Columns["Position"].Visible = false;

            view.Columns["DateCreated"].HeaderText = "Start";
            view.Columns["DateCreated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["DateCreated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateCreated"].Width = formData.ContainsKey("DateCreated_test") ? (int)formData["DateCreated_test"] : view.Columns["DateCreated"].Width;
            view.Columns["DateCreated"].DisplayIndex = 0;

            view.Columns["DateUpdated"].HeaderText = "Koniec";
            view.Columns["DateUpdated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["DateUpdated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateUpdated"].Width = formData.ContainsKey("DateUpdated_test") ? (int)formData["DateUpdated_test"] : view.Columns["DateUpdated"].Width;
            view.Columns["DateUpdated"].DisplayIndex = 1;

            view.Columns["Days"].HeaderText = "Doba";
            view.Columns["Days"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["Days"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Days"].ReadOnly = true;
            view.Columns["Days"].Width = formData.ContainsKey("Days_test") ? (int)formData["Days_test"] : view.Columns["Days"].Width;
            view.Columns["Days"].DisplayIndex = 2;

            view.Columns["Norm"].HeaderText = "Norma";
            view.Columns["Norm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Norm"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Norm"].Width = formData.ContainsKey("Norm_test") ? (int)formData["Norm_test"] : view.Columns["Norm"].Width;
            view.Columns["Norm"].DisplayIndex = 3;

            view.Columns["Description"].HeaderText = "Opis";
            view.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Description"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Description"].Width = formData.ContainsKey("Description_test") ? (int)formData["Description_test"] : view.Columns["Description"].Width;
            view.Columns["Description"].DisplayIndex = 4;

            view.Columns["Requirement"].HeaderText = "Wymogi";
            view.Columns["Requirement"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Requirement"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Requirement"].Width = formData.ContainsKey("Requirement_test") ? (int)formData["Requirement_test"] : view.Columns["Requirement"].Width;
            view.Columns["Requirement"].DisplayIndex = 5;

            view.Columns["Result"].HeaderText = "Wynik";
            view.Columns["Result"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Result"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Result"].Width = formData.ContainsKey("Result_test") ? (int)formData["Result_test"] : view.Columns["Result"].Width;
            view.Columns["Result"].DisplayIndex = 6;

            view.Columns["Substrate"].HeaderText = "Podłoże";
            view.Columns["Substrate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Substrate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Substrate"].Width = formData.ContainsKey("Substrate_test") ? (int)formData["Substrate_test"] : view.Columns["Substrate"].Width;
            view.Columns["Substrate"].DisplayIndex = 7;

            view.Columns["Comments"].HeaderText = "Uwagi";
            view.Columns["Comments"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Comments"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Comments"].Width = formData.ContainsKey("Comments_test") ? (int)formData["Comments_test"] : view.Columns["Comments"].Width;
            view.Columns["Comments"].DisplayIndex = 8;
        }

        private void PrepareNormMenu()
        {

        }

        public void SynchronizeData(int LaboId)
        {
            //throw new NotImplementedException();
        }

        public void AddNew(int number)
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
