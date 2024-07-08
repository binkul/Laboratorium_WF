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
    public class LabBookViscosityService : IDgvService
    {
        private readonly SqlConnection _connection;
        private readonly LabForm _form;
        private readonly IService _service;
        IDictionary<string, double> _formData;
        private readonly IBasicCRUD<LaboDataViscosityDto> _repository;
        private IList<LaboDataViscosityDto> _laboViscosityList;
        public BindingSource LaboViscosityBinding { get; private set; }

        public LabBookViscosityService(SqlConnection connection, LabForm form, IDictionary<string, double> formData, IService service)
        {
            _connection = connection;
            _form = form;
            _service = service;
            _formData = formData;
            _repository = new LabBookViscosityRepository(_connection, _service);
        }

        public bool IsModified()
        {
            return _laboViscosityList
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();
        }

        private LaboDto GetCurrentLaboDto()
        {
            LabBookService service = (LabBookService)_service;
            return service.CurrentLabBook;
        }

        public void PrepareData()
        {
            _laboViscosityList = _repository.GetAll();
            LaboViscosityBinding = new BindingSource();
            LaboViscosityBinding.DataSource = _laboViscosityList;
            LaboViscosityBinding.AllowNew = true;

            PrepareDgvViscosity();
        }

        private void PrepareDgvViscosity()
        {
            DataGridView view = _form.GetDgvViscosity;
            view.DataSource = LaboViscosityBinding;
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
            view.AutoGenerateColumns = false;

            view.Columns.Remove("GetRowState");
            view.Columns.Remove("CrudState");
            view.Columns["Id"].Visible = false;
            view.Columns["TmpId"].Visible = false;
            view.Columns["LaboId"].Visible = false;
            view.Columns["Service"].Visible = false;

            view.Columns["ToCompare"].HeaderText = "X";
            view.Columns["ToCompare"].DisplayIndex = 0;
            view.Columns["ToCompare"].Width = _formData.ContainsKey("ToCompare") ? (int)_formData["ToCompare"] : 30;
            view.Columns["ToCompare"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["ToCompare"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["DateCreated"].HeaderText = "Start";
            view.Columns["DateCreated"].DisplayIndex = 1;
            view.Columns["DateCreated"].Width = _formData.ContainsKey("DateCreated") ? (int)_formData["DateCreated"] : 120;
            view.Columns["DateCreated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateCreated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["DateUpdated"].HeaderText = "Koniec";
            view.Columns["DateUpdated"].DisplayIndex = 2;
            view.Columns["DateUpdated"].Width = _formData.ContainsKey("DateUpdated") ? (int)_formData["DateUpdated"] : 120;
            view.Columns["DateUpdated"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["DateUpdated"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Days"].HeaderText = "Doba";
            view.Columns["Days"].DisplayIndex = 3;
            view.Columns["Days"].Width = _formData.ContainsKey("Days") ? (int)_formData["Days"] : 100;
            view.Columns["Days"].ReadOnly = true;
            view.Columns["Days"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Days"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Temp"].HeaderText = "Temp";
            view.Columns["Temp"].DisplayIndex = 4;
            view.Columns["Temp"].Width = _formData.ContainsKey("Temp") ? (int)_formData["Temp"] : 100;
            view.Columns["Temp"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Temp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["pH"].HeaderText = "pH";
            view.Columns["pH"].DisplayIndex = 5;
            view.Columns["pH"].Width = _formData.ContainsKey("pH") ? (int)_formData["pH"] : 100;
            view.Columns["pH"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["pH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook1"].HeaderText = "Lep 1";
            view.Columns["Brook1"].DisplayIndex = 6;
            view.Columns["Brook1"].Width = _formData.ContainsKey("Brook1") ? (int)_formData["Brook1"] : 100;
            view.Columns["Brook1"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook5"].HeaderText = "Lep 5";
            view.Columns["Brook5"].DisplayIndex = 7;
            view.Columns["Brook5"].Width = _formData.ContainsKey("Brook5") ? (int)_formData["Brook5"] : 100;
            view.Columns["Brook5"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook5"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook10"].HeaderText = "Lep 10";
            view.Columns["Brook10"].DisplayIndex = 8;
            view.Columns["Brook10"].Width = _formData.ContainsKey("Brook10") ? (int)_formData["Brook10"] : 100;
            view.Columns["Brook10"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook10"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook20"].HeaderText = "Lep 20";
            view.Columns["Brook20"].DisplayIndex = 9;
            view.Columns["Brook20"].Width = _formData.ContainsKey("Brook20") ? (int)_formData["Brook20"] : 100;
            view.Columns["Brook20"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook20"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook30"].HeaderText = "Lep 30";
            view.Columns["Brook30"].DisplayIndex = 10;
            view.Columns["Brook30"].Width = _formData.ContainsKey("Brook30") ? (int)_formData["Brook30"] : 100;
            view.Columns["Brook30"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook30"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook40"].HeaderText = "Lep 40";
            view.Columns["Brook40"].DisplayIndex = 11;
            view.Columns["Brook40"].Width = _formData.ContainsKey("Brook40") ? (int)_formData["Brook40"] : 100;
            view.Columns["Brook40"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook40"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook50"].HeaderText = "Lep 50";
            view.Columns["Brook50"].DisplayIndex = 12;
            view.Columns["Brook50"].Width = _formData.ContainsKey("Brook50") ? (int)_formData["Brook50"] : 100;
            view.Columns["Brook50"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook50"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook60"].HeaderText = "Lep 60";
            view.Columns["Brook60"].DisplayIndex = 13;
            view.Columns["Brook60"].Width = _formData.ContainsKey("Brook60") ? (int)_formData["Brook60"] : 100;
            view.Columns["Brook60"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook60"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook70"].HeaderText = "Lep 70";
            view.Columns["Brook70"].DisplayIndex = 14;
            view.Columns["Brook70"].Width = _formData.ContainsKey("Brook70") ? (int)_formData["Brook70"] : 100;
            view.Columns["Brook70"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook70"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook80"].HeaderText = "Lep 80";
            view.Columns["Brook80"].DisplayIndex = 15;
            view.Columns["Brook80"].Width = _formData.ContainsKey("Brook80") ? (int)_formData["Brook80"] : 100;
            view.Columns["Brook80"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook80"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook90"].HeaderText = "Lep 90";
            view.Columns["Brook90"].DisplayIndex = 16;
            view.Columns["Brook90"].Width = _formData.ContainsKey("Brook90") ? (int)_formData["Brook90"] : 100;
            view.Columns["Brook90"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook90"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["Brook100"].HeaderText = "Lep 100";
            view.Columns["Brook100"].DisplayIndex = 17;
            view.Columns["Brook100"].Width = _formData.ContainsKey("Brook100") ? (int)_formData["Brook100"] : 100;
            view.Columns["Brook100"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Brook100"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookDisc"].HeaderText = "Dysk";
            view.Columns["BrookDisc"].DisplayIndex = 18;
            view.Columns["BrookDisc"].Width = _formData.ContainsKey("BrookDisc") ? (int)_formData["BrookDisc"] : 70;
            view.Columns["BrookDisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookDisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookComment"].HeaderText = "Brook uwagi";
            view.Columns["BrookComment"].DisplayIndex = 19;
            view.Columns["BrookComment"].Width = _formData.ContainsKey("BrookComment") ? (int)_formData["BrookComment"] : 200;
            view.Columns["BrookComment"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns["BrookXvisc"].HeaderText = "Lep X";
            view.Columns["BrookXvisc"].DisplayIndex = 20;
            view.Columns["BrookXvisc"].Width = _formData.ContainsKey("BrookXvisc") ? (int)_formData["BrookXvisc"] : 100;
            view.Columns["BrookXvisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookXvisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookXrpm"].HeaderText = "Obr. X";
            view.Columns["BrookXrpm"].DisplayIndex = 21;
            view.Columns["BrookXrpm"].Width = _formData.ContainsKey("BrookXrpm") ? (int)_formData["BrookXrpm"] : 100;
            view.Columns["BrookXrpm"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookXrpm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["BrookXdisc"].HeaderText = "Dysk X";
            view.Columns["BrookXdisc"].DisplayIndex = 22;
            view.Columns["BrookXdisc"].Width = _formData.ContainsKey("BrookXdisc") ? (int)_formData["BrookXdisc"] : 100;
            view.Columns["BrookXdisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["BrookXdisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns["Krebs"].HeaderText = "Krebs";
            view.Columns["Krebs"].DisplayIndex = 23;
            view.Columns["Krebs"].Width = 100;
            view.Columns["Krebs"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["Krebs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["KrebsComment"].HeaderText = "Krebs uwagi";
            view.Columns["KrebsComment"].DisplayIndex = 24;
            view.Columns["KrebsComment"].Width = 200;
            view.Columns["KrebsComment"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["KrebsComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns["ICI"].HeaderText = "ICI";
            view.Columns["ICI"].DisplayIndex = 25;
            view.Columns["ICI"].Width = 100;
            view.Columns["ICI"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["ICI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["IciDisc"].HeaderText = "ICI dysk";
            view.Columns["IciDisc"].DisplayIndex = 26;
            view.Columns["IciDisc"].Width = 100;
            view.Columns["IciDisc"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["IciDisc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            view.Columns["IciComment"].HeaderText = "ICI uwagi";
            view.Columns["IciComment"].DisplayIndex = 27;
            view.Columns["IciComment"].Width = 200;
            view.Columns["IciComment"].SortMode = DataGridViewColumnSortMode.NotSortable;
            view.Columns["IciComment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[LaboViscosityBinding.Position].Cells["DateCreated"];
            }

        }

        public void SynchronizeData(int LaboId)
        {
            IList<LaboDataViscosityDto> viscosity = _laboViscosityList
                .Where(i => i.LaboId == LaboId)
                .OrderBy(i => i.DateCreated)
                .ThenBy(i => i.Days)
                .ToList();

            LaboViscosityBinding.DataSource = viscosity;

            SetViscosityVisbility();
        }

        private void SetViscosityVisbility()
        {
            LaboDto labo = GetCurrentLaboDto();
            if (labo == null)
                return;

            LaboDataViscosityColDto profile = labo.ViscosityProfile;

            if (profile == null)
            {
                profile = new LaboDataViscosityColDto(labo.Id, Profile.STD_X, "");
            }

            DataGridView view = _form.GetDgvViscosity;
            view.Columns["pH"].Visible = false;
            view.Columns["Brook1"].Visible = false;
            view.Columns["Brook5"].Visible = false;
            view.Columns["Brook10"].Visible = false;
            view.Columns["Brook20"].Visible = false;
            view.Columns["Brook30"].Visible = false;
            view.Columns["Brook40"].Visible = false;
            view.Columns["Brook50"].Visible = false;
            view.Columns["Brook60"].Visible = false;
            view.Columns["Brook70"].Visible = false;
            view.Columns["Brook80"].Visible = false;
            view.Columns["Brook90"].Visible = false;
            view.Columns["Brook100"].Visible = false;
            view.Columns["BrookDisc"].Visible = false;
            view.Columns["BrookComment"].Visible = false;
            view.Columns["BrookXvisc"].Visible = false;
            view.Columns["BrookXrpm"].Visible = false;
            view.Columns["BrookXdisc"].Visible = false;
            view.Columns["Krebs"].Visible = false;
            view.Columns["KrebsComment"].Visible = false;
            view.Columns["ICI"].Visible = false;
            view.Columns["IciDisc"].Visible = false;
            view.Columns["IciComment"].Visible = false;

            IList<string> profiles = LabBookViscosityColumnService.Profiles[profile.Profile];
            foreach (string column in profiles)
            {
                view.Columns[column].Visible = true;
            }
        }

        private int GetNextTmpId()
        {
            int id = _laboViscosityList
                .Select(i => i.TmpId)
                .Distinct()
                .DefaultIfEmpty()
                .Max();

            return ++id;
        }

        public void AddNew(int id)
        {
            IList<LaboDataViscosityDto> tmpList = (IList<LaboDataViscosityDto>)LaboViscosityBinding.DataSource;
            LaboDataViscosityDto added = tmpList[tmpList.Count - 1];

            if (!_laboViscosityList.Contains(added))
                _laboViscosityList.Add(added);
        }

        internal void AddNew(DataGridViewRowEventArgs e, int labBookId)
        {
            e.Row.Cells["TmpId"].Value = GetNextTmpId();
            e.Row.Cells["LaboId"].Value = labBookId;
            e.Row.Cells["ToCompare"].Value = false;
            e.Row.Cells["Temp"].Value = "20oC";
            e.Row.Cells["DateCreated"].Value = DateTime.Today;
            e.Row.Cells["DateUpdated"].Value = DateTime.Today;
            e.Row.Cells["Service"].Value = _service;
        }

        public bool Delete()
        {
            LaboViscosityBinding.EndEdit();

            if (LaboViscosityBinding.Current == null || _form.GetDgvViscosity.SelectedCells.Count == 0 || _form.GetDgvViscosity.SelectedCells[0].RowIndex == _form.GetDgvViscosity.NewRowIndex)
                return false;

            LaboDataViscosityDto current = (LaboDataViscosityDto)LaboViscosityBinding.Current;
            int id = current.Id;
            int tmpId = current.TmpId;

            if (id > 0)
            {
                LaboDataViscosityDto data = _laboViscosityList
                    .Where(i => i.Id == id)
                    .FirstOrDefault();

                if (data == null)
                    return false;

                _laboViscosityList.Remove(data);
                _repository.DeleteById(id);
                LaboDto laboDto = GetCurrentLaboDto();
                SynchronizeData(laboDto != null ? laboDto.Id : -1);
            }
            else
            {
                LaboDataViscosityDto data = _laboViscosityList
                    .Where(i => i.TmpId == tmpId)
                    .FirstOrDefault();

                if (data == null)
                    return false;

                _laboViscosityList.Remove(data);
                LaboDto laboDto = GetCurrentLaboDto();
                SynchronizeData(laboDto != null ? laboDto.Id : -1);
            }

            return true;
        }

        public bool Save()
        {
            _form.GetDgvViscosity.EndEdit();
            LaboViscosityBinding.EndEdit();

            #region Save new

            var added = _laboViscosityList
                .Where(i => i.GetRowState == RowState.ADDED)
                .ToList();

            foreach (var vis in added)
            {
                CrudState answer = _repository.Save(vis).CrudState;
                if (answer == CrudState.OK)
                    vis.AcceptChanges();
                else
                    return false;
            }

            #endregion

            #region Update

            var modified = _laboViscosityList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var vis in modified)
            {
                CrudState answer = _repository.Update(vis).CrudState;
                if (answer == CrudState.OK)
                    vis.AcceptChanges();
                else
                    return false;
            }

            #endregion


            return true;
        }

        public void AcceptAllChanges()
        {
            foreach (LaboDataViscosityDto item in _laboViscosityList)
            {
                item.AcceptChanges();
            }
        }
    }
}
