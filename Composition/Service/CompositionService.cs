using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Composition.Forms;
using Laboratorium.Composition.LocalDto;
using Laboratorium.Composition.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Laboratorium.Composition.Service
{
    public class CompositionService : LoadService, IService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string VISIBLE = "Visible";
        private const string VISIBLE_LEVEL = "VisibleLevel";
        private const string LABO_ID = "LaboId";
        private const string ORDERING = "Ordering";
        private const string VERSION = "Version";
        private const string MATERIAL = "Material";
        private const string MATERIAL_ID = "MaterialId";
        private const string PERCENT = "Percent";
        private const string PERCENT_ORIGINAL = "PercentOryginal";
        private const string MASS = "Mass";
        private const string IS_SEMIPRODUCT = "IsSemiproduct";
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
        private const string SUB_PRODUCT_COMPOSITION = "SubProductComposition";

        #endregion

        private const int STD_WIDTH = 100;      // Standard column width in DGV composition
        private const int ABSENCE = -1;
        private const int PARAGRAPH = 25;       // Space between sub levels
        private const int RECTANGLE_SIZE = 13;  // Size of rectangle [+] and [-] - only odd numbers
        private const int LEFT_PADDING = 2;     // Distance from RowHeaders for [+] and [-]
        private const int HEADER_WIDTH = 40;    // RowHeaders width
        private const string FORM_DATA = "CompositionForm";

        private Pen PEN_BLACK_1 = new Pen(new SolidBrush(Color.Black), 1);
        private Pen PEN_BLACK_2 = new Pen(new SolidBrush(Color.Black), 2);
        private Brush BRUSH_WHITE = new SolidBrush(Color.White);
        private readonly IList<string> _dgvCompositionFields = new List<string> { ORDERING, MATERIAL, PERCENT, MASS, COMMENT, PRICE_PL_KG, PRICE_CURRENCY, VOC_PERCENT, VOC_AMOUNT, PRICE_MASS };

        private readonly CompositionForm _form;
        private readonly UserDto _user;
        private readonly SqlConnection _connection;
        private readonly LaboDto _laboDto;

        private readonly IBasicCRUD<CompositionDto> _repository;
        private readonly IBasicCRUD<CompositionHistoryDto> _historyRepository;
        private CompositionHistoryDto _lastVersion;
        private IList<CompositionDto> _recipe;
        private IList<CmbMaterialCompositionDto> _materials;
        private BindingSource _recipeBinding;
        private bool _comboBlock = true;
        private int _id = 1;


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

        private CompositionDto GetCurrent => (_recipeBinding != null && _recipeBinding.Count > 0) ? (CompositionDto)_recipeBinding.Current : null;

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

            PrepareRecipe();
            _recipeBinding = new BindingSource();
            _recipeBinding.DataSource = _recipe;
            _recipeBinding.PositionChanged += RecipeBinding_PositionChanged;

            CompositionRepository repositoryComp = (CompositionRepository)_repository;
            _materials = repositoryComp.GetCmbMaterials();

            PrepareDgvComposition();
            PrepareCmbMaterial();
            RecipeBinding_PositionChanged(null, null);
        }

        public void PrepareRecipe()
        {
            if (_lastVersion.IsNew)
                return;

            IList<CompositionDto> recipe = _repository.GetAllByLaboId(_laboDto.Id);
            _recipe = new List<CompositionDto>();

            foreach (CompositionDto component in recipe)
            {
                component.Id = _id++;
                component.Visible = true;
                component.VisibleLevel = 0;
                _recipe.Add(component);

                if (component.IsSemiproduct)
                {
                    var tmp = FillSemiproductMaterial(component, Percent(_lastVersion.Mass, component.Percent));
                    FillSemiProductData(tmp, component);
                }

                RecalculateAll(component, _lastVersion.Mass, component.Percent);
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
            view.RowHeadersWidth = HEADER_WIDTH;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(ROW_STATE);
            view.Columns.Remove(SEMIPRODUCT_STATE);
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
            view.Columns.Remove(SUB_PRODUCT_COMPOSITION);

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
            view.Columns[ORDERING].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[MATERIAL].HeaderText = "Surowiec";
            view.Columns[MATERIAL].DisplayIndex = displayIndex++;
            view.Columns[MATERIAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[MATERIAL].Width = _formData.ContainsKey(MATERIAL) ? (int)_formData[MATERIAL] : STD_WIDTH;
            view.Columns[MATERIAL].ReadOnly = true;
            view.Columns[MATERIAL].SortMode = DataGridViewColumnSortMode.NotSortable;


            view.Columns[ID].HeaderText = "Lev";
            view.Columns[ID].DisplayIndex = displayIndex++;
            view.Columns[ID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[ID].Width = STD_WIDTH;
            view.Columns[ID].ReadOnly = true;
            view.Columns[ID].SortMode = DataGridViewColumnSortMode.NotSortable;




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
            view.Columns[COMMENT].Width = _formData.ContainsKey(COMMENT) ? (int)_formData[COMMENT] : STD_WIDTH;
            view.Columns[COMMENT].ReadOnly = true;
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

        private void RecipeBinding_PositionChanged(object sender, EventArgs e)
        {
            if (_recipeBinding == null || _recipeBinding.Count == 0)
                return;

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
        }

        private bool ContainsSemiProduct => _recipe.Where(i => i.VisibleLevel == 0).Any(i => i.IsSemiproduct);

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
                current.SemiProductState = current.IsSemiproduct ? ExpandState.Collapsed : ExpandState.None;
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
                current.MaterialId = ABSENCE;
                current.IsSemiproduct = false;
                current.Visible = true;
                current.VisibleLevel = 0;
                current.SemiProductState = ExpandState.None;
                current.VOC = ABSENCE;
                current.PriceOriginal = ABSENCE;
                current.PricePlKg = ABSENCE;
                current.Currency = "Brak";
                current.Rate = 0;
            }

            _recipeBinding.EndEdit();
            _recipeBinding.ResetBindings(false);
            RecalculateAll(current, _lastVersion.Mass, current.Percent);
        }

        #endregion


        #region Semiproduct Operation

        private SemiProductTransferDto FillSemiproductMaterial(CompositionDto semiProduct, double mass)
        {
            bool noPrice = false;
            bool noVOC = false;
            double totalPrice = 0;
            double totalVOC = 0;

            SemiProductTransferDto result = new SemiProductTransferDto();
            IList<CompositionDto> composition = _repository.GetAllByLaboId(semiProduct.MaterialId);

            if (composition.Count == 0) 
                return result;

            int maxId = _recipe.Select(i => i.Id).DefaultIfEmpty().Max();
            maxId++;

            foreach (CompositionDto component in composition) 
            {

                component.Id = maxId++;
                component.Visible = true;
                component.SemiProductState = ExpandState.Collapsed;
                component.VisibleLevel = Convert.ToByte(semiProduct.VisibleLevel + 1);
                component.Operation = semiProduct.Operation;
                component.Percent = Percent(semiProduct.Percent, component.PercentOryginal);
                component.LastPosition = composition.IndexOf(component) == composition.Count - 1;
                component.AcceptChanges();
                _recipe.Add(component);

                if (component.IsSemiproduct)
                {
                    var subSemiProduct = FillSemiproductMaterial(component, Percent(semiProduct.PercentOryginal, mass));

                    FillSemiProductData(subSemiProduct, component);

                    noPrice = subSemiProduct.Price <= 0 && subSemiProduct.SubProductComposition.Count > 0;
                    noVOC = subSemiProduct.VOC < 0 && subSemiProduct.SubProductComposition.Count >= 0;
                }
                else
                {
                    noPrice = component.PricePlKg <= 0 || component.Rate == 0;
                    noVOC = component.VOC < 0;
                }

                totalPrice += Percent(Convert.ToDouble(component.PricePlKg), component.PercentOryginal);
                totalVOC += Percent(Convert.ToDouble(component.VOC), component.PercentOryginal);
                RecalculateAll(component, mass, component.PercentOryginal);
            }

            result.SubProductComposition = composition;
            result.Price = !noPrice ? totalPrice : ABSENCE;
            result.VOC = !noVOC ? totalVOC : ABSENCE;

            return result;
        }

        private void FillSemiProductData(SemiProductTransferDto source, CompositionDto destination)
        {
            destination.SubProductComposition = source.SubProductComposition;
            destination.PriceOriginal = source.Price > 0 ? source.Price : ABSENCE;
            destination.PricePlKg = source.Price > 0 ? source.Price : ABSENCE;
            destination.Currency = "Zł";
            destination.Rate = 1;
            destination.VOC = source.VOC >= 0 ? source.VOC : ABSENCE;
        }

        #endregion


        #region Mathematic

        private void RecalculateAll(CompositionDto component, double mass, double amount)
        {
            component.Mass = Math.Round(mass * (amount / 100), 4);

            double? voc = (amount * component.VOC) / 100;
            component.VocAmount = component.VOC != ABSENCE ? ((Convert.ToDouble(voc) * mass) / 100).ToString("0.00") : "Brak";

            double? price = component.PricePlKg * component.Mass;
            component.PriceMass = component.PricePlKg != ABSENCE ? Convert.ToDouble(price).ToString("0.00") : "Brak";

            component.SemiProductState = component.IsSemiproduct ? ExpandState.Collapsed : ExpandState.None;
        }

        private double Percent(double value, double percent) => value * percent / 100d;

        #endregion


        #region DataGridView, Combo and other events

        private int FrameWidth => _form.GetDgvComposition.Columns[MATERIAL].Width + _form.GetDgvComposition.Columns[PERCENT].Width + _form.GetDgvComposition.Columns[MASS].Width +
                _form.GetDgvComposition.Columns[PRICE_PL_KG].Width + _form.GetDgvComposition.Columns[PRICE_MASS].Width + _form.GetDgvComposition.Columns[PRICE_CURRENCY].Width +
                _form.GetDgvComposition.Columns[VOC_PERCENT].Width + _form.GetDgvComposition.Columns[VOC_AMOUNT].Width + _form.GetDgvComposition.Columns[COMMENT].Width;

        private int FrameX => _form.GetDgvComposition.Columns[ORDERING].Width + _form.GetDgvComposition.RowHeadersWidth;

        public void RecipeCellFormat(DataGridViewCellFormattingEventArgs e)
        {
            string cell = _form.GetDgvComposition.Columns[e.ColumnIndex].Name;
            CompositionDto row = (CompositionDto)_recipeBinding[e.RowIndex];

            double price = Convert.ToDouble(row.PricePlKg);
            double voc = Convert.ToDouble(row.VOC);
            double amount = row.Percent;
            double mass = row.Mass;

            #region Yellow color of background in frame

            if (row.Operation > 1 && cell != ORDERING && cell != SEMIPRODUCT_STATE)
            {
                e.CellStyle.BackColor = Color.Yellow;
            }

            #endregion

            #region Red color of font in Price and VOC

            if (cell == PRICE_MASS || cell == PRICE_PL_KG || cell == PRICE_CURRENCY)
            {
                e.CellStyle.ForeColor = price != ABSENCE ? Color.Black : Color.Red;
                e.CellStyle.Font = price != ABSENCE ? e.CellStyle.Font : new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
            }


            if (cell == VOC_AMOUNT || cell == VOC_PERCENT)
            {
                e.CellStyle.ForeColor = voc != ABSENCE ? Color.Black : Color.Red;
                e.CellStyle.Font = voc != ABSENCE ? e.CellStyle.Font : new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
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
                    e.Value = price != ABSENCE ? price.ToString("0.00") : "Brak";
                    break;
            }

            #endregion

            #region Section format

            if (row.VisibleLevel > 0)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 8, FontStyle.Regular);
                e.CellStyle.ForeColor = Color.Red;
            }

            if (cell == ORDERING)
            {
                int left = PARAGRAPH * (row.VisibleLevel + 1);
                var padding = new Padding(left, 0, 0, 0);
                e.CellStyle.Padding = padding;
            }

            #endregion
        }

        public void RecipeRowsPaint(DataGridViewRowPostPaintEventArgs e)
        {
            CompositionDto row = (CompositionDto)_recipeBinding[e.RowIndex];
            Brush red = new SolidBrush(Color.Red);
            Pen penRed = new Pen(red, 2);
            int distance = 1;

            #region Paint red frame around separate

            int x = FrameX;
            int y = e.RowBounds.Top;
            int width = FrameWidth;
            int height = e.RowBounds.Height;
            byte operation = row.Operation;

            Point top_left = new Point(x + distance, y);
            Point top_right = new Point(x + width - distance, y);
            Point bottom_left = new Point(x + distance, y + height);
            Point bottom_right = new Point(x + width - distance, y + height);

            if (operation == 2)
            {
                e.Graphics.DrawLine(penRed, top_left, top_right);
                e.Graphics.DrawLine(penRed, top_left, bottom_left);
                e.Graphics.DrawLine(penRed, top_right, bottom_right);
            }
            else if (operation == 3)
            {
                e.Graphics.DrawLine(penRed, top_left, bottom_left);
                e.Graphics.DrawLine(penRed, top_right, bottom_right);
            }
            else if (operation == 4)
            {
                e.Graphics.DrawLine(penRed, new Point(x + distance, y + height - distance), new Point(x + width - distance, y + height - distance));
                e.Graphics.DrawLine(penRed, top_left, bottom_left);
                e.Graphics.DrawLine(penRed, top_right, bottom_right);
            }

            #endregion

            #region Paint SemiProduct composition

            if (row.IsSemiproduct && row.VisibleLevel == 0)
            {
                PlusPaint(e, LEFT_PADDING + row.VisibleLevel * PARAGRAPH);
            }
            else if (row.IsSemiproduct && row.VisibleLevel > 0 && !row.LastPosition)
            {
                CrossPaint(e, LEFT_PADDING + (row.VisibleLevel - 1) * PARAGRAPH);
                PlusPaint(e, LEFT_PADDING + row.VisibleLevel * PARAGRAPH);
            }
            else if (row.IsSemiproduct && row.VisibleLevel > 0 && row.LastPosition)
            {
                CornerPaint(e, LEFT_PADDING + (row.VisibleLevel - 1) * PARAGRAPH);
                PlusPaint(e, LEFT_PADDING + row.VisibleLevel * PARAGRAPH);
            }
            else if (row.VisibleLevel > 0 && !row.LastPosition)
            {
                CrossPaint(e, LEFT_PADDING + (row.VisibleLevel - 1) * PARAGRAPH);
            }
            else if (row.VisibleLevel > 0 && row.LastPosition)
            {
                CornerPaint(e, LEFT_PADDING + (row.VisibleLevel - 1) * PARAGRAPH);
            }

            #endregion
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
            int materialWidth = _form.GetDgvComposition.Columns[MATERIAL].Width;
            int amountWidth = _form.GetDgvComposition.Columns[PERCENT].Width;
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
            _form.GetCmbMaterial.Left = left + headerWidth + orderWidth;
            _form.GetCmbMaterial.Width = materialWidth;

            _form.GetTxtSetAmount.Top = top;
            _form.GetTxtSetAmount.Left = left + headerWidth + orderWidth + materialWidth + 2;
            _form.GetTxtSetAmount.Width = amountWidth - 1;

            _form.GetTxtSetMass.Top = top;
            _form.GetTxtSetMass.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + 2;
            _form.GetTxtSetMass.Width = massWidth - 1;

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

            _form.GetLblPricePerKg.Top = bottom;
            _form.GetLblPricePerKg.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + Math.Abs(pricePlWidth - _form.GetLblPricePerKg.Width);
            _form.GetLblPricePerL.Top = bottomII;
            _form.GetLblPricePerL.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + Math.Abs(pricePlWidth - _form.GetLblPricePerL.Width);

            _form.GetLblCalcPricePerKg.Top = bottom;
            _form.GetLblCalcPricePerKg.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth + (Math.Abs(priceWidth - _form.GetLblCalcPricePerKg.Width) / 2);
            _form.GetLblCalcPricePerL.Top = bottomII;
            _form.GetLblCalcPricePerL.Left = left + headerWidth + orderWidth + materialWidth + amountWidth + massWidth + pricePlWidth + (Math.Abs(priceWidth - _form.GetLblCalcPricePerL.Width) / 2);

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

        #endregion


        #region SemiProduct paints function

        private void PlusPaint(DataGridViewRowPostPaintEventArgs e, int left)
        {
            int x = _form.GetDgvComposition.RowHeadersWidth + left;
            int y = e.RowBounds.Top + Math.Abs(e.RowBounds.Height - RECTANGLE_SIZE) / 2;

            Rectangle rectangle = new Rectangle(x, y, RECTANGLE_SIZE, RECTANGLE_SIZE);
            e.Graphics.FillRectangle(BRUSH_WHITE, rectangle);
            e.Graphics.DrawRectangle(PEN_BLACK_1, rectangle);

            Point top_plus_Vert = new Point(1 + x + RECTANGLE_SIZE / 2, y + 3);
            Point bottom_plus_Vert = new Point(1 + x + RECTANGLE_SIZE / 2, y + RECTANGLE_SIZE - 2);
            e.Graphics.DrawLine(PEN_BLACK_2, top_plus_Vert, bottom_plus_Vert);
            Point top_plus_Hor = new Point(x + 3, 1 + y + RECTANGLE_SIZE / 2);
            Point bottom_plus_Hor = new Point(x + RECTANGLE_SIZE - 2, 1+ y + RECTANGLE_SIZE / 2);
            e.Graphics.DrawLine(PEN_BLACK_2, top_plus_Hor, bottom_plus_Hor);

            int distance = PARAGRAPH;
            Point left_line = new Point(x + RECTANGLE_SIZE, 1 + y + RECTANGLE_SIZE / 2);
            Point right_line = new Point(x + distance, 1 + y + RECTANGLE_SIZE / 2);
            e.Graphics.DrawLine(PEN_BLACK_1, left_line, right_line);
        }

        private void MinusPaint(DataGridViewRowPostPaintEventArgs e, int left)
        {
            Brush black = new SolidBrush(Color.Black);
            Pen penBlack_1 = new Pen(black, 1);
            Pen penBlack_2 = new Pen(black, 2);

            int x = _form.GetDgvComposition.RowHeadersWidth + left;
            int y = e.RowBounds.Top + Math.Abs(e.RowBounds.Height - RECTANGLE_SIZE) / 2;

            Rectangle rectangle = new Rectangle(x, y, RECTANGLE_SIZE, RECTANGLE_SIZE);
            e.Graphics.FillRectangle(BRUSH_WHITE, rectangle);
            e.Graphics.DrawRectangle(penBlack_1, rectangle);

            Point top_plus_Hor = new Point(x + 3, 1 + y + RECTANGLE_SIZE / 2);
            Point bottom_plus_Hor = new Point(x + RECTANGLE_SIZE - 2, 1 + y + RECTANGLE_SIZE / 2);
            e.Graphics.DrawLine(penBlack_2, top_plus_Hor, bottom_plus_Hor);

            int distance = PARAGRAPH;
            Point left_line = new Point(x + RECTANGLE_SIZE, 1 + y + RECTANGLE_SIZE / 2);
            Point right_line = new Point(x + distance, 1 + y + RECTANGLE_SIZE / 2);
            e.Graphics.DrawLine(penBlack_1, left_line, right_line);
        }

        private void CrossPaint(DataGridViewRowPostPaintEventArgs e, int left)
        {
            int x = 1 + _form.GetDgvComposition.RowHeadersWidth + left + (RECTANGLE_SIZE / 2);
            int y = e.RowBounds.Top;
            int distance = PARAGRAPH - (RECTANGLE_SIZE / 2);

            Point top = new Point(x, y);
            Point bottom = new Point(x, y + e.RowBounds.Height);
            Point halfTop = new Point(x, y + e.RowBounds.Height / 2);
            Point halfRight = new Point(x + distance, y + e.RowBounds.Height / 2);
            e.Graphics.DrawLine(PEN_BLACK_1, top, bottom);
            e.Graphics.DrawLine(PEN_BLACK_1, halfTop, halfRight);
        }

        private void CornerPaint(DataGridViewRowPostPaintEventArgs e, int left)
        {
            int x = 1 + _form.GetDgvComposition.RowHeadersWidth + left + (RECTANGLE_SIZE / 2);
            int y = e.RowBounds.Top;
            int distance = PARAGRAPH - (RECTANGLE_SIZE / 2);

            Point top = new Point(x, y);
            Point middle = new Point(x, y + e.RowBounds.Height / 2);
            Point end = new Point(x + distance, y + e.RowBounds.Height / 2);
            Point[] points = new Point[] { top, middle, end };
            e.Graphics.DrawLines(PEN_BLACK_1, points);
        }

        #endregion


        #region Buttons

        public void Print()
        {
            throw new NotImplementedException();
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
