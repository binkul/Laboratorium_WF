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
        private const string IS_SEMIPRODUCT = "IsSemiproduct";
        private const string SEMIPRODUCT_LEVEL = "SemiProductLevel";
        private const string SEMIPRODUCT_STATE = "SemiProductState";
        private const string OPERATION = "Operation";
        private const string COMMENT = "Comment";
        private const string ROW_STATE = "GetRowState";
        private const string CRUD_STATE = "CrudState";
        private const string CURRENCY = "Currency";
        private const string RATE = "Rate";
        private const string PRICE_MASS = "PriceMass";
        private const string PRICE_PL_KG = "PricePlKg";
        private const string PRICE_ORIGINAL = "PriceOriginal";
        private const string PRICE_CURRENCY = "PriceCurrency";
        private const string VOC = "VOC";
        private const string VOC_PERCENT = "VocPercent";
        private const string VOC_AMOUNT = "VocAmount";

        #endregion

        private const int STD_WIDTH = 100;
        private const string FORM_DATA = "CompositionForm";
        private readonly IList<string> _dgvCompositionFields = new List<string> { SEMIPRODUCT_STATE, ORDERING, MATERIAL, AMOUNT, MASS, COMMENT, PRICE_PL_KG, PRICE_CURRENCY, VOC_PERCENT, VOC_AMOUNT, PRICE_MASS };

        private readonly CompositionForm _form;
        private readonly UserDto _user;
        private readonly SqlConnection _connection;
        private readonly LaboDto _laboDto;

        private readonly IBasicCRUD<CompositionDto> _repository;
        private readonly IBasicCRUD<CompositionHistoryDto> _historyRepository;
        private CompositionHistoryDto _lastVersion;
        private IList<CompositionDto> _recipe;
        private BindingSource _recipeBinding;

        public CompositionService(SqlConnection connection, UserDto user, CompositionForm form, LaboDto laboDto)
            : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _user = user;
            _laboDto = laboDto;

            _repository = new CompositionRepository(_connection, this);
            _historyRepository = new CompositionHistoryRepository(_connection);
        }


        #region Modification markers

        protected override bool Status => _recipe.Where(i => i.GetRowState != RowState.UNCHANGED).Any();

        public void Modify(RowState state)
        {

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
            _lastVersion = repository.GetLastFromLaboId(_laboDto.Id, _user.Id);

            _recipe = _repository.GetAllByLaboId(_laboDto.Id);
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

                double? voc = (component.Amount * component.VOC) / 100;
                component.VocAmount = component.VOC != -1 ? ((Convert.ToDouble(voc) * mass) / 100).ToString("0.00") : "Brak";

                double? price = component.PricePlKg * component.Mass;
                component.PriceMass = component.PricePlKg != -1 ? Convert.ToDouble(price).ToString("0.00") : "Brak";

                component.SemiProductState = component.IsSemiproduct ? ExpandState.Collapsed : ExpandState.None;
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
            view.Columns.Remove(CURRENCY);
            view.Columns.Remove(RATE);
            view.Columns.Remove(PRICE_ORIGINAL);
            view.Columns.Remove(VOC);
            view.Columns.Remove(SEMIPRODUCT_LEVEL);

            view.Columns[MATERIAL_ID].Visible = false;
            view.Columns[IS_SEMIPRODUCT].Visible = false;
            view.Columns[OPERATION].Visible = false;

            int displayIndex = 0;

            view.Columns[ORDERING].HeaderText = "L.p.";
            view.Columns[ORDERING].DisplayIndex = displayIndex++;
            view.Columns[ORDERING].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[ORDERING].Width = _formData.ContainsKey(ORDERING) ? (int)_formData[ORDERING] : STD_WIDTH;
            view.Columns[ORDERING].ReadOnly = true;
            view.Columns[ORDERING].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[SEMIPRODUCT_STATE].HeaderText = "";
            view.Columns[SEMIPRODUCT_STATE].DisplayIndex = displayIndex++;
            view.Columns[SEMIPRODUCT_STATE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[SEMIPRODUCT_STATE].Width = _formData.ContainsKey(SEMIPRODUCT_STATE) ? (int)_formData[SEMIPRODUCT_STATE] : STD_WIDTH;
            view.Columns[SEMIPRODUCT_STATE].ReadOnly = true;
            view.Columns[SEMIPRODUCT_STATE].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[MATERIAL].HeaderText = "Surowiec";
            view.Columns[MATERIAL].DisplayIndex = displayIndex++;
            view.Columns[MATERIAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[MATERIAL].Width = _formData.ContainsKey(MATERIAL) ? (int)_formData[MATERIAL] : STD_WIDTH;
            view.Columns[MATERIAL].ReadOnly = true;
            view.Columns[MATERIAL].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[AMOUNT].HeaderText = "Ilość [%]";
            view.Columns[AMOUNT].DisplayIndex = displayIndex++;
            view.Columns[AMOUNT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[AMOUNT].Width = _formData.ContainsKey(AMOUNT) ? (int)_formData[AMOUNT] : STD_WIDTH;
            view.Columns[AMOUNT].ReadOnly = true;
            view.Columns[AMOUNT].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[MASS].HeaderText = "Masa [kg]";
            view.Columns[MASS].DisplayIndex = displayIndex++;
            view.Columns[MASS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[MASS].Width = _formData.ContainsKey(MASS) ? (int)_formData[MASS] : STD_WIDTH;
            view.Columns[MASS].ReadOnly = true;
            view.Columns[MASS].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[PRICE_PL_KG].HeaderText = "Cena [kg]";
            view.Columns[PRICE_PL_KG].DisplayIndex = displayIndex++;
            view.Columns[PRICE_PL_KG].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[PRICE_PL_KG].Width = _formData.ContainsKey(PRICE_PL_KG) ? (int)_formData[PRICE_PL_KG] : STD_WIDTH;
            view.Columns[PRICE_PL_KG].ReadOnly = true;
            view.Columns[PRICE_PL_KG].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[PRICE_MASS].HeaderText = "Cena";
            view.Columns[PRICE_MASS].DisplayIndex = displayIndex++;
            view.Columns[PRICE_MASS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[PRICE_MASS].Width = _formData.ContainsKey(PRICE_MASS) ? (int)_formData[PRICE_MASS] : STD_WIDTH;
            view.Columns[PRICE_MASS].ReadOnly = true;
            view.Columns[PRICE_MASS].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[PRICE_CURRENCY].HeaderText = "Cena oryg.";
            view.Columns[PRICE_CURRENCY].DisplayIndex = displayIndex++;
            view.Columns[PRICE_CURRENCY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[PRICE_CURRENCY].Width = _formData.ContainsKey(PRICE_CURRENCY) ? (int)_formData[PRICE_CURRENCY] : STD_WIDTH;
            view.Columns[PRICE_CURRENCY].ReadOnly = true;
            view.Columns[PRICE_CURRENCY].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[VOC_PERCENT].HeaderText = "VOC [%]";
            view.Columns[VOC_PERCENT].DisplayIndex = displayIndex++;
            view.Columns[VOC_PERCENT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[VOC_PERCENT].Width = _formData.ContainsKey(VOC_PERCENT) ? (int)_formData[VOC_PERCENT] : STD_WIDTH;
            view.Columns[VOC_PERCENT].ReadOnly = true;
            view.Columns[VOC_PERCENT].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[VOC_AMOUNT].HeaderText = "VOC [kg]";
            view.Columns[VOC_AMOUNT].DisplayIndex = displayIndex++;
            view.Columns[VOC_AMOUNT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[VOC_AMOUNT].Width = _formData.ContainsKey(VOC_AMOUNT) ? (int)_formData[VOC_AMOUNT] : STD_WIDTH;
            view.Columns[VOC_AMOUNT].ReadOnly = true;
            view.Columns[VOC_AMOUNT].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[COMMENT].HeaderText = "Uwagi";
            view.Columns[COMMENT].DisplayIndex = displayIndex++;
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

        private bool ContainsSemiProduct => _recipe.Where(i => i.SemiProductLevel == 0).Any(i => i.IsSemiproduct);

        #endregion


        #region DataGridView, Combo and other events

        public void RecipeCellFormat(DataGridViewCellFormattingEventArgs e)
        {
            if (_form.GetDgvComposition.Columns[e.ColumnIndex].Name == AMOUNT)
            {
                double value = Convert.ToDouble(e.Value);
                e.Value = value > 10 ? value.ToString("0.00") : value.ToString("0.000");
            }

            if (_form.GetDgvComposition.Columns[e.ColumnIndex].Name == MASS)
            {
                double value = Convert.ToDouble(e.Value);
                e.Value = value >= 100 ? value.ToString("0.0") :
                          value >= 10 ? value.ToString("0.00") :
                          value >= 1 ? value.ToString("0.000") :
                          value.ToString("0.0000");
            }

            if (_form.GetDgvComposition.Columns[e.ColumnIndex].Name == PRICE_PL_KG)
            {
                double value = Convert.ToDouble(e.Value);
                e.Value = value != -1 ? value.ToString("0.00") : "Brak";
            }

            if (_form.GetDgvComposition.Columns[e.ColumnIndex].Name == SEMIPRODUCT_STATE)
            {
                ExpandState state = (ExpandState)e.Value;
                e.Value = state == ExpandState.None ? " " : state == ExpandState.Collapsed ? "[+]" : "[-]";
            }
        }

        public void ChangeColumnWidth()
        {
            if (_form.Init)
                return;

            int bottom = _form.GetDgvComposition.Top + _form.GetDgvComposition.Height + 2;
            int bottomII = bottom + _form.GetLblPricePerKg.Height + 4;
            int top = _form.GetDgvComposition.Top - _form.GetCmbMaterial.Height - 5;
            int left = _form.GetDgvComposition.Left;
            int headerWidth = _form.GetDgvComposition.RowHeadersWidth;
            int orderWidth = _form.GetDgvComposition.Columns[ORDERING].Width;
            int semiWidth = ContainsSemiProduct ? _form.GetDgvComposition.Columns[SEMIPRODUCT_STATE].Width : 0;
            int materialWidth = _form.GetDgvComposition.Columns[MATERIAL].Width;
            int amountWidth = _form.GetDgvComposition.Columns[AMOUNT].Width;
            int massWidth = _form.GetDgvComposition.Columns[MASS].Width;
            int priceWidth = _form.GetDgvComposition.Columns[PRICE_MASS].Width;
            int pricePlWidth = _form.GetDgvComposition.Columns[PRICE_PL_KG].Width;
            int priceCurWidth = _form.GetDgvComposition.Columns[PRICE_CURRENCY].Width;
            int vocWidth = _form.GetDgvComposition.Columns[VOC_PERCENT].Width;
            int vocAmountWidth = _form.GetDgvComposition.Columns[VOC_AMOUNT].Width;
            int commentWidth = _form.GetDgvComposition.Columns[COMMENT].Width;

            _form.GetRadioAmount.Top = top + 2;
            _form.GetRadioAmount.Left = left;
            _form.GetRadioMass.Top = top + 2;
            _form.GetRadioMass.Left = left + _form.GetRadioAmount.Width;

            _form.GetCmbMaterial.Top = top;
            _form.GetCmbMaterial.Left = left + headerWidth + orderWidth + semiWidth;
            _form.GetCmbMaterial.Width = materialWidth;

            _form.GetTxtSetAmount.Top = top;
            _form.GetTxtSetAmount.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + 2;
            _form.GetTxtSetAmount.Width = amountWidth - 1;

            _form.GetTxtSetMass.Top = top;
            _form.GetTxtSetMass.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + 2;
            _form.GetTxtSetMass.Width = massWidth - 1;

            _form.GetTxtComment.Top = top;
            _form.GetTxtComment.Left = _form.GetTxtSetMass.Left + massWidth + priceWidth + pricePlWidth + priceCurWidth + vocWidth + vocAmountWidth;
            _form.GetTxtComment.Width = commentWidth;


            _form.GetLblDensity.Top = bottom;
            _form.GetLblDensity.Left = left;

            _form.GetLblSumText.Top = bottom;
            _form.GetLblSumText.Left = left + headerWidth + orderWidth + semiWidth + materialWidth - (_form.GetLblSumText.Width + 2);
            _form.GetLblMassText.Top = bottomII;
            _form.GetLblMassText.Left = left + headerWidth + orderWidth + semiWidth + materialWidth - (_form.GetLblMassText.Width + 2);

            _form.GetLblSumPrecent.Top = bottom;
            _form.GetLblSumPrecent.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + (Math.Abs(amountWidth - _form.GetLblSumPrecent.Width) / 2);

            _form.GetLblSumMass.Top = bottom;
            _form.GetLblSumMass.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + (Math.Abs(massWidth - _form.GetLblSumMass.Width) / 2);
            _form.GetTxtTotalMass.Top = bottomII;
            _form.GetTxtTotalMass.Width = massWidth;
            _form.GetTxtTotalMass.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth;

            _form.GetLblPricePerKg.Top = bottom;
            _form.GetLblPricePerKg.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + Math.Abs(pricePlWidth - _form.GetLblPricePerKg.Width);
            _form.GetLblPricePerL.Top = bottomII;
            _form.GetLblPricePerL.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + Math.Abs(pricePlWidth - _form.GetLblPricePerL.Width);

            _form.GetLblCalcPricePerKg.Top = bottom;
            _form.GetLblCalcPricePerKg.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + pricePlWidth + (Math.Abs(priceWidth - _form.GetLblCalcPricePerKg.Width) / 2);
            _form.GetLblCalcPricePerL.Top = bottomII;
            _form.GetLblCalcPricePerL.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + pricePlWidth + (Math.Abs(priceWidth - _form.GetLblCalcPricePerL.Width) / 2);

            _form.GetLblVocKg.Top = bottom;
            _form.GetLblVocKg.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + Math.Abs(vocWidth - _form.GetLblVocKg.Width);
            _form.GetLblVocL.Top = bottomII;
            _form.GetLblVocL.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + Math.Abs(vocWidth - _form.GetLblVocL.Width);


            _form.GetLblVocPerKg.Top = bottom;
            _form.GetLblVocPerKg.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + vocWidth + (Math.Abs(vocAmountWidth - _form.GetLblVocPerKg.Width) / 2);
            _form.GetLblVocPerL.Top = bottomII;
            _form.GetLblVocPerL.Left = left + headerWidth + orderWidth + semiWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + vocWidth + (Math.Abs(vocAmountWidth - _form.GetLblVocPerL.Width) / 2);
        }


        public void ChangeCalculationType(RadioButton button)
        {
            if (_form.GetRadioAmount.Checked)
            {
                _form.GetTxtSetAmount.Visible = true;
                _form.GetTxtSetMass.Visible = false;
                _form.GetTxtTotalMass.Enabled = true;
            }
            else
            {
                _form.GetTxtSetAmount.Visible = false;
                _form.GetTxtSetMass.Visible = true;
                _form.GetTxtTotalMass.Enabled = false;
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
