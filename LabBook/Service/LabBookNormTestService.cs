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
    public class LabBookNormTestService : IDgvService
    {
        private readonly SqlConnection _connection;
        private readonly LabForm _form;
        private readonly IService _service;
        private IList<NormDto> _normList;
        private IList<LaboDataNormTestDto> _laboNormTestList;
        public BindingSource LaboNormTestBinding { get; private set; }
        private readonly IBasicCRUD<LaboDataNormTestDto> _repository;

        public LabBookNormTestService(SqlConnection connection, LabForm form, IService service)
        {
            _connection = connection;
            _form = form;
            _service = service;
            _repository = new LabBookNormTestRepository(_connection, _service);
        }

        public bool IsModified()
        {
            return _laboNormTestList
                .Where(i => i.GetRowState != RowState.UNCHANGED)
                .Any();
        }

        private LaboDataNormTestDto _current => LaboNormTestBinding != null ? (LaboDataNormTestDto)LaboNormTestBinding.Current : null;
        private bool _isHead => _current != null ? _current.TmpId == -1 : false;

        public void PrepareData()
        {
            _laboNormTestList = _repository.GetAll();
            LaboNormTestBinding = new BindingSource();
            LaboNormTestBinding.DataSource = new List<LaboDataNormTestDto>();
            LaboNormTestBinding.PositionChanged += LaboNormTestBinding_PositionChanged;

            PrepareDgvNormTest();
            PrepareNormMenu();
            LaboNormTestBinding_PositionChanged(null, null);
        }

        private void LaboNormTestBinding_PositionChanged(object sender, EventArgs e)
        {
            DataGridView view = _form.GetDgvNormTest;
            view.ReadOnly = _isHead;
            view.Columns["Days"].ReadOnly = true;
        }

        private LaboDto GetCurrentLaboDto() 
        {
            LabBookService service = (LabBookService)_service;
            return service.CurrentLabBook;
        }

        private void PrepareDgvNormTest()
        {
            DataGridView view = _form.GetDgvNormTest;
            LabBookService service = (LabBookService)_service;
            IDictionary<string, double> formData = service.GetFormData;

            view.DataSource = LaboNormTestBinding;
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
            view.Columns.Remove("GroupId");

            view.Columns["Id"].Visible = false;
            view.Columns["TmpId"].Visible = false;
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
            LoadDataForMenu();

            var menus = _normList
                .Select(i => new { i.Group, i.GroupId })
                .Distinct()
                .ToList();

            foreach (var menu in menus)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = menu.Group;
                item.Name = menu.Group;
                item.Tag = menu.GroupId;

                var subMenus = _normList
                    .Where(i => i.Group.Equals(menu.Group))
                    .Select(i => new { i.NamePl, i.Id } )
                    .ToList();
                foreach(var subMenu in subMenus)
                {
                    ToolStripMenuItem subItem = new ToolStripMenuItem();
                    subItem.Name = "SubMenu_" + subMenu.Id.ToString();
                    subItem.Text = subMenu.NamePl;
                    subItem.Tag = subMenu.Id;
                    subItem.Click += AddNormFromMenu;
                    item.DropDownItems.Add(subItem);
                }

;                _form.GetNormMenu.DropDownItems.Add(item);
            }
        }

        private void LoadDataForMenu()
        {
            IBasicCRUD<NormDto> normRep = new NormRepository(_connection);
            _normList = normRep.GetAll();

            IBasicCRUD<NormDetailDto> detailRep = new NormDetailRepository(_connection);
            IList<NormDetailDto> details = detailRep.GetAll();

            foreach (NormDto norm in _normList)
            {
                List<NormDetailDto> tmp = details
                    .Where(i => i.NormId == norm.Id)
                    .ToList();

                norm.Details = tmp;
            }
        }

        private void AddNormFromMenu(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            LaboDto laboDto = GetCurrentLaboDto();

            if (menu.Tag == null || laboDto == null)
                return;

            int id = Convert.ToInt16(menu.Tag);
            AddNew(id);

            SynchronizeData(laboDto.Id);
            _service.Modify(RowState.ADDED);
        }

        public void SynchronizeData(int LaboId)
        {
            LaboDto laboDto = GetCurrentLaboDto();

            if (laboDto == null)
                return;

            // get all norm test for current labo
            List<LaboDataNormTestDto> normList = _laboNormTestList
                .Where(i => i.LaboId == laboDto.Id)
                .OrderBy(i => i.Position)
                .ToList();

            // get all groups from above
            List<byte> groups = normList
                .Select(i => i.GroupId)
                .Distinct()
                .ToList();

            // add groups as heders (wth LaboId = -1)
            foreach(byte group in groups)
            {
                string name = _normList.Where(i => i.GroupId == group).Select(i => i.Group).Distinct().FirstOrDefault();
                LaboDataNormTestDto head = new LaboDataNormTestDto(-1, -1, 0, name, "", "", group, _service);
                normList.Add(head);
            }

            // order norm test I-by groups, II-by position in groups
            normList = normList
                .OrderBy(i => i.GroupId)
                .ThenBy(i => i.Position)
                .ToList();

            LaboNormTestBinding.DataSource = normList;
        }

        public void AddNew(int id)
        {
            LaboDto laboDto = GetCurrentLaboDto();

            NormDto norm = _normList
                .Where(i => i.Id == id)
                .FirstOrDefault();

            if (norm == null)
                return;

            // get max TmpId in LaboNormTestList
            int maxId = _laboNormTestList
                .Select(i => i.TmpId)
                .Distinct()
                .DefaultIfEmpty()
                .Max();

            // get all tests that belong to the norm
            List<NormDetailDto> subNorm = norm.Details
                .Where(i => i.NormId == id)
                .OrderBy(i => i.Id)
                .ToList();

            if (subNorm.Count == 0)
            {
                NormDetailDto mainSub = new NormDetailDto(0, norm.Id, "", norm.NamePl);
                subNorm.Add(mainSub);
            }

            // get last position of tests that belong to the current labo
            short position = _laboNormTestList
                .Where(i => i.LaboId == laboDto.Id)
                .Select(i => i.Position)
                .DefaultIfEmpty()
                .Max();

            // insert new test to list
            position++;
            maxId++;
            foreach (NormDetailDto detail in subNorm)
            {
                LaboDataNormTestDto test = new LaboDataNormTestDto(maxId, laboDto.Id, position, norm.NamePl, detail.Detail, detail.Substrate, norm.GroupId, _service);
                _laboNormTestList.Add(test);
                position++;
                maxId++;
            }
        }

        public bool Delete()
        {
            LaboNormTestBinding.EndEdit();

            if (LaboNormTestBinding.Current == null || _form.GetDgvNormTest.SelectedCells.Count == 0 || _form.GetDgvNormTest.SelectedCells[0].RowIndex == _form.GetDgvNormTest.NewRowIndex)
                return false;

            LaboDataNormTestDto current = (LaboDataNormTestDto)LaboNormTestBinding.Current;
            int id = current.Id;
            int tmpId = current.TmpId;


            if (id > 0)
            {
                LaboDataNormTestDto data = _laboNormTestList
                    .Where(i => i.Id == id)
                    .FirstOrDefault();

                if (data == null)
                    return false;

                _laboNormTestList.Remove(data);
                _repository.DeleteById(id);
                LaboDto laboDto = GetCurrentLaboDto();
                SynchronizeData(laboDto != null ? laboDto.Id : -1);
            }
            else
            {
                LaboDataNormTestDto data = _laboNormTestList
                    .Where(i => i.TmpId == tmpId)
                    .FirstOrDefault();

                if (data == null)
                    return false;

                _laboNormTestList.Remove(data);
                LaboDto laboDto = GetCurrentLaboDto();
                SynchronizeData(laboDto != null ? laboDto.Id : -1);
            }

            return true;
        }

        public bool Save()
        {
            _form.GetDgvNormTest.EndEdit();
            LaboNormTestBinding.EndEdit();

            #region Save new

            var added = _laboNormTestList
                .Where(i => i.GetRowState == RowState.ADDED)
                .ToList();

            foreach (var norm in added)
            {
                CrudState answer = _repository.Save(norm).CrudState;
                if (answer == CrudState.OK)
                    norm.AcceptChanges();
                else
                    return false;
            }

            #endregion

            #region Update

            var modified = _laboNormTestList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var norm in modified)
            {
                CrudState answer = _repository.Update(norm).CrudState;
                if (answer == CrudState.OK)
                    norm.AcceptChanges();
                else
                    return false;
            }

            #endregion

            return true;
        }

        public void AcceptAllChanges()
        {
            foreach (LaboDataNormTestDto item in _laboNormTestList)
            {
                item.AcceptChanges();
            }
        }

    }
}
