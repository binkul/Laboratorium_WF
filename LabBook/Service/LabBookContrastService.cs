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
using System.Windows.Forms;

namespace Laboratorium.LabBook.Service
{
    public class LabBookContrastService : IDgvService
    {
        private readonly SqlConnection _connection;
        private readonly LabForm _form;
        private readonly IService _service;
        private readonly IBasicCRUD<LaboDataContrastDto> _repository;
        private IList<LaboDataContrastDto> _laboContrastList;
        public BindingSource LaboContrastBinding { get; private set; }

        public LabBookContrastService(SqlConnection connection, LabForm form, IService service)
        {
            _connection = connection;
            _form = form;
            _service = service;
            _repository = new LabBookContrastRepository(_connection, _service);
        }

        public bool IsModified()
        {
            return _laboContrastList
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();
        }

        public void PrepareData()
        {
            _laboContrastList = _repository.GetAll();
            LaboContrastBinding = new BindingSource();
            LaboContrastBinding.DataSource = new List<LaboDataContrastDto>();

            PrepareDgvContrast();
        }

        private void PrepareDgvContrast()
        {
            DataGridView view = _form.GetDgvContrast;
            view.DataSource = LaboContrastBinding;
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
            view.Columns.Remove("Days");
            view.Columns.Remove("DateUpdated");

            view.Columns["Id"].Visible = false;
            view.Columns["LaboId"].Visible = false;
            view.Columns["IsDeleted"].Visible = false;
            view.Columns["Position"].Visible = false;

            view.Columns.Add(CommonFunction.GetDgvDeleteButtonColumn());

            view.Columns["DateCreated"].HeaderText = "Data";
            view.Columns["DateCreated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["DateCreated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateCreated"].DisplayIndex = 1;

            view.Columns["Applicator"].HeaderText = "Aplikator";
            view.Columns["Applicator"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Applicator"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Applicator"].DisplayIndex = 2;

            view.Columns["Substrate"].HeaderText = "Podłoże";
            view.Columns["Substrate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Substrate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Substrate"].DisplayIndex = 3;

            view.Columns["Contrast"].HeaderText = "Krycie";
            view.Columns["Contrast"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["Contrast"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Contrast"].DisplayIndex = 4;

            view.Columns["Sp"].HeaderText = "Sp";
            view.Columns["Sp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["Sp"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Sp"].DisplayIndex = 5;

            view.Columns["Tw"].HeaderText = "Tw";
            view.Columns["Tw"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns["Tw"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Tw"].DisplayIndex = 6;

            view.Columns["Comments"].HeaderText = "Uwagi";
            view.Columns["Comments"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns["Comments"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Comments"].DisplayIndex = 7;
        }

        private LaboDto GetCurrentLaboDto()
        {
            LabBookService service = (LabBookService)_service;
            return service.CurrentLabBook;
        }

        public void AddNew(int id)
        {
            _form.GetDgvContrast.EndEdit();
            LaboContrastBinding.EndEdit();

            LaboDto laboDto = GetCurrentLaboDto();
            if (laboDto == null)
                return;

            if (id == CommonData.STD_APPLICATOR)
                AddStandardApplicators(laboDto.Id);
            else
                AddOthersApplicators(laboDto.Id, id);

            _service.Modify(RowState.ADDED);
        }

        private void AddStandardApplicators(int laboId)
        {
            short position = GetMaxPosition(laboId);

            position++;
            for (int i = 0; i < 4; i++)
            {
                LaboDataContrastDto contrast = new LaboDataContrastDto(-1, laboId, DateTime.Today, false, CommonData.AplikatorsStd[i], position, CommonData.LENETA, DateTime.Today, _service);
                _laboContrastList.Add(contrast);
            }
            SynchronizeData(laboId);
        }

        private void AddOthersApplicators(int laboId, int applicatorNr)
        {
            short position = GetMaxPosition(laboId);

            position++;
            string applicator;
            if (applicatorNr == CommonData.NONE_APPLIKATOR)
            {
                applicator = CommonData.Aplikators[0];
            }
            else
            {
                applicator = CommonData.Aplikators[applicatorNr];
            }

            LaboDataContrastDto contrast = new LaboDataContrastDto(-1, laboId, DateTime.Today, false, applicator, position, CommonData.LENETA, DateTime.Today, _service);
            _laboContrastList.Add(contrast);
            SynchronizeData(laboId);
        }

        private short GetMaxPosition(int laboId)
        {
            return _laboContrastList
                .Where(i => i.LaboId == laboId)
                .Select(i => i.Position)
                .Distinct()
                .DefaultIfEmpty()
                .Max();
        }

        public bool Delete(long id, long tmpId)
        {
            if (id > 0)
            {
                LaboDataContrastDto data = _laboContrastList
                    .Where(i => i.Id == id)
                    .FirstOrDefault();

                if (data == null)
                    return false;

                _laboContrastList.Remove(data);
                _repository.DeleteById(id);
                LaboDto laboDto = GetCurrentLaboDto();
                SynchronizeData(laboDto != null ? laboDto.Id : -1);
            }
            else
            {
                LaboDataContrastDto data = _laboContrastList
                    .Where(i => i.TmpId == tmpId)
                    .FirstOrDefault();

                if (data == null)
                    return false;

                _laboContrastList.Remove(data);
                LaboDto laboDto = GetCurrentLaboDto();
                SynchronizeData(laboDto != null ? laboDto.Id : -1);
            }

            return true;
        }

        public bool Save()
        {
            _form.GetDgvContrast.EndEdit();
            LaboContrastBinding.EndEdit();

            #region Save new

            var added = _laboContrastList
                .Where(i => i.GetRowState == RowState.ADDED)
                .ToList();

            foreach (var con in added)
            {
                CrudState answer = _repository.Save(con).CrudState;
                if (answer == CrudState.OK)
                    con.AcceptChanges();
                else
                    return false;
            }

            #endregion

            #region Update

            var modified = _laboContrastList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var con in modified)
            {
                CrudState answer = _repository.Update(con).CrudState;
                if (answer == CrudState.OK)
                    con.AcceptChanges();
                else
                    return false;
            }

            #endregion

            return true;
        }

        public void SynchronizeData(int LaboId)
        {
            IList<LaboDataContrastDto> contrast = _laboContrastList
                .Where(i => i.LaboId == LaboId)
                .OrderBy(i => i.Position)
                .ToList();

            LaboContrastBinding.DataSource = contrast;
        }

        public void AcceptAllChanges()
        {
            foreach (LaboDataContrastDto item in _laboContrastList)
            {
                item.AcceptChanges();
            }
        }

    }
}
