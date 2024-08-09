using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Composition.Forms;
using Laboratorium.Composition.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laboratorium.Composition.Service
{
    public class CompositionService : LoadService, IService
    {
        #region DTO-s fields for DGV column

        private const string LABO_ID = "LaboId";
        private const string ORDERING = "Ordering";
        private const string VERSION = "Version";
        private const string MATERIAL = "Material";
        private const string MATERIAL_ID = "MaterialId";
        private const string AMOUNT = "Amount";
        private const string MASS = "Mass";
        private const string INTERMEDIATE = "IsIntermediate";
        private const string OPERATION = "Operation";
        private const string COMMENT = "Comment";
        private const string ROW_STATE = "GetRowState";
        private const string CRUD_STATE = "CrudState";

        #endregion

        private const int STD_WIDTH = 100;
        private const string FORM_DATA = "CompositionForm";
        private readonly IList<string> _dgvCompositionFields = new List<string> { ORDERING, MATERIAL, AMOUNT, MASS, COMMENT };

        private readonly CompositionForm _form;
        private readonly UserDto _user;
        private readonly SqlConnection _connection;
        private readonly int _laboId;

        private readonly IBasicCRUD<CompositionDto> _repository;
        private readonly IBasicCRUD<CompositionHistoryDto> _historyRepository;
        private CompositionHistoryDto _lastVersion;
        private IList<CompositionDto> _recipe;
        private BindingSource _recipeBinding;

        public CompositionService(SqlConnection connection, UserDto user, CompositionForm form, int laboId)
            : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _user = user;
            _laboId = laboId;

            _repository = new CompositionRepository(_connection, this);
            _historyRepository = new CompositionHistoryRepository(_connection);
        }


        #region Modification markers

        protected override bool Status => _recipe.Where(i => i.GetRowState != RowState.UNCHANGED).Any();

        public void Modify(RowState state)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Prepare Data

        protected override void PrepareColumns()
        {
            CompositionForm form = (CompositionForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvComposition,  _dgvCompositionFields}
            };
        }

        public override void PrepareAllData()
        {
            CompositionHistoryRepository repository = (CompositionHistoryRepository)_historyRepository;
            _lastVersion = repository.GetLastFromLaboId(_laboId, _user.Id);

            _recipe = _repository.GetAllByLaboId(_laboId);
            _recipeBinding = new BindingSource();
            _recipeBinding.DataSource = _recipe;
            _recipeBinding.PositionChanged += RecipeBinding_PositionChanged;
            PrepareRecipe();

            PrepareDgvComposition();
        }

        public void PrepareRecipe()
        {
            if (_lastVersion.IsNew)
                return;

            double mass = _lastVersion.Mass;
            foreach (CompositionDto component in _recipe)
            {
                component.Mass = Math.Round(mass * (component.Amount / 100), 4);
            }
        }

        private void PrepareDgvComposition()
        {
            DataGridView view = _form.GetDgvComposition;
            view.DataSource = _recipeBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(ROW_STATE);
            view.Columns.Remove(CRUD_STATE);
            view.Columns.Remove(LABO_ID);
            view.Columns.Remove(VERSION);

            view.Columns[MATERIAL_ID].Visible = false;
            view.Columns[INTERMEDIATE].Visible = false;
            view.Columns[OPERATION].Visible = false;

            view.Columns[ORDERING].HeaderText = "L.p.";
            view.Columns[ORDERING].DisplayIndex = 0;
            view.Columns[ORDERING].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[ORDERING].Width = _formData.ContainsKey(ORDERING) ? (int)_formData[ORDERING] : STD_WIDTH;
            view.Columns[ORDERING].ReadOnly = true;
            view.Columns[ORDERING].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[MATERIAL].HeaderText = "Surowiec";
            view.Columns[MATERIAL].DisplayIndex = 1;
            view.Columns[MATERIAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[MATERIAL].Width = _formData.ContainsKey(MATERIAL) ? (int)_formData[MATERIAL] : STD_WIDTH;
            view.Columns[MATERIAL].ReadOnly = true;
            view.Columns[MATERIAL].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[AMOUNT].HeaderText = "Ilość [%]";
            view.Columns[AMOUNT].DisplayIndex = 2;
            view.Columns[AMOUNT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[AMOUNT].Width = _formData.ContainsKey(AMOUNT) ? (int)_formData[AMOUNT] : STD_WIDTH;
            view.Columns[AMOUNT].ReadOnly = true;
            view.Columns[AMOUNT].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[MASS].HeaderText = "Masa [kg]";
            view.Columns[MASS].DisplayIndex = 3;
            view.Columns[MASS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[MASS].Width = _formData.ContainsKey(MASS) ? (int)_formData[MASS] : STD_WIDTH;
            view.Columns[MASS].ReadOnly = true;
            view.Columns[MASS].SortMode = DataGridViewColumnSortMode.NotSortable;


            view.Columns[COMMENT].HeaderText = "Uwagi";
            view.Columns[COMMENT].DisplayIndex = 4;
            view.Columns[COMMENT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[COMMENT].Width = _formData.ContainsKey(COMMENT) ? (int)_formData[COMMENT] : STD_WIDTH;
            view.Columns[COMMENT].ReadOnly = true;
            view.Columns[COMMENT].SortMode = DataGridViewColumnSortMode.NotSortable;


        }

        #endregion


        #region Current/Binkding/Navigation

        private void RecipeBinding_PositionChanged(object sender, EventArgs e)
        {

        }

        #endregion


        #region DataGridView and Combo  events

        public void RecipeCellFormat(DataGridViewCellFormattingEventArgs e)
        {
            if (_form.GetDgvComposition.Columns[e.ColumnIndex].Name == AMOUNT)
            {
                double value = Convert.ToDouble(e.Value);
                e.Value = value.ToString("0.00");
            }
        }

        #endregion


        #region CRUD

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
