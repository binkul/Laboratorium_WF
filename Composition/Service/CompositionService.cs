using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.Composition.Forms;
using Laboratorium.Composition.LocalDto;
using Laboratorium.Composition.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Composition.Service
{
    public class CompositionService : LoadService, IService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string VISIBLE = "Visible";
        private const string VISIBLE_LEVEL = "VisibleLevel";
        private const string EXPAND_STATE = "ExpandStatus";
        private const string SUB_LEVEL = "SubLevel";
        private const string LAST_POSITION = "LastPosition";
        private const string PARENTS = "Parents";
        private const string LABO_ID = "LaboId";
        private const string ORDERING = "Ordering";
        private const string VERSION = "Version";
        private const string MATERIAL = "Material";
        private const string MATERIAL_ID = "MaterialId";
        private const string PERCENT = "Percent";
        private const string PERCENT_ORIGINAL = "PercentOryginal";
        private const string MASS = "Mass";
        private const string IS_SEMIPRODUCT = "IsSemiproduct";
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
        private const string SUB_PRODUCT_COMPOSITION = "SubProductComposition";

        #endregion

        #region Private transfer class

        private class SemiProductTransferDto
        {
            public double? Price { get; set; }
            public double? VOC { get; set; }
            public IList<CompositionDto> SubProductComposition { get; set; } = null;

            public SemiProductTransferDto()
            { }

            public SemiProductTransferDto(double? price, double? voc, IList<CompositionDto> subProductComposition)
            {
                Price = price;
                VOC = voc;
                SubProductComposition = subProductComposition;
            }
        }

        #endregion

        private const int STD_WIDTH = 100;      // Standard column width in DGV composition
        private const int ERROR_CODE = -1;
        private const int START_SPACING = 2;    // Distance from RowHeaders for [+] and [-]
        private const int SUB_SPACING = 25;     // Space between sub levels
        private const int RECTANGLE_SIZE = 14;  // Size of rectangle [+] and [-] - size of PLUS_14 and MINUS_14
        private const int HEADER_WIDTH = 40;
        private Color COLOR_ERROR = Color.Red;
        private Color COLOR_SEMIPROD = Color.Gray;
        private Color COLOR_STD = Color.Black;
        private const string FORM_DATA = "CompositionForm";

        private readonly Image PLUS_14 = Properties.Resources.Dgv_plus;
        private readonly Image MINUS_14 = Properties.Resources.Dgv_minus;
        private readonly Pen PEN_GRID ;
        private readonly Pen PEN_BLACK;
        private readonly Pen PEN_RED;
        private readonly IList<string> _dgvCompositionFields = new List<string> { ORDERING, MATERIAL, PERCENT, MASS, COMMENT, PRICE_PL_KG, PRICE_CURRENCY, VOC_PERCENT, VOC_AMOUNT, PRICE_MASS };

        private readonly CompositionForm _form;
        private readonly UserDto _user;
        private readonly SqlConnection _connection;
        private readonly LaboDto _laboDto;

        private readonly IBasicCRUD<CompositionDto> _repository;
        private readonly IBasicCRUD<CompositionHistoryDto> _historyRepository;
        private CompositionHistoryDto _lastVersion;
        private readonly IList<LaboDto> _laboList;
        private IList<CompositionDto> _recipe;
        private IList<CmbMaterialCompositionDto> _materials;
        private BindingSource _recipeBinding;
        private bool _comboBlock = true;
        private bool _modified = false;  


        public CompositionService(SqlConnection connection, UserDto user, CompositionForm form, LaboDto laboDto, IList<LaboDto> laboList)
            : base(FORM_DATA, form)
        {
            _form = form;
            _connection = connection;
            _user = user;
            _laboDto = laboDto;
            _laboList = laboList;

            PEN_BLACK = new Pen(new SolidBrush(Color.Black), 1);
            PEN_RED = new Pen(new SolidBrush(Color.Red), 2);
            PEN_GRID = new Pen(new SolidBrush(form.GetDgvComposition.GridColor));

            _repository = new CompositionRepository(_connection, this);
            _historyRepository = new CompositionHistoryRepository(_connection);
        }


        #region Modification markers

        private CompositionDto GetCurrent => (_recipeBinding != null && _recipeBinding.Count > 0) ? (CompositionDto)_recipeBinding.Current : null;

        protected override bool Status => _recipe != null && (_recipe.Where(i => i.GetRowState != RowState.UNCHANGED).Any()) | _modified;

        public void Modify(RowState state)
        {
            _form.SaveEnable(Status);
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

            _recipeBinding = new BindingSource();
            _recipe = PrepareRecipe();
            FilterRecipe();

            CompositionRepository repositoryComp = (CompositionRepository)_repository;
            _materials = repositoryComp.GetCmbMaterials();

            PrepareDgvComposition();
            PrepareCmbMaterial();
            RecipeBinding_PositionChanged(null, null);

            _recipeBinding.PositionChanged += RecipeBinding_PositionChanged;
        }

        public IList<CompositionDto> PrepareRecipe()
        {
            if (_lastVersion.IsNew)
                return new List<CompositionDto>();

            return FillRecipe(_laboDto.Id);
        }

        private void PrepareDgvComposition()
        {
            DataGridView view = _form.GetDgvComposition;
            view.DataSource = _recipeBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = COLOR_STD;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.RowHeadersWidth = HEADER_WIDTH;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(ROW_STATE);
            view.Columns.Remove(EXPAND_STATE);
            view.Columns.Remove(CRUD_STATE);
            view.Columns.Remove(LABO_ID);
            view.Columns.Remove(VERSION);
            view.Columns.Remove(PERCENT_ORIGINAL);
            view.Columns.Remove(CURRENCY);
            view.Columns.Remove(RATE);
            view.Columns.Remove(PRICE_ORIGINAL);
            view.Columns.Remove(VOC);
            view.Columns.Remove(VISIBLE_LEVEL);
            view.Columns.Remove(VISIBLE);
            view.Columns.Remove(PARENTS);
            view.Columns.Remove(SUB_PRODUCT_COMPOSITION);
            view.Columns.Remove(SUB_LEVEL);
            view.Columns.Remove(LAST_POSITION);

            view.Columns[ID].Visible = false;
            view.Columns[MATERIAL_ID].Visible = false;
            view.Columns[IS_SEMIPRODUCT].Visible = false;
            view.Columns[OPERATION].Visible = false;

            int displayIndex = 0;

            view.Columns[ORDERING].HeaderText = "L.p.";
            view.Columns[ORDERING].DisplayIndex = displayIndex++;
            view.Columns[ORDERING].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[ORDERING].Width = _formData.ContainsKey(ORDERING) ? (int)_formData[ORDERING] : STD_WIDTH;
            view.Columns[ORDERING].ReadOnly = true;
            view.Columns[ORDERING].Frozen = true;
            view.Columns[ORDERING].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[MATERIAL].HeaderText = "Surowiec";
            view.Columns[MATERIAL].DisplayIndex = displayIndex++;
            view.Columns[MATERIAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[MATERIAL].Width = _formData.ContainsKey(MATERIAL) ? (int)_formData[MATERIAL] : STD_WIDTH;
            view.Columns[MATERIAL].ReadOnly = true;
            view.Columns[MATERIAL].SortMode = DataGridViewColumnSortMode.NotSortable;



            //view.Columns[SUB_LEVEL].HeaderText = "Lev";
            //view.Columns[SUB_LEVEL].DisplayIndex = displayIndex++;
            //view.Columns[SUB_LEVEL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //view.Columns[SUB_LEVEL].Width = STD_WIDTH;
            //view.Columns[SUB_LEVEL].ReadOnly = true;
            //view.Columns[SUB_LEVEL].SortMode = DataGridViewColumnSortMode.NotSortable;




            view.Columns[PERCENT].HeaderText = "Ilość [%]";
            view.Columns[PERCENT].DisplayIndex = displayIndex++;
            view.Columns[PERCENT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[PERCENT].Width = _formData.ContainsKey(PERCENT) ? (int)_formData[PERCENT] : STD_WIDTH;
            view.Columns[PERCENT].ReadOnly = true;
            view.Columns[PERCENT].SortMode = DataGridViewColumnSortMode.NotSortable;

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
            view.Columns[COMMENT].ReadOnly = true;
            view.Columns[COMMENT].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            view.Columns[COMMENT].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void PrepareCmbMaterial()
        {
            _form.GetCmbMaterial.DataSource = _materials;
            _form.GetCmbMaterial.ValueMember = ID;
            _form.GetCmbMaterial.DisplayMember = MATERIAL;

            foreach (CmbMaterialCompositionDto mat in _materials)
            {
                _form.GetCmbMaterial.AutoCompleteCustomSource.Add(mat.Material.ToString());
            }
            _form.GetCmbMaterial.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _form.GetCmbMaterial.AutoCompleteSource = AutoCompleteSource.ListItems;

            _form.GetCmbMaterial.SelectedIndexChanged += GetCmbMaterial_SelectedIndexChanged;
        }

        #endregion


        #region Current/Binkding/Navigation

        private bool IsLast(IList<CompositionDto> list, CompositionDto component) => list.IndexOf(component) == list.Count - 1; 

        private void RecipeBinding_PositionChanged(object sender, EventArgs e)
        {
            if (_recipeBinding == null || _recipeBinding.Count == 0)
                return;

            if (GetCurrent != null && GetCurrent.VisibleLevel > 0)
            {
                BlockControls();
                return;
            }
            else if (!_form.GetCmbMaterial.Enabled)
            {
                UnblockControls();
            }

            #region Combo Materials

            _comboBlock = true;

            if (GetCurrent != null)
            {
                int id = GetCurrent.MaterialId;
                string name = GetCurrent.Material.ToLower();

                CmbMaterialCompositionDto selected = _materials
                    .Where(i => i.MaterialId == id && i.Material.ToLower() == name)
                    .FirstOrDefault();

                if (selected != null)
                {
                    _form.GetCmbMaterial.SelectedValue = selected.Id;                 
                }
                else
                {
                    _form.GetCmbMaterial.Text = name;
                }
            }

            _comboBlock = false;

            #endregion
        }

        private void GetCmbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_comboBlock)
                return;

            CmbMaterialCompositionDto mat = _form.GetCmbMaterial.SelectedItem != null ? (CmbMaterialCompositionDto)_form.GetCmbMaterial.SelectedItem : null;
            CompositionDto current = GetCurrent;

            int maxId = _recipe.Select(i => i.Id).DefaultIfEmpty().Max();
            maxId++;

            if (mat != null && current != null)
            {
                current.Id = maxId;
                current.Material = mat.Material;
                current.MaterialId = mat.MaterialId;
                current.IsSemiproduct = mat.IsSemiproduct;
                current.Visible = true;
                current.VisibleLevel = 0;
                current.ExpandStatus = current.IsSemiproduct ? ExpandState.Collapsed : ExpandState.None;
                current.VOC = mat.VOC;
                current.PriceOriginal = mat.PriceOryg;
                current.PricePlKg = mat.PricePl;
                current.Currency = mat.Currency;
                current.Rate = mat.Rate;
            }
            else
            {
                current.Id = maxId;
                current.Material = _form.GetCmbMaterial.Text;
                current.MaterialId = ERROR_CODE;
                current.IsSemiproduct = false;
                current.Visible = true;
                current.VisibleLevel = 0;
                current.ExpandStatus = ExpandState.None;
                current.VOC = ERROR_CODE;
                current.PriceOriginal = ERROR_CODE;
                current.PricePlKg = ERROR_CODE;
                current.Currency = "Brak";
                current.Rate = 0;
            }

            _recipeBinding.EndEdit();
            _recipeBinding.ResetBindings(false);
            CompleteData(current, _lastVersion.Mass, current.Percent);
        }

        private void BlockControls()
        {
            _form.GetCmbMaterial.Enabled = false;
            _form.GetRadioAmount.Enabled = false;
            _form.GetRadioMass.Enabled = false;
            _form.GetTxtSetAmount.Enabled = false;
            _form.GetTxtSetMass.Enabled = false;
            _form.GetBtnExchange.Enabled = false;
            _form.GetBtnDelete.Enabled = false;
            _form.GetBtnUp.Enabled = false;
            _form.GetBtnDown.Enabled = false;
            _form.GetBtnFrameDown.Enabled = false;
            _form.GetBtnFrameUp.Enabled = false;
            _form.GetBtnCut.Enabled = false;
            _form.GetBtnUp100.Enabled = false;
            _form.GetBtnAddInside.Enabled = false;
        }

        private void UnblockControls()
        {
            _form.GetCmbMaterial.Enabled = true;
            _form.GetRadioAmount.Enabled = true;
            _form.GetRadioMass.Enabled = true;
            _form.GetTxtSetAmount.Enabled = true;
            _form.GetTxtSetMass.Enabled = true;
            _form.GetBtnExchange.Enabled = true;
            _form.GetBtnDelete.Enabled = true;
            _form.GetBtnUp.Enabled = true;
            _form.GetBtnDown.Enabled = true;
            _form.GetBtnFrameDown.Enabled = true;
            _form.GetBtnFrameUp.Enabled = true;
            _form.GetBtnCut.Enabled = true;
            _form.GetBtnUp100.Enabled = true;
            _form.GetBtnAddInside.Enabled = true;
        }

        #endregion


        #region Recipe Operation

        private IList<CompositionDto> FillRecipe(int laboId)
        {
            IList<CompositionDto> recipe = _repository.GetAllByLaboId(laboId);
            IList<CompositionDto> finalRecipe = new List<CompositionDto>();

            foreach (CompositionDto component in recipe)
            {
                component.Id = finalRecipe.Select(i => i.Id).DefaultIfEmpty().Max() + 1;
                component.LaboId = _laboDto.Id;
                component.Version = _lastVersion.Version;
                component.Visible = true;
                component.VisibleLevel = 0;
                finalRecipe.Add(component);

                if (component.IsSemiproduct)
                {
                    component.ExpandStatus = ExpandState.Collapsed;
                    var tmp = FillSemiproductRecipe(finalRecipe, component, CommonFunction.Percent(_lastVersion.Mass, component.Percent), 0, -1);
                    FillSemiProductData(tmp, component);
                }

                CompleteData(component, _lastVersion.Mass, component.Percent);
            }

            return finalRecipe;
        }

        private void FilterRecipe()
        {
            IList<CompositionDto> recipe = _recipe
                .Where(i => i.Visible)
                .ToList();

            _recipeBinding.DataSource = recipe;
        }

        private void ResetRecipe()
        {
            for (int i = 0; i < _recipe.Count; i++)
            {
                _recipe[i] = null;
            }
            _recipe = null;
        }

        private void DeleteRow(CompositionDto row)
        {
            if (!row.IsSemiproduct)
            {
                _recipe.Remove(row);
                return;
            }
            IList<CompositionDto> removeList = new List<CompositionDto>();
            foreach(CompositionDto compound in _recipe)
            {
                if (compound.Parents == null || compound.Parents.Count == 0)
                    continue;

                if (compound.Parents[0] == row.Id)
                    removeList.Add(compound);
            }

            _recipe.Remove(row);
            foreach(CompositionDto compound in removeList)
            {
                _recipe.Remove(compound);
            }

            removeList = null;
        }

        private void RecalculateOrdering()
        {
            short ordering = 1;
            for (int i = 0; i < _recipe.Count; i++)
            {
                if (_recipe[i].VisibleLevel == 0)
                    _recipe[i].Ordering = ordering++;
            }
        }

        #endregion


        #region Semiproduct Operation

        private SemiProductTransferDto FillSemiproductRecipe(IList<CompositionDto> recipe, CompositionDto semiProduct, double mass, int subLevel, int position)
        {
            SemiProductSumDto sum = new SemiProductSumDto();
            SemiProductTransferDto result = new SemiProductTransferDto();
            IList<CompositionDto> composition = _repository.GetAllByLaboId(semiProduct.MaterialId);

            if (composition.Count == 0) 
                return result;
            
            for (int i = 0; i < composition.Count; i++)
            {
                CompositionDto component = composition[i];

                FillParents(semiProduct.Parents, component.Parents);
                component.Id = recipe.Select(x => x.Id).DefaultIfEmpty().Max() + 1;
                component.Visible = false;
                component.SubLevel = subLevel;
                component.AddParent(semiProduct.Id);
                component.VisibleLevel = Convert.ToByte(semiProduct.VisibleLevel + 1);
                component.Operation = semiProduct.Operation;
                component.Percent = CommonFunction.Percent(semiProduct.Percent, component.PercentOryginal);
                component.LastPosition = composition.IndexOf(component) == composition.Count - 1;
                component.AcceptChanges();

                if (position < 1)
                    recipe.Add(component);
                else
                {
                    recipe.Insert(position++, component);
                }

                if (component.IsSemiproduct)
                {
                    component.ExpandStatus = ExpandState.Collapsed;
                    int level = IsLast(composition, component) ? 0 : subLevel + 1;
                    double massPercent = CommonFunction.Percent(semiProduct.PercentOryginal, mass);

                    SemiProductTransferDto subSemiProduct = FillSemiproductRecipe(recipe, component, massPercent, level, position);
                    FillSemiProductData(subSemiProduct, component);

                    sum.AddPriceOk(subSemiProduct.Price > 0 && subSemiProduct.SubProductComposition.Count > 0);
                    sum.AddVocOk(subSemiProduct.VOC >= 0 && subSemiProduct.SubProductComposition.Count > 0);
                }
                else
                {
                    sum.AddPriceOk(component.PricePlKg > 0 && component.Rate != 0);
                    sum.AddVocOk(component.VOC >= 0);
                }

                sum.SumPrice(component.PricePlKg, component.PercentOryginal);
                sum.SumVOC(component.VOC, component.PercentOryginal);
                CompleteData(component, mass, component.PercentOryginal);
            }

            result.SubProductComposition = composition;
            result.Price = sum.GetPrice();
            result.VOC = sum.GetVOC();

            return result;
        }

        private void FillSemiProductData(SemiProductTransferDto source, CompositionDto destination)
        {
            destination.SubProductComposition = source.SubProductComposition;
            destination.PriceOriginal = source.Price > 0 ? source.Price : ERROR_CODE;
            destination.PricePlKg = source.Price > 0 ? source.Price : ERROR_CODE;
            destination.Currency = "Zł";
            destination.Rate = 1;
            destination.VOC = source.VOC >= 0 ? source.VOC : ERROR_CODE;
        }

        private void FillParents(IList<int> source, IList<int> destination)
        {
            for (int i = 0; i < source.Count; i++)
            {
                destination.Add(source[i]);
            }
        }

        private void ShowAndHideSemiProductRecipe(CompositionDto compound)
        {
            if (!compound.IsSemiproduct)
                return;

            switch (compound.ExpandStatus)
            {
                case ExpandState.Collapsed:
                    compound.ExpandStatus = ExpandState.Expanded;
                    ShowCompounds(compound.SubProductComposition);
                    break;
                case ExpandState.Expanded:
                    compound.ExpandStatus = ExpandState.Collapsed;
                    HideCompounds(compound.SubProductComposition);
                    break;
                default:
                    return;
            }
        }

        private void ShowCompounds(IList<CompositionDto> compositions)
        {
            if (compositions == null || compositions.Count == 0)
                return;

            foreach (CompositionDto subCompound in compositions)
            {
                subCompound.Visible = true;
            }
        }

        private void HideCompounds(IList<CompositionDto> compositions)
        {
            if (compositions == null || compositions.Count == 0)
                return;

            foreach (CompositionDto subCompound in compositions)
            {
                subCompound.Visible = false;
                if (subCompound.IsSemiproduct)
                {
                    subCompound.ExpandStatus = ExpandState.Collapsed;
                    HideCompounds(subCompound.SubProductComposition);
                }
            }
        }

        #endregion


        #region Mathematic

        private void CompleteData(CompositionDto component, double mass, double amount)
        {
            component.Mass = Math.Round(mass * (amount / 100), 4);

            double? voc = (amount * component.VOC) / 100;
            component.VocAmount = component.VOC != ERROR_CODE ? ((Convert.ToDouble(voc) * mass) / 100).ToString("0.00") : "Brak";

            double? price = component.PricePlKg * component.Mass;
            component.PriceMass = component.PricePlKg != ERROR_CODE ? Convert.ToDouble(price).ToString("0.00") : "Brak";

            component.ExpandStatus = component.IsSemiproduct ? ExpandState.Collapsed : ExpandState.None;
        }

        #endregion


        #region DataGridView, Combo and other events

        public void DgvCellFormat(DataGridViewCellFormattingEventArgs e)
        {
            string cell = _form.GetDgvComposition.Columns[e.ColumnIndex].Name;
            CompositionDto row = (CompositionDto)_recipeBinding[e.RowIndex];

            Font FONT_BOLD_10 = new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
            Font FONT_BOLD_08 = new Font(e.CellStyle.Font.Name, 8, FontStyle.Bold);
            Font FONT_REG_08 = new Font(e.CellStyle.Font.Name, 8, FontStyle.Regular);

            double price = Convert.ToDouble(row.PricePlKg);
            double voc = Convert.ToDouble(row.VOC);
            double amount = row.Percent;
            double mass = row.Mass;

            #region Yellow color of background in frame

            if (row.Operation > 1 && cell != ORDERING && cell != MATERIAL)
            {
                e.CellStyle.BackColor = Color.Yellow;
            }

            #endregion

            #region Semi Product format

            if (row.VisibleLevel > 0)
            {
                e.CellStyle.ForeColor = COLOR_SEMIPROD;
                e.CellStyle.Font = FONT_REG_08;
            }

            #endregion

            #region Red color of font in Price and VOC

            if (cell == PRICE_MASS || cell == PRICE_PL_KG || cell == PRICE_CURRENCY)
            {
                if (row.VisibleLevel == 0)
                {
                    e.CellStyle.ForeColor = price != ERROR_CODE ? COLOR_STD : (row.IsSemiproduct && cell != PRICE_MASS && cell != PRICE_PL_KG) ? COLOR_STD : COLOR_ERROR;
                    e.CellStyle.Font = price != ERROR_CODE ? e.CellStyle.Font : FONT_BOLD_10;
                }
                else
                {
                    e.CellStyle.ForeColor = price != ERROR_CODE ? COLOR_SEMIPROD : (row.IsSemiproduct && cell != PRICE_MASS && cell != PRICE_PL_KG) ? COLOR_SEMIPROD : COLOR_ERROR; ;
                }
            }


            if (cell == VOC_AMOUNT || cell == VOC_PERCENT)
            {
                if (row.VisibleLevel == 0)
                {
                    e.CellStyle.ForeColor = voc != ERROR_CODE ? COLOR_STD : COLOR_ERROR;
                    e.CellStyle.Font = voc != ERROR_CODE ? e.CellStyle.Font : FONT_BOLD_10;
                }
                else
                {
                    e.CellStyle.ForeColor = voc != ERROR_CODE ? COLOR_SEMIPROD : COLOR_ERROR;
                }
            }

            #endregion

            #region Numbers format

            switch (cell)
            {
                case PERCENT:
                    e.Value = amount > 10 ? amount.ToString("0.00") : amount.ToString("0.000");
                    break;
                case MASS:
                    e.Value = mass >= 100 ? mass.ToString("0.0") :
                              mass >= 10 ? mass.ToString("0.00") :
                              mass >= 1 ? mass.ToString("0.000") :
                              mass.ToString("0.0000");
                    break;
                case PRICE_PL_KG:
                    e.Value = price != ERROR_CODE ? price.ToString("0.00") : "Brak";
                    break;
            }

            #endregion
        }

        public void DgvCellPaint(DataGridView view, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || _recipeBinding.Count == 0)
                return;

            bool isHeader = e.ColumnIndex < 0;
            bool isOrdering = view.Columns[ORDERING].Index == e.ColumnIndex;
            bool isMaterial = view.Columns[MATERIAL].Index == e.ColumnIndex;
            bool isComment = view.Columns[COMMENT].Index == e.ColumnIndex;

            int x = e.CellBounds.Left;
            int y = e.CellBounds.Top;
            int x_end = e.CellBounds.Right;
            int y_end = e.CellBounds.Bottom;

            CompositionDto row = (CompositionDto)_recipeBinding[e.RowIndex];

            #region Column Orgering + SemiProduct Expand

            if (isOrdering)
            {
                e.PaintBackground(e.CellBounds, true);
                PaintStandardCellBorder(e, x, x_end, y, y_end);

                int left = START_SPACING + row.VisibleLevel * SUB_SPACING;
                int left_minus = START_SPACING + (row.VisibleLevel - 1) * SUB_SPACING;

                if (row.IsSemiproduct & row.VisibleLevel == 0 && row.ExpandStatus == ExpandState.Collapsed)
                {
                    SmallLinePaint(e.Graphics, e.CellBounds, left);
                    PlusPaint(e.Graphics, e.CellBounds, left);
                }
                else if (row.IsSemiproduct & row.VisibleLevel == 0 && row.ExpandStatus == ExpandState.Expanded)
                {
                    SmallLinePaint(e.Graphics, e.CellBounds, left);
                    MinusPaint(e.Graphics, e.CellBounds, left);
                }
                else if (row.IsSemiproduct & row.VisibleLevel > 0 && !row.LastPosition && row.ExpandStatus == ExpandState.Collapsed)
                {
                    CrossPaint(e.Graphics, e.CellBounds, left_minus, row.SubLevel);
                    PlusPaint(e.Graphics, e.CellBounds, left);
                }
                else if (row.IsSemiproduct & row.VisibleLevel > 0 && !row.LastPosition && row.ExpandStatus == ExpandState.Expanded)
                {
                    SmallLinePaint(e.Graphics, e.CellBounds, left);
                    CrossPaint(e.Graphics, e.CellBounds, left_minus, row.SubLevel);
                    MinusPaint(e.Graphics, e.CellBounds, left);
                }
                else if (row.IsSemiproduct & row.VisibleLevel > 0 && row.LastPosition && row.ExpandStatus == ExpandState.Collapsed)
                {
                    CornerPaint(e.Graphics, e.CellBounds, left_minus, row.SubLevel);
                    PlusPaint(e.Graphics, e.CellBounds, left);
                }
                else if (row.IsSemiproduct & row.VisibleLevel > 0 && row.LastPosition && row.ExpandStatus == ExpandState.Expanded)
                {
                    SmallLinePaint(e.Graphics, e.CellBounds, left);
                    CornerPaint(e.Graphics, e.CellBounds, left_minus, row.SubLevel);
                    MinusPaint(e.Graphics, e.CellBounds, left);
                }
                else if (row.VisibleLevel > 0 && !row.LastPosition)
                {
                    CrossPaint(e.Graphics, e.CellBounds, left_minus, row.SubLevel);
                }
                else if (row.VisibleLevel > 0 && row.LastPosition)
                {
                    CornerPaint(e.Graphics, e.CellBounds, left_minus, row.SubLevel);
                }

                if (e.Value != null)
                {
                    float textSize = e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
                    textSize /= 2;
                    textSize = (e.CellBounds.Height / 2 - textSize);


                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black,
                        e.CellBounds.X + START_SPACING + (row.VisibleLevel + 1) * SUB_SPACING,
                        e.CellBounds.Y + textSize, StringFormat.GenericDefault);
                }

                e.Handled = true;
            }

            #endregion

            #region Column Material

            if (isMaterial && row.Operation > 1)
            {
                PaintCellBackGround(e);
                PaintStandardCellBorder(e, x, x_end, y, y_end);

                e.Graphics.DrawLine(PEN_RED, x + 1, y, x + 1, y_end);
                PaintRedTopBottomCellBorder(e, row.Operation, x, x_end, y, y_end);

                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }

            #endregion

            #region Column Comment

            if (isComment && row.Operation > 1)
            {
                PaintCellBackGround(e);
                PaintStandardCellBorder(e, x, x_end, y, y_end);

                e.Graphics.DrawLine(PEN_RED, x_end - 1, y, x_end - 1, y_end);
                PaintRedTopBottomCellBorder(e, row.Operation, x, x_end, y, y_end);
                
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }

            #endregion

            #region Columns between Material and Comment

            if (!isHeader && !isOrdering && !isMaterial && !isComment && row.Operation > 1)
            {
                PaintCellBackGround(e);
                PaintStandardCellBorder(e, x, x_end, y, y_end);
                PaintRedTopBottomCellBorder(e, row.Operation, x, x_end, y, y_end);

                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }

            #endregion
        }

        private void PaintStandardCellBorder(DataGridViewCellPaintingEventArgs e, int x, int x_end, int y, int y_end)
        {
            e.Graphics.DrawLine(PEN_GRID, x, y_end - 1, x_end - 1, y_end - 1);
            e.Graphics.DrawLine(PEN_GRID, x_end - 1, y, x_end - 1, y_end);
        }

        private void PaintRedTopBottomCellBorder(DataGridViewCellPaintingEventArgs e, byte operation, int x, int x_end, int y, int y_end)
        {
            switch (operation)
            {
                case 2:
                    e.Graphics.DrawLine(PEN_RED, x, y + 1, x_end, y + 1);
                    break;
                case 4:
                    e.Graphics.DrawLine(PEN_RED, x, y_end - 2, x_end, y_end - 2);
                    break;
                default:

                    break;
            }
        }

        private void PaintCellBackGround(DataGridViewCellPaintingEventArgs e)
        {
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                e.PaintBackground(e.CellBounds, true);
            else
                e.Graphics.FillRectangle(Brushes.Yellow, e.CellBounds);
        }

        public void DgvChangeColumnWidth()
        {
            if (_form.Init)
                return;

            int bottom = _form.GetDgvComposition.Top + _form.GetDgvComposition.Height + 2;
            int bottomII = bottom + _form.GetLblVocL.Height + 4;
            int top = _form.GetDgvComposition.Top - _form.GetCmbMaterial.Height - 5;
            int left = _form.GetDgvComposition.Left;
            int headerWidth = _form.GetDgvComposition.RowHeadersWidth;
            int orderWidth = _form.GetDgvComposition.Columns[ORDERING].Width;
            int materialWidth = _form.GetDgvComposition.Columns[MATERIAL].Width;
            int amountWidth = _form.GetDgvComposition.Columns[PERCENT].Width;
            int massWidth = _form.GetDgvComposition.Columns[MASS].Width;
            int priceWidth = _form.GetDgvComposition.Columns[PRICE_MASS].Width;
            int pricePlWidth = _form.GetDgvComposition.Columns[PRICE_PL_KG].Width;
            int priceCurWidth = _form.GetDgvComposition.Columns[PRICE_CURRENCY].Width;
            int vocWidth = _form.GetDgvComposition.Columns[VOC_PERCENT].Width;
            int vocAmountWidth = _form.GetDgvComposition.Columns[VOC_AMOUNT].Width;
            int commentWidth = _form.GetDgvComposition.Columns[COMMENT].Width;


            _form.GetCmbMaterial.Top = top;
            _form.GetCmbMaterial.Left = left + headerWidth + orderWidth;
            _form.GetCmbMaterial.Width = materialWidth;

            _form.GetTxtSetAmount.Top = top;
            _form.GetTxtSetAmount.Left = left + headerWidth + orderWidth + materialWidth + 2;
            _form.GetTxtSetAmount.Width = amountWidth - 1;

            _form.GetTxtSetMass.Top = top;
            _form.GetTxtSetMass.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + 2;
            _form.GetTxtSetMass.Width = massWidth - 1;

            _form.GetRadioAmount.Top = top + 2;
            _form.GetRadioAmount.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + 10;
            _form.GetRadioMass.Top = top + 2;
            _form.GetRadioMass.Left = _form.GetRadioAmount.Left + _form.GetRadioAmount.Width;

            _form.GetTxtComment.Top = top;
            _form.GetTxtComment.Left = _form.GetTxtSetMass.Left + massWidth + priceWidth + pricePlWidth + priceCurWidth + vocWidth + vocAmountWidth;
            _form.GetTxtComment.Width = commentWidth;


            _form.GetLblDensity.Top = bottom;
            _form.GetLblDensity.Left = left;

            _form.GetLblSumText.Top = bottom;
            _form.GetLblSumText.Left = left + headerWidth + orderWidth + materialWidth - (_form.GetLblSumText.Width + 2);
            _form.GetLblMassText.Top = bottomII;
            _form.GetLblMassText.Left = left + headerWidth + orderWidth + materialWidth - (_form.GetLblMassText.Width + 2);

            _form.GetLblSumPrecent.Top = bottom;
            _form.GetLblSumPrecent.Left = left + headerWidth + orderWidth + materialWidth + (Math.Abs(amountWidth - _form.GetLblSumPrecent.Width) / 2);

            _form.GetLblSumMass.Top = bottom;
            _form.GetLblSumMass.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + (Math.Abs(massWidth - _form.GetLblSumMass.Width) / 2);
            _form.GetTxtTotalMass.Top = bottomII;
            _form.GetTxtTotalMass.Width = massWidth;
            _form.GetTxtTotalMass.Left = left + headerWidth + orderWidth + materialWidth + amountWidth;

            _form.GetLblCalcPricePerKg.Top = bottom;
            _form.GetLblCalcPricePerKg.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth;
            _form.GetLblCalcPricePerL.Top = bottomII;
            _form.GetLblCalcPricePerL.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth;

            _form.GetLblVocKg.Top = bottom;
            _form.GetLblVocKg.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + Math.Abs(vocWidth - _form.GetLblVocKg.Width);
            _form.GetLblVocL.Top = bottomII;
            _form.GetLblVocL.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + Math.Abs(vocWidth - _form.GetLblVocL.Width);


            _form.GetLblVocPerKg.Top = bottom;
            _form.GetLblVocPerKg.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + vocWidth + (Math.Abs(vocAmountWidth - _form.GetLblVocPerKg.Width) / 2);
            _form.GetLblVocPerL.Top = bottomII;
            _form.GetLblVocPerL.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth + priceWidth + priceCurWidth + vocWidth + (Math.Abs(vocAmountWidth - _form.GetLblVocPerL.Width) / 2);

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

        public void DgvCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string column = _form.GetDgvComposition.Columns[e.ColumnIndex].Name;
            CompositionDto compound = (CompositionDto)_recipeBinding[e.RowIndex];

            if (!column.Equals(ORDERING) || !compound.IsSemiproduct)
                return;

            int height = _form.GetDgvComposition.Rows[e.RowIndex].Height;
            int x_start = START_SPACING + compound.VisibleLevel * SUB_SPACING;
            int x_end = x_start + RECTANGLE_SIZE;
            int y_start = Math.Abs(height - RECTANGLE_SIZE) / 2;
            int y_end = y_start + RECTANGLE_SIZE;

            if (e.X > x_start && e.X < x_end && e.Y > y_start && e.Y < y_end)
            {
                ShowAndHideSemiProductRecipe(compound);
            }

            _recipeBinding.ResetBindings(false);
            FilterRecipe();
        }

        public void DgvCellToolTipTextNeeded(DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex == _form.GetDgvComposition.Columns[MATERIAL].Index)
            {
                CompositionDto row = (CompositionDto)_recipeBinding[e.RowIndex];
                if (row.IsSemiproduct)
                {
                    e.ToolTipText = "Półprodukt D" + row.MaterialId.ToString();
                }
                else
                {
                    e.ToolTipText = "Surowiec: " + row.Material.ToString();
                }
            }
        }

        #endregion


        #region SemiProduct paints method

            /// <summary>
            /// paint on current line: '-' after [+] and [-]
            /// </summary>
            /// <param name="height"></param>
            /// <param name="gr"></param>
            /// <param name="left"></param>
            private void SmallLinePaint(Graphics g, Rectangle rec, int left)
        {
            int x_start = rec.X + left;
            int x_end = x_start + SUB_SPACING;
            int y = rec.Y + rec.Height / 2;
            g.DrawLine(PEN_BLACK, x_start, y, x_end, y);
        }

        /// <summary>
        /// paint on current line: '[+]'
        /// </summary>
        /// <param name="e"></param>
        /// <param name="left"></param>
        private void PlusPaint(Graphics g, Rectangle rec, int left)
        {
            int x = rec.X + left;
            int y = rec.Y + Math.Abs(rec.Height - RECTANGLE_SIZE) / 2;
            int size = RECTANGLE_SIZE;
            g.DrawImage(PLUS_14, x, y, size, size);
        }

        /// <summary>
        /// paint on current line: '[-]'
        /// </summary>
        /// <param name="e"></param>
        /// <param name="left"></param>
        private void MinusPaint(Graphics g, Rectangle rec, int left)
        {
            int x = rec.X + left;
            int y = rec.Y + Math.Abs(rec.Height - RECTANGLE_SIZE) / 2;
            int size = RECTANGLE_SIZE;
            g.DrawImage(MINUS_14, x, y, size, size);
        }

        /// <summary>
        /// paint on current line: '|-'
        /// </summary>
        /// <param name="e"></param>
        /// <param name="left"></param>
        /// <param name="vertLines"></param>
        private void CrossPaint(Graphics g, Rectangle rec, int left, int vertLines)
        {
            int x = rec.X + left + (RECTANGLE_SIZE / 2);
            int y = rec.Y;
            int distance = SUB_SPACING - (RECTANGLE_SIZE / 2);

            Point top = new Point(x, y);
            Point bottom = new Point(x, y + rec.Height);
            g.DrawLine(PEN_BLACK, top, bottom);

            Point halfTop = new Point(x, y + rec.Height / 2);
            Point halfRight = new Point(x + distance, y + rec.Height / 2);
            g.DrawLine(PEN_BLACK, halfTop, halfRight);

            VerticalLinePaint(g, rec, x, vertLines);
        }

        /// <summary>
        /// paint on current line: '|_'
        /// </summary>
        /// <param name="e"></param>
        /// <param name="left"></param>
        /// <param name="vertLines"></param>
        private void CornerPaint(Graphics g, Rectangle rec, int left, int vertLines)
        {
            int x = rec.X + left + (RECTANGLE_SIZE / 2);
            int y = rec.Y;
            int distance = SUB_SPACING - (RECTANGLE_SIZE / 2);

            Point top = new Point(x, y);
            Point middle = new Point(x, y + rec.Height / 2);
            Point end = new Point(x + distance, y + rec.Height / 2);
            Point[] points = new Point[] { top, middle, end };
            g.DrawLines(PEN_BLACK, points);

            VerticalLinePaint(g, rec, x, vertLines);
        }

        /// <summary>
        /// paint on current line: '|' vertLines times
        /// </summary>
        /// <param name="e"></param>
        /// <param name="left"></param>
        /// <param name="vertLines"></param>
        private void VerticalLinePaint(Graphics g, Rectangle rec, int left, int vertLines)
        {
            int x = left - SUB_SPACING;
            int y = rec.Y;
            for (int i = 0; i < vertLines; i++)
            {
                Point top = new Point(x, y);
                Point bottom = new Point(x, y + rec.Height);
                g.DrawLine(PEN_BLACK, top, bottom);
                x -= SUB_SPACING;
            }
        }

        #endregion


        #region Buttons

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void LoadExistingRecipen()
        {
            using (InsertRecipeForm form = new InsertRecipeForm(_laboList))
            {
                form.ShowDialog();

                if (form.Ok && form.Result != null && form.Result != _laboDto)
                {
                    ResetRecipe();
                    _recipe = FillRecipe(form.Result.Id);
                    FilterRecipe();

                    _modified = true;
                    Modify(RowState.MODIFIED);
                }
            }
        }

        public void InsertExistingRecipe()
        {
            if (_recipeBinding == null || _recipeBinding.Count == 0)
                return;

            var current = (CompositionDto)_recipeBinding.Current;
            var row = _recipe.FirstOrDefault(i => i.Id == current.Id);
            int position = _recipe.IndexOf(row);

            using (InsertRecipeForm form = new InsertRecipeForm(_laboList))
            {
                form.ShowDialog();

                if (form.Ok && form.Result != null)
                {
                    IList<CompositionDto> insertRecipe = FillRecipe(form.Result.Id);

                    if (insertRecipe.Count == 0)
                    {
                        return;
                    }
                    else if (insertRecipe.Count == 1)
                    {
                        CompositionDto newItem = insertRecipe[0];
                        newItem.Id = _recipe.Select(x => x.Id).DefaultIfEmpty().Max() + 1;
                        newItem.LastPosition = row.LastPosition;
                        newItem.Percent = row.Percent;
                        newItem.Operation = row.Operation;

                        CompleteData(newItem, _lastVersion.Mass, row.Percent);
                        _recipe.Insert(position, newItem);
                        DeleteRow(row);
                        RecalculateOrdering();
                        FilterRecipe();

                        _modified = true;
                        Modify(RowState.MODIFIED);
                    }
                    else
                    {

                    }
                }
            }

            row = null;
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
