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
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Compound.Service
{
    public class CompoundService : LoadService
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

        public CompoundService(SqlConnection connection, CompoundForm form) : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _repository = new CompoundRepository(_connection);
        }


        #region Change status to save button

        private void ChangeStatus(bool status)
        {
            CompoundChanged = status;
            _form.EnableSave(Status);
        }

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


        #region DataGridView Events

        public void ChangeFilterWidth()
        {

        }

        #endregion

        public override bool Save()
        {
            throw new NotImplementedException();
        }

    }
}
