using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Compound.Forms;
using Laboratorium.Material.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Compound.Service
{
    public class CompoundService : LoadService, IService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string NAME_EN = "NameEn";
        private const string SHORT_PL = "ShortPl";
        private const string SHORT_EN = "ShortEn";
        private const string INDEX = "Index";
        private const string CAS = "CAS";
        private const string WE = "WE";
        private const string FORMULA = "Formula";
        private const string IS_BIO = "IsBio";
        private const string GET_ROW_STATE = "GetRowState";
        private const string DATE_CREATED = "DateCreated";
        private const string CRUD_STATE = "CrudState";

        #endregion

        private readonly IList<string> _dgvCompundFields = new List<string> { NAME_PL, SHORT_PL, CAS, WE, IS_BIO };
        private const string FORM_DATA = "Compound Form";
        private const int STD_WIDTH = 100;

        private readonly CompoundForm _form;
        private readonly SqlConnection _connection;
        private readonly IBasicCRUD<CompoundDto> _repository;

        public CompoundDto CurrentCompound;
        private IList<CompoundDto> _compoundList;
        private BindingSource _compoundBinding;

        private bool CompoundChanged = false;
        private bool FilterBlock = false;

        public CompoundService(SqlConnection connection, CompoundForm form) : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _repository = new CompoundRepository(_connection, this);
        }


        #region Change status to save button
        
        public void Modify(RowState state)
        {
            if (state != RowState.UNCHANGED)
                ChangeStatus(true);
        }

        public void ChangeStatus(bool status)
        {
            CompoundChanged = status;
            _form.EnableSave(Status);
        }

        private bool CheckStatus => _compoundList.Any(i => i.GetRowState != RowState.UNCHANGED);

        protected override bool Status => CompoundChanged;

        #endregion


        #region Data preparation

        protected override void PrepareColumns()
        {
            CompoundForm form = (CompoundForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvCompound,  _dgvCompundFields}
            };
        }

        public override void PrepareAllData()
        {
            _compoundList = _repository.GetAll();
            _compoundBinding = new BindingSource();
            _compoundBinding.DataSource = _compoundList;
            _compoundBinding.PositionChanged += CompoundBinding_PositionChanged;
            _form.GetNavigator.BindingSource = _compoundBinding;

            PrepareDgvCompound();
            PrepareOtherConrols();

            CompoundBinding_PositionChanged(null, null);
        }

        private void PrepareDgvCompound()
        {
            DataGridView view = _form.GetDgvCompound;
            view.DataSource = _compoundBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.AutoGenerateColumns = false;

            view.Columns.Remove(NAME_EN);
            view.Columns.Remove(SHORT_EN);
            view.Columns.Remove(DATE_CREATED);
            view.Columns.Remove(INDEX);
            view.Columns.Remove(FORMULA);
            view.Columns.Remove(GET_ROW_STATE);
            view.Columns.Remove(CRUD_STATE);

            view.Columns[ID].Visible = false;

            view.Columns[NAME_PL].HeaderText = "Nazwa";
            view.Columns[NAME_PL].DisplayIndex = 0;
            view.Columns[NAME_PL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[NAME_PL].Width = _formData.ContainsKey(NAME_PL) ? (int)_formData[NAME_PL] : STD_WIDTH;
            view.Columns[NAME_PL].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[SHORT_PL].HeaderText = "Skrót";
            view.Columns[SHORT_PL].DisplayIndex = 1;
            view.Columns[SHORT_PL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[SHORT_PL].Width = _formData.ContainsKey(SHORT_PL) ? (int)_formData[SHORT_PL] : STD_WIDTH;
            view.Columns[SHORT_PL].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[IS_BIO].HeaderText = "Bio";
            view.Columns[IS_BIO].DisplayIndex = 2;
            view.Columns[IS_BIO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[IS_BIO].Width = _formData.ContainsKey(IS_BIO) ? (int)_formData[IS_BIO] : STD_WIDTH;
            view.Columns[IS_BIO].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[CAS].HeaderText = "CAS";
            view.Columns[CAS].DisplayIndex = 3;
            view.Columns[CAS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CAS].Width = _formData.ContainsKey(CAS) ? (int)_formData[CAS] : STD_WIDTH;
            view.Columns[CAS].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[WE].HeaderText = "WE";
            view.Columns[WE].DisplayIndex = 4;
            view.Columns[WE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[WE].Width = _formData.ContainsKey(WE) ? (int)_formData[WE] : STD_WIDTH;
            view.Columns[WE].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_compoundBinding.Position].Cells[SHORT_PL];
            }
        }

        private void PrepareOtherConrols()
        {
            _form.GetTxtNamePl.DataBindings.Clear();
            _form.GetTxtNameEn.DataBindings.Clear();
            _form.GetTxtShortPl.DataBindings.Clear();
            _form.GetTxtShortEn.DataBindings.Clear();
            _form.GetTxtIndeks.DataBindings.Clear();
            _form.GetTxtCas.DataBindings.Clear();
            _form.GetTxtWE.DataBindings.Clear();
            _form.GetTxtFormula.DataBindings.Clear();
            _form.GetChbIsBio.DataBindings.Clear();

            _form.GetTxtNamePl.DataBindings.Add("Text", _compoundBinding, NAME_PL);
            _form.GetTxtNameEn.DataBindings.Add("Text", _compoundBinding, NAME_EN);
            _form.GetTxtShortPl.DataBindings.Add("Text", _compoundBinding, SHORT_PL);
            _form.GetTxtShortEn.DataBindings.Add("Text", _compoundBinding, SHORT_EN);
            _form.GetTxtIndeks.DataBindings.Add("Text", _compoundBinding, INDEX);
            _form.GetTxtCas.DataBindings.Add("Text", _compoundBinding, CAS);
            _form.GetTxtWE.DataBindings.Add("Text", _compoundBinding, WE);
            _form.GetTxtFormula.DataBindings.Add("Text", _compoundBinding, FORMULA);
            _form.GetChbIsBio.DataBindings.Add("Checked", _compoundBinding, IS_BIO);
        }

        #endregion


        #region Current/Binding/Navigation

        private void CompoundBinding_PositionChanged(object sender, EventArgs e)
        {
            #region Set Current Compound

            if (_compoundBinding == null || _compoundBinding.Count == 0)
            {
                CurrentCompound = null;
            }
            else
            {
                CurrentCompound = (CompoundDto)_compoundBinding.Current;
            }

            #endregion

            #region Set Current Controls

            if (CurrentCompound != null)
            {
                DateTime date = Convert.ToDateTime(CurrentCompound.DateCreated);
                string show = date.ToString("dd-MM-yyyy");
                _form.GetLblDate.Text = show;
            }
            else
            {
                _form.GetLblDate.Text = "-- Brak --";
            }

            #endregion
        }

        #endregion


        #region Button Add/Delete

        public void AddNewCompound()
        {
            CompoundDto compound = new CompoundDto(0 ,"", "", "", "", "", "", "", "", false, DateTime.Today, this);
            ClearFiltrationByNewAdd();
            _compoundBinding.Add(compound);
            _compoundBinding.Position = _compoundBinding.Count - 1;
            ChangeStatus(true);
        }

        public void DeleteCompound()
        {
            DialogResult result = MessageBox.Show("Czy usunąć bierzący związek chemiczny: '" + CurrentCompound.ShortPl + "' z bazy danych ?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No || CurrentCompound == null)
                return;

            _compoundBinding.RemoveCurrent();

            if (CurrentCompound.Id > 0)
            {
                _repository.DeleteById(CurrentCompound.Id);
            }

            ChangeStatus(CheckStatus);
        }

        #endregion


        #region DataGridView Events

        public void ChangeFilterWidth()
        {
            int border = 2;
            var view = _form.GetDgvCompound;

            _form.GetBtnFilerClear.Left = view.Left + (view.RowHeadersWidth / 2) - (_form.GetBtnFilerClear.Width / 2) - border;

            _form.GetTxtFilterName.Left = view.Left + view.RowHeadersWidth - border;
            _form.GetTxtFilterName.Width = view.Columns[NAME_PL].Width - border;

            _form.GetTxtFilterShort.Left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width + border;
            _form.GetTxtFilterShort.Width = view.Columns[SHORT_PL].Width - border;

            _form.GetTxtFilterCas.Left = _form.GetTxtFilterShort.Left + _form.GetTxtFilterShort.Width + border;
            _form.GetTxtFilterCas.Width = view.Columns[CAS].Width - border;

            _form.GetChbFilterIsBio.Left = _form.GetTxtFilterCas.Left + _form.GetTxtFilterCas.Width
                + (view.Columns[IS_BIO].Width / 2) - (_form.GetChbFilterIsBio.Width / 2);

            _form.GetTxtFilterWe.Left = _form.GetTxtFilterCas.Left + _form.GetTxtFilterCas.Width + view.Columns[IS_BIO].Width + border;
            _form.GetTxtFilterWe.Width = view.Columns[WE].Width - border;
        }

        #endregion


        #region CRUD
        public override bool Save()
        {
            _form.GetDgvCompound.EndEdit();
            _compoundBinding.EndEdit();

            #region save

            var saveList = _compoundList
                .Where(i => i.GetRowState == RowState.ADDED)
                .ToList();

            foreach (var compound in saveList)
            {
                if (!CheckBeforeSave(compound))
                {
                    ClearFiltrationByNewAdd();
                    var find = _compoundList.IndexOf(compound);
                    _compoundBinding.Position = find;
                    return false;
                }

                CrudState saveState = _repository.Save(compound).CrudState;
                if (saveState == CrudState.OK)
                    compound.AcceptChanges();
                else
                    return false;
            }

            #endregion

            #region Update

            var updateList = _compoundList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var compound in saveList)
            {
                if (!CheckBeforeSave(compound))
                {
                    ClearFiltrationByNewAdd();
                    var find = _compoundList.IndexOf(compound);
                    _compoundBinding.Position = find;
                    return false;
                }

                CrudState saveState = _repository.Update(compound).CrudState;
                if (saveState == CrudState.OK)
                    compound.AcceptChanges();
                else
                    return false;
            }

            #endregion

            return true;
        }

        private bool CheckBeforeSave(CompoundDto compound)
        {
            if (string.IsNullOrEmpty(compound.NamePl))
            {
                MessageBox.Show("Brak polskiej nazwy związku chemicznego. Nie mozna zapisac związku bez nazwy. Uzupełnij nazwę.", "Brak nazwy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(compound.NamePl))
            {
                MessageBox.Show("Brak polskiej nazwy skrótowej związku chemicznego. Nie mozna zapisac związku bez jego skrótu. Uzupełnij skrót.", "Brak skrótu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion


        #region Filtering

        private void ClearFiltrationByNewAdd()
        {
            FilterBlock = true;

            _form.GetTxtFilterName.Text = "";
            _form.GetTxtFilterShort.Text = "";
            _form.GetTxtFilterCas.Text = "";
            _form.GetTxtFilterWe.Text = "";
            _form.GetChbFilterIsBio.Checked = false;

            _compoundBinding.DataSource = _compoundList;

            FilterBlock = false;
        }

        public void ClearFiltrationByButton()
        {
            FilterBlock = true;

            _form.GetTxtFilterName.Text = "";
            _form.GetTxtFilterShort.Text = "";
            _form.GetTxtFilterCas.Text = "";
            _form.GetTxtFilterWe.Text = "";
            _form.GetChbFilterIsBio.Checked = false;

            int position = -1;
            if (CurrentCompound != null)
            {
                int id = CurrentCompound.Id;

                var current = _compoundList
                    .Where(i => i.Id == id)
                    .FirstOrDefault();
                position = current != null ? _compoundList.IndexOf(current) : -1;
            }

            _compoundBinding.DataSource = _compoundList;
            _compoundBinding.Position = position;

            FilterBlock = false;
        }

        private bool IsFiltrationSet()
        {
            string namePl = _form.GetTxtFilterName.Text;
            string shortPl = _form.GetTxtFilterShort.Text;
            string cas = _form.GetTxtFilterCas.Text;
            string we = _form.GetTxtFilterWe.Text;
            bool bio = _form.GetChbFilterIsBio.Checked;

            return !string.IsNullOrEmpty(namePl) | !string.IsNullOrEmpty(shortPl)
                | !string.IsNullOrEmpty(cas) | !string.IsNullOrEmpty(we) | bio;
        }

        public void SetFiltration()
        {
            if (FilterBlock)
                return;

            string namePl = _form.GetTxtFilterName.Text.ToLower();
            string shortPl = _form.GetTxtFilterShort.Text.ToLower();
            string cas = _form.GetTxtFilterCas.Text.ToLower();
            string we = _form.GetTxtFilterWe.Text.ToLower();
            bool bio = _form.GetChbFilterIsBio.Checked;

            bool namePl_set = string.IsNullOrEmpty(namePl);
            bool shortPl_set = string.IsNullOrEmpty(shortPl);
            bool cas_set = string.IsNullOrEmpty(cas);
            bool we_set = string.IsNullOrEmpty(we);

            if (IsFiltrationSet())
            {
                List<CompoundDto> filtered = _compoundList
                    .Where(i => i.NamePl != null && (namePl_set || i.NamePl.ToLower().Contains(namePl)))
                    .Where(i => i.ShortPl != null && (shortPl_set || i.ShortPl.ToLower().Contains(shortPl)))
                    .Where(i => i.CAS != null && (cas_set || i.CAS.ToLower().Contains(cas)))
                    .Where(i => i.WE != null && (we_set || i.WE.ToLower().Contains(we)))
                    .Where(i => !bio || i.IsBio)
                    .ToList();

                _compoundBinding.DataSource = filtered;
            }
            else
            {
                _compoundBinding.DataSource = _compoundList;
            }
        }


        #endregion`
    }
}
