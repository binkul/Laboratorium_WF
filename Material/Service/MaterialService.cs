using Laboratorium.ADO;
using Laboratorium.ADO.DTO;
using Laboratorium.ADO.Repository;
using Laboratorium.ADO.Service;
using Laboratorium.Commons;
using Laboratorium.Currency.Forms;
using Laboratorium.Currency.Repository;
using Laboratorium.Material.Dto;
using Laboratorium.Material.Forms;
using Laboratorium.Material.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laboratorium.Material.Service
{
    public class MaterialService : LoadService, IService
    {
        #region DTO-s fields for DGV column

        private const string ID = "Id";
        private const string NAME_PL = "NamePl";
        private const string INDEX = "Index";
        private const string SUPPLIER_ID = "SupplierId";
        private const string FUNCTION_ID = "FunctionId";
        private const string CURRENCY_ID = "CurrencyId";
        private const string MATERIAL_ID = "MaterialId";
        private const string UNIT_ID = "UnitId";
        private const string CODE_ID = "CodeId";
        private const string TYPE = "Type";
        private const string IS_INTER = "IsIntermediate";
        private const string IS_OBSERVED = "IsObserved";
        private const string IS_PACKAGE = "IsPackage";
        private const string IS_ACTIVE = "IsActive";
        private const string IS_DANGER = "IsDanger";
        private const string IS_PRODUCTION = "IsProduction";
        private const string NAME = "Name";
        private const string PRICE = "Price";
        private const string QUANTITY = "Quantity";
        private const string PRICE_UNIT = "PriceUnit";
        private const string PRICE_QUANTITY = "PricePerQuantity";
        private const string PRICE_TRANSPORT = "PriceTransport";
        private const string VOC_PROC = "VocPercent";
        private const string DATE_UPDATE = "DateUpdated";
        private const string DATE_CREATE = "DateCreated";
        private const string CURRENCY = "Currency";
        private const string DENSITY = "Density";
        private const string SOLIDS = "Solids";
        private const string ASH = "Ash450";
        private const string VOC = "VOC";
        private const string REMARKS = "Remarks";
        private const string ROW_STATE = "GetRowState";
        private const string SERVICE = "Service";
        private const string CRUD_STATE = "CrudState";
        private const string GHS_LIST = "GhsCodeList";
        private const string HP_LIST = "HPcodeList";
        private const string SIGNAL_WORD = "SignalWord";
        private const string ORDERING = "Ordering";
        private const string CLASS_CLP = "ClassClp";
        private const string CODE_CLP = "CodeClp";
        private const string DESCRIPTION_CLP = "DescriptionClp";

        #endregion

        private const string ADMIN = "admin";
        private readonly IList<string> _dgvMaterialFields = new List<string> { NAME, IS_ACTIVE, IS_DANGER, IS_PRODUCTION, PRICE, PRICE_UNIT, VOC_PROC, DATE_UPDATE };
        private readonly IList<string> _dgvClpFields = new List<string> { CLASS_CLP, CODE_CLP, DESCRIPTION_CLP };
        private const string FORM_DATA = "MaterialForm";
        private const int STD_WIDTH = 100;

        private bool _filterBlock = false;
        private bool _cmbBlock = false;
        private readonly UserDto _user;
        private readonly MaterialForm _form;
        private readonly SqlConnection _connection;
        private readonly IBasicCRUD<MaterialClpGhsDto> _ghsRepository;
        private readonly IBasicCRUD<MaterialClpPCodeDto> _pCodeRepository;
        private readonly IBasicCRUD<MaterialClpHCodeDto> _hCodeRepository;
        private readonly IBasicCRUD<MaterialClpSignalDto> _signalRepository;
        private readonly IExtendedCRUD<MaterialDto> _repository;
        private IList<MaterialDto> _materialList;
        private IList<ClpHPcombineDto> _materialClpList;
        private IList<CmbUnitDto> _unitList;
        private IList<CmbCurrencyDto> _currencyList;
        private IList<CmbMaterialFunctionDto> _functionList;
        private BindingSource _materialBinding;
        private BindingSource _materialClpBinding;
        public MaterialDto CurrentMaterial;

        public MaterialService(SqlConnection connection, UserDto user, MaterialForm form)
            : base(FORM_DATA, form)
        {
            _connection = connection;
            _user = user;
            _form = form;

            _ghsRepository = new MaterialGHSRepository(_connection);
            _pCodeRepository = new MaterialPcodeRepository(_connection);
            _hCodeRepository = new MaterialHcodeRepository(_connection);
            _signalRepository = new MaterialSignalRepository(_connection);
            _repository = new MaterialRepository(_connection, this);
        }


        #region Modification markers

        public void Modify(RowState state)
        {
            if (_form.Init)
                return;

            if (state != RowState.UNCHANGED)
            {
                _form.ActivateSave(true);
                return;
            }
            else
                _form.ActivateSave(Status);
        }

        protected override bool Status => _materialList.Where(i => i.GetRowState != RowState.UNCHANGED).Any();

        #endregion


        #region Prepare Data

        protected override void PrepareColumns()
        {
            MaterialForm form = (MaterialForm)_baseForm;
            _fields = new Dictionary<DataGridView, IList<string>>
            {
                { form.GetDgvMaterial,  _dgvMaterialFields},
                { form.GetDgvClp, _dgvClpFields }
            };
        }

        public override void PrepareAllData()
        {
            #region Tables/Views/Bindings

            _materialList = _repository.GetAll();
            _materialBinding = new BindingSource();
            _materialBinding.DataSource = _materialList;
            _form.GetBindingNavigator.BindingSource = _materialBinding;
            _materialBinding.PositionChanged += MaterialBinding_PositionChanged;

            _materialClpList = new List<ClpHPcombineDto>();
            _materialClpBinding = new BindingSource();
            _materialClpBinding.DataSource = _materialClpList;

            IBasicCRUD<CmbUnitDto> repoUnit = new CmbUnitRepository(_connection);
            _unitList = repoUnit.GetAll();

            IBasicCRUD<CmbCurrencyDto> repoCurr = new CmbCurrencyRepository(_connection);
            _currencyList = repoCurr.GetAll();

            PrepareCmbMaterialFunction();

            #endregion

            PrepareClpAndComposition();
            PreparaeDgvMaterial();
            PrepareDgvClpMaterial();
            PrepareDgvComposition();
            PrepareCombBoxes();
            PrepareOtherControls();

            _form.GetPicClpImage.Size = new Size(300,200);

            MaterialBinding_PositionChanged(null, null);

            ShowMessage("Załadowano");
        }

        private void PreparaeDgvMaterial()
        {
            DataGridView view = _form.GetDgvMaterial;
            view.DataSource = _materialBinding;
            view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.RowsDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Regular);
            view.ColumnHeadersDefaultCellStyle.Font = new Font(view.DefaultCellStyle.Font.Name, 10, FontStyle.Bold);
            view.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            view.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            view.DefaultCellStyle.ForeColor = Color.Black;
            view.MultiSelect = false;
            view.SelectionMode = DataGridViewSelectionMode.CellSelect;
            view.ReadOnly = false;
            view.AutoGenerateColumns = false;
            view.AllowUserToResizeRows = false;

            view.Columns.Remove(INDEX);
            view.Columns.Remove(SUPPLIER_ID);
            view.Columns.Remove(FUNCTION_ID);
            view.Columns.Remove(IS_INTER);
            view.Columns.Remove(IS_OBSERVED);
            view.Columns.Remove(IS_PACKAGE);
            view.Columns.Remove(PRICE_QUANTITY);
            view.Columns.Remove(PRICE_TRANSPORT);
            view.Columns.Remove(QUANTITY);
            view.Columns.Remove(UNIT_ID);
            view.Columns.Remove(DENSITY);
            view.Columns.Remove(SOLIDS);
            view.Columns.Remove(ASH);
            view.Columns.Remove(VOC);
            view.Columns.Remove(REMARKS);
            view.Columns.Remove(DATE_CREATE);
            view.Columns.Remove(ROW_STATE);
            view.Columns.Remove(SERVICE);
            view.Columns.Remove(CRUD_STATE);
            view.Columns.Remove(GHS_LIST);
            view.Columns.Remove(HP_LIST);
            view.Columns.Remove(SIGNAL_WORD);

            view.Columns[ID].Visible = false;
            view.Columns[CURRENCY_ID].Visible = false;

            view.Columns[NAME].HeaderText = "Surowiec";
            view.Columns[NAME].DisplayIndex = 0;
            view.Columns[NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[NAME].Width = _formData.ContainsKey(NAME) ? (int)_formData[NAME] : STD_WIDTH;
            view.Columns[NAME].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[IS_ACTIVE].HeaderText = "Aktywny";
            view.Columns[IS_ACTIVE].DisplayIndex = 1;
            view.Columns[IS_ACTIVE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[IS_ACTIVE].Width = _formData.ContainsKey(IS_ACTIVE) ? (int)_formData[IS_ACTIVE] : STD_WIDTH;
            view.Columns[IS_ACTIVE].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[IS_DANGER].HeaderText = "CLP";
            view.Columns[IS_DANGER].DisplayIndex = 2;
            view.Columns[IS_DANGER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[IS_DANGER].Width = _formData.ContainsKey(IS_DANGER) ? (int)_formData[IS_DANGER] : STD_WIDTH;
            view.Columns[IS_DANGER].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[IS_PRODUCTION].HeaderText = "Prod.";
            view.Columns[IS_PRODUCTION].DisplayIndex = 3;
            view.Columns[IS_PRODUCTION].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[IS_PRODUCTION].Width = _formData.ContainsKey(IS_PRODUCTION) ? (int)_formData[IS_PRODUCTION] : STD_WIDTH;
            view.Columns[IS_PRODUCTION].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[PRICE].HeaderText = "Cena";
            view.Columns[PRICE].DisplayIndex = 4;
            view.Columns[PRICE].ReadOnly = true;
            view.Columns[PRICE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[PRICE].Width = _formData.ContainsKey(PRICE) ? (int)_formData[PRICE] : STD_WIDTH;
            view.Columns[PRICE].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[PRICE_UNIT].HeaderText = "Waluta";
            view.Columns[PRICE_UNIT].DisplayIndex = 5;
            view.Columns[PRICE_UNIT].ReadOnly = true;
            view.Columns[PRICE_UNIT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[PRICE_UNIT].Width = _formData.ContainsKey(PRICE_UNIT) ? (int)_formData[PRICE_UNIT] : STD_WIDTH;
            view.Columns[PRICE_UNIT].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[VOC_PROC].HeaderText = "VOC";
            view.Columns[VOC_PROC].DisplayIndex = 6;
            view.Columns[VOC_PROC].ReadOnly = true;
            view.Columns[VOC_PROC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[VOC_PROC].Width = _formData.ContainsKey(VOC_PROC) ? (int)_formData[VOC_PROC] : STD_WIDTH;
            view.Columns[VOC_PROC].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[DATE_UPDATE].HeaderText = "Data zmian";
            view.Columns[DATE_UPDATE].DisplayIndex = 7;
            view.Columns[DATE_UPDATE].ReadOnly = true;
            view.Columns[DATE_UPDATE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[DATE_UPDATE].Width = _formData.ContainsKey(DATE_UPDATE) ? (int)_formData[DATE_UPDATE] : STD_WIDTH;
            view.Columns[DATE_UPDATE].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (view.Rows.Count > 0)
            {
                view.CurrentCell = view.Rows[_materialBinding.Position].Cells[NAME];
            }
        }

        private void PrepareDgvClpMaterial()
        {
            DataGridView view = _form.GetDgvClp;
            view.DataSource = _materialClpBinding;
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

            view.Columns.Remove(MATERIAL_ID);
            view.Columns.Remove(ORDERING);
            view.Columns.Remove(CODE_ID);
            view.Columns.Remove(TYPE);

            view.Columns[CLASS_CLP].HeaderText = "Klasa";
            view.Columns[CLASS_CLP].DisplayIndex = 0;
            view.Columns[CLASS_CLP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CLASS_CLP].Width = _formData.ContainsKey(CLASS_CLP) ? (int)_formData[CLASS_CLP] : STD_WIDTH;
            view.Columns[CLASS_CLP].ReadOnly = true;
            view.Columns[CLASS_CLP].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[CODE_CLP].HeaderText = "Kod";
            view.Columns[CODE_CLP].DisplayIndex = 1;
            view.Columns[CODE_CLP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            view.Columns[CODE_CLP].Width = _formData.ContainsKey(CODE_CLP) ? (int)_formData[CODE_CLP] : STD_WIDTH;
            view.Columns[CODE_CLP].ReadOnly = true;
            view.Columns[CODE_CLP].SortMode = DataGridViewColumnSortMode.NotSortable;

            view.Columns[DESCRIPTION_CLP].HeaderText = "Opis";
            view.Columns[DESCRIPTION_CLP].DisplayIndex = 2;
            view.Columns[DESCRIPTION_CLP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            view.Columns[DESCRIPTION_CLP].Width = _formData.ContainsKey(DESCRIPTION_CLP) ? (int)_formData[DESCRIPTION_CLP] : STD_WIDTH;
            view.Columns[DESCRIPTION_CLP].ReadOnly = true;
            view.Columns[DESCRIPTION_CLP].SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private void PrepareDgvComposition()
        {

        }

        private void PrepareOtherControls()
        {
            _form.GetTxtName.DataBindings.Clear();
            _form.GetTxtIndex.DataBindings.Clear();
            _form.GetTxtDensity.DataBindings.Clear();
            _form.GetTxtSolids.DataBindings.Clear();
            _form.GetTxtAsh.DataBindings.Clear();
            _form.GetTxtVoc.DataBindings.Clear();
            _form.GetTxtPrice.DataBindings.Clear();
            _form.GetTxtPriceQuantity.DataBindings.Clear();
            _form.GetTxtTransport.DataBindings.Clear();
            _form.GetTxtQuantity.DataBindings.Clear();
            _form.GetTxtRemarks.DataBindings.Clear();
            _form.GetChbDanger.DataBindings.Clear();
            _form.GetChbActive.DataBindings.Clear();
            _form.GetChbPacking.DataBindings.Clear();
            _form.GetChbProduction.DataBindings.Clear();
            _form.GetChbSample.DataBindings.Clear();
            _form.GetChbSemiproduct.DataBindings.Clear();

            _form.GetTxtName.DataBindings.Add("Text", _materialBinding, NAME);
            _form.GetTxtIndex.DataBindings.Add("Text", _materialBinding, INDEX);
            _form.GetTxtRemarks.DataBindings.Add("Text", _materialBinding, REMARKS);
            _form.GetChbDanger.DataBindings.Add("Checked", _materialBinding, IS_DANGER);
            _form.GetChbActive.DataBindings.Add("Checked", _materialBinding, IS_ACTIVE);
            _form.GetChbPacking.DataBindings.Add("Checked", _materialBinding, IS_PACKAGE);
            _form.GetChbProduction.DataBindings.Add("Checked", _materialBinding, IS_PRODUCTION);
            _form.GetChbSample.DataBindings.Add("Checked", _materialBinding, IS_OBSERVED);
            _form.GetChbSemiproduct.DataBindings.Add("Checked", _materialBinding, IS_INTER);

            Binding price = new Binding("Text", _materialBinding, PRICE, true);
            price.Parse += Double_Parse;
            Binding priceQ = new Binding("Text", _materialBinding, PRICE_QUANTITY, true);
            priceQ.Parse += Double_Parse;
            Binding priceT = new Binding("Text", _materialBinding, PRICE_TRANSPORT, true);
            priceT.Parse += Double_Parse;
            Binding quant = new Binding("Text", _materialBinding, QUANTITY, true);
            quant.Parse += Double_Parse;
            Binding dens = new Binding("Text", _materialBinding, DENSITY, true);
            dens.Parse += Double_Parse;
            Binding solid = new Binding("Text", _materialBinding, SOLIDS, true);
            solid.Parse += Double_Parse;
            Binding ash = new Binding("Text", _materialBinding, ASH, true);
            ash.Parse += Double_Parse;
            Binding voc = new Binding("Text", _materialBinding, VOC, true);
            voc.Parse += Double_Parse;

            _form.GetTxtPrice.DataBindings.Add(price);
            _form.GetTxtPriceQuantity.DataBindings.Add(priceQ);
            _form.GetTxtTransport.DataBindings.Add(priceT);
            _form.GetTxtQuantity.DataBindings.Add(quant);
            _form.GetTxtDensity.DataBindings.Add(dens);
            _form.GetTxtSolids.DataBindings.Add(solid);
            _form.GetTxtAsh.DataBindings.Add(ash);
            _form.GetTxtVoc.DataBindings.Add(voc);

        }

        private void PrepareCombBoxes()
        {
            _form.GetCmbUnit.DataSource = _unitList;
            _form.GetCmbUnit.ValueMember = ID;
            _form.GetCmbUnit.DisplayMember = NAME_PL;
            _form.GetCmbUnit.SelectedIndexChanged += CmbUnit_SelectedIndexChanged;

            _form.GetCmbCurrency.DataSource = _currencyList;
            _form.GetCmbCurrency.ValueMember = ID;
            _form.GetCmbCurrency.DisplayMember = CURRENCY;
            _form.GetCmbCurrency.SelectedIndexChanged += CmbCurrency_SelectedIndexChanged;
        }

        private void PrepareClpAndComposition()
        {
            IBasicCRUD<ClpHPcombineDto> repo = new ClpHPcombineRepository(_connection);
            IList<ClpHPcombineDto> clpList = repo.GetAll();
            IList<MaterialClpGhsDto> ghsList = _ghsRepository.GetAll();
            IList<MaterialClpSignalDto> sigList = _signalRepository.GetAll();

            IBasicCRUD<MaterialCompositionDto> repoComposition = new MaterialCompositionRepository(_connection);
            IList<MaterialCompositionDto> compositionList = repoComposition.GetAll();

            IBasicCRUD<MaterialCompoundDto> repoCompound = new MaterialCompoundRepository(_connection);
            IList<MaterialCompoundDto> compoundList = repoCompound.GetAll();

            foreach (var ingredient in compositionList)
            {
                var compound = compoundList.Where(i => i.Id == ingredient.CompoundId).FirstOrDefault();
                ingredient.Compound = compound;
            }

            foreach (var material in _materialList)
            {
                if (material.IsDanger)
                {
                    var ghs = ghsList
                        .Where(i => i.MaterialId == material.Id)
                        .ToList();
                    material.GhsCodeList = ghs;

                    var clp = clpList
                        .Where(i => i.MaterialId == material.Id)
                        .ToList();
                    material.HPcodeList = clp;

                    var sig = sigList
                        .Where(i => i.MaterialId == material.Id)
                        .FirstOrDefault();
                    material.SignalWord = sig ?? new MaterialClpSignalDto(material.Id, 1, "-- Brak --", DateTime.Today);
                }

                var ingedrients = compositionList
                    .Where(i => i.MaterialId == material.Id)
                    .OrderBy(i => i.Ordering)
                    .ToList();
                material.MaterialCompositionList = ingedrients;
            }
        }

        private void PrepareCmbMaterialFunction()
        {
            IBasicCRUD<CmbMaterialFunctionDto> repoFunction = new CmbMaterialFunctionRepository(_connection);
            _functionList = repoFunction.GetAll();

            _form.GetCmbFunction.DataSource = null;
            _form.GetCmbFunction.DataSource = _functionList;
            _form.GetCmbFunction.ValueMember = ID;
            _form.GetCmbFunction.DisplayMember = NAME_PL;
            _form.GetCmbFunction.SelectedIndexChanged += CmbFunction_SelectedIndexChanged;

        }

        #endregion


        #region Current/Binkding/Navigation

        private void MaterialBinding_PositionChanged(object sender, System.EventArgs e)
        {
            _cmbBlock = true;

            #region Get Current Material

            if (_materialBinding == null || _materialBinding.Count == 0)
            {
                CurrentMaterial = null;
            }
            else
            {
                CurrentMaterial = (MaterialDto)_materialBinding.Current;
            }

            #endregion

            #region Set Current Controls

            if (CurrentMaterial != null)
            {
                DateTime date = Convert.ToDateTime(CurrentMaterial.DateCreated);
                string show = date.ToString("dd-MM-yyyy");
                _form.GetLblDateCreated.Text = "Utworzenie: " + show;
                _form.GetLblDateCreated.Left = _form.ClientSize.Width - _form.GetLblDateCreated.Width - 10;
            }
            else
            {
                _form.GetLblDateCreated.Text = "Utworzenie: Brak";
            }

            #endregion

            #region Synchronize CLP controls

            if (CurrentMaterial != null)
            {
                SetGhsImage(true);
                SetSignalWord(true);
                SynchronizeHPcode(true);
            }
            else
            {
                SetGhsImage(false);
                SetSignalWord(false);
                SynchronizeHPcode(false);
            }

            #endregion

            #region Synchronize Combo Gloss Units

            if (CurrentMaterial != null)
            {
                _form.GetCmbUnit.SelectedValue = CurrentMaterial.UnitId;
            }
            else
            {
                _form.GetCmbUnit.SelectedIndex = _form.GetCmbUnit.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo Currency

            if (CurrentMaterial != null)
            {
                _form.GetCmbCurrency.SelectedValue = CurrentMaterial.CurrencyId;
            }
            else
            {
                _form.GetCmbCurrency.SelectedIndex = _form.GetCmbCurrency.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize Combo Function

            if (CurrentMaterial != null)
            {
                _form.GetCmbFunction.SelectedValue = CurrentMaterial.FunctionId;
            }
            else
            {
                _form.GetCmbFunction.SelectedIndex = _form.GetCmbFunction.Items.Count > 0 ? 0 : -1;
            }

            #endregion

            #region Synchronize others controls

            SynchronizeOthersControls();

            #endregion

            _cmbBlock = false;
        }

        public void ChangePriceUnit(MaterialDto material)
        {
            string unitName = _unitList
                .Where(i => i.Id == material.UnitId)
                .Select(i => i.NamePl)
                .FirstOrDefault();

            string currName = _currencyList
                .Where(i => i.Id == material.CurrencyId)
                .Select(i => i.Currency)
                .FirstOrDefault();

            if (material.CurrencyId != 1 && !string.IsNullOrEmpty(currName))
            {
                material.PriceUnit = currName + " / " + unitName;
            }
            else
            {
                material.PriceUnit = "-- / " + unitName;
            }
        }

        private void SetGhsImage(bool danger)
        {
            //       1       5
            //   2       4       7
            //       3       6

            _form.GetPicClpImage.Image = null;
            if (!danger)
                return;

            if (CurrentMaterial.IsDanger)
            {
                var ghsList = CurrentMaterial.GhsCodeList;
                Bitmap bitmap = new Bitmap(300, 200);
                Graphics graphics = Graphics.FromImage(bitmap);

                int position = 0;
                foreach (var ghs in ghsList)
                {
                    graphics.DrawImage(CommonData.GhsImages[ghs.CodeId - 1], CommonData.GhsPoints[position]);
                    position++;
                }
                
                graphics.Dispose();
                _form.GetPicClpImage.Image = bitmap;
            }
        }

        private void SetSignalWord(bool danger)
        {
            if (!danger || CurrentMaterial.SignalWord.CodeId == 1)
            {
                _form.GetLblClpSignal.Text = "Nieszkodliwy";
                _form.GetLblClpSignal.ForeColor = Color.Blue;
            }
            else
            {
                _form.GetLblClpSignal.Text = CurrentMaterial.SignalWord.NamePl;
                _form.GetLblClpSignal.ForeColor = Color.Red;
            }
        }

        private void SynchronizeHPcode(bool danger)
        {
            if (danger)
            {
                _materialClpBinding.DataSource = new List<ClpHPcombineDto>();
                _materialClpList = CurrentMaterial.HPcodeList;
                _materialClpBinding.DataSource = _materialClpList;
            }
            else
            {
                _materialClpList = new List<ClpHPcombineDto>();
                _materialClpBinding.DataSource = _materialClpList;
            }
        }

        private void SynchronizeOthersControls()
        {
            _form.GetBtnClpEdit.Enabled = CurrentMaterial != null && _user.Permission.ToLower().Equals(ADMIN) && CurrentMaterial.IsDanger && CurrentMaterial.GetRowState != RowState.ADDED;
            _form.GetBtnDelete.Enabled = CurrentMaterial != null && _user.Permission.ToLower().Equals(ADMIN);
            _form.GetBtnNavigatorDelete.Enabled = _form.GetBtnDelete.Enabled;
        }

        public void ShowMessage(string message, bool frontColor = true)
        {
            _form.GetMessageLabel.Text = message;

            if (frontColor)
                _form.GetMessageLabel.ForeColor = Color.Blue;
            else
                _form.GetMessageLabel.ForeColor = Color.Red;
        }

        #endregion


        #region ComboBox Events

        private void CmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (CurrentMaterial != null)
            {
                CmbUnitDto cmb = (CmbUnitDto)_form.GetCmbUnit.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (CurrentMaterial.UnitId != cmbId))
                {
                    CurrentMaterial.UnitId = cmbId;
                    _materialBinding.EndEdit();
                }
            }
        }

        private void CmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (CurrentMaterial != null)
            {
                CmbCurrencyDto cmb = (CmbCurrencyDto)_form.GetCmbCurrency.SelectedItem;
                byte cmbId = cmb.Id;
                if (cmb != null && (CurrentMaterial.CurrencyId != cmbId))
                {
                    CurrentMaterial.CurrencyId = cmbId;
                    _materialBinding.EndEdit();
                }
            }
        }

        private void CmbFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbBlock)
                return;

            if (CurrentMaterial != null)
            {
                CmbMaterialFunctionDto cmb = (CmbMaterialFunctionDto)_form.GetCmbFunction.SelectedItem;
                short cmbId = cmb.Id;
                if (cmb != null && (CurrentMaterial.FunctionId != cmbId))
                {
                    CurrentMaterial.FunctionId = cmbId;
                    _materialBinding.EndEdit();
                }
            }
        }

        #endregion


        #region Parse Double

        private void Double_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value.Equals(""))
                e.Value = null;
        }

        #endregion


        #region Menu

        public void AddNewMaterial()
        {
            string permission = _user.Permission.ToLower();
            if (!permission.Equals(ADMIN))
                return;

            MaterialDto material = new MaterialDto.Builder()
                .Id(0)
                .Name("")
                .SupplierId(1)
                .FunctinoId(1)
                .IsIntermediate(false)
                .IsObserved(false)
                .IsActive(false)
                .Isdanger(false)
                .IsPackage(false)
                .IsProduction(false)
                .CurrencyId(1)
                .UnitId(1)
                .Service(this)
                .Build();

            CancelFilter(false);

            _materialBinding.Add(material);
            _materialBinding.EndEdit();
            _form.GetDgvMaterial.Refresh();

            _materialBinding.Position = _materialBinding.Count - 1;

            Modify(RowState.ADDED);
        }

        #endregion


        #region Open buttons

        public void OpenCLP()
        {
            if (CurrentMaterial == null)
                return;

            if (CurrentMaterial.GetRowState != RowState.UNCHANGED)
            {
                MessageBox.Show("Należy zapisać zmiany przed otwarciem edycją CLP.", "zapisz zmiany", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MaterialClpForm form = new MaterialClpForm(_connection, CurrentMaterial))
            {
                form.ShowDialog();
                if (form.GetBtnOk)
                {
                    CurrentMaterial.SignalWord = form.GetNewMaterialSignalWord;
                    CurrentMaterial.GhsCodeList = form.GetNewMaterialGhsList;
                    CurrentMaterial.HPcodeList = form.GetNewMaterialClpList;
                    SetGhsImage(true);
                    SetSignalWord(true);
                    SynchronizeHPcode(true);
                }
            }
        }

        public void OpenCurrency()
        {
            using (CurrencyForm form = new CurrencyForm(_connection))
            {
                form.ShowDialog();
            }
        }

        public void OpenFunction()
        {
            using (MaterialFunctionForm form = new MaterialFunctionForm(_connection))
            {
                form.ShowDialog();
                if (form.IsChanged)
                {
                    _cmbBlock = true;
                    PrepareCmbMaterialFunction();
                    _cmbBlock = false;
                    MaterialBinding_PositionChanged(null, null);
                }
            }
        }

        public void OpenComposition()
        {
            if (CurrentMaterial == null)
                return;

            using (MaterialCompositionForm form = new MaterialCompositionForm(_connection, CurrentMaterial))
            {
                form.ShowDialog();
            }
        }

        #endregion


        #region CRUD

        public override bool Save()
        {
            CancelFilter(false);
            _materialBinding.EndEdit();
            _form.GetDgvMaterial.EndEdit();

            #region

            var saveList = _materialList
                .Where(i => i.GetRowState == RowState.ADDED)
                .ToList();

            foreach(var material in saveList)
            {
                if (!CheckBeforeSave(material))
                {
                    int position = _materialList.IndexOf(material);
                    _materialBinding.Position = position;
                    ShowMeassageError(0);
                    return false;
                }

                CrudState saveState = _repository.Save(material).CrudState;
                if (saveState == CrudState.OK)
                {
                    material.SignalWord.MaterialId = material.Id;
                    _signalRepository.Save(material.SignalWord);
                    material.AcceptChanges();
                }
                else
                {
                    ShowMeassageError(1);
                    return false;
                }
            }

            #endregion

            #region

            var updateList = _materialList
                .Where(i => i.GetRowState == RowState.MODIFIED)
                .ToList();

            foreach (var material in updateList)
            {
                if (!CheckBeforeSave(material))
                {
                    int position = _materialList.IndexOf(material);
                    _materialBinding.Position = position;
                    ShowMeassageError(0);
                    return false;
                }

                CrudState saveState = _repository.Update(material).CrudState;
                if (saveState == CrudState.OK)
                {
                    material.AcceptChanges();
                }
                else
                {
                    ShowMeassageError(1);
                    return false;
                }
            }

            #endregion

            Modify(RowState.UNCHANGED);
            ShowMessage("Zapisano");

            return true;
        }

        public void Delete()
        {
            if (CurrentMaterial == null)
                return;

            if (MessageBox.Show("Czy usunać surowiec '" + CurrentMaterial.Name + "' z bazy danych? usunięcie jest nieodwracalne!", 
                "Usuwanie", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RemoveMaterial(CurrentMaterial);
            }
        }

        private bool RemoveMaterial(MaterialDto material)
        {
            _materialBinding.EndEdit();
            _form.GetDgvMaterial.EndEdit();
            
            if (material.Id > 0)
            {
                _materialBinding.RemoveCurrent();
                bool a1 = _repository.DeleteById(material.Id);
                bool a2 = _signalRepository.DeleteById(material.Id);
                bool a3 = _ghsRepository.DeleteById(material.Id);
                bool a4 = _pCodeRepository.DeleteById(material.Id);
                bool a5 = _hCodeRepository.DeleteById(material.Id);

                if (a1 && a2 && a3 && a4 && a5)
                    ShowMessage("Usunięto");
                else
                    ShowMessage("Błąd usuwania", false);
            }
            else
            {
                _materialBinding.RemoveCurrent();
            }

            return true;
        }

        private bool CheckBeforeSave(MaterialDto material)
        {
            return !string.IsNullOrEmpty(material.Name);
        }

        private void ShowMeassageError(int nr)
        {
            switch(nr)
            {
                case 0:
                    MessageBox.Show("Brak nazwy surowca. Nie mozna zapisac surowca bez nazwy. Uzupełnij nazwę.", "Brak nazwy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1:
                    MessageBox.Show("Błąd w czasie zapisu. Sprawdź wszystkie wypełnione pozycje.", "Błąd zapisu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }
            ShowMessage("Błąd zapisu", false);
        }

        #endregion


        #region DataGridView and others events

        public void DgvClpHPdataFormat(DataGridViewCellFormattingEventArgs e)
        {
            DataGridView view = _form.GetDgvClp;

            if (view.Columns[e.ColumnIndex].Name == CLASS_CLP)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 10, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.Red;
            }
            else if (view.Columns[e.ColumnIndex].Name == CODE_CLP)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 9, FontStyle.Bold);
                if (e.Value.ToString().Contains("EUH"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
                else if (e.Value.ToString().Contains("P"))
                {
                    e.CellStyle.ForeColor = Color.Magenta;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
            else
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.Name, 8, FontStyle.Regular);
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        public void DangerStateChanged()
        {
            bool enabled = false;

            if (_form.GetChbDanger.Checked)
                enabled = CurrentMaterial != null && _user.Permission.ToLower().Equals(ADMIN);

            _form.GetBtnClpEdit.Enabled = enabled;
            _form.GetBtnClp.Enabled = enabled;
        }

        public void ColumnWidthChanged()
        {
            DataGridView view = _form.GetDgvMaterial;
            int left;

            _form.GetBtnFilterCancel.Left = view.Left;
            _form.GetBtnFilterCancel.Width = view.RowHeadersWidth - 2;

            _form.GetTxtFilterName.Left = view.Left + view.RowHeadersWidth;
            _form.GetTxtFilterName.Width = view.Columns[NAME].Width;

            left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width 
                + (view.Columns[IS_ACTIVE].Width / 2);
            _form.GetChbFilterActive.Left = left - (_form.GetChbFilterActive.Width / 2);

            left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width
                + view.Columns[IS_ACTIVE].Width + (view.Columns[IS_DANGER].Width / 2);
            _form.GetChbFilterClp.Left = left - (_form.GetChbFilterClp.Width / 2);

            left = _form.GetTxtFilterName.Left + _form.GetTxtFilterName.Width
                + view.Columns[IS_ACTIVE].Width + view.Columns[IS_DANGER].Width + (view.Columns[IS_PRODUCTION].Width / 2);
            _form.GetChbFilterProd.Left = left - (_form.GetChbFilterProd.Width / 2);
        }

        public void CellvalueChanged(DataGridViewCellEventArgs e)
        {
            if (_form.GetDgvMaterial.Columns[e.ColumnIndex].Name.Equals(IS_DANGER))
            {
                SynchronizeOthersControls();
            }
        }

        #endregion


        #region Filtration

        public void Filtering()
        {
            if (_filterBlock)
                return;

            string name = _form.GetTxtFilterName.Text;
            bool isName = !string.IsNullOrEmpty(name);
            bool isActive = _form.GetChbFilterActive.Checked;
            bool isDanger = _form.GetChbFilterClp.Checked;
            bool isProd = _form.GetChbFilterProd.Checked;

            if (IsFilterSet())
            {
                SetFilter(_materialList);
                ShowMessage("Filtracja wyłączona");
                return;
            }

            IList<MaterialDto> filtered;

            filtered = _materialList
                .Where(i => isName ? i.Name.ToLower().Contains(name.ToLower()) : true)
                .Where(i => isActive ? i.IsActive : true)
                .Where(i => isDanger ? i.IsDanger : true)
                .Where(i => isProd ? i.IsProduction : true)
                .ToList();

            SetFilter(filtered);
        }

        public void SetFilter(IList<MaterialDto> filter)
        {
            _materialBinding.DataSource = filter;
            _materialBinding.Position = filter.Count > 0 ? 0 : -1;
            MaterialBinding_PositionChanged(null, null);
            ShowMessage("Filtracja włączona");
        }

        public void CancelFilter(bool GoToFirst)
        {
            if (IsFilterSet())
                return;

            _filterBlock = true;
            _form.GetTxtFilterName.Text = "";
            _form.GetChbFilterActive.Checked = false;
            _form.GetChbFilterClp.Checked = false;
            _form.GetChbFilterProd.Checked = false;

            _materialBinding.DataSource = _materialList;
            if (GoToFirst)
                _materialBinding.Position = 0;

            _filterBlock = false;

            ShowMessage("Filtrowanie wyłączone");
        }

        private bool IsFilterSet()
        {
            string name = _form.GetTxtFilterName.Text;
            bool isActive = _form.GetChbFilterActive.Checked;
            bool isDanger = _form.GetChbFilterClp.Checked;
            bool isProd = _form.GetChbFilterProd.Checked;

            return string.IsNullOrEmpty(name) && !isActive && !isDanger && !isProd;
        }

        #endregion
    }
}
