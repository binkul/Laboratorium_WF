using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Material.Service
{
    public class MaterialCompositionService : LoadService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string MATERIAL_ID = "MaterialId";
        private const string COMPOUND_ID = "CompoundId";
        private const string COMPOUND = "Compound";
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
        private const string AMOUNT_MIN = "AmountMin";
        private const string AMOUNT_MAX = "AmountMax";
        private const string ORDERING = "Ordering";
        private const string REMARKS = "Remarks";
        private const string COMPOUND_NAME = "CompoundName";
        private const string COMPOUND_SHORT = "CompoundShort";
        private const string COMPOUND_CAS = "CompoundCas";
        private const string COMPOUND_WE = "CompoundWe";
        private const string CRUD_STATE = "CrudState";

        #endregion

        private readonly IList<string> _dgvCompundFields = new List<string> { SHORT_PL, CAS };
        private readonly IList<string> _dgvCompositionFields = new List<string> { COMPOUND_SHORT, COMPOUND_CAS, AMOUNT_MIN, AMOUNT_MAX, REMARKS };
        private const string FORM_DATA = "CompositionForm";
        private const int STD_WIDTH = 100;

        private readonly MaterialCompositionForm _form;
        private readonly SqlConnection _connection;
        private readonly MaterialDto _material;
        private readonly IBasicCRUD<MaterialCompositionDto> _repository;

        public IList<MaterialCompositionDto> CompositionList { get; private set; }
        public bool IsSaved = false;
        private IList<CompoundDto> _compoundList;
        private BindingSource _compoundBinding;
        private BindingSource _compositionBinding;
        private bool CompositionChanged = false;

        public MaterialCompositionService(SqlConnection connection, MaterialCompositionForm form, MaterialDto material) : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _material = material;
            _repository = new MaterialCompositionRepository(connection);
            
        }

        #region Change status to save button

        private void ChangeStatus(bool status)
        {
            CompositionChanged = status;
            _form.EnableSave(Status);
        }

        protected override bool Status => CompositionChanged;

        #endregion


        #region Prepare data

        protected override void PrepareColumns()
        {
            MaterialCompositionForm form = (MaterialCompositionForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvCompound,  _dgvCompundFields},
                { form.GetDgvComposition, _dgvCompositionFields }
            };
        }

        public override void PrepareAllData()
        {
            IBasicCRUD<CompoundDto> repo = new CompoundRepository(_connection, null);
            _compoundList = repo.GetAll();
            _compoundBinding = new BindingSource();
            _compoundBinding.DataSource = _compoundList;

            CompositionList = _repository.GetAllByLaboId(_material.Id);
            _compositionBinding = new BindingSource();
            _compositionBinding.DataSource = CompositionList;

            PrepareComposition();
            PrepareDgvCompound();
            PrepareDgvComposition();
        }

        private void PrepareComposition()
        {
            foreach (MaterialCompositionDto composition in CompositionList)
            {
                int id = composition.CompoundId;
                CompoundDto cpompoud = _compoundList.Where(i => i.Id == id).FirstOrDefault();
                composition.Compound = cpompoud;
            }
        }

        private void PrepareDgvCompound()
        {
            DataGridView view = _form.GetDgvCompound;
            view.DataSource = _compoundBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersVisible = false;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = true;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(NAME_PL);
            view.Columns.Remove(NAME_EN);
            view.Columns.Remove(SHORT_EN);
            view.Columns.Remove(INDEX);
            view.Columns.Remove(WE);
            view.Columns.Remove(FORMULA);
            view.Columns.Remove(IS_BIO);
            view.Columns.Remove(GET_ROW_STATE);
            view.Columns.Remove(DATE_CREATED);
            view.Columns.Remove(CRUD_STATE);

            view.Columns[ID].Visible = false;

            view.Columns[SHORT_PL].HeaderText = "Nazwa";
            view.Columns[SHORT_PL].DisplayIndex = 0;
            view.Columns[SHORT_PL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[SHORT_PL].Width = _formData.ContainsKey(SHORT_PL) ? (int)_formData[SHORT_PL] : STD_WIDTH;
            view.Columns[SHORT_PL].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[CAS].HeaderText = "CAS";
            view.Columns[CAS].DisplayIndex = 1;
            view.Columns[CAS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CAS].Width = _formData.ContainsKey(CAS) ? (int)_formData[CAS] : STD_WIDTH;
            view.Columns[CAS].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_compoundBinding.Position].Cells[SHORT_PL];
            }
        }

        private void PrepareDgvComposition()
        {
            DataGridView view = _form.GetDgvComposition;
            view.DataSource = _compositionBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidth = 35;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(GET_ROW_STATE);
            view.Columns.Remove(DATE_CREATED);
            view.Columns.Remove(MATERIAL_ID);
            view.Columns.Remove(COMPOUND_ID);
            view.Columns.Remove(COMPOUND);
            view.Columns.Remove(CRUD_STATE);

            view.Columns[ORDERING].Visible = false;
            view.Columns[COMPOUND_NAME].Visible = false;
            view.Columns[COMPOUND_WE].Visible = false;

            view.Columns[COMPOUND_SHORT].HeaderText = "Nazwa";
            view.Columns[COMPOUND_SHORT].DisplayIndex = 0;
            view.Columns[COMPOUND_SHORT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[COMPOUND_SHORT].Width = _formData.ContainsKey(COMPOUND_SHORT) ? (int)_formData[COMPOUND_SHORT] : STD_WIDTH;
            view.Columns[COMPOUND_SHORT].ReadOnly = true;
            view.Columns[COMPOUND_SHORT].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[COMPOUND_CAS].HeaderText = "CAS";
            view.Columns[COMPOUND_CAS].DisplayIndex = 1;
            view.Columns[COMPOUND_CAS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[COMPOUND_CAS].Width = _formData.ContainsKey(COMPOUND_CAS) ? (int)_formData[COMPOUND_CAS] : STD_WIDTH;
            view.Columns[COMPOUND_CAS].ReadOnly = true;
            view.Columns[COMPOUND_CAS].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[AMOUNT_MIN].HeaderText = "Ilośc min";
            view.Columns[AMOUNT_MIN].DisplayIndex = 2;
            view.Columns[AMOUNT_MIN].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[AMOUNT_MIN].Width = _formData.ContainsKey(AMOUNT_MIN) ? (int)_formData[AMOUNT_MIN] : STD_WIDTH;
            view.Columns[AMOUNT_MIN].ReadOnly = false;
            view.Columns[AMOUNT_MIN].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[AMOUNT_MAX].HeaderText = "Ilość max";
            view.Columns[AMOUNT_MAX].DisplayIndex = 3;
            view.Columns[AMOUNT_MAX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[AMOUNT_MAX].Width = _formData.ContainsKey(AMOUNT_MAX) ? (int)_formData[AMOUNT_MAX] : STD_WIDTH;
            view.Columns[AMOUNT_MAX].ReadOnly = false;
            view.Columns[AMOUNT_MAX].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[REMARKS].HeaderText = "Uwagi";
            view.Columns[REMARKS].DisplayIndex = 4;
            view.Columns[REMARKS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[REMARKS].Width = _formData.ContainsKey(REMARKS) ? (int)_formData[REMARKS] : STD_WIDTH;
            view.Columns[REMARKS].ReadOnly = false;
            view.Columns[REMARKS].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_compositionBinding.Position].Cells[COMPOUND_SHORT];
            }

        }

        #endregion


        #region CRUD

        public override bool Save()
        {
            _compositionBinding.EndEdit();
            _repository.DeleteById(_material.Id);

            foreach (var compo in CompositionList)
            {
                var status = _repository.Save(compo).CrudState;
                if (status != ADO.CrudState.OK)
                    return false;
            }

            ChangeStatus(false);
            IsSaved = true;
            return true;
        }

        #endregion


        #region Add/Remove/Up/Down button

        public void AddOne()
        {
            if (_compoundBinding.Count == 0 || _compoundBinding.Current == null || _material == null)
                return;

            CompoundDto compound = (CompoundDto)_compoundBinding.Current;
            byte position = CompositionList.Count > 0 ? CompositionList.Max(i => i.Ordering) : (byte)0;
            position++;
            MaterialCompositionDto compo = new MaterialCompositionDto(_material.Id, compound.Id, 0, 0, position);
            compo.Compound = compound;
            CompositionList.Add(compo);
            CompositionList = CompositionList.OrderBy(i => i.Ordering).ToList();
            _compositionBinding.DataSource = CompositionList;
            _compositionBinding.Position = _compositionBinding.Count - 1;

            ChangeStatus(true);
        }

        public void RemoveOne()
        {
            if (_compositionBinding.Count == 0 || _compositionBinding.Current == null)
                return;
            _compositionBinding.EndEdit();

            MaterialCompositionDto compo = (MaterialCompositionDto)_compositionBinding.Current;
            _compositionBinding.Remove(compo);

            ChangeStatus(true);
        }

        public void RemoveAll()
        {
            if (_compositionBinding.Count == 0)
                return;

            _compositionBinding.EndEdit();

            do
            {
                _compositionBinding.RemoveAt(0);
            } while (_compositionBinding.Count > 0);

            ChangeStatus(true);
        }

        public void MoveUp()
        {
            if (CompositionList.Count <= 1 || _compositionBinding.Current == null || _compositionBinding.Position == 0)
                return;

            MaterialCompositionDto current = (MaterialCompositionDto)_compositionBinding.Current;
            MaterialCompositionDto compoUp = (MaterialCompositionDto)_compositionBinding[_compositionBinding.Position - 1];

            byte currentPosition = current.Ordering;
            byte upPosition = compoUp.Ordering;
            int position = _compositionBinding.Position - 1;

            current.Ordering = upPosition;
            compoUp.Ordering = currentPosition;

            CompositionList = CompositionList.OrderBy(i => i.Ordering).ToList();
            _compositionBinding.DataSource = CompositionList;
            _compositionBinding.Position = position;

            ChangeStatus(true);
        }

        public void MoveDown()
        {
            if (CompositionList.Count <= 1 || _compositionBinding.Current == null || _compositionBinding.Position == _compositionBinding.Count - 1)
                return;

            MaterialCompositionDto current = (MaterialCompositionDto)_compositionBinding.Current;
            MaterialCompositionDto compoDown = (MaterialCompositionDto)_compositionBinding[_compositionBinding.Position + 1];

            byte currentPosition = current.Ordering;
            byte downPosition = compoDown.Ordering;
            int position = _compositionBinding.Position + 1;

            current.Ordering = downPosition;
            compoDown.Ordering = currentPosition;

            CompositionList = CompositionList.OrderBy(i => i.Ordering).ToList();
            _compositionBinding.DataSource = CompositionList;
            _compositionBinding.Position = position;

            ChangeStatus(true);
        }

        #endregion


        #region DataGridView and others events

        public void DgvCompoundColumnWidthChanged()
        {
            DataGridView view = _form.GetDgvCompound;
            int headerWidth = view.RowHeadersVisible ? view.RowHeadersWidth : 0;

            _form.GetTxtFilteringName.Left = view.Left + headerWidth;
            _form.GetTxtFilteringName.Width = view.Columns[SHORT_PL].Width - 1;

            _form.GetTxtFilteringCas.Left = view.Left + view.Columns[SHORT_PL].Width + headerWidth + 1;
            _form.GetTxtFilteringCas.Width = view.Columns[CAS].Width;
        }


        public void CellValueChanged()
        {
            if (_form.Init)
                return;

            ChangeStatus(true);
        }

        #endregion


        #region Filtration

        public void Filtering()
        {
            string name = _form.GetTxtFilteringName.Text;
            string cas = _form.GetTxtFilteringCas.Text;

            IList<CompoundDto> filtered;

            filtered = _compoundList
                .Where(i => i.ShortPl.ToLower().Contains(name.ToLower()))
                .Where(i => i.CAS.ToLower().Contains(cas.ToLower()))
                .ToList();

            _compoundBinding.DataSource = filtered;
            _compoundBinding.Position = filtered.Count > 0 ? 0 : -1;

        }


        #endregion
    }
}
